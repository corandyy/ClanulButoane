using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace UnifiedPost
{
    public partial class Form1 : Form
    {
        Boolean drag = false;
        int mousex;
        int mousey;
        string database = Properties.Settings.Default.datasrc;
        string con_string = "datasource= " + Properties.Settings.Default.hostname + "; port=" + Properties.Settings.Default.port + ";username=" + Properties.Settings.Default.uname + ";password=" + Properties.Settings.Default.pass;

        public Form1()
        {
            InitializeComponent();
            label3.Text += "- Login";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                string query = "SELECT * FROM "+ database + ".user WHERE username='" + textBox1.Text + "'";
                MySqlConnection cn = new MySqlConnection(con_string);
                cn.Open();
                DataSet ds = new DataSet();
                MySqlCommand cmd = new MySqlCommand(query, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(query, cn);
                adp.Fill(ds, "user");
                foreach (DataRow s in ds.Tables["user"].Rows)
                {
                    if (s["parola"].ToString() == textBox2.Text)
                    {
                        Forme.Welcome frm = new Forme.Welcome(Convert.ToBoolean(s["admin"].ToString()),textBox1.Text,Int32.Parse(s["votepoints"].ToString()));
                        textBox1.Text = "";
                        textBox2.Text = "";
                        this.Hide();
                        frm.Show();
                    }
                }
                cn.Close();
            }
            catch
            {
                MessageBox.Show("Login incorect !!");
            }



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.Opacity = 0;
            timer1.Interval = 10;
            timer1.Start();
            Properties.Settings.Default.Reload();
            label4.Text = Properties.Settings.Default.autoclr.ToString() + "    " + Properties.Settings.Default.luna_ant.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
         this.Opacity = this.Opacity + 0.01;
            if (this.Opacity == 1)
            {
                timer1.Stop();
            }
        }

        private void apasa (object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1.PerformClick();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {

            pictureBox1.Image = Properties.Resources.exit_hov;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.exit_norm;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Forme.settings set = new Forme.settings(false);
            set.ShowDialog();
            Application.Restart();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.autoclr = true;
            Properties.Settings.Default.luna_ant = 3;
            Properties.Settings.Default.Save();
            label4.Text = Properties.Settings.Default.autoclr.ToString() + "    " + Properties.Settings.Default.luna_ant.ToString();

        }


    }
}
