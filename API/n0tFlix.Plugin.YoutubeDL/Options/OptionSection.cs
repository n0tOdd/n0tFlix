// Copyright 2020 Brian Allred
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

namespace n0tFlix.Plugin.YoutubeDL.Options
{
    #region Using

    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Helpers;

    #endregion

    /// <summary>
    ///     Abstract parent class, inherited by objects that represent sections of options.
    /// </summary>
    public abstract class OptionSection : NotifyPropertyChangedEx
    {
        protected internal readonly List<string> CustomParameters = new List<string>();

        /// <summary>
        ///     Retrieves the options from this option section
        /// </summary>
        /// <returns>
        ///     The parameterized string of the options in this section
        /// </returns>
        public virtual string ToCliParameters()
        {
            List<string> parameterList = new List<string>();

            // Use reflection to loop over all Option fields and add the parameters to the list.
            // This prevents code duplication, since this method is inherited by all children classes.
            foreach (FieldInfo fieldInfo in this.GetType().GetRuntimeFields())
            {
                // Make sure the field has the [Option] attribute
                if (!fieldInfo.GetCustomAttributes(typeof(OptionAttribute), true).Any())
                {
                    continue;
                }

                object field = fieldInfo.GetValue(this);
                if (!string.IsNullOrWhiteSpace(field.ToString()))
                {
                    parameterList.Add(field.ToString());
                }
            }

            return string.Join(" ", parameterList) + " " +
                   string.Join(" ", this.CustomParameters).RemoveExtraWhitespace();
        }
    }
}