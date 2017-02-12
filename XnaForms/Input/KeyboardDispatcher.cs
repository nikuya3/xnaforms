// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyboardDispatcher.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace XnaForms.Input
{
    using System;
    using System.Threading;
    using System.Windows;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Dispatches input and delivers it to an <see cref="IInputReceivable"/>.
    /// </summary>
    public class KeyboardDispatcher
    {
        private IInputReceivable receiver;
        private string pasteResult = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardDispatcher"/> class.
        /// </summary>
        /// <param name="window">
        /// An instance of <see cref="GameWindow"/> needed for visualizing.
        /// </param>
        public KeyboardDispatcher(GameWindow window)
        {
            EventInput.Initialize(window);
            EventInput.CharEntered += new EventHandler<CharacterEventArgs>(this.EventInput_CharEntered);
            EventInput.KeyDown += new EventHandler<KeyEventArgs>(this.EventInput_KeyDown);
        }

        /// <summary>
        /// Gets or sets the <see cref="IInputReceivable"/> which receives the input dispatched by this <see cref="KeyboardDispatcher"/>.
        /// </summary>
        public IInputReceivable Receiver
        {
            get
            {
                return this.receiver;
            }

            set
            {
                if (this.receiver != null)
                {
                    this.receiver.Focused = false;
                }

                this.receiver = value;
                if (value != null)
                {
                    value.Focused = true;
                }
            }
        }

        private void EventInput_CharEntered(object sender, CharacterEventArgs e)
        {
            if (this.receiver == null)
            {
                return;
            }

            if (char.IsControl(e.Character))
            {
                // Ctrl + V
                if (e.Character == 0x16)
                {
                    // XNA runs in Multiple Thread Apartment state, which cannot receive clipboard
                    Thread thread = new Thread(this.PasteThread);
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                    thread.Join();
                    this.receiver.ReceiveTextInput(this.pasteResult);
                }
                else
                {
                    this.receiver.ReceiveCommandInput(e.Character);
                }
            }
            else
            {
                this.receiver.ReceiveTextInput(e.Character);
            }
        }

        private void EventInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.receiver == null)
            {
                return;
            }

            this.receiver.ReceiveSpecialInput(e.KeyCode);
        }

        [STAThread]
        private void PasteThread()
        {
            if (Clipboard.ContainsText())
            {
                this.pasteResult = Clipboard.GetText();
            }
            else
            {
                this.pasteResult = string.Empty;
            }
        }
    }
}
