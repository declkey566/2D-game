using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class EndScreen : UserControl
    {
        public EndScreen()
        {
            InitializeComponent();
            label1.Text = "THANKS FOR PLAYING! YOUR SCORE WAS " + Convert.ToString(MainScreen.finalScore); // Congratulate the player and display their final score!
        }
    }
}
