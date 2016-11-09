using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace BinaryOcean.Library.Imaging
{
    public static class ImageExtensions
    {
        public static Image ToImage(this byte[] bytes)
        {
            // http://msdn.microsoft.com/en-us/library/1kcb3wy4.aspx
            // http://stackoverflow.com/questions/336387/image-save-throws-a-gdi-exception-because-the-memory-stream-is-closed
            //
            // alr - Feb 18 2011
            // You must keep the stream open for the lifetime of the Image.
            // Image disposal does clean up the stream.

           var stream = new MemoryStream(bytes);
           return Image.FromStream(stream);

            // this also appears to correctly GC the stream but add additional CPU cycles 
            // coping the image and is not needed. go with the first approach.
            //using (var stream = new MemoryStream(bytes))
            //using (var temp = Image.FromStream(stream))
            //{
            //    var copy = new Bitmap(temp.Width, temp.Height);
            //    copy.SetResolution(temp.HorizontalResolution, temp.VerticalResolution);
            //    using (var graphics = Graphics.FromImage(copy))
            //    {
            //        graphics.DrawImageUnscaled(temp, 0, 0);
            //    }

            //    return copy;
            //}
        }

        public static byte[] ToJpegBytes(this Image image, long quality)
        {
            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);

            var codec = ImageCodecInfo.GetImageEncoders().Single(i => i.FormatID == ImageFormat.Jpeg.Guid);

            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, codec, encoderParameters);
                return stream.ToArray();
            }
        }

        public static byte[] ToPngBytes(this Image image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Png);
                stream.Close();
                return stream.ToArray();
            }
        }
    }
}
