using System;
using System.Windows.Forms;
using System.IO;

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
            if (chk_overlay.Checked) overplay.Show();
            else overplay.Hide();

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
                ((fm_overlay)overplay).bar = colorDialog1.Color;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(auto_save_config == true)
            {
                auto_save_config = false;
                StreamWriter file = new StreamWriter("config.txt");
                file.Write(numericUpDown1.Value + " : " + numericUpDown2.Value);
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
                if(str.Length > 1)
                {
                    numericUpDown1.Value = Convert.ToInt32(str[0]);
                    numericUpDown2.Value = Convert.ToInt32(str[1]);
                    Console.WriteLine(str[0] + "  " + str[1]);
                }
                file.Close();
            }
        }

        private void btn_restore_Click(object sender, EventArgs e)
        {
            ((fm_overlay)overplay).restore();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ((fm_overlay)overplay).panel2.Visible = chk_bar.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ((fm_overlay)overplay).lbl_hp.Visible = chk_lbl.Checked;
        }
    }
}
