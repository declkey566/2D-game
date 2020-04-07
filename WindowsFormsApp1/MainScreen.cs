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
    public partial class MainScreen : UserControl
    {
      // VARIABLE DECLARATION //
        Hero hero;
        int counter = 0; 
        int bonusCounter = 0; 
        int timer = 0; 
        int timerS = 60; 
        public static int finalScore;
        int nety;
        public int bugScore = 0;
        int bonustime = 0;
        int netx;
        int netsize = 30;
        Random randGen = new Random();
        int netsize2 = 5;
        int bugHeightL = 10; 
        int bugHeightR = 100; 
        int newBugCounter = 0;
        List<Bugs> bugsL = new List<Bugs>();
        List<Bugs> bugsR = new List<Bugs>();
        List<Bugs> bugsBonusList = new List<Bugs>();
        SolidBrush boxBrush = new SolidBrush(Color.Black);
        Boolean leftArrowDown, rightArrowDown, spaceDown;
        SoundPlayer player = new SoundPlayer(Properties.Resources.pop);
        ////////////////////////////////////////////////////

        public MainScreen()
        {
            InitializeComponent();
            OnStart();

        }

        public void OnStart()
        {
            
            hero = new Hero(350, 358, 10); //set initial values for hero and bugs
            Bugs r = new Bugs(0, bugHeightR, 2);
            Bugs l = new Bugs(400, bugHeightL, 2);
            Bugs bonus = new Bugs(400, bugHeightL, 5);
            bugsL.Add(l); // create lists for bug types 
            bugsR.Add(r);
            newBugCounter++;
            label3.Text = "Bugs caught:" + Convert.ToString(bugScore); //display number of bugs caught on screen
            BackColor.Equals(Color.AntiqueWhite); // set background colour
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player button presses//
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Space:
                    spaceDown = true;
                    break;

            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player button releases//
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Space:
                    spaceDown = false;
                    break;
                   
            }
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {

        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            foreach (Bugs l in bugsL) // for the number of bugs in the left side list
            {
                l.Move("left"); // move the bugs left
            }
            foreach (Bugs r in bugsR) // for the number of bugs in the right side list
            {
                r.Move("right"); // move the bugs right
            }
            foreach(Bugs bonus in bugsBonusList ) // for the number of bugs in the bonus list
            {
                bonus.Move("right"); //move the bugs right
            }

            // Deletes any bugs that move off screen //
            if (bugsL[0].x <0)
            {
                bugsL.RemoveAt(0);
            }
            if (bugsR[0].x > 400)
            {
                bugsR.RemoveAt(0);
            }
            
            counter++;
            timer++;
            
            if (timer ==30) // when the timer reaches 30 (30 game loops)
                {
                timerS = timerS - 1; // timer for the game goes down 1
                timer = 0; // reset timer 
                if (timerS == 0) // when the game timer reches 0, the game ends
                {
                    finalScore = bugScore; // produce final score
                    gameLoop.Enabled = false; // end game loop
                    Form f = this.FindForm();
                    f.Controls.Remove(this); // delete this screen
                    EndScreen es = new EndScreen(); 
                    f.Controls.Add(es); //open end screen
                }

               
            }
            label1.Text = "Time:" + Convert.ToString(timerS) + " s"; // display time left in game
            if (counter ==7) // when the game counter reaches 7
            {
                //create random spawn point for new bugs and add them to the left and right bug lists//
                bugHeightR = randGen.Next(1, 300); 
                bugHeightL = randGen.Next(1, 300);
                newBugCounter++;
                Bugs bugsLadd = new Bugs(400, bugHeightL,2);
                Bugs bugsRadd = new Bugs(0, bugHeightR,2);
                
                bugsL.Add(bugsLadd);
                bugsR.Add(bugsRadd);
               
                counter = 0; //reset counter
            }
            bonusCounter++;
            if (bonusCounter == 60) // when the bonus counter reaches 60, spawn a bonus bug
            {
                Bugs bugsBonusadd = new Bugs(0, bugHeightL, 5);
                bugsBonusList.Add(bugsBonusadd);
                bonusCounter = 0; //reset bonus counter

            }



            // HERO MOVEMENT //
            if (leftArrowDown)
            {
                hero.Move("left"); // move left
                
                if (hero.x < 0) // does not allow hero do go off screen
                {
                    hero.x = hero.x + 5;
                }
            }
            if (rightArrowDown)
            {
                hero.Move("right"); // move right
                
                if (hero.x >this.Width-10) // does not allow hero to go off screen
                {
                    hero.x = hero.x - 5; 
                }
            }
            if (spaceDown)
            {
                // Keeps bug net centred on the hero //
                if (netx < hero.x - 10)
                {
                    netx = netx + 4;
                }
                if (netx > hero.x - 10)
                {
                    netx = netx - 4;
                }
                // Net will continue to move down unless the hero and net have the same y position //
                if (nety < hero.y)
                {
                    nety = nety + 4;
                }
            }

            Rectangle heroRec = new Rectangle(hero.x, hero.y, hero.size, hero.size); // create new objects for the hero and net //
            Rectangle netRec = new Rectangle(netx, nety, netsize, netsize2);
            foreach (Bugs bug in bugsL) //for the number of bugs in the left list, create object for each bug
            {
                Rectangle bugRec = new Rectangle(bug.x, bug.y, bug.size, bug.size);
                if (netRec.IntersectsWith(bugRec)) // If the net collides with any bug object...
                {
                    bugScore = bugScore + 1; // score increases
                    player.Play(); //plays music when bug is caught
                    label3.Text = "Bugs caught:" + Convert.ToString(bugScore); // display bugs caught
                    int index = bugsL.IndexOf(bug); // find the location of this bug in the list
                    bugsL.RemoveAt(index); // delete the caught bug
                    break;
                   
                }
            }
            foreach (Bugs bug in bugsR) // Same process as the above code, except this time for bugs in the right list
            {
                Rectangle bugRec = new Rectangle(bug.x, bug.y, bug.size, bug.size);
                if (netRec.IntersectsWith(bugRec))
                {
                    bugScore = bugScore + 1;
                    player.Play();
                    label3.Text = "Bugs caught:" + Convert.ToString(bugScore);
                    int index = bugsR.IndexOf(bug);
                    bugsR.RemoveAt(index);
                    break;
                   
                }
            }
            foreach (Bugs bug in bugsBonusList) // for the number of bugs in the bonus bug list...
            {
                Rectangle bugRec = new Rectangle(bug.x, bug.y, bug.size+3, bug.size+3); // create a larger object for each bonus bug (noticably a bonus)
                if (netRec.IntersectsWith(bugRec)) // if the net collides with a bonus bug...
                {
                    
                    bugScore = bugScore + 20; // Score increases by 20 (lots more than a regular bug)
                    player.Play();//plays music when bug is caught
                    timerS = timerS + 2; // Game timer is increased (you are awarded with bonus playtime)
                    netsize = netsize + 50; // Size of the net increases 
                    bonustime = 0; // bonus time is reset
                    label3.Text = "Bugs caught:" + Convert.ToString(bugScore); //display and update bug score
                    int index = bugsBonusList.IndexOf(bug); //find bonus bugs location in list
                    bugsBonusList.RemoveAt(index); //delete bonus bug at that point
                    break; 

                }
            }

            if (netsize >30)  // If the players net size has surpassed the original size of 30...
            {
                bonustime++; // Bonus time increases each loop 
                    if (bonustime == 100) // Once bonus time reaches 100...
                {
                    netsize = 30; // Net size is decreased back to its original value
                    bonustime = 0; // Bonus time is reset
                }
            }
            if (heroRec.IntersectsWith(netRec)) // If the hero intersects with the net...
            {
                nety = 0; // Reset the nets y position 
            }
           

            Refresh();
        }


        private void MainScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(hero.boxBrush, hero.x, hero.y, hero.size, hero.size); // Display the object for the hero
            e.Graphics.FillRectangle(hero.netBrush, netx, nety, netsize, netsize2); // Display the object for the net
            // For each bug in their respective lists... //
            foreach (Bugs l in bugsL)
            {
                e.Graphics.FillRectangle(l.bugBrush, l.x, l.y, l.size, l.size); // Display left list bug objects
            }
            foreach (Bugs r in bugsR)
            { 
                e.Graphics.FillRectangle(r.bugBrush, r.x, r.y, r.size, r.size); // Display right list bug objects
            }
            foreach (Bugs bonus in bugsBonusList)
            {
                e.Graphics.FillRectangle(bonus.bonusBrush, bonus.x,bonus.y, bonus.size+3, bonus.size+3); // Display bonus bug objects
            }
        }
       


    }
}
