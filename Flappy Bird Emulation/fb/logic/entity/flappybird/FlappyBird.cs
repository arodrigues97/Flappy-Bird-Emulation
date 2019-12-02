using Flappy_Bird.fb.Screen;
using Flappy_Bird.entity;
using Flappy_Bird_Emulation.fb.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Flappy_Bird_Emulation.fb.logic.entity.flappybird;

namespace Flappy_Bird.fb.entity {
    public class FlappyBird : Entity {

        public const long FLAP_SPEED = 200L;

        private static readonly Vector2 START_LOCATION = new Vector2(50, 250);

        private readonly BirdType type;

        private readonly BirdPhysics physics;

        private long flapChange;

        private int flapIndex;

        private bool hitPipe;

        private int score;

        private bool hitSoundPlayed;

        private bool gameOverSoundPlayed;

        public FlappyBird(BirdType type) : base(EntityType.BIRD, START_LOCATION, new Rectangle((int)START_LOCATION.X, (int)START_LOCATION.Y, 24, 24)) {
            this.type = type;
            this.physics = new BirdPhysics();
            this.flapChange = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public FlappyBird() : this((BirdType)Enum.GetValues(typeof(BirdType)).GetValue(FlappyBirdGame.Random.Next(Enum.GetValues(typeof(BirdType)).Length))) { }

        public override void Draw(SpriteBatch spriteBatch) {
            Texture2D texture = GetTexture();
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Vector2 origin = new Vector2(0, 0);
            spriteBatch.Draw(texture, location, sourceRectangle, Color.White, physics.GetRotation(), origin, 1.0f, SpriteEffects.None, 1);
        }

        public override void Update(GameTime gameTime) {
            if (IsDead()) {
                if (!hitSoundPlayed) {
                    hitSoundPlayed = true;
                    PlayScreen playScreen = (PlayScreen)GameManager.GetGame().GetGameScreen();
                    playScreen.GetHitSfx().Play();
                }
                physics.SetRotation(90 % 360);
                return;
            }
            if (hitPipe) {
                if (!hitSoundPlayed) {
                    hitSoundPlayed = true;
                    PlayScreen playScreen = (PlayScreen)GameManager.GetGame().GetGameScreen();
                    playScreen.GetHitSfx().Play();
                }
                location.Y+=10;
                return;
            }
            CheckCollision();
            if ((long)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - flapChange > FLAP_SPEED) {
                flapChange = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                flapIndex++;
                if (flapIndex == 3) {
                    flapIndex = 0;
                }
            }
            Vector2 l = location;
            physics.Calculate(ref l);
            location = l;
            rectangle.X = (int) location.X;
            rectangle.Y = (int) location.Y;
        }

        public bool IsDead() {
            if (location.Y >= 375) {
                if (!gameOverSoundPlayed) {
                    gameOverSoundPlayed = true;
                    PlayScreen playScreen = (PlayScreen)GameManager.GetGame().GetGameScreen();
                    playScreen.GetDieSfx().Play();
                }
                return true;
            }
            return false;
        }

        private void CheckCollision() {
            List<Entity> entities = (List<Entity>)GameManager.GetGame().GetEntityManager().GetEntitiesByType(EntityType.PIPE);
            if (entities != null && entities.Count > 0) {
                foreach (Entity e in entities) {
                    if (e.GetRectangle().Intersects(GetRectangle())) {
                        hitPipe = true;
                        return;
                    }
                }

            }
            return;
        }

        public void IncrementScore() {
            score++;
        }

        public override Texture2D GetTexture() {
            Texture2D[] birdAnimations = ((PlayScreen)GameManager.GetGame().GetGameScreen()).GetBirdAnimations()[(int)type];
            return birdAnimations[flapIndex];
        }

        public int GetScore() {
            return score;
        }

        public bool HasHitPipe() {
            return hitPipe;
        }

        public BirdPhysics GetPhysics() {
            return physics;
        }
    }
}
