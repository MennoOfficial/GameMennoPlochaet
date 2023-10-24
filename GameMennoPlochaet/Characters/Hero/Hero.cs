using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using TestProject.Animations;
using System.Collections.Generic;

namespace GameMennoPlochaet.Characters.Hero
{
    internal class Hero
    {
        public Animation animation;
        public Vector2 position = Vector2.Zero;
        public Vector2 speed = new Vector2(2, 2);
        public float Run = 2f;
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

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    flipped = false;
                    CurrentAnimation = Animations[0];
                    CurrentAnimation.addFrame(6, 128);
                    CurrentTexture = textureListHero[0];
                    position.X += speed.X * Run;
                }
                else
                {
                    flipped = false;
                    CurrentAnimation = Animations[3];
                    CurrentAnimation.addFrame(8, 128);
                    CurrentTexture = textureListHero[3];
                    position.X += speed.X;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    flipped = true;
                    CurrentAnimation = Animations[0];
                    CurrentAnimation.addFrame(8, 128);
                    CurrentTexture = textureListHero[0];
                    position.X -= speed.X * Run;
                }
                else
                {
                    flipped = true;
                    CurrentAnimation = Animations[3];
                    CurrentAnimation.addFrame(8, 128);
                    CurrentTexture = textureListHero[3];
                    position.X -= speed.X;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    CurrentAnimation = Animations[0];
                    CurrentAnimation.addFrame(8, 128);
                    CurrentTexture = textureListHero[0];
                    position.Y -= speed.Y * Run;
                }
                else
                {
                    CurrentAnimation = Animations[3];
                    CurrentAnimation.addFrame(8, 128);
                    CurrentTexture = textureListHero[3];
                    position.Y -= speed.Y;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    CurrentAnimation = Animations[0];
                    CurrentAnimation.addFrame(8, 128);
                    CurrentTexture = textureListHero[0];
                    position.Y += speed.Y * Run;
                }
                else
                {
                    CurrentAnimation = Animations[3];
                    CurrentAnimation.addFrame(8, 128);
                    CurrentTexture = textureListHero[3];
                    position.Y += speed.Y;
                }
            }

            if (!Keyboard.GetState().IsKeyDown(Keys.D) && !Keyboard.GetState().IsKeyDown(Keys.A) && !Keyboard.GetState().IsKeyDown(Keys.W) && !Keyboard.GetState().IsKeyDown(Keys.S))
            {
                CurrentAnimation = Animations[2];
                CurrentAnimation.addFrame(6, 128);
                CurrentTexture = textureListHero[2];
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
