using _2DPlatformer;
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
        Player player;
        List<Platform> platforms = new List<Platform>();
        Texture2D grassBlock, grassFloor, deadlyTest;
        public static int screenWidth = 1280;
        public static int screenHeight = 720;
        private Camera camera;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = screenWidth;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = screenHeight;   // set this value to the desired height of your window
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic her

            base.Initialize();
        }

        protected override void LoadContent()
        {
            grassBlock = Content.Load<Texture2D>("grass");
            grassFloor = Content.Load<Texture2D>("grass");
            deadlyTest = Content.Load<Texture2D>("big-grass");

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            platforms.Add(new Platform(grassBlock, new Vector2(200, 500), false));
            platforms.Add(new Platform(grassBlock, new Vector2(300, 400), false));
            platforms.Add(new Platform(grassBlock, new Vector2(500, 500), false));
            platforms.Add(new Platform(grassBlock, new Vector2(700, 300), false));

            platforms.Add(new Platform(deadlyTest, new Vector2(900, 500), true));



            //GOLV
            for (int i = 0; i < 50; i++)
            {
                platforms.Add(new Platform(grassFloor, new Vector2(grassFloor.Width * i, 720 - grassFloor.Height), false));
            }

            player = new Player(Content.Load<Texture2D>("Capture"), new Vector2(50, 50), platforms);
            camera = new Camera();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                _graphics.ToggleFullScreen();
            }
                player.Update(gameTime);
            camera.Follow(player);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(transformMatrix: camera.Transform);
            player.Draw(_spriteBatch);
            foreach (Platform platform in platforms)
            {
                platform.Draw(_spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}