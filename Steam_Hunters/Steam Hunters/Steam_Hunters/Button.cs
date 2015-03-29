using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Steam_Hunters
{
    class Button : GameObject
    {
        public bool selected;
        public Rectangle hitBox;

        public Button(Texture2D tex, Vector2 pos)
            : base(tex, pos)
        {
            this.hitBox = new Rectangle((int)pos.X, (int)pos.Y, 50, 50);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (selected)
                spriteBatch.Draw(tex, hitBox, Color.Tomato);
            else
                spriteBatch.Draw(tex, hitBox, Color.White);
        }

    }
}
