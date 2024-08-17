using GameMennoPlochaet.Entities.Hero;
using GameMennoPlochaet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMennoPlochaet.UI
{
    internal class Gem
    {
        private List<Rectangle> gems = new List<Rectangle>();
        private const int GemSize = 38;
        private const int Margin = 4;

        public void Update(Hero hero)
        {
            gems.Clear();
            int startX = 1920 - Margin - GemSize;
            int increment = GemSize + Margin;

            for (int i = 0; i < hero.gems.count; i++)
            {
                gems.Add(new Rectangle(startX - (i * increment), Margin, GemSize, GemSize));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var gem in gems)
            {
            spriteBatch.Draw(ContentLoader.BlueGemSingleTexture, gem, Color.White);
            }
        }
    }
}

