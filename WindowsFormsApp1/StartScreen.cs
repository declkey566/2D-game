using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media; //allows me to play sounds

namespace WindowsFormsApp1
{
    public partial class StartScreen : UserControl
    {
        SoundPlayer player2 = new SoundPlayer(Properties.Resources.waltz);
        public StartScreen()
        {
            InitializeComponent();
            player2.Play(); //plays music throughout the start screen
        }

        private void StartButton_Click(object sender, EventArgs e) //when start button is clicked
        {
            Form f = this.FindForm();
            f.Controls.Remove(this); //remove start screen
            player2.Stop(); // stops playing start screen music
            MainScreen ms = new MainScreen();
            f.Controls.Add(ms); //pull up mainscreen
           

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
