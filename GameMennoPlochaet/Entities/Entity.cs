using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMennoPlochaet.Entities
{
    public abstract class Entity
    {
        public  Texture2D Texture { get; set; }
        public abstract Vector2 position { get; set; }
        public Rectangle Hitbox { get; set; }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
