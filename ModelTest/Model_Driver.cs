using System;
using System.Collections.Generic;
using System.Text;
using Model;
using NUnit.Framework;

namespace ModelTest
{
    [TestFixture]
    public class Model_Driver
    {
        [Test]
        public void Driver_exists()
        {
            string name = "Hai";
            int points = 100;
            Car testCar = new Car(10, 100, 10, false);
            TeamColors teamColor = TeamColors.Green;
            Driver driver = new Driver(name, points, testCar, teamColor);

            Assert.AreEqual(name, driver.Name);
            Assert.AreEqual(points, driver.Points);
            Assert.AreEqual(teamColor, driver.TeamColor);
            Assert.AreEqual(testCar, driver.Equipment);
        }
        
    }
}
