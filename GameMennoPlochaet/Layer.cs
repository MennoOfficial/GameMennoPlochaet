using GameMennoPlochaet.Core;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMennoPlochaet
{
    internal class Layer
    {
        private readonly Texture2D texture;
        private Vector2 position;
        private readonly float depth;
        private readonly float moveScale;

        public Layer(Texture2D texture, float depth, float moveScale)
        {
            this.texture = texture;
            this.depth = depth;
            this.moveScale = moveScale;
            position = Vector2.Zero;
        }

        public void update(float movement, GameTime gametime)
        {
            position.X += movement * moveScale * gametime.ElapsedGameTime;
            position.X %= texture.Width;
        }
        public void Draw()
        {
            Data.SpriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, depth);
        }
    }
}
