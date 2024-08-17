using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMennoPlochaet.Core
{
    public static class Data
    {
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static GraphicsDevice GraphicsDevice { get; set; }
        public enum Scenes
        {
            Menu,
            Level1,
            Level2,
            Defeat,
            Win
        }
        public static Scenes CurrentScene { get; set; } = Scenes.Menu;
    }
}
