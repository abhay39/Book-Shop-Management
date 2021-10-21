using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Book_Shop_Management
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\C#\gui\Book Shop Management\BookShopDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (usertb.Text == "" || passtb.Text == "")
            {
                MessageBox.Show("Missing Information.");
            }
            else
            {
                Con.Open();

                Con.Close();
            }
        }
    }
}
