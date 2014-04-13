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
    public partial class vote : Form
    {
        int vtp;
        public vote(int vp)
        {
            InitializeComponent();
            calc_vtp();
        }
        void calc_vtp()
        {
            string query = "SELECT * FROM " + database + ".user WHERE username='" + Welcome.usr + "'";
            MySqlConnection cn = new MySqlConnection(con_string);
            cn.Open();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand(query, cn);
            MySqlDataAdapter adp = new MySqlDataAdapter(query, cn);
            adp.Fill(ds, "user");
            foreach (DataRow s in ds.Tables["user"].Rows)
            {
                vtp = Int32.Parse(s["votepoints"].ToString());
            }
            cn.Close();
        }
        string con_string = "datasource= " + Properties.Settings.Default.hostname + "; port=" + Properties.Settings.Default.port + ";username=" + Properties.Settings.Default.uname + ";password=" + Properties.Settings.Default.pass;
        string database = Properties.Settings.Default.datasrc;
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void vote_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }

        private void toolStripSplitButton4_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            string fructe;
            fructe=((ToolStripButton)sender).ToString();
            string query = "SELECT * FROM " + database + ".products WHERE `category`='" + fructe + "'";
            MySqlConnection cn = new MySqlConnection(con_string);
            cn.Open();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand(query, cn);
            MySqlDataAdapter adp = new MySqlDataAdapter(query, cn);
            adp.Fill(ds, "products");
            foreach (DataRow s in ds.Tables["products"].Rows)
            {
                Button btn = new Button();
                btn.FlatStyle = FlatStyle.Flat;
                btn.ForeColor = Color.Black ;
                btn.BackColor = Color.FromName("Control");
                btn.Font = new Font("Segoe UI", 9);
                btn.Width = 120;
                btn.Height = 70;
                btn.Text = s["product"].ToString()+"\n Rate: "+s["avg_vote"].ToString()+" * \n Price: "+s["price"].ToString()+" RON";
                btn.Name = s["product"].ToString();
                btn.Click += new EventHandler(btn_Click);
                flowLayoutPanel1.Controls.Add(btn);
            }
            cn.Close();
        }

        void btn_Click(object sender, EventArgs e)
        {
            string produs = ((Button)sender).Name;
            Form votefor = new votefor(produs,vtp);
            votefor.ShowDialog();
            calc_vtp();
            flowLayoutPanel1.Controls.Clear();


        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }


       
    }
}
