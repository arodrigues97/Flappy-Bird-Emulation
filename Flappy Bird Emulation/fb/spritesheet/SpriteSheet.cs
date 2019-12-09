using Flappy_Bird.fb;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Flappy_Bird_Emulation.fb
{

    /// <summary>
    /// Parsesa sprite sheet into individual sprite textures.
    /// <see cref="https://gamedev.stackexchange.com/questions/35358/create-a-texture2d-from-larger-image"/>
    /// </summary>
    public abstract class SpriteSheet
    {

        /// <summary>
        /// Represents the sprite sheet instance.
        /// </summary>
        private FlappyBirdGame game;

        /// <summary>
        /// Represents the Content Manager to use.
        /// </summary>
        private readonly ContentManager contentManager;

        /// <summary>
        /// Represents the dictionary of sprites assigned to a key.
        /// </summary>
        private readonly Dictionary<string, Texture2D> sprites = new Dictionary<string, Texture2D>();

        private readonly Texture2D spriteSheet;

        public SpriteSheet(FlappyBirdGame game, string sourceKey)
        {
            this.game = game;
            this.contentManager = game.Content;
            this.spriteSheet = contentManager.Load<Texture2D>(sourceKey);
        }

        /// <summary>
        /// Parses the sprite sheet.
        /// </summary>
        public abstract void Parse();

        /// <summary>
        /// Adds a texture to the dictionary set.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        protected void Add(String key, int x, int y, int width, int height)
        {
            Add(key, parseTexture(x, y, width, height));
        }

        /// <summary>
        /// Adds a texture to the dicionary set.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="texture">The texture.</param>
        protected void Add(String key, Texture2D texture)
        {
            sprites.Add(key, texture);
        }

        /// <summary>
        /// Draws a texture.
        /// </summary>
        /// <param name="key">The key of the texture to draw.</param>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        public void DrawTexture(String key, int x, int y)
        {
            Texture2D texture;
            sprites.TryGetValue(key, out texture);
            DrawTexture(texture, x, y);
        }

        /// <summary>
        /// Dr
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void DrawTexture(Texture2D texture, int x, int y)
        {
            if (texture == null)
            {
                return;
            }
            game.GetSpriteBatch().Draw(texture, new Rectangle(x, y, texture.Width, texture.Height), Color.White);
        }

        /// <summary>
        /// Parses a texture.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public Texture2D parseTexture(int x, int y, int width, int height)
        {
            Texture2D originalTexture = spriteSheet;
            Rectangle sourceRectangle = new Rectangle(x, y, width, height);
            Texture2D cropTexture = new Texture2D(game.GraphicsDevice, width, height);
            Color[] data = new Color[sourceRectangle.Width * sourceRectangle.Height];
            originalTexture.GetData(0, sourceRectangle, data, 0, data.Length);
            cropTexture.SetData(data);
            return cropTexture;
        }

        /// <summary>
        /// Checks if a mouse is inside a texture.
        /// </summary>
        /// <param name="key">The key of the texture to check.</param>
        /// <param name="mouseState">The mouse state to use.</param>
        /// <param name="x">The x-oordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>True if so.</returns>
        public bool IsInsideTexture(string key, MouseState mouseState, int x, int y)
        {
            Texture2D texture = GetTexture(key);
            return mouseState.X >= x && mouseState.X <= x + texture.Width && mouseState.Y >= y && mouseState.Y <= y + texture.Height;
        }

        /// <summary>
        /// Gets a texture.
        /// </summary>
        /// <param name="key">The key identifier.</param>
        /// <returns>The texture.</returns>
        public Texture2D GetTexture(String key)
        {
            Texture2D texture;
            sprites.TryGetValue(key, out texture);
            return texture;
        }

        /// <summary>
        /// Gets the Content Manager.
        /// </summary>
        /// <returns></returns>
        public ContentManager GetContentManager()
        {
            return contentManager;
        }
    }
}



