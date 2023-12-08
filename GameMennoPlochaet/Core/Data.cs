using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMennoPlochaet.Core
{
    internal class Data
    {
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
