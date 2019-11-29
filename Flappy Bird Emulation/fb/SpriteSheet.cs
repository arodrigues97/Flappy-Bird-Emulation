using Flappy_Bird.fb;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird_Emulation.fb {
   public class SpriteSheet {

        private readonly ContentManager contentManager;

        private  Texture2D spriteSheet;

        private readonly Dictionary<string, Texture2D> sprites = new Dictionary<string, Texture2D>();

        private FlappyBirdGame game;
        private readonly string sourceKey;

        public SpriteSheet(FlappyBirdGame game, string sourceKey) {
            this.game = game;
            this.sourceKey = sourceKey;
            this.contentManager = game.Content;
        }

        public void parse() {
            this.spriteSheet = contentManager.Load<Texture2D>(sourceKey);
            sprites.Add("menu-button", parseTexture(722, 41, 63, 22));
            sprites.Add("score-button", parseTexture(657, 198, 61, 22));
            sprites.Add("pipe-up", parseTexture(39, 505, 48, 252));
            sprites.Add("pipe-down", parseTexture(91, 505, 48, 252));

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



