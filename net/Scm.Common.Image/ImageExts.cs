using Com.Scm.Image.Enums;
using Com.Scm.Utils;
using SkiaSharp;
using System.Collections.Generic;
using System.IO;

namespace Com.Scm.Image
{
    public static class ImageExts
    {
        public static List<ImageColor> GetMajorColors(this SKBitmap image, int length, int delta = 16, int scanRow = 1)
        {
            return ImageUtils.GetMajorColor(image, length, delta, scanRow);
        }

        public static Dictionary<string, string> GetExifInfo(this Stream stream)
        {
            return ImageUtils.GetExifInfo(stream);
        }

        /// <summary>
        /// 保存图像
        /// </summary>
        /// <param name="image"></param>
        /// <param name="stream"></param>
        /// <param name="format"></param>
        public static void Save(this SKBitmap image, Stream stream, ImageFormat format)
        {
            ImageUtils.Save(image, stream, format);
        }

        /// <summary>
        /// 保存图像
        /// </summary>
        /// <param name="image"></param>
        /// <param name="path"></param>
        /// <param name="format"></param>
        public static void Save(this SKBitmap image, string path, ImageFormat format)
        {
            ImageUtils.Save(image, path, format);
        }

        /// <summary>
        /// 旋转图像
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static SKBitmap Flip(this SKBitmap image, FlipOption option)
        {
            return ImageUtils.Flip(image, option);
        }

        /// <summary>
        /// 翻转图像
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static SKBitmap Flop(this SKBitmap image, FlopOption option)
        {
            return ImageUtils.Flop(image, option);
        }

        /// <summary>
        /// 旋转图像
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="degrees">旋转角度(度)</param>
        /// <returns></returns>
        public static SKBitmap Rotate(this SKBitmap image, double degrees)
        {
            return ImageUtils.Rotate(image, degrees);
        }

        /// <summary>
        /// 生成指定范围的缩略图
        /// </summary>
        /// <param name="image"></param>
        /// <param name="size"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static SKBitmap Thumbs(this SKBitmap image, int size, ThumbsOption option)
        {
            return ImageUtils.Thumbs(image, size, option);
        }

        /// <summary>
        /// 生成指定大小的缩略图
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="ratio"></param>
        /// <returns></returns>
        public static SKBitmap Thumbs(this SKBitmap image, int width, int height, bool ratio)
        {
            return ImageUtils.Thumbs(image, width, height, ratio);
        }
    }
}
