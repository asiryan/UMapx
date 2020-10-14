﻿using System;

namespace UMapx.Imaging
{
    /// <summary>
    /// Defines the logarithmic correction filter.
    /// </summary>
    [Serializable]
    public class LogCorrection : Correction, IBitmapFilter
    {
        #region Private data
        private double nbase = 3.14f;
        private double delta = 0.5;
        #endregion

        #region Filter components
        /// <summary>
        /// Initializes the logarithmic correction filter.
        /// </summary>
        /// <param name="nbase">Logarithm base</param>
        /// <param name="delta">Delta [-1, 1]</param>
        /// <param name="space">Color space</param>
        public LogCorrection(double nbase, double delta, Space space)
        {
            Base = nbase; Delta = delta; this.Space = space;
        }
        /// <summary>
        /// Initializes the logarithmic correction filter.
        /// </summary>
        public LogCorrection() { }
        /// <summary>
        /// Gets or sets the base value of the logarithm.
        /// </summary>
        public double Base
        {
            get
            {
                return this.nbase;
            }
            set
            {
                this.nbase = value;
                this.rebuild = true;
            }
        }
        /// <summary>
        /// Gets or sets the delta value [-1, 1].
        /// </summary>
        public double Delta
        {
            get
            {
                return this.delta;
            }
            set
            {
                this.delta = value;
                this.rebuild = true;
            }
        }
        /// <summary>
        /// Implements filter rebuilding.
        /// </summary>
        protected override void Rebuild()
        {
            this.values = Intensity.Log(nbase, delta / 2.0, 256);
        }
        #endregion
    }
}
