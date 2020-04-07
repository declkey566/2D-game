using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp1
{
    class Bugs
    {
        //Create brushes//
        public SolidBrush bugBrush = new SolidBrush(Color.Black);
        public SolidBrush bonusBrush = new SolidBrush(Color.Black);
        //Declare variables//
        public int x, y, size;

        public Bugs (int _x, int _y, int _size) //Intertwine variables into the class
        {
            x = _x;
            y = _y;
            size = _size;
        }
        public void Move (string direction)
        {
            if (direction == "left") // when direction is set to left, x value is decreased by 5
            {
                x = x - 5;
            }
            if (direction == "right") // when direction is set to right, x value is increased by 5
            {
                x = x + 5;
            }
        }


    }
}
