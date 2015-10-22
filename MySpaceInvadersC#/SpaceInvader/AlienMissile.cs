using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceInvader
{
    //Alien Missile variables
    class AlienMissile
    {
        Vector2 Position;
        //Variables for each class 
        public AlienMissile(int XInitialPos, int YInitialPos)
        {
            Position = new Vector2(XInitialPos, YInitialPos);
        }
        public void Move()
        {
            Position.Y = Position.Y + 7;
        }
        public Vector2 GetPosition()
        {
            return Position;
        }
    }
}
