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
using _2DPlatformer.States;
using _2DPlatformer.Sprites;
using Microsoft.Xna.Framework.Audio;
using _2DPlatformer.Tiles;

namespace _2DPlatformer
{

    public class Player
    {
        Texture2D texture, walking_texture, jumping_texture;
        SoundEffect jump_sound, enemy_death_sound, coin_sound, death_sound;

        public Vector2 position;
        public Vector2 velocity;
        public bool hasJumped, isGrounded, blockedLeft, blockedRight;
        public Rectangle rectangle, rectangleFeet, rectangleHead, rectangleLeft, rectangleRight;
        public int score = 0;
        public int level = 1;
        float speed = 4f;
        public bool win = false;
        bool justJumped = false;
        Animation walk_animation;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        SpriteEffects s = SpriteEffects.FlipHorizontally;

        public Player(Texture2D newTexture, Texture2D newWalkingTexture, Texture2D newJumpingTexture, Vector2 newPosition)
        {
            texture = newTexture;
            walking_texture = newWalkingTexture;
            jumping_texture = newJumpingTexture;
            position = newPosition;
            hasJumped = true;
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width, (int)texture.Height);
            walk_animation = new Animation(walking_texture, 10, 0.1f);

            jump_sound = GameState.jump_sound;
            enemy_death_sound = GameState.enemy_death_sound;
            coin_sound = GameState.coin_sound;
            death_sound = GameState.player_death_sound;
        }

        public void Die()
        {
            position.X = 46;
            position.Y = 489;
            score = 0;
            GameState.score_str = score.ToString();
            death_sound.Play(0.1f, 0, 0);

            GameState.enemies.Clear();
            GameState.coins.Clear();
            GameState.platforms.Clear();
            GameState.movingPlatforms.Clear();
            Map.Generate();
        } 
        public void Update(GameTime gameTime)
        {
            walk_animation.Update();
            position += velocity;
            //Debug.WriteLine("Position: " + position.X + "," + position.Y + "|| Level: " + level);

            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width, (int)texture.Height);
            rectangleFeet = new Rectangle((int)position.X, (int)position.Y + (int)texture.Height, (int)texture.Width, 1);
            rectangleHead = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width, 1);
            rectangleLeft = new Rectangle((int)position.X - 7, (int)position.Y, 1, (int)texture.Height - 1);
            rectangleRight = new Rectangle((int)position.X + (int)texture.Width + 6, (int)position.Y, 1, (int)texture.Height - 3);

            #region input
            
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && blockedRight == false || 
                Keyboard.GetState().IsKeyDown(Keys.D) && blockedRight == false)
            {
                velocity.X = speed;
                s = SpriteEffects.None;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && blockedLeft == false ||
                     Keyboard.GetState().IsKeyDown(Keys.A) && blockedLeft == false)
            {
                velocity.X = -speed;
                s = SpriteEffects.FlipHorizontally;
            }
            else
            {
                velocity.X = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false && justJumped == false)
            {
                hasJumped = true;
                velocity.Y = -10f;
                isGrounded = false;
                justJumped = true;
            }
            else if ((Keyboard.GetState().IsKeyUp(Keys.Space)))
            {
                justJumped = false;
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
                velocity.Y += 0.5f;
                isGrounded = false;
            }

            if (isGrounded == false)
            {
                hasJumped = true;
            }
            if (isGrounded && velocity.Y >= 0)
            {
                velocity.Y = 0f;
                
            }
            #endregion

            #region kollisioner med plattformar

            bool intersectedFeet = false;
            bool intersectedLeft = false;
            bool intersectedRight = false;

            for (int i = 0; i < GameState.platforms.Count; i++)
            {
                if (rectangle.Intersects(GameState.platforms[i].rectangle) && GameState.platforms[i].texture == GameState.sign_texture) // om spelaren rör en skylt ska han vinna
                {
                    win = true;
                    
                }
                if (rectangleFeet.Intersects(GameState.platforms[i].rectangle))
                {
                    if (GameState.platforms[i].isDeadly)
                    {
                        Die();
                    }
                    else if (GameState.platforms[i].isBouncy)
                    {
                        hasJumped = true;
                        velocity.Y = -18f;
                        isGrounded = false;
                        jump_sound.Play(0.1f, 0, 0);
                    }
                    else //normal plattform
                    {
                        intersectedFeet = true;
                        hasJumped = false;
                        isGrounded = true;
                        position.Y = GameState.platforms[i].rectangle.Top - texture.Height;
                    }
                }

                if (rectangleHead.Intersects(GameState.platforms[i].rectangle))
                {
                    velocity.Y = 0f;
                    position.Y += 2;
                    if (GameState.platforms[i].isDeadly)
                    {
                        Die();
                    }
                    if (GameState.platforms[i].isBreakable)
                    {
                        GameState.platforms.RemoveAt(i);
                    }
                }

                if (rectangleRight.Intersects(GameState.platforms[i].rectangle) && GameState.platforms[i].texture != GameState.sign_texture)
                {
                    intersectedRight = true;
                    if (GameState.platforms[i].isDeadly)
                    {
                        Die();
                    }
                }
                if (rectangleLeft.Intersects(GameState.platforms[i].rectangle) && GameState.platforms[i].texture != GameState.sign_texture)
                {
                    intersectedLeft = true;
                    if (GameState.platforms[i].isDeadly)
                    {
                        Die();
                    }
                }
                if (position.X <= texture.Width / 2 -10)
                {
                    intersectedLeft = true;
                }
            }

            foreach (MovingPlatform movingPlatform in GameState.movingPlatforms)
            {
                if (rectangleFeet.Intersects(movingPlatform.rectangle))
                {
                    intersectedFeet = true;
                    hasJumped = false;
                    isGrounded = true;
                    position.Y = movingPlatform.rectangle.Top - texture.Height;
                    velocity.X += movingPlatform.velocity;
                }
                if (rectangleHead.Intersects(movingPlatform.rectangle))
                {
                    velocity.Y = 0f;
                    position.Y += 2;
                }
                if (rectangleRight.Intersects(movingPlatform.rectangle))
                {
                    intersectedRight = true;
                    if (movingPlatform.velocity < 0)
                    {
                        movingPlatform.velocity = movingPlatform.stop;
                    }
                }
                else if (rectangleLeft.Intersects(movingPlatform.rectangle))
                {
                    intersectedLeft = true;
                    if (movingPlatform.velocity > 0)
                    {
                        movingPlatform.velocity = movingPlatform.stop;
                    }
                }
                else 
                {
                    movingPlatform.velocity = movingPlatform.start;
                }
            }

            isGrounded = intersectedFeet;
            blockedLeft = intersectedLeft;
            blockedRight = intersectedRight;
            #endregion

            #region kollisioner med fiender

            for (int i = 0; i < GameState.enemies.Count; i++)
            {
                if (rectangleFeet.Intersects(GameState.enemies[i].rectangleHead)) //goombastomp
                {
                    //studsar på fienden
                    hasJumped = true;
                    velocity.Y = -7f;
                    isGrounded = false;
                    enemy_death_sound.Play(0.3f, 0, 0);
                    score += 100;
                    GameState.score_str = score.ToString();

                    GameState.enemies.RemoveAt(i);
                }
                else if (rectangle.Intersects(GameState.enemies[i].rectangle))
                {
                    Die();
                }
            }

            #endregion

            #region kollisioner med coins

            for (int i = 0; i < GameState.coins.Count; i++)
            {
                if (rectangle.Intersects(GameState.coins[i].rectangle))
                {
                    GameState.coins.RemoveAt(i);
                    score += 50;
                    coin_sound.Play(0.1f, 0, 0);
                    GameState.score_str = score.ToString();
                }
            }

            #endregion

        }
        public void Draw(SpriteBatch spriteBatch)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Right) && blockedRight == false && !hasJumped ||
                Keyboard.GetState().IsKeyDown(Keys.D) && blockedRight == false && !hasJumped)
            {
                walk_animation.Draw(position, spriteBatch, SpriteEffects.None);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && blockedLeft == false && !hasJumped ||
                     Keyboard.GetState().IsKeyDown(Keys.A) && blockedLeft == false  && !hasJumped)
            {
                walk_animation.Draw(position, spriteBatch, SpriteEffects.FlipHorizontally);
            }
            else if (hasJumped)
            {
                spriteBatch.Draw(jumping_texture, position, null, Color.White, 0, Vector2.Zero, 1, s, 0);
            }
            else
            {
                spriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 1, s, 0);
            }
            
        }

    }
}
