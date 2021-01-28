using System;
using System.Collections.Generic;
using System.Text;
using Model;
using NUnit.Framework;

namespace Race_SimulatorTest
{
    [TestFixture]
    public class Visualization_Misc
    {
        private static string[] _startN = { "|##|", "|1 |", "| 2|", "|  |" };
        private static string[] _startE = { "----", "  1#", " 2# ", "----" };
        private static string[] _startS = { "|  |", "|2 |", "|  1|", "|##|" };
        private static string[] _startW = { "----", "#1  ", " #2 ", "----" };
        private static string[] _finishN = { "|  |", "|==|", "|1 |", "| 2|" };
        private static string[] _finishE = { "----", " 1[]", "2 []", "----" };
        private static string[] _finishS = { "|2 |", "| 1|", "|==|", "|  |" };
        private static string[] _finishW = { "----", "[] 2", "[]1 ", "----" };
        private static string[] _straightN = { "|  |", "|1 |", "| 2|", "|  |" };
        private static string[] _straightE = { "----", "  1 ", " 2  ", "----" };
        private static string[] _straightS = { "|  |", "|2 |", "| 1|", "|  |" };
        private static string[] _straightW = { "----", "  2 ", " 1  ", "----" };
        private static string[] _leftN = { "--\\ ", " 1 \\", "  2|", "|  |" };
        private static string[] _leftE = { "|  |", " 1 |", "  2/", "--/ " };
        private static string[] _leftS = { "|  |", "|2  ", "\\ 1 ", " \\--" };
        private static string[] _leftW = { " /--", "/2  ", "| 1 ", "|  |" };
        private static string[] _rightN = { " /--", "/1  ", "| 2 ", "|  |" };
        private static string[] _rightE = { "--\\ ", "  1\\", " 2 |", "|  |" };
        private static string[] _rightS = { "|  |", " 2 |", "  1/", "--/ " };
        private static string[] _rightW = { "|  |", "|  2", "\\1  ", " \\--" };

        [SetUp]
        public void SetUp()
        {
        }
        [Test]
        public void ReplaceStrings_Test()
        {
            Car superzoef = new Car(9, 80, 10, false);
            Driver driver1 = new Driver("Lilith", 0, superzoef, TeamColors.Purple);

            Car ultrazoef = new Car(10, 100, 10, false);
            Driver driver2 = new Driver("Videospeler", 0, ultrazoef, TeamColors.Yellow);

            string one = "1";
            one = Race_Simulator.Visualization.ReplaceStrings(one, driver1, driver2);

            Assert.AreEqual("L", one);

            string two = "2";
            two = Race_Simulator.Visualization.ReplaceStrings(two, driver1, driver2);
            Assert.AreEqual("V", two);

            one = "1";
            one = Race_Simulator.Visualization.ReplaceStrings(one, driver2, driver1);

            Assert.AreEqual("V", one);

            two = "2";
            two = Race_Simulator.Visualization.ReplaceStrings(two, driver2, driver1);
            Assert.AreEqual("L", two);
        }
    }
}
