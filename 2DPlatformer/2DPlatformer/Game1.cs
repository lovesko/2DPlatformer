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
        Texture2D grassTexture;
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
            grassTexture = Content.Load<Texture2D>("grass");

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            platforms.Add(new Platform(grassTexture, new Vector2(200, 500)));
            platforms.Add(new Platform(grassTexture, new Vector2(300, 400)));
            platforms.Add(new Platform(grassTexture, new Vector2(500, 500)));
            platforms.Add(new Platform(grassTexture, new Vector2(700, 300)));
            platforms.Add(new Platform(grassTexture, new Vector2(900, 400)));

            //GOLV
            for (int i = 0; i < 50; i++)
            {
                platforms.Add(new Platform(grassTexture, new Vector2(grassTexture.Width * i, 720 - grassTexture.Height)));
            }


            player = new Player(Content.Load<Texture2D>("character"), new Vector2(50, 50), platforms);

            camera = new Camera();


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();




            // TODO: Add your update logic here
            player.Update(gameTime);
            camera.Follow(player);




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
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