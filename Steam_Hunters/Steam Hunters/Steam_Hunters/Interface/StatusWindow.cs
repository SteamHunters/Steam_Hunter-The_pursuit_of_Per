using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class StatusWindow: GameObject
    {
        private enum Attributs
        {
            intelligence,
            strength,
            agility,
            vitality,
            luck
        }

        private Attributs Selectedattributs;
        private int intelligence, strength, agility, vitality, luck, hp, maxHp, mana, maxMana, lvl, money, nextLvl, exp, points;
        private string karakterName;
        private Color colorint, colorstr, coloragili, colorvitality, colorluck;
        private PlayerIndex playerIndex;
        private GamePadState gamePadState, oldgamePadState;
        private bool active;
        

        public StatusWindow(Texture2D tex, Vector2 pos, string karakterName, int intelligence, int strength, int agility, int vitality, int luck, int hp, int mana, int lvl, PlayerIndex playerIndex)
            :base(tex, pos)
        {
            this.karakterName = karakterName;
            this.intelligence = intelligence;
            this.strength = strength;
            this.agility = agility;
            this.vitality = vitality;
            this.luck = luck;
            this.hp = hp;
            this.maxHp = hp;
            this.mana = mana;
            this.maxMana = mana;
            this.lvl = lvl;
            this.money = 0;
            this.playerIndex = playerIndex;
            this.Selectedattributs = Attributs.intelligence;
        }

        public override void Update(GameTime gameTime)
        {
            gamePadState = GamePad.GetState(playerIndex);

            #region Select attribut
            switch (Selectedattributs)
            {
                case Attributs.intelligence:
                    #region intelligence
                    if (gamePadState.DPad.Up == ButtonState.Pressed && oldgamePadState.DPad.Up == ButtonState.Released)
                    {

                    }
                    if (gamePadState.DPad.Down == ButtonState.Pressed && oldgamePadState.DPad.Down == ButtonState.Released)
                    {

                    }
                   
                    #endregion
                    break;
                case Attributs.strength:
                    #region strength
                    if (gamePadState.DPad.Up == ButtonState.Pressed && oldgamePadState.DPad.Up == ButtonState.Released)
                    {

                    }
                    if (gamePadState.DPad.Down == ButtonState.Pressed && oldgamePadState.DPad.Down == ButtonState.Released)
                    {

                    }
                   
                    #endregion
                    break;
                case Attributs.agility:
                    #region agility
                    if (gamePadState.DPad.Up == ButtonState.Pressed && oldgamePadState.DPad.Up == ButtonState.Released)
                    {

                    }
                    if (gamePadState.DPad.Down == ButtonState.Pressed && oldgamePadState.DPad.Down == ButtonState.Released)
                    {

                    }
                   
                    #endregion
                    break;
                case Attributs.vitality:
                    #region vitality
                    if (gamePadState.DPad.Up == ButtonState.Pressed && oldgamePadState.DPad.Up == ButtonState.Released)
                    {

                    }
                    if (gamePadState.DPad.Down == ButtonState.Pressed && oldgamePadState.DPad.Down == ButtonState.Released)
                    {

                    }
                    
                    #endregion
                    break;
                case Attributs.luck:
                    #region luck
                   if (gamePadState.DPad.Up == ButtonState.Pressed && oldgamePadState.DPad.Up == ButtonState.Released)
                    {

                    }
                    if (gamePadState.DPad.Down == ButtonState.Pressed && oldgamePadState.DPad.Down == ButtonState.Released)
                    {

                    }

                    #endregion
                    break;
            }
            #endregion

            oldgamePadState = GamePad.GetState(playerIndex);          
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(font, "attribut", pos, color);
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
            return intelligence;
        }
        public int GetStyrka()
        {
            return strength;
        }
        public int GetAgility()
        {
            return agility;
        }
        public int GetLivskraft()
        {
            return vitality ;
        }
        public int GetTur()
        {
            return luck;
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

        public bool StatusWinwosActiv()
        {
            return active;
        }

        #endregion
        //
        #region Set metods

        #region Set stats
        public int SetInteligens
        {
            get { return intelligence; }
            set { intelligence = value; }
        }
        public int SetStyrka
        {
            get { return strength; }
            set { strength = value; }
        }
        public int SetAgility
        {
            get { return agility; }
            set { agility = value; }
        }
        public int SetLivskraft
        {
            get { return vitality; }
            set { vitality = value; }
        }
        public int SetTur
        {
            get { return luck; }
            set { luck = value; }
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

        public bool SetStatusWinwosActiv
        {
            get { return active; }
            set { active = value; }
        }

        #endregion
        #endregion

    }
}
