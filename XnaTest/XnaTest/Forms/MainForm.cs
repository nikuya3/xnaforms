// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace XnaTest.Forms
{
    using Microsoft.Xna.Framework;

    using XnaForms.Controls;

    /// <summary>
    /// Represents the main <see cref="Form"/> of this application.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (gameTime.TotalGameTime.Seconds == 2)
            {
                if (!this.comboBox.Items.Contains("Item4"))
                {
                    this.comboBox.Items.Add("Item4");
                }
            }

            if (gameTime.TotalGameTime.Seconds == 3)
            {
                if (!this.comboBox.Items.Contains("Item5"))
                {
                    this.comboBox.Items.Add("Item5");
                }
            }

            if (gameTime.TotalGameTime.Seconds == 4)
            {
                if (this.comboBox.Items.Contains("Item5"))
                {
                    this.comboBox.Items.Remove("Item5");
                }
            }
        }
    }
}
