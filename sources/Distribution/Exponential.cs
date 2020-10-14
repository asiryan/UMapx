﻿using System;
using UMapx.Core;

namespace UMapx.Distribution
{
    /// <summary>
    /// Defines the exponential distribution.
    /// <remarks>
    /// More information can be found on the website:
    /// https://en.wikipedia.org/wiki/Exponential_distribution
    /// </remarks>
    /// </summary>
    [Serializable]
    public class Exponential : IDistribution
    {
        #region Private data
        private double l = 1;
        #endregion

        #region Exp components
        /// <summary>
        /// Initializes an exponential distribution.
        /// </summary>
        public Exponential() { }
        /// <summary>
        /// Initializes an exponential distribution.
        /// </summary>
        /// <param name="lambda">Intensity parameter (0, + inf)</param>
        public Exponential(double lambda)
        {
            Lambda = lambda;
        }
        /// <summary>
        /// Gets or sets the value of the intensity parameter (0, + inf).
        /// </summary>
        public double Lambda
        {
            get
            {
                return this.l;
            }
            set
            {
                if (value <= 0)
                    throw new Exception("Invalid argument value");

                this.l = value;
            }
        }
        /// <summary>
        /// Gets the support interval of the argument.
        /// </summary>
        public RangeDouble Support
        {
            get
            {
                return new RangeDouble(0, double.PositiveInfinity);
            }
        }
        /// <summary>
        /// Gets the mean value.
        /// </summary>
        public double Mean
        {
            get
            {
                return Maths.Pow(l, -1);
            }
        }
        /// <summary>
        /// Gets the variance value.
        /// </summary>
        public double Variance
        {
            get
            {
                return Maths.Pow(l, -2);
            }
        }
        /// <summary>
        /// Gets the mode value.
        /// </summary>
        public double Mode
        {
            get
            {
                return 0.0;
            }
        }
        /// <summary>
        /// Gets the median value.
        /// </summary>
        public double Median
        {
            get
            {
                return Maths.Log(2) / l;
            }
        }
        /// <summary>
        /// Gets the value of the asymmetry coefficient.
        /// </summary>
        public double Skewness
        {
            get
            {
                return 2.0;
            }
        }
        /// <summary>
        /// Gets the kurtosis coefficient.
        /// </summary>
        public double Excess
        {
            get
            {
                return 6.0;
            }
        }
        /// <summary>
        /// Returns the value of the probability density function.
        /// </summary>
        /// <param name="x">Value</param>
        /// <returns>Double precision floating point number</returns>
        public double Function(double x)
        {
            if (x < 0)
            {
                return 0;
            }
            return l * Maths.Exp(-l * x);
        }
        /// <summary>
        /// Returns the value of the probability distribution function.
        /// </summary>
        /// <param name="x">Value</param>
        /// <returns>Double precision floating point number</returns>
        public double Distribution(double x)
        {
            if (x < 0)
            {
                return 0;
            }
            return 1 - Maths.Exp(-l * x);
        }
        /// <summary>
        /// Returns the value of differential entropy.
        /// </summary>
        /// <returns>Double precision floating point number</returns>
        public double Entropy
        {
            get
            {
                return 1 - Maths.Log(l);
            }
        }
        #endregion
    }
}
