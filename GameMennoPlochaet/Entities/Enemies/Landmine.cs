using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TestProject.Animations;
using System;
using GameMennoPlochaet.Managers;
using GameMennoPlochaet.Entities;

internal class Landmine : Entity
{
    public override Vector2 position { get; set; }
    private Animation CurrentAnimation;
    private Rectangle button;

    private const float TopOffset = -70f;
    private const float RightOffset = -70f;

    private int updateCounter = 0;
    private bool hasStartedAnimation = false;
    public bool hasTriggered = false;

    public Landmine(Texture2D texture, Rectangle button)
    {
        if (texture == null)
        {
            throw new ArgumentNullException(nameof(texture), "Texture cannot be null");
        }

        Texture = texture;
        this.button = button;
        Hitbox = button;

        // Initialize the animation (assume it has 12 frames of 96 height each)
        CurrentAnimation = new Animation();
        CurrentAnimation.addFrame(12, 96);
        position = new Vector2(button.X + button.Width + RightOffset, button.Y + TopOffset);
    }

    public void Trigger()
    {
        if (!hasTriggered)
        {
            hasTriggered = true;
            if (!hasStartedAnimation)
            {
                hasStartedAnimation = true;
                updateCounter = 0;
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (hasStartedAnimation)
        {
            // Continuously update the animation while it hasn't completed
            CurrentAnimation.Update(gameTime);

            updateCounter++;

            // Stop updating once the animation completes
            if (updateCounter >= 48)
            {
                hasStartedAnimation = false;
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (hasStartedAnimation)
        {
            // Draw the current frame of the animation
            spriteBatch.Draw(
                Texture,
                position,
                CurrentAnimation.CurrentFrame.SourceRectangle,
                Color.White,
                0f,
                Vector2.Zero, // No rotation or origin offset
                1f,
                SpriteEffects.None,
                0f
            );
        }
    }
}
