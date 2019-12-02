using Flappy_Bird.entity;
using Flappy_Bird_Emulation.fb.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird_Emulation.fb.logic {
    public class EntityManager {

        private readonly Dictionary<EntityType, List<Entity>> entities = new Dictionary<EntityType, List<Entity>>();

        private readonly PipeManager pipeManager = new PipeManager();

        public EntityManager() {
            /**
             * Empty
             */
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            foreach (KeyValuePair<EntityType, List<Entity>> entry in entities) {
                foreach (Entity e in entry.Value) {
                    e.Draw(spriteBatch);
                }
            }
        }

        public void Update(GameTime gameTime) {
            foreach (KeyValuePair<EntityType, List<Entity>> entry in entities) {
                foreach (Entity e in entry.Value) {
                    e.Update(gameTime);
                }
            }
        }

        public void AddEntity(Entity entity) {
            List<Entity> entityList = GetEntitiesByType(entity.GetEntityType());
            if (entityList == null) {
                entityList = new List<Entity>();
            }
            if (entities.ContainsKey(entity.GetEntityType())) {
                entities.Remove(entity.GetEntityType());
            }
            entityList.Add(entity);
            Console.WriteLine("Adding entity type=" + entity.GetType() + ", entity=" + entity);
            entities.Add(entity.GetEntityType(), entityList);
            Console.WriteLine(entities.Count);
        }

        public void RemoveEntity(Entity entity) {
            List<Entity> entityList = GetEntitiesByType(entity.GetEntityType());
            if (entityList == null) {
                entityList = new List<Entity>();
            }
            entityList.Remove(entity);
            entities.Add(entity.GetEntityType(), entityList);
        }

        public List<Entity> GetAllEntities() {
            List<Entity> entityList = new List<Entity>();
            foreach (KeyValuePair<EntityType, List<Entity>> entry in entities) {
                foreach (Entity e in entry.Value) {
                    entityList.Add(e);
                }
            }
            return entityList;
        }

        public List<Entity> GetEntitiesByType(EntityType type) {
            List<Entity> entityList;
            entities.TryGetValue(type, out entityList);
            return entityList;
        }

        public PipeManager getPipeManager() {
            return pipeManager;
        }


    }
}
