using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Model;

namespace ModelTest
{
    public class Model_ParticipantPoints
    {
        private List<IDataConstraints> _list;
        [SetUp]
        public void Setup()
        {
            _list = new List<IDataConstraints>();
        }
        [Test]
        public void Add_SameParticipant_MultiplePoints_Sum()
        {
            string sameName = "Hai";
            var firstPoint = new ParticipantPoints() { Name = sameName, Points = 12};
            var secondPoint = new ParticipantPoints() { Name = sameName, Points = 8 };
            var thirdPoint = new ParticipantPoints() { Name = sameName, Points = 10 };
            int totalPoints = firstPoint.Points + secondPoint.Points + thirdPoint.Points;

            firstPoint.Add(_list);
            secondPoint.Add(_list);
            thirdPoint.Add(_list);

            int listPoints = _list.Cast<ParticipantPoints>().First().Points;
            Assert.AreEqual(totalPoints, listPoints);
        }
        [Test]
        public void Add_DifferentParticipants_MultiplePoints_Different()
        {
            var firstPoint = new ParticipantPoints() { Name = "a", Points = 12 };
            var secondPoint = new ParticipantPoints() { Name = "b", Points = 16 };
            var thirdPoint = new ParticipantPoints() { Name = "c", Points = 8 };
            int totalPoints = firstPoint.Points + secondPoint.Points + thirdPoint.Points;

            firstPoint.Add(_list);
            secondPoint.Add(_list);
            thirdPoint.Add(_list);

            int firstListPoint = _list.Cast<ParticipantPoints>().First().Points;
            int secondListPoint = _list.Cast<ParticipantPoints>().ElementAt(1).Points;
            int thirdListPoint = _list.Cast<ParticipantPoints>().Last().Points;


            Assert.AreEqual(firstPoint.Points, firstListPoint);
            Assert.AreEqual(secondPoint.Points, secondListPoint);
            Assert.AreEqual(thirdPoint.Points, thirdListPoint);
        }
        [Test]
        public void Add_DifferentParticipants_MultiplePoints_Different_BestParticipant()
        {
            var firstPoint = new ParticipantPoints() { Name = "a", Points = 12 };
            var secondPoint = new ParticipantPoints() { Name = "b", Points = 16 };
            var thirdPoint = new ParticipantPoints() { Name = "c", Points = 8 };
            int totalPoints = firstPoint.Points + secondPoint.Points + thirdPoint.Points;

            firstPoint.Add(_list);
            secondPoint.Add(_list);
            thirdPoint.Add(_list);

            string bestParticipant = firstPoint.BestParticipant(_list);
            Assert.AreEqual(secondPoint.Name, bestParticipant);
        }
    }
}
