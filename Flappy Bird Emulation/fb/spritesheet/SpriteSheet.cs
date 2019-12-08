using Flappy_Bird.fb;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird_Emulation.fb {

    /// <summary>
    /// Parsesa sprite sheet into individual sprite textures.
    /// <see cref="https://gamedev.stackexchange.com/questions/35358/create-a-texture2d-from-larger-image"/>
    /// </summary>
    public abstract class SpriteSheet {

        private FlappyBirdGame game;

        private readonly ContentManager contentManager;

        private readonly Dictionary<string, Texture2D> sprites = new Dictionary<string, Texture2D>();

        private  readonly Texture2D spriteSheet;

        public SpriteSheet(FlappyBirdGame game, string sourceKey) {
            this.game = game;
            this.contentManager = game.Content;
            this.spriteSheet = contentManager.Load<Texture2D>(sourceKey);
        }

        public abstract void Parse();

        protected void Add(String key, int x, int y, int width, int height) {
            Add(key, parseTexture(x, y, width, height));
        }

        protected void Add(String key, Texture2D texture) {
            sprites.Add(key, texture);
        }
        public void DrawTexture(String key, int x, int y) {
            Texture2D texture;
            sprites.TryGetValue(key, out texture);
            DrawTexture(texture, x, y);
        }

        public void DrawTexture(Texture2D texture, int x, int y) {
            if (texture == null) {
                return;
            }
            game.GetSpriteBatch().Draw(texture, new Rectangle(x, y, texture.Width, texture.Height), Color.White);
        }

        public Texture2D parseTexture(int x, int y, int width, int height) {
            Texture2D originalTexture = spriteSheet;
            Rectangle sourceRectangle = new Rectangle(x, y, width,  height);
            Texture2D cropTexture = new Texture2D(game.GraphicsDevice, width, height);
            Color[] data = new Color[sourceRectangle.Width * sourceRectangle.Height];
            originalTexture.GetData(0, sourceRectangle, data, 0, data.Length);
            cropTexture.SetData(data);
            return cropTexture;
        }

        public bool IsInsideTexture(string key, MouseState mouseState, int x, int y) {
            Texture2D texture = GetTexture(key);
            return mouseState.X >= x && mouseState.X <= x + texture.Width && mouseState.Y >= y && mouseState.Y <= y + texture.Height;
        }

        public Texture2D GetTexture(String key) {
            Texture2D texture;
            sprites.TryGetValue(key, out texture);
            return texture;
        }

        public ContentManager GetContentManager() {
            return contentManager;
        }
    }
}



