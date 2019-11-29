using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static Flappy_Bird.fb.entity.FlappyBird;

namespace Flappy_Bird.fb.Screen {
    public class PlayScreen : GameScreen {

        private Texture2D background;
        private Texture2D baseLand;
        private readonly Texture2D[][] birdAnimations = new Texture2D[3][];

        public PlayScreen(FlappyBirdGame game) : base(game) {
        }

        public override void Load() {
            background = game.Content.Load<Texture2D>("background-day");
            baseLand = game.Content.Load<Texture2D>("base");
            String color;
            for (int i = 0; i < 3; i++) {
                color = Enum.GetValues(typeof(BirdType)).GetValue(i).ToString().ToLower();
                birdAnimations[i] =  new Texture2D[] { game.Content.Load<Texture2D>(color + "bird-upflap"), game.Content.Load<Texture2D>(color + "bird-midflap"), game.Content.Load<Texture2D>(color + "bird-downflap") };
            }
        }

        public override void Draw(GameTime gameTime) {
            game.GetSpriteBatch().Draw(background, new Rectangle(0, 0, 288, 512), Color.White);
            game.GetSpriteBatch().Draw(baseLand, new Rectangle(0, 512 - 112, 336, 112), Color.White);
        }

        public override void Update(GameTime gameTime) {
            
        }

        public Texture2D[][] GetBirdAnimations() {
            return birdAnimations;
        }
    }
}
