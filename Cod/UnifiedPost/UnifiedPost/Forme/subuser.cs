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
    public partial class subuser : Form
    {
        int mousex, mousey;
        Boolean drag = false;
        string con_string = "datasource= " + Properties.Settings.Default.hostname + "; port=" + Properties.Settings.Default.port + ";username=" + Properties.Settings.Default.uname + ";password=" + Properties.Settings.Default.pass;
        string database = Properties.Settings.Default.datasrc;

        public subuser(string usr)
        {
            InitializeComponent();
            label1.Text = usr;
        }
        void show()
        {            
            string query = "SELECT * FROM ";
            query += database;
            query += ".user where username='" + label1.Text +"'" ;
            MySqlConnection cn = new MySqlConnection(con_string);
            cn.Open();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand(query, cn);
            MySqlDataAdapter adp = new MySqlDataAdapter(query, cn);
            adp.Fill(ds, "user");
            foreach (DataRow r in ds.Tables["user"].Rows)
            {
                label8.Text = r["parola"].ToString();
                label7.Text=r["admin"].ToString();
                label6.Text=r["votepoints"].ToString();
                label9.Text=r["group"].ToString();
            }

        }

        private void subuser_Load(object sender, EventArgs e)
        {
            show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "UPDATE ";
            query += database;
            query += ".user set admin=true where username='" + label1.Text + "'";
            MySqlConnection cn = new MySqlConnection(con_string);
            cn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = query;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();
            cn.Close();
            show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string query = "UPDATE ";
            query += database;
            query += ".user set admin=false where username='" + label1.Text + "'";
            MySqlConnection cn = new MySqlConnection(con_string);
            cn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = query;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();
            cn.Close();
            show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM ";
            query += database;
            query += ".user where username='" + label1.Text + "'";
            MySqlConnection cn = new MySqlConnection(con_string);
            cn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = query;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Done!");
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.Visible = true;
            comboBox1.Focus();
            string query = "SELECT * FROM ";
            query += database;
            query += ".user";
            MySqlConnection cn = new MySqlConnection(con_string);
            cn.Open();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand(query, cn);
            MySqlDataAdapter adp = new MySqlDataAdapter(query, cn);
            adp.Fill(ds, "user");
            foreach (DataRow r in ds.Tables["user"].Rows)
            {
                if (!comboBox1.Items.Contains(r["group"].ToString())) comboBox1.Items.Add(r["group"].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "UPDATE ";
            query += database;
            query += ".user set votepoints=votepoints+5 where username='" + label1.Text + "'";
            MySqlConnection cn = new MySqlConnection(con_string);
            cn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = query;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();
            cn.Close();
            show();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            string query = "UPDATE ";
            query += database;
            query += ".user set votepoints=votepoints-1 where username='" + label1.Text + "'";
            MySqlConnection cn = new MySqlConnection(con_string);
            cn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = query;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();
            cn.Close();
            show();

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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            textBox1.Focus();

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string query = "UPDATE ";
                query += database;
                query += ".user set parola='" + textBox1.Text + "' where username='" + label1.Text + "'" ;
                MySqlConnection cn = new MySqlConnection(con_string);
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                cn.Close();
                show();
                MessageBox.Show("Your new password is " + textBox1.Text + ".");
                textBox1.Visible = false;

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
 
            try
            {
                string query = "UPDATE " + database + ".user SET `group`='" + comboBox1.Text + "' WHERE `username`='" + label1.Text + "'";
                MySqlConnection cn = new MySqlConnection(con_string);
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                cn.Close();
                show();
                MessageBox.Show("Done!");
                comboBox1.Visible = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string query = "UPDATE " + database + ".user SET `group`='" + comboBox1.Text + "' WHERE `username`='" + label1.Text + "'";
                    MySqlConnection cn = new MySqlConnection(con_string);
                    cn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = cn;
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    show();
                    MessageBox.Show("Done!");
                    comboBox1.Visible = false;
                }
                catch (Exception p)
                {
                    MessageBox.Show(p.Message);
                }

            }
        }
    }
}
