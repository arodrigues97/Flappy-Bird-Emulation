
using Flappy_Bird.fb.entity;
using Flappy_Bird.fb.Screen;
using Flappy_Bird_Emulation;
using Flappy_Bird_Emulation.fb;
using Flappy_Bird_Emulation.fb.logic;
using Flappy_Bird_Emulation.fb.spritesheet;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;


namespace Flappy_Bird.fb
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class FlappyBirdGame : Game
    {

        /// <summary>
        /// Represents the random instance to use.
        /// </summary>
        public static Random Random = new Random();

        /// <summary>
        /// Represents the games menu screen.
        /// </summary>
        private readonly MenuScreen menuScreen;

        /// <summary>
        /// Represents the games play screen.
        /// </summary>
        private readonly PlayScreen playScreen;

        /// <summary>
        /// Represents the Graphics Device manager instance.
        /// </summary>
        private GraphicsDeviceManager graphics;

        /// <summary>
        /// Represents the SpriteBatch instance.
        /// </summary>
        private SpriteBatch spriteBatch;

        /// <summary>
        /// Represents the 
        /// </summary>
        private FlappyBird flappyBird;

        /// <summary>
        /// Represents the Sprite Sheet we will be parsing and using.
        /// </summary>
        private FlappyBirdSpriteSheet spriteSheet;

        /// <summary>
        /// Represents the Entity Manager instance.
        /// </summary>
        private readonly EntityManager entityManager;

        /// <summary>
        /// Represents the Highscore Manager instance.
        /// </summary>
        private readonly HighscoreManager highscoreManager;

        /// <summary>
        /// Represents the made by adam texture.
        /// </summary>
        private Texture2D madeByTexture;

        /// <summary>
        /// Constructs a new DoodleJumpGame  instance.
        /// </summary>
        public FlappyBirdGame()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            entityManager = new EntityManager();
            menuScreen = new MenuScreen(this);
            playScreen = new PlayScreen(this);
            highscoreManager = new HighscoreManager();
            Console.WriteLine("Created new Flappy Bird Game");
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 512;
            graphics.PreferredBackBufferWidth = 288;
            graphics.ApplyChanges();
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 100.0f);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteSheet = new FlappyBirdSpriteSheet(this);
            spriteSheet.Parse();
            menuScreen.Load();
            playScreen.Load();
            madeByTexture = Content.Load<Texture2D>("madeby");
            Debug.WriteLine("Loaded Content!");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            GameScreen screen = GetGameScreen();
            if (screen != null)
            {
                GetGameScreen().Update(gameTime);
            }
            if (GameManager.GetGameState() == GameState.PLAYING)
            {
                GetEntityManager().Update(gameTime);
            }
            base.Update(gameTime);
            MouseState mouseState = Mouse.GetState(Window);
            if (Program.DEV_MODE)
            {
                spriteBatch.Begin();
                Console.WriteLine("Mouse X: " + mouseState.Position.X + ", Mouse Y: " + mouseState.Position.Y, new Vector2(10, 10), Color.Black);
                spriteBatch.End();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GameScreen screen = GetGameScreen();
            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            if (GameManager.GetGameState() == GameState.PLAYING)
            {
                GetEntityManager().Draw(gameTime, spriteBatch);
            }
            if (screen != null)
            {
                GetGameScreen().Draw(gameTime);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public FlappyBird SetFlappyBird(FlappyBird flappyBird)
        {
            this.flappyBird = flappyBird;
            return this.flappyBird;
        }

        /// <summary>
        /// Gets the Game Screen to render/logic.
        /// </summary>
        /// <returns>The Game Screen to use.</returns>
        public GameScreen GetGameScreen()
        {
            switch (GameManager.GetGameState())
            {
                case GameState.MAIN_MENU:
                    return menuScreen;
                case GameState.PLAYING:
                    return playScreen;
            }
            return null;
        }

        public FlappyBird GetFlappyBird()
        {
            return flappyBird;
        }

        public EntityManager GetEntityManager()
        {
            return entityManager;
        }

        public SpriteSheet GetSpriteSheet()
        {
            return spriteSheet;
        }

        public PlayScreen GetPlayScreen()
        {
            return playScreen;
        }

        public SpriteBatch GetSpriteBatch()
        {
            return spriteBatch;
        }

        public HighscoreManager GetHighscoreManager()
        {
            return highscoreManager;
        }

        public Texture2D GetMadeByTexture()
        {
            return madeByTexture;
        }

    }
}
