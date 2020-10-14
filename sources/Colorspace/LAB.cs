﻿using System;

namespace UMapx.Colorspace
{
    /// <summary>
    /// Defines a color model CIE Lab.
    /// </summary>
    [Serializable]
    public struct LAB : IColorSpace, ICloneable
    {
        #region Private data
        private double l;
        private double a;
        private double b;
        #endregion

        #region Structure components
        /// <summary>
        /// Creates an instance of the structure CIE Lab.
        /// </summary>
        /// <param name="l">Component L [0, 100]</param>
        /// <param name="a">Component a [-127, 127]</param>
        /// <param name="b">Component b [-127, 127]</param>
        public LAB(double l, double a, double b)
        {
            this.l = (l > 100.0) ? 100.0 : ((l < 0) ? 0 : l);
            this.a = (a > 127.0) ? 127.0 : ((a < -127) ? -127 : a);
            this.b = (b > 127.0) ? 127.0 : ((b < -127) ? -127 : b);
        }
        /// <summary>
        /// Defines a component of the model [0, 100].
        /// </summary>
        public double L
        {
            get
            {
                return this.l;
            }
            set
            {
                this.l = (value > 100.0) ? 100.0 : ((value < 0) ? 0 : value);
            }
        }
        /// <summary>
        /// Defines a component of the model [-127, 127].
        /// </summary>
        public double A
        {
            get
            {
                return this.a;
            }
            set
            {
                this.a = (value > 127.0) ? 127.0 : ((value < -127) ? -127 : value);
            }
        }
        /// <summary>
        /// Defines a component of the model [-127, 127].
        /// </summary>
        public double B
        {
            get
            {
                return this.b;
            }
            set
            {
                this.b = (value > 127.0) ? 127.0 : ((value < -127) ? -127 : value);
            }
        }
        #endregion

        #region Operators
        /// <summary>
        /// Checks the equality of two class objects.
        /// </summary>
        /// <param name="item1">CIE Lab structure</param>
        /// <param name="item2">CIE Lab structure</param>
        /// <returns>Boolean</returns>
        public static bool operator ==(LAB item1, LAB item2)
        {
            return (
                item1.L == item2.L
                && item1.A == item2.A
                && item1.B == item2.B
                );
        }
        /// <summary>
        /// Checks the inequality of two class objects.
        /// </summary>
        /// <param name="item1">CIE Lab structure</param>
        /// <param name="item2">CIE Lab structure</param>
        /// <returns>Boolean</returns>
        public static bool operator !=(LAB item1, LAB item2)
        {
            return !(item1 == item2);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Defines whether the specified System.Object is equal to the current System.Object.
        /// </summary>
        /// <param name="obj">Element</param>
        /// <returns>Boolean</returns>
        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            return (this == (LAB)obj);
        }
        /// <summary>
        /// Plays the role of a hash function of a certain type.
        /// </summary>
        /// <returns>Integer number</returns>
        public override int GetHashCode()
        {
            return L.GetHashCode() ^ a.GetHashCode() ^ b.GetHashCode();
        }
        /// <summary>
        /// Returns a System.String object that represents the current object.
        /// </summary>
        /// <returns>Text as a sequence of Unicode characters</returns>
        public override string ToString()
        {
            return l.ToString() + "\n" + a.ToString() + "\n" + b.ToString();
        }
        #endregion

        #region Clone members
        /// <summary>
        /// Creates a copy of the color model.
        /// </summary>
        /// <returns>Structure</returns>
        object ICloneable.Clone()
        {
            return new LAB(this.L, this.A, this.B);
        }
        /// <summary>
        /// Creates a copy of the color model.
        /// </summary>
        /// <returns>Structure</returns>
        public LAB Clone()
        {
            return new LAB(this.L, this.A, this.B);
        }
        #endregion

        #region CIE Lab convert
        /// <summary>
        /// Converts a color model CIE Lab in model CIE XYZ.
        /// </summary>
        /// <param name="l">Component L</param>
        /// <param name="a">Component a</param>
        /// <param name="b">Component b</param>
        /// <returns>CIE XYZ structure</returns>
        public static XYZ ToXYZ(double l, double a, double b)
        {
            double theta = 6.0 / 29.0;
            double fy = (l + 16) / 116.0;
            double fx = fy + (a / 500.0);
            double fz = fy - (b / 200.0);
            double theta2 = theta * theta;
            double k = 16.0 / 116.0;
            XYZ D65 = XYZ.White;

            return new XYZ(
                (fx > theta) ? D65.X * (fx * fx * fx) : (fx - k) * 3 * theta2 * D65.X,
                (fy > theta) ? D65.Y * (fy * fy * fy) : (fy - k) * 3 * theta2 * D65.Y,
                (fz > theta) ? D65.Z * (fz * fz * fz) : (fz - k) * 3 * theta2 * D65.Z
                );
        }
        /// <summary>
        /// Converts a color model CIE Lab in model CIE XYZ.
        /// </summary>
        /// <param name="lab">CIE Lab structure</param>
        /// <returns>CIE XYZ structure</returns>
        public static XYZ ToXYZ(LAB lab)
        {
            return LAB.ToXYZ(lab.L, lab.A, lab.B);
        }
        /// <summary>
        /// Converts a color model RGB in model CIE Lab.
        /// </summary>
        /// <param name="red">Red [0, 255]</param>
        /// <param name="green">Green [0, 255]</param>
        /// <param name="blue">Blue [0, 255]</param>
        /// <returns>CIE Lab structure</returns>
        public static LAB ToLAB(int red, int green, int blue)
        {
            return XYZ.ToLAB(XYZ.FromRGB(red, green, blue));
        }
        /// <summary>
        /// Converts a color model RGB in model CIE Lab.
        /// </summary>
        /// <param name="rgb">RGB structure</param>
        /// <returns>CIE Lab structure</returns>
        public static LAB ToLAB(RGB rgb)
        {
            return XYZ.ToLAB(XYZ.FromRGB(rgb.Red, rgb.Green, rgb.Blue));
        }
        #endregion

        #region RGB convert
        /// <summary>
        /// Converts a color model CIE Lab in model RGB.
        /// </summary>
        /// <returns>RGB structure</returns>
        public RGB ToRGB
        {
            get
            {
                return LAB.ToXYZ(l, a, b).ToRGB;
            }
        }
        #endregion
    }
}
