using Com.Scm.Image;
using Com.Scm.Image.Enums;
using MetadataExtractor;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace Com.Scm.Utils
{
    public static class ImageUtils
    {
        /// <summary>
        /// 加载图像
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static SKBitmap Load(Stream stream)
        {
            return SKBitmap.Decode(stream);
        }

        /// <summary>
        /// 加载图像
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static SKBitmap Load(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                return SKBitmap.Decode(stream);
            }
        }

        /// <summary>
        /// 保存图像
        /// </summary>
        /// <param name="image"></param>
        /// <param name="path"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static bool Save(SKBitmap image, string path, ImageFormat format)
        {
            SKEncodedImageFormat fmt;
            switch (format)
            {
                case ImageFormat.Png:
                    fmt = SKEncodedImageFormat.Png;
                    break;

                case ImageFormat.Jpg:
                    fmt = SKEncodedImageFormat.Jpeg;
                    break;

                case ImageFormat.Gif:
                    fmt = SKEncodedImageFormat.Gif;
                    break;

                case ImageFormat.Bmp:
                    fmt = SKEncodedImageFormat.Bmp;
                    break;

                default:
                    return false;
            }

            using (var stream = File.OpenWrite(path))
            {
                image.Encode(stream, fmt, 100);
                return true;
            }
        }

        /// <summary>
        /// 保存图像
        /// </summary>
        /// <param name="image"></param>
        /// <param name="stream"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static bool Save(SKBitmap image, Stream stream, ImageFormat format)
        {
            SKEncodedImageFormat fmt;
            switch (format)
            {
                case ImageFormat.Png:
                    fmt = SKEncodedImageFormat.Png;
                    break;

                case ImageFormat.Jpg:
                    fmt = SKEncodedImageFormat.Jpeg;
                    break;

                case ImageFormat.Gif:
                    fmt = SKEncodedImageFormat.Gif;
                    break;

                case ImageFormat.Bmp:
                    fmt = SKEncodedImageFormat.Bmp;
                    break;

                default:
                    return false;
            }

            image.Encode(stream, fmt, 100);
            return true;
        }

        /// <summary>
        /// 调整大小
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static SKBitmap Resize(SKBitmap image, int width, int height)
        {
            var tmp = new SKBitmap(width, height);
            using (var canvas = new SKCanvas(tmp))
            {
                canvas.DrawBitmap(image, new SKRect(0, 0, width, height));
            }
            return tmp;
        }

        /// <summary>
        /// 生成指定范围的缩略图
        /// </summary>
        /// <param name="size">最大尺寸</param>
        /// <param name="maxHeight">最大高度</param>
        /// <returns></returns>
        public static SKBitmap Thumbs(SKBitmap image, int size, ThumbsOption option)
        {
            var width = image.Width;
            var height = image.Height;
            if (option == ThumbsOption.LimitWidth)
            {
                width = size;
                height = width * height / image.Width;
            }
            else if (option == ThumbsOption.LimitHeight)
            {
                height = size;
                width = width * height / image.Height;
            }

            var tmp = new SKBitmap(width, height);
            using (var canvas = new SKCanvas(tmp))
            {
                canvas.DrawBitmap(image, new SKRect(0, 0, width, height));
            }
            return tmp;
        }

        /// <summary>
        /// 生成指定大小的缩略图
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width">目标宽度</param>
        /// <param name="height">目标高度</param>
        /// <param name="ratio">是否保持缩放比例</param>
        /// <returns></returns>
        public static SKBitmap Thumbs(SKBitmap image, int width, int height, bool ratio)
        {
            if (ratio)
            {
                var rwidth = (float)width / image.Width;
                var rheight = (float)height / image.Height;
                var min = rwidth < rheight ? rwidth : rheight;
                width = (int)(image.Width * min);
                height = (int)(image.Height * min);
            }

            var tmp = new SKBitmap(width, height);
            using (var canvas = new SKCanvas(tmp))
            {
                canvas.DrawBitmap(image, new SKRect(0, 0, width, height));
            }
            return tmp;
        }

        /// <summary>
        /// 旋转图像
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="degrees">旋转角度(度)</param>
        /// <returns></returns>
        public static SKBitmap Rotate(SKBitmap bitmap, double degrees)
        {
            double radians = Math.PI * degrees / 180;
            float sine = (float)Math.Abs(Math.Sin(radians));
            float cosine = (float)Math.Abs(Math.Cos(radians));
            int originalWidth = bitmap.Width;
            int originalHeight = bitmap.Height;
            int rotatedWidth = (int)(cosine * originalWidth + sine * originalHeight);
            int rotatedHeight = (int)(cosine * originalHeight + sine * originalWidth);

            var rotatedBitmap = new SKBitmap(rotatedWidth, rotatedHeight);

            using (var surface = new SKCanvas(rotatedBitmap))
            {
                surface.Translate(rotatedWidth / 2, rotatedHeight / 2);
                surface.RotateDegrees((float)degrees);
                surface.Translate(-originalWidth / 2, -originalHeight / 2);
                surface.DrawBitmap(bitmap, new SKPoint());
            }
            return rotatedBitmap;
        }

        /// <summary>
        /// 旋转图像
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static SKBitmap Flip(SKBitmap bitmap, FlipOption option)
        {
            var rotated = new SKBitmap(bitmap.Height, bitmap.Width);

            var degress = option == FlipOption.Clockwise ? -90 : 90;
            using (var surface = new SKCanvas(rotated))
            {
                surface.Translate(rotated.Width, 0);
                surface.RotateDegrees(degress);
                surface.DrawBitmap(bitmap, 0, 0);
            }

            return rotated;
        }

        /// <summary>
        /// 翻转图像
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static SKBitmap Flop(SKBitmap bitmap, FlopOption option)
        {
            var rotated = new SKBitmap(bitmap.Width, bitmap.Height);

            using (var surface = new SKCanvas(rotated))
            {
                if (option == FlopOption.Horizontal)
                {
                    surface.Translate(bitmap.Width, 0);
                    surface.Scale(-1, 1);
                }
                else if (option == FlopOption.Vertical)
                {
                    surface.Translate(0, bitmap.Height);
                    surface.Scale(1, -1);
                }

                surface.DrawBitmap(bitmap, 0, 0);
            }

            return rotated;
            //return bitmap;
        }

        public static void DefaultImage(Stream stream)
        {
            using (var codec = SKCodec.Create(stream))
            {
                for (int i = 0; i < codec.FrameCount; i += 1)
                {

                }
            }
        }
    }
}