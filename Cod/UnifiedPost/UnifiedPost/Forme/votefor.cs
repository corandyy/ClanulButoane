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
    public partial class votefor : Form
    {
        int vpg;
        double pretcod;
        int oldhover;
        PictureBox[] pc = new PictureBox[11];
        Boolean drag = false;
        int mousex;
        int mousey;
        string product;
        string con_string = "datasource= " + Properties.Settings.Default.hostname + "; port=" + Properties.Settings.Default.port + ";username=" + Properties.Settings.Default.uname + ";password=" + Properties.Settings.Default.pass;
        string database = Properties.Settings.Default.datasrc;
        void genstelute()
        {
            int x = 0;
            
            for (int i = 1; i <= 10; i++)
            {
                pc[i] = new PictureBox();
                pc[i].Location = new Point(15+x, 85);
                pc[i].Size = new Size(25, 25);
                pc[i].Visible = true;
                pc[i].SizeMode = PictureBoxSizeMode.StretchImage;
                pc[i].BackColor = Color.Transparent;
                pc[i].Image = Properties.Resources.stea;
                x = x + 23;
                this.Controls.Add(pc[i]);
                pc[i].MouseHover += new EventHandler(votefor_MouseHover);
                pc[i].MouseLeave += new EventHandler(votefor_MouseLeave);
                pc[i].Click += new EventHandler(votefor_Click);

            }
        }

        void votefor_Click(object sender, EventArgs e)
        {
            if (vpg > pretcod)
            {
                int k = 0;
                if (sender == pc[1]) k = 1;
                else if (sender == pc[2]) k = 2;
                else if (sender == pc[3]) k = 3;
                else if (sender == pc[4]) k = 4;
                else if (sender == pc[5]) k = 5;
                else if (sender == pc[6]) k = 6;
                else if (sender == pc[7]) k = 7;
                else if (sender == pc[8]) k = 8;
                else if (sender == pc[9]) k = 9;
                else if (sender == pc[10]) k = 10;
                MySqlCommand cmd = new MySqlCommand();
                MySqlConnection cn = new MySqlConnection(con_string);
                cmd.Connection = cn;
                cn.Open();
                string query;
                query = "SELECT sum_vote FROM " + database + ".products WHERE `product`='" + product + "'";
                cmd.CommandText = query;
                int sum_vote = Int32.Parse(cmd.ExecuteScalar().ToString());
                sum_vote = sum_vote + k;
                query = "UPDATE " + database + ".products SET `sum_vote`='" + sum_vote.ToString() + "' WHERE `product`='" + product + "'";
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                query = "SELECT nr_vote FROM " + database + ".products WHERE `product`='" + product + "'";
                cmd.CommandText = query;
                int votes = Int32.Parse(cmd.ExecuteScalar().ToString());
                votes++;
                query = "UPDATE " + database + ".products SET `nr_vote`='" + votes.ToString() + "' WHERE `product`='" + product + "'";
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                double newavg = (double)(sum_vote / Double.Parse(votes.ToString()));
                query = "UPDATE " + database + ".products SET `avg_vote`='" + newavg.ToString("0.00") + "' WHERE `product`='" + product + "'";
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                query = "UPDATE " + database + " .products SET vp=avg_vote / 2";
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                string update = "UPDATE " + database + ".user set votepoints=(votepoints-" + Int16.Parse(pretcod.ToString()) + ") WHERE username='" + Welcome.usr.ToString() + "'";
                cmd.CommandText = update;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                cn.Close();
                votefor_Load(sender, e);
                MessageBox.Show("You voted for " + product + " with " + k.ToString() + " stars!!");
                this.Close();
            }
            else
                MessageBox.Show("Not enough vote points!");
        }

        void votefor_MouseLeave(object sender, EventArgs e)
        {
            hoverstars(oldhover);
        }

        void votefor_MouseHover(object sender, EventArgs e)
        {
            int k = 0;
            if (sender == pc[1]) k = 1;
            else if (sender == pc[2]) k = 2;
            else if (sender == pc[3]) k = 3;
            else if (sender == pc[4]) k = 4;
            else if (sender == pc[5]) k = 5;
            else if (sender == pc[6]) k = 6;
            else if (sender == pc[7]) k = 7;
            else if (sender == pc[8]) k = 8;
            else if (sender == pc[9]) k = 9;
            else if (sender == pc[10]) k = 10;
            hoverstars(k); 
        }

        void hoverstars(int k)
        {
            for (int i = 1; i <= k; i++)
            {
                pc[i].Image = Properties.Resources.stea_on;
            }
            try
            {
                for (int i = k + 1; i <= 10; i++)
                {
                    pc[i].Image = Properties.Resources.stea;
                }
            }
            catch (Exception)
            {
            }
        }

        public votefor(string nume_produs,int vp)
        {
            InitializeComponent();
            vpg = vp;
            label17.Text = vp.ToString() ;
            if (vp > 0)
            {
                genstelute();
            }
            else MessageBox.Show("You don't have enough votepoints!");
            product = nume_produs;
        }
       
        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            hoverstars(oldhover);
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.exit_hov;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void votefor_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            label3.Text = "UnifiedPost - Vote for " + product;
            label4.Text = product;
            string query = "SELECT nr_vote FROM " + database + ".products WHERE `product`='" + product + "'";
            string query2 = "SELECT avg_vote FROM " + database + ".products WHERE `product`='" + product + "'";
            string query3 = "SELECT price FROM " + database + ".products WHERE `product`='" + product + "'";
            string query4 = "SELECT category FROM " + database + ".products WHERE `product`='" + product + "'";
            string query5 = "SELECT vp FROM " + database + ".products WHERE `product`='" + product + "'";
            MySqlConnection cn = new MySqlConnection(con_string);
            cn.Open();
            MySqlCommand cmd = new MySqlCommand(query, cn);
            string nrvoturi = cmd.ExecuteScalar().ToString();
            MySqlCommand cmd2 = new MySqlCommand(query2, cn);
            MySqlCommand cmd3 = new MySqlCommand(query3, cn);
            MySqlCommand cmd4 = new MySqlCommand(query4, cn);
            MySqlCommand cmd5 = new MySqlCommand(query5, cn);
            pretcod = Double.Parse(cmd5.ExecuteScalar().ToString());
            string category = cmd4.ExecuteScalar().ToString();
            double price = double.Parse(cmd3.ExecuteScalar().ToString());
            double avgvoturi = Double.Parse(cmd2.ExecuteScalar().ToString());
        
            label8.Text = (string)(Int32.Parse(avgvoturi.ToString("0")) + " stars");
            label9.Text = avgvoturi.ToString("0.00");
            label7.Text = nrvoturi;
            label11.Text = price.ToString("0.00");
            label14.Text = category;
            label15.Text = pretcod.ToString();
            cn.Close();
            try
            {
                hoverstars(Int32.Parse(avgvoturi.ToString("0")));
                oldhover = Int32.Parse(avgvoturi.ToString("0"));
            }
            catch
            {
            }
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
    }
}
