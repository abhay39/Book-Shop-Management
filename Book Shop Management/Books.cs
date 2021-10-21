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
    public partial class Books : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\C#\gui\Book Shop Management\BookShopDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string Query = "select * from BookTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void filter()
        {
            Con.Open();
            string Query = "select * from BookTbl where BCat ='"+ Catcbserch.SelectedItem.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        public Books()
        {
            InitializeComponent();
            populate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Users u = new Users();
            u.Show();
            this.Hide();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if(BAuthtb.Text=="" || BTitletb.Text == "" || BCat.SelectedIndex == -1 || BQtytb.Text == "" || BPricetb.Text == "")
            {
                MessageBox.Show("Missing Informations.");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into BookTbl values('"+BTitletb.Text+"','"+BAuthtb.Text+"','"+BCat.SelectedItem.ToString()+"',"+BQtytb.Text+","+BPricetb.Text+")";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Booked Saved Successfully.");
                    Con.Close();
                    Reset();
                    populate();
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Catcbserch_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void refreshbtn_Click(object sender, EventArgs e)
        {
            populate();
            Catcbserch.SelectedIndex = -1;
        }
        private void Reset()
        {
            BTitletb.Text = "";
            BAuthtb.Text = "";
            BCat.SelectedIndex = -1;
            BQtytb.Text = "";
            BPricetb.Text = "";
        }

        private void resetbtn_Click(object sender, EventArgs e)
        {
            Reset();
            BCat.SelectedIndex = -1;
        }
        int key = 0;

        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BTitletb.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
            BAuthtb.Text = BookDGV.SelectedRows[0].Cells[2].Value.ToString();
            BCat.SelectedItem = BookDGV.SelectedRows[0].Cells[3].Value.ToString();
            BQtytb.Text = BookDGV.SelectedRows[0].Cells[4].Value.ToString();
            BPricetb.Text = BookDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (BTitletb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        

        private void label4_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Hide();
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
                    string Query = "delete from BookTbl where BId = "+key+";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Booked Deleted Successfully.");
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

        private void editbtn_Click(object sender, EventArgs e)
        {
            if (BAuthtb.Text == "" || BTitletb.Text == "" || BCat.SelectedIndex == -1 || BQtytb.Text == "" || BPricetb.Text == "")
            {
                MessageBox.Show("Missing Informations.");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "update BookTbl set BTitle = '" + BTitletb.Text + "', BAuthor='"+BAuthtb.Text+"', BCat='"+BCat.SelectedItem.ToString()+"', BQty="+BQtytb.Text+",BPrice="+BPricetb.Text+" where BId="+key+";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Booked Updated Successfully.");
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
    }
}
