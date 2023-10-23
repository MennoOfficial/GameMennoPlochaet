using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using TestProject.Animations;
using System.Collections.Generic;

namespace GameMennoPlochaet.Characters.Hero
{
    internal class Hero
    {
        Texture2D heroTexture;
        Animation animation;
        public Vector2 position = Vector2.Zero;
        public Vector2 speed = new Vector2(2, 2);
        public Vector2 acceleration = new Vector2(0.2f, 0.2f); 
        public List<Texture2D> textureListHero = new();
        private bool flipped = false;
        //test

        public Hero(List<Texture2D> textureList)
        {
            textureListHero = textureList;
            animation = new Animation();
           GetFramesTextureProperties(8, 128);

        }
        public void Move()
        {
            float maxSpeed = 10;
            speed = Limit(speed, maxSpeed);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.X += speed.X;
                flipped = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.X -= speed.X;
                flipped = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.Y -= speed.Y;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y += speed.Y;
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

        public void GetFramesTextureProperties(int frames, int height)
        {
            for (int row = 0; row < frames; row++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(height * row, 0, height, height)));
            }
        }
    
        public void Update(GameTime gameTime)
        {
            Move();
            animation.Update(gameTime);
        }

        public void Draw(SpriteBatch _spritebatch)
        {
            if (flipped)
            {
                _spritebatch.Draw(heroTexture, position, animation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 1);
            }
            else
            {
                _spritebatch.Draw(heroTexture, position, animation.CurrentFrame.SourceRectangle, Color.White);
            }
        }
    }
}
