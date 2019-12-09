using Flappy_Bird.entity;
using Flappy_Bird.fb.Screen;
using Flappy_Bird_Emulation.fb.entity;
using Flappy_Bird_Emulation.fb.entity.pipe;
using Flappy_Bird_Emulation.fb.logic.entity.flappybird;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Flappy_Bird.fb.entity
{

    /// <summary>
    /// Represents the Flappy Bird entity.
    /// </summary>
    public class FlappyBird : Entity
    {

        /// <summary>
        /// Represents the flap speed of the bird,
        /// </summary>
        public const long FLAP_SPEED = 100L;

        /// <summary>
        /// Represents the starting location.
        /// </summary>
        private static readonly Vector2 START_LOCATION = new Vector2(50, 250);

        /// <summary>
        /// Represents the bird type.
        /// </summary>
        private readonly BirdType type;

        /// <summary>
        /// Represents the bird physics.
        /// </summary>
        private readonly BirdPhysics physics;

        /// <summary>
        /// Represents the delay in changing the flap animation.
        /// </summary>
        private long flapChange;

        /// <summary>
        /// The flap index.
        /// </summary>
        private int flapIndex;

        /// <summary>
        /// If we died by hitting a pipe.
        /// </summary>
        private bool hitPipe;

        /// <summary>
        /// The score.
        /// </summary>
        private int score;

        /// <summary>
        /// If we played the hit sound.
        /// </summary>
        private bool hitSoundPlayed;

        /// <summary>
        /// IF we played the game over sound.
        /// </summary>
        private bool gameOverSoundPlayed;

        /// <summary>
        /// Constructs a new Flappy Bird.
        /// </summary>
        /// <param name="type">The Bird Type.</param>
        public FlappyBird(BirdType type) : base(EntityType.BIRD, START_LOCATION, new Rectangle((int)START_LOCATION.X, (int)START_LOCATION.Y, 24, 24))
        {
            this.type = type;
            this.physics = new BirdPhysics();
            this.flapChange = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public FlappyBird() : this((BirdType)Enum.GetValues(typeof(BirdType)).GetValue(FlappyBirdGame.Random.Next(Enum.GetValues(typeof(BirdType)).Length))) { }

        /// <summary>
        /// The draw method for the flappy bird.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = GetTexture();
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Vector2 origin = new Vector2(0, 0);
            spriteBatch.Draw(texture, location, sourceRectangle, Color.White, physics.GetRotation(), origin, 1.0f, SpriteEffects.None, 1);
        }

        /// <summary>
        /// The update logic of the flappy bird.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (IsDead())
            {
                if (!hitSoundPlayed)
                {
                    hitSoundPlayed = true;
                    PlayScreen playScreen = (PlayScreen)GameManager.GetGame().GetGameScreen();
                    playScreen.GetHitSfx().Play();
                }
                return;
            }
            if (hitPipe)
            {
                if (!hitSoundPlayed)
                {
                    hitSoundPlayed = true;
                    PlayScreen playScreen = (PlayScreen)GameManager.GetGame().GetGameScreen();
                    playScreen.GetHitSfx().Play();
                }
                location.Y += 10;
                return;
            }
            CheckCollision();
            if ((long)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - flapChange > FLAP_SPEED)
            {
                flapChange = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                flapIndex++;
                if (flapIndex == 3)
                {
                    flapIndex = 0;
                }
            }
            Vector2 l = location;
            physics.Calculate(ref l);
            location = l;
            rectangle.X = (int)location.X;
            rectangle.Y = (int)location.Y;
        }

        /// <summary>
        /// Checks if the flappy bird is dead.
        /// </summary>
        /// <returns></returns>
        public bool IsDead()
        {
            Pipe pipe = GameManager.GetGame().GetEntityManager().getPipeManager().GetPipe();
            bool abovePipe = location.Y < 0 && (pipe != null && pipe.GetRectangle().X <= GetRectangle().X);
            if (location.Y >= 375 || abovePipe)
            {
                if (abovePipe)
                {
                    hitPipe = true;
                }
                if (!gameOverSoundPlayed)
                {
                    gameOverSoundPlayed = true;
                    PlayScreen playScreen = (PlayScreen)GameManager.GetGame().GetGameScreen();
                    playScreen.GetDieSfx().Play();
                    GameManager.GetGame().GetHighscoreManager().AddScore(score);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the flappy bird has collided with a pipe.
        /// </summary>
        private void CheckCollision()
        {
            List<Entity> entities = (List<Entity>)GameManager.GetGame().GetEntityManager().GetEntitiesByType(EntityType.PIPE);
            if (entities != null && entities.Count > 0)
            {
                foreach (Entity e in entities)
                {
                    if (e.GetRectangle().Intersects(GetRectangle()))
                    {
                        hitPipe = true;
                        return;
                    }
                }

            }
            return;
        }

        /// <summary>
        /// Increments the score.
        /// </summary>
        public void IncrementScore()
        {
            score++;
        }

        /// <summary>
        /// Gets the texture to draw.
        /// </summary>
        /// <returns>The texture.</returns>
        public override Texture2D GetTexture()
        {
            Texture2D[] birdAnimations = ((PlayScreen)GameManager.GetGame().GetGameScreen()).GetBirdAnimations()[(int)type];
            return birdAnimations[flapIndex];
        }

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <returns></returns>
        public int GetScore()
        {
            return score;
        }

        /// <summary>
        /// If a pipe has been hit.
        /// </summary>
        /// <returns></returns>
        public bool HasHitPipe()
        {
            return hitPipe;
        }

        /// <summary>
        /// Gets the bird physics.
        /// </summary>
        /// <returns>The bird physics.</returns>
        public BirdPhysics GetPhysics()
        {
            return physics;
        }
    }
}
