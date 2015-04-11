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
        private Color colorint = Color.White, colorstr = Color.White, coloragili = Color.White, colorvitality = Color.White, colorluck = Color.White;
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
            this.points = 100;
            this.Selectedattributs = Attributs.intelligence;
        }

        public override void Update(GameTime gameTime)
        {
            gamePadState = GamePad.GetState(playerIndex);

            if (active == true)
            {
                #region Select attribut
                switch (Selectedattributs)
                {
                    case Attributs.intelligence:
                        #region intelligence
                        colorint = Color.Red;
                        if (gamePadState.DPad.Up == ButtonState.Pressed && oldgamePadState.DPad.Up == ButtonState.Released)
                        {
                            this.colorint = Color.White;
                            Selectedattributs = Attributs.luck;
                        }
                        if (gamePadState.DPad.Down == ButtonState.Pressed && oldgamePadState.DPad.Down == ButtonState.Released)
                        {
                            this.colorint = Color.White;
                            Selectedattributs = Attributs.strength;
                        }
                        if (gamePadState.Buttons.A == ButtonState.Pressed && oldgamePadState.Buttons.A == ButtonState.Released)
                        {
                            if (points > 0)
                            {
                                intelligence++;
                                points--;
                            }
                        }
                        #endregion
                        break;
                    case Attributs.strength:
                        #region strength
                        colorstr = Color.Red;
                        if (gamePadState.DPad.Up == ButtonState.Pressed && oldgamePadState.DPad.Up == ButtonState.Released)
                        {
                            colorstr = Color.White;
                            Selectedattributs = Attributs.intelligence;
                        }
                        if (gamePadState.DPad.Down == ButtonState.Pressed && oldgamePadState.DPad.Down == ButtonState.Released)
                        {
                            colorstr = Color.White;
                            Selectedattributs = Attributs.agility;
                        }
                        if (gamePadState.Buttons.A == ButtonState.Pressed && oldgamePadState.Buttons.A == ButtonState.Released)
                        {
                            if (points > 0)
                            {
                                strength++;
                                points--;
                            }
                        }
                        #endregion
                        break;
                    case Attributs.agility:
                        #region agility
                        coloragili = Color.Red;
                        if (gamePadState.DPad.Up == ButtonState.Pressed && oldgamePadState.DPad.Up == ButtonState.Released)
                        {
                            coloragili = Color.White;
                            Selectedattributs = Attributs.strength;
                        }
                        if (gamePadState.DPad.Down == ButtonState.Pressed && oldgamePadState.DPad.Down == ButtonState.Released)
                        {
                            coloragili = Color.White;
                            Selectedattributs = Attributs.vitality;
                        }
                        if (gamePadState.Buttons.A == ButtonState.Pressed && oldgamePadState.Buttons.A == ButtonState.Released)
                        {
                            if (points > 0)
                            {
                                agility++;
                                points--;
                            }
                        }
                        #endregion
                        break;
                    case Attributs.vitality:
                        #region vitality
                        colorvitality = Color.Red;
                        if (gamePadState.DPad.Up == ButtonState.Pressed && oldgamePadState.DPad.Up == ButtonState.Released)
                        {
                            colorvitality = Color.White;
                            Selectedattributs = Attributs.agility;
                        }
                        if (gamePadState.DPad.Down == ButtonState.Pressed && oldgamePadState.DPad.Down == ButtonState.Released)
                        {
                            colorvitality = Color.White;
                            Selectedattributs = Attributs.luck;
                        }
                        if (gamePadState.Buttons.A == ButtonState.Pressed && oldgamePadState.Buttons.A == ButtonState.Released)
                        {
                            if (points > 0)
                            {
                                vitality++;
                                points--;
                            }
                        }
                        #endregion
                        break;
                    case Attributs.luck:
                        #region luck
                        colorluck = Color.Red;
                        if (gamePadState.DPad.Up == ButtonState.Pressed && oldgamePadState.DPad.Up == ButtonState.Released)
                        {
                            colorluck = Color.White;
                            Selectedattributs = Attributs.vitality;
                        }
                        if (gamePadState.DPad.Down == ButtonState.Pressed && oldgamePadState.DPad.Down == ButtonState.Released)
                        {
                            colorluck = Color.White;
                            Selectedattributs = Attributs.intelligence;
                        }
                        if (gamePadState.Buttons.A == ButtonState.Pressed && oldgamePadState.Buttons.A == ButtonState.Released)
                        {
                            if (points > 0)
                            {
                                luck++;
                                points--;
                            }
                        }
                        #endregion
                        break;
                }
                #endregion
            }

            oldgamePadState = GamePad.GetState(playerIndex);          
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (active == true)
            {
                spriteBatch.DrawString(FontManager.font, " Int: "+ intelligence, new Vector2(pos.X -200 , pos.Y - 100 ), colorint);
                spriteBatch.DrawString(FontManager.font, "\n str: " + strength, new Vector2(pos.X - 200, pos.Y - 100), colorstr);
                spriteBatch.DrawString(FontManager.font, "\n\n agil: "+ agility, new Vector2(pos.X - 200, pos.Y - 100), coloragili);
                spriteBatch.DrawString(FontManager.font, "\n\n\n vita: "+ vitality, new Vector2(pos.X - 200, pos.Y - 100), colorvitality);
                spriteBatch.DrawString(FontManager.font, "\n\n\n\n luck: "+ luck, new Vector2(pos.X - 200, pos.Y - 100), colorluck);
            }
            
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
        public Vector2 SetPos
        {
            get { return pos; }
            set { pos = value; }
        }

        #endregion
        #endregion

    }
}
