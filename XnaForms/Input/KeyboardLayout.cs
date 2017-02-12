// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyboardLayout.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace XnaForms.Input
{
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// Represents a keyboard layout.
    /// </summary>
    public static class KeyboardLayout
    {
        private const uint KLF_ACTIVATE = 1;
        private const int KeyboardLayoutNameLength = 9;
        private const string LANG_EN_US = "00000409";
        private const string LANG_HE_IL = "0001101A";

        /// <summary>
        /// Gets the name of this <see cref="KeyboardLayout"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> indicating the name of this <see cref="KeyboardLayout"/>.
        /// </returns>
        public static string Name
        {
            get
            {
                StringBuilder name = new StringBuilder(KeyboardLayout.KeyboardLayoutNameLength);
                KeyboardLayout.GetKeyboardLayoutName(name);
                return name.ToString();
            }
        }

        [DllImport("user32.dll")]
        private static extern long LoadKeyboardLayout(
              string pwszKLID,
              uint Flags);

        [DllImport("user32.dll")]
        private static extern long GetKeyboardLayoutName(
              StringBuilder pwszKLID);
    }
}
