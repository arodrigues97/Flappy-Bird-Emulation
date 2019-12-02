using Flappy_Bird.fb;
using Flappy_Bird.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird_Emulation.fb.entity.pipe {
    public class Pipe : Entity {

        private readonly PipeDirection pipeDirection;

        private Texture2D pipeTexture;

        public Pipe(PipeDirection pipeDirection, Vector2 location, Rectangle rectangle) : base(EntityType.PIPE, location, rectangle) {
            this.pipeDirection = pipeDirection;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            Texture2D texture = GetTexture();
            Rectangle sourceRectangle = new Rectangle(0, 0, rectangle.Width, rectangle.Height);
            Vector2 origin = new Vector2(0, 0);
            Vector2 location = new Vector2(rectangle.X, rectangle.Y);
            spriteBatch.Draw(texture, location, sourceRectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0.9f);

        }

        public override Texture2D GetTexture() {
           if (pipeTexture == null) {
                pipeTexture = GameManager.GetGame().GetSpriteSheet().GetTexture("pipe-" + pipeDirection.ToString().ToLower());
            }
            return pipeTexture;
        }

        public override void Update(GameTime gameTime) {
            if (GameManager.GetGame().GetFlappyBird().IsDead()) {
                return;
            }
            rectangle.X--;
        }
    }

    public enum PipeDirection {
        UP = 0,
        DOWN = 1
    }
}
