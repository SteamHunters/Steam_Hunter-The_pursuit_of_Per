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
        public Grid level;
        ContentManager content;
        public List<Tile> hitboxList, EnimesMonsterType, SpawnPlayer1, SpawnPlayer2, SpawnPlayer3, SpawnPlayer4, traps, npc, mm;
        Stream gridDataStream, tileBankStream;

        public World(ContentManager content)
        {
            this.content = content;

            if(GameData.Level == 1)
                LoadLevel1();

        }

        public void Update()
        {
            //skriv in kolition här kanske?

            // kollar om det kolideras med hitbox tilsen ex kod
            foreach (Tile h in hitboxList)
            {
                Rectangle rect = new Rectangle((int)h.Position.X, (int)h.Position.Y, 50, 50);
                foreach (Player p in GameData.playerList)
                {
                    if (p.hitBox.Intersects(rect))
                    {
                        p.pos = p.prevPos;
                        break;
                    }
                }
            }
        }

        public void DrawLayerBase(SpriteBatch spriteBatch)
        {
            level.GetLayer("Base").Draw(spriteBatch);
            level.GetLayer("Middle").Draw(spriteBatch);
        }

        public void DrawLayerTop(SpriteBatch spriteBatch)
        {
            level.GetLayer("Top").Draw(spriteBatch);
        }
        public void DrawLayerHitbox(SpriteBatch spriteBatch)
        {
            level.GetLayer("Hitbox").Draw(spriteBatch);
        }

        

        private void LoadLevel1()
        {
            this.gridDataStream = new FileStream("Content/Maps/tileMap_v1.tmx", FileMode.Open, FileAccess.Read);
            this.tileBankStream = new FileStream("Content/Maps/tileBank_v1.xml", FileMode.Open, FileAccess.Read);

            GridData gridData = GridData.NewFromStreamAndWorldPosition(gridDataStream, new Vector2(1, 0));
            TileBank tileBank = TileBank.CreateFromSerializedData(tileBankStream, content);

            gridDataStream.Position = 0;
            SerializedGridFactory gridFactory = SerializedGridFactory.NewFromData(gridDataStream, gridData, tileBank);

            level = Grid.NewGrid(gridData, gridFactory, DefaultGridDrawer.NewFromGridData(gridData, content, Color.Black));

            // YEY detta funkar
            Predicate<Tile> temp = FindHitboxTiles;
            hitboxList = level.GetLayer("hitbox").GetAllMatchingTiles(temp);
        }

        private static bool FindHitboxTiles(Tile obj)
        {
            return obj.Name == "hitbox";
        }

        #region Get and Set method
        #endregion




    }
}
