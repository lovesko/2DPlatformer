using _2DPlatformer.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace _2DPlatformer.States
{
    public class GameState : State
    {
        public static List<Platform> platforms = new List<Platform>();
        public static List<Enemy> enemies = new List<Enemy>();
        public static List<Coin> coins = new List<Coin>();
        public static List<MovingPlatform> movingPlatforms = new List<MovingPlatform>();
        public static List<BackgroundTile> backgroundTiles = new List<BackgroundTile>();
        public static List<Ladder> ladders = new List<Ladder>();

        public static Texture2D grass2_texture, dirt_texture, grass_texture, grass3_texture, grass4_texture,
                                spike_texture, sign_texture, spring_texture, coin_texture, ladder_texture, box_texture,
                                player_texture, player_walking_texture, player_jumping_texture,
                                enemy_walking_texture,
                                cloud_texture, water1_texture, water2_texture, mushroom_texture, plant1_texture, plant2_texture, plant3_texture;

        public static SoundEffect jump_sound, enemy_death_sound, coin_sound, player_death_sound, win_sound;

        Song music;

        SpriteFont score_font;
        public static string score_str = "0";
        public static string youdied_str = "";
        public static Vector2 score_pos;

        public static Player player;
        Map map;
        Camera camera;

        public GameState(Game1 game, GraphicsDevice graphics, ContentManager content)
            : base(game, graphics, content)
        {

        }
        public override void LoadContent()
        {
            

            player_walking_texture = _content.Load<Texture2D>("Sprites/walk");
            player_jumping_texture = _content.Load<Texture2D>("Sprites/jump");
            player_texture = _content.Load<Texture2D>("Sprites/player");
            dirt_texture = _content.Load<Texture2D>("Sprites/ground6");
            grass2_texture = _content.Load<Texture2D>("Sprites/ground4");
            grass_texture = _content.Load<Texture2D>("Sprites/ground11");
            grass3_texture = _content.Load<Texture2D>("Sprites/ground10");
            grass4_texture = _content.Load<Texture2D>("Sprites/ground14");
            spike_texture = _content.Load<Texture2D>("Sprites/spike");
            coin_texture = _content.Load<Texture2D>("Sprites/coin4");
            enemy_walking_texture = _content.Load<Texture2D>("Sprites/zombie-walk");
            sign_texture = _content.Load<Texture2D>("Sprites/pointer2");
            spring_texture = _content.Load<Texture2D>("Sprites/spring2");
            box_texture = _content.Load<Texture2D>("Sprites/box");

            cloud_texture = _content.Load<Texture2D>("Sprites/cloud");
            water1_texture = _content.Load<Texture2D>("Sprites/water1");
            water2_texture = _content.Load<Texture2D>("Sprites/water2");
            mushroom_texture = _content.Load<Texture2D>("Sprites/mushroom");
            ladder_texture = _content.Load<Texture2D>("Sprites/ladder");
            plant1_texture = _content.Load<Texture2D>("Sprites/grass1");
            plant2_texture = _content.Load<Texture2D>("Sprites/grass2");
            plant3_texture = _content.Load<Texture2D>("Sprites/grass3");


            jump_sound = _content.Load<SoundEffect>("Sound Effects/jump01");
            player_death_sound = _content.Load<SoundEffect>("Sound Effects/death2");
            enemy_death_sound = _content.Load<SoundEffect>("Sound Effects/death1");
            coin_sound = _content.Load<SoundEffect>("Sound Effects/coin01");
            win_sound = _content.Load<SoundEffect>("Sound Effects/win");

            music = _content.Load<Song>("Music/game-music");
            
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(music);

            player = new Player(player_texture, player_walking_texture, player_jumping_texture, new Vector2(46, 489));
            map = new Map();
            Map.Generate();
            camera = new Camera();
            score_font = _content.Load<SpriteFont>("Fonts/font");
            score_pos = new Vector2(600, 489);
            score_str = player.score.ToString();
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
            else if (player.position.X > 3390)
            {
                score_pos = new Vector2(3940, 100);
            }
            else
            {
                score_pos = new Vector2(player.position.X + (_graphics.Viewport.Width / 2) - 85, 100);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                enemies.Clear();
                coins.Clear();
                platforms.Clear();
                movingPlatforms.Clear();
                backgroundTiles.Clear();
                _game.ChangeState(new MenuState(_game, _graphics, _content));
                _game.IsMouseVisible = true;
            }
            if (player.win)
            {
                player.win = false;
                player.position.X = 46;
                player.position.Y = 489;
                player.level++;
                MediaPlayer.Stop();
                win_sound.Play();
                Thread.Sleep(2500);
                enemies.Clear();
                coins.Clear();
                platforms.Clear();
                movingPlatforms.Clear();
                backgroundTiles.Clear();
                Map.Generate();

                if (player.level >= 4)
                {
                    _game.ChangeState(new EndState(_game, _graphics, _content));
                    _game.IsMouseVisible = true;
                }
                else
                {
                    MediaPlayer.Play(music);
                }
            }
            

        }
        public override void PostUpdate(GameTime gameTime) {   }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(transformMatrix: camera.Transform);
            foreach (BackgroundTile backgroundTile in backgroundTiles)
            {
                backgroundTile.Draw(spriteBatch);
            }
            foreach (Ladder ladder in ladders)
            {
                ladder.Draw(spriteBatch);
            }
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
            player.Draw(spriteBatch);
            spriteBatch.DrawString(score_font, "Score", new Vector2(score_pos.X - 65, score_pos.Y - 50), Color.Yellow);
            spriteBatch.DrawString(score_font, youdied_str, new Vector2(player.position.X, 720 / 2), Color.Red);
            spriteBatch.DrawString(score_font, score_str, score_pos, Color.Yellow);

            if (player.isDead)

            {
                spriteBatch.DrawString(score_font, youdied_str, new Vector2(player.position.X, 720 / 2), Color.Red);
                Thread.Sleep(500);
                youdied_str = "";
                player.isDead = false;
            }
            spriteBatch.End();
        }
    }
}
