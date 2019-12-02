//using System;
using Flappy_Bird.fb.Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird.fb{

    /// <summary>
    /// Represents the class used to manage the dispatching of the game session and it's state.
    /// </summary>
    public class GameManager {

        /// <summary>
        /// Represents the state the game is in.
        /// </summary>
        private static GameState gameState = GameState.MAIN_MENU;

        /// <summary>
        /// The instance of the Monogame DoodleJump game instance.
        /// </summary>
        private static readonly FlappyBirdGame game = new FlappyBirdGame();

        /// <summary>
        /// Constructs a new Game Manager instance.
        /// </summary>
        public GameManager() {
            /**
             * Empty.
             */
        }
        
        public static void InitializeState(GameState state) {
            gameState = state;
            GameScreen screen = game.GetGameScreen();
            if (screen == null) {
                Console.WriteLine("Error for game state: " + state + ", screen is null " + screen);
                return;
            }
            screen.Initialize();
        }
    
        /// <summary>
        /// Gets the current GameState.
        /// </summary>
        /// <returns>The Game State.</returns>
        public static GameState GetGameState() {
            return gameState;
        }

        /// <summary>
        /// Gets the Doodle Jump Game instance.
        /// </summary>
        /// <returns></returns>
        public static FlappyBirdGame GetGame() {
            return game;
        }

    }
}
