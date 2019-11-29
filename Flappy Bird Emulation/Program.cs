using Flappy_Bird.fb;
using System;

namespace Flappy_Bird_Emulation {
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program  {

        public const bool DEV_MODE = true;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            using (var game = new FlappyBirdGame())
                game.Run();
        }

    }

#endif
}
