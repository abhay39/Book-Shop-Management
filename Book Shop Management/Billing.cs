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
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\C#\gui\Book Shop Management\BookShopDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string Query = "select * from BookTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookGDV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2CirclePictureBox4_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Hide();
        }
        int key = 0, stock=0;
        private void BookGDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Bnametb.Text = BookGDV.SelectedRows[0].Cells[1].Value.ToString();
            //BQty.Text = BookGDV.SelectedRows[0].Cells[4].Value.ToString();
            prictb.Text = BookGDV.SelectedRows[0].Cells[5].Value.ToString();
            if (Bnametb.Text == "")
            {
                key = 0;
                stock = 0;
            }
            else
            {
                key = Convert.ToInt32(BookGDV.SelectedRows[0].Cells[0].Value.ToString());
                stock = Convert.ToInt32(BookGDV.SelectedRows[0].Cells[4].Value.ToString());
            }
        }

        private void resetbtn_Click(object sender, EventArgs e)
        {
            Bnametb.Text = "";
            BQty.Text = "";
            prictb.Text = "";
            Clientnametb.Text = "";
        }
        int n = 0, grand=0;

        private void printbtn_Click(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 400, 660);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
        int Prodid, prodqty, prodprice, total, pos=60;
        string prodname;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("    Abhay Book Shop", new Font("Century Gothic" ,12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("ID PRODUCT     PRICE   QUANTITY   TOTAL", new Font("Century Gothic" ,12, FontStyle.Bold), Brushes.Red, new Point(26, 40));
            foreach(DataGridViewRow row in BillGDV.Rows)
            {
                Prodid = Convert.ToInt32(row.Cells["Column1"].Value);
                prodname = ""+row.Cells["Column2"].Value;
                prodprice = Convert.ToInt32(row.Cells["Column3"].Value);
                prodqty = Convert.ToInt32(row.Cells["Column4"].Value);
                total = Convert.ToInt32(row.Cells["Column5"].Value);

                e.Graphics.DrawString(""+Prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString(""+prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("\t"+prodprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("\t"+prodqty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("  \t\t"+total, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;
            }
            e.Graphics.DrawString("Grand Total Rs. "+grand, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(60, pos+50));
            e.Graphics.DrawString("**********Abhay Book Store************", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(40, pos+85));
            BillGDV.Rows.Clear();
            BillGDV.Refresh();
            pos = 100;
            //grand = 0;
        }

        private void addtobillbtn_Click(object sender, EventArgs e)
        {
            
            if(BQty.Text=="" || Convert.ToInt32(BQty.Text) > stock){
                MessageBox.Show("No Enough Stock.");
            }
            else
            {
                int total = Convert.ToInt32(BQty.Text) * Convert.ToInt32(prictb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillGDV); ;
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = Bnametb.Text;
                newRow.Cells[2].Value = prictb.Text;
                newRow.Cells[3].Value = BQty.Text;
                newRow.Cells[4].Value = total;
                BillGDV.Rows.Add(newRow);
                n++;
                update_Book();
                grand = grand + total;
                totoallbl.Text = "Rs. " + grand;
            }
        }
        
        private void update_Book()
        {
            int newQty = stock - Convert.ToInt32(BQty.Text);
            try
            {
                Con.Open();
                string Query = "update BookTbl set BQty=" + newQty + " where BId=" + key + ";";
                SqlCommand cmd = new SqlCommand(Query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Booked Updated Successfully.");
                Con.Close();
                //Reset();
                populate();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
