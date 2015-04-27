using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Steam_Hunters
{
    class CloudAnimation
    {
        List<Vector2> foreground, middleground, background;
        int fgSpacing, mgSpacing, bgSpacing;
        float fgSpeed, mgSpeed, bgSpeed;
        Texture2D[] tex;
        GameWindow window;

        public CloudAnimation(ContentManager Content, GameWindow window)
        {
            this.tex = new Texture2D[3];
            this.window = window;

            tex[0] = Content.Load<Texture2D>("Cloud");
            tex[1] = Content.Load<Texture2D>("Cloud2");
            tex[2] = Content.Load<Texture2D>("Cloud3");

            middleground = new List<Vector2>();
            mgSpacing = window.ClientBounds.Width /1;
            mgSpeed = 0.75f;

            for (int i = 0; i < (window.ClientBounds.Width / mgSpacing) + 2; i++)
            {
                middleground.Add(new Vector2(i * mgSpacing,
                window.ClientBounds.Height - tex[0].Height - tex[1].Height));
            }

            background = new List<Vector2>();
            bgSpacing = window.ClientBounds.Width / 2;
            bgSpeed = 1.0f;

            for (int i = 0; i < (window.ClientBounds.Width / bgSpacing) + 2; i++)
            {
                background.Add(new Vector2(i * bgSpacing, window.ClientBounds.Height
                - tex[2].Height));
            }

            foreground = new List<Vector2>();
            fgSpacing = window.ClientBounds.Width /1 ;
            fgSpeed = 0.8f;

            for (int i = 0; i < (window.ClientBounds.Width / fgSpacing) + 2; i++)
            {
                foreground.Add(new Vector2(i * fgSpacing, window.ClientBounds.Height
                - tex[0].Height));
            }
        }

        public void Update()
        {
            mgSpeed = 0.25f;
            fgSpeed = 0.5f;
            bgSpeed = 0.1f;

            for (int i = 0; i < foreground.Count; i++)
            {
                foreground[i] = new Vector2(foreground[i].X - fgSpeed,
                foreground[i].Y - fgSpeed);
                if (foreground[i].X <= -fgSpacing)
                {
                    int j = i - 1;
                    if (j < 0)
                    {
                        j = foreground.Count - 1;
                    }
                    foreground[i] = new Vector2(foreground[j].X + fgSpacing - 1,
                    foreground[i].Y +(window.ClientBounds.Height) - 1);
                }
            }

            for (int i = 0; i < middleground.Count; i++)
            {
                middleground[i] = new Vector2(middleground[i].X - mgSpeed,
                middleground[i].Y - mgSpeed);
                if (middleground[i].X <= -mgSpacing)
                {
                    int j = i - 1;
                    if (j < 0)
                    {
                        j = middleground.Count - 1;
                    }
                    middleground[i] = new Vector2(middleground[j].X + mgSpacing - 1,
                    middleground[i].Y+(window.ClientBounds.Height) - 1);
                }
            }
            for (int i = 0; i < background.Count; i++)
            {
                background[i] = new Vector2(background[i].X - bgSpeed,
                background[i].Y - bgSpeed);
                if (background[i].X <= -bgSpacing)
                {
                    int j = i - 1;
                    if (j < 0)
                    {
                        j = background.Count - 1;
                    }
                    background[i] = new Vector2(background[j].X + bgSpacing - 1,
                    background[i].Y+(window.ClientBounds.Height) - 1);
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (Vector2 v in background)
            {
                sb.Draw(tex[2], v, Color.White);
            }
            foreach (Vector2 v in middleground)
            {
                sb.Draw(tex[1], v, Color.White);
            }
            foreach (Vector2 v in foreground)
            {
                sb.Draw(tex[0], v, Color.White);
            }
        }
    }
    }