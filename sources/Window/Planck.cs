﻿using System;
using UMapx.Core;

namespace UMapx.Window
{
    /// <summary>
    /// Defines the window function of Planck.
    /// </summary>
    [Serializable]
    public class Planck : WindowBase
    {
        #region Private data
        private double a = 0.15;
        #endregion

        #region Window components
        /// <summary>
        /// Initializes the Planck window function.
        /// </summary>
        /// <param name="frameSize">Window size</param>
        /// <param name="a">Form parameter [0, 0.5]</param>
        public Planck(int frameSize, double a = 0.15)
        {
            this.FrameSize = frameSize;
            this.A = a;
        }
        /// <summary>
        /// Gets or sets the value of the form parameter [0, 0.5].
        /// </summary>
        public double A
        {
            get
            {
                return this.a;
            }
            set
            {
                this.a = Maths.Range(value, 0, 0.5);
            }
        }
        /// <summary>
        /// Function Z+-(x, a).
        /// </summary>
        /// <param name="x">Argument</param>
        /// <param name="p">Sign</param>
        /// <param name="frameSize">Window size</param>
        /// <returns>Double precision floating point number</returns>
        private double Z(double x, bool p, int frameSize)
        {
            // params:
            double t = p ? 1 : -1;
            double y = 2 * x / (frameSize - 1) - 1;

            // function:
            double u = 1.0 / (1 + t * y);
            double v = 1.0 / (1 - 2 * a + t * y);
            return 2 * a * (u + v);
        }
        /// <summary>
        /// Returns the value of a window function.
        /// </summary>
        /// <param name="x">Argument</param>
        /// <param name="frameSize">Window size</param>
        /// <returns>Double precision floating point number</returns>
        public override double Function(double x, int frameSize)
        {
            // Planck taper window:
            double n = frameSize - 1;
            double b = a * n;
            double c = (1 - a) * n;

            // Creating:
            if (x >= 0 && x < b)
            {
                return 1.0 / (Math.Exp(Z(x, true, frameSize)) + 1);
            }
            else if (x >= b && x <= c)
            {
                return 1.0;
            }
            else if (x > c && x <= n)
            {
                return 1.0 / (Math.Exp(Z(x, false, frameSize)) + 1);
            }
            return 0;
        }
        /// <summary>
        /// Returns the window function.
        /// </summary>
        /// <returns>Array</returns>
        public override double[] GetWindow(int frameSize)
        {
            double t = (frameSize - 1);
            double[] x = Matrice.Compute(0, t, 1);
            return this.Function(x, frameSize);
        }
        #endregion
    }
}
