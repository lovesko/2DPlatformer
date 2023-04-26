using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2DPlatformer.Controls
{
    public class Button
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;
        SpriteFont font;
        MouseState lastMouseState;
        public bool clicked = false;
        public string text;

        Color color = Color.White;

        public Button(Texture2D newTexture, Vector2 newPosition, SpriteFont newFont, string newText)
        {
            texture = newTexture;
            position = newPosition;
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width, (int)texture.Height);
            font = newFont;
            text = newText;
        }

        public void Update()
        {
            MouseState mouseState = Mouse.GetState();
            Rectangle cursor = new Rectangle(mouseState.X, mouseState.Y, 10, 10);
            if (cursor.Intersects(rectangle))
            {
                color = Color.LightGray;
                if (mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                {
                    clicked = true;
                }
            }
            else
            {
                color = Color.White;
            }

            lastMouseState = mouseState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);
            spriteBatch.DrawString(font, text, new Vector2(position.X + 100, position.Y + 15), Color.Black);
        }
    }
}
