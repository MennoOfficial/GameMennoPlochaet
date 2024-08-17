using GameMennoPlochaet.Entities.Hero;
using GameMennoPlochaet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace GameMennoPlochaet.UI
{
    internal class Heart
    {
        private List<Rectangle> hearts = new List<Rectangle>();
        public void Update(Hero hero)
        {
            int incrementHearts = 4;
            hearts.Clear();
            for (int i = 0; i < hero.Health.lives; i++)
            {
                hearts.Add(new Rectangle(48 * i + incrementHearts, 4, 48, 32));
                incrementHearts += 3;
            }
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < hearts.Count; i++)
            {
                _spriteBatch.Draw(ContentLoader.Heart, hearts[i], Color.White);
            }
        }
    }
}
