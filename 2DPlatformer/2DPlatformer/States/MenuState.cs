using _2DPlatformer.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DPlatformer.States
{
    public class MenuState : State
    {
        SpriteFont font;
        SpriteFont font_larger;
        Texture2D button_texture;
        

        public static Button button_play;
        public static Button button_exit;

        public static List<Button> buttons;

        public MenuState(Game1 game,GraphicsDevice graphics, ContentManager content)
            : base(game, graphics, content)
        {
            font = _content.Load<SpriteFont>("Fonts/font");
            font_larger = _content.Load<SpriteFont>("Fonts/font_larger");

            button_texture = _content.Load<Texture2D>("Controls/button");

            button_play = new Button(button_texture, new Vector2(Game1.screenWidth / 2 - button_texture.Width / 2, Game1.screenHeight / 2 - button_texture.Height / 2), font, "Play");
            button_exit = new Button (button_texture, new Vector2(Game1.screenWidth / 2 - button_texture.Width / 2, Game1.screenHeight / 2 - button_texture.Height / 2 + 150), font, "Exit");
           

        }
        public override void LoadContent()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
            button_play.Update(); 
            if (button_play.clicked) //om klickar play --> spelet startar
            {
                _game.ChangeState(new GameState(_game, _graphics, _content));
                _game.IsMouseVisible = false;
            }

            button_exit.Update();
            if (button_exit.clicked)
            {
                _game.Quit();  //om klickar exit --> applikation avslutas
            }
        }
        public override void PostUpdate(GameTime gameTime)
        {
           
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            
            button_play.Draw(spriteBatch);
            button_exit.Draw(spriteBatch);
            spriteBatch.DrawString(font_larger, "2DPlatformer", new Vector2(1280 / 2 - 250, 150), Color.White);


            spriteBatch.End();
        }
    }
}
