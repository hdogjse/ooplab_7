namespace ooplab_4
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MultiSelect = new CheckBox();
            ctrl_use = new CheckBox();
            comboBox1 = new ComboBox();
            colorDialog1 = new ColorDialog();
            clr_btn = new Button();
            make_grp_btn = new Button();
            Save_btn = new Button();
            Load_btn = new Button();
            SuspendLayout();
            // 
            // MultiSelect
            // 
            MultiSelect.AutoSize = true;
            MultiSelect.Location = new Point(10, 9);
            MultiSelect.Margin = new Padding(3, 2, 3, 2);
            MultiSelect.Name = "MultiSelect";
            MultiSelect.Size = new Size(85, 19);
            MultiSelect.TabIndex = 0;
            MultiSelect.Text = "MultiSelect";
            MultiSelect.UseVisualStyleBackColor = true;
            MultiSelect.KeyDown += Form1_KeyDown;
            // 
            // ctrl_use
            // 
            ctrl_use.AutoSize = true;
            ctrl_use.Location = new Point(108, 9);
            ctrl_use.Margin = new Padding(3, 2, 3, 2);
            ctrl_use.Name = "ctrl_use";
            ctrl_use.Size = new Size(166, 19);
            ctrl_use.TabIndex = 1;
            ctrl_use.Text = "Using CTRL for MultiSelect";
            ctrl_use.UseVisualStyleBackColor = true;
            ctrl_use.KeyDown += Form1_KeyDown;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Круг", "Квадрат", "Линия" });
            comboBox1.Location = new Point(302, 8);
            comboBox1.Margin = new Padding(3, 2, 3, 2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(133, 23);
            comboBox1.TabIndex = 2;
            comboBox1.KeyDown += Form1_KeyDown;
            // 
            // clr_btn
            // 
            clr_btn.BackColor = SystemColors.ControlDark;
            clr_btn.Location = new Point(453, 7);
            clr_btn.Name = "clr_btn";
            clr_btn.Size = new Size(75, 23);
            clr_btn.TabIndex = 3;
            clr_btn.Text = "Цвет";
            clr_btn.UseVisualStyleBackColor = false;
            clr_btn.Click += clr_btn_Click;
            // 
            // make_grp_btn
            // 
            make_grp_btn.Location = new Point(535, 7);
            make_grp_btn.Margin = new Padding(3, 2, 3, 2);
            make_grp_btn.Name = "make_grp_btn";
            make_grp_btn.Size = new Size(111, 22);
            make_grp_btn.TabIndex = 4;
            make_grp_btn.Text = "Сгруппировать";
            make_grp_btn.UseVisualStyleBackColor = true;
            make_grp_btn.Click += make_grp_btn_Click;
            // 
            // Save_btn
            // 
            Save_btn.Location = new Point(652, 7);
            Save_btn.Name = "Save_btn";
            Save_btn.Size = new Size(75, 23);
            Save_btn.TabIndex = 5;
            Save_btn.Text = "Сохранить";
            Save_btn.UseVisualStyleBackColor = true;
            Save_btn.Click += Save_btn_Click;
            // 
            // Load_btn
            // 
            Load_btn.Location = new Point(733, 8);
            Load_btn.Name = "Load_btn";
            Load_btn.Size = new Size(75, 23);
            Load_btn.TabIndex = 6;
            Load_btn.Text = "Загрузить";
            Load_btn.UseVisualStyleBackColor = true;
            Load_btn.Click += Load_btn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(819, 563);
            Controls.Add(Load_btn);
            Controls.Add(Save_btn);
            Controls.Add(make_grp_btn);
            Controls.Add(clr_btn);
            Controls.Add(comboBox1);
            Controls.Add(ctrl_use);
            Controls.Add(MultiSelect);
            MaximizeBox = false;
            MaximumSize = new Size(835, 602);
            MinimumSize = new Size(835, 602);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            Click += Form1_Click;
            Paint += Form1_Paint;
            KeyDown += Form1_KeyDown;
            KeyPress += Form1_KeyPress;
            MouseClick += Form1_MouseClick;
            MouseDown += Form1_MouseDown;
            MouseMove += Form1_MouseMove;
            MouseUp += Form1_MouseUp;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox MultiSelect;
        private CheckBox ctrl_use;
        private ComboBox comboBox1;
        private ColorDialog colorDialog1;
        private Button clr_btn;
        private Button make_grp_btn;
        private Button Save_btn;
        private Button Load_btn;
    }
}