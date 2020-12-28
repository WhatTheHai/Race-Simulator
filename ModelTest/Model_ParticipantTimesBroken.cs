using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using NUnit.Framework;

namespace ModelTest
{
    public class Model_ParticipantTimesBroken
    {
        private List<IDataConstraints> _list;
        [SetUp]
        public void Setup()
        {
            _list = new List<IDataConstraints>();
        }
        [Test]
        public void Add_SameParticipant_MultipleTimes_Sum()
        {
            TimeSpan time = new TimeSpan(1,1,1);
            string name = "Hai";
            var firstBrokenTime = new ParticipantTimesBroken() {Name = name, Time = time };
            var secondBrokenTime = new ParticipantTimesBroken() { Name = name, Time = time.Multiply(2) };
            var thirdBrokenTime = new ParticipantTimesBroken() { Name = name, Time = time.Multiply(3) };

            TimeSpan expectedTime = time.Multiply(6);

            firstBrokenTime.Add(_list);
            secondBrokenTime.Add(_list);
            thirdBrokenTime.Add(_list);

            TimeSpan timeActual = _list.Cast<ParticipantTimesBroken>().First().Time;
            Assert.AreEqual(expectedTime, timeActual);
        }
        [Test]
        public void Add_DifferentParticipants_MultipleTimes_Different()
        {
            TimeSpan time = new TimeSpan(1,2,3);
            var firstBrokenTime = new ParticipantTimesBroken() { Name = "a", Time = time };
            var secondBrokenTime = new ParticipantTimesBroken() { Name = "b", Time = time.Multiply(3) };
            var thirdBrokenTime = new ParticipantTimesBroken() { Name = "c", Time = time.Multiply(2) };

            firstBrokenTime.Add(_list);
            secondBrokenTime.Add(_list);
            thirdBrokenTime.Add(_list);

            TimeSpan firstTime = _list.Cast<ParticipantTimesBroken>().First().Time;
            TimeSpan secondTime = _list.Cast<ParticipantTimesBroken>().ElementAt(1).Time;
            TimeSpan thirdTime = _list.Cast<ParticipantTimesBroken>().Last().Time;


            Assert.AreEqual(time, firstTime);
            Assert.AreEqual(time.Multiply(3), secondTime);
            Assert.AreEqual(time.Multiply(2), thirdTime);
        }
        [Test]
        public void Add_DifferentParticipants_MultipleTimes_Different_BestParticipant()
        {
            TimeSpan time = new TimeSpan(1, 2, 3);
            var firstBrokenTime = new ParticipantTimesBroken() { Name = "a", Time = time };
            var secondBrokenTime = new ParticipantTimesBroken() { Name = "b", Time = time.Multiply(3) };
            var thirdBrokenTime = new ParticipantTimesBroken() { Name = "c", Time = time.Multiply(2) };

            firstBrokenTime.Add(_list);
            secondBrokenTime.Add(_list);
            thirdBrokenTime.Add(_list);

            string bestParticipant = firstBrokenTime.BestParticipant(_list);
            Assert.AreEqual("a", bestParticipant);
        }
    }
}
