using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;

namespace Steam_Hunters
{
    class Dialog
    {
        public Vector2 pos;
        public Texture2D tex;
        public String text, nextText;
        public SpriteFont font;
        public bool changeText;

        public Dialog(Texture2D tex, Vector2 pos, String text, String nextText, SpriteFont font)
        {
            this.tex = tex;
            this.pos = pos;
            this.text = text;
            this.nextText = nextText;
            this.font = font;
        }
        public void Update(GameTime gameTime)
        {
            if(changeText == true)
            {
                text = nextText;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
            spriteBatch.DrawString(font, text, pos, Color.White);
        }
    }
}
