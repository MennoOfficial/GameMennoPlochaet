using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMennoPlochaet.Core;
using GameMennoPlochaet.Entities.Enemies;
using GameMennoPlochaet.Entities.Item;
using GameMennoPlochaet.Entities.Finish;
using GameMennoPlochaet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMennoPlochaet.Scenes
{
    internal class LevelOneScene : Component
    {
        private MapManager mapManager;
        private Rectangle nextLevel;
        public LevelOneScene()
        {
            MapManager.mapHitbox.Clear();
            MapManager.enemies.Clear();
            MapManager.blueGems.Clear();
            MapManager.stars.Clear();

            mapManager = new MapManager(Data.SpriteBatch ,ContentLoader.Map, ContentLoader.Tileset);
            foreach (var o in ContentLoader.Map.ObjectGroups["Birds"].Objects)
            {
                Vector2 location = new Vector2((int)o.X, (int)o.Y);
                Vector2 locationEnd = new Vector2((int)o.X + (int)o.Width, (int) o.Y);
                MapManager.enemies.Add(new Bird(ContentLoader.BirdTexture, location , locationEnd));
            }
            foreach (var o in ContentLoader.Map.ObjectGroups["Items"].Objects)
            {
                Vector2 location = new Vector2((int)o.X, (int)o.Y);
                MapManager.blueGems.Add(new BlueGem(ContentLoader.BlueGemTexture, location));
            }
            foreach (var o in ContentLoader.Map.ObjectGroups["LevelOneEnd"].Objects)
            {
                Vector2 location = new Vector2((int)o.X, (int)o.Y);
                MapManager.stars.Add(new Star(ContentLoader.Star, location));
            }
            var rec = ContentLoader.Map.ObjectGroups["LevelOneEnd"].Objects["Objective"];
            nextLevel = new Rectangle((int)rec.X, (int)rec.Y, (int)rec.Width, (int)rec.Height);
        }


        internal override void Update(GameTime gameTime)
        {
            GamestateManager.hero.Update(gameTime);
            MapManager.enemies.ForEach(b =>b.Update(gameTime));

            MapManager.blueGems.ForEach(g => g.Update(gameTime));
            MapManager.stars.ForEach(s => s.Update(gameTime));
            if (GamestateManager.hero.Hitbox.Intersects(nextLevel) && GamestateManager.hero.gems.count == 1)
            {
                GamestateManager.getInstance().UpdateScene(Data.Scenes.Level2);
            }
            
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            mapManager.Draw();

            spriteBatch.Draw(ContentLoader.Background1, new Vector2(0, 0), new Rectangle(0, 0, 1920, 1080), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.Draw(ContentLoader.map1, new Vector2(0, 0), new Rectangle(0, 0, 1920, 1080), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

            MapManager.enemies.ForEach(b => b.Draw(spriteBatch));
            MapManager.blueGems.ForEach(g => g.Draw(spriteBatch));
            MapManager.stars.ForEach(s => s.Draw(spriteBatch));
            GamestateManager.hero.Draw(spriteBatch);
        }
    }
}

