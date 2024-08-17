using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace GameMennoPlochaet.Entities.Finish
{
    internal class Star : Entity
    {
        public override Vector2 position { get; set; }

        private float initialY;
        private float offsetY = 10f;
        private float amplitude = 2f;
        private float frequency = 0.2f;


        public Star(Texture2D texture, Vector2 initialPosition)
        {
            position = initialPosition;
            initialY = initialPosition.Y;
            Texture = texture;

            Hitbox = new Rectangle((int)position.X, (int)position.Y, 48, 48);
        }

        public override void Update(GameTime gameTime)
        {

            float time = (float)gameTime.TotalGameTime.TotalSeconds;
            float offsetY = amplitude * (float)Math.Sin(frequency * time);

            position = new Vector2(position.X, initialY + offsetY);

            Hitbox = new Rectangle(position.ToPoint(), Hitbox.Size);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Texture,
                position,
                null,
                Color.White
            );
        }

    }
}
