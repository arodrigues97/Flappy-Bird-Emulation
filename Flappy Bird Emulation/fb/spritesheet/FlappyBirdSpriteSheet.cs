using Flappy_Bird.fb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird_Emulation.fb.spritesheet {
    public class FlappyBirdSpriteSheet : SpriteSheet {

        const String SPRITESHEET_NAME = "fb-spritesheet";

        public FlappyBirdSpriteSheet(FlappyBirdGame game) :  base(game, SPRITESHEET_NAME) {}

        public override void Parse() {
            Add("menu-button", parseTexture(722, 41, 63, 22));
            Add("score-button", parseTexture(657, 198, 61, 22));
            Add("pipe-up", parseTexture(128, 505, 44, 252));
            Add("pipe-down", parseTexture(86, 505, 44, 252));
            Add("gameover", parseTexture(617, 94, 145, 33));
            Add("gameover-panel", parseTexture(3, 405, 177, 100));
        }

    }
}
