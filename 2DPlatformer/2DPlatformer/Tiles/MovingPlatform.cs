using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DPlatformer.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2DPlatformer.Tiles
{
    public class MovingPlatform
    {
        public Texture2D texture;
        public Vector2 position;
        public Rectangle rectangle;
        public float velocity = 2f;
        public float stop = 0f;
        public float start = 2f;

        public MovingPlatform(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
        }

        public void Update()
        {
            position.X += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width, (int)texture.Height);
            foreach (Platform platform in GameState.platforms)
            {
                if (rectangle.Intersects(platform.rectangle))
                {
                    velocity = -velocity;
                    start = -start;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
