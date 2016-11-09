using System;
using System.Drawing;
using System.Net;

namespace BinaryOcean.Library.Imaging
{
    public static class ImageUtility
    {
        public static Image ImageFromUrl(string url)
        {
            var request = WebRequest.Create(url);
            var response = request.GetResponse();
            using (var stream = response.GetResponseStream())
                return Image.FromStream(stream);
        }

        /// <summary>
        /// Calcualte the new width of an image after it is rotated
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="angle">Rotation in degrees. Measured clockwise.</param>
        /// <returns>Width</returns>
        public static int RotatedWidth(int width, int height, double angle)
        {
            // help from http://www.leunen.com/cbuilder/rotbmp.html
            // newx = x * cos(angle) + y * sin(angle)
            // modified to use abs values. not tracking points but rather resulting size

            var w = Math.Abs(width * Math.Cos(Radians(angle))) + Math.Abs(height * Math.Sin(Radians(angle)));

            return Convert.ToInt32(Math.Ceiling(w));
        }

        /// <summary>
        /// Calcualte the new height of an image after it is rotated
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="angle">Rotation in degrees. Measured clockwise.</param>
        /// <returns>Height</returns>
        public static int RotatedHeight(int width, int height, double angle)
        {
            // help from http://www.leunen.com/cbuilder/rotbmp.html
            // newy = y * cos(angle) - x * sin(angle)
            // modified to use abs values. not tracking points but rather resulting size

            var h = Math.Abs(height * Math.Cos(Radians(angle))) + Math.Abs(width * Math.Sin(Radians(angle)));

            return Convert.ToInt32(Math.Ceiling(h));
        }

        /// <summary>
        /// Calcualte the new Size of an image after it is rotated
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="angle">Rotation angle in degrees</param>
        /// <returns></returns>
        public static Size RotatedSize(int width, int height, double angle)
        {
            return new Size(RotatedWidth(width, height, angle), RotatedHeight(width, height, angle));
        }

        /// <summary>
        /// Calculate the new location of a point when an image is rotated around an origin.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="point">A point on the image.</param>
        /// <param name="origin">A point on the image.</param>
        /// <param name="angle">Rotation in degrees. Measured clockwise.</param>
        /// <returns>New point.</returns>
        public static PointF RotatePoint(PointF point, PointF origin, double angle)
        {
            //   x2 = cos(a) * (x1 - 50) + sin(a) * (y1 - 50) + 50;
            //   y2 = cos(a) * (y1 - 50) - sin(a) * (x1 - 50) + 50;

            // correct so that all rotations are clockwise
            angle = -angle;

            var x = Math.Cos(Radians(angle)) * (point.X - origin.X) + Math.Sin(Radians(angle)) * (point.Y - origin.Y) + origin.X;
            var y = Math.Cos(Radians(angle)) * (point.Y - origin.Y) - Math.Sin(Radians(angle)) * (point.X - origin.X) + origin.Y;

            return new PointF(Convert.ToSingle(x), Convert.ToSingle(y));
        }

        /// <summary>
        /// Calculate the new location of a point when an image is rotated around its center point at (0,0).
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="point">A point on the image.</param>
        /// <param name="angle">Rotation in degrees. Measured clockwise.</param>
        /// <returns>New point.</returns>
        public static PointF RotatePoint(PointF point, double angle)
        {
            return RotatePoint(point, new PointF(0, 0), angle);
        }

        public static double Radians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        public static double Degrees(double radians)
        {
            return radians * (180 / Math.PI);
        }
    }
}
