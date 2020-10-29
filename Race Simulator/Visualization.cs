using Controller;
using Model;
using System;
using System.Collections;
using System.Globalization;
using System.Linq;

namespace Race_Simulator
{
    public static class Visualization
    {
        private static int compass;
        private static int trueX, trueY;
        public static void Initialize()
        {
            Console.CursorVisible = false;
            compass = 1;
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.NextRace += OnNextRace;
        }
        #region graphics
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
        #endregion

        public static void DrawTrack(Track track)
        {
            if(track.Sections.Any())
            {
                TrackSetCursorPosition(track);

                foreach (Section section in track.Sections)
                {
                    switch (section.SectionType)
                    {
                        case SectionTypes.StartGrid:
                            PrintFromCompass(_startN, _startE, _startS, _startW, section);
                            break;
                        case SectionTypes.Finish:
                            PrintFromCompass(_finishN, _finishE, _finishS, _finishW, section);
                            break;
                        case SectionTypes.LeftCorner:
                            PrintFromCompass(_leftN, _leftE, _leftS, _leftW, section);
                            compass = Rotate(compass, "Left");
                            break;
                        case SectionTypes.RightCorner:
                            PrintFromCompass(_rightN, _rightE, _rightS, _rightW, section);
                            compass = Rotate(compass, "Right");
                            break;
                        case SectionTypes.Straight:
                            PrintFromCompass(_straightN, _straightE, _straightS, _straightW, section);
                            break;
                    }
                    switch (compass)
                    {
                        case 0:
                            trueY -= 4;
                            break;
                        case 1:
                            trueX += 4;
                            break;
                        case 2:
                            trueY += 4;
                            break;
                        case 3:
                            trueX -= 4;
                            break;
                    }
                    Console.SetCursorPosition(trueX, trueY);
                }
            }
        }
        public static void TrackSetCursorPosition(Track track)
        {
            int x, y, globalX, globalY;
            x = y = globalX = globalY = 0;
            foreach(Section section in track.Sections)
            {
                switch (section.SectionType)
                {
                    case SectionTypes.LeftCorner:
                        compass = Rotate(compass, "Left");
                        break;
                    case SectionTypes.RightCorner:
                        compass = Rotate(compass, "Right");
                        break;
                }
                switch (compass)
                {
                    case 0:
                        y -= 4;
                        break;
                    case 1:
                        x += 4;
                        break;
                    case 2:
                        y += 4;
                        break;
                    case 3:
                        x -= 4;
                        break;
                }
                if(globalX > x)
                {
                    globalX = x;
                }
                if(globalY > y)
                {
                    globalY = y;
                }
            }
            Console.SetCursorPosition(-globalX, -globalY);
            compass = 1;
            trueX = -globalX;
            trueY = -(globalY - 4);
        }
        public static int Rotate(int Compass, string rotateDirection)
        {
            switch(rotateDirection)
            {
                case "Left":
                    Compass = (Compass < 1) ? 3 : Compass -= 1;
                    //If compass == 0, reset to the left, otherwise countdown.
                    return Compass;
                case "Right":
                    Compass = (Compass > 2) ? 0 : Compass += 1;
                    //Same as case left, but instead if compass == 3, reset to north
                    return Compass;
                default:
                    return -1;
            }
        }
        public static void PrintSection(string[] section, Section Section)
        {
            trueY -= 4;
            foreach (string s in section)
            {
                IParticipant left = Data.CurrentRace.GetSectionData(Section).Left;
                IParticipant right = Data.CurrentRace.GetSectionData(Section).Right;
                string replaced = ReplaceStrings(s, left, right);
                Console.SetCursorPosition(trueX, trueY);
                Console.WriteLine(replaced);
                trueY++;
            }
        }
        public static void PrintFromCompass(string[] north, string[] east, string[] south, string[] west, Section section)
        {
            switch (compass)
            {
                case 0:
                    PrintSection(north, section);
                    break;
                case 1:
                    PrintSection(east, section);
                    break;
                case 2:
                    PrintSection(south, section);
                    break;
                case 3:
                    PrintSection(west, section);
                    break;
            }
        }
        public static string ReplaceStrings(string text, IParticipant first, IParticipant second)
        {
            if (first != null)
            {
                if (!first.Equipment.IsBroken)
                {
                    text = text.Replace('1', first.Name[0]);
                }
                else
                {
                    text = text.Replace('1', char.ToLower(first.Name[0]));
                }
            }
            else
            {
                text = text.Replace('1', ' ');
            }
            if (second != null)
            {
                if (!second.Equipment.IsBroken)
                {
                    text = text.Replace('2', second.Name[0]);
                }
                else
                {
                    text = text.Replace('2', char.ToLower(second.Name[0]));
                }
            }
            else
            {
                text = text.Replace('2', ' ');
            }
            return text; 
        }

        public static void OnDriversChanged(object sender, EventArgs e)
        {
            DriversChangedEventArgs driverE = (DriversChangedEventArgs) e;
            DrawTrack(driverE.Track); 
        }

        public static void OnNextRace(object sender, EventArgs e)
        {
            Console.Clear();
            Data.CurrentRace.CleanupEvents();
            Data.NextRace();
            //Visualizer itself
            if (Data.CurrentRace != null)
            {
                Initialize();
            }
            else
            {
                Console.WriteLine("All races are finished.");
            }
        }
    }
}
 