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
        string text;
        SpriteFont font;

        public MenuState(Game1 game,GraphicsDevice graphics, ContentManager content)
            : base(game, graphics, content)
        {

        }
        public override void LoadContent()
        {
            Texture2D buttonTexture = _content.Load<Texture2D>("Controls/button");
            font = _content.Load<SpriteFont>("Fonts/font");
            text = "Press Enter To Start";
        }
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _game.ChangeState(new GameState(_game, _graphics, _content));
            }
        }
        public override void PostUpdate(GameTime gameTime)
        {
            
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(_content.Load<SpriteFont>("Fonts/font"), "Press Enter To Start", new Vector2(400, 360), Color.White);
            spriteBatch.End();
        }
    }
}
