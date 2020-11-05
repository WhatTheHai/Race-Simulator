using System;
using System.Collections.Generic;
using System.Text;
using Model;
using NUnit.Framework;

namespace ModelTest
{
    class Model_SectionData
    {
        private Driver _driver;
        private Driver _driver2;
        [SetUp]
        public void SetUp()
        {
            Car testCar = new Car(10, 10, 10, false);
            _driver = new Driver("test", 0, testCar, TeamColors.Green);
            _driver2 = new Driver("test2", 10, testCar, TeamColors.Blue);
        }

        [Test]
        public void Left_SetParticipantAndDistance()
        {
            SectionData sectionData = new SectionData();
            int testDistance = Section.Length - 1;
            sectionData.Left = _driver;
            sectionData.DistanceLeft = testDistance;

            Assert.AreEqual(_driver, sectionData.Left);
            Assert.AreEqual(testDistance, sectionData.DistanceLeft);
        }
        [Test]
        public void Right_SetParticipantAndDistance()
        {
            SectionData sectionData = new SectionData();
            int testDistance = Section.Length - 1;
            sectionData.Right = _driver;
            sectionData.DistanceRight = testDistance;

            Assert.AreEqual(_driver, sectionData.Right);
            Assert.AreEqual(testDistance, sectionData.DistanceRight);
        }
        [Test]
        public void Fill_SectionData_FourVariables()
        {
            int leftDistance = 100;
            int rightDistance = 200;
            SectionData sectionData = new SectionData(_driver, leftDistance, _driver2, rightDistance);
            Assert.AreEqual(_driver, sectionData.Left);
            Assert.AreEqual(_driver2, sectionData.Right);
            Assert.AreEqual(leftDistance, sectionData.DistanceLeft);
            Assert.AreEqual(rightDistance, sectionData.DistanceRight);
        }
    }
}
