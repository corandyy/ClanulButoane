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
    public partial class users : Form
    {
        int x1 = 0, x2 = 0, x3 = 0, x4 = 0, mousex, mousey;
        bool drag;
        string con_string = "datasource= " + Properties.Settings.Default.hostname + "; port=" + Properties.Settings.Default.port + ";username=" + Properties.Settings.Default.uname + ";password=" + Properties.Settings.Default.pass;
        string database = Properties.Settings.Default.datasrc;
        public users()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
        }
        void gen_lbl1(string nume, string text)
        {


            Label lbl = new Label();
            lbl.Text = text;
            lbl.Name = nume;
            lbl.TabIndex = 0;
            lbl.AutoSize = true;
            lbl.Location = new Point(label1.Location.X, 30 + label1.Location.Y + x1);
            x1 += 30;
            lbl.Visible = true;
            panel1.Controls.Add(lbl);
            lbl.Click += new EventHandler(lbl_Click);
            lbl.Cursor = Cursors.Hand;

        }
        void gen_lbl2(string nume, string text)
        {


            Label lbl = new Label();
            lbl.Text = text;
            lbl.Name = nume;
            lbl.TabIndex = 0;
            lbl.AutoSize = true;
            lbl.Location = new Point(label2.Location.X, 30 + label2.Location.Y + x2);
            x2 += 30;
            lbl.Visible = true;
            panel1.Controls.Add(lbl);

        }
        void gen_lbl3(string nume, string text)
        {


            Label lbl = new Label();
            lbl.Text = text;
            lbl.Name = nume;
            lbl.TabIndex = 0;
            lbl.AutoSize = true;
            lbl.Location = new Point(label3.Location.X, 30 + label3.Location.Y + x3);
            x3 += 30;
            lbl.Visible = true;
            panel1.Controls.Add(lbl);


        }
        void gen_lbl4(string nume, string text)
        {


            Label lbl = new Label();
            lbl.Text = text;
            lbl.Name = nume;
            lbl.TabIndex = 0;
            lbl.AutoSize = true;
            lbl.Location = new Point(label4.Location.X, 30 + label4.Location.Y + x4);
            x4 += 30;
            lbl.Visible = true;
            panel1.Controls.Add(lbl);

        }

        void lbl_Click(object sender, EventArgs e)
        {
            subuser su = new subuser(((Label)sender).Name);
            su.ShowDialog();
            users_Load(sender, e);

        }

        private void users_Load(object sender, EventArgs e)
        {

            panel1.Controls.Clear();
            x1 = 0; x2 = 0; x3 = 0; x4 = 0;
            string query = "SELECT * FROM ";
            query += database;
            query += ".user";
            MySqlConnection cn = new MySqlConnection(con_string);
            label10.Text = query;
            cn.Open();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand(query, cn);
            MySqlDataAdapter adp = new MySqlDataAdapter(query, cn);
            adp.Fill(ds, "user");
            foreach (DataRow s in ds.Tables["user"].Rows)
            {
                gen_lbl1(s["username"].ToString(), s["username"].ToString());
                gen_lbl2(s["group"].ToString(), s["group"].ToString());
                gen_lbl3(s["admin"].ToString(), s["admin"].ToString());
                gen_lbl4(s["votepoints"].ToString(), s["votepoints"].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "") users_Load(sender, e);
            else
            {

                panel1.Controls.Clear();
                x1 = 0; x2 = 0; x3 = 0; x4 = 0;
                string query = "SELECT * FROM ";
                query += database;
                query += ".user where username='" + textBox1.Text + "'";
                MySqlConnection cn = new MySqlConnection(con_string);
                label10.Text = query;
                cn.Open();
                DataSet ds = new DataSet();
                MySqlCommand cmd = new MySqlCommand(query, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(query, cn);
                adp.Fill(ds, "user");
                foreach (DataRow s in ds.Tables["user"].Rows)
                {
                    gen_lbl1(s["username"].ToString(), s["username"].ToString());
                    gen_lbl2(s["group"].ToString(), s["group"].ToString());
                    gen_lbl3(s["admin"].ToString(), s["admin"].ToString());
                    gen_lbl4(s["votepoints"].ToString(), s["votepoints"].ToString());
                }
            }
        }
    }
}
