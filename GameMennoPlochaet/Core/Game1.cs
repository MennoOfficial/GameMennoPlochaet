using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameMennoPlochaet.Characters.Hero;
using System.Collections.Generic;
using TiledSharp;
using GameMennoPlochaet.Manager;

namespace GameMennoPlochaet.Core
{

    public class Game1 : Game
    {

        private static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TmxMap map;
        private MapManager mapManager;

        private List<Texture2D> _texture = new();
        Hero hero;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {

            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            map = new TmxMap("Content/Map/Map1.tmx");
            var tileset = Content.Load<Texture2D>(map.Tilesets[0].Name.ToString());
            var tileWidth = map.Tilesets[0].TileWidth;
            var tileHeight = map.Tilesets[0].TileHeight;
            var TileSetTilesWide = tileset.Width / tileWidth;
            mapManager = new MapManager(_spriteBatch, map, tileset, TileSetTilesWide, tileWidth, tileHeight);


            _texture.Add(Content.Load<Texture2D>("Characters/Hero/Animation/Run"));
            _texture.Add(Content.Load<Texture2D>("Characters/Hero/Animation/Jump"));
            _texture.Add(Content.Load<Texture2D>("Characters/Hero/Animation/Idle"));
            _texture.Add(Content.Load<Texture2D>("Characters/Hero/Animation/Walk"));

            InitializeGameObjects();
        }
        private void InitializeGameObjects()
        {
            hero = new Hero(_texture, GraphicsDevice);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            hero.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.PowderBlue);
            _spriteBatch.Begin();
            hero.Draw(_spriteBatch);
            mapManager.Draw();
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}