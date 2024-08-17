using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMennoPlochaet.Core;
using GameMennoPlochaet.Entities.Enemies;
using GameMennoPlochaet.Entities.Finish;
using GameMennoPlochaet.Entities.Hero;
using GameMennoPlochaet.Entities.Item;
using GameMennoPlochaet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameMennoPlochaet.Scenes
{
    internal class LevelTwoScene : Component
    {
        private MapManager mapManager;
        private Rectangle nextLevel;
        private Rectangle deathBarier;
        public LevelTwoScene()
        {
            MapManager.mapHitbox.Clear();
            MapManager.enemies.Clear();
            MapManager.blueGems.Clear();
            MapManager.stars.Clear();

            mapManager = new MapManager(Data.SpriteBatch, ContentLoader.Map2, ContentLoader.Tileset2);
            foreach (var o in ContentLoader.Map2.ObjectGroups["Bats"].Objects)
            {
                Vector2 location = new Vector2((int)o.X, (int)o.Y);
                MapManager.enemies.Add(new Bat(ContentLoader.BatTexture, location));
            }
            foreach (var o in ContentLoader.Map2.ObjectGroups["Items"].Objects)
            {
                Vector2 location = new Vector2((int)o.X, (int)o.Y);
                MapManager.blueGems.Add(new BlueGem(ContentLoader.BlueGemTexture, location));
            }
            foreach (var o in ContentLoader.Map2.ObjectGroups["Buttons"].Objects)
            {   
                MapManager.enemies.Add(new Landmine(ContentLoader.ExplosionTexture, new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height)));
            }
            foreach (var o in ContentLoader.Map2.ObjectGroups["LevelTwoEnd"].Objects)
            {
                Vector2 location = new Vector2((int)o.X, (int)o.Y);
                MapManager.stars.Add(new Star(ContentLoader.Star, location));
            }
            var rec = ContentLoader.Map2.ObjectGroups["LevelTwoEnd"].Objects["Objective"];
            nextLevel = new Rectangle((int)rec.X, (int)rec.Y, (int)rec.Width, (int)rec.Height);
            var dangerRec = ContentLoader.Map2.ObjectGroups["Danger"].Objects["Danger"];
            deathBarier = new Rectangle((int)dangerRec.X, (int)dangerRec.Y, (int)dangerRec.Width, (int)dangerRec.Height);
        }


        internal override void Update(GameTime gameTime)
        {
            GamestateManager.hero.Update(gameTime);
            MapManager.enemies.ForEach(b => b.Update(gameTime));

            MapManager.blueGems.ForEach(g => g.Update(gameTime));
            if (GamestateManager.hero.Hitbox.Intersects(nextLevel) && GamestateManager.hero.gems.count == 3)
            {
                GamestateManager.getInstance().UpdateScene(Data.Scenes.Win);
            }

            if (GamestateManager.hero.Hitbox.Intersects(deathBarier))
            {
                GamestateManager.hero.Health.lives = 0;
            }
            MapManager.stars.ForEach(s => s.Update(gameTime));
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            mapManager.Draw();
            
            spriteBatch.Draw(ContentLoader.Background2, new Vector2(0, 0), new Rectangle(0, 0, 1920, 1080), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.Draw(ContentLoader.map2, new Vector2(0, 0), new Rectangle(0, 0, 1920, 1080), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            
            MapManager.enemies.ForEach(b => b.Draw(spriteBatch));
            MapManager.blueGems.ForEach(g => g.Draw(spriteBatch));
            MapManager.stars.ForEach(s => s.Draw(spriteBatch));
            GamestateManager.hero.Draw(spriteBatch);
        }
    }
}
