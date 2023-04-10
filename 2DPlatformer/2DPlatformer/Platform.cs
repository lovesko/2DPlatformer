using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2DPlatformer
{
    public class Platform
    {
        public Texture2D texture;
        public Vector2 position;
        public Rectangle rectangle;
        public bool isDeadly, isBreakable;

        public Platform(Texture2D newTexture, Vector2 newPosition, bool deadly, bool breakable)
        {
            texture = newTexture;
            position = newPosition;
            rectangle = new Rectangle((int)position.X, (int)position.Y + 1, (int)texture.Width, (int)texture.Height);
            isDeadly = deadly;
            isBreakable = breakable;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

    }
}
