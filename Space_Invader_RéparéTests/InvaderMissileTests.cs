using Microsoft.VisualStudio.TestTools.UnitTesting;
using Space_Invader_Réparé;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invader_Réparé.Tests
{
    [TestClass()]
    public class InvaderMissileTests
    {


        /// <summary>
        /// Tests the Move method of InvaderMissile.
        /// </summary>
        [TestMethod()]
        public void MoveTest()
        {
            // Arrange: Initialize a new InvaderMissile with a starting position (2, 4).
            InvaderMissile missile = new InvaderMissile(2,4);

            // Act: Invoke the Move method to change the missile's position.
            missile.Move();

            Assert.AreEqual(5,missile.PositionY);
            Assert.AreEqual(2,missile.PositionX);
            
        }
    }
}