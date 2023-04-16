﻿using _2DPlatformer.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DPlatformer.States
{
    internal class EndState : State
    {
        SpriteFont font;
        SpriteFont font_larger;
        Button button;
        Texture2D button_texture;
        public EndState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            font = _content.Load<SpriteFont>("Fonts/font");
            font_larger = _content.Load<SpriteFont>("Fonts/font_larger");
            button_texture = _content.Load<Texture2D>("Controls/button");
            button = new Button(button_texture, new Vector2(Game1.screenWidth / 2 - button_texture.Width / 2, Game1.screenHeight - 100), font, "Menu");
            MediaPlayer.Stop();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font_larger, "You Win!", new Vector2(1280 / 2 - 100, 150), Color.White);
            button.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void LoadContent()
        {
            
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            Debug.WriteLine(GameState.player.level);
            button.Update();
            if (button.clicked)
            {
                _game.ChangeState(new MenuState(_game, _graphics, _content));
                
            }
        }
    }
}
