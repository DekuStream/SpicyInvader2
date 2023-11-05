using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invader_Réparé
{

    /// <summary>
    /// Represents a missile fired by an invader.
    /// </summary>
    public class InvaderMissile
    {
        /// <summary>
        /// Gets or sets the horizontal position of the missile.
        /// </summary>
        public int PositionX { get; set; }
        /// <summary>
        /// Gets or sets the vertical position of the missile.
        /// </summary>
        public int PositionY { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="InvaderMissile"/> class with a specific position.
        /// </summary>
        /// <param name="x">The horizontal position of the missile.</param>
        /// <param name="y">The vertical position of the missile.</param>
        public InvaderMissile(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }

        /// <summary>
        /// Moves the missile one unit downwards.
        /// </summary>
        public void Move()
        {
            PositionY++; // Moves missile down
        }
        /// <summary>
        /// Draws the missile on the console at the current position.
        /// </summary>
        public void Draw()
        {
            Console.SetCursorPosition(PositionX, PositionY);
            Console.Write("|");
        }
    }
}
