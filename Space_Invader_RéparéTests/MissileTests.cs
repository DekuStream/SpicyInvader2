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
    public class MissileTests
    {
        [TestMethod()]
        public void MoveTest()
        {
            Missile missile = new Missile(2,4);

            missile.Move();

            Assert.AreEqual(3, missile.PositionY);
            Assert.AreEqual(2, missile.PositionX);
        }
            
    }
}