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
        protected int intelegens, styrka, agility, livskraft, tur, hp, maxHp, mana, maxMana,  lvl, money, nextLvl, exp;
        private string karakterName;
        private PlayerIndex playerIndex;

        public StatusWindow(string karakterName, int hp, int maxHp, int mana, int lvl, PlayerIndex playerIndex)
        {
            this.karakterName = karakterName;
            this.hp = hp;
            this.maxHp = hp;
            this.mana = mana;
            this.maxMana = mana;
            this.lvl = lvl;
            this.playerIndex = playerIndex;
        }


        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void GenerateHealthBar(int CurrentHp, int MaxHp, SpriteBatch spriteBatch)
        {
            Double Percent = (Double)CurrentHp / MaxHp;
            //spriteBatch.Draw(healthTexture, new Rectangle(pos, storlek), Color.White);
        }
        public void GenerateManaBar(int CurrentMana, int MaxMana, SpriteBatch spriteBatch)
        {
            Double Percent = (Double)CurrentMana / MaxMana;
            //spriteBatch.Draw(ManaTexture, new Rectangle(pos, storlek), Color.White);
        }




        #region Get and Set methods

        #region Get methods

        #region Get stats
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

        #region Get Hp mana
        public int GetHP()
        {
            return hp;
        }
        public int GetMana()
        {
            return mana;
        }

        #endregion

        #endregion
        //
        #region Set metods

        #region Set stats
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

        #region Set Hp Mana
        public int SetHp
        {
            get { return hp; }
            set { hp = value; }
        }
        public int SetMana
        {
            get { return mana; }
            set { mana = value; }
        }
        #endregion

        #endregion

        #endregion






    }
}
