// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventInput.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace XnaForms.Input
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Receives the Windows32 input via events.
    /// </summary>
    public static class EventInput
    {
        private const int GWL_WNDPROC = -4;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_CHAR = 0x102;
        private const int WM_IME_SETCONTEXT = 0x0281;
        private const int WM_INPUTLANGCHANGE = 0x51;
        private const int WM_GETDLGCODE = 0x87;
        private const int WM_IME_COMPOSITION = 0x10f;
        private const int DLGC_WANTALLKEYS = 4;
        private static bool initialized;
        private static IntPtr prevWndProc;
        private static WndProc hookProcDelegate;
        private static IntPtr hIMC;

        private delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// The event raised when a <see cref="char"/> has been entered.
        /// </summary>
        public static event EventHandler<CharacterEventArgs> CharEntered;

        /// <summary>
        /// The event raised when a key has been pressed down. May fire multiple times due to keyboard repeat.
        /// </summary>
        public static event EventHandler<KeyEventArgs> KeyDown;

        /// <summary>
        /// The event raised when a key has been released.
        /// </summary>
        public static event EventHandler<KeyEventArgs> KeyUp;

        /// <summary>
        /// Initializes the <see cref="EventInput"/> class.
        /// </summary>
        /// <param name="window">
        /// A <see cref="GameWindow"/> to which text input should be linked.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="window"/> is a null reference.
        /// </exception>
        public static void Initialize(GameWindow window)
        {
            if (initialized)
            {
                throw new InvalidOperationException("EventInput.Initialize(GameWindow) can only be called once!");
            }

            if (window == null)
            {
                throw new ArgumentNullException();
            }

            EventInput.hookProcDelegate = new WndProc(HookProc);
            EventInput.prevWndProc = (IntPtr)EventInput.SetWindowLong(window.Handle, EventInput.GWL_WNDPROC, (int)Marshal.GetFunctionPointerForDelegate(EventInput.hookProcDelegate));

            EventInput.hIMC = ImmGetContext(window.Handle);
            EventInput.initialized = true;
        }

        [DllImport("Imm32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr ImmGetContext(IntPtr hWnd);

        [DllImport("Imm32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr ImmAssociateContext(IntPtr hWnd, IntPtr hIMC);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private static IntPtr HookProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            IntPtr returnCode = EventInput.CallWindowProc(prevWndProc, hWnd, msg, wParam, lParam);

            switch (msg)
            {
                case EventInput.WM_GETDLGCODE:
                    returnCode = (IntPtr)(returnCode.ToInt32() | EventInput.DLGC_WANTALLKEYS);
                    break;
                case EventInput.WM_KEYDOWN:
                    if (EventInput.KeyDown != null)
                    {
                        EventInput.KeyDown(null, new KeyEventArgs((Keys)wParam));
                    }

                    break;
                case EventInput.WM_KEYUP:
                    if (EventInput.KeyUp != null)
                    {
                        EventInput.KeyUp(null, new KeyEventArgs((Keys)wParam));
                    }

                    break;
                case EventInput.WM_CHAR:
                    if (EventInput.CharEntered != null)
                    {
                        EventInput.CharEntered(null, new CharacterEventArgs((char)wParam, lParam.ToInt32()));
                    }

                    break;

                case EventInput.WM_IME_SETCONTEXT:
                    if (wParam.ToInt32() == 1)
                    {
                        EventInput.ImmAssociateContext(hWnd, EventInput.hIMC);
                    }

                    break;

                case EventInput.WM_INPUTLANGCHANGE:
                    EventInput.ImmAssociateContext(hWnd, EventInput.hIMC);
                    returnCode = (IntPtr)1;
                    break;
            }

            return returnCode;
        }
    }
}
