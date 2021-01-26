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
        private static readonly string _corner = ".\\Assets\\Tracks\\Corner.png";
        private static readonly string _straight = ".\\Assets\\Tracks\\Straight.png";
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
            currentX = -minX;
            currentY = -minY;
            int compass = 1;
            Graphics g = Graphics.FromImage(bitmap);
            foreach (Section section in track.Sections)
            {
                switch (section.SectionType)
                {
                    case SectionTypes.StartGrid:
                        Bitmap startGrid = new Bitmap(ImageCache.GetImgBitmap(_start));
                        g.DrawImage(RotateAsset(startGrid, compass, "straight"), new Point(currentX* trackSizePx, currentY*trackSizePx));
                        break;
                    case SectionTypes.Finish:
                        Bitmap finish = new Bitmap(ImageCache.GetImgBitmap(_finish));
                        g.DrawImage(RotateAsset(finish, compass, "straight"), new Point(currentX * trackSizePx, currentY * trackSizePx));
                        break;
                    case SectionTypes.LeftCorner:
                        Bitmap leftCorner = new Bitmap(ImageCache.GetImgBitmap(_corner));
                        g.DrawImage(RotateAsset(leftCorner, compass, "leftCorner"), new Point(currentX * trackSizePx, currentY * trackSizePx));
                        compass = (compass < 1) ? 3 : compass -= 1;
                        break;
                    case SectionTypes.RightCorner:
                        Bitmap rightCorner = new Bitmap(ImageCache.GetImgBitmap(_corner));
                        g.DrawImage(RotateAsset(rightCorner, compass, "rightCorner"), new Point(currentX * trackSizePx, currentY * trackSizePx));
                        compass = (compass > 2) ? 0 : compass += 1;
                        break;
                    case SectionTypes.Straight:
                        Bitmap straight = new Bitmap(ImageCache.GetImgBitmap(_straight));
                        g.DrawImage(RotateAsset(straight, compass, "straight"), new Point(currentX * trackSizePx, currentY * trackSizePx));
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

        public static Bitmap RotateAsset(Bitmap asset, int compass, string type)
        {
            switch (type)
            {
                case "straight":
                    switch (compass)
                    {
                        case 0:
                            asset.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            return asset;
                        case 1:
                            return asset;
                        case 2:
                            asset.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            return asset;
                        case 3:
                            asset.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            return asset;
                    }
                    break;
                case "leftCorner":
                    switch (compass)
                    {
                        case 0:
                            asset.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            return asset;
                        case 1:
                            asset.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            return asset;
                        case 2:
                            return asset;
                        case 3:
                            asset.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            return asset;
                    }
                    break;
                case "rightCorner":
                    switch (compass)
                    {
                        case 0:
                            asset.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            return asset;
                        case 1:
                            asset.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            return asset;
                        case 2:
                            asset.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            return asset;
                        case 3:
                            return asset;
                    }
                    break;
            }

            return asset;
        }
        public static Bitmap PutParticipants(Bitmap bitmap, Track track)
        {

            int currentX, currentY;
            currentX = -minX;
            currentY = -minY;
            int compass = 1;
            Graphics g = Graphics.FromImage(bitmap);
            foreach (Section section in track.Sections)
            {
                switch (section.SectionType)
                {
                    case SectionTypes.StartGrid: case SectionTypes.Finish: case SectionTypes.Straight:
                        break;
                    case SectionTypes.LeftCorner:
                        compass = (compass < 1) ? 3 : compass -= 1;
                        break;
                    case SectionTypes.RightCorner:
                        compass = (compass > 2) ? 0 : compass += 1;
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
