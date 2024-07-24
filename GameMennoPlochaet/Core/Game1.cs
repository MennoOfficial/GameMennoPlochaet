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
        private MapManager mapManager;

        
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
            ContentLoader contentLoader = new ContentLoader(Content);
            ContentLoader.LoadAllContent();
            mapManager = new MapManager(_spriteBatch, ContentLoader.map, ContentLoader.tileset);

            InitializeGameObjects();
        }
        private void InitializeGameObjects()
        {
            setFullscreen();
            hero = new Hero(GraphicsDevice);
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
            GraphicsDevice.Clear(Color.DeepSkyBlue);
            _spriteBatch.Begin();
            hero.Draw(_spriteBatch);
            mapManager.Draw();
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void setFullscreen()
        {
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();
        }
    }
}