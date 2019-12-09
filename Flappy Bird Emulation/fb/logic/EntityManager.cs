using Flappy_Bird.entity;
using Flappy_Bird_Emulation.fb.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Flappy_Bird_Emulation.fb.logic
{
    public class EntityManager
    {

        /// <summary>
        /// The dictionary set of entities in the game.
        /// </summary>
        private readonly Dictionary<EntityType, List<Entity>> entities = new Dictionary<EntityType, List<Entity>>();

        /// <summary>
        /// Represents the instance of the pipe manager.
        /// </summary>
        private readonly PipeManager pipeManager = new PipeManager();

        /// <summary>
        /// Constructs a new EntityManager.
        /// </summary>
        public EntityManager()
        {
            /**
             * Empty
             */
        }

        /// <summary>
        /// Draws all the enities.
        /// </summary>
        /// <param name="gameTime">The gametime.</param>
        /// <param name="spriteBatch">The spritebatch to use.</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (KeyValuePair<EntityType, List<Entity>> entry in entities)
            {
                foreach (Entity e in entry.Value)
                {
                    e.Draw(spriteBatch);
                }
            }
        }

        /// <summary>
        /// Updates the entities.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<EntityType, List<Entity>> entry in entities)
            {
                foreach (Entity e in entry.Value)
                {
                    e.Update(gameTime);
                }
            }
        }

        /// <summary>
        /// Adds an entity to the manager system.
        /// </summary>
        /// <param name="entity">The entity instance.</param>
        public void AddEntity(Entity entity)
        {
            List<Entity> entityList = GetEntitiesByType(entity.GetEntityType());
            if (entityList == null)
            {
                entityList = new List<Entity>();
            }
            if (entities.ContainsKey(entity.GetEntityType()))
            {
                entities.Remove(entity.GetEntityType());
            }
            entityList.Add(entity);
            Console.WriteLine("Adding entity type=" + entity.GetType() + ", entity=" + entity);
            entities.Add(entity.GetEntityType(), entityList);
            Console.WriteLine(entities.Count);
        }

        /// <summary>
        /// Clears an entity type list in the dictionary.
        /// </summary>
        /// <param name="type">The entity type.</param>
        public void Clear(EntityType type)
        {
            entities.Remove(type);
        }

        /// <summary>
        /// Removes an entity.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        public void RemoveEntity(Entity entity)
        {
            List<Entity> entityList = GetEntitiesByType(entity.GetEntityType());
            if (entityList == null)
            {
                entityList = new List<Entity>();
            }
            if (entities.ContainsKey(entity.GetEntityType()))
            {
                entities.Remove(entity.GetEntityType());
            }
            entityList.Remove(entity);
            entities.Add(entity.GetEntityType(), entityList);
        }

        /// <summary>
        /// Gets all the entities.
        /// </summary>
        /// <returns>The entity list.</returns>
        public List<Entity> GetAllEntities()
        {
            List<Entity> entityList = new List<Entity>();
            foreach (KeyValuePair<EntityType, List<Entity>> entry in entities)
            {
                foreach (Entity e in entry.Value)
                {
                    entityList.Add(e);
                }
            }
            return entityList;
        }

        /// <summary>
        /// Gets the entity list by the type of entity.
        /// </summary>
        /// <param name="type">The entity type.</param>
        /// <returns>The list of entities.</returns>
        public List<Entity> GetEntitiesByType(EntityType type)
        {
            List<Entity> entityList;
            entities.TryGetValue(type, out entityList);
            return entityList;
        }

        /// <summary>
        /// Gets the Pipe Manager.
        /// </summary>
        /// <returns>The pipe manager.</returns>
        public PipeManager getPipeManager()
        {
            return pipeManager;
        }


    }
}
