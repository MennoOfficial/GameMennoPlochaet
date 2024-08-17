using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using TestProject.Animations;
using System.Collections.Generic;
using System;
using GameMennoPlochaet.Managers;
using GameMennoPlochaet.UI;
using System.Diagnostics;

namespace GameMennoPlochaet.Entities.Hero
{
    internal class Hero : Entity
    {
        public Health Health { get; set; }
        public Gems gems { get; set; }
        public override Vector2 position { get; set; }
        public Vector2 speed = new Vector2(2, 2);
        public Vector2 acceleration = new Vector2(0.2f, 0.2f);
        private Vector2 velocity = Vector2.Zero;
        public List<Texture2D> textureListHero = new();
        private bool flipped = false;
        public Animation CurrentAnimation;
        public Animation[] Animations;
        public Texture2D CurrentTexture;
        private Color color = Color.White;
        private double hitCounter;
        private double flickerTimer;
        private const double FlickerInterval = 750;
        public Rectangle playerHitbox;
        public Rectangle nextHitbox;
        private int lives = 3;
        public const float Run = 2f;
        public const float Gravity = 17f;
        public const float Jump = 9f;
        public const float MaxVerticalSpeed = 10f;
        public bool isColliding;
        public bool isJumping;
        public bool isGrounded;
        public bool isHit;
        private Heart heart;
        private Gem gem;
        public KeyboardState keyboardState = Keyboard.GetState();
        private bool isInvincible = false;
        private double invincibilityTimer = 0;
        private const double InvincibilityDuration = 5000;

        public Hero()
        {
            Health = new Health(lives);
            gem = new Gem();
            heart = new Heart();
            gems = new Gems();

            position = new Vector2(0, 1000 - 30);
            nextHitbox = new Rectangle((int)position.X, (int)position.Y, 25, 70);
            textureListHero = ContentLoader.HeroTextures;
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

        public override void Update(GameTime gameTime)
        {
            if (Health.lives == 0)
            {
                GamestateManager.getInstance().UpdateScene(Core.Data.Scenes.Defeat);
            }
            else
            {
                Move(gameTime);
            }

            keyboardState = Keyboard.GetState();
            HandleJump();
            HandleGravity(gameTime);
            HandleCollisionWithMap();
            CheckCollisionWithEnemy(gameTime);
            CheckCollisionWithItem();

            heart.Update(this);
            gem.Update(this);

            playerHitbox = nextHitbox;
            position = new Vector2(playerHitbox.X - 50, playerHitbox.Y - 60);
            Hitbox = playerHitbox;

            CurrentAnimation.Update(gameTime);

            if (isInvincible)
            {
                invincibilityTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (invincibilityTimer >= InvincibilityDuration)
                {
                    isInvincible = false;
                    color = Color.White;
                }
                else
                {
                    flickerTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (flickerTimer >= FlickerInterval)
                    {
                        flickerTimer = 0;
                        color = (color == Color.White) ? Color.DarkBlue : Color.White;
                    }
                }
            }
            else if (isHit)
            {
                flickerTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (flickerTimer >= FlickerInterval)
                {
                    flickerTimer = 0;
                    color = (color == Color.White) ? Color.DarkGray : Color.White;
                }
            }
            else
            {
                color = Color.White;
            }
        }
        private void ActivateInvincibility()
        {
            isInvincible = true;
            invincibilityTimer = 0; // Reset the timer
            color = Color.DarkGray; // Start with flickering color to indicate invincibility
        }

        public override void Draw(SpriteBatch _spritebatch)
        {
            if (flipped)
            {
                _spritebatch.Draw(CurrentTexture, position, CurrentAnimation.CurrentFrame.SourceRectangle, color, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 1);
            }
            else
            {
                _spritebatch.Draw(CurrentTexture, position, CurrentAnimation.CurrentFrame.SourceRectangle, color);
            }
            heart.Draw(_spritebatch);
            gem.Draw(_spritebatch);
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
        public void CheckCollisionWithItem()
        {
            var gemsToRemove = new List<Entity>();

            foreach (var gem in MapManager.blueGems)
            {
                if (playerHitbox.Intersects(gem.Hitbox))
                {
                    gems.count++;
                    gemsToRemove.Add(gem);
                    ActivateInvincibility(); // Activate invincibility when picking up a gem
                }
            }
            foreach (var gem in gemsToRemove)
            {
                MapManager.blueGems.Remove(gem);
            }
        }

        public void CheckCollisionWithEnemy(GameTime gameTime)
        {
            if (!isInvincible)
            {
                foreach (var enemy in MapManager.enemies)
                {
                    if (Hitbox.Intersects(enemy.Hitbox))
                    {
                        if (enemy is Landmine landmine && landmine.hasTriggered)
                        {
                            continue;
                        }

                        if (enemy is Landmine mine && !mine.hasTriggered)
                        {
                            mine.Trigger();
                        }

                        if (!isHit)
                        {
                            isHit = true;
                            Health.lives--;
                            velocity.Y = -3f;
                            isJumping = true;
                            isGrounded = false;
                        }
                    }
                }
            }

            if (isHit)
            {
                hitCounter += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (hitCounter < 5000) // Immunity period
                {
                    color = (hitCounter % 200 < 100) ? Color.DarkGray : Color.White;
                }
                else
                {
                    color = Color.White;
                    isHit = false;
                    hitCounter = 0;
                }
            }
        }


        private void HandleCollisionWithMap()
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
        #endregion
    }
}
