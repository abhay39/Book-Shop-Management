using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Book_Shop_Management
{
    public partial class Splash : Form
    {
        int startpoint = 0;
        public Splash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint += 1;
            myprogreess.Value = startpoint;
            percenaglbl.Text = startpoint + "%";
            if (myprogreess.Value == 100)
            {
                myprogreess.Value = 0;
                timer1.Stop();
                Login l = new Login();
                l.Show();
                this.Hide();
            }
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
