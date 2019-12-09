using Flappy_Bird.fb.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Flappy_Bird.fb.Screen
{
    /// <summary>
    /// Represents the Menu Screen.
    /// </summary>
    public class MenuScreen : GameScreen
    {

        /// <summary>
        /// Represents the bird animations.
        /// </summary>
        private Texture2D[] birdAnimations;

        /// <summary>
        /// Represents the delay between changing flap animation.
        /// </summary>
        private long flapChange = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        /// <summary>
        /// Represents the animation index.
        /// </summary>
        private int birdFlapCount = 0;

        /// <summary>
        /// The location of the bird.
        /// </summary>
        private Vector2 birdLocation = new Vector2(126, 210);

        /// <summary>
        /// The timer for changing the y of the bird.
        /// </summary>
        private long bobChange = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        /// <summary>
        /// If the bird is going up or down.
        /// </summary>
        private bool up;

        /// <summary>
        /// If we're showing highscores menu.
        /// </summary>
        private bool highscores;

        /// <summary>
        /// If we're showing help menu.
        /// </summary>
        private bool help;

        /// <summary>
        /// IF the left button is down.
        /// </summary>
        private bool leftDown;

        public MenuScreen(FlappyBirdGame game) : base(game) { }

        /// <summary>
        /// Loads the menu screen.
        /// </summary>
        public override void Load()
        {
            birdAnimations = new Texture2D[] { game.GetSpriteSheet().GetTexture("yellow-upflap"), game.GetSpriteSheet().GetTexture("yellow-midflap"), game.GetSpriteSheet().GetTexture("yellow-downflap") };
        }

        /// <summary>
        /// Initializes the menu screen.
        /// </summary>
        public override void Initialize() { }


        /// <summary>
        /// Updates the menu screen.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState(game.Window);
            if (game.GetSpriteSheet().IsInsideTexture("help", state, 213, 20) && state.LeftButton == ButtonState.Pressed && !leftDown)
            {
                leftDown = true;
                help = !help;
            }
            if (highscores)
            {
                if (game.GetSpriteSheet().IsInsideTexture("menu-button", state, 17, 20) && state.LeftButton == ButtonState.Pressed && !leftDown)
                {
                    highscores = !highscores;
                }
            }
            else
            {
                if ((long)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - flapChange > FlappyBird.FLAP_SPEED)
                {
                    flapChange = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    birdFlapCount++;
                    if (birdFlapCount == 3)
                    {
                        birdFlapCount = 0;
                    }
                }
                if ((long)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - bobChange > 300L)
                {
                    bobChange = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    up = !up;
                }
                if (up)
                {
                    birdLocation.Y += 1;
                }
                else
                {
                    birdLocation.Y -= 1;
                }
                if (game.GetSpriteSheet().IsInsideTexture("play-button", state, 30, 338) && state.LeftButton == ButtonState.Pressed)
                {
                    GameManager.InitializeState(GameState.PLAYING);
                }
                if (game.GetSpriteSheet().IsInsideTexture("highscore-button", state, 150, 338) && state.LeftButton == ButtonState.Pressed)
                {
                    highscores = !highscores;
                }
            }
            if (state.LeftButton == ButtonState.Released)
            {
                leftDown = false;
            }

        }

        /// <summary>
        /// Draws the menu screen.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            game.GetSpriteSheet().DrawTexture("background-day", 0, 0);
            game.GetSpriteSheet().DrawTexture("base", 0, 512 - 112);
            game.GetSpriteSheet().DrawTexture("logo", 50, 120);
            game.GetSpriteSheet().DrawTexture("help", 213, 20);
            if (help)
            {
                game.GetSpriteSheet().DrawTexture("controls", 85, 200);
                Texture2D texture = game.GetMadeByTexture();
                Vector2 location = new Vector2(60, 330);
                Vector2 origin = new Vector2(0, 0);
                Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
                game.GetSpriteBatch().Draw(texture, location, sourceRectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 1);
                return;
            }
            if (highscores)
            {
                PlayScreen playScreen = game.GetPlayScreen();
                int y = 200;
                for (int i = 0; i < game.GetHighscoreManager().GetHighScores().Count; i++)
                {
                    playScreen.DrawNumber((i + 1), 100, y, false);
                    game.GetSpriteSheet().DrawTexture("square", 127, y + 30);
                    playScreen.DrawScore(game.GetHighscoreManager().GetHighScores()[(game.GetHighscoreManager().GetHighScores().Count - 1) - i], 144, y + 10, true);
                    y += 50;
                    if (i == 3)
                    {
                        break;
                    }
                }
                game.GetSpriteSheet().DrawTexture("menu-button", 17, 20);
            }
            else
            {
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
        public void SetHighScore(bool highscore)
        {
            highscore = true;
        }

    }
}
