using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace UnifiedPost.Forme
{
    public partial class budget : Form
    {
        int bgt;
        string con_string = "datasource= " + Properties.Settings.Default.hostname + "; port=" + Properties.Settings.Default.port + ";username=" + Properties.Settings.Default.uname + ";password=" + Properties.Settings.Default.pass;
        string database = Properties.Settings.Default.datasrc;
        public budget(int budget)
        {
            InitializeComponent();
            bgt = budget;
            label2.Text = bgt.ToString();
        }

        private void budget_Load(object sender, EventArgs e)
        {
            string cmd_string;
            cmd_string = "SELECT * FROM " + database + ".products ORDER BY avg_vote DESC";
            MySqlConnection con = new MySqlConnection(con_string);
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd_string, con);
            cmd.Connection = con;
            cmd.CommandText = cmd_string;
            con.Open();
            DataSet ds = new DataSet();
            adp.Fill(ds, "products");
            foreach (DataRow r in ds.Tables["products"].Rows)
            {
                listBox1.Items.Add(r["product"].ToString());
            }
       }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bgt > 0)
            {
                listBox2.Items.Add(listBox1.SelectedItem);
                string cmd_string;
                cmd_string = "SELECT price FROM " + database + ".products WHERE product='" + listBox1.SelectedItem.ToString() + "'";
                MySqlConnection con = new MySqlConnection(con_string);
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = cmd_string;
                con.Open();
                bgt = bgt - Int32.Parse(cmd.ExecuteScalar().ToString());
                if (bgt < 0) bgt = 0;
                con.Close();
                label2.Text = bgt.ToString();
            }
            else
            {
                MessageBox.Show("You don't have sufficient founds!");
            }
            

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
