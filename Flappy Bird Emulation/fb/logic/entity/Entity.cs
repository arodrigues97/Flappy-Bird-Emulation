using Flappy_Bird_Emulation.fb.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird.entity {

    public abstract class Entity {

        protected readonly EntityType entityType;

        /// <summary>
        /// Represents the location of the entity.
        /// </summary>
        protected Vector2 location;

        protected Rectangle rectangle;

        public Entity(EntityType entityType, Vector2 location, Rectangle rectangle) {
            this.entityType = entityType;
            this.location = location;
            this.rectangle = rectangle;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract Texture2D GetTexture();

        public Rectangle GetRectangle() {
            return rectangle;
        }

        public EntityType GetEntityType() {
            return entityType;
        }

        public T Cast<T>(EntityType entityType) {
            return (T)Convert.ChangeType(entityType.ToString(), typeof(T));
        }

    }

}
