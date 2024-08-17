using GameMennoPlochaet.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameMennoPlochaet.Scenes;
using GameMennoPlochaet.Entities.Hero;

namespace GameMennoPlochaet.Managers
{
    internal partial class GamestateManager
    {
        private static GamestateManager instance;

        private Data.Scenes state = Data.Scenes.Menu;
        private Data.Scenes upcomingState;
        private Data.Scenes currentLevel;
        public bool changeState = false;

        // Scenes
        private MenuScene ms = new MenuScene();
        private LevelOneScene los;
        private LevelTwoScene lts;
        private DefeatScene ds = new DefeatScene();
        private WinScene ws = new WinScene();

        // Hero
        public static Hero hero;

        private GamestateManager() { }

        public static GamestateManager getInstance()
        {
            if (instance == null)
            {
                instance = new GamestateManager();
            }
            return instance;
        }

        public void UpdateScene(Data.Scenes newState)
        {
            if (newState == Data.Scenes.Level1) resetHero();
            upcomingState = newState;
            changeState = true;
        }

        internal void Update(GameTime gameTime)
        {
            if (changeState)
            {
                state = upcomingState;
                changeState = false;

                switch (state)
                {
                    case Data.Scenes.Level1:
                        currentLevel = Data.Scenes.Level1;
                        los = new LevelOneScene();
                        hero.nextHitbox.X = (int)MapManager.PlayerSpawn.X;
                        hero.nextHitbox.Y = (int)MapManager.PlayerSpawn.Y;
                        break;
                    case Data.Scenes.Level2:
                        currentLevel = Data.Scenes.Level2;
                        lts = new LevelTwoScene();
                        hero.nextHitbox.X = (int)MapManager.PlayerSpawn.X;
                        hero.nextHitbox.Y = (int)MapManager.PlayerSpawn.Y;
                        break;
                }
            }

            switch (state)
            {
                case Data.Scenes.Menu:
                    ms.Update();
                    break;
                case Data.Scenes.Level1:
                    los?.Update(gameTime);
                    break;
                case Data.Scenes.Level2:
                    lts?.Update(gameTime);
                    break;
                case Data.Scenes.Defeat:
                    ds.Update();
                    break;
                case Data.Scenes.Win:
                    ws.Update();
                    break;
            }
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
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
        private void resetHero()
        {
            if (hero == null)
            {
                hero = new Hero();
            }
            else
            {
                hero.Health.lives = 3;
                hero.gems.count = 0;
            }
        }
    }
}
