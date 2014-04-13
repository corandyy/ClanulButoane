using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ExcelLibrary.CompoundDocumentFormat;
using ExcelLibrary.SpreadSheet;
using System.Runtime.InteropServices;
using System.Reflection;
 

namespace UnifiedPost.Forme
{
    public partial class administrator : Form
    {
        string con_string = "datasource= " + Properties.Settings.Default.hostname + "; port=" + Properties.Settings.Default.port + ";username=" + Properties.Settings.Default.uname + ";password=" + Properties.Settings.Default.pass;
        string database = Properties.Settings.Default.datasrc;
        bool drag;
        int mousex, mousey;
        void grupe()
        {
            string cmd_string = "SELECT * FROM " + database + ".user";
            MySqlConnection con = new MySqlConnection(con_string);
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd_string, con);
            cmd.Connection = con;
            cmd.CommandText = cmd_string;
            con.Open();
            DataSet ds = new DataSet();
            adp.Fill(ds, "user");
            comboBox4.Items.Clear();
            foreach (DataRow s in ds.Tables["user"].Rows)
            {
                if (!comboBox4.Items.Contains(s["group"].ToString())) comboBox4.Items.Add(s["group"].ToString());
            }
        }
        public administrator()
        {
            InitializeComponent();
            grupe();
        }

       
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            mousex = Cursor.Position.X - this.Left;
            mousey = Cursor.Position.Y - this.Top;
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag == true)
            {
                this.Top = Cursor.Position.Y - mousey;
                this.Left = Cursor.Position.X - mousex;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void administrator_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            label9.Text = "";
            label10.Text = "";
            label11.Text = "";
            label13.Text = "";
            label14.Text = "";
            label15.Text = "";
            label25.Text = "-";
            textBox1_TextChanged(sender, e);
            comboBox1.SelectedIndex = 0;
            combocat();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            showtop(); 
        }
        private void showtop()
        {
            try
            {
            string cmd_string;
            if (radioButton1.Checked == true) cmd_string = "SELECT * FROM " + database + ".products ORDER BY avg_vote DESC LIMIT " + textBox1.Text;
            else cmd_string = "SELECT * FROM " + database + ".products ORDER BY avg_vote ASC LIMIT " + textBox1.Text;
            MySqlConnection con = new MySqlConnection(con_string);
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd_string, con);
            cmd.Connection = con;
            cmd.CommandText = cmd_string;
            con.Open();
            DataSet ds = new DataSet();
            adp.Fill(ds, "products");
            label9.Text = "";
            label10.Text = "";
            label11.Text = "";
            label13.Text = "";
            label14.Text = "";
            label15.Text = "";
            foreach (DataRow s in ds.Tables["products"].Rows)
            {
                label9.Text += s["id"].ToString() + "\n";
                label10.Text += s["product"].ToString() + "\n";
                label11.Text += (double.Parse(s["price"].ToString())).ToString("0.00") + "\n";
                label13.Text += s["category"].ToString() + "\n";
                label14.Text += s["nr_vote"].ToString() + "\n";
                label15.Text += s["avg_vote"].ToString() + "\n";
            }
            con.Close();
            }
            catch
            {
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1_TextChanged(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                    string cmd_string = "INSERT INTO " + database + ".products(`product`, `category`) VALUES('" + textBox2.Text + "', '" + comboBox1.SelectedItem + "')";
                    MySqlConnection con = new MySqlConnection(con_string);
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = cmd_string;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    textBox2.Clear();
                    comboprod();
                    combocat();
                    showtop();
                    comboBox2.Text = comboBox1.Text;
                    comboBox3.Text = comboBox1.Text;
                    textBox4.Focus();
                    MessageBox.Show("Added!");

            }
            catch
            {
            }
        }

        private void combocat()
        {
            string cmd_string = "SELECT * FROM " + database + ".products";
            MySqlConnection con = new MySqlConnection(con_string);
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd_string, con);
            cmd.Connection = con;
            cmd.CommandText = cmd_string;
            con.Open();
            DataSet ds = new DataSet();
            adp.Fill(ds, "products");
            comboBox2.Items.Clear();
            foreach (DataRow s in ds.Tables["products"].Rows)
            {
                if (!comboBox2.Items.Contains(s["category"].ToString())) comboBox2.Items.Add(s["category"].ToString());
            }
            try
            {
                comboBox2.SelectedIndex = 0;
                if (comboBox2.SelectedIndex == 0)
                {
                    comboBox3.Enabled = true;
                    button3.Enabled = true;
                    label22.ForeColor = Color.Black;
                    comboprod();
                }
            }
            catch
            {
            }
            con.Close();
        }
        private void comboprod()
        {
            string cmd_string = "SELECT * FROM " + database + ".products WHERE `category`='" + comboBox2.SelectedItem.ToString() + "'";
            MySqlConnection con = new MySqlConnection(con_string);
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd_string, con);
            cmd.Connection = con;
            cmd.CommandText = cmd_string;
            con.Open();
            DataSet ds = new DataSet();
            adp.Fill(ds, "products");
            comboBox3.Items.Clear();
            foreach (DataRow s in ds.Tables["products"].Rows)
            {
               comboBox3.Items.Add(s["product"].ToString());
            }
            try
            {
                comboBox3.SelectedIndex = 0;
                cmd_string = "SELECT price FROM " + database + ".products WHERE `product`='" + comboBox3.SelectedItem + "'";
                cmd.CommandText = cmd_string;
                double price = double.Parse(cmd.ExecuteScalar().ToString());
                label25.Text = price.ToString("0.00");
                if (comboBox3.Items.Count != 0) button3.Enabled = true;
            }
            catch
            {
            }
            con.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection con = new MySqlConnection(con_string);
                MySqlCommand cmd = new MySqlCommand();
                string cmd_string = "SELECT price FROM " + database + ".products WHERE `product`='" + comboBox3.SelectedItem + "'";
                cmd.CommandText = cmd_string;
                cmd.Connection = con;
                con.Open();
                double price = double.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
                label25.Text = price.ToString("0.00");
                comboprod();
            }
            catch
            {
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection con = new MySqlConnection(con_string);
                MySqlCommand cmd = new MySqlCommand();
                string cmd_string = "SELECT price FROM " + database + ".products WHERE `product`='" + comboBox3.SelectedItem + "'";
                cmd.CommandText = cmd_string;
                cmd.Connection = con;
                con.Open();
                double price = double.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
                label25.Text = price.ToString("0.00");
                if(comboBox3.Items.Count != 0) button3.Enabled = true;
            }
            catch
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection con = new MySqlConnection(con_string);
                MySqlCommand cmd = new MySqlCommand();
                string cmd_string = "UPDATE " + database + ".products SET `price`='" + (double.Parse(textBox4.Text)).ToString("0.00") + "' WHERE `product`='" + comboBox3.SelectedItem + "'";
                cmd.CommandText = cmd_string;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                label25.Text = (double.Parse(textBox4.Text)).ToString("0.00");
                textBox4.Clear();
                showtop();
                MessageBox.Show("Changed!");
            }
            catch
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string qr = "INSERT INTO ";
            qr += database;
            qr+=".user(`username`, `parola`, `admin` , `group`, `votepoints`) values('" + tbuname.Text + "','" + tbpass.Text + "','0','" + comboBox4.Text + "','5')";
            MySqlConnection cn = new MySqlConnection(con_string);
            cn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = qr;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();
            cn.Close();
            tbpass.Text = "";
            tbpass.Text = "";
            MessageBox.Show("Added!");



        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            users usr = new users();
            usr.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            chartstatistics cs = new chartstatistics();
            cs.WindowState = FormWindowState.Maximized;
            cs.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(con_string);
            MySqlCommand cmd = new MySqlCommand();
            string cmd_string = "DELETE FROM " + database + ".products WHERE `product`='" + comboBox3.SelectedItem.ToString() + "'";
            cmd.CommandText = cmd_string;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            comboprod();
            combocat();
        }
       
        void conversie()
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string cmd_string = "SELECT product,category,avg_vote FROM " + database + ".products order by avg_vote DESC";
                MySqlConnection con = new MySqlConnection(con_string);
                MySqlCommand cmd = new MySqlCommand(cmd_string, con);
                MySqlDataAdapter adp = new MySqlDataAdapter();
                DataTable dbdataset = new DataTable();
                adp.SelectCommand = cmd;
                //adp.Fill(dbdataset);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dbdataset;
                adp.Update(dbdataset);
                DataSet ds = new DataSet("products");
                ds.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                adp.Fill(dbdataset);
                ds.Tables.Add(dbdataset);
                ExcelLibrary.DataSetHelper.CreateWorkbook("Raport_date_"+ DateTime.Now.ToString("dd-MM-yyyy")+".xls", ds);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void button8_Click(object sender, EventArgs e)
        {
            budget bgt = new budget(Int32.Parse(textBox3.Text));
            bgt.ShowDialog();
        }

    }
}
