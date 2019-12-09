using Flappy_Bird_Emulation.fb.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Flappy_Bird.entity
{

    /// <summary>
    /// Represents an Entity.
    /// </summary>
    public abstract class Entity
    {

        /// <summary>
        /// The entity type.
        /// </summary>
        protected readonly EntityType entityType;

        /// <summary>
        /// Represents the location of the entity.
        /// </summary>
        protected Vector2 location;

        /// <summary>
        /// Represents the rectangle of the entity.
        /// </summary>
        protected Rectangle rectangle;

        public Entity(EntityType entityType, Vector2 location, Rectangle rectangle)
        {
            this.entityType = entityType;
            this.location = location;
            this.rectangle = rectangle;
        }

        /// <summary>
        /// The update logic method of an entity.
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);


        /// <summary>
        /// The drawing of an entity.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to use.</param>
        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Gets the texture to draw.
        /// </summary>
        /// <returns>The texture.</returns>
        public abstract Texture2D GetTexture();

        public Rectangle GetRectangle()
        {
            return rectangle;
        }

        public EntityType GetEntityType()
        {
            return entityType;
        }

        public T Cast<T>(EntityType entityType)
        {
            return (T)Convert.ChangeType(entityType.ToString(), typeof(T));
        }

    }

}
