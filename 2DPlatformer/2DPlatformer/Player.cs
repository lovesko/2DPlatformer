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

    internal class Player
    {
        List<Platform> platforms;
        Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public bool hasJumped, isGrounded, blockedLeft, blockedRight;
        public Rectangle rectangle, rectangleFeet, rectangleHead, rectangleLeft, rectangleRight;

        // Define a KeyboardState object to store the current state of the keyboard
        KeyboardState currentKeyboardState;
        // Define a KeyboardState object to store the previous state of the keyboard
        KeyboardState previousKeyboardState;

        SpriteEffects s = SpriteEffects.FlipHorizontally;

        public Player(Texture2D newTexture, Vector2 newPosition, List<Platform> newPlatforms)
        {
            texture = newTexture;
            position = newPosition;
            hasJumped = true;
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width, (int)texture.Height);
            platforms = newPlatforms;
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;

            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width, (int)texture.Height);
            rectangleFeet = new Rectangle((int)position.X, (int)position.Y + (int)texture.Height, (int)texture.Width, 1);
            rectangleHead = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width, 1);
            rectangleLeft = new Rectangle((int)position.X - 13, (int)position.Y, 1, (int)texture.Height);
            rectangleRight = new Rectangle((int)position.X + (int)texture.Width + 13, (int)position.Y, 1, (int)texture.Height);

            // Store the current state of the keyboard
            

            if (Keyboard.GetState().IsKeyDown(Keys.Right) && blockedRight == false)
            {
                velocity.X = 7f;
                s = SpriteEffects.None;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && blockedLeft == false)
            {
                velocity.X = -7f;
                s = SpriteEffects.FlipHorizontally;
            }

            else
            {
                velocity.X = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                hasJumped = true;
                position.Y -= 40f;
                velocity.Y = -9f;
                isGrounded = false;
           }

            //Varierande hopp beroende på hur länge man trycker Space
            currentKeyboardState = Keyboard.GetState();
            if (previousKeyboardState.IsKeyDown(Keys.Space) && currentKeyboardState.IsKeyUp(Keys.Space) && velocity.Y < 0)
            {
                velocity.Y = -3f;
                hasJumped = true;
            }
            previousKeyboardState = currentKeyboardState;

            if (hasJumped)
            {
                velocity.Y += 0.30f;
                
            }

            if (isGrounded == false)
            {
                hasJumped = true;
            }
            if (isGrounded)
            {
                velocity.Y = 0f;
            }

            if (position.Y + texture.Height >= 720)
            {
                hasJumped = false;
                isGrounded = true;
            }
            if (!hasJumped)
            {
                velocity.Y = 0f;
            }




            //Kollisioner
            bool intersected = false;
            bool intersectedLeft = false;
            bool intersectedRight = false;
            foreach (Platform platform in platforms)
            {
                if (rectangleFeet.Intersects(platform.rectangle))
                {
                    intersected = true;
                    hasJumped = false;
                    isGrounded = true;
                    position.Y = platform.rectangle.Top - texture.Height;
                }

                if (rectangleHead.Intersects(platform.rectangle))
                {
                    velocity.Y = 0f;
                    position.Y += 2;
                }

                if (rectangleRight.Intersects(platform.rectangle))
                {
                    intersectedRight = true;
                }
                if (rectangleLeft.Intersects(platform.rectangle))
                {
                    intersectedLeft = true;
                }
            }
            isGrounded = intersected;
            blockedLeft = intersectedLeft;
            blockedRight = intersectedRight;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 1, s, 0);
        }

    }
}
