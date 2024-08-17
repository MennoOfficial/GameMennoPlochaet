using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using TestProject.Animations;
using System.Diagnostics;

namespace GameMennoPlochaet.Entities.Item
{
    internal class BlueGem : Entity
    {
        public override Vector2 position { get; set; }

        private float initialY;
        private float amplitude = 10f;
        private float frequency = 0.2f;
        private Animation CurrentAnimation;


        public BlueGem(Texture2D texture, Vector2 initialPosition)
        {
            position = initialPosition;
            initialY = initialPosition.Y;
            Texture = texture;

            Hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width / 5, texture.Width / 5);

            CurrentAnimation = new Animation();
            CurrentAnimation.addFrame(4, texture.Width / 5);
        }

        public override void Update(GameTime gameTime)
        {
            CurrentAnimation.Update(gameTime);

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
                CurrentAnimation.CurrentFrame.SourceRectangle,
                Color.White
            );
        }

    }
}