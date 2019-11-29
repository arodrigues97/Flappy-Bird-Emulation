using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird.fb.Screen {

    public abstract class GameScreen {

        protected readonly FlappyBirdGame game;

        public GameScreen(FlappyBirdGame game) {
            this.game = game;
            Load();
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);

        public abstract void Load();

    }

}
