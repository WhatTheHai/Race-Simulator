using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Media.Imaging;
using Model;

namespace RaceWPF
{
    public static class Visualization
    {
        #region graphics
        private static readonly string _start = ".\\Assets\\Tracks\\StartGrid.png";
        private static readonly string _finish = ".\\Assets\\Tracks\\Finish.png";
        private static readonly string _corner = ".\\Assets\\Tracks\\StartGrid.png";
        private static readonly string _straight = ".\\Assets\\Tracks\\Finish.png";
        private static readonly string _blue = ".\\Assets\\Cars\\Cars\\Blue.png";
        private static readonly string _green = ".\\Assets\\Cars\\Cars\\Green.png";
        private static readonly string _grey = ".\\Assets\\Cars\\Cars\\Grey.png";
        private static readonly string _purple = ".\\Assets\\Cars\\Cars\\Purple.png";
        private static readonly string _red = ".\\Assets\\Cars\\Cars\\Red.png";
        private static readonly string _yellow = ".\\Assets\\Cars\\Cars\\Yellow.png";
        private static readonly string _blueBroken = ".\\Assets\\Cars\\Broken\\Blue.png";
        private static readonly string _greenBroken = ".\\Assets\\Cars\\Broken\\Green.png";
        private static readonly string _greyBroken = ".\\Assets\\Cars\\Broken\\Grey.png";
        private static readonly string _purpleBroken = ".\\Assets\\Cars\\Broken\\Purple.png";
        private static readonly string _redBroken = ".\\Assets\\Cars\\Broken\\Red.png";
        private static readonly string _yellowBroken = ".\\Assets\\Cars\\Broken\\Yellow.png";
        #endregion

        private const int trackSizePx = 128;
        private static int _globalX, _globalY, minX, minY;
        public static BitmapSource DrawTrack(Track track)
        {
            CalculateWidthAndHeight(track);
            Bitmap noTrack = ImageCache.CreateEmptyBitmap(_globalX* trackSizePx, _globalY* trackSizePx);
            Bitmap emptyTrack = PutTrack(noTrack, track); 
            return ImageCache.CreateBitmapSourceFromGdiBitmap(emptyTrack);
        }

        public static void CalculateWidthAndHeight(Track track)
        {
            int compass = 1;
            int x, y, maxX, maxY;
            x = y = minX = minY = maxX = maxY = 0;
            foreach (Section section in track.Sections)
            {
                switch (section.SectionType)
                {
                    case SectionTypes.LeftCorner:
                        compass = (compass < 1) ? 3 : compass -= 1;
                        break;
                    case SectionTypes.RightCorner:
                        compass = (compass > 2) ? 0 : compass += 1;
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

                if (maxX < x)
                {
                    maxX = x;
                } else if (minX > x)
                {
                    minX = x;
                }

                if (maxY < y)
                {
                    maxY = y;
                }
                else if (minY > y)
                {
                    minY = y;
                }
            }
            _globalX = maxX - minX + 1;
            _globalY = maxY - minY + 1;
        }

        public static Bitmap PutTrack(Bitmap bitmap, Track track)
        {

            int currentX, currentY;
            currentX = minX;
            currentY = minY;
            int compass = 1;
            Graphics g = Graphics.FromImage(bitmap);
            foreach (Section section in track.Sections)
            {
                switch (section.SectionType)
                {
                    case SectionTypes.StartGrid:
                        g.DrawImage(ImageCache.GetImgBitmap(_start), new Point(currentX* trackSizePx, currentY*trackSizePx));
                        break;
                    case SectionTypes.Finish:
                        break;
                    case SectionTypes.LeftCorner:
                        compass = (compass < 1) ? 3 : compass -= 1;
                        break;
                    case SectionTypes.RightCorner:
                        compass = (compass > 2) ? 0 : compass += 1;
                        break;
                    case SectionTypes.Straight:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                switch (compass)
                {
                    case 0:
                        currentY--;
                        break;
                    case 1:
                        currentX++;
                        break;
                    case 2:
                        currentY++;
                        break;
                    case 3:
                        currentX--;
                        break;
                }
            }

            return bitmap;
        }
    }
}
