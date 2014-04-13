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
    public partial class settings : Form
    {
        bool drag;
        int mousex, mousey;
        bool a;
        string con_string = "datasource= " + Properties.Settings.Default.hostname + "; port=" + Properties.Settings.Default.port + ";username=" + Properties.Settings.Default.uname + ";password=" + Properties.Settings.Default.pass;
        string database = Properties.Settings.Default.datasrc;

        public settings(bool admin)
        {
            InitializeComponent();
            a = admin;
            if (admin == true) groupBox3.Enabled = true;
            else groupBox3.Enabled = false;
            label3.Text += " - " + this.Text;

        }

        private void settings_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            textBox1.Text = Properties.Settings.Default.hostname;
            textBox2.Text = Properties.Settings.Default.port.ToString();
            textBox3.Text = Properties.Settings.Default.datasrc;
            textBox4.Text = Properties.Settings.Default.uname;
            textBox10.Text = Properties.Settings.Default.welcomemsg;
            checkBox1.Checked = Properties.Settings.Default.autoclr;
            MySql.Data.MySqlClient.MySqlConnection cn = new MySqlConnection(con_string);
            
            try
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT message FROM " + database + ".wm", cn);
                cmd.ExecuteNonQuery();
                textBox6.Text = cmd.ExecuteScalar().ToString();
                cn.Close();
            }
            catch
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.welcomemsg = textBox10.Text;
                Properties.Settings.Default.hostname = textBox1.Text;
                Properties.Settings.Default.port = Int32.Parse(textBox2.Text);
                Properties.Settings.Default.uname = textBox4.Text;
                Properties.Settings.Default.datasrc = textBox3.Text;
                Properties.Settings.Default.autoclr = Convert.ToBoolean(checkBox1.CheckState);
                Properties.Settings.Default.Save();
                if (a == true)
                {
                    try
                    {
                        MySqlConnection cn = new MySqlConnection(con_string);
                        MySqlCommand cmd = new MySqlCommand("UPDATE " + database + ".wm set message='" + textBox6.Text + "'", cn);
                        cn.Open();
                        cmd.ExecuteNonQuery();
                        cn.Close();

                    }
                    catch { }
                }
                MessageBox.Show("Saved!!");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Incorrect settings!! Please recheck !!");
            }

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

        private void button3_Click(object sender, EventArgs e)
        {
            string con_string1 = "datasource= " + textBox1.Text + "; port=" + textBox2.Text + ";username=" + textBox4.Text + ";password=" + textBox5.Text ;
            MySqlConnection cn = new MySqlConnection(con_string1);
            try
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM "+ textBox3.Text + ".user",cn);
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Everythings OK!!");
            }
            catch
            {
                MessageBox.Show("Error!! Connection setting invalid !!");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                MySqlConnection cn = new MySqlConnection(con_string);
                cmd.Connection = cn;
                cn.Open();
                string query = "UPDATE ";
                query += database;
                query += ".products set sum_vote=0 , nr_vote=0 , avg_vote=5 , vp=0";
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                query = "UPDATE " + database + ".user set votepoints=5";
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Done!!");
                this.Close();
            }
            catch
            {
                MessageBox.Show("I can't reset votes !!");
            }
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string querry = "TRUNCATE TABLE products";
                MySqlCommand cmd = new MySqlCommand();
                MySqlConnection cn = new MySqlConnection(con_string);
                cmd.Connection = cn;
                cn.Open();
                cmd.CommandText = querry;
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Done!!");
            }
            catch
            {
                MessageBox.Show("I can't wipe you all data!!");
            }

        }
    }
}
