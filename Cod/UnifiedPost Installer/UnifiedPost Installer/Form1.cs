using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace UnifiedPost_Installer
{
    public partial class Form1 : Form
    {
        Boolean drag = false;
        int mousex;
        int mousey;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
  
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            //panel2.Visible = false;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowser = new FolderBrowserDialog();
            folderbrowser.Description = "Select installation folder for UnifiedPost...";
            folderbrowser.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderbrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (folderbrowser.SelectedPath.ToString().EndsWith(((char)0x5C).ToString()) == true)
                    textBox1.Text = folderbrowser.SelectedPath.ToString();
                else
                    textBox1.Text = folderbrowser.SelectedPath.ToString() + (char)0x5C;

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                panel3.Visible = false;
                timer1.Enabled = true;
                this.BackgroundImage = Properties.Resources.Capture1;
                this.BackgroundImageLayout = ImageLayout.Stretch;
                try
                {
                    byte[] myfile = Properties.Resources.UnifiedPost;
                    System.IO.File.WriteAllBytes(textBox1.Text + "UnifiedPost.exe", myfile);
                    byte[] myfile2 = Properties.Resources.mysql_data;
                    System.IO.File.WriteAllBytes(textBox1.Text + "mysql.data.dll", myfile2);
                    byte[] myfile3 = Properties.Resources.ExcelLibrary;
                    System.IO.File.WriteAllBytes(textBox1.Text + "ExcelLibrary.dll", myfile3);
                }
                catch (Exception)
                {
                }
            }
            else
            {
                button6.PerformClick();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
            this.BackgroundImage = Properties.Resources.background2;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                if (progressBar1.Value == 10)
                {
                    this.BackgroundImage = Properties.Resources.Capture3;
                    this.BackgroundImageLayout = ImageLayout.Zoom;
                }
                if (progressBar1.Value == 25)
                {
                    this.BackgroundImage = Properties.Resources.Capture5;
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }
                if (progressBar1.Value == 50)
                {
                    this.BackgroundImage = Properties.Resources.Capture11;
                }
                if (progressBar1.Value == 75)
                {
                    this.BackgroundImage = Properties.Resources.Capture8;
                }

                if ((progressBar1.Value > 30) && (progressBar1.Value < 40)) timer1.Interval = 200;
                if ((progressBar1.Value > 40) && (progressBar1.Value < 70)) timer1.Interval = 200;
                if ((progressBar1.Value > 80) && (progressBar1.Value < 85)) timer1.Interval = 200;
                if (progressBar1.Value > 85) timer1.Interval = 50;
                progressBar1.Value++;
            }
            else
            {
                progressBar1.Visible = false;
                label6.Text = "UnifiedPost installed successfully!";
                button9.Visible = true;
                timer1.Enabled = false;
                button9.Enabled = true;
                button9.PerformClick();
                

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) //System.Diagnostics.Process.Start(textBox1.Text + "UnifiedPost.exe");
            {
                Process unified = new Process();
                unified.StartInfo.FileName = textBox1.Text + "UnifiedPost.exe";
                unified.StartInfo.Arguments = "\"" + textBox1.Text + "UnifiedPost.exe" + "\"";
                unified.Start();
            } 
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.exit_hov;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.exit_norm;
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
