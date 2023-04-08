﻿using System;
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
        List<Enemy> enemies;
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

        public Player(Texture2D newTexture, Vector2 newPosition, List<Platform> newPlatforms, List<Enemy> newEnemies)
        {
            texture = newTexture;
            position = newPosition;
            hasJumped = true;
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width, (int)texture.Height);
            platforms = newPlatforms;
            enemies = newEnemies;
        }

        public void Die()
        {
            position.Y = 50;
            position.X = 50;
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;
            //Debug.WriteLine("Position: " + position.X + "," + position.Y);

            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width, (int)texture.Height);
            rectangleFeet = new Rectangle((int)position.X, (int)position.Y + (int)texture.Height, (int)texture.Width, 1);
            rectangleHead = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width, 1);
            rectangleLeft = new Rectangle((int)position.X - 10, (int)position.Y, 1, (int)texture.Height - 1);
            rectangleRight = new Rectangle((int)position.X + (int)texture.Width + 8, (int)position.Y, 1, (int)texture.Height - 3);

            #region input
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && blockedRight == false || 
                Keyboard.GetState().IsKeyDown(Keys.D) && blockedRight == false)
            {
                velocity.X = 5f;
                s = SpriteEffects.None;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && blockedLeft == false ||
                     Keyboard.GetState().IsKeyDown(Keys.A) && blockedLeft == false)
            {
                velocity.X = -5f;
                s = SpriteEffects.FlipHorizontally;
            }
            else
            {
                velocity.X = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                hasJumped = true;
                velocity.Y = -12f;
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
            #endregion

            #region gravity
            if (hasJumped)
            {
                velocity.Y += 0.50f;
            }

            if (isGrounded == false)
            {
                hasJumped = true;
            }
            if (isGrounded)
            {
                velocity.Y = 0f;
            }
            if (!hasJumped)
            {
                velocity.Y = 0f;
            }
            #endregion



            #region kollisioner med plattformar

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
                    if (platform.isDeadly)
                    {
                        Die();
                    }
                }

                if (rectangleHead.Intersects(platform.rectangle))
                {
                    velocity.Y = 0f;
                    position.Y += 2;
                    if (platform.isDeadly)
                    {
                        Die();
                    }
                }

                if (rectangleRight.Intersects(platform.rectangle))
                {
                    intersectedRight = true;
                    if (platform.isDeadly)
                    {
                        Die();
                    }
                }
                if (rectangleLeft.Intersects(platform.rectangle))
                {
                    intersectedLeft = true;
                    if (platform.isDeadly)
                    {
                        Die();
                    }
                }

                if (position.X <= texture.Width / 2 -5)
                {
                    intersectedLeft = true;
                }
                if (position.Y >= 720)
                {
                    Die();
                }
            }

            isGrounded = intersected;
            blockedLeft = intersectedLeft;
            blockedRight = intersectedRight;
            #endregion


            #region kollisioner med fiender

            foreach (Enemy enemy in enemies)
            {
                if (rectangle.Intersects(enemy.rectangle))
                {
                    Die();
                }
                if (rectangleFeet.Intersects(enemy.rectangleHead))
                {
                    Debug.WriteLine("död");
                }
            }

            #endregion

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 1, s, 0);
        }

    }
}
