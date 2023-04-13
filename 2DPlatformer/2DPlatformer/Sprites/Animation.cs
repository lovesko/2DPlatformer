using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DPlatformer.Sprites
{
    public class Animation
    {
        Texture2D texture;
        List<Rectangle> sourceRectangles = new();
        public static float TotalSeconds { get; set; }

        int frames;
        int frame;
        float frameTime;
        float frameTimeLeft;
        bool active = true;

        public Animation(Texture2D newTexture, int framesX, float newFrameTime)
        {
            texture = newTexture;
            frameTime = newFrameTime;
            frameTimeLeft = frameTime;
            frames = framesX;


            var frameWidth = texture.Width / framesX;
            var frameHeight = texture.Height;

            for (int i = 0; i < frames; i++)
            {
                sourceRectangles.Add(new Rectangle(i * frameWidth, 0, frameWidth, frameHeight));
            }
        }

        public void Stop()
        {
            active = false;
        }

        public void Start()
        {
            active = true;
        }

        public void Reset()
        {
            frame = 0;
            frameTimeLeft = frameTime;
        }


        public void Update()
        {
            if (active == false) return;

            frameTimeLeft -= Game1.TotalSeconds;

            if (frameTimeLeft <= 0)
            {
                frameTimeLeft += frameTime;
                frame = (frame +1) % frames;
            }
        }

        public void Draw(Vector2 position, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRectangles[frame], Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
        }

    }
}
