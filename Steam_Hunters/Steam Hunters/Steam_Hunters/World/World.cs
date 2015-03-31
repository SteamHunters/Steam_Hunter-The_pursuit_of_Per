using Griddy2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class World
    {
        // Anton ska koda här imorgon rör ej
        Grid level1;
        ContentManager content;
        List<Tile> hitbox, EnimesMonsterType, SpawnPlayer1, SpawnPlayer2, SpawnPlayer3, SpawnPlayer4, traps, npc, mm;
        Stream gridDataStream, tileBankStream;

        public World(ContentManager content)
        {
            this.content = content;
        }

        public void Update(/*Playerlist*/)
        {
            //skriv in kolition här kanske?

            // kollar om det kolideras med hitbox tilsen ex kod
            //foreach (Tile h in hitbox)
            //{
            //    Rectangle rect = new Rectangle((int)h.Position.X, (int)h.Position.Y, 35, 35);
            //    if (PLayer.Intersects(rect))
            //    {
            //        kolide = true;
            //        PLayer.X = (int)pastpos.X;
            //        PLayer.Y = (int)pastpos.Y;

            //        break;
            //    }
            //}
        }


        public void Draw(SpriteBatch spritebatch)
        {

        }

        private void LoadLevel1()
        {
            //this.gridDataStream = new FileStream("Content/Maps/testTiled2.tmx", FileMode.Open, FileAccess.Read);
            //this.tileBankStream = new FileStream("Content/Maps/tileBank2.xml", FileMode.Open, FileAccess.Read);

            //GridData gridData = GridData.NewFromStreamAndWorldPosition(gridDataStream, new Vector2(1, 0));
            //TileBank tileBank = TileBank.CreateFromSerializedData(tileBankStream, content);

            //gridDataStream.Position = 0;
            //SerializedGridFactory gridFactory = SerializedGridFactory.NewFromData(gridDataStream, gridData, tileBank);

            //level1 = Grid.NewGrid(gridData, gridFactory, DefaultGridDrawer.NewFromGridData(gridData, content, Color.Black));

            // YEY detta funkar
            //Predicate<Tile> temp = FindHitboxTiles;
            //tiles = level1.GetLayer("Hitbox").GetAllMatchingTiles(temp);

        }

        //private static bool FindHitboxTiles(Tile obj)
        //{
        //    return obj.Name == "hitbox";
        //}


        #region Get and Set method
        #endregion




    }
}
