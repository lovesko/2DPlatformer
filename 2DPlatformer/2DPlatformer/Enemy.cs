using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DPlatformer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2DPlatformer
{
    public class Enemy
    {
        List<Platform> platforms;
        Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public Rectangle rectangle, turningRectangle, rectangleHead;
        bool intersected, isGrounded;
        bool movingRight;
        public bool isDead = false;

        public Enemy(Texture2D newTexture, Vector2 newPosition, List<Platform> newPlatforms)
        {
            texture = newTexture;
            position = newPosition;
            platforms = newPlatforms;
            velocity.X = 1f;
            movingRight = true;
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y + 10, (int)texture.Width, (int)texture.Height + 2);
            rectangleHead = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width + 10, 10);

            if (movingRight)
            {
                turningRectangle = new Rectangle((int)position.X + texture.Width, (int)position.Y, (int)texture.Width, (int)texture.Height + 2);
            }
            else if (!movingRight)
            {
                
                turningRectangle = new Rectangle((int)position.X - texture.Width, (int)position.Y, (int)texture.Width, (int)texture.Height + 2);
            }

            intersected = false;
            foreach (Platform platform in platforms)
            {
                if (turningRectangle.Intersects(platform.rectangle))
                {
                    intersected = true;
                }
            }
            isGrounded = intersected;

            
            if (isGrounded == false)
            {
                velocity.X = -velocity.X;
                if (movingRight)
                {
                    movingRight = false;
                }

                else if (!movingRight)
                {
                    movingRight = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
