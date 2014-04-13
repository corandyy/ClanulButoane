using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using System.Web;

namespace UnifiedPost.Forme
{
    public partial class chartstatistics : Form
    {
        string con_string = "datasource= " + Properties.Settings.Default.hostname + "; port=" + Properties.Settings.Default.port + ";username=" + Properties.Settings.Default.uname + ";password=" + Properties.Settings.Default.pass;
        string database = Properties.Settings.Default.datasrc;
        public chartstatistics()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chart_Load(object sender, EventArgs e)
        {
            //comboBox3.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            toolStrip1.Width = this.Width;
            comboprod();
            CreateChart("");
            comboprod();
        }
        private void CreateChart(string category)
        {
            try
            {
                chart1.Series.Clear();
                string cmd_string;
                if (category == "") cmd_string = "SELECT * FROM user.products";
                else cmd_string = "SELECT * FROM user.products WHERE `category`='" + category + "'";
                MySqlConnection con = new MySqlConnection(con_string);
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd_string, con);
                DataSet ds = new DataSet();
                adp.Fill(ds, "product");
                foreach (DataRow r in ds.Tables["product"].Rows)
                {
                    var series = new Series(r["product"].ToString());
                    // parametru 1: axa OX, parametru 2: axa OY
                    series.Points.DataBindXY(new[] { Int32.Parse((double.Parse(r["nr_vote"].ToString())).ToString("0")) }, new[] { Int32.Parse((double.Parse(r["avg_vote"].ToString())).ToString("0")) });
                    chart1.Series.Add(series);
                }
             
            }
            catch
            {
            }
            foreach (Series s in chart1.Series)
            {
                s.Sort(System.Windows.Forms.DataVisualization.Charting.PointSortOrder.Ascending, "X");

            }
        }

        private void comboprod()
        {
            string cmd_string = "SELECT * FROM user.products";
            MySqlConnection con = new MySqlConnection(con_string);
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd_string, con);
            cmd.Connection = con;
            cmd.CommandText = cmd_string;
            con.Open();
            DataSet ds = new DataSet();
            adp.Fill(ds, "products");
            comboBox1.Items.Clear();
            foreach (DataRow s in ds.Tables["products"].Rows)
            {
                if (!comboBox1.Items.Contains(s["category"].ToString())) comboBox1.Items.Add(s["category"].ToString());
            }
            try
            {
                comboBox1.SelectedIndex = 0;
            }
            catch
            {
            }
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateChart(comboBox1.SelectedItem.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save chart statistics as image";
            
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if(save.FileName.EndsWith(".png") == true) chart1.SaveImage(save.FileName, ChartImageFormat.Png);
                else if(save.FileName.EndsWith(".jpg") == true) chart1.SaveImage(save.FileName, ChartImageFormat.Jpeg);
                else if (save.FileName.EndsWith(".PNG") == true) chart1.SaveImage(save.FileName, ChartImageFormat.Png);
                else if (save.FileName.EndsWith(".JPG") == true) chart1.SaveImage(save.FileName, ChartImageFormat.Jpeg);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            chart1.Palette = ChartColorPalette.Bright;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            chart1.Palette = ChartColorPalette.Grayscale;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            chart1.Palette = ChartColorPalette.Excel;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            chart1.Palette = ChartColorPalette.Pastel;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            chart1.Palette = ChartColorPalette.EarthTones;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            chart1.Palette = ChartColorPalette.SemiTransparent;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            chart1.Palette = ChartColorPalette.Berry;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            chart1.Palette = ChartColorPalette.Chocolate;
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            chart1.Palette = ChartColorPalette.Fire;
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            chart1.Palette = ChartColorPalette.SeaGreen;
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            chart1.Palette = ChartColorPalette.BrightPastel;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                hScrollBar1.Visible = true;
                hScrollBar2.Visible = true;
                vScrollBar1.Visible = true;
                label4.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
                numericUpDown1.Enabled = true;
                comboBox2.Enabled = true;
            }
            else
            {
                chart1.ChartAreas[0].Area3DStyle.Enable3D = false;
                hScrollBar1.Visible = false;
                hScrollBar2.Visible = false;
                vScrollBar1.Visible = false;
                label4.ForeColor = Color.DarkGray;
                label2.ForeColor = Color.DarkGray;
                numericUpDown1.Enabled = false;
                comboBox2.Enabled = false;
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (chart1.ChartAreas[0].Area3DStyle.Enable3D == true)
                chart1.ChartAreas[0].Area3DStyle.Rotation = Int32.Parse(hScrollBar1.Value.ToString());
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (chart1.ChartAreas[0].Area3DStyle.Enable3D == true)
                chart1.ChartAreas[0].Area3DStyle.Perspective = Int32.Parse(vScrollBar1.Value.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {

            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                chart1.ChartAreas[0].BackColor = color.Color;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (chart1.ChartAreas[0].Area3DStyle.Enable3D == true)
                chart1.ChartAreas[0].Area3DStyle.WallWidth= Int32.Parse(numericUpDown1.Value.ToString());
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chart1.ChartAreas[0].Area3DStyle.Enable3D == true)
                if (comboBox2.SelectedIndex == 0) chart1.ChartAreas[0].Area3DStyle.LightStyle = LightStyle.None;
                else if (comboBox2.SelectedIndex == 1) chart1.ChartAreas[0].Area3DStyle.LightStyle = LightStyle.Simplistic;
                else if (comboBox2.SelectedIndex == 2) chart1.ChartAreas[0].Area3DStyle.LightStyle = LightStyle.Realistic;
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            if (chart1.ChartAreas[0].Area3DStyle.Enable3D == true)
                chart1.ChartAreas[0].Area3DStyle.Inclination = Int32.Parse(hScrollBar2.Value.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                chart1.BackColor = color.Color;
                this.BackColor = color.Color;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            chart1.BackColor = Color.Transparent;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].BackColor = Color.Transparent;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        } 
    }
}
