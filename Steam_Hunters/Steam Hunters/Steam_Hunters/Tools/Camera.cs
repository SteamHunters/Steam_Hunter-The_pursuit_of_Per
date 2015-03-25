using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class Camera
    {
        public Matrix transform;
        Viewport view;
        public Vector2 centre;

        public Camera(Viewport newView)
        {
            view = newView;
        }

        public void Update(GameTime gameTime, Player engineer)
        {
            centre = new Vector2(engineer.pos.X + (engineer.hitBox.Width / 2) - view.Width / 2, engineer.pos.Y + (engineer.hitBox.Height / 2) - view.Height / 2);
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
        }
    }
}
