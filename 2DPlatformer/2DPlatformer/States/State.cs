using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DPlatformer.States
{
    //https://www.youtube.com/watch?v=76Mz7ClJLoE
    public abstract class State
    {
        protected ContentManager content;

        protected GraphicsDevice graphics;

        protected Game1 game;

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        public abstract void PostUpdate(GameTime gameTime);

        public abstract void Update(GameTime gameTime);

        public State(Game1 newGame, GraphicsDevice newGraphicsDevice, ContentManager newContent)
        {
            content = newContent;
            graphics = newGraphicsDevice;
            game = newGame;
        }

    }
}
