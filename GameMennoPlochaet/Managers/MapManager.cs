using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameMennoPlochaet.Entities;
using TiledSharp;
using System.Diagnostics;
using System.Linq;

namespace GameMennoPlochaet.Managers
{
    public class MapManager
    {
        private SpriteBatch spriteBatch;
        TmxMap map;
        Texture2D tileset;
        int tilesetTilesWide;
        int tileWidth;
        int tileHeight;

        public static Vector2 PlayerSpawn;

        public static List<Rectangle> mapHitbox = new();
        public static List<Entity> enemies = new();
        public static List<Entity> blueGems = new();
        public static List<Entity> stars = new();

        public MapManager(SpriteBatch _spriteBatch, TmxMap _map, Texture2D _tileset)

        {
            spriteBatch = Game1._spriteBatch;
            map = _map;
            tileset = _tileset;


            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;
            tilesetTilesWide = tileset.Width / tileWidth;

            foreach (var tile in map.ObjectGroups["MapHitbox"].Objects)
            {
                mapHitbox.Add(new Rectangle((int)tile.X, (int)tile.Y, (int)tile.Width, (int)tile.Height));
            }
            if (map.ObjectGroups.Contains("PlayerSpawn"))
            {
                var playerGroup = map.ObjectGroups["PlayerSpawn"];
                var spawnObject = playerGroup.Objects.First((o => o.Name == "Spawn"));
                if (spawnObject != null)
                {
                    PlayerSpawn = new Vector2((int)spawnObject.X, (int)spawnObject.Y);
                }
            }

            PlayerSpawn = new Vector2((int)map.ObjectGroups["PlayerSpawn"].Objects["Spawn"].X, (int)map.ObjectGroups["PlayerSpawn"].Objects["Spawn"].Y);

        }
        public void Draw()
        {
            for (var i = 0; i < map.Layers.Count; i++) // Loop through all tile map layers
            
            {
                var layer = map.Layers[i];
                for (var j = 0; j < layer.Tiles.Count; j++) // Loop through the tiles in each layer
                {
                    int gid = layer.Tiles[j].Gid; // Getting the GID
                    if (gid == 0)
                    {
                    }
                    // Processing non-empty tiles

                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetTilesWide;
                    int row = (int)Math.Floor(tileFrame / (double)tilesetTilesWide);
                    float x = (j % map.Width) * map.TileWidth;
                    float y = (float)Math.Floor(j / (double)map.Width) * map.TileHeight;
                    Rectangle tilesetRec = new(tileWidth * column, tileHeight * row, tileWidth, tileHeight);


                    spriteBatch.Draw(tileset, new Vector2(x, y), tilesetRec, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                }
            }
        }
    }
}