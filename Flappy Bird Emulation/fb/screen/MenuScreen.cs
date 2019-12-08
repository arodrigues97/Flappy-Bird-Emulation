using Flappy_Bird.fb.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Flappy_Bird.fb.Screen {
    public class MenuScreen : GameScreen {

        private Texture2D[] birdAnimations;

        private long flapChange = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        private int birdFlapCount = 0;

        private Vector2 birdLocation = new Vector2(126, 210);

        private long bobChange = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        private bool up;

        private bool highscores;

        public MenuScreen(FlappyBirdGame game) : base(game) {
        }
        public override void Load() {
            birdAnimations = new Texture2D[] { game.GetSpriteSheet().GetTexture("yellow-upflap"), game.GetSpriteSheet().GetTexture("yellow-midflap"), game.GetSpriteSheet().GetTexture("yellow-downflap") };
            ;
        }

        public override void Initialize() { }

        public override void Update(GameTime gameTime) {
            if (highscores) {

            } else {
                MouseState state = Mouse.GetState(game.Window);
                if ((long)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - flapChange > FlappyBird.FLAP_SPEED) {
                    flapChange = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    birdFlapCount++;
                    if (birdFlapCount == 3) {
                        birdFlapCount = 0;
                    }
                }
                if ((long)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - bobChange > 300L) {
                    bobChange = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    up = !up;
                }
                if (up) {
                    birdLocation.Y += 1;
                } else {
                    birdLocation.Y -= 1;
                }
                if (game.GetSpriteSheet().IsInsideTexture("play-button", state, 30, 338) && state.LeftButton == ButtonState.Pressed) {
                    GameManager.InitializeState(GameState.PLAYING);
                }
                if (game.GetSpriteSheet().IsInsideTexture("highscore-button", state, 150, 338) && state.LeftButton == ButtonState.Pressed) {
                    highscores = !highscores;
                }
            }
        }

        public override void Draw(GameTime gameTime) {
            game.GetSpriteSheet().DrawTexture("background-day", 0, 0);
            game.GetSpriteSheet().DrawTexture("base", 0, 512 - 112);
            game.GetSpriteSheet().DrawTexture("logo", 50, 120);
            if (highscores) {
                PlayScreen playScreen = game.GetPlayScreen();
                playScreen.DrawNumber(1, 100, 10, false);
            } else {
                game.GetSpriteSheet().DrawTexture("play-button", 30, 338);
                game.GetSpriteSheet().DrawTexture("highscore-button", 150, 338);
                //game.GetSpriteSheet().DrawTexture("rate-button", 115, 274);
                game.GetSpriteBatch().Draw(birdAnimations[birdFlapCount], new Rectangle((int)birdLocation.X, (int)birdLocation.Y, 35, 25), Color.White);
            }
        }

        /// <summary>
        /// Sets the bool value of showing the highscore screen.
        /// </summary>
        /// <param name="highscore">The boolean value.</param>
        public void SetHighScore(bool highscore) {
            highscore = true;
        }

    }
}
