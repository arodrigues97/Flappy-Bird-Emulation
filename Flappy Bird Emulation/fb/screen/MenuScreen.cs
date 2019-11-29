using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Flappy_Bird.fb.Screen {
    public class MenuScreen : GameScreen {

        private const long FLAP_SPEED = 100L;

        private Texture2D background;

        private Texture2D baseLand;

        private Texture2D menuMessage;

        private Texture2D[] birdAnimations;

        private Rectangle menuMessageRectangle = new Rectangle(50, 100, 184, 267);

        private long flapChange = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        private int birdFlapCount = 0;

        private Vector2 birdLocation = new Vector2(45, 265);

        private long bobChange = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        private bool up;

        public MenuScreen(FlappyBirdGame game) : base(game) {
        }
        public override void Load() {
            background = game.Content.Load<Texture2D>("background-day");
            baseLand = game.Content.Load<Texture2D>("base");
            menuMessage = game.Content.Load<Texture2D>("message");
            birdAnimations = new Texture2D[]{game.Content.Load<Texture2D>("yellowbird-upflap"), game.Content.Load<Texture2D>("yellowbird-midflap"), game.Content.Load<Texture2D>("yellowbird-downflap")};
;        }

        public override void Update(GameTime gameTime) {
            MouseState state = Mouse.GetState(game.Window);
            if ((long)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - flapChange > FLAP_SPEED) {
                flapChange = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                birdFlapCount++;
                if (birdFlapCount == 3) {
                    birdFlapCount = 0;
                }
            }
            if ((long) (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - bobChange  > 100L) {
                bobChange = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                up = !up;
            }
            if (up) {
                birdLocation.Y += 1;
             } else {
                birdLocation.Y -= 1;
            }
            if (menuMessageRectangle.Contains(state.Position) && state.LeftButton == ButtonState.Pressed) {
                GameManager.InitializeState(GameState.PLAYING);
            }
        }

        public override void Draw(GameTime gameTime) {
            game.GetSpriteBatch().Draw(background, new Rectangle(0, 0, 288, 512), Color.White);
            game.GetSpriteBatch().Draw(baseLand, new Rectangle(0,    512-112, 336, 112), Color.White);
            game.GetSpriteBatch().Draw(menuMessage, menuMessageRectangle, Color.White);
            game.GetSpriteBatch().Draw(birdAnimations[birdFlapCount], new Rectangle((int) birdLocation.X, (int) birdLocation.Y, 34, 24), Color.White);
            game.GetSpriteSheet().DrawTexture("score-button", 186, 372);
            game.GetSpriteSheet()t.DrawTexture("menu-button", 58, 372);
        }

 
    }
}
