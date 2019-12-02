using Flappy_Bird.fb.entity;
using Flappy_Bird_Emulation.fb.logic.entity.flappybird;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Flappy_Bird.fb.Screen {
    public class PlayScreen : GameScreen {

        private Texture2D backgroundDay;

        private Texture2D backgroundNight;

        private Texture2D baseLand;

        private readonly Texture2D[][] birdAnimations = new Texture2D[3][];

        private readonly Texture2D[] scoreTexures = new Texture2D[10];

        private SoundEffect wingFlapSfx;

        private SoundEffect dieSfx;

        private SoundEffect hitSfx;

        private SoundEffect pointSfx;

        private bool hasJumped;

        private bool spacebarDown;

        private long baseMove = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        private int baseX = 0;

        private Texture2D background;

        public PlayScreen(FlappyBirdGame game) : base(game) { }

        public override void Load() {
            backgroundDay = game.Content.Load<Texture2D>("background-day");
            backgroundNight = game.Content.Load<Texture2D>("background-night");
            baseLand = game.Content.Load<Texture2D>("base");
            String color;
            for (int i = 0; i < 3; i++) {
                color = Enum.GetValues(typeof(BirdType)).GetValue(i).ToString().ToLower();
                birdAnimations[i] = new Texture2D[] { game.Content.Load<Texture2D>(color + "bird-upflap"), game.Content.Load<Texture2D>(color + "bird-midflap"), game.Content.Load<Texture2D>(color + "bird-downflap") };
            }
            for (int i = 0; i < scoreTexures.Length; i++) {
                Console.WriteLine(i.ToString());
                scoreTexures[i] = game.Content.Load<Texture2D>(i.ToString());
            }
            wingFlapSfx = game.Content.Load<SoundEffect>("sfx_wing");
            dieSfx = game.Content.Load<SoundEffect>("sfx_die");
            hitSfx = game.Content.Load<SoundEffect>("sfx_hit");
            pointSfx = game.Content.Load<SoundEffect>("sfx_point");
        }

        public override void Initialize() {
            background = FlappyBirdGame.Random.Next(1, 3) == 1 ?  backgroundDay : backgroundNight;
            game.GetEntityManager().AddEntity(game.SetFlappyBird(new FlappyBird()));
            Console.WriteLine("Initialized Play Screen!");
        }

        public override void Draw(GameTime gameTime) {
            game.GetSpriteBatch().Draw(background, new Rectangle(0, 0, 288, 512), Color.White);
            Texture2D texture = baseLand;
            Rectangle sourceRectangle = new Rectangle(0, 0, 336, 112);
            Vector2 origin = new Vector2(0, 0);
            Vector2 location = new Vector2(baseX, 512 - 112);
            game.GetSpriteBatch().Draw(texture, location, sourceRectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 1);
            if (baseX >= -336) {
                location = new Vector2(288 + baseX, 512 - 112);
                game.GetSpriteBatch().Draw(texture, location, sourceRectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 1);
            }
            DrawScore();
            if (game.GetFlappyBird().IsDead() || game.GetFlappyBird().HasHitPipe()) {
                texture = game.GetSpriteSheet().GetTexture("gameover");
                location = new Vector2(72, 140);
                sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
                game.GetSpriteBatch().Draw(texture, location, sourceRectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 1);
                game.GetSpriteSheet().DrawTexture("gameover-panel", 50, 200);
                return;
            }

        }

        public override void Update(GameTime gameTime) {
            if (game.GetFlappyBird().IsDead() || game.GetFlappyBird().HasHitPipe()) {
                return;
            }
            game.GetEntityManager().getPipeManager().Update();
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false && !spacebarDown) {
                game.GetFlappyBird().GetPhysics().Jump();
                wingFlapSfx.Play();
                hasJumped = true;
                spacebarDown = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Space)) {
                spacebarDown = false;
                hasJumped = false;
            }
            MouseState state = Mouse.GetState(game.Window);
            baseX--;
            if (baseX == -336) {
                baseX = 0;
            }
        }

        private void DrawScore() {
            String scoreString = game.GetFlappyBird().GetScore().ToString();
            char[] numberChars = scoreString.ToCharArray();
            int[] numbers = new int[numberChars.Length];
            for (int i = 0; i < numberChars.Length; i++) {
                numbers[i] = int.Parse(numberChars[i].ToString());
            }
            int x = 115;
            Texture2D numberTexture;
            for (int i = 0; i < numbers.Length; i++) {
                numberTexture = DrawNumber(numbers[i], x);
                x += numberTexture.Width;
            }
        }

        private Texture2D DrawNumber(int number, int x) {
            Texture2D texture = scoreTexures[number];
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, 36);
            Vector2 origin = new Vector2(0, 0);
            Vector2 location = new Vector2(x, 40);
            game.GetSpriteBatch().Draw(texture, location, sourceRectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 1);
            return texture;
        }

        public SoundEffect GetWingFlapSfx() {
            return wingFlapSfx;
        }

        public SoundEffect GetDieSfx() {
            return dieSfx;
        }

        public SoundEffect GetHitSfx() {
            return hitSfx;
        }

        public SoundEffect GetPointSfx() {
            return pointSfx;
        }

        public Texture2D[][] GetBirdAnimations() {
            return birdAnimations;
        }
    }
}
