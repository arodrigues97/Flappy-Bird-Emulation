using Microsoft.Xna.Framework;

namespace Flappy_Bird.fb.Screen
{

    /// <summary>
    /// Represents a Game Screen.
    /// </summary>
    public abstract class GameScreen
    {

        /// <summary>
        /// Represents the flappy bird game instance to use.
        /// </summary>
        protected readonly FlappyBirdGame game;

        /// <summary>
        /// Constructs a new GameScreen.
        /// </summary>
        /// <param name="game">The game instance.</param>
        public GameScreen(FlappyBirdGame game)
        {
            this.game = game;
        }

        /// <summary>
        /// Called on the games update loop.
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Called on the games draw method.
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Draw(GameTime gameTime);

        /// <summary>
        /// Loada data for the game screen.
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Called when a game screen is initialized on show.
        /// </summary>
        public abstract void Initialize();

    }

}
