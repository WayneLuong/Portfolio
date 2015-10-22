using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceInvader
{
    class Missile
    {
        //Missile variables
        Vector2 Position;
        //Variables for each class 
        public Missile(int XInitialPos, int YInitialPos)
        {
            Position = new Vector2(XInitialPos, YInitialPos);
        }
        public void Move()
        {
            Position.Y = Position.Y - 5;
        }
        public void Move2()
        {
            Position.Y = Position.Y - 100;
        }
        public void Move3()
        {
            Position.Y = Position.Y - 7;
        }
        public Vector2 GetPosition()
        {
            return Position;
        }
    }
}
