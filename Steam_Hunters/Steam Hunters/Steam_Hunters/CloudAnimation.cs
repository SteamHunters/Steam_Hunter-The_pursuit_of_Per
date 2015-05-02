using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Steam_Hunters
{
    public class CloudAnimation
    {
        private Texture2D texture;          // layer texture  
        public Vector2 position1;           // position of 1st layer copy
        public Vector2 position2;           // position of 1st layer copy
        public float speed;                // layer speed (for parallax scrolling)
        private float viewportWidth;        // viewport width
        private float viewportHeight;       // viewport height


        /// <summary>
        /// Building a new layer in our Parallax Scroll. 
        /// </summary>
        /// <param name="graphics">graphics device where we are drawing our layer </param>
        /// <param name="texture">Layer background texture</param>
        /// <param name="speed">speed to move our layer (parallax)</param>
        public CloudAnimation(GraphicsDeviceManager graphics, Texture2D texture, float speed)
        {
            // Assigments
            this.texture = texture;
            this.speed = speed;
            viewportWidth = graphics.GraphicsDevice.Viewport.Width;
            viewportHeight = graphics.GraphicsDevice.Viewport.Height;

            // First layer in Screen, second layer, just behind first one
            position1 = new Vector2(0.0f, 0.0f);
            position2 = new Vector2(0.0f, 0.0f + texture.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime">time elapsed since last iteration</param>
        public void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Method to move up our parrallax layer ( moving backwards effect).
        /// </summary>
        /// <param name="gameTime">time elapsed since last iteration</param>
        public void MoveUp(GameTime gameTime)
        {
            // get seconds since last iteration
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // update the current layer position (using layer's speed and delta)
            position1.Y -= speed * delta;
            position2.Y -= speed * delta;

            // Check if the layer have reach out of our Viewporth. If yes we
            // move it behind the other layer. 
            if (position1.Y < (viewportHeight * -1))
                position1.Y = position2.Y + texture.Height;

            if (position2.Y < (viewportHeight * -1))
                position2.Y = position1.Y + texture.Height;

        }

        /// <summary>
        /// Method to move up our parrallax layer ( moving forward effect).
        /// </summary>
        /// <param name="gameTime">time elapsed since last iteration</param>
        public void MoveDown(GameTime gameTime)
        {
            // get seconds since last iteration
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // update the current layer position (using layer's speed and delta)
            position1.Y += speed * delta;
            position2.Y += speed * delta;

            // Check if the layer have reach out of our Viewporth. In afirmative case, we
            // move it behind the other layer. 
            if (position1.Y > viewportHeight)
                position1.Y = position2.Y - texture.Height;

            if (position2.Y > viewportHeight)
                position2.Y = position1.Y - texture.Height;
        }

        /// <summary>
        /// Draw the layer
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Only if visible. 
            if (position1.X < viewportWidth)
                spriteBatch.Draw(texture, position1, Color.White);
            if (position2.X < viewportWidth)
                spriteBatch.Draw(texture, position2, Color.White);
        }
    }
}
