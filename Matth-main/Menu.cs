using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Space_Invader_Réparé
{
    /// <summary>
    /// Represents the main menu for the game with options to start playing, view scores, or exit.
    /// </summary>
    public class Menu
    {
        static Game game = new Game();
        static DatabaseHandler SQL = new DatabaseHandler();

        /// <summary>
        /// Displays the main menu and handles the user input to navigate through the options.
        /// </summary>
        public void Afficher()
        {
            bool continuer = true;

            while (continuer)
            {
                AfficherOptions();

                char choix = Console.ReadKey().KeyChar;

                switch (choix)
                {
                    case '1':
                        Jouer();
                        break;
                    case '2':
                        AfficherScore();
                        break;
                    case '3':
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("\nOption invalide. Veuillez choisir une option valide.");
                        break;
                }
            }
        }

        /// <summary>
        /// Displays the different options available in the main menu.
        /// </summary>
        private void AfficherOptions()
        {
            Console.Clear();
            Console.WriteLine("------------ Spicy Invaders ------------");
            Console.WriteLine();
            Console.WriteLine("1. Jouer");
            Console.WriteLine("2. Score");
            Console.WriteLine("3. Quitter");
            Console.WriteLine();
            Console.Write("Entrez votre choix: ");
        }

        /// <summary>
        /// Initiates the gameplay by starting the game.
        /// </summary>
        private void Jouer()
        {
            game.StartGame();
        }

        /// <summary>
        /// Displays the high scores by fetching them from the database.
        /// </summary>
        private void AfficherScore()
        {
            SQL.GetTopScores();
        }
    }
}

