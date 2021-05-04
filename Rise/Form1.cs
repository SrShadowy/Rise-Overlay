using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace overlay_testing
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            
        }


        bool auto_save_config = false;
        Form overplay = new fm_overlay();

        private void chk_overlay_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_overlay.Checked)
            {
                overplay.Show();
                rb_MHGU.Enabled = false;
                rb_RISE.Enabled = false;
            }
            else
            {
                overplay.Hide();
            }
            {
                if (((fm_overlay)overplay).Visible)
                {
                    Console.WriteLine("Overlay opend!");
                    ((fm_overlay)overplay).timer1.Stop();
                    ((fm_overlay)overplay).tmr_process.Start();

                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
          ((fm_overlay)overplay).winW = (int)numericUpDown1.Value;
            auto_save_config = true;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            auto_save_config = true;
            ((fm_overlay)overplay).winH = (int)numericUpDown2.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if( colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ((fm_overlay)overplay).panel2.BackColor = colorDialog1.Color;
            }
            auto_save_config = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(auto_save_config == true)
            {
                auto_save_config = false;
                StreamWriter file = new StreamWriter("config.txt");
                file.WriteLine(numericUpDown1.Value + " : " + numericUpDown2.Value);
                file.WriteLineAsync("" + ((fm_overlay)overplay).panel2.BackColor.Name);
                file.WriteLineAsync("" + ((fm_overlay)overplay).lbl_hp.ForeColor.Name);
                file.WriteLineAsync("" + tb_barOpc.Value);
                file.WriteLineAsync("" + tb_lblOpc.Value);
                file.WriteLineAsync("" + numic_size.Value);
                file.WriteLineAsync("" + comboBox1.Text);
                file.WriteLineAsync("" + radioButton1.Enabled);
                file.Close();
            }
            if (((fm_overlay)overplay).timer1.Enabled == true)
            {
                lbl_status.Text = "Status: Found ";
                if (((fm_overlay)overplay).msc > 0x10)
                {
                    lbl_status.Text += " Hooked";
                }
            }
            chk_overlay.Checked = ((fm_overlay)overplay).Visible;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(File.Exists("config.txt"))
            {
                StreamReader file = new StreamReader("config.txt");
                var str = file.ReadLine().Split(':');
                    numericUpDown1.Value = Convert.ToInt32(str[0]);
                    numericUpDown2.Value = Convert.ToInt32(str[1]);
                var  str1 = file.ReadLine();
                if(str1 != null)
                {
                    ((fm_overlay)overplay).panel2.BackColor = Color.FromName(str1);
                    str1 = file.ReadLine();
                    ((fm_overlay)overplay).lbl_hp.ForeColor = Color.FromName(str1);
                    str1 = file.ReadLine();
                    tb_barOpc.Value = Convert.ToInt32(str1);
                    str1 = file.ReadLine();
                    tb_lblOpc.Value = Convert.ToInt32(str1);
                    str1 = file.ReadLine();
                    numic_size.Value = Convert.ToInt32(str1);
                    str1 = file.ReadLine();
                    comboBox1.Text = str1;
                    ((fm_overlay)overplay).lbl_hp.Font = new Font(str1, (int)numic_size.Value);
                    str1 = file.ReadLine();
                    if(str1.CompareTo("True") == 0)
                    {
                        radioButton1.Checked = true;
                    }
                }

                Console.WriteLine("loaded file");
                
                file.Close();
            }
            this.BackColor = Color.FromArgb(255, 0, 0, 0);
            this.TransparencyKey = this.BackColor;

            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                comboBox1.Items.Add(font.Name);
            }

        }

        private void btn_restore_Click(object sender, EventArgs e)
        {
            ((fm_overlay)overplay).restore();
            rb_MHGU.Enabled = true;
            rb_RISE.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ((fm_overlay)overplay).panel2.Visible = chk_bar.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ((fm_overlay)overplay).lbl_hp.Visible = chk_lbl.Checked;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((fm_overlay)overplay).lbl_hp.Font = new Font(comboBox1.Text, (int)numic_size.Value);
            auto_save_config = true;
        }

        bool mouse_clicked = false;
        Point init_cursor;
        Point init_form;

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(mouse_clicked)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(init_cursor));
                this.Location = Point.Add(init_form, new Size(dif));
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouse_clicked = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_clicked = true;
            init_cursor = MousePosition;
            init_form = this.Location;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ((fm_overlay)overplay).restore();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ((fm_overlay)overplay).lbl_hp.ForeColor = colorDialog1.Color;
                auto_save_config = true;

            }
        }

        private void tb_lblOpc_Scroll(object sender, EventArgs e)
        {
            ((fm_overlay)overplay).Opacity = ((tb_lblOpc.Value) / 100.0);
            auto_save_config = true;
        }

        private void numic_size_ValueChanged(object sender, EventArgs e)
        {
            ((fm_overlay)overplay).lbl_hp.Font = new Font(((fm_overlay)overplay).lbl_hp.Font.Name, (int)numic_size.Value);
            auto_save_config = true;
        }

        private void tb_barOpc_Scroll(object sender, EventArgs e)
        {
            ((fm_overlay)overplay).panel3.BackColor = Color.FromArgb(tb_barOpc.Value, 60,60,60);
            auto_save_config = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                ((fm_overlay)overplay).pn_basebar.Dock = DockStyle.Bottom;
                ((fm_overlay)overplay).lbl_hp.Dock = DockStyle.Top;
            }
            else
            {
                ((fm_overlay)overplay).pn_basebar.Dock = DockStyle.Top;
                ((fm_overlay)overplay).lbl_hp.Dock = DockStyle.Bottom;
            }
            auto_save_config = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rb_MHGU_CheckedChanged(object sender, EventArgs e)
        {
            ((fm_overlay)overplay).rise = rb_RISE.Checked;
            ((fm_overlay)overplay).gu = rb_MHGU.Checked;
            if (rb_MHGU.Checked)
            {
                this.BackgroundImage = Properties.Resources.Teste2;
            }
        }

        private void rb_RISE_CheckedChanged(object sender, EventArgs e)
        {
            ((fm_overlay)overplay).rise = rb_RISE.Checked;
            ((fm_overlay)overplay).gu = rb_MHGU.Checked;
            if (rb_RISE.Checked)
            {
                this.BackgroundImage = Properties.Resources.Teste1;
            }

        }
    }
}
