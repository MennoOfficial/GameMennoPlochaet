using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using TestProject.Animations;

namespace GameMennoPlochaet.Entities.Enemies
{
    internal class Bird : Entity
    {
        public override Vector2 position { get; set; }

        private float initialY;
        private Vector2 startPosition;
        private Vector2 endPosition;
        private float amplitude = 5f;
        private float frequency = 0.1f;
        private Animation CurrentAnimation;
        private float speed = 100f; // Speed of the bird's horizontal movement
        private bool movingRight = true; // Flag to check the direction of movement

        public Bird(Texture2D texture, Vector2 initialPosition, Vector2 endPosition)
        {
            position = initialPosition;
            startPosition = initialPosition;
            this.endPosition = endPosition;
            initialY = initialPosition.Y;
            Texture = texture;

            // Initialize hitbox based on texture dimensions
            Hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width / 8, texture.Width / 8);

            // Initialize the animation
            CurrentAnimation = new Animation();
            CurrentAnimation.addFrame(8, texture.Width / 8);
        }

        public override void Update(GameTime gameTime)
        {
            // Update animation
            CurrentAnimation.Update(gameTime);

            // Calculate vertical movement using a sine wave
            float time = (float)gameTime.TotalGameTime.TotalSeconds;
            float offsetY = amplitude * (float)Math.Sin(frequency * time);

            // Calculate horizontal movement
            float deltaX = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 newPosition = position;

            if (movingRight)
            {
                newPosition.X += deltaX;
                if (newPosition.X >= endPosition.X)
                {
                    newPosition.X = endPosition.X;
                    movingRight = false;
                }
            }
            else
            {
                newPosition.X -= deltaX;
                if (newPosition.X <= startPosition.X)
                {
                    newPosition.X = startPosition.X;
                    movingRight = true;
                }
            }

            // Apply vertical offset
            newPosition.Y = initialY + offsetY;

            // Assign the new position back to the position property
            position = newPosition;

            // Update hitbox position
            Hitbox = new Rectangle(position.ToPoint(), Hitbox.Size);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            // Determine the sprite effects based on the direction of movement
            if (movingRight)
            spriteBatch.Draw(Texture, position, CurrentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 1);
            else
            spriteBatch.Draw(Texture,position, CurrentAnimation.CurrentFrame.SourceRectangle,Color.White
            );
        }
    }
}