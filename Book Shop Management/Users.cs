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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            populate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Books u = new Books();
            u.Show();
            this.Hide();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\C#\gui\Book Shop Management\BookShopDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string Query = "select * from UserTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UserGDv.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            Unametb.Text = "";
            Upasstb.Text = "";
            Uphonetb.Text = "";
            Uaddresstb.Text = "";
        }
        private void savebtn_Click(object sender, EventArgs e)
        {
            if (Unametb.Text == "" || Uphonetb.Text == "" || Uaddresstb.Text == "" || Upasstb.Text == "")
            {
                MessageBox.Show("Missing Informations.");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into UserTbl values('" + Unametb.Text + "','" + Uphonetb.Text + "','" + Uaddresstb.Text + "','" + Upasstb.Text + "')";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Added Successfully.");
                    Con.Close();
                    Reset();
                    populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        int key = 0;

       
        private void edittbn_Click(object sender, EventArgs e)
        {
            if (Unametb.Text == "" || Uphonetb.Text == "" || Uaddresstb.Text == "" || Upasstb.Text == "")
            {
                MessageBox.Show("Missing Informations.");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "update UserTbl set UName = '" + Unametb.Text + "', UPhone='" + Uphonetb.Text + "', UAddress='" + Uaddresstb.Text + "', UPass='" + Upasstb.Text + "' where UId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated Successfully.");
                    Con.Close();
                    Reset();
                    populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void resetbtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Missing Informations.");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "delete from UserTbl where UId = " + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted Successfully.");
                    Con.Close();
                    Reset();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void UserGDv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Unametb.Text = UserGDv.SelectedRows[0].Cells[1].Value.ToString();
            Uphonetb.Text = UserGDv.SelectedRows[0].Cells[2].Value.ToString();
            Uaddresstb.Text = UserGDv.SelectedRows[0].Cells[3].Value.ToString();
            Upasstb.Text = UserGDv.SelectedRows[0].Cells[4].Value.ToString();
            if (Unametb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(UserGDv.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
