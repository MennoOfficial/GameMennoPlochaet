using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameMennoPlochaet.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace GameMennoPlochaet.Manager
{
    internal class BackgroundManager
    {
        private readonly List<Layer> _layers;

        public BackgroundManager()
        {
            _layers = new();
        }

        public void AddLayer(Layer layer)
        {
            _layers.Add(layer);
        }

        public void Update(float movement)
        {
            foreach (var layer in _layers)
            {
                layer.Update(movement);
            }
        }

        public void Draw()
        {
            foreach (var layer in _layers)
            {
                layer.Draw();
            }
        }
    }
}
