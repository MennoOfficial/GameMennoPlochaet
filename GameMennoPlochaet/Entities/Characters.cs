using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TestProject.Animations;

namespace GameMennoPlochaet.Entities
{
    internal abstract class Character
    {
        public Animation[] Animations { get; set; }
        public Vector2 MovingDirection { get; set; }
        public Rectangle nextHitBox;
        public float Gravity = 4.3f; //origineel 4f
        public Vector2 Velocity;

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);

        public void HandleGravity(GameTime gameTime)
        {
            nextHitBox.Y += (int)Velocity.Y;
            nextHitBox.X += (int)Velocity.X;
            Velocity.Y += Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Velocity.X > 0)
            {
                Velocity.X -= Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (Velocity.X < 0)
            {
                Velocity.X += Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
