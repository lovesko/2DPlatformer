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
        public Rectangle rectangle, turningRectangle, rectangleHead, rectangleRight, rectangleLeft;
        bool intersected, isGrounded;
        bool movingRight;
        public bool isDead = false;
        Animation animation;
        int framesX = 10;

        public Enemy(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            velocity.X = 2f;
            movingRight = true;
            animation = new Animation(texture, framesX, 0.1f);
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width / framesX, texture.Height);
            rectangleHead = new Rectangle((int)position.X + 10, (int)position.Y, (int)texture.Width / framesX - 10, 10);
            rectangleLeft = new Rectangle((int)position.X, (int)position.Y, 1, texture.Height / 2);
            rectangleRight = new Rectangle((int)position.X + texture.Width / framesX, (int)position.Y, 1, texture.Height / 2);
            animation.Update();

            if (movingRight)
            {
                turningRectangle = new Rectangle((int)position.X + texture.Width / framesX, (int)position.Y, (int)texture.Width / framesX, (int)texture.Height);
            }
            else if (!movingRight)
            {
                turningRectangle = new Rectangle((int)position.X - texture.Width / framesX, (int)position.Y, (int)texture.Width / framesX, (int)texture.Height);
            }

            intersected = false;
            foreach (Platform platform in GameState.platforms)
            {
                if (rectangleRight.Intersects(platform.rectangle) || rectangleLeft.Intersects(platform.rectangle))
                {
                    Turn();
                }
                else if (turningRectangle.Intersects(platform.rectangle))
                {
                    intersected = true;
                }
            }
            isGrounded = intersected;

            
            if (isGrounded == false)
            {
                Turn();
            }
        }

        public void Turn()
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
