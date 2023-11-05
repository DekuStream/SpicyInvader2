using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invader_Réparé
{
    /// <summary>
    /// Represents an invader in the game, with its own position and health.
    /// </summary>
    public class Invader
    {
        static Random random = new Random();

        /// <summary>
        /// Gets or sets the horizontal position of the invader.
        /// </summary>
        public int PositionX { get; set; } = random.Next(0, Console.WindowWidth - 1);

        /// <summary>
        /// Gets or sets the vertical position of the invader, initially at the top of the screen.
        /// </summary>
        public int PositionY { get; set; } = 0;

        /// <summary>
        /// Gets or sets the health of the invader.
        /// </summary>
        public int Health { get; set; } = 2;

        /// <summary>
        /// Moves the invader down the screen. If it reaches the bottom, 
        /// its position is reset to the top with a new random horizontal position.
        /// </summary>
        public void Move()
        {
            PositionY++;

            if (PositionY >= Console.WindowHeight)
            {
                PositionY = 0;
                PositionX = random.Next(0, Console.WindowWidth - 1);
            }
        }

        /// <summary>
        /// Draws the invader on the screen in different colors depending on its health.
        /// </summary>
        public void Draw()
        {
            if (Health > 0) // Only draw the invader if it is alive (has health).
            {
                Console.SetCursorPosition(PositionX, PositionY);
                if (Health == 2)
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (Health == 1)
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("W");
                Console.ResetColor();
            }
        }
    }
}
