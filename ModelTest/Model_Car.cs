using System;
using System.Collections.Generic;
using System.Text;
using Model;
using NUnit.Framework;

namespace ModelTest
{
    public class Model_Car
    {
        [Test]
        public void Car_exists()
        {
            int quality = 9;
            int performance = 100;
            int speed = 10;
            bool brokenStatus = false;
            Car testCar = new Car(quality, performance, speed, brokenStatus);

            Assert.AreEqual(quality, testCar.Quality);
            Assert.AreEqual(performance, testCar.Performance);
            Assert.AreEqual(speed, testCar.Speed);
            Assert.AreEqual(brokenStatus, testCar.IsBroken);
        }

    }
}
