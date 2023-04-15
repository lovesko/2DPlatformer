using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using _2DPlatformer.States;
using _2DPlatformer.Tiles;

namespace _2DPlatformer
{
    public class Map
    {

        public static int[,] map_layout = new int[,] {
            { 0, 0, 0, 0, 0, 4, 4, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 1, 9, 0, 0, 0, 3, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, //64x9
            { 0, 0, 0, 0, 3, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 3, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 3, 2, 2, 2, 0, 0, 0, 6, 6, 6, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        };

        public Map() { }

        public static void Generate(List<Platform> platforms)
        {
            for (int x = 0; x < map_layout.GetLength(1); x++)
            {
                for (int y = 0; y < map_layout.GetLength(0); y++)
                {
                    int number = map_layout[y, x];

                    switch (number)
                    {
                        case 1:
                            platforms.Add(new Platform(GameState.grass_texture, new Vector2(x * GameState.grass_texture.Width, y * GameState.grass_texture.Width), false, false, false));
                            break;
                        case 2:
                            platforms.Add(new Platform(GameState.dirt_texture, new Vector2(x * GameState.dirt_texture.Width, y * GameState.dirt_texture.Width), false, false, false));
                            break;
                        case 3:
                            platforms.Add(new Platform(GameState.grass2_texture, new Vector2(x * GameState.grass2_texture.Width, y * GameState.grass2_texture.Width), false, false, false));
                            break;
                        case 4:
                            GameState.coins.Add(new Coin(GameState.coin_texture, new Vector2(x * 64 + 16, y * 64)));
                            break;
                        case 5:
                            GameState.enemies.Add(new Enemy(GameState.enemy_texture, new Vector2(x * 64, y), platforms));
                            break;
                        case 6:
                            platforms.Add(new Platform(GameState.spike_texture, new Vector2(x * GameState.spike_texture.Width, y * GameState.spike_texture.Width), true, false, false));
                            break;
                        case 7:
                            platforms.Add(new Platform(GameState.sign_texture, new Vector2(x * 64, y * 64 + 10), false, false, false));
                            break;
                        case 8:
                            platforms.Add(new Platform(GameState.spring_texture, new Vector2(x * GameState.spring_texture.Width, y * GameState.spring_texture.Height), false, false, true));
                            break;
                        case 9:
                            GameState.movingPlatforms.Add(new MovingPlatform(GameState.grass_texture, new Vector2(x * GameState.grass_texture.Width, y * GameState.grass_texture.Height)));
                            break;
                    }
                }
            }
            GameState.platforms = platforms;
        }


    }
}
