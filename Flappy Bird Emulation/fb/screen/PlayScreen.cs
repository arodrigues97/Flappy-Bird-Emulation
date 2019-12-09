using Flappy_Bird.fb.entity;
using Flappy_Bird_Emulation.fb.entity;
using Flappy_Bird_Emulation.fb.logic.entity.flappybird;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Flappy_Bird.fb.Screen
{

    /// <summary>
    /// Represents the Play Screen.
    /// </summary>
    public class PlayScreen : GameScreen
    {

        /// <summary>
        /// Represents the bird animations to choose from.
        /// </summary>
        private readonly Texture2D[][] birdAnimations = new Texture2D[3][];

        /// <summary>
        /// Represents the score textures.
        /// </summary>
        private readonly Texture2D[] scoreTextures = new Texture2D[10];

        /// <summary>
        /// Represents the small score textures.
        /// </summary>
        private readonly Texture2D[] smallScoreTextures = new Texture2D[10];

        /// <summary>
        /// Represents the wing flap sound effect.
        /// </summary>
        private SoundEffect wingFlapSfx;

        /// <summary>
        /// Represents the die sound effect.
        /// </summary>
        private SoundEffect dieSfx;

        /// <summary>
        /// Represents the hit sound effect.
        /// </summary>
        private SoundEffect hitSfx;

        /// <summary>
        /// Represents the point sound effect.
        /// </summary>
        private SoundEffect pointSfx;

        /// <summary>
        /// Checks if the bird has jumped.
        /// </summary>
        private bool hasJumped;

        /// <summary>
        /// Checks if the left button is down.
        /// </summary>
        private bool leftDown;

        /// <summary>
        /// The delay for moving the base texture.
        /// </summary>
        private long baseMove = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        /// <summary>
        /// The x-coordinate of the base.
        /// </summary>
        private int baseX = 0;

        /// <summary>
        /// The background we're using.
        /// </summary>
        private Texture2D background;

        /// <summary>
        /// Constructs a new Play Screen.
        /// </summary>
        /// <param name="game"></param>
        public PlayScreen(FlappyBirdGame game) : base(game) { }

        /// <summary>
        /// Loads data for the play screen.
        /// </summary>
        public override void Load()
        {
            String color;
            for (int i = 0; i < 3; i++)
            {
                color = Enum.GetValues(typeof(BirdType)).GetValue(i).ToString().ToLower();
                birdAnimations[i] = new Texture2D[] { game.GetSpriteSheet().GetTexture(color + "-upflap"), game.GetSpriteSheet().GetTexture(color + "-midflap"), game.GetSpriteSheet().GetTexture(color + "-downflap") };
            }
            for (int i = 0; i < scoreTextures.Length; i++)
            {
                scoreTextures[i] = game.GetSpriteSheet().GetTexture(i.ToString());
                smallScoreTextures[i] = game.GetSpriteSheet().GetTexture(i.ToString() + "-small");
            }
            wingFlapSfx = game.Content.Load<SoundEffect>("sfx_wing");
            dieSfx = game.Content.Load<SoundEffect>("sfx_die");
            hitSfx = game.Content.Load<SoundEffect>("sfx_hit");
            pointSfx = game.Content.Load<SoundEffect>("sfx_point");
        }

        /// <summary>
        /// Initializes the play screen on show.
        /// </summary>
        public override void Initialize()
        {
            background = FlappyBirdGame.Random.Next(1, 3) == 1 ? game.GetSpriteSheet().GetTexture("background-day") : game.GetSpriteSheet().GetTexture("background-night");
            if (game.GetFlappyBird() != null)
            {
                game.GetEntityManager().RemoveEntity(game.GetFlappyBird());
                game.GetEntityManager().Clear(EntityType.PIPE);
                game.GetEntityManager().getPipeManager().Clear();
            }
            game.GetEntityManager().AddEntity(game.SetFlappyBird(new FlappyBird()));
        }

        /// <summary>
        /// Draws the play screen.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            game.GetSpriteBatch().Draw(background, new Rectangle(0, 0, 288, 512), Color.White);
            Texture2D texture = game.GetSpriteSheet().GetTexture("base");
            Rectangle sourceRectangle = new Rectangle(0, 0, 336, 112);
            Vector2 origin = new Vector2(0, 0);
            Vector2 location = new Vector2(baseX, 512 - 112);
            game.GetSpriteBatch().Draw(texture, location, sourceRectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 1);
            if (baseX >= -336)
            {
                location = new Vector2(288 + baseX, 512 - 112);
                game.GetSpriteBatch().Draw(texture, location, sourceRectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 1);
            }
            DrawScore(game.GetFlappyBird().GetScore(), 115, 40, false);
            if (game.GetFlappyBird().IsDead() || game.GetFlappyBird().HasHitPipe())
            {
                texture = game.GetSpriteSheet().GetTexture("gameover");
                location = new Vector2(50, 140);
                sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
                game.GetSpriteBatch().Draw(texture, location, sourceRectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 1);
                game.GetSpriteSheet().DrawTexture("gameover-panel", 34, 200);
                texture = game.GetSpriteSheet().GetTexture("play-button");
                location = new Vector2(30, 338);
                sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
                game.GetSpriteBatch().Draw(texture, location, sourceRectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 1);
                texture = game.GetSpriteSheet().GetTexture("highscore-button");
                location = new Vector2(150, 338);
                sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
                game.GetSpriteBatch().Draw(texture, location, sourceRectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 1);
                int score = game.GetFlappyBird().GetScore();
                String medal = "";
                int medalX = 0;
                int medalY = 0;
                if (score >= 40)
                {
                    medal = "platinum";
                    medalY = 249;
                    medalX = 59;
                }
                else if (score >= 30)
                {
                    medal = "gold";
                    medalY = 246;
                    medalX = 62;
                }
                else if (score >= 20)
                {
                    medal = "silver";
                    medalX = 59; medalY = 245;
                }
                else if (score >= 10)
                {
                    medal = "bronze";
                    medalY = 248;
                    medalX = 62;
                }
                if (medal != "")
                {
                    game.GetSpriteSheet().DrawTexture(medal + "-medal", medalX, medalY);
                }
                DrawScore(game.GetFlappyBird().GetScore(), 212, 239, true);
                int bestScore = game.GetHighscoreManager().GetBestScore();
                if (bestScore != -1)
                {
                    DrawScore(bestScore, 212, 287, true);
                }
                return;
            }

        }

        /// <summary>
        /// Updates the play screen logic.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState(game.Window);
            if ((game.GetFlappyBird().IsDead() || game.GetFlappyBird().HasHitPipe()) && game.GetSpriteSheet().IsInsideTexture("play-button", state, 30, 338) && state.LeftButton == ButtonState.Pressed)
            {
                GameManager.InitializeState(GameState.PLAYING);
            }
            if ((game.GetFlappyBird().IsDead() || game.GetFlappyBird().HasHitPipe()) && game.GetSpriteSheet().IsInsideTexture("highscore-button", state, 150, 338) && state.LeftButton == ButtonState.Pressed)
            {
                GameManager.InitializeState(GameState.MAIN_MENU);
                ((MenuScreen)GameManager.GetGame().GetGameScreen()).SetHighScore(true);
            }
            if (game.GetFlappyBird().IsDead() || game.GetFlappyBird().HasHitPipe())
            {
                return;
            }
            game.GetEntityManager().getPipeManager().Update();
            if (state.LeftButton == ButtonState.Pressed && hasJumped == false && !leftDown)
            {
                game.GetFlappyBird().GetPhysics().Jump();
                wingFlapSfx.Play();
                hasJumped = true;
                leftDown = true;
            }
            if (state.LeftButton == ButtonState.Released)
            {
                leftDown = false;
                hasJumped = false;
            }
            baseX--;
            if (baseX == -336)
            {
                baseX = 0;
            }
        }

        /// <summary>
        /// Draws the entire score.
        /// </summary>
        /// <param name="score">The score to draw.</param>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="small">If we're drawing small textures.</param>
        public void DrawScore(int score, int x, int y, bool small)
        {
            String scoreString = score.ToString();
            char[] numberChars = scoreString.ToCharArray();
            int[] numbers = new int[numberChars.Length];
            for (int i = 0; i < numberChars.Length; i++)
            {
                numbers[i] = int.Parse(numberChars[i].ToString());
            }
            Texture2D numberTexture;
            for (int i = 0; i < numbers.Length; i++)
            {
                numberTexture = DrawNumber(numbers[i], x, y, small);
                x += numberTexture.Width;
            }
        }

        /// <summary>
        /// Draws a digit.
        /// </summary>
        /// <param name="number">The digit to draw.</param>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="small">If we're drawing small textures.</param>
        /// <returns></returns>
        public Texture2D DrawNumber(int number, int x, int y, bool small)
        {
            Texture2D texture = small ? smallScoreTextures[number] : scoreTextures[number];
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, 36);
            Vector2 origin = new Vector2(0, 0);
            Vector2 location = new Vector2(x, y);
            game.GetSpriteBatch().Draw(texture, location, sourceRectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 1);
            return texture;
        }

        public SoundEffect GetWingFlapSfx()
        {
            return wingFlapSfx;
        }

        public SoundEffect GetDieSfx()
        {
            return dieSfx;
        }

        public SoundEffect GetHitSfx()
        {
            return hitSfx;
        }

        public SoundEffect GetPointSfx()
        {
            return pointSfx;
        }

        public Texture2D[][] GetBirdAnimations()
        {
            return birdAnimations;
        }
    }
}
