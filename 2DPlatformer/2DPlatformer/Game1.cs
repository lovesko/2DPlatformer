using _2DPlatformer;
using _2DPlatformer.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Net.Mime;

namespace _2DPlatformer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        

        public static int screenWidth = 1280;
        public static int screenHeight = 720;

        private State _currentState;
        private State _nextState;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = screenWidth; 
            _graphics.PreferredBackBufferHeight = screenHeight;  
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            
            

            
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic her

            base.Initialize();
        }

        protected override void LoadContent()
        {
            

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentState = new MenuState(this, GraphicsDevice, Content);
        }

        protected override void Update(GameTime gameTime)
        {

            if (_nextState != null)
            {
                _currentState = _nextState;
                _currentState.LoadContent();

                _nextState = null;
            }

            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);


            
            
            base.Update(gameTime);
        }

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentState.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }
    }
}