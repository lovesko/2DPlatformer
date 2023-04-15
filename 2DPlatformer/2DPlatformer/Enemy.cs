using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DPlatformer;
using _2DPlatformer.Sprites;
using _2DPlatformer.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2DPlatformer
{
    public class Enemy
    {
        Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public Rectangle rectangle, turningRectangle, rectangleHead;
        bool intersected, isGrounded;
        bool movingRight;
        public bool isDead = false;
        Animation animation;

        public Enemy(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            velocity.X = 1f;
            movingRight = true;
            animation = new Animation(texture, 11, 0.1f);
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width / 11, 5);
            rectangleHead = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width + 10, 10);
            animation.Update();

            if (movingRight)
            {
                turningRectangle = new Rectangle((int)position.X + texture.Width / 11, (int)position.Y, (int)texture.Width / 11, (int)texture.Height);
            }
            else if (!movingRight)
            {
                turningRectangle = new Rectangle((int)position.X - texture.Width / 11, (int)position.Y, (int)texture.Width / 11, (int)texture.Height);
            }

            intersected = false;
            foreach (Platform platform in GameState.platforms)
            {
                if (turningRectangle.Intersects(platform.rectangle))
                {
                    intersected = true;
                }
                else if (rectangle.Intersects(platform.rectangle))
                {
                    isGrounded = false;
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
            if (movingRight)
            {
                animation.Draw(position, spriteBatch, SpriteEffects.None);
            }
            else
            {
                animation.Draw(position, spriteBatch, SpriteEffects.FlipHorizontally);
            }
             
        }
    }
}
