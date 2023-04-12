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
        Camera camera;

        public static int screenWidth = 1280;
        public static int screenHeight = 720;

        public static List<Platform> platforms = new List<Platform>();
        public static List<Enemy> enemies = new List<Enemy>();
        public static List<Coin> coins = new List<Coin>();

        public static Texture2D grass2_texture, enemy_texture, dirt_texture, coin_texture, grass_texture, spike_texture;

        SpriteFont score_font;
        public static string score_str = "0";
        public static Vector2 score_pos;

        


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
            map = new Map();
            player = new Player(Content.Load<Texture2D>("Sprites/character"), new Vector2(38, 157), platforms, enemies);
            camera = new Camera();

            dirt_texture = Content.Load<Texture2D>("Sprites/ground6");
            grass2_texture = Content.Load<Texture2D>("Sprites/ground4");
            grass_texture = Content.Load<Texture2D>("Sprites/ground11");
            spike_texture = Content.Load<Texture2D>("Sprites/spike");
            coin_texture = Content.Load<Texture2D>("Sprites/coin1");
            enemy_texture = Content.Load<Texture2D>("Sprites/mushroom");

            score_font = Content.Load<SpriteFont>("Fonts/font");
            score_pos = new Vector2(player.position.X + GraphicsDevice.Viewport.Width, 0);

            Map.Generate(platforms);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
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

            if(player.position.X < 625) //när kameran inte är centrerad på spelaren ska score-visaren inte vara baserad på spelarens position. Kameran börjar röra sig ~625.
            {
                score_pos = new Vector2(GraphicsDevice.Viewport.Width - 100, -200);
            }
            else
            {
                score_pos = new Vector2(player.position.X + (GraphicsDevice.Viewport.Width / 2) - 85, -200);
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(transformMatrix: camera.Transform);

            player.Draw(_spriteBatch);
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(_spriteBatch);
            }
            foreach (Platform platform in platforms)
            {
                platform.Draw(_spriteBatch);
            }
            foreach (Coin coin in coins)
            {
                coin.Draw(_spriteBatch);
            }
            _spriteBatch.DrawString(score_font, score_str, score_pos, Color.White);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}