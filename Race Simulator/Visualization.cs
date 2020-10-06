using Model;
using System.Collections;
using System.Linq;

namespace Race_Simulator
{
    public static class Visualization
    {
        private static int compass;
        public static void Initalize()
        {
            compass = 1;
        }
        #region graphics
        private static string[] _finishHorizontal =
        {
            "----",
            "  # ",
            "  # ",
            "----"
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
                foreach (Section section in track.Sections)
                {

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
                switch(section.SectionType)
                {
                    case SectionTypes.LeftCorner:
                        break;
                    case SectionTypes.RightCorner:
                        break;
                    default:
                        //StartGrid, Finish and straight
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
                        break;

                }
            }
        }
        public static int rotate(int Compass, string rotateDirection)
        {
            switch(rotateDirection)
            {
                case "Left":
                    if(Compass < 0)
                    {
                        Compass = 3;
                    }
                    else
                    {
                        Compass--;
                    }
                    return Compass;
                case "Right":
                    if(Compass > 3)
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
    }
}
