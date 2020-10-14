﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using UMapx.Core;

namespace UMapx.Imaging
{
    /// <summary>
    /// Defines the flat-field correction filter.
    /// <remarks>
    /// More information can be found on the website:
    /// http://imagej.net/Image_Intensity_Processing
    /// </remarks>
    /// </summary>
    [Serializable]
    public class FlatFieldCorrection : IBitmapFilter2
    {
        #region Private data
        private BoxBlur gb;     // box blur filter,
        private double mR;      // mean of red channel,
        private double mG;      // mean of green channel,
        private double mB;      // mean of blue channel.
        #endregion

        #region Filter components
        /// <summary>
        /// Initializes the flat-field correction filter.
        /// </summary>
        /// <param name="radius">Radius</param>
        public FlatFieldCorrection(int radius = 15)
        {
            gb = new BoxBlur(radius);
        }
        /// <summary>
        /// Initializes the flat-field correction filter.
        /// </summary>
        /// <param name="width">Filter width</param>
        /// <param name="height">Filter height</param>
        public FlatFieldCorrection(int width, int height)
        {
            gb = new BoxBlur(width, height);
        }
        /// <summary>
        /// Initializes the flat-field correction filter.
        /// </summary>
        /// <param name="size">Radius</param>
        public FlatFieldCorrection(SizeInt size)
        {
            gb = new BoxBlur(size);
        }
        /// <summary>
        /// Gets or sets the filter size.
        /// </summary>
        public SizeInt Size
        {
            get
            {
                return gb.Size;
            }
            set
            {
                gb.Size = value;
            }
        }
        /// <summary>
        /// Apply filter.
        /// </summary>
        /// <param name="bmData">Bitmap data</param>
        /// <param name="bmSrc">Bitmap data</param>
        public void Apply(BitmapData bmData, BitmapData bmSrc)
        {
            gb.Apply(bmSrc);
            flatfield(bmData, bmSrc);
            return;
        }
        /// <summary>
        /// Apply filter.
        /// </summary>
        /// <param name="Data">Bitmap</param>
        /// <param name="Src">Bitmap</param>
        public void Apply(Bitmap Data, Bitmap Src)
        {
            BitmapData bmData = BitmapConverter.Lock32bpp(Data);
            BitmapData bmSrc = BitmapConverter.Lock32bpp(Src);
            Apply(bmData, bmSrc);
            BitmapConverter.Unlock(Data, bmData);
            BitmapConverter.Unlock(Src, bmSrc);
            return;
        }
        /// <summary>
        /// Apply filter.
        /// </summary>
        /// <param name="Data">Bitmap</param>
        public void Apply(Bitmap Data)
        {
            Bitmap Src = (Bitmap)Data.Clone();
            BitmapData bmData = BitmapConverter.Lock32bpp(Data);
            BitmapData bmSrc = BitmapConverter.Lock32bpp(Src);
            Apply(bmData, bmSrc);
            BitmapConverter.Unlock(Data, bmData);
            BitmapConverter.Unlock(Src, bmSrc);
            Src.Dispose();
            return;
        }
        #endregion

        #region Private voids
        /// <summary>
        /// Flat-field filter.
        /// </summary>
        /// <param name="bmData">Bitmap data</param>
        /// <param name="bmSrc">Bitmap data</param>
        private unsafe void flatfield(BitmapData bmData, BitmapData bmSrc)
        {
            byte* p = (byte*)bmData.Scan0.ToPointer();
            byte* pSrc = (byte*)bmSrc.Scan0.ToPointer();
            int width = bmData.Width, height = bmData.Height, stride = bmData.Stride;
            this.globalmeans(bmSrc); // calculating medians.

            Parallel.For(0, height, y =>
            {
                int x, ystride, k;

                ystride = y * stride;

                for (x = 0; x < width; x++)
                {
                    k = ystride + x * 4;

                    if (pSrc[k + 2] != 0)
                    {
                        p[k + 2] = Maths.Byte(p[k + 2] * mR / pSrc[k + 2]);
                    }
                    if (pSrc[k + 1] != 0)
                    {
                        p[k + 1] = Maths.Byte(p[k + 1] * mR / pSrc[k + 1]);
                    }
                    if (pSrc[k] != 0)
                    {
                        p[k] = Maths.Byte(p[k] * mR / pSrc[k]);
                    }
                }
            }
            );

            return;
        }
        /// <summary>
        /// Global means.
        /// </summary>
        /// <param name="bmData">Bitmap data</param>
        /// <returns>Array</returns>
        private unsafe void globalmeans(BitmapData bmData)
        {
            byte* p = (byte*)bmData.Scan0.ToPointer();
            int y, x, width = bmData.Width, height = bmData.Height;
            double total = width * height;
            double r = 0, g = 0, b = 0;

            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width; x++, p += 4)
                {
                    r += p[2];
                    g += p[1];
                    b += p[0];
                }
            }

            this.mR = r / total;
            this.mG = g / total;
            this.mB = b / total;
            return;
        }
        #endregion
    }
}
