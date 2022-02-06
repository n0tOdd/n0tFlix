// Copyright 2021 Brian Allred
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

namespace NYoutubeDL.Helpers
{
    #region Using

    using System;
    using System.Globalization;

    #endregion

    /// <summary>
    ///     Class holding information about file size or file rate
    /// </summary>
    public class FileSizeRate
    {
        /// <summary>
        ///     Returns new instance of FileSizeRate
        /// </summary>
        /// <param name="sizeRate">
        ///     File size (or speed)
        /// </param>
        /// <param name="unit">
        ///     Unit of size/speed
        /// </param>
        public FileSizeRate(double sizeRate, Enums.ByteUnit unit)
        {
            this.SizeRate = sizeRate;
            this.Unit = unit;
        }

        /// <summary>
        ///     Returns a new instance of FileSizeRate
        /// </summary>
        /// <param name="sizeRate">
        ///     String representation of file size/rate (ie, "1024K", "5.2M")
        /// </param>
        public FileSizeRate(string sizeRate)
        {
            this.SizeRate = double.Parse(sizeRate.Substring(0, sizeRate.Length - 1));
            this.Unit =
                (Enums.ByteUnit)Enum.Parse(typeof(Enums.ByteUnit), sizeRate[sizeRate.Length - 1].ToString().ToUpper(), true);
        }

        /// <summary>
        ///     File size or speed
        /// </summary>
        public double SizeRate { get; set; }

        /// <summary>
        ///     Size or speed unit
        /// </summary>
        public Enums.ByteUnit Unit { get; set; }

        public override string ToString()
        {
            return this.SizeRate.ToString(CultureInfo.InvariantCulture) + this.Unit;
        }
    }
}