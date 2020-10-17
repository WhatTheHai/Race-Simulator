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
            compass = 1;
        }
        #region graphics
        private static string[] _startN = { "|##|", "|1 |", "| 2|", "|  |" };
        private static string[] _startE = { "----", "  1#", " 2# ", "----" };
        private static string[] _startS = { "|  |", "|2 |", "|  1|", "|##|" };
        private static string[] _startW = { "----", "#1  ", " #2 ", "----" };

        private static string[] _finishN = { "|  |", "|--|", "|  |", "|  |" };
        private static string[] _finishE = { "----", "  | ", "  | ", "----" };
        private static string[] _finishS = { "|  |", "|  |", "|--|", "|  |" };
        private static string[] _finishW = { "----", " |  ", " |  ", "----" };

        private static string[] _straightHorizontal = { "----", "    ", "    ", "----" };
        private static string[] _straightVertical = { "|  |", "|  |", "|  |", "|  |" };

        private static string[] _bendSW = { "--\\", "   \\", "   |", "|  |" };
        private static string[] _bendSE = { " /--", "/   ", "|   ", "|  |" };
        private static string[] _bendNW = { "|  |", "   |", "   /", "--/ " };
        private static string[] _bendNE = { "|  |", "|   ", "\\   ", " \\--" };
        #endregion

        public static void DrawTrack(Track track)
        {
            if(track.Sections.Any())
            {
                setCursorPosition(track);

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
                            PrintFromCompass(_bendSW, _bendNW, _bendNE, _bendSE, section);
                            compass = Rotate(compass, "Left");
                            break;
                        case SectionTypes.RightCorner:
                            PrintFromCompass(_bendSE, _bendSW, _bendNW, _bendNE, section);
                            compass = Rotate(compass, "Right");
                            break;
                        case SectionTypes.Straight:
                            if (compass == 1 || compass == 3)
                            {
                                PrintSection(_straightHorizontal, section);
                            }
                            else
                            {
                                PrintSection(_straightVertical, section);
                            }
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
        public static void setCursorPosition(Track track)
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
            Console.WriteLine(globalX);
            Console.WriteLine(globalY);
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
            text = (first != null) ? text.Replace('1', first.Name[0]) : text.Replace('1', ' ');
            text = (second != null) ? text.Replace('2', second.Name[0]) : text.Replace('2', ' ');
            return text; 
        }

        public static void OnDriversChanged(object sender, EventArgs e)
        {
            DriversChangedEventArgs driverE = (DriversChangedEventArgs) e;
            DrawTrack(driverE.Track); 
        }
    }
}
 