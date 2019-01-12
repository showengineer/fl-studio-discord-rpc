namespace FLRPC_GUI
{
    partial class settings
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sec_mode_sel = new System.Windows.Forms.RadioButton();
            this.def_mode_selector = new System.Windows.Forms.RadioButton();
            this.SecMode_txt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.noPrj_txt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.debugLevel_sel = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dangerZone_enable = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.debugLevel_sel)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.SecMode_txt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.noPrj_txt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 139);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General Settings";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.sec_mode_sel);
            this.panel1.Controls.Add(this.def_mode_selector);
            this.panel1.Location = new System.Drawing.Point(95, 99);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(277, 34);
            this.panel1.TabIndex = 5;
            // 
            // sec_mode_sel
            // 
            this.sec_mode_sel.AutoSize = true;
            this.sec_mode_sel.Location = new System.Drawing.Point(136, 10);
            this.sec_mode_sel.Name = "sec_mode_sel";
            this.sec_mode_sel.Size = new System.Drawing.Size(86, 17);
            this.sec_mode_sel.TabIndex = 1;
            this.sec_mode_sel.TabStop = true;
            this.sec_mode_sel.Text = "Secret Mode";
            this.sec_mode_sel.UseVisualStyleBackColor = true;
            // 
            // def_mode_selector
            // 
            this.def_mode_selector.AutoSize = true;
            this.def_mode_selector.Location = new System.Drawing.Point(5, 10);
            this.def_mode_selector.Name = "def_mode_selector";
            this.def_mode_selector.Size = new System.Drawing.Size(88, 17);
            this.def_mode_selector.TabIndex = 0;
            this.def_mode_selector.TabStop = true;
            this.def_mode_selector.Text = "Normal Mode";
            this.def_mode_selector.UseVisualStyleBackColor = true;
            // 
            // SecMode_txt
            // 
            this.SecMode_txt.Location = new System.Drawing.Point(192, 73);
            this.SecMode_txt.Name = "SecMode_txt";
            this.SecMode_txt.Size = new System.Drawing.Size(180, 20);
            this.SecMode_txt.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Default mode:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Text to display when in secret mode:";
            // 
            // noPrj_txt
            // 
            this.noPrj_txt.Location = new System.Drawing.Point(165, 31);
            this.noPrj_txt.Name = "noPrj_txt";
            this.noPrj_txt.Size = new System.Drawing.Size(207, 20);
            this.noPrj_txt.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Text to display if no project file:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.debugLevel_sel);
            this.groupBox2.Location = new System.Drawing.Point(418, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(258, 138);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Advanced Settings";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(126, 70);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(38, 20);
            this.numericUpDown1.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "State refresh interval (s):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Terminal Debug Level:";
            // 
            // debugLevel_sel
            // 
            this.debugLevel_sel.Location = new System.Drawing.Point(126, 30);
            this.debugLevel_sel.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.debugLevel_sel.Name = "debugLevel_sel";
            this.debugLevel_sel.Size = new System.Drawing.Size(30, 20);
            this.debugLevel_sel.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDown2);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(683, 43);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(234, 108);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Danger Zone";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(87, 47);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            65967,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(38, 20);
            this.numericUpDown2.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Network Pipe:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(102, 17);
            this.textBox1.MaxLength = 18;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "476022809429147648";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Discord Client ID:";
            // 
            // dangerZone_enable
            // 
            this.dangerZone_enable.AutoSize = true;
            this.dangerZone_enable.Location = new System.Drawing.Point(683, 20);
            this.dangerZone_enable.Name = "dangerZone_enable";
            this.dangerZone_enable.Size = new System.Drawing.Size(165, 17);
            this.dangerZone_enable.TabIndex = 3;
            this.dangerZone_enable.Text = "Enable Danger Zone controls";
            this.dangerZone_enable.UseVisualStyleBackColor = true;
            this.dangerZone_enable.CheckedChanged += new System.EventHandler(this.dangerZone_enable_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 157);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(399, 39);
            this.button1.TabIndex = 4;
            this.button1.Text = "SAVE!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 207);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dangerZone_enable);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(948, 246);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(948, 246);
            this.Name = "settings";
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.debugLevel_sel)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton sec_mode_sel;
        private System.Windows.Forms.RadioButton def_mode_selector;
        private System.Windows.Forms.TextBox SecMode_txt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox noPrj_txt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown debugLevel_sel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox dangerZone_enable;
        private System.Windows.Forms.Button button1;
    }
}