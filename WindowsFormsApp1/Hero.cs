using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp1
{
    class Hero
    {
        //Create brushes//
        public SolidBrush boxBrush = new SolidBrush(Color.Blue);
        public SolidBrush netBrush = new SolidBrush(Color.Yellow); 
        public int x, y, size; // Declare variables

        // Set each variable to be intertwined with the class//
        public Hero(int _x, int _y, int _size) 
        {
            x = _x;
            y = _y;
            size = _size;
        }
        public Hero(SolidBrush _boxBrush, int _x, int _y, int _size)
        {
            boxBrush = _boxBrush;
            x = _x;
            y = _y;
            size = _size;
        }
        public void Move(string direction)
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
