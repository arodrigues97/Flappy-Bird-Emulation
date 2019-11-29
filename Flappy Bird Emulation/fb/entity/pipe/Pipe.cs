using Flappy_Bird.fb;
using Flappy_Bird.node;
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
            GameManager.GetGame().GetSpriteSheet().DrawTexture(GetTexture(), 0, 0);
        }

        public override Texture2D GetTexture() {
           if (pipeTexture == null) {
                pipeTexture = GameManager.GetGame().GetSpriteSheet().GetTexture("pipe-" + pipeDirection.ToString().ToLower());
            }
            return pipeTexture;
        }

        public override void Update(GameTime gameTime) {
           
        }
    }

    public enum PipeDirection {
        UP = 0,
        DOWN = 1
    }
}
