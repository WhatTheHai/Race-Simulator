using System;
using System.Collections.Generic;
using System.Text;
using Controller;
using NUnit.Framework;

namespace ControllerTest
{
    class Controller_Data_Initialize
    {
        [Test]
        public void Initialize_Contains_Something()
        {
            Data.Initialize();
            Assert.IsTrue(Data.Competition.Participants.Count > 0);
            Assert.IsTrue(Data.Competition.Tracks.Count > 0);
        }
    }
}
