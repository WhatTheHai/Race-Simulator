using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace ModelTest
{
    [TestFixture]
    public class Model_Competition_AddCarParticipant
    {
        private Competition _competition;
        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }
        [Test]
        public void Driver_Contains_Everything()
        {
            string name = "Hai";
            Car zoef = new Car(10, 10, 100, false);
            Driver hai = new Driver(name, 100, zoef, TeamColors.Green);
            Assert.AreEqual(hai.Name, name);
            Assert.AreEqual(hai.Points, 100);
            Assert.AreEqual(hai.Equipment, zoef);
            Assert.AreEqual(hai.TeamColor, TeamColors.Green);
        }
        [Test]
        public void Competition_Contains_Participant()
        {
            Car zoef = new Car(10, 10, 100, false);
            Driver hai = new Driver("Hai", 100, zoef, TeamColors.Green);
            _competition.Participants.Add(hai);
            Assert.IsTrue(_competition.Participants.Contains(hai));
        }
    }
}