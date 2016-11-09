using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Text;

namespace BinaryOcean.Library.Imaging
{
    public sealed class ImageKit : IDisposable
    {
        public Image Image { get; private set; }

        public int Width
        {
            get { return Image.Width; }
        }

        public int Height
        {
            get { return Image.Height; }
        }

        public Point CenterPoint
        {
            get { return new Point(this.Width / 2, this.Height / 2); }
        }

        public Size Size
        {
            get { return new Size(this.Width, this.Height); }
        }

        /// <summary>
        /// The Image used with the kit will be disposed of upon the completion of most operations.
        /// </summary>
        /// <param name="imageKit"></param>
        public ImageKit(ImageKit imageKit)
        {
            this.Image = imageKit.Image;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image">The initial image for the kit.
        /// This image will be disposed of upon the completion of most operations.</param>
        public ImageKit(Image image)
        {
            this.Image = new Bitmap(image);
        }

        public ImageKit(byte[] bytes)
        {
            this.Image = bytes.ToImage();
        }

        public ImageKit(int width, int height)
        {
            this.Image = new Bitmap(width, height);
        }

        public ImageKit CropHorizontal(HorizontalAlignment horizontalAlignment, int width)
        {
            if (this.Width > width)
            {
                switch (horizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        Crop(0, 0, width, this.Height);
                        break;

                    case HorizontalAlignment.Center:
                        Crop((this.Width - width) / 2, 0, width, this.Height);
                        break;

                    case HorizontalAlignment.Right:
                        Crop(this.Width - width, 0, width, this.Height);
                        break;
                }
            }

            return this;
        }

        public ImageKit CropVertical(VerticalAlignment verticalAlignment, int height)
        {
            if (this.Height > height)
            {
                switch (verticalAlignment)
                {
                    case VerticalAlignment.Top:
                        Crop(0, 0, this.Width, height);
                        break;

                    case VerticalAlignment.Middle:
                        Crop(0, (this.Height - height) / 2, this.Width, height);
                        break;

                    case VerticalAlignment.Bottom:
                        Crop(0, this.Height - height, this.Width, height);
                        break;
                }
            }

            return this;
        }

        public ImageKit Crop(int x, int y, int width, int height)
        {
            var bitmap = new Bitmap(width, height);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(this.Image, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
            }

            SwapDispose(bitmap);

            return this;
        }

        public ImageKit Crop(int x, int y, int width, int height, float angle)
        {
            return Crop(x, y, width, height, angle, Color.Empty);
        }

        public ImageKit Crop(int x, int y, int width, int height, float angle, Color fill)
        {
            var selectWidth = ImageUtility.RotatedWidth(width, height, angle);
            var selectHeight = ImageUtility.RotatedHeight(width, height, angle);

            var dx = Convert.ToInt32((selectWidth - width) / 2.0f);
            var dy = Convert.ToInt32((selectHeight - height) / 2.0f);

            var bitmap = new Bitmap(selectWidth, selectHeight);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(new SolidBrush(fill), 0, 0, selectWidth, selectHeight);
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.TranslateTransform(selectWidth / 2.0f, selectHeight / 2.0f);
                graphics.RotateTransform(angle);
                graphics.TranslateTransform(-selectWidth / 2.0f, -selectHeight / 2.0f);
                graphics.DrawImage(this.Image, new Rectangle(0, 0, selectWidth, selectHeight), x - dx, y - dy, selectWidth, selectHeight, GraphicsUnit.Pixel);
            }

            SwapDispose(bitmap);

            return Crop(dx, dy, width, height);
        }

        public ImageKit CropMargins(int left, int right, int top, int bottom)
        {
            return Crop(left, top, this.Width - left - right, this.Height - top - bottom);
        }

        public ImageKit CropCenter(int width, int height)
        {
            return Crop(Convert.ToInt32((this.Width - width) / 2.0f), Convert.ToInt32((this.Height - height) / 2.0f), width, height);
        }

        /// <summary>
        /// Draw a circle
        /// </summary>
        /// <param name="x">x center</param>
        /// <param name="y">y center</param>
        /// <param name="radius"></param>
        /// <param name="width">pen width</param>
        /// <param name="color">pen color</param>
        /// <returns></returns>
        public ImageKit DrawCircle(int x, int y, int radius, int width, Color color)
        {
            using (var graphics = Graphics.FromImage(this.Image))
            {
                graphics.DrawEllipse(new Pen(color, width), x - radius, y - radius, radius * 2, radius * 2);
            }

            return this;
        }

        public ImageKit DrawRectangleTopLeft(int x, int y, int width, int height, int weight, Color color)
        {
            using (var graphics = Graphics.FromImage(this.Image))
            {
                graphics.DrawRectangle(new Pen(color, weight), new Rectangle(x, y, width, height));
            }

            return this;
        }

        public ImageKit DrawRectangleCenter(int x, int y, int width, int height, int weight, Color color)
        {
            return DrawRectangleTopLeft(x - width / 2, y - height / 2, width, height, weight, color);
        }

        //public ImageKit DrawText(int offsetX, int offsetY, int width, int height, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, string text, Font font, int resolution, Color textColor)
        //{
        //    return DrawText(offsetX, offsetY, width, height, horizontalAlignment, verticalAlignment, text, font, resolution, textColor, Color.Transparent);
        //}

        public ImageKit DrawText(int offsetX, int offsetY, int width, int height, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, string text, Font font, int resolution, Color textColor, Color backgroundColor)
        {
            var bitmap = new Bitmap(this.Image);
            bitmap.SetResolution(resolution, resolution);

            var format = new StringFormat();

            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    format.Alignment = StringAlignment.Near;
                    break;

                case HorizontalAlignment.Center:
                    format.Alignment = StringAlignment.Center;
                    break;

                case HorizontalAlignment.Right:
                    format.Alignment = StringAlignment.Far;
                    break;
            }

            switch (verticalAlignment)
            {
                case VerticalAlignment.Top:
                    format.LineAlignment = StringAlignment.Near;
                    break;

                case VerticalAlignment.Middle:
                    format.LineAlignment = StringAlignment.Center;
                    break;

                case VerticalAlignment.Bottom:
                    format.LineAlignment = StringAlignment.Far;
                    break;
            }

            using (var graphics = Graphics.FromImage(bitmap))
            {
                if (backgroundColor != Color.Transparent)
                    graphics.FillRectangle(new SolidBrush(backgroundColor), new RectangleF(offsetX, offsetY, width, height));

                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.DrawString(text, font, new SolidBrush(textColor), new RectangleF(offsetX, offsetY, width, height), format);

                //// start attempt at allowing alpha text draw. works but background alpha is not correctly preserved.
                //// needs more work. not sure that there is any value in this so not persuing it at this time. (Jan 2012).
                //if (color.A < 0xff)
                //{
                //    using (var alphaBitmap = new Bitmap(bitmap.Width, bitmap.Height))
                //    using (var alphaGraphics = Graphics.FromImage(alphaBitmap))
                //    {
                //        alphaGraphics.FillRectangle(new SolidBrush(Color.White), 0, 0, alphaBitmap.Width, alphaBitmap.Height);

                //        alphaBitmap.SetResolution(resolution, resolution);
                //        var alphaBrush = new SolidBrush(Color.FromArgb(color.A, color.A, color.A));

                //        alphaGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                //        alphaGraphics.DrawString(text, font, alphaBrush, new RectangleF(offsetX, offsetY, width, height), format);

                //        SwapDispose(bitmap);

                //        this.ChannelSet(alphaBitmap, (a, r, g, b) => r, null, null, null);

                //        return this;
                //    }
                //}
            }

            SwapDispose(bitmap);

            return this;
        }

        //public ImageKit DrawText(int offsetX, int offsetY, string text, Font font, int resolution, Color color)
        //{
        //    return DrawText(offsetX, offsetY, Int32.MaxValue, Int32.MaxValue, HorizontalAlignment.Left, VerticalAlignment.Top, text, font, resolution, color);
        //}

        public ImageKit Fill(Color color)
        {
            return Fill(color, false);
        }

        public ImageKit Fill(Color color, bool preserveAlpha)
        {
            if (!preserveAlpha)
            {
                using (var graphics = Graphics.FromImage(this.Image))
                    graphics.FillRectangle(new SolidBrush(color), 0, 0, this.Width, this.Height);
            }
            else
            {
                using (var alpha = new Bitmap(this.Image))
                {
                    using (var graphics = Graphics.FromImage(this.Image))
                        graphics.FillRectangle(new SolidBrush(color), 0, 0, this.Width, this.Height);

                    AlphaLoad(alpha);
                }
            }

            return this;
        }

        public ImageKit Fit(int width, int height)
        {
            float factorWidth = 1.0f;
            if (this.Image.Width > width)
            {
                factorWidth = (float)width / Image.Width;
            }

            float factorHeight = 1.0f;
            if (this.Image.Height > height)
            {
                factorHeight = (float)height / Image.Height;
            }

            float factor = Math.Min(factorWidth, factorHeight);

            Scale(factor);

            return this;
        }

        public ImageKit FitWidth(int width)
        {
            return Fit(width, int.MaxValue);
        }

        public ImageKit FitHeight(int height)
        {
            return Fit(int.MaxValue, height);
        }

        public ImageKit Layer(Image source, int x, int y)
        {
            using (var graphics = Graphics.FromImage(Image))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                graphics.DrawImage(source, x, y, source.Width, source.Height);
            }

            return this;
        }

        public ImageKit Layer(ImageKit source, int x, int y)
        {
            return Layer(source.Image, x, y);
        }

        public ImageKit Layer(Image source)
        {
            return Layer(source, 0, 0);
        }

        public ImageKit Layer(ImageKit source)
        {
            return Layer(source.Image);
        }

        public ImageKit LayerCentered(Image source)
        {
            var x = Convert.ToInt32((this.Width - source.Width) / 2.0f);
            var y = Convert.ToInt32((this.Height - source.Height) / 2.0f);

            return Layer(source, x, y);
        }

        public ImageKit LayerCentered(ImageKit source)
        {
            return LayerCentered(source.Image);
        }

        public ImageKit LayerCentered(Image source, int offsetX, int offsetY)
        {
            var x = Convert.ToInt32((this.Width - source.Width) / 2.0f);
            var y = Convert.ToInt32((this.Height - source.Height) / 2.0f);

            return Layer(source, x + offsetX, y + offsetY);
        }

        public ImageKit LayerCentered(ImageKit source, int offsetX, int offsetY)
        {
            return LayerCentered(source.Image, offsetX, offsetY);
        }

        public ImageKit Layer(Image source, int offsetX, int offsetY, int width, int height, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            var ik = new ImageKit(source);

            if (ik.Width > width)
                ik.CropHorizontal(horizontalAlignment, width);

            if (ik.Height > height)
                ik.CropVertical(verticalAlignment, height);

            var x = 0;
            var y = 0;

            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    x = 0;
                    break;

                case HorizontalAlignment.Center:
                    x = (width - ik.Width) / 2;
                    break;

                case HorizontalAlignment.Right:
                    x = width - ik.Width;
                    break;
            }

            switch (verticalAlignment)
            {
                case VerticalAlignment.Top:
                    y = 0;
                    break;

                case VerticalAlignment.Middle:
                    y = (height - ik.Height) / 2;
                    break;

                case VerticalAlignment.Bottom:
                    y = height - ik.Height;
                    break;
            }

            Layer(ik, offsetX + x, offsetY + y);

            return this;
        }

        public ImageKit Layer(ImageKit source, int offsetX, int offsetY, int width, int height, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            return Layer(source.Image, offsetX, offsetY, width, height, horizontalAlignment, verticalAlignment);
        }

        public ImageKit AlphaLoad(Image source)
        {
            return AlphaSet(source, (s, d) => s);
        }

        public ImageKit AlphaInvert()
        {
            return AlphaSet(a => (byte)(255 - a));
        }

        public ImageKit AlphaLimitMax(byte value)
        {
            return AlphaSet(a => (byte)a > value ? value : a);
        }

        public ImageKit AlphaLimitMin(byte value)
        {
            return AlphaSet(a => (byte)a < value ? value : a);
        }

        public ImageKit AlphaSet(byte value)
        {
            return AlphaSet(a => (byte)value);
        }

        private ImageKit AlphaSet(Func<byte, byte> func)
        {
            var destinationBitmap = new Bitmap(this.Image);

            //const int blueChannel = 0;
            //const int greenChannel = 1;
            //const int redChannel = 2;
            const int alphaChannel = 3;

            var rec = new Rectangle(Point.Empty, this.Image.Size);

            var destinationData = destinationBitmap.LockBits(rec, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            unsafe
            {
                var destinationPointer = (byte*)destinationData.Scan0.ToPointer();

                for (int i = rec.Height * rec.Width; i > 0; i--)
                {
                    *(destinationPointer + alphaChannel) = func(*(destinationPointer + alphaChannel));

                    destinationPointer += 4;
                }
            }

            destinationBitmap.UnlockBits(destinationData);

            SwapDispose(destinationBitmap);

            return this;
        }

        private ImageKit AlphaSet(Image source, Func<byte, byte, byte> func)
        {
            using (var sourceBitmap = new Bitmap(source))
            {
                var destinationBitmap = new Bitmap(this.Image);

                //const int blueChannel = 0;
                //const int greenChannel = 1;
                //const int redChannel = 2;
                const int alphaChannel = 3;

                if (source.Size != this.Image.Size)
                    throw new InvalidOperationException("Alpha source must match image size.");

                var rec = new Rectangle(Point.Empty, this.Image.Size);

                var sourceData = sourceBitmap.LockBits(rec, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                var destinationData = destinationBitmap.LockBits(rec, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                unsafe
                {
                    var sourcePointer = (byte*)sourceData.Scan0.ToPointer();
                    var destinationPointer = (byte*)destinationData.Scan0.ToPointer();

                    for (int i = rec.Height * rec.Width; i > 0; i--)
                    {
                        *(destinationPointer + alphaChannel) = func(*(sourcePointer + alphaChannel), *(destinationPointer + alphaChannel));
                        sourcePointer += 4;
                        destinationPointer += 4;
                    }
                }

                sourceBitmap.UnlockBits(sourceData);
                destinationBitmap.UnlockBits(destinationData);

                SwapDispose(destinationBitmap);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private ImageKit ChannelSet(Image source, Func<byte, byte, byte, byte, byte> alpha, Func<byte, byte, byte, byte, byte> red, Func<byte, byte, byte, byte, byte> green, Func<byte, byte, byte, byte, byte> blue)
        {
            using (var sourceBitmap = new Bitmap(source))
            {
                var destinationBitmap = new Bitmap(this.Image);

                const int blueChannel = 0;
                const int greenChannel = 1;
                const int redChannel = 2;
                const int alphaChannel = 3;

                if (source.Size != this.Image.Size)
                    throw new InvalidOperationException("Alpha source must match image size.");

                var rec = new Rectangle(Point.Empty, this.Image.Size);

                var sourceData = sourceBitmap.LockBits(rec, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                var destinationData = destinationBitmap.LockBits(rec, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                unsafe
                {
                    var sourcePointer = (byte*)sourceData.Scan0.ToPointer();
                    var destinationPointer = (byte*)destinationData.Scan0.ToPointer();

                    for (int i = rec.Height * rec.Width; i > 0; i--)
                    {
                        var a = *(sourcePointer + alphaChannel);
                        var r = *(sourcePointer + redChannel);
                        var g = *(sourcePointer + greenChannel);
                        var b = *(sourcePointer + blueChannel);

                        if (alpha != null)
                            *(destinationPointer + alphaChannel) = alpha(a, r, g, b);

                        if (red != null)
                            *(destinationPointer + redChannel) = red(a, r, g, b);

                        if (green != null)
                            *(destinationPointer + greenChannel) = green(a, r, g, b);

                        if (blue != null)
                            *(destinationPointer + blueChannel) = blue(a, r, g, b);

                        sourcePointer += 4;
                        destinationPointer += 4;
                    }
                }

                sourceBitmap.UnlockBits(sourceData);
                destinationBitmap.UnlockBits(destinationData);

                SwapDispose(destinationBitmap);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source">Source Image</param>
        /// <param name="alpha">Alpha Channel Result. Source: Alpha, Red, Green, Blue, Destination: Alpha, Red, Green, Blue.</param>
        /// <param name="red">Red Channel Result. Source: Alpha, Red, Green, Blue, Destination: Alpha, Red, Green, Blue.</param>
        /// <param name="green">Green Channel Result. Source: Alpha, Red, Green, Blue, Destination: Alpha, Red, Green, Blue.</param>
        /// <param name="blue">Blue Channel Result. Source: Alpha, Red, Green, Blue, Destination: Alpha, Red, Green, Blue.</param>
        /// <returns></returns>
        private ImageKit ChannelSet(Image source,
             Func<byte, byte, byte, byte, byte, byte, byte, byte, byte> alpha = null,
             Func<byte, byte, byte, byte, byte, byte, byte, byte, byte> red = null,
             Func<byte, byte, byte, byte, byte, byte, byte, byte, byte> green = null,
             Func<byte, byte, byte, byte, byte, byte, byte, byte, byte> blue = null)
        {
            using (var sourceBitmap = new Bitmap(source))
            {
                var destinationBitmap = new Bitmap(this.Image);

                const int blueChannel = 0;
                const int greenChannel = 1;
                const int redChannel = 2;
                const int alphaChannel = 3;

                if (source.Size != this.Image.Size)
                    throw new InvalidOperationException("Source must match image size.");

                var rec = new Rectangle(Point.Empty, this.Image.Size);

                var sourceData = sourceBitmap.LockBits(rec, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                var destinationData = destinationBitmap.LockBits(rec, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                unsafe
                {
                    var sourcePointer = (byte*)sourceData.Scan0.ToPointer();
                    var destinationPointer = (byte*)destinationData.Scan0.ToPointer();

                    for (int i = rec.Height * rec.Width; i > 0; i--)
                    {
                        var sourceA = *(sourcePointer + alphaChannel);
                        var sourceR = *(sourcePointer + redChannel);
                        var sourceG = *(sourcePointer + greenChannel);
                        var sourceB = *(sourcePointer + blueChannel);

                        var destinationA = *(destinationPointer + alphaChannel);
                        var destinationR = *(destinationPointer + redChannel);
                        var destinationG = *(destinationPointer + greenChannel);
                        var destinationB = *(destinationPointer + blueChannel);

                        if (alpha != null)
                            *(destinationPointer + alphaChannel) = alpha(sourceA, sourceR, sourceG, sourceB, destinationA, destinationR, destinationG, destinationB);

                        if (red != null)
                            *(destinationPointer + redChannel) = red(sourceA, sourceR, sourceG, sourceB, destinationA, destinationR, destinationG, destinationB);

                        if (green != null)
                            *(destinationPointer + greenChannel) = green(sourceA, sourceR, sourceG, sourceB, destinationA, destinationR, destinationG, destinationB);

                        if (blue != null)
                            *(destinationPointer + blueChannel) = blue(sourceA, sourceR, sourceG, sourceB, destinationA, destinationR, destinationG, destinationB);

                        sourcePointer += 4;
                        destinationPointer += 4;
                    }
                }

                sourceBitmap.UnlockBits(sourceData);
                destinationBitmap.UnlockBits(destinationData);

                SwapDispose(destinationBitmap);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private ImageKit ChannelSet(Func<byte, byte, byte, byte, byte> alpha, Func<byte, byte, byte, byte, byte> red, Func<byte, byte, byte, byte, byte> green, Func<byte, byte, byte, byte, byte> blue)
        {
            var bitmap = new Bitmap(this.Image);

            const int blueChannel = 0;
            const int greenChannel = 1;
            const int redChannel = 2;
            const int alphaChannel = 3;

            var rec = new Rectangle(Point.Empty, this.Image.Size);

            var data = bitmap.LockBits(rec, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            unsafe
            {
                var dataPointer = (byte*)data.Scan0.ToPointer();

                for (int i = rec.Height * rec.Width; i > 0; i--)
                {
                    var a = *(dataPointer + alphaChannel);
                    var r = *(dataPointer + redChannel);
                    var g = *(dataPointer + greenChannel);
                    var b = *(dataPointer + blueChannel);

                    if (alpha != null)
                        *(dataPointer + alphaChannel) = alpha(a, r, g, b);

                    if (red != null)
                        *(dataPointer + redChannel) = red(a, r, g, b);

                    if (green != null)
                        *(dataPointer + greenChannel) = green(a, r, g, b);

                    if (blue != null)
                        *(dataPointer + blueChannel) = blue(a, r, g, b);

                    dataPointer += 4;
                }
            }

            bitmap.UnlockBits(data);

            SwapDispose(bitmap);

            return this;
        }

        /// <summary>
        /// Return a single 1 x 1 pixel image of the specified color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Image Pixel(Color color)
        {
            var bitmap = new Bitmap(1, 1);
            bitmap.SetPixel(0, 0, color);

            return bitmap;
        }

        /// <summary>
        /// Rotate image by an arbitrary angle
        /// </summary>
        /// <param name="angle">Measured in degrees clockwise</param>
        /// <returns></returns>
        public ImageKit Rotate(float angle)
        {
            return Rotate(angle, false);
        }

        /// <summary>
        /// Rotate image by an arbitrary angle
        /// </summary>
        /// <param name="angle">Measured in degrees clockwise</param>
        /// <param name="resize">Resize image to accommodate new angular size</param>
        /// <returns></returns>
        public ImageKit Rotate(float angle, bool resize)
        {
            var w = this.Width;
            var h = this.Height;

            if (resize)
            {
                w = ImageUtility.RotatedWidth(this.Width, this.Height, angle);
                h = ImageUtility.RotatedHeight(this.Width, this.Height, angle);
            }

            var bitmap = new Bitmap(w, h);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.TranslateTransform(bitmap.Width / 2.0f, bitmap.Height / 2.0f);
                graphics.RotateTransform(angle);
                graphics.TranslateTransform(-bitmap.Width / 2.0f, -bitmap.Height / 2.0f);
                graphics.DrawImage(this.Image, new RectangleF((w - this.Width) / 2.0f, (h - this.Height) / 2.0f, Width, Height), new RectangleF(0.0f, 0.0f, this.Width, this.Height), GraphicsUnit.Pixel);
            }

            SwapDispose(bitmap);

            return this;
        }

        public ImageKit Rotate0()
        {
            Image.RotateFlip(RotateFlipType.RotateNoneFlipNone);
            return this;
        }

        public ImageKit Rotate90()
        {
            Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            return this;
        }

        public ImageKit Rotate180()
        {
            Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            return this;
        }

        public ImageKit Rotate270()
        {
            Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            return this;
        }

        public void Save(string filename)
        {
            Image.Save(filename);
        }

        public void Save(string filename, ImageFormat format)
        {
            Image.Save(filename, format);
        }

        public void Save(Stream stream, ImageFormat format)
        {
            Image.Save(stream, format);
        }

        public void Save(string filename, ImageCodecInfo encoder, EncoderParameters encoderParams)
        {
            Image.Save(filename, encoder, encoderParams);
        }

        public void Save(Stream stream, ImageCodecInfo encoder, EncoderParameters encoderParams)
        {
            Image.Save(stream, encoder, encoderParams);
        }

        public ImageKit Scale(float factor)
        {
            int sizedWidth = (int)Math.Round(this.Image.Width * factor);
            int sizedHeight = (int)Math.Round(this.Image.Height * factor);

            var bitmap = new Bitmap(sizedWidth, sizedHeight);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                graphics.DrawImage(this.Image, 0, 0, sizedWidth, sizedHeight);
            }

            SwapDispose(bitmap);

            return this;
        }

        public ImageKit Scale(int width, int height)
        {
            var bitmap = new Bitmap(width, height);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                graphics.DrawImage(bitmap, 0, 0, width, height);
            }

            SwapDispose(bitmap);

            return this;
        }

        /// <summary>
        /// Resizes the canvas of an image. The image is centered on the new canvas.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public ImageKit Recanvas(int width, int height)
        {
            return Recanvas(width, height, Color.Empty, HorizontalAlignment.Center, VerticalAlignment.Middle);
        }

        /// <summary>
        /// Resizes the canvas of an image. The image is centered on the new canvas.
        /// </summary>
        /// <param name="fill"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public ImageKit Recanvas(int width, int height, Color fill)
        {
            return Recanvas(width, height, fill, HorizontalAlignment.Center, VerticalAlignment.Middle);
        }

        /// <summary>
        ///  Resizes the canvas of an image. The image is aligned as specificed.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="fill"></param>
        /// <param name="horizontalAlignment"></param>
        /// <param name="verticalAlignment"></param>
        /// <returns></returns>
        public ImageKit Recanvas(int width, int height, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            return Recanvas(width, height, Color.Empty, HorizontalAlignment.Center, VerticalAlignment.Middle);
        }

        /// <summary>
        ///  Resizes the canvas of an image. The image is aligned as specificed.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="fill"></param>
        /// <param name="horizontalAlignment"></param>
        /// <param name="verticalAlignment"></param>
        /// <returns></returns>
        public ImageKit Recanvas(int width, int height, Color fill, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            if (this.Width == width && this.Height == height)
                return this;

            int x = 0;
            int y = 0;

            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    x = 0;
                    break;
                case HorizontalAlignment.Center:
                    x = Convert.ToInt32((width - this.Width) / 2.0f);
                    break;
                case HorizontalAlignment.Right:
                    x = width - this.Width;
                    break;
            }

            switch (verticalAlignment)
            {
                case VerticalAlignment.Top:
                    y = 0;
                    break;
                case VerticalAlignment.Middle:
                    y = Convert.ToInt32((height - this.Height) / 2.0f);
                    break;
                case VerticalAlignment.Bottom:
                    y = height - this.Height;
                    break;
            }

            var bitmap = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                if (fill != Color.Empty)
                    graphics.FillRectangle(new SolidBrush(fill), 0, 0, bitmap.Width, bitmap.Height);

                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(this.Image, x, y, this.Width, this.Height);
            }

            SwapDispose(bitmap);

            return this;
        }

        public ImageKit SetResolution(float resolution)
        {
            var bitmap = new Bitmap(this.Image);
            bitmap.SetResolution(resolution, resolution);

            SwapDispose(bitmap);

            return this;
        }

        private void SwapDispose(Image image)
        {
            using (var old = this.Image)
                this.Image = image;

            //GC.Collect();
            //GC.WaitForPendingFinalizers();
        }

        public void Dispose()
        {
            this.Image.Dispose();
        }
    }
}
