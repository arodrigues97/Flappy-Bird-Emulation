using Flappy_Bird.fb;
using System;

namespace Flappy_Bird_Emulation.fb.spritesheet
{
    /// <summary>
    /// Represents the Flappy Bird Sprite Sheet.
    /// </summary>
    public class FlappyBirdSpriteSheet : SpriteSheet
    {

        /// <summary>
        /// The sprite sheet name.
        /// </summary>
        public const String SPRITESHEET_NAME = "flappybird-spritesheet";

        /// <summary>
        /// Constructs a new FlappyBirdSpriteSheet.
        /// </summary>
        /// <param name="game"></param>
        public FlappyBirdSpriteSheet(FlappyBirdGame game) : base(game, SPRITESHEET_NAME) { }

        /// <summary>
        /// Parses the sprite sheet.
        /// </summary>
        public override void Parse()
        {

            ///new textures
            Add("background-day", 0, 0, 288, 511);
            Add("background-night", 292, 0, 288, 511);
            Add("base", 585, 0, 335, 341);
            Add("logo", 698, 177, 185, 58);
            Add("play-button", 706, 236, 112, 78);
            Add("highscore-button", 825, 237, 117, 76);
            Add("rate-button", 931, 0, 70, 38);
            //23, 36
            int birdWidth = 35;
            int birdHeight = 25;
            int startBirdX = 6;
            int startBirdY = 982;
            //Yelow
            Add("yellow-upflap", startBirdX, startBirdY, birdWidth, birdHeight);
            startBirdX = 62;
            Add("yellow-midflap", startBirdX, startBirdY, birdWidth, birdHeight);
            startBirdX = 118;
            Add("yellow-downflap", startBirdX, startBirdY, birdWidth, birdHeight);
            //Red
            startBirdX = 229;
            startBirdY = 760;
            Add("red-upflap", startBirdX, startBirdY, birdWidth, birdHeight);
            startBirdY = 815;
            Add("red-midflap", startBirdX, startBirdY, birdWidth, birdHeight);
            startBirdY = 864;
            Add("red-downflap", startBirdX, startBirdY, birdWidth, birdHeight);
            //Blue
            startBirdX = 171;
            startBirdY = 980;
            Add("blue-upflap", startBirdX, startBirdY, birdWidth, birdHeight);
            startBirdX = 229;
            startBirdY = 655;
            Add("blue-midflap", startBirdX, startBirdY, birdWidth, birdHeight);
            startBirdY = 708;
            Add("blue-downflap", startBirdX, startBirdY, birdWidth, birdHeight);

            //Pipe Down
            Add("pipe-down", 112, 646, 55, 321);
            Add("pipe-up", 112 + 55, 646, 55, 321);

            //Scores
            int scoreWidth = 26;
            int scoreHeight = 38;
            int scoreX = 990;
            int scoreY = 119;
            Add("0", scoreX, scoreY, scoreWidth, scoreHeight);
            scoreX = 271;
            scoreY = 911;
            Add("1", scoreX, scoreY, scoreWidth, scoreHeight);
            scoreX = 584;
            scoreY = 319;
            Add("2", scoreX, scoreY, scoreWidth, scoreHeight);
            scoreX += scoreWidth;
            Add("3", scoreX, scoreY, scoreWidth, scoreHeight);
            scoreX += scoreWidth;
            Add("4", scoreX, scoreY, scoreWidth, scoreHeight);
            scoreX += scoreWidth;
            scoreX += 2;
            Add("5", scoreX, scoreY, scoreWidth, scoreHeight);
            scoreY += 50;
            scoreX = 584;
            Add("6", scoreX, scoreY, scoreWidth, scoreHeight);
            scoreX += scoreWidth;
            Add("7", scoreX, scoreY, scoreWidth, scoreHeight);
            scoreX += scoreWidth;
            Add("8", scoreX, scoreY, scoreWidth, scoreHeight);
            scoreX += scoreWidth;
            scoreX += 2;
            Add("9", scoreX, scoreY, scoreWidth, scoreHeight);

            Add("gameover", 787, 114, 195, 62);
            Add("gameover-panel", 5, 512, 237, 125);
            Add("platinum-medal", 240, 516, 74, 46);
            Add("gold-medal", 240, 516 + 47, 74, 46);
            Add("silver-medal", 223, 516 + 386, 48, 46);
            Add("bronze-medal", 223, 516 + 393 + 45, 48, 46);

            //small numbers
            int width = 17;
            int height = 17;
            Add("0-small", 275, 645, width, height);
            Add("1-small", 279, 663, width, height);
            Add("2-small", 274, 698, width, height);
            Add("3-small", 273, 715, width, height);
            Add("4-small", 274, 748, width, height);
            Add("5-small", 273, 767, width, height);
            Add("6-small", 273, 801, width, height);
            Add("7-small", 273, 818, width, height);
            Add("8-small", 273, 853, width, height);
            Add("9-small", 273, 871, width, height);
            Add("square", 594, 426, 4, 4);
            Add("menu-button", 924, 51, 83, 31);

            Add("controls", 582, 173, 119, 105);
            Add("help", 666, 283, 30, 32);
        }


    }
}
