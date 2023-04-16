﻿using _2DPlatformer.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DPlatformer.States
{
    public class GameState : State
    {
        public static List<Platform> platforms = new List<Platform>();
        public static List<Enemy> enemies = new List<Enemy>();
        public static List<Coin> coins = new List<Coin>();
        public static List<MovingPlatform> movingPlatforms = new List<MovingPlatform>();

        public static Texture2D grass2_texture, enemy_texture, dirt_texture, coin_texture, grass_texture, spike_texture, sign_texture, spring_texture, 
                                player_texture, player_walking_texture, player_jumping_texture, enemy_walking_texture;

        public static SoundEffect jump_sound, enemy_death_sound, coin_sound, player_death_sound;

        Song music;

        SpriteFont score_font;
        public static string score_str = "0";
        public static Vector2 score_pos;

        Player player;
        Map map;
        Camera camera;

        public GameState(Game1 game, GraphicsDevice graphics, ContentManager content)
            : base(game, graphics, content)
        {

        }
        public override void LoadContent()
        {
            

            player_walking_texture = _content.Load<Texture2D>("Sprites/walk2");
            player_jumping_texture = _content.Load<Texture2D>("Sprites/jump");
            player_texture = _content.Load<Texture2D>("Sprites/player");
            dirt_texture = _content.Load<Texture2D>("Sprites/ground6");
            grass2_texture = _content.Load<Texture2D>("Sprites/ground4");
            grass_texture = _content.Load<Texture2D>("Sprites/ground11");
            spike_texture = _content.Load<Texture2D>("Sprites/spike");
            coin_texture = _content.Load<Texture2D>("Sprites/coin4");
            enemy_texture = _content.Load<Texture2D>("Sprites/slime-monster");
            enemy_walking_texture = _content.Load<Texture2D>("Sprites/santa-walk");
            sign_texture = _content.Load<Texture2D>("Sprites/pointer2");
            spring_texture = _content.Load<Texture2D>("Sprites/spring2");


            jump_sound = _content.Load<SoundEffect>("Sound Effects/jump01");
            player_death_sound = _content.Load<SoundEffect>("Sound Effects/death2");
            enemy_death_sound = _content.Load<SoundEffect>("Sound Effects/death1");
            coin_sound = _content.Load<SoundEffect>("Sound Effects/coin01");

            music = _content.Load<Song>("Music/game-music");
            
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(music);


            map = new Map();
            Map.Generate();

            player = new Player(player_texture, player_walking_texture, player_jumping_texture, new Vector2(46, 489));

            
            camera = new Camera();

            score_font = _content.Load<SpriteFont>("Fonts/font");
            score_pos = new Vector2(600, 489);

            
            
        }
        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            camera.Follow(player);
            foreach (Enemy enemy in enemies)
            {
                enemy.Update(gameTime);
            }
            foreach (Coin coin in coins)
            {
                coin.Update();
            }
            foreach (MovingPlatform platform in movingPlatforms)
            {
                platform.Update();
            }
            if (player.position.X < 625) //när kameran inte är centrerad på spelaren ska score-visaren inte vara baserad på spelarens position. Kameran börjar röra sig ~625.
            {
                score_pos = new Vector2(_graphics.Viewport.Width - 100, 100);
            }
            else
            {
                score_pos = new Vector2(player.position.X + (_graphics.Viewport.Width / 2) - 85, 100);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _game.ChangeState(new MenuState(_game, _graphics, _content));
                _game.IsMouseVisible = true;
                player.Die();
            }
            if (player.win)
            {
                _game.ChangeState(new EndState(_game, _graphics, _content));
                _game.IsMouseVisible = true;
            }
        }
        public override void PostUpdate(GameTime gameTime) {   }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(transformMatrix: camera.Transform);
            player.Draw(spriteBatch);
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (Platform platform in platforms)
            {
                platform.Draw(spriteBatch);
            }
            foreach (Coin coin in coins)
            {
                coin.Draw(spriteBatch);
            }
            foreach (MovingPlatform platform in movingPlatforms)
            {
                platform.Draw(spriteBatch);
            }
            spriteBatch.DrawString(score_font, score_str, score_pos, Color.Yellow);
            spriteBatch.End();
        }
    }
}
