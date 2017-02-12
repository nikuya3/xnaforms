// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Manager.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace XnaForms.Core
{
    using System;
    using System.ComponentModel;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using XnaForms.Input;

    /// <summary>
    /// Manages all components of XnaForms.
    /// </summary>
    public static class Manager
    {
        private static GraphicsDevice graphicsDevice;
        private static KeyboardDispatcher keyboardDispatcher;
        private static ComponentSpriteBatch spriteBatch;
        private static GameWindow window;

        /// <summary>
        /// Gets or sets the <see cref="GraphicsDevice"/> used by the components of XnaForms.
        /// </summary>
        public static GraphicsDevice GraphicsDevice
        {
            get
            {
                return Manager.graphicsDevice;
            }

            set
            {
                Manager.graphicsDevice = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Manager"/> class has already been initialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static bool IsInitialized { get; private set; }

        /// <summary>
        /// Gets or sets the <see cref="ComponentSpriteBatch"/> used to draw the components of XnaForms.
        /// </summary>
        public static ComponentSpriteBatch SpriteBatch
        {
            get
            {
                return Manager.spriteBatch;
            }

            set
            {
                Manager.spriteBatch = value;
            }
        }

        /// <summary>
        /// Gets or sets the  <see cref="GameWindow"/> used by XnaForms.
        /// </summary>
        public static GameWindow Window
        {
            get
            {
                return Manager.window;
            }

            set
            {
                Manager.window = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="KeyboardDispatcher"/> used by the components of XnaForms to retrieve input.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static KeyboardDispatcher KeyboardDispatcher
        {
            get
            {
                return Manager.keyboardDispatcher;
            }

            set
            {
                Manager.keyboardDispatcher = value;
            }
        }

        /// <summary>
        /// Initializes the <see cref="Manager"/> class with the given values.
        /// </summary>
        /// <param name="device">
        /// A <see cref="GraphicsDevice"/> used to create default objects.
        /// </param>
        /// <param name="font">
        /// A <see cref="SpriteFont"/> used as default <see cref="SpriteFont"/>.
        /// </param>
        /// <param name="window">
        /// An instance of <see cref="GameWindow"/> used by XnaForms.
        /// </param>
        public static void Initialize(GraphicsDevice device, SpriteFont font, GameWindow window)
        {
            Manager.graphicsDevice = device;
            Manager.spriteBatch = new ComponentSpriteBatch(device);
            Manager.window = window;
            Manager.keyboardDispatcher = new KeyboardDispatcher(window);
            Texture2D texture = new Texture2D(device, 1, 1);
            texture.SetData<Color>(new[] { Color.White });
            DefaultStyles.Initialize(device, font);
            Manager.IsInitialized = true;
        }

        /// <summary>
        /// Updates all components maintained by the <see cref="Manager"/> class.
        /// </summary>
        /// <param name="gameTime">
        /// An instance of <see cref="GameTime"/> indicating the current time.
        /// </param>
        public static void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates all components maintained by the <see cref="Manager"/> class.
        /// </summary>
        /// <param name="gameTime">
        /// An instance of <see cref="GameTime"/> indicating the current time.
        /// </param>
        public static void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
