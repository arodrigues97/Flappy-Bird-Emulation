using Flappy_Bird.entity;
using Flappy_Bird.fb;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flappy_Bird_Emulation.fb.entity.pipe
{
    /// <summary>
    /// Represents a Pipe entity.
    /// </summary>
    public class Pipe : Entity
    {

        /// <summary>
        /// The pipe direction.
        /// </summary>
        private readonly PipeDirection pipeDirection;

        /// <summary>
        /// Represents the pipe texture.
        /// </summary>
        private Texture2D pipeTexture;

        /// <summary>
        /// Constructs a new Pipe.
        /// </summary>
        /// <param name="pipeDirection">The pipe direction.</param>
        /// <param name="location">The location of the pipe.</param>
        /// <param name="rectangle">The rectangle of the pipe.</param>
        public Pipe(PipeDirection pipeDirection, Vector2 location, Rectangle rectangle) : base(EntityType.PIPE, location, rectangle)
        {
            this.pipeDirection = pipeDirection;
        }

        /// <summary>
        /// Draws the Pipe.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to use.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = GetTexture();
            Rectangle sourceRectangle = new Rectangle(0, 0, rectangle.Width, rectangle.Height);
            Vector2 origin = new Vector2(0, 0);
            Vector2 location = new Vector2(rectangle.X, rectangle.Y);
            spriteBatch.Draw(texture, location, sourceRectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0.9f);

        }

        /// <summary>
        /// Gets the Pipe texture.
        /// </summary>
        /// <returns>The pipe texture.</returns>
        public override Texture2D GetTexture()
        {
            if (pipeTexture == null)
            {
                pipeTexture = GameManager.GetGame().GetSpriteSheet().GetTexture("pipe-" + pipeDirection.ToString().ToLower());
            }
            return pipeTexture;
        }

        /// <summary>
        /// The update logic of a pipe.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (GameManager.GetGame().GetFlappyBird().IsDead())
            {
                return;
            }
            rectangle.X--;
        }
    }

    /// <summary>
    /// Representa a Pipe direction.
    /// </summary>
    public enum PipeDirection
    {
        UP = 0,
        DOWN = 1
    }
}
