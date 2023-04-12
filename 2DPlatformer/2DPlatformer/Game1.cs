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
        Map map;


        public static List<Platform> platforms = new List<Platform>();
        public static List<Enemy> enemies = new List<Enemy>();
        public static List<Enemy> enemiesReset = new List<Enemy>();
        public static List<Coin> coins = new List<Coin>();


        public static Texture2D brick, block, enemy_texture, breakable_brick, coin_texture;
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
            brick = Content.Load<Texture2D>("brick");
            block = Content.Load<Texture2D>("block");
            enemy_texture = Content.Load<Texture2D>("enemy");
            breakable_brick = Content.Load<Texture2D>("breakable-brick");
            coin_texture = Content.Load<Texture2D>("coin");

            map = new Map();
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Map.Generate(platforms);

            player = new Player(Content.Load<Texture2D>("character"), new Vector2(38, 157), platforms, enemies);

            

            //enemies.Add(new Enemy(enemy_texture, new Vector2(428, -35), platforms));
            //enemies.Add(new Enemy(enemy_texture, new Vector2(905, -35), platforms));

            //enemiesReset.Add(new Enemy(enemy_texture, new Vector2(428, -35), platforms));
            //enemiesReset.Add(new Enemy(enemy_texture, new Vector2(905, -35), platforms));



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
            foreach (Enemy enemy in enemies)
            {
                enemy.Update(gameTime);
            }
            camera.Follow(player);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(transformMatrix: camera.Transform);

            player.Draw(_spriteBatch);
            for (int i =0; i < enemies.Count; i++)
            {
                enemies[i].Draw(_spriteBatch);
            }
            foreach (Platform platform in platforms)
            {
                platform.Draw(_spriteBatch);
            }
            foreach (Coin coin in coins)
            {
                coin.Draw(_spriteBatch);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}