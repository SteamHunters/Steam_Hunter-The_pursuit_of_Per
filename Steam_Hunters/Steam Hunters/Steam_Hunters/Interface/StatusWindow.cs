﻿using Microsoft.Xna.Framework;
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
        public enum Attributs
        {
            intelligence,
            strength,
            agility,
            vitality,
            luck
        }

        public Attributs Selectedattributs;
        public int intelligence, strength, agility, vitality, luck, lvl, money, nextLvl, exp, points;
        public double hp, maxHp, mana, maxMana;
        private string karakterName;
        private Color colorint = Color.Black, colorstr = Color.Black, coloragili = Color.Black, colorvitality = Color.Black, colorluck = Color.Black;
        private PlayerIndex playerIndex;
        private GamePadState gamePadState, oldgamePadState;
        public bool active;
        Color color;
        

        public StatusWindow(Texture2D tex, Vector2 pos, string karakterName, int intelligence, int strength, int agility, int vitality, int luck, float hp, float mana, int lvl, PlayerIndex playerIndex)
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

            color = new Color(255, 255, 255, 0.75f);
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
                            this.colorint = Color.Black;
                            Selectedattributs = Attributs.luck;
                        }
                        if (gamePadState.DPad.Down == ButtonState.Pressed && oldgamePadState.DPad.Down == ButtonState.Released)
                        {
                            this.colorint = Color.Black;
                            Selectedattributs = Attributs.strength;
                        }
                        if (gamePadState.Buttons.A == ButtonState.Pressed && oldgamePadState.Buttons.A == ButtonState.Released)
                        {
                            if (points > 0)
                            {
                                intelligence++;
                                points--;
                                maxMana += intelligence;
                            }
                        }
                        #endregion
                        break;
                    case Attributs.strength:
                        #region strength
                        colorstr = Color.Red;
                        if (gamePadState.DPad.Up == ButtonState.Pressed && oldgamePadState.DPad.Up == ButtonState.Released)
                        {
                            colorstr = Color.Black;
                            Selectedattributs = Attributs.intelligence;
                        }
                        if (gamePadState.DPad.Down == ButtonState.Pressed && oldgamePadState.DPad.Down == ButtonState.Released)
                        {
                            colorstr = Color.Black;
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
                            coloragili = Color.Black;
                            Selectedattributs = Attributs.strength;
                        }
                        if (gamePadState.DPad.Down == ButtonState.Pressed && oldgamePadState.DPad.Down == ButtonState.Released)
                        {
                            coloragili = Color.Black;
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
                            colorvitality = Color.Black;
                            Selectedattributs = Attributs.agility;
                        }
                        if (gamePadState.DPad.Down == ButtonState.Pressed && oldgamePadState.DPad.Down == ButtonState.Released)
                        {
                            colorvitality = Color.Black;
                            Selectedattributs = Attributs.luck;
                        }
                        if (gamePadState.Buttons.A == ButtonState.Pressed && oldgamePadState.Buttons.A == ButtonState.Released)
                        {
                            if (points > 0)
                            {
                                vitality++;
                                points--;
                                maxHp += vitality;
                            }
                        }
                        #endregion
                        break;
                    case Attributs.luck:
                        #region luck
                        colorluck = Color.Red;
                        if (gamePadState.DPad.Up == ButtonState.Pressed && oldgamePadState.DPad.Up == ButtonState.Released)
                        {
                            colorluck = Color.Black;
                            Selectedattributs = Attributs.vitality;
                        }
                        if (gamePadState.DPad.Down == ButtonState.Pressed && oldgamePadState.DPad.Down == ButtonState.Released)
                        {
                            colorluck = Color.Black;
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
                spriteBatch.Draw(TextureManager.StatusWindowTexture, new Vector2(pos.X - 420, pos.Y - 220), color);
                spriteBatch.DrawString(FontManager.SteamFont, " Name: " + karakterName, new Vector2(pos.X - 385, pos.Y - 185), Color.Black);
                spriteBatch.DrawString(FontManager.SteamFont, " Level: " + lvl, new Vector2(pos.X - 385, pos.Y - 160), Color.Black);
                //spriteBatch.DrawString(FontManager.font, " Next Level: " + nextLvl, new Vector2(pos.X - 220, pos.Y - 200), Color.White);
                GenerateHealthBar(hp, maxHp, spriteBatch);
                GenerateManaBar(mana, maxMana, spriteBatch);
                spriteBatch.DrawString(FontManager.SteamFont, " Intelligence:", new Vector2(pos.X - 385, pos.Y - 15), colorint);
                spriteBatch.DrawString(FontManager.SteamFont, "\n Strength:", new Vector2(pos.X - 385, pos.Y - 15), colorstr);
                spriteBatch.DrawString(FontManager.SteamFont, "\n\n Agility:", new Vector2(pos.X - 385, pos.Y - 15), coloragili);
                spriteBatch.DrawString(FontManager.SteamFont, "\n\n\n Vitality:", new Vector2(pos.X - 385, pos.Y - 15), colorvitality);
                spriteBatch.DrawString(FontManager.SteamFont, "\n\n\n\n luck:", new Vector2(pos.X - 385, pos.Y - 15), colorluck);


                spriteBatch.DrawString(FontManager.SteamFont, "\t\t" + intelligence
                                                             + "\n\t\t" + strength
                                                             + "\n\t\t" + agility
                                                             + "\n\t\t" + vitality
                                                             + "\n\t\t" + luck, new Vector2(pos.X - 385, pos.Y - 15), Color.Black);




                spriteBatch.DrawString(FontManager.SteamFont, "\n\n\n\n\n\n Points:" + points, new Vector2(pos.X - 385, pos.Y - 15), Color.Black);
                spriteBatch.DrawString(FontManager.SteamFont, "\n\n\n\n\n\n\n Money: " + money, new Vector2(pos.X - 385, pos.Y - 15), Color.Black);
            }
            
        }

        public void GenerateHealthBar(double CurrentHp, double MaxHp, SpriteBatch spriteBatch)
        {
            Double Percent = (Double)CurrentHp / MaxHp;
            spriteBatch.Draw(TextureManager.hpTexture, new Vector2(pos.X - 340, pos.Y - 80), new Rectangle(0, 0, (int)(Percent * 150), 15), Color.White);
            spriteBatch.DrawString(FontManager.font, (int)CurrentHp + "/" + MaxHp, new Vector2(pos.X - 300, pos.Y - 86), Color.White);
            spriteBatch.DrawString(FontManager.SteamFont, " HP: ", new Vector2(pos.X - 385, pos.Y - 85), Color.Black);
        }
        public void GenerateManaBar(double CurrentMana, double MaxMana, SpriteBatch spriteBatch)
        {
            Double Percent = (Double)CurrentMana / MaxMana;
            spriteBatch.Draw(TextureManager.manaTexture, new Vector2(pos.X - 340, pos.Y - 60), new Rectangle(0, 0, (int)(Percent * 150), 15), Color.White);
            spriteBatch.DrawString(FontManager.font, (int)CurrentMana + "/" + MaxMana, new Vector2(pos.X - 300, pos.Y - 66), Color.White);
            spriteBatch.DrawString(FontManager.SteamFont, " MP: ", new Vector2(pos.X - 385, pos.Y - 65), Color.Black);
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
