using System;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.IO;

namespace BinaryOcean.Library.Imaging
{
    public static class TextUtility
    {
        public static float? FontSize(string text, FontFamily fontFamily, FontStyle fontStyle, int resolution, int width, int height, float minSize, float maxSize, int steps)
        {
            if (string.IsNullOrEmpty(text))
                return maxSize;

            using (var bitmap = new Bitmap(1, 1))
            {
                bitmap.SetResolution(resolution, resolution);
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    var words = text.Split(new[] { ' ', '-' });
                    string maxWord = null;

                    var percentage = (float)Math.Pow(maxSize / minSize, 1.0f / (steps - 1));
                    var size = maxSize;

                    for (var i = 0; i < steps; i++)
                    {
                        if (i > 0)
                            size = size / percentage;

                        var rounded = (float)Math.Round(size, 2);

                        using (var font = new Font(fontFamily, rounded, fontStyle))
                        {
                            // find max word
                            if (string.IsNullOrEmpty(maxWord))
                                maxWord = words
                                    .Select(ii => new { Word = ii, Width = graphics.MeasureString(ii, font).Width, })
                                    .OrderByDescending(ii => ii.Width)
                                    .Select(ii => ii.Word)
                                    .FirstOrDefault();

                            var sizeF = graphics.MeasureString(text, font, width);
                            var wordWidth = graphics.MeasureString(maxWord, font).Width;

                            if ((int)Math.Ceiling(wordWidth) <= width
                                && (int)Math.Ceiling(sizeF.Width) <= width
                                && (int)Math.Ceiling(sizeF.Height) <= height)
                            {
                                return rounded;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public static bool FontFit(string text, FontFamily fontFamily, FontStyle fontStyle, float fontSize, int resolution, int width, int height)
        {
            using (var bitmap = new Bitmap(1, 1))
            {
                bitmap.SetResolution(resolution, resolution);
                using (var graphics = Graphics.FromImage(bitmap))
                using (var font = new Font(fontFamily, fontSize, fontStyle))
                {
                    //get max word width
                    var words = text.Split(new[] { ' ', ',', '-' });
                    var maxWidth = words
                        .Select(i => graphics.MeasureString(i, font).Width)
                        .Max();

                    var sizeF = graphics.MeasureString(text, font, width);

                    return (int)Math.Ceiling(maxWidth) <= width
                        && (int)Math.Ceiling(sizeF.Width) <= width
                        && (int)Math.Ceiling(sizeF.Height) <= height;
                }
            }
        }

        public static Image TextImage(string text, Font font, int resolution, Color color, Color backColor)
        {
            var size = TextSize(text, font, resolution);

            var bitmap = new Bitmap(size.Width, size.Height);
            bitmap.SetResolution(resolution, resolution);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(new SolidBrush(backColor), 0, 0, size.Width, size.Height);
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.DrawString(text, font, new SolidBrush(color), 0, 0);
            }

            return bitmap;
        }

        public static Size TextSize(string text, Font font, int resolution)
        {
            using (var bitmap = new Bitmap(1, 1))
            {
                bitmap.SetResolution(resolution, resolution);
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    var sizef = graphics.MeasureString(text, font);
                    return new Size(Convert.ToInt32(sizef.Width), Convert.ToInt32(sizef.Height));
                }
            }
        }

        //public static FontFamily FontFamilyFromUrl(string url)
        //{
        //    byte[] bytes = null;

        //    var response = WebRequest.Create(url).GetResponse();

        //    //using (var stream = response.GetResponseStream())
        //    //using (var reader = new StreamReader(stream))
        //    //{
        //    //    bytes = new UTF8Encoding().GetBytes(reader.ReadToEnd());
        //    //}

        //    using (var stream = response.GetResponseStream())
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        int count = 0;
        //        do
        //        {
        //            byte[] buf = new byte[1024];
        //            count = stream.Read(buf, 0, 1024);
        //            memoryStream.Write(buf, 0, count);
        //        } while (stream.CanRead && count > 0);

        //        bytes = memoryStream.ToArray();
        //    }

        //    FontFamily fontFamily = null;

        //    var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

        //    try
        //    {
        //        var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
        //        var fontCollection = new PrivateFontCollection();
        //        fontCollection.AddMemoryFont(ptr, bytes.Length);
        //        fontFamily = fontCollection.Families[0];
        //    }
        //    finally
        //    {
        //        // don't forget to unpin the array!
        //        handle.Free();
        //    }

        //    return fontFamily;
        //}
    }
}
