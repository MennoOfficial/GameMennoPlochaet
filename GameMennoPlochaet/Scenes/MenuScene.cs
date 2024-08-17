using GameMennoPlochaet.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameMennoPlochaet.Managers;

namespace GameMennoPlochaet.Scenes
{
    public class MenuScene
    {
        private Texture2D background;
        private Texture2D playButton;
        private Vector2 playButtonPos = new Vector2(1920/2 - 207/2, 900);
        private Rectangle playButtonRec;

        private Vector2 mousePoint = new Vector2();

        public MenuScene() {
            background = ContentLoader.MenuScreenBackground;
            playButton = ContentLoader.PlayButton;
        }
        public void Update()
        {
            playButtonRec = new Rectangle((int)playButtonPos.X, (int)playButtonPos.Y, 207, 111);
            MouseState mouseState = Mouse.GetState();
            mousePoint = new Vector2(mouseState.X, mouseState.Y);
            if (playButtonRec.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
            {
                GamestateManager.getInstance().UpdateScene(Data.Scenes.Level1);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);

            Color buttonColor = playButtonRec.Contains(mousePoint) ? Color.LightGray : Color.White;
            spriteBatch.Draw(playButton, playButtonPos, buttonColor);

        }

    }
}
