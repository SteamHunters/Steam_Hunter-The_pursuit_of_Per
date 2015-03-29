using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class StatusWindow
    {
        // byta namn på dess sen då vi kommer på de rätta namnen på dem
        protected int intelegens, styrka, agility, livskraft, tur, hp, mana, lvl, money, nextLvl, exp;
        private string karakterName;
        private Rectangle healthBB;
        private PlayerIndex playerIndex;

        public StatusWindow(string karakterName, int hp, int mana, int lvl, PlayerIndex playerIndex)
        {
            this.karakterName = karakterName;
            this.hp = hp;
            this.mana = mana;
            this.lvl = lvl;
            this.playerIndex = playerIndex;
        }


        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        #region Get and GetSet methods

        #region Get methods
        public int GetInteligens()
        {
            return intelegens;
        }
        public int GetStyrka()
        {
            return styrka;
        }
        public int GetAgility()
        {
            return agility;
        }
        public int GetLivskraft()
        {
            return livskraft ;
        }
        public int GetTur()
        {
            return tur;
        }
        public int GetMoney()
        {
            return money;
        }
        public int GetExp()
        {
            return exp;
        }
        #endregion

        #region GetSet metods
        public int SetInteligens
        {
            get { return intelegens; }
            set { intelegens = value; }
        }
        public int SetStyrka
        {
            get { return styrka; }
            set { styrka = value; }
        }
        public int SetAgility
        {
            get { return agility; }
            set { agility = value; }
        }
        public int SetLivskraft
        {
            get { return livskraft; }
            set { livskraft = value; }
        }
        public int SetTur
        {
            get { return tur; }
            set { tur = value; }
        }
        public int SetMoney
        {
            get { return money; }
            set { money = value; }
        }
        public int SetExp
        {
            get { return exp; }
            set { exp = value; }
        }
        #endregion

        #endregion





    }
}
