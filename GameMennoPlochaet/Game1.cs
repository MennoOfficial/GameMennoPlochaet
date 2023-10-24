using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameMennoPlochaet.Characters.Hero;
using System.Collections.Generic;

namespace GameMennoPlochaet
{

    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

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
            _texture.Add(Content.Load<Texture2D>("Characters/Hero/Animation/Run"));
            _texture.Add(Content.Load<Texture2D>("Characters/Hero/Animation/Jump"));
            _texture.Add(Content.Load<Texture2D>("Characters/Hero/Animation/Idle"));
            _texture.Add(Content.Load<Texture2D>("Characters/Hero/Animation/Walk"));

            InitializeGameObjects();
        }
        private void InitializeGameObjects()
        {
            hero = new Hero(_texture);
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
            GraphicsDevice.Clear(Color.DarkRed);
            _spriteBatch.Begin();
            hero.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}