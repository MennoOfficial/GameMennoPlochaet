using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameMennoPlochaet.Managers;
using GameMennoPlochaet.Core;
using GameMennoPlochaet.Entities.Hero;

namespace GameMennoPlochaet
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        private GamestateManager _gamestateManager;
        public static Game1 Instance { get; private set; }
        public Game1()
        {
            Instance = this;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            SetFullscreen();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            ContentLoader.Initialize(Content);
            ContentLoader.LoadAllContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _gamestateManager = GamestateManager.getInstance();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _gamestateManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            GamestateManager.getInstance().Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void SetFullscreen()
        {
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Window.IsBorderless = true;
            _graphics.ApplyChanges();
        }
        public void ExitGame()
        {
            Exit();
        }
    }
}
