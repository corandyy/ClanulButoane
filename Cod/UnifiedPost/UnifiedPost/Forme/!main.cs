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
    public partial class _main : Form
    {
        int votep;
        Boolean drag = false;
        int mousex;
        int mousey;
        int k = 1;
        bool adm;
        void excel()
        {
            try
            {
                string cmd_string = "SELECT product,category,avg_vote FROM " + database + ".products";
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        string con_string = "datasource= " + Properties.Settings.Default.hostname + "; port=" + Properties.Settings.Default.port + ";username=" + Properties.Settings.Default.uname + ";password=" + Properties.Settings.Default.pass;
        string database = Properties.Settings.Default.datasrc;
        void verif_luna()
        {
            if(Properties.Settings.Default.luna_ant!=Int32.Parse(DateTime.Now.ToString("MM")))
            {
                Properties.Settings.Default.luna_ant=Int32.Parse(DateTime.Now.ToString("MM"));
                Properties.Settings.Default.Save();
                if (Properties.Settings.Default.autoclr == true)
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
                        excel();
                        MessageBox.Show("All data was reseted and raport was generated.");
                        
                        
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }



            }
        }
        public _main(string nume,bool admin,int vote)
        {
            InitializeComponent();
            adm = admin;
            votep = vote;
            toolStripStatusLabel2.Text = nume;
            hide.Start();
            label3.Text += "-" + this.Text;
            toolStripStatusLabel5.Text = admin.ToString();
            if (admin == true) button5.Enabled = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (k == 1)
            { try
                {
                panel1.Visible = false ;
                panel2.Location = new Point(0, panel2.Location.Y);
                panel2.Size = new Size(this.Width, panel2.Height);
                Application.OpenForms["vote"].Size = new Size(this.Width, panel2.Height);
                k = 0;
                }
                catch (Exception)
                {
                    k = 1;
                }
                    
              
            }
            else
            { 
                try
                {
                panel1.Visible = true;
                panel2.Location = new Point(208, panel2.Location.Y);
                panel2.Size = new Size(728, panel2.Height);
                Application.OpenForms["vote"].Size = new Size(720, panel2.Height);
                k = 1;
                }
                catch(Exception)
                {
                    k = 0;
                };
                   
            }
        }

    
        private void button1_Click(object sender, EventArgs e)
        {
            Forme.vote vote = new Forme.vote(votep);
            vote.TopLevel = false;
            vote.AutoScroll = true;
            this.panel2.Controls.Add(vote);
            vote.Show();
            pictureBox1.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.OpenForms["Form1"].Show();
            this.Close();
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.exit_hov;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.exit_norm;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void _main_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            verif_luna();
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

        private void button2_Click(object sender, EventArgs e)
        {
            settings set = new settings(adm);
            set.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form administrator = new administrator();
            administrator.ShowDialog();
        }

        private void hide_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel4.Text = DateTime.Now.ToString("dd-MM-yyyy / HH:mm:ss");
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
