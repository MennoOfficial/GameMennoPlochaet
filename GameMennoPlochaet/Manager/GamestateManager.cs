using System;
using System.Collections.Generic;
using GameMennoPlochaet.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using GameMennoPlochaet.Scenes;

namespace GameMennoPlochaet.Manager
{
    internal partial class GamestateManager : Component
    {
        private MenuScene ms = new MenuScene();
        private LevelOneScene los = new LevelOneScene();
        private LevelTwoScene lts = new LevelTwoScene();
        private DefeatScene ds = new DefeatScene();
        private WinScene ws = new WinScene();
        
 

        internal override void LoadContent(ContentManager content)
        {
            ms.LoadContent(content);
            los.LoadContent(content);
            lts.LoadContent(content);
            ds.LoadContent(content);
            ws.LoadContent(content);
        }

        internal override void Update(GameTime gameTime)
        {
            switch (Data.CurrentScene)
            {
                case Data.Scenes.Menu:
                    ms.Update(gameTime);
                    break;
                case Data.Scenes.Level1:
                    los.Update(gameTime);
                    break;
                case Data.Scenes.Level2:
                    lts.Update(gameTime);
                    break;
                case Data.Scenes.Defeat:
                    ds.Update(gameTime);
                    break;
                case Data.Scenes.Win:
                    ws.Update(gameTime);
                    break;
            }
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            switch (Data.CurrentScene)
            {
                case Data.Scenes.Menu:
                    ms.Draw(spriteBatch);
                    break;
                case Data.Scenes.Level1:
                    los.Draw(spriteBatch);
                    break;
                case Data.Scenes.Level2:
                    lts.Draw(spriteBatch);
                    break;
                case Data.Scenes.Defeat:
                    ds.Draw(spriteBatch);
                    break;
                case Data.Scenes.Win:
                    ws.Draw(spriteBatch);
                    break;
            }
        }
    }
}