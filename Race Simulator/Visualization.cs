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
            "----",
            "  || ",
            "  || ",
            "----"
        };
        private static string[] _startGridVertical =
{
            "|  |",
            "|--|",
            "|  |",
            "|  |"
        };
        private static string[] _finishHorizontal =
        {
            "----",
            "  # ",
            "  # ",
            "----"
        };
        private static string[] _finishVertical =
{
            "|  |",
            "|##|",
            "|  |",
            "|  |"
        };
        private static string[] _straightHorizontal =
{
            "----",
            "    ",
            "    ",
            "----"
        };
        private static string[] _straightVertical =
{
            "|  |",
            "|  |",
            "|  |",
            "|  |"
        };
        private static string[] _bendSW =
        {
            "--\\ ",
            "   \\",
            "\\  |",
            "|  |"
        };
        private static string[] _bendSE =
        {
            " /--",
            "/   ",
            "|   /",
            "|   |"
        };
        private static string[] _bendNW =
        {
            "|  |",
            "/  |",
            "   /",
            "--/ "
        };
        private static string[] _bendNE =
        {
            "|  |",
            "|  \\",
            "\\   ",
            " \\--"
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
                                    PrintSection(_bendNW);
                                    break;
                                case 1:
                                    PrintSection(_bendNE);
                                    break;
                                case 2:
                                    PrintSection(_bendSW);
                                    break;
                                case 3:
                                    break;
                            }
                            Rotate(compass, "Left");
                            break;
                        case SectionTypes.RightCorner:
                            Rotate(compass, "Right");
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
                            trueY--;
                            break;
                        case 1:
                            trueX++;
                            break;
                        case 2:
                            trueY++;
                            break;
                        case 3:
                            trueX--;
                            break;
                    }
                }
            }
        }
        public static void setCursorPosition(Track track)
        {
            int x, y, globalX, globalY;
            x = y = 0;
            globalX = globalY = 1000;
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
                        y--;
                        break;
                    case 1:
                        x++;
                        break;
                    case 2:
                        y++;
                        break;
                    case 3:
                        x--;
                        break;
                }
                globalX = (globalX > x) ? y : 0;
                globalY = (globalY > y) ? y : 0;
            }
            Console.SetCursorPosition(-globalX * 4, -globalY * 4);
            compass = 1;
            trueX = globalX;
            trueY = globalY;
        }
        public static int Rotate(int Compass, string rotateDirection)
        {
            switch(rotateDirection)
            {
                case "Left":
                    if(Compass < 1)
                    {
                        Compass = 3;
                    }
                    else
                    {
                        Compass--;
                    }
                    return Compass;
                case "Right":
                    if(Compass > 2)
                    {
                        Compass = 0;
                    }
                    else
                    {
                        Compass++;
                    }
                    return Compass;
            }
            return -1;
        }
        public static void PrintSection(string[] section)
        {
            foreach(string s in section)
            {
                Console.WriteLine(s);
            }
        }
    }
}
 