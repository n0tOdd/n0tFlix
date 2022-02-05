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

namespace n0tFlix.Plugin.YoutubeDL.Helpers
{
    #region Using

    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    #endregion

    
    /// <summary>
    ///     Abstract class that extends the functionality of a traditional NotifyPropertyChanged implementation
    /// </summary>
    public abstract class NotifyPropertyChangedEx : INotifyPropertyChanged
    {
        internal PropertyChangedEventHandler propertyChangedEvent;

        /// <summary>
        ///     Fires when property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add => this.propertyChangedEvent += value;
            remove => this.propertyChangedEvent -= value;
        }

        /// <summary>
        ///     Fires the changed event whenever a property changes
        /// </summary>
        /// <param name="propertyName"></param>
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.propertyChangedEvent?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Called by properties that wish to notify listeners about changes
        /// </summary>
        /// <typeparam name="T">
        ///     Type being set
        /// </typeparam>
        /// <param name="field">
        ///     Field being set (passed by reference in order to retain value)
        /// </param>
        /// <param name="value">
        ///     New value for field
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property changing
        /// </param>
        /// <returns>
        ///     Whether the property changed
        /// </returns>
        public bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
    }
}