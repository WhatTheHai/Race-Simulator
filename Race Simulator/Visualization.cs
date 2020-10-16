using Model;
using System;
using System.Collections;
using System.Linq;

namespace Race_Simulator
{
    public static class Visualization
    {
        private static int compass = 1;
        private static int trueX, trueY;
        public static void Initalize()
        {
            compass = 1;
        }
        #region graphics
        private static string[] _startGridHorizontal =
        {
            "----", "  || ", "  || ", "----"
        };
        private static string[] _startGridVertical =
        {
            "|  |", "|--|", "|  |", "|  |"
        };
        private static string[] _finishHorizontal =
        {
            "----", "  # ", "  # ", "----"
        };
        private static string[] _finishVertical =
{
            "|  |", "|##|", "|  |", "|  |"
        };
        private static string[] _straightHorizontal =
{
            "----", "    ", "    ", "----"
        };
        private static string[] _straightVertical =
{
            "|  |", "|  |", "|  |", "|  |"
        };
        private static string[] _bendSW =
        {
            "--\\", "   \\", "   |", "|  |"
        };
        private static string[] _bendSE =
        {
            " /--", "/   ", "|   ", "|  |"
        };
        private static string[] _bendNW =
        {
            "|  |", "   |", "   /", "--/ "
        };
        private static string[] _bendNE =
        {
            "|  |", "|   ", "\\   ", " \\--"
        };
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
                            if(compass == 1 || compass == 3)
                            {
                                PrintSection(_startGridHorizontal);
                            } else
                            {
                                PrintSection(_startGridVertical);
                            }
                            break;
                        case SectionTypes.Finish:
                            if (compass == 1 || compass == 3)
                            {
                                PrintSection(_finishHorizontal);
                            }
                            else
                            {
                                PrintSection(_finishVertical);
                            }
                            break;
                        case SectionTypes.LeftCorner:
                            switch (compass)
                            {
                                case 0:
                                    PrintSection(_bendSW);
                                    break;
                                case 1:
                                    PrintSection(_bendNW);
                                    break;
                                case 2:
                                    PrintSection(_bendNE);
                                    break;
                                case 3:
                                    PrintSection(_bendSE);
                                    break;
                            }
                            compass = Rotate(compass, "Left");
                            break;
                        case SectionTypes.RightCorner:
                            switch (compass)
                            {
                                case 0:
                                    PrintSection(_bendSE);
                                    break;
                                case 1:
                                    PrintSection(_bendSW);
                                    break;
                                case 2:
                                    PrintSection(_bendNW);
                                    break;
                                case 3:
                                    PrintSection(_bendNE);
                                    break;
                            }
                            compass = Rotate(compass, "Right");
                            break;
                        case SectionTypes.Straight:
                            if (compass == 1 || compass == 3)
                            {
                                PrintSection(_straightHorizontal);
                            }
                            else
                            {
                                PrintSection(_straightVertical);
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
            x = y = 0;
            globalX = globalY = 0;
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
        public static void PrintSection(string[] section)
        {
            trueY -= 4;
            foreach (string s in section)
            {
                Console.SetCursorPosition(trueX, trueY);
                Console.WriteLine(s);
                trueY++;

            }
        }
    }
}
 