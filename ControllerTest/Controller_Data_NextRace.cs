using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Controller;
using Model;

namespace ControllerTest
{
    [TestFixture]
    class Controller_Data_NextRace
    {
        private Competition _competition;
        [SetUp]
        public void SetUp()
        {

            SectionTypes[] sections1 =
            {
                SectionTypes.StartGrid,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Finish,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner
            };
            Track testTrack1 = new Track("testTrack1", sections1);

            SectionTypes[] sections2 =
            {
                SectionTypes.StartGrid,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Finish,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner
            };
            Track testTrack2 = new Track("testTrack2", sections2);
            _competition.Tracks.Enqueue(testTrack1);
            _competition.Tracks.Enqueue(testTrack2);
            Car zoef = new Car(10, 10, 100, false);
            Driver hai = new Driver("Hai", 100, zoef, TeamColors.Green);

            Car minizoef = new Car(10, 10, 100, false);
            Driver gamer = new Driver("Gamer", 80, minizoef, TeamColors.Blue);
            _competition.Participants.Add(hai);
            _competition.Participants.Add(gamer);
        }

        [Test]
        public void NextRace_TwoTracks_CurrentTrack()
        {
            Assert.IsTrue(true);
        }
        //[Test]
        public void NextRace_TwoTracks_NextTrack()
        {
        }
    }
}
