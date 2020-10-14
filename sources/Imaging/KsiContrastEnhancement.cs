﻿using System;
using UMapx.Core;

namespace UMapx.Imaging
{
    /// <summary>
    /// Defines the contrast enhancement filter.
    /// </summary>
    [Serializable]
    public class KsiContrastEnhancement : LocalCorrection, IBitmapFilter2
    {
        #region Private data
        private double a;
        private double b;
        #endregion

        #region Filter components
        /// <summary>
        /// Initializes the contrast enhancement filter.
        /// </summary>
        /// <param name="radius">Radius</param>
        /// <param name="space">Color space</param>
        /// <param name="a">Contrast [-1, 1]</param>
        /// <param name="b">Offset [-1, 1]</param>
        public KsiContrastEnhancement(int radius, Space space, double a = 0.75, double b = 0.05)
        {
            gb = new BoxBlur(radius);
            Space = space; A = a; B = b;
        }
        /// <summary>
        /// Initializes the contrast enhancement filter.
        /// </summary>
        /// <param name="width">Filter width</param>
        /// <param name="height">Filter height</param>
        /// <param name="space">Color space</param>
        /// <param name="a">Contrast [-1, 1]</param>
        /// <param name="b">Offset [-1, 1]</param>
        public KsiContrastEnhancement(int width, int height, Space space, double a = 0.75, double b = 0.05)
        {
            gb = new BoxBlur(width, height);
            Space = space; A = a; B = b;
        }
        /// <summary>
        /// Initializes the contrast enhancement filter.
        /// </summary>
        /// <param name="size">Radius</param>
        /// <param name="space">Color space</param>
        /// <param name="a">Contrast [-1, 1]</param>
        /// <param name="b">Offset [-1, 1]</param>
        public KsiContrastEnhancement(SizeInt size, Space space, double a = 0.75, double b = 0.05)
        {
            gb = new BoxBlur(size);
            Space = space; A = a; B = b;
        }
        /// <summary>
        /// Gets or sets the contrast value [-1, 1].
        /// </summary>
        public double A
        {
            get
            {
                return this.a;
            }
            set
            {
                this.a = value;
                this.rebuild = true;
            }
        }
        /// <summary>
        /// Gets or sets the offset value [-1, 1].
        /// </summary>
        public double B
        {
            get
            {
                return this.b;
            }
            set
            {
                this.b = value;
                this.rebuild = true;
            }
        }
        /// <summary>
        /// Implements filter rebuilding.
        /// </summary>
        protected override void Rebuild()
        {
            this.values = Intensity.KsiContrastEnchancement(this.a, this.b, 256);
        }
        #endregion
    }
}
