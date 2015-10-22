using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceInvader
{
    class BossAlien
    {
        //Boss Alien variables
        Vector2 BossAlienPos;
        //Variables for each class 
        public BossAlien()
        {
            BossAlienPos = new Vector2();
            BossAlienPos.X = 0;
            BossAlienPos.Y = 0;
        }
        public void MoveHorizontal(int amount)
        {
            BossAlienPos.X = BossAlienPos.X + amount;
        }
        public void MoveVertical(int amount)
        {
            BossAlienPos.Y = BossAlienPos.Y + amount;
        }
        public void SetXPos(int pos)
        {
            BossAlienPos.X = pos;
        }
        public int GetXPos()
        {
            return (int)BossAlienPos.X;
        }
        public void SetYPos(int pos)
        {
            BossAlienPos.Y = pos;
        }
        public int GetYPos()
        {
            return (int)BossAlienPos.Y;
        }

        public Vector2 GetPos()
        {
            return BossAlienPos;
        }
    }
}
