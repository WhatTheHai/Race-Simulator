using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace ModelTest
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {
        private Competition _competition;
        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }
        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            Track testResult = _competition.NextTrack();
            Assert.IsNull(testResult);
        }
        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            SectionTypes[] sections =
            {
                SectionTypes.StartGrid,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Finish
            };
            Track Track = new Track("Test", sections);
            _competition.Tracks.Enqueue(Track);
            Track result = _competition.NextTrack();
            Assert.AreEqual(Track, result);
        }
        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            SectionTypes[] sections =
{
                SectionTypes.StartGrid,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Finish
            };
            Track Track = new Track("Test", sections);
            _competition.Tracks.Enqueue(Track);
            Track result = _competition.NextTrack();
            result = _competition.NextTrack();
            Assert.IsNull(result);
        }
        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            SectionTypes[] sections =
{
                SectionTypes.StartGrid,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Finish
            };
            SectionTypes[] sections2 =
{
                SectionTypes.StartGrid,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Finish
            };
            Track Track1 = new Track("Track1", sections);
            Track Track2 = new Track("Track1", sections2);
            _competition.Tracks.Enqueue(Track1);
            _competition.Tracks.Enqueue(Track2);
            Track result = _competition.NextTrack();
            Assert.AreEqual(Track1, result);
            result = _competition.NextTrack();
            Assert.AreEqual(Track2, result);
        }
    }
}
