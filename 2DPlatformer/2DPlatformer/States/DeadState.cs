using _2DPlatformer.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2DPlatformer.States
{
    public class DeadState : State
    {
        public static Button button_respawn;
        public static Button button_menu;

        //varje gång vi går från DeadState till GameState så skapar vi ett nytt spelarobjekt i GameState. När vi skapar en ny spelare så blir spelarens level till 1 och scoret till 0 eftersom det
        //är värdena som är givna i spelarens konstruktor. För att fixa detta så måste vi spara spelarens level och score i en separat variabel utanför spelarens konstruktor, vilket vi gör här.
        //När vi då klickar på Respawn-knappen och går från DeadState till GameState så sparar vi spelarens tidigare värden i dessa variabler och sedan ger tillbaka dem till spelaren i GameState
        //när spelaren respawnar.
        public static int savedPlayerLevel = 1;
        public static int savedPlayerScore = 0;

        SpriteFont font, font_larger;
        Texture2D button_texture;
        SoundEffect click_sound;
        public DeadState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            font = _content.Load<SpriteFont>("Fonts/font");
            font_larger = content.Load<SpriteFont>("Fonts/font_larger");
            button_texture = _content.Load<Texture2D>("Controls/button");
            click_sound = _content.Load<SoundEffect>("Sound Effects/interface1");

            button_respawn = new Button(button_texture, new Vector2(Game1.screenWidth / 2 - button_texture.Width / 2, Game1.screenHeight / 2 - button_texture.Height / 2), font, "Respawn");
            button_menu = new Button(button_texture, new Vector2(Game1.screenWidth / 2 - button_texture.Width / 2, Game1.screenHeight / 2 - button_texture.Height / 2 + 150), font, "Menu");

            MediaPlayer.Stop();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.Green);
            spriteBatch.Begin();
            button_menu.Draw(spriteBatch);
            button_respawn.Draw(spriteBatch);
            spriteBatch.DrawString(font_larger, "You Died!", new Vector2(1280 / 2 - 250, 150), Color.White);
            spriteBatch.End();
        }

        public override void LoadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            button_respawn.Update();
            if (button_respawn.clicked) //om klickar play --> spelet startar
            {
                click_sound.Play();
                Thread.Sleep(200);
                GameState.enemies.Clear();
                GameState.coins.Clear();
                GameState.platforms.Clear();
                GameState.movingPlatforms.Clear();
                GameState.backgroundTiles.Clear();
                savedPlayerLevel = GameState.player.level;
                savedPlayerScore = GameState.player.savedScore;
                _game.ChangeState(new GameState(_game, _graphics, _content));
                _game.IsMouseVisible = false;
            }

            button_menu.Update();
            if (button_menu.clicked)
            {
                click_sound.Play();
                Thread.Sleep(200);
                _game.ChangeState(new MenuState(_game, _graphics, _content));
            }
        }
    }
}
