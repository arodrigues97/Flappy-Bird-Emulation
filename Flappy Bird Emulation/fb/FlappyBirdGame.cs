
using Flappy_Bird_Emulation;
using Flappy_Bird.fb.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Flappy_Bird.fb.entity;
using System.Diagnostics;
using Flappy_Bird_Emulation.fb;
using Flappy_Bird_Emulation.fb.logic;

//https://www.pngkey.com/detail/u2q8o0r5i1i1y3q8_flappy-bird-atlas-png/
//https://github.com/sourabhv/FlapPyBird/tree/master/assets/sprites

namespace Flappy_Bird.fb
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class FlappyBirdGame : Game {


        public static Random Random = new Random();

        /// <summary>
        /// Represents the games menu screen.
        /// </summary>
        private MenuScreen menuScreen;

        /// <summary>
        /// Represents the games play screen.
        /// </summary>
        private PlayScreen playScreen;

        /// <summary>
        /// Represents the games pause screen.
        /// </summary>
        private PauseScreen pauseScreen;

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
        private SpriteSheet spriteSheet;


        private PipeManager pipeManager = new PipeManager();

        /// <summary>
        /// Constructs a new DoodleJumpGame  instance.
        /// </summary>
        public FlappyBirdGame() {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            base.Initialize();
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 512;
            graphics.PreferredBackBufferWidth = 288;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()  {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            menuScreen = new MenuScreen(this);
            playScreen = new PlayScreen(this);
            pauseScreen = new PauseScreen(this);
            spriteSheet = new SpriteSheet(this, "fb-spritesheet");
            spriteSheet.parse();
            Debug.WriteLine("Loaded Content!");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            GetGameScreen().Update(gameTime);
            base.Update(gameTime);
            MouseState mouseState = Mouse.GetState(Window);
            if (Program.DEV_MODE) {
                spriteBatch.Begin();
                Console.WriteLine("Mouse X: " + mouseState.Position.X + ", Mouse Y: " + mouseState.Position.Y , new Vector2(10, 10), Color.Black);
                spriteBatch.End();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            spriteBatch.Begin();
            GetGameScreen().Draw(gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void InitializeState(GameState state) {
            switch (state) {
                case GameState.MAIN_MENU:
                    break;
                case GameState.PAUSE:
                    break;
                       case GameState.PLAYING:
                    flappyBird = new FlappyBird(this);
                    break;
            }
        }

        /// <summary>
        /// Gets the Game Screen to render/logic.
        /// </summary>
        /// <returns>The Game Screen to use.</returns>
        public GameScreen GetGameScreen() {
            switch (GameManager.GetGameState()) {
                case GameState.MAIN_MENU:
                    return menuScreen;
                case GameState.PLAYING:
                    return playScreen;
                case GameState.PAUSE:
                    return pauseScreen;
            }
            return null;
        }

        public SpriteSheet GetSpriteSheet() {
            return spriteSheet;
        }

        public PlayScreen GetPlayScreen() {
            return playScreen;
        }

        public SpriteBatch GetSpriteBatch() {
            return spriteBatch;
        }

    }
}
