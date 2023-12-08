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

        //FOR TESTS
        public Texture2D blockTexture;
        //FOR TESTS


        public const float Run = 2f;
        public const float Gravity = 15f;
        public const float Jump = 7f;
        public const float MaxVerticalSpeed = 10f;

        public Hero(List<Texture2D> textureList, GraphicsDevice gD)
        {
            position = new Vector2(0, 0);
            playerHitbox = new Rectangle((int)position.X, (int)position.Y, 25, 70);
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
            playerHitbox.X = (int)position.X + 50;
            playerHitbox.Y = (int)position.Y + 60;
            Debug.WriteLine($"Position: {position}, Velocity: {velocity}, Touching Bottom: {IsTouchingBottom()}, Touching Left: {IsTouchingLeft()}, Touching Right: {IsTouchingRight()}");
            Move(gameTime);
            CurrentAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch _spritebatch)
        {
            var isColliding = false;
            foreach (var colliderBox in MapManager.mapHitbox)
            {
                if (this.playerHitbox.Intersects(colliderBox))
                {
                    isColliding = true;
                    break;
                }
            }
            if (flipped)
            {
                _spritebatch.Draw(CurrentTexture, position, CurrentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 1);
            }
            else
            {
                _spritebatch.Draw(CurrentTexture, position, CurrentAnimation.CurrentFrame.SourceRectangle, Color.White);
            }

            _spritebatch.Draw(blockTexture, playerHitbox, Color.Pink);
        }

        public void Move(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var isRunning = keyboardState.IsKeyDown(Keys.LeftShift);
            var movementSpeed = isRunning ? speed.X * Run : speed.X;

            if (keyboardState.IsKeyDown(Keys.D) && !IsTouchingRight())
            {
                flipped = false;
                SetAnimationAndTexture(isRunning ? 0 : 3, isRunning ? 8 : 6);
                position.X += movementSpeed;
            }
            else if (keyboardState.IsKeyDown(Keys.A) && !IsTouchingLeft())
            {
                flipped = true;
                SetAnimationAndTexture(isRunning ? 0 : 3, isRunning ? 8 : 6);
                position.X -= movementSpeed;

            }
            else
            {
                SetAnimationAndTexture(2, 6);
            }

            if (!IsTouchingBottom())
            {
                ApplyGravity(gameTime);
            }
            else
            {
                velocity.Y = 0;
            }
            if (keyboardState.IsKeyDown(Keys.Space) && IsTouchingBottom())
            {
                velocity.Y = -Jump;
                SetAnimationAndTexture(1, 12);
            }
            void SetAnimationAndTexture(int animationIndex, int frameCount)
            {
                CurrentAnimation = Animations[animationIndex];
                CurrentAnimation.addFrame(frameCount, 128);
                CurrentTexture = textureListHero[animationIndex];
            }
        }

        private void ApplyGravity(GameTime gameTime)
        {
            if (!IsTouchingBottom())
            {
                // Apply gravity by adding a constant amount to the vertical velocity
                velocity.Y += Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Update the character's position based ofvd n the velocity
                position.Y += velocity.Y;

                // Update the playerHitbox position
                playerHitbox.Y = (int)position.Y + 60; // Adjust the value based on your character's position and dimensions
            }
        }

        #region Collisions
        public bool IsTouchingRight()
        {
            foreach (var colliderBox in MapManager.mapHitbox)
            {
                if (this.playerHitbox.Right + this.velocity.X > colliderBox.Left &&
                    this.playerHitbox.Left < colliderBox.Left &&
                    this.playerHitbox.Bottom > colliderBox.Top &&
                    this.playerHitbox.Top < colliderBox.Bottom)
                {
                    // If the character is already overlapping with the collider,
                    // only reset their velocity if they are moving into the collider.
                    /*
                    if (this.velocity.X > 0)
                    {
                        this.velocity.X = 0;
                    }
                    else
                    {
                        this.playerHitbox.X = colliderBox.Left - this.playerHitbox.Width;
                        this.velocity.X = -this.velocity.X;
                    }
                    */
                    return true;
                }
            }
            return false;
        }

        public bool IsTouchingLeft()
        {
            foreach (var colliderBox in MapManager.mapHitbox)
            {
                if (this.playerHitbox.Left + this.velocity.X < colliderBox.Right &&
                    this.playerHitbox.Right > colliderBox.Right &&
                    this.playerHitbox.Bottom > colliderBox.Top &&
                    this.playerHitbox.Top < colliderBox.Bottom)
                {
                    // If the character is already overlapping with the collider,
                    // only reset their velocity if they are moving into the collider.
                    /*
                    if (this.velocity.X < 0)
                    {
                        this.velocity.X = 0;
                    }
                    else
                    {
                        this.playerHitbox.X = colliderBox.Right;
                        this.velocity.X = -this.velocity.X;
                    }
                    */
                    return true;
                }
            }
            return false;
        }

        public bool IsTouchingBottom()
        {
            foreach (var colliderBox in MapManager.mapHitbox)
            {
                if (this.playerHitbox.Bottom + this.velocity.Y > colliderBox.Top &&
                    this.playerHitbox.Top < colliderBox.Top &&
                    this.playerHitbox.Right > colliderBox.Left &&
                    this.playerHitbox.Left < colliderBox.Right)
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsTouchingTop()
        {
            foreach (var colliderBox in MapManager.mapHitbox)
            {
                if (this.playerHitbox.Top + this.velocity.Y < colliderBox.Bottom &&
                    this.playerHitbox.Bottom > colliderBox.Bottom &&
                    this.playerHitbox.Right > colliderBox.Left &&
                    this.playerHitbox.Left < colliderBox.Right)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
        
    }
}
