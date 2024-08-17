using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMennoPlochaet.Core;
using GameMennoPlochaet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameMennoPlochaet.Scenes
{
    internal class DefeatScene
    {
        private Texture2D background;
        private Texture2D retryButton;
        private Vector2 retryButtonPos = new Vector2(1920 / 2 - 300 / 2, 900);
        private Rectangle retryButtonRec;
        private Texture2D quitButton;
        private Vector2 quitButtonPos = new Vector2(1920 / 2 - 300 / 2, 700);
        private Rectangle quitButtonRec;

        private Vector2 mousePoint = new Vector2();

        public DefeatScene()
        {
            background = ContentLoader.DefeatScreenBackground;
            retryButton = ContentLoader.RetryButton;
            quitButton = ContentLoader.QuitButton;
        }
        public void Update()
        {
            MouseState mouseState = Mouse.GetState();
            mousePoint = new Vector2(mouseState.X, mouseState.Y);
            //Retry
            retryButtonRec = new Rectangle((int)retryButtonPos.X, (int)retryButtonPos.Y, 207, 111);
            if (retryButtonRec.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
            {
                GamestateManager.getInstance().UpdateScene(Data.Scenes.Menu);
            }

            //Quit
            quitButtonRec = new Rectangle((int)quitButtonPos.X, (int)quitButtonPos.Y, 207, 111);
            mousePoint = new Vector2(mouseState.X, mouseState.Y);
            if (quitButtonRec.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
            {
                Game1.Instance.ExitGame();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);

            Color retryButtonColor = retryButtonRec.Contains(mousePoint) ? Color.LightGray : Color.White;
            spriteBatch.Draw(retryButton, retryButtonPos, retryButtonColor);

            Color quitButtonColor = quitButtonRec.Contains(mousePoint) ? Color.LightGray : Color.White;
            spriteBatch.Draw(quitButton, quitButtonPos, quitButtonColor);

        }

    }
}
