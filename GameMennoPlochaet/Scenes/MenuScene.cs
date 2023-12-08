using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMennoPlochaet.Core;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameMennoPlochaet.Scenes
{
    internal class MenuScene : Component
    {
        private const int MAX_BTNS = 3;
        private Texture2D[] btns = new Texture2D[MAX_BTNS];
        private Rectangle[] btnRects = new Rectangle[MAX_BTNS];
        internal override void LoadContent(ContentManager Content)
        {
            const int INCREMENT_VALUE = 125;
            for (int i = 0; i < btns.Length; i++)
            {
                btns[i] = Content.Load<Texture2D>($"btn{i + 1}");
                btnRects[i] = new Rectangle(0, 0 + (INCREMENT_VALUE * 8), btns[i].Width / 2, btns[i].Height / 2);
            }
        }
        internal override void Update(GameTime gameTime)
        {

        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < btns.Length; i++)
            {
                spriteBatch.Draw(btns[i], btnRects[i], Color.White);
            }
        }
    }
}
