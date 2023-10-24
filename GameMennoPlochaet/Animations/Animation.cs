using Microsoft.Xna.Framework;
using System;
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
        }
        public void addFrame(int frameCount, int height)
        {
            for (int row = 0; row < frameCount; row++)
            {
                frames.Add(new AnimationFrame(new Rectangle(height * row, 0, height, height)));
            }
        }

        public void Update(GameTime gameTime)
        {
            CurrentFrame = frames[counter];

            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            int fps =  15;

            if(secondCounter >= 1d / fps)
            {
                counter++;
                secondCounter = 0;
            }
            if (counter >= frames.Count)
                counter = 0;
        }   
    }
}
