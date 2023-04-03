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
        public Rectangle rectangle, rectangleFeet, rectangleHead, rectangleLeft, rectangleRight, leftScreen, rightScreen;


        SpriteEffects s = SpriteEffects.FlipHorizontally;

        public Player(Texture2D newTexture, Vector2 newPosition, List<Platform> newPlatforms)
        {
            texture = newTexture;
            position = newPosition;
            hasJumped = true;
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width, (int)texture.Height);
            platforms = newPlatforms;
            leftScreen = new Rectangle(0, 0, 1, 1280);
            leftScreen = new Rectangle(500, 0, 1, 1280);
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;

            rectangleFeet = new Rectangle((int)position.X, (int)position.Y + (int)texture.Height, (int)texture.Width, 1);

            rectangleHead = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width, 1);

            rectangleLeft = new Rectangle((int)position.X - 13, (int)position.Y, 1, (int)texture.Height);

            rectangleRight = new Rectangle((int)position.X + (int)texture.Width + 12, (int)position.Y, 1, (int)texture.Height);

            if (Keyboard.GetState().IsKeyDown(Keys.Right) && blockedRight == false)
            {
                velocity.X = 7f;
                rectangle.X = (int)position.X;
                s = SpriteEffects.None;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && blockedLeft == false)
            {
                velocity.X = -7f;
                rectangle.Y = (int)position.X;
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
                rectangle.Y = (int)position.Y;
                velocity.Y = -9f;
                isGrounded = false;
            }
            if (hasJumped)
            {
                float i = 1;
                velocity.Y += 0.30f * i;
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

                if (rectangleRight.Intersects(platform.rectangle) || rectangleRight.Intersects(rightScreen))
                {
                    intersectedRight = true;
                }
                if (rectangleLeft.Intersects(platform.rectangle) || rectangleRight.Intersects(leftScreen))
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
