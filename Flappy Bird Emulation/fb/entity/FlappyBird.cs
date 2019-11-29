using Flappy_Bird.fb.Screen;
using Flappy_Bird.node;
using Flappy_Bird_Emulation.fb.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace Flappy_Bird.fb.entity {
    class FlappyBird : Entity {

        private static readonly Vector2 START_LOCATION = new Vector2(50, 250);

        private BirdType type;

        private Texture2D[] birdAnimations;

        private long flapChange = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        private int flapIndex;

        public FlappyBird(FlappyBirdGame game, BirdType type) : base(EntityType.BIRD, START_LOCATION, new Rectangle((int) START_LOCATION.X, (int) START_LOCATION.Y, 32, 27)) {
            this.type = type;
            //this.birdAnimations = ((PlayScreen) game.GetGameScreen()).GetBirdAnimations()[(int) type];
        }

        public FlappyBird(FlappyBirdGame game) : this(game, (BirdType) Enum.GetValues(typeof(BirdType)).GetValue(FlappyBirdGame.Random.Next(Enum.GetValues(typeof(BirdType)).Length))) {
            
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin();
           // spriteBatch.Draw(birdAnimations[flapIndex], location, rectangle);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime) {
            if ((long) (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - flapChange > 100L) {
                flapIndex++;
                if (flapIndex == 3) {
                    flapIndex = 0;
                }
            }
        }

        public enum BirdType {
            RED = 0,
            BLUE = 1,
            YELLOW
        }
    }
}
