using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using static System.Formats.Asn1.AsnWriter;
using MySql.Data.MySqlClient;

namespace Space_Invader_Réparé
{
    public class Game
    {

        static DatabaseHandler dbHandler = new DatabaseHandler();


        /// <summary>
        /// The main player of the game.
        /// </summary>
        static Player player = new Player();

        /// <summary>
        /// List of all invaders present in the game.
        /// </summary>
        static List<Invader> invaders = new List<Invader>();

        /// <summary>
        /// List of all missiles shot by the player.
        /// </summary>
        static List<Missile> missiles = new List<Missile>();

        /// <summary>
        /// List of all missiles shot by the invaders.
        /// </summary>
        static List<InvaderMissile> invaderMissiles = new List<InvaderMissile>();

        /// <summary>
        /// Random number generator, used for random events like shooting.
        /// </summary>
        static Random random = new Random();

        /// <summary>
        /// The player's current score.
        /// </summary>
        static int score = 0;

        /// <summary>
        /// Counter to determine when invaders should move.
        /// </summary>
        private int moveCounter = 0;

        /// <summary>
        /// Frequency at which invaders should move.
        /// </summary>
        private int moveFrequency = 20;

        private string playerPseudo;

        /// <summary>
        /// Adds a missile to the game's missile list.
        /// </summary>
        /// <param name="missile">The missile to add.</param>
        public static void AddMissile(Missile missile)
        {
            missiles.Add(missile);
        }

        /// <summary>
        /// Begins the game loop, setting up and updating game entities.
        /// </summary>
        public void StartGame()
        {
            playerPseudo = GetPlayerPseudo();
            for (int i = 0; i < 10; i++)
            {
                invaders.Add(new Invader());
            }

            while (true)
            {
                Draw();
                player.HandleInput();
                UpdateMissiles();
                UpdateInvaderMissiles();
                CheckCollisions();
                InvadersShoot();
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Draws the game entities on the console.
        /// </summary>
        public void Draw()
        {
            Console.Clear();

            moveCounter++;
            if (moveCounter % moveFrequency == 0)
            {
                foreach (var invader in invaders)
                {
                    invader.Move(); 
                }
            }

            foreach (var invader in invaders)
            {
                invader.Draw();
            }

            foreach (var missile in missiles)
                missile.Draw();

            foreach (var invMissile in invaderMissiles)
                invMissile.Draw(); 


            player.Draw();
            DrawScore(); // Call this method every time the frame is redrawn
        }

        /// <summary>
        /// Draws the player's current score on the console.
        /// </summary>
        public void DrawScore()
        {
            Console.SetCursorPosition(Console.WindowWidth - 10, 0); 
            Console.Write("Score: " + score); 
        }


        /// <summary>
        /// Updates the position of the missiles.
        /// </summary>

        public void UpdateMissiles()
        {
            for (int i = missiles.Count - 1; i >= 0; i--)
            {
                missiles[i].Move();

                if (missiles[i].PositionY < 0)
                    missiles.RemoveAt(i); // Remove missile if out of bounds
            }
        }

        /// <summary>
        /// Checks for collisions between missiles and invaders or the player.
        /// </summary>
        public void CheckCollisions()
        {
            for (int i = missiles.Count - 1; i >= 0; i--)
            {
                for (int j = invaders.Count - 1; j >= 0; j--)
                {
                    if (missiles[i].PositionX == invaders[j].PositionX &&
                        missiles[i].PositionY == invaders[j].PositionY &&
                        invaders[j].Health > 0)
                    {
                        // Collision detected!

                        invaders[j].Health -= 1; // Reduces the invader's health

                        if (invaders[j].Health <= 0)
                        {
                            score += 180; //Increase the score by 10 points if the invader is destroyed
                        }

                        missiles.RemoveAt(i); // Remove the player's missile after the collision
                        break; // Since this missile is deleted, move on to the next missile
                    }
                }
            }

            // After handling missile collisions with invaders:
            bool allInvadersDestroyed = true; // Assume all invaders are destroyed
            foreach (var invader in invaders)
            {
                if (invader.Health > 0)
                {
                    allInvadersDestroyed = false; // An invader is still alive
                    break;
                }
            }

            if (allInvadersDestroyed)
            {
                SaveScoreToDatabase(playerPseudo, score);
                Console.Clear();
                Console.WriteLine($"Bravo! Votre score est : {score}");
                Thread.Sleep(5000); // Wait 5 seconds for the player to see the message
                Environment.Exit(0); // Complete the game, you could also start the next level here
            }
        }


        /// <summary>
        /// Updates the position of the missiles shot by the invaders.
        /// </summary>
        public void UpdateInvaderMissiles()
        {
            for (int i = invaderMissiles.Count - 1; i >= 0; i--)
            {
                invaderMissiles[i].Move();

                // Check if an invader missile has reached the player's position
                if (invaderMissiles[i].PositionY >= Console.WindowHeight - 1 && invaderMissiles[i].PositionX == player.Position)
                {
                    // Collision detected with the player
                    player.Health--; // Decrease the player's health
                    invaderMissiles.RemoveAt(i); // Delete the missile

                    // If the player's health is zero, end the game
                    if (player.Health <= 0)
                    {
                        SaveScoreToDatabase(playerPseudo, score);
                        Console.Clear();
                        Console.WriteLine("Game Over!");

                        // Offer to play again
                        Console.WriteLine("Voulez-vous rejouer? (y/n)");
                        char choix = Console.ReadKey().KeyChar;
                        if (choix == 'y' || choix == 'Y')
                        {
                            // Reset the game and start it again.
                            StartGame();
                        }
                        else
                        {
                            Environment.Exit(0); // If the player doesn't want to play again, quit the game.
                        }
                    }
                }
                else if (invaderMissiles[i].PositionY >= Console.WindowHeight)
                {
                    invaderMissiles.RemoveAt(i); // Delete the missile if it goes off screen
                }
            }
        }

        /// <summary>
        /// Makes the invaders shoot missiles.
        /// </summary>
        public void InvadersShoot()
        {
            foreach (var invader in invaders)
            {
                if (invader.Health > 0 && random.Next(100) < 2) // 2% chance for each living invader to shoot
                {
                    invaderMissiles.Add(new InvaderMissile(invader.PositionX, invader.PositionY + 1));
                }
            }
        }


        /// <summary>
        /// Resets the game to its initial state.
        /// </summary>
        public static void ResetGame()
        {
            player = new Player();
            invaders.Clear();
            missiles.Clear();
            invaderMissiles.Clear();
            score = 0;

            for (int i = 0; i < 5; i++)
            {
                invaders.Add(new Invader());
            }
        }
        /// <summary>
        /// Gets the current number of missiles in the game.
        /// </summary>
        /// <returns>The number of missiles.</returns>
        public static int GetMissileCount()
        {
            return missiles.Count;
        }

        /// <summary>
        /// Prompts the user for their pseudo (nickname) and retrieves it.
        /// </summary>
        /// <returns>The pseudo entered by the user.</returns>
        public string GetPlayerPseudo()
        {
            Console.Clear();
            Console.WriteLine("Veuillez entrer votre pseudo:");
            string pseudo = Console.ReadLine();
            return pseudo;
        }

        /// <summary>
        /// Saves the player's score to the database using the provided pseudo.
        /// </summary>
        /// <param name="pseudo">The pseudo associated with the player's score.</param>
        /// <param name="playerScore">The score achieved by the player.</param>
        public void SaveScoreToDatabase(string pseudo, int playerScore)
        {
            dbHandler.SavePlayerScore(pseudo, playerScore);
        }
    }
    


}

