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
    public partial class Welcome : Form
    {
    bool adm;
    int vtp1;
    string database = Properties.Settings.Default.datasrc;
    string con_string = "datasource= " + Properties.Settings.Default.hostname + "; port=" + Properties.Settings.Default.port + ";username=" + Properties.Settings.Default.uname + ";password=" + Properties.Settings.Default.pass;
    public static string usr;
        public Welcome(bool admin,string username,int vtp)
        {
            InitializeComponent();
            if (admin == true) label2.Text = "Administrator mode";
            else label2.Text = "User mode";
            label4.Text = username;
            usr = username;
            adm = admin;
            vtp1 = vtp;
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.Opacity = 0;
            label1.Text = Properties.Settings.Default.welcomemsg;
            timer1.Start();
            label3.Text = Environment.UserName;
            string query = "SELECT message FROM " + database + ".wm";
            MySqlConnection cn = new MySqlConnection(con_string);
            cn.Open();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand(query, cn);
            MySqlDataAdapter adp = new MySqlDataAdapter(query, cn);
            adp.Fill(ds, "wm");
            label7.Text=cmd.ExecuteScalar().ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity + 0.01;
            if (this.Opacity == 1)
            {
                timer1.Stop();
                this.Opacity = 1;
                System.Threading.Thread.Sleep(1000);
                timer2.Start();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.01;
            if (this.Opacity == 0)
            {
                timer2.Stop();
                this.Close();
                Forme._main main = new Forme._main(usr,adm,vtp1);
                main.ShowDialog();
            }
        }
    }
}
