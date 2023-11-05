using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using TestProject.Animations;
using System.Collections.Generic;
using System.Linq;

namespace GameMennoPlochaet.Characters.Hero
{
    internal class Hero
    {
        public Animation animation;
        public Vector2 position = Vector2.Zero;
        public Vector2 speed = new Vector2(2, 2);
        public const float Run = 2f;
        public const float Gravity = 10f;
        public const float Jump = 20f;
        public Vector2 acceleration = new Vector2(0.2f, 0.2f);
        public List<Texture2D> textureListHero = new();
        private bool flipped = false;
        public Animation CurrentAnimation;
        public Animation[] Animations;
        public Texture2D CurrentTexture;
        public Hero(List<Texture2D> textureList)
        {
            textureListHero = textureList;
            Animations = new Animation[]
            {
                new Animation(),
                new Animation(),
                new Animation(),
                new Animation(),
                new Animation()
            };
            CurrentAnimation = Animations[2];
            CurrentAnimation.addFrame(6, 128);
            CurrentTexture = textureListHero[2];
        }
        public void Move()
        {
            float maxSpeed = 10;
            speed = Limit(speed, maxSpeed);
            var keyboardState = Keyboard.GetState();
            var isRunning = keyboardState.IsKeyDown(Keys.LeftShift);
            var movementSpeed = isRunning ? speed.X * Run : speed.X;

            if (keyboardState.IsKeyDown(Keys.D))
            {
                flipped = false;
                SetAnimationAndTexture(isRunning ? 0 : 3, isRunning ? 8 : 6);
                position.X += movementSpeed;
            }
            else if (keyboardState.IsKeyDown(Keys.A))
            {
                flipped = true;
                SetAnimationAndTexture(isRunning ? 0 : 3, isRunning ? 8 : 6);
                position.X -= movementSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.S))
            {
                var verticalSpeed = isRunning ? speed.Y * Run : speed.Y;
                var direction = keyboardState.IsKeyDown(Keys.S) ? 1 : -1;
                SetAnimationAndTexture(isRunning ? 0 : 3, isRunning ? 8 : 6);
                position.Y += direction * verticalSpeed;
            }

            if (!keyboardState.GetPressedKeys().Any(key => key == Keys.W || key == Keys.S || key == Keys.A || key == Keys.D || key == Keys.Space))
            {
                SetAnimationAndTexture(2, 6);
            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                SetAnimationAndTexture(1, 12);
            }

            void SetAnimationAndTexture(int animationIndex, int frameCount)
            {
                CurrentAnimation = Animations[animationIndex];
                CurrentAnimation.addFrame(frameCount, 128);
                CurrentTexture = textureListHero[animationIndex];
            }
        }

        private Vector2 Limit(Vector2 v, float max)
        {
            if (v.Length() > max)
            {
                var ratio = max / v.Length();
                v.X = ratio;
                v.Y = ratio;
            }
            return v;
        }

        public void Update(GameTime gameTime)
        {
            Move();
            CurrentAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch _spritebatch)
        {
            if (flipped)
            {
                _spritebatch.Draw(CurrentTexture, position, CurrentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 1);
            }
            else
            {
                _spritebatch.Draw(CurrentTexture, position, CurrentAnimation.CurrentFrame.SourceRectangle, Color.White);
            }
        }
    }
}
