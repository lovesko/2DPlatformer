using _2DPlatformer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DPlatformer
{
    internal class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Player target)
        {
            var position = Matrix.CreateTranslation(
                MathHelper.Clamp(-target.position.X - (target.rectangle.Width / 2), -538745873475, -650),
                //-target.position.Y - (target.rectangle.Height / 2),
                0,
                0);
            
            var offset = Matrix.CreateTranslation(
                    Game1.screenWidth / 2,
                    Game1.screenHeight / 2 -100,
                    0);

            Transform = position * offset;
        }
    }
}
