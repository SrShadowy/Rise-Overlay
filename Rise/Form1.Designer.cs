
namespace overlay_testing
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.chk_overlay = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btn_restore = new System.Windows.Forms.Button();
            this.lbl_status = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_barOpc = new System.Windows.Forms.TrackBar();
            this.tb_lblOpc = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.numic_size = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chk_lbl = new System.Windows.Forms.CheckBox();
            this.chk_bar = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_wait = new System.Windows.Forms.Label();
            this.rb_MHGU = new System.Windows.Forms.RadioButton();
            this.rb_RISE = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_barOpc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_lblOpc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numic_size)).BeginInit();
            this.SuspendLayout();
            // 
            // chk_overlay
            // 
            this.chk_overlay.AutoSize = true;
            this.chk_overlay.BackColor = System.Drawing.Color.Transparent;
            this.chk_overlay.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_overlay.ForeColor = System.Drawing.SystemColors.Control;
            this.chk_overlay.Location = new System.Drawing.Point(115, 48);
            this.chk_overlay.Name = "chk_overlay";
            this.chk_overlay.Size = new System.Drawing.Size(139, 22);
            this.chk_overlay.TabIndex = 0;
            this.chk_overlay.Text = "enable overlay";
            this.chk_overlay.UseVisualStyleBackColor = false;
            this.chk_overlay.CheckedChanged += new System.EventHandler(this.chk_overlay_CheckedChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.numericUpDown1.Location = new System.Drawing.Point(128, 180);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(148, 25);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.numericUpDown2.Location = new System.Drawing.Point(128, 211);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(148, 25);
            this.numericUpDown2.TabIndex = 2;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(242, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 25);
            this.button1.TabIndex = 3;
            this.button1.Text = "Color";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btn_restore
            // 
            this.btn_restore.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btn_restore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_restore.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_restore.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_restore.Location = new System.Drawing.Point(118, 242);
            this.btn_restore.Name = "btn_restore";
            this.btn_restore.Size = new System.Drawing.Size(161, 28);
            this.btn_restore.TabIndex = 4;
            this.btn_restore.Text = "restore hook";
            this.btn_restore.UseVisualStyleBackColor = false;
            this.btn_restore.Click += new System.EventHandler(this.btn_restore_Click);
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.BackColor = System.Drawing.Color.Transparent;
            this.lbl_status.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_status.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_status.Location = new System.Drawing.Point(84, 295);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(80, 18);
            this.lbl_status.TabIndex = 5;
            this.lbl_status.Text = "Status...";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb_barOpc);
            this.groupBox1.Controls.Add(this.tb_lblOpc);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.numic_size);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chk_lbl);
            this.groupBox1.Controls.Add(this.chk_bar);
            this.groupBox1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Location = new System.Drawing.Point(282, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 279);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options Overlay";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.Control;
            this.button2.Location = new System.Drawing.Point(242, 110);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(74, 25);
            this.button2.TabIndex = 6;
            this.button2.Text = "Color";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(232, 175);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(74, 22);
            this.radioButton2.TabIndex = 12;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Button";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(6, 210);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 18);
            this.label5.TabIndex = 10;
            this.label5.Text = "opacity all:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(176, 175);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(50, 22);
            this.radioButton1.TabIndex = 11;
            this.radioButton1.Text = "Top";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(6, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "background opacity:";
            // 
            // tb_barOpc
            // 
            this.tb_barOpc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(18)))), ((int)(((byte)(19)))));
            this.tb_barOpc.Location = new System.Drawing.Point(6, 42);
            this.tb_barOpc.Maximum = 255;
            this.tb_barOpc.Name = "tb_barOpc";
            this.tb_barOpc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tb_barOpc.Size = new System.Drawing.Size(310, 45);
            this.tb_barOpc.TabIndex = 7;
            this.tb_barOpc.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tb_barOpc.Scroll += new System.EventHandler(this.tb_barOpc_Scroll);
            // 
            // tb_lblOpc
            // 
            this.tb_lblOpc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(18)))), ((int)(((byte)(19)))));
            this.tb_lblOpc.Location = new System.Drawing.Point(6, 231);
            this.tb_lblOpc.Maximum = 100;
            this.tb_lblOpc.Name = "tb_lblOpc";
            this.tb_lblOpc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tb_lblOpc.Size = new System.Drawing.Size(310, 45);
            this.tb_lblOpc.TabIndex = 9;
            this.tb_lblOpc.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tb_lblOpc.Value = 100;
            this.tb_lblOpc.Scroll += new System.EventHandler(this.tb_lblOpc_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(6, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Font:";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(97, 140);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(219, 26);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.Text = "Consolas";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // numic_size
            // 
            this.numic_size.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numic_size.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.numic_size.Location = new System.Drawing.Point(97, 172);
            this.numic_size.Name = "numic_size";
            this.numic_size.Size = new System.Drawing.Size(52, 25);
            this.numic_size.TabIndex = 3;
            this.numic_size.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numic_size.ValueChanged += new System.EventHandler(this.numic_size_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(6, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Font size:";
            // 
            // chk_lbl
            // 
            this.chk_lbl.AutoSize = true;
            this.chk_lbl.Checked = true;
            this.chk_lbl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_lbl.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_lbl.ForeColor = System.Drawing.SystemColors.Control;
            this.chk_lbl.Location = new System.Drawing.Point(6, 112);
            this.chk_lbl.Name = "chk_lbl";
            this.chk_lbl.Size = new System.Drawing.Size(99, 22);
            this.chk_lbl.TabIndex = 1;
            this.chk_lbl.Text = "Number HP";
            this.chk_lbl.UseVisualStyleBackColor = true;
            this.chk_lbl.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // chk_bar
            // 
            this.chk_bar.AutoSize = true;
            this.chk_bar.Checked = true;
            this.chk_bar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_bar.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_bar.ForeColor = System.Drawing.SystemColors.Control;
            this.chk_bar.Location = new System.Drawing.Point(6, 84);
            this.chk_bar.Name = "chk_bar";
            this.chk_bar.Size = new System.Drawing.Size(75, 22);
            this.chk_bar.TabIndex = 0;
            this.chk_bar.Text = "Bar HP";
            this.chk_bar.UseVisualStyleBackColor = true;
            this.chk_bar.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(630, 1);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(24, 22);
            this.button3.TabIndex = 7;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(126, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 36);
            this.label4.TabIndex = 9;
            this.label4.Text = "Position on \r\nthe game screen";
            // 
            // lbl_wait
            // 
            this.lbl_wait.AutoSize = true;
            this.lbl_wait.BackColor = System.Drawing.Color.Transparent;
            this.lbl_wait.Font = new System.Drawing.Font("Corbel", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_wait.ForeColor = System.Drawing.Color.Red;
            this.lbl_wait.Location = new System.Drawing.Point(287, 1);
            this.lbl_wait.Name = "lbl_wait";
            this.lbl_wait.Size = new System.Drawing.Size(54, 23);
            this.lbl_wait.TabIndex = 10;
            this.lbl_wait.Text = "WAIT";
            this.lbl_wait.Visible = false;
            // 
            // rb_MHGU
            // 
            this.rb_MHGU.AutoSize = true;
            this.rb_MHGU.BackColor = System.Drawing.Color.Transparent;
            this.rb_MHGU.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_MHGU.ForeColor = System.Drawing.SystemColors.Control;
            this.rb_MHGU.Location = new System.Drawing.Point(200, 100);
            this.rb_MHGU.Name = "rb_MHGU";
            this.rb_MHGU.Size = new System.Drawing.Size(39, 19);
            this.rb_MHGU.TabIndex = 14;
            this.rb_MHGU.Text = "GU";
            this.rb_MHGU.UseVisualStyleBackColor = false;
            this.rb_MHGU.CheckedChanged += new System.EventHandler(this.rb_MHGU_CheckedChanged);
            // 
            // rb_RISE
            // 
            this.rb_RISE.AutoSize = true;
            this.rb_RISE.BackColor = System.Drawing.Color.Transparent;
            this.rb_RISE.Checked = true;
            this.rb_RISE.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_RISE.ForeColor = System.Drawing.SystemColors.Control;
            this.rb_RISE.Location = new System.Drawing.Point(120, 100);
            this.rb_RISE.Name = "rb_RISE";
            this.rb_RISE.Size = new System.Drawing.Size(53, 19);
            this.rb_RISE.TabIndex = 13;
            this.rb_RISE.TabStop = true;
            this.rb_RISE.Text = "Rise";
            this.rb_RISE.UseVisualStyleBackColor = false;
            this.rb_RISE.CheckedChanged += new System.EventHandler(this.rb_RISE_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(116, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 18);
            this.label6.TabIndex = 15;
            this.label6.Text = "Game:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.BackgroundImage = global::overlay_testing.Properties.Resources.Teste1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(716, 322);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.rb_MHGU);
            this.Controls.Add(this.rb_RISE);
            this.Controls.Add(this.lbl_wait);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.btn_restore);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.chk_overlay);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.Text = "OVER";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_barOpc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_lblOpc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numic_size)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chk_overlay;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_restore;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chk_lbl;
        private System.Windows.Forms.CheckBox chk_bar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.NumericUpDown numic_size;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar tb_barOpc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar tb_lblOpc;
        public System.Windows.Forms.Label lbl_wait;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.RadioButton rb_MHGU;
        public System.Windows.Forms.RadioButton rb_RISE;
    }
}

