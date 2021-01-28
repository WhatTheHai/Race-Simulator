using System.Drawing;
using Model;
using NUnit.Framework;
using RaceWPF;

namespace RaceWPFTest
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }

        [TestCase(TeamColors.Green, false, ".\\Assets\\Cars\\Cars\\Green.png")]
        [TestCase(TeamColors.Red, false, ".\\Assets\\Cars\\Cars\\Red.png")]
        [TestCase(TeamColors.Blue, false, ".\\Assets\\Cars\\Cars\\Blue.png")]
        [TestCase(TeamColors.Purple, false, ".\\Assets\\Cars\\Cars\\Purple.png")]
        [TestCase(TeamColors.Grey, false, ".\\Assets\\Cars\\Cars\\Grey.png")]
        [TestCase(TeamColors.Yellow, false, ".\\Assets\\Cars\\Cars\\Yellow.png")]
        [TestCase(TeamColors.Green, true, ".\\Assets\\Cars\\Broken\\Green.png")]
        [TestCase(TeamColors.Red, true, ".\\Assets\\Cars\\Broken\\Red.png")]
        [TestCase(TeamColors.Blue, true, ".\\Assets\\Cars\\Broken\\Blue.png")]
        [TestCase(TeamColors.Purple, true, ".\\Assets\\Cars\\Broken\\Purple.png")]
        [TestCase(TeamColors.Grey, true, ".\\Assets\\Cars\\Broken\\Grey.png")]
        [TestCase(TeamColors.Yellow, true, ".\\Assets\\Cars\\Broken\\Yellow.png")]
        public void TestAll_TeamColours_GetTeamColour(TeamColors teamColor, bool brokenStatus, string expected)
        {
            var result = RaceWPF.Visualization.GetTeamColour(teamColor, brokenStatus);
            Assert.AreEqual(expected,result);
        }

    }
}