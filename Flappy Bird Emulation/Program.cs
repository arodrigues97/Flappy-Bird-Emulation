using Flappy_Bird.fb;
using System;

namespace Flappy_Bird_Emulation
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {

        /// <summary>
        /// Represents if we're in development mode.
        /// </summary>
        public const bool DEV_MODE = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GameManager.GetGame().Run();
        }

    }

#endif
}
