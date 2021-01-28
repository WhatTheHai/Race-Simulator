using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using NUnit.Framework;

namespace ModelTest
{
    public class Model_ParticipantSpeedPerTrack
    {
        private List<IDataConstraints> _list;
        private Track _track;
        private Track _track2;
        [SetUp]
        public void Setup()
        {
            _list = new List<IDataConstraints>();
            SectionTypes[] sections1 = {
                SectionTypes.StartGrid,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Finish,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner
            };
            _track = new Track("test", sections1);
            SectionTypes[] sections2 = {
                SectionTypes.StartGrid,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Finish,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner
            };
            _track2 = new Track("test2", sections2);
        }

        [Test]
        public void Add_SameParticipant_DifferentCars_track1()
        {
            string name = "Hai";
            var firstSpeed = new ParticipantSpeedPerTrack()
            {
                Name = name,Performance = 89,Speed = 10,Track =  _track
            };
            var secondSpeed = new ParticipantSpeedPerTrack()
            {
                Name = name,
                Performance = 99,
                Speed = 9,
                Track = _track
            };
            var thirdSpeed = new ParticipantSpeedPerTrack()
            {
                Name = name,
                Performance = 100,
                Speed = 9,
                Track = _track2
            };

            firstSpeed.Add(_list);
            secondSpeed.Add(_list);
            thirdSpeed.Add(_list);

            string expectName = _list.Cast<ParticipantSpeedPerTrack>().First().Name;
            int expectPerf = _list.Cast<ParticipantSpeedPerTrack>().First().Performance;
            int expectSpeed = _list.Cast<ParticipantSpeedPerTrack>().First().Speed;
            Track expectTrack = _list.Cast<ParticipantSpeedPerTrack>().First().Track;

            Assert.AreEqual(expectName, firstSpeed.Name);
            Assert.AreEqual(expectPerf, firstSpeed.Performance);
            Assert.AreEqual(expectSpeed, firstSpeed.Speed);
            Assert.AreEqual(expectTrack, firstSpeed.Track);
        }
        [Test]
        public void Add_SameParticipant_DifferentCars_BestParticipant_Track1()
        {
            var firstSpeed = new ParticipantSpeedPerTrack()
            {
                Name = "a",
                Performance = 91,
                Speed = 10,
                Track = _track
            };
            var secondSpeed = new ParticipantSpeedPerTrack()
            {
                Name = "b",
                Performance = 99,
                Speed = 9,
                Track = _track
            };
            var thirdSpeed = new ParticipantSpeedPerTrack()
            {
                Name = "c",
                Performance = 100,
                Speed = 9,
                Track = _track2
            };

            firstSpeed.Add(_list);
            secondSpeed.Add(_list);
            thirdSpeed.Add(_list);

            string actualString = firstSpeed.BestParticipant(_list);

            string expected = "a in the track test";
            Assert.AreEqual(expected, actualString);
        }
        [Test]
        public void Add_SameParticipant_DifferentCars_BestParticipant_Track2()
        {
            var firstSpeed = new ParticipantSpeedPerTrack()
            {
                Name = "a",
                Performance = 89,
                Speed = 10,
                Track = _track
            };
            var secondSpeed = new ParticipantSpeedPerTrack()
            {
                Name = "b",
                Performance = 99,
                Speed = 9,
                Track = _track
            };
            var thirdSpeed = new ParticipantSpeedPerTrack()
            {
                Name = "c",
                Performance = 100,
                Speed = 9,
                Track = _track2
            };

            firstSpeed.Add(_list);
            secondSpeed.Add(_list);
            thirdSpeed.Add(_list);

            string actualString = firstSpeed.BestParticipant(_list);

            string expected = "c in the track test2";
            Assert.AreEqual(expected, actualString);
        }
    }
}
