    using Microsoft.Xna.Framework;
    using System.Collections.Generic;

    namespace TestProject.Animations
    {
    internal class Animation
    {
        public AnimationFrame CurrentFrame { get; set; }
        private List<AnimationFrame> frames;
        private int counter;
        private double secondCounter;

        public Animation()
        {
            frames = new List<AnimationFrame>();
            counter = 0; // Ensure counter starts at 0
        }

        public void addFrame(int frameCount, int frameHeight)
        {
            for (int row = 0; row < frameCount; row++)
            {
                frames.Add(new AnimationFrame(new Rectangle(frameHeight * row, 0, frameHeight, frameHeight)));
            }
            if (frames.Count > 0)
            {
                CurrentFrame = frames[0];
            }
        }

        public void Update(GameTime gameTime)
        {
            if (frames.Count == 0) return;

            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            int fps = 15;

            if (secondCounter >= 1d / fps)
            {
                counter++;
                secondCounter = 0;
            }

            if (counter >= frames.Count)
            {
                counter = 0;
            }

            CurrentFrame = frames[counter];
        }
    }
}