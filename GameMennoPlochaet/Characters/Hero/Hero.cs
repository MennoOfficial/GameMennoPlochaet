using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using TestProject.Animations;
using System.Collections.Generic;
using GameMennoPlochaet.Manager;
using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace GameMennoPlochaet.Characters.Hero
{
    internal class Hero
    {
        public Animation animation;
        public Vector2 position;
        public Vector2 speed = new Vector2(2, 2);
        public Vector2 acceleration = new Vector2(0.2f, 0.2f);
        private Vector2 velocity = Vector2.Zero;
        public List<Texture2D> textureListHero = new();
        private bool flipped = false;
        public Animation CurrentAnimation;
        public Animation[] Animations;
        public Texture2D CurrentTexture;
        public Rectangle playerHitbox;
        public Rectangle nextHitbox;
        //FOR TESTS
        public Texture2D blockTexture;
        //FOR TESTS
        public const float Run = 2f;
        public const float Gravity = 15f;
        public const float Jump = 7f;
        public const float MaxVerticalSpeed = 10f;
        //Colision
        public bool isColliding;
        public bool isJumping;
        public bool isGrounded;
        public KeyboardState keyboardState = Keyboard.GetState();
        public Hero(List<Texture2D> textureList, GraphicsDevice gD)
        {
            position = new Vector2(0, 0);
            nextHitbox = new Rectangle((int)position.X, (int)position.Y, 25, 70);
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

            //ColiderTests
            blockTexture = new Texture2D(gD, 1, 1);
            blockTexture.SetData(new[] { Color.White });
        }
        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            Move(gameTime);
            HandleJump();
            HandleGravity(gameTime);
            HandleCollision3();
            playerHitbox = nextHitbox;
            position = new Vector2(playerHitbox.X - 50, playerHitbox.Y - 60);
            Debug.WriteLine($"Position: {position}, Velocity: {velocity}, Collision: {isColliding}");
            
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

            _spritebatch.Draw(blockTexture, nextHitbox, Color.Pink);
        }
        public void Move(GameTime gameTime)
        {
            var isRunning = keyboardState.IsKeyDown(Keys.LeftShift);
            var movementSpeed = isRunning ? speed.X * Run : speed.X;

            if (keyboardState.IsKeyDown(Keys.D))
            {
                flipped = false;
                SetAnimationAndTexture(isRunning ? 0 : 3, isRunning ? 8 : 6);
                nextHitbox.X += (int)movementSpeed;
            }
            else if (keyboardState.IsKeyDown(Keys.A))
            {
                flipped = true;
                SetAnimationAndTexture(isRunning ? 0 : 3, isRunning ? 8 : 6);
                nextHitbox.X -= (int)movementSpeed;
            }
            else
            {
                SetAnimationAndTexture(2, 6);
            }          
        }
        public void HandleJump()
        {
            if (keyboardState.IsKeyDown(Keys.Space) && !isJumping && isGrounded)
            {
                velocity.Y = -Jump;
                isJumping = true;
                isGrounded = false;
                SetAnimationAndTexture(1, 12);
            }
        }
        public void SetAnimationAndTexture(int animationIndex, int frameCount)
        {
            CurrentAnimation = Animations[animationIndex];
            CurrentAnimation.addFrame(frameCount, 128);
            CurrentTexture = textureListHero[animationIndex];
        }
        private void HandleGravity(GameTime gameTime)
        {           
                nextHitbox.Y += (int)velocity.Y;
                velocity.Y += Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        #region Collisions
        private void HandleCollision3()
        {
            Vector2 correctionVector = new Vector2(100000, 100000);

            isColliding = false;
            foreach (var box in MapManager.mapHitbox)
            {
                if (box.Intersects(nextHitbox))
                {
                   isJumping = false;
                    velocity.Y = 0;
                    if (box.Top - nextHitbox.Bottom < 0) // ground collision
                    {
                        int correction = nextHitbox.Bottom - box.Top;
                        if (Math.Abs(correction) < Math.Abs(correctionVector.Y))
                        {
                            correctionVector.Y = correction;

                        }
                    }
                    if (box.Bottom - nextHitbox.Top > 0) // top collision
                    {
                        int correction = nextHitbox.Top - box.Bottom;
                        if (Math.Abs(correction) < Math.Abs(correctionVector.Y))
                        {
                            correctionVector.Y = correction;
                        }
                    }
                    if (box.Left - nextHitbox.Left > 0) // Right collision
                    {
                        int correction = box.Left - nextHitbox.Right;
                        if (Math.Abs(correction) < Math.Abs(correctionVector.X))
                        {
                            correctionVector.X = correction;
                        }
                    }
                    if (box.Right - nextHitbox.Right < 0) // Left collision
                    {
                        int correction = box.Right - nextHitbox.Left;
                        if (Math.Abs(correction) < Math.Abs(correctionVector.X))
                        {
                            correctionVector.X = correction;
                        }
                    }
                }
            }
            if (correctionVector.Y < 20 || correctionVector.X < 20)
            {
                if (Math.Abs(correctionVector.Y) > Math.Abs(correctionVector.X))
                {
                    nextHitbox.X += (int)correctionVector.X;
                    isColliding = true;
                }
                else
                {
                    if (correctionVector.Y > 0)
                    {
                        isGrounded = true;
                    }
                    nextHitbox.Y -= (int)correctionVector.Y;
                }
            }
        }
    }
    #endregion
}
