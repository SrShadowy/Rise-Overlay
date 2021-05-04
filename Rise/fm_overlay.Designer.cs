
namespace overlay_testing
{
    partial class fm_overlay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pn_basebar = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_hp = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tmr_process = new System.Windows.Forms.Timer(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.pn_basebar.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pn_basebar
            // 
            this.pn_basebar.BackColor = System.Drawing.Color.Transparent;
            this.pn_basebar.Controls.Add(this.panel2);
            this.pn_basebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pn_basebar.Location = new System.Drawing.Point(0, 0);
            this.pn_basebar.Name = "pn_basebar";
            this.pn_basebar.Size = new System.Drawing.Size(148, 20);
            this.pn_basebar.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkOrange;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(107, 20);
            this.panel2.TabIndex = 1;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
            this.panel2.MouseEnter += new System.EventHandler(this.panel2_MouseEnter);
            this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseMove);
            // 
            // lbl_hp
            // 
            this.lbl_hp.AutoSize = true;
            this.lbl_hp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_hp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_hp.ForeColor = System.Drawing.Color.White;
            this.lbl_hp.Location = new System.Drawing.Point(0, 20);
            this.lbl_hp.Name = "lbl_hp";
            this.lbl_hp.Size = new System.Drawing.Size(26, 16);
            this.lbl_hp.TabIndex = 0;
            this.lbl_hp.Text = "0/0";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tmr_process
            // 
            this.tmr_process.Interval = 1000;
            this.tmr_process.Tick += new System.EventHandler(this.tmr_process_Tick);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Controls.Add(this.lbl_hp);
            this.panel3.Controls.Add(this.pn_basebar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(148, 36);
            this.panel3.TabIndex = 1;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // fm_overlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(148, 36);
            this.Controls.Add(this.panel3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fm_overlay";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fm_overlay";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.fm_overlay_Load);
            this.Shown += new System.EventHandler(this.fm_overlay_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fm_overlay_KeyDown);
            this.pn_basebar.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Timer tmr_process;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label lbl_hp;
        public System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Panel pn_basebar;
    }
}