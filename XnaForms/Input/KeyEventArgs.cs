// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyEventArgs.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace XnaForms.Input
{
    using System;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Contains event data for a key event.
    /// </summary>
    public class KeyEventArgs : EventArgs
    {
        private Keys keyCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyEventArgs"/> class.
        /// </summary>
        /// <param name="keyCode">
        /// A key associated with the <see cref="KeyEventArgs"/>.
        /// </param>
        public KeyEventArgs(Keys keyCode)
        {
            this.keyCode = keyCode;
        }

        /// <summary>
        /// Gets the key associated with this <see cref="KeyEventArgs"/>.
        /// </summary>
        public Keys KeyCode
        {
            get
            {
                return this.keyCode;
            }
        }
    }
}
