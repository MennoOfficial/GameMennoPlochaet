using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TiledSharp;

namespace GameMennoPlochaet.Manager
{
    //This class is mostly copied from the internet.
    public class MapManager
    {
        //All the variables which we will need
        private SpriteBatch spriteBatch;
        TmxMap map;
        Texture2D tileset;
        int tilesetTilesWide;
        int tileWidth;
        int tileHeight;


        public static List<Rectangle> mapHitbox = new();
        public MapManager(SpriteBatch _spriteBatch, TmxMap _map, Texture2D _tileset)

        {
            spriteBatch = _spriteBatch;
            map = _map;
            tileset = _tileset;
            
            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;
            tilesetTilesWide = tileset.Width / tileWidth;

            foreach (var tile in map.ObjectGroups["MapHitbox"].Objects)
            {
                mapHitbox.Add(new Rectangle((int)tile.X, (int)tile.Y, (int)tile.Width, (int)tile.Height));
            }

        }
        public void Draw()
        {
            for (var i = 0; i < map.Layers.Count; i++)//This loops through all the tile map layers present on our tile map
            {
                for (var j = 0; j < map.Layers[i].Tiles.Count; j++)//this loops through the tiles in each tile layer
                {
                    int gid = map.Layers[i].Tiles[j].Gid;//Getting the GID
                    if (gid == 0)
                    {
                        //If empty then do nothing
                    }
                    else//If not empty
                    {//Some complex math to check for the tile position 😦
                        int tileFrame = gid - 1;
                        int column = tileFrame % tilesetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);
                        float x = (j % map.Width) * map.TileWidth;
                        float y = (float)Math.Floor(j / (double)map.Width) * map.TileHeight;
                        Rectangle tilesetRec = new((tileWidth) * column, (tileHeight) * row, tileWidth, tileHeight);//The origin rectangle

                        //spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);//Drawing the tile
                        spriteBatch.Draw(tileset, new Vector2(x, y), tilesetRec, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);//Drawing the tile with Layer Depth

                        if (i == 2)
                        {
                            mapHitbox.Add(new Rectangle((int)x, (int)y, tileWidth, tileHeight));
                        }

                    }
                }
            }
        }
    }
}
