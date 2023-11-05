using Space_Invader_Réparé;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Space_Invader_Réparé
{
    /// <summary>
    /// Represents the player in the game.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// The horizontal position of the player on the screen.
        /// </summary>
        public int Position { get; set; } = 10;

        /// <summary>
        /// The current health of the player.
        /// </summary>
        public int Health { get; set; } = 3;

        /// <summary>
        /// Draws the player at the current position with a color that reflects their health.
        /// </summary>
        public void Draw()
        {
            Console.SetCursorPosition(Position, Console.WindowHeight - 1);
            // Change color based on the player's health
            switch (Health)
            {
                case 3:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
            Console.Write("A"); // Representation of the player on the screen
            Console.ResetColor(); // Resets to the default color for the following text
        }

        /// <summary>
        /// Handles user input to move the player or fire a missile.
        /// </summary>
        public void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Moves the player left if the left arrow is pressed and the player isn't already at the left edge
                if (key.Key == ConsoleKey.LeftArrow && Position > 0)
                {
                    Position--;
                }
                // Moves the player right if the right arrow is pressed and the player isn't already at the right edge
                else if (key.Key == ConsoleKey.RightArrow && Position < Console.WindowWidth - 1)
                {
                    Position++;
                }
                // If the spacebar is pressed, fire a missile from the player's current position
                else if (key.Key == ConsoleKey.Spacebar)
                {
                    Game.AddMissile(new Missile(Position, Console.WindowHeight - 2));
                }
            }
        }
    }
}
