namespace AlgorithmProcess.Window
{
    partial class FromParameterSetting
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.AlgorithmDisplay = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel_background = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel_menu = new System.Windows.Forms.TableLayoutPanel();
            this.runprocess_btn = new System.Windows.Forms.Button();
            this.saveparameter_btn = new System.Windows.Forms.Button();
            this.selectprocess_cb = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SelectSlot_cb = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel_Main = new System.Windows.Forms.TableLayoutPanel();
            this.algorithmdisplay_panel = new System.Windows.Forms.Panel();
            this.stepdisplay_tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel_Under = new System.Windows.Forms.TableLayoutPanel();
            this.test_btn = new System.Windows.Forms.Button();
            this.apply_btn = new System.Windows.Forms.Button();
            this.ParameterSetting = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.AlgorithmDisplay.SuspendLayout();
            this.tableLayoutPanel_background.SuspendLayout();
            this.tableLayoutPanel_menu.SuspendLayout();
            this.tableLayoutPanel_Main.SuspendLayout();
            this.stepdisplay_tabControl.SuspendLayout();
            this.tableLayoutPanel_Under.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1918, 900);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.AlgorithmDisplay);
            this.tabControl1.Controls.Add(this.ParameterSetting);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1918, 900);
            this.tabControl1.TabIndex = 0;
            // 
            // AlgorithmDisplay
            // 
            this.AlgorithmDisplay.Controls.Add(this.tableLayoutPanel_background);
            this.AlgorithmDisplay.Location = new System.Drawing.Point(4, 35);
            this.AlgorithmDisplay.Name = "AlgorithmDisplay";
            this.AlgorithmDisplay.Padding = new System.Windows.Forms.Padding(3);
            this.AlgorithmDisplay.Size = new System.Drawing.Size(1910, 861);
            this.AlgorithmDisplay.TabIndex = 0;
            this.AlgorithmDisplay.Text = "AlgorithmDisplay";
            this.AlgorithmDisplay.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel_background
            // 
            this.tableLayoutPanel_background.ColumnCount = 1;
            this.tableLayoutPanel_background.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_background.Controls.Add(this.tableLayoutPanel_menu, 0, 0);
            this.tableLayoutPanel_background.Controls.Add(this.tableLayoutPanel_Main, 0, 1);
            this.tableLayoutPanel_background.Controls.Add(this.tableLayoutPanel_Under, 0, 2);
            this.tableLayoutPanel_background.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_background.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel_background.Name = "tableLayoutPanel_background";
            this.tableLayoutPanel_background.RowCount = 3;
            this.tableLayoutPanel_background.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel_background.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel_background.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel_background.Size = new System.Drawing.Size(1904, 855);
            this.tableLayoutPanel_background.TabIndex = 0;
            // 
            // tableLayoutPanel_menu
            // 
            this.tableLayoutPanel_menu.ColumnCount = 6;
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel_menu.Controls.Add(this.runprocess_btn, 5, 0);
            this.tableLayoutPanel_menu.Controls.Add(this.saveparameter_btn, 4, 0);
            this.tableLayoutPanel_menu.Controls.Add(this.selectprocess_cb, 3, 0);
            this.tableLayoutPanel_menu.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel_menu.Controls.Add(this.SelectSlot_cb, 1, 0);
            this.tableLayoutPanel_menu.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel_menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_menu.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel_menu.Name = "tableLayoutPanel_menu";
            this.tableLayoutPanel_menu.RowCount = 1;
            this.tableLayoutPanel_menu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_menu.Size = new System.Drawing.Size(1898, 79);
            this.tableLayoutPanel_menu.TabIndex = 0;
            // 
            // runprocess_btn
            // 
            this.runprocess_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runprocess_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runprocess_btn.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runprocess_btn.Location = new System.Drawing.Point(1583, 3);
            this.runprocess_btn.Name = "runprocess_btn";
            this.runprocess_btn.Size = new System.Drawing.Size(312, 73);
            this.runprocess_btn.TabIndex = 14;
            this.runprocess_btn.Text = "Run Process";
            this.runprocess_btn.UseVisualStyleBackColor = true;
            this.runprocess_btn.Click += new System.EventHandler(this.runprocess_btn_Click);
            // 
            // saveparameter_btn
            // 
            this.saveparameter_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveparameter_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveparameter_btn.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveparameter_btn.Location = new System.Drawing.Point(1267, 3);
            this.saveparameter_btn.Name = "saveparameter_btn";
            this.saveparameter_btn.Size = new System.Drawing.Size(310, 73);
            this.saveparameter_btn.TabIndex = 13;
            this.saveparameter_btn.Text = "Save Parameter";
            this.saveparameter_btn.UseVisualStyleBackColor = true;
            // 
            // selectprocess_cb
            // 
            this.selectprocess_cb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectprocess_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectprocess_cb.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectprocess_cb.FormattingEnabled = true;
            this.selectprocess_cb.Items.AddRange(new object[] {
            "CountSlice",
            "Stack",
            "Thickness",
            "Warpage",
            "Gap"});
            this.selectprocess_cb.Location = new System.Drawing.Point(951, 3);
            this.selectprocess_cb.Name = "selectprocess_cb";
            this.selectprocess_cb.Size = new System.Drawing.Size(310, 40);
            this.selectprocess_cb.TabIndex = 12;
            this.selectprocess_cb.SelectedIndexChanged += new System.EventHandler(this.selectprocess_cb_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("標楷體", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(635, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 79);
            this.label1.TabIndex = 11;
            this.label1.Text = "Select Process";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SelectSlot_cb
            // 
            this.SelectSlot_cb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectSlot_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectSlot_cb.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectSlot_cb.FormattingEnabled = true;
            this.SelectSlot_cb.Items.AddRange(new object[] {
            "Recipe ROI",
            "Custom ROI"});
            this.SelectSlot_cb.Location = new System.Drawing.Point(319, 3);
            this.SelectSlot_cb.Name = "SelectSlot_cb";
            this.SelectSlot_cb.Size = new System.Drawing.Size(310, 40);
            this.SelectSlot_cb.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("標楷體", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(310, 79);
            this.label3.TabIndex = 9;
            this.label3.Text = "Select Slot";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel_Main
            // 
            this.tableLayoutPanel_Main.ColumnCount = 2;
            this.tableLayoutPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Main.Controls.Add(this.algorithmdisplay_panel, 0, 0);
            this.tableLayoutPanel_Main.Controls.Add(this.stepdisplay_tabControl, 1, 0);
            this.tableLayoutPanel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Main.Location = new System.Drawing.Point(3, 88);
            this.tableLayoutPanel_Main.Name = "tableLayoutPanel_Main";
            this.tableLayoutPanel_Main.RowCount = 1;
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 678F));
            this.tableLayoutPanel_Main.Size = new System.Drawing.Size(1898, 678);
            this.tableLayoutPanel_Main.TabIndex = 1;
            // 
            // algorithmdisplay_panel
            // 
            this.algorithmdisplay_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.algorithmdisplay_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.algorithmdisplay_panel.Location = new System.Drawing.Point(3, 3);
            this.algorithmdisplay_panel.Name = "algorithmdisplay_panel";
            this.algorithmdisplay_panel.Size = new System.Drawing.Size(943, 672);
            this.algorithmdisplay_panel.TabIndex = 0;
            // 
            // stepdisplay_tabControl
            // 
            this.stepdisplay_tabControl.Controls.Add(this.tabPage1);
            this.stepdisplay_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepdisplay_tabControl.Location = new System.Drawing.Point(952, 3);
            this.stepdisplay_tabControl.Name = "stepdisplay_tabControl";
            this.stepdisplay_tabControl.SelectedIndex = 0;
            this.stepdisplay_tabControl.Size = new System.Drawing.Size(943, 672);
            this.stepdisplay_tabControl.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 35);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(935, 633);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel_Under
            // 
            this.tableLayoutPanel_Under.ColumnCount = 3;
            this.tableLayoutPanel_Under.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Under.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_Under.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_Under.Controls.Add(this.test_btn, 1, 0);
            this.tableLayoutPanel_Under.Controls.Add(this.apply_btn, 2, 0);
            this.tableLayoutPanel_Under.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Under.Location = new System.Drawing.Point(3, 772);
            this.tableLayoutPanel_Under.Name = "tableLayoutPanel_Under";
            this.tableLayoutPanel_Under.RowCount = 1;
            this.tableLayoutPanel_Under.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Under.Size = new System.Drawing.Size(1898, 80);
            this.tableLayoutPanel_Under.TabIndex = 2;
            // 
            // test_btn
            // 
            this.test_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.test_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.test_btn.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.test_btn.Location = new System.Drawing.Point(952, 3);
            this.test_btn.Name = "test_btn";
            this.test_btn.Size = new System.Drawing.Size(468, 74);
            this.test_btn.TabIndex = 14;
            this.test_btn.Text = "Test";
            this.test_btn.UseVisualStyleBackColor = true;
            // 
            // apply_btn
            // 
            this.apply_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.apply_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.apply_btn.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.apply_btn.Location = new System.Drawing.Point(1426, 3);
            this.apply_btn.Name = "apply_btn";
            this.apply_btn.Size = new System.Drawing.Size(469, 74);
            this.apply_btn.TabIndex = 15;
            this.apply_btn.Text = "Apply";
            this.apply_btn.UseVisualStyleBackColor = true;
            // 
            // ParameterSetting
            // 
            this.ParameterSetting.Location = new System.Drawing.Point(4, 35);
            this.ParameterSetting.Name = "ParameterSetting";
            this.ParameterSetting.Padding = new System.Windows.Forms.Padding(3);
            this.ParameterSetting.Size = new System.Drawing.Size(1910, 861);
            this.ParameterSetting.TabIndex = 1;
            this.ParameterSetting.Text = "ParameterSetting";
            this.ParameterSetting.UseVisualStyleBackColor = true;
            // 
            // FromParameterSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "FromParameterSetting";
            this.Size = new System.Drawing.Size(1918, 900);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.AlgorithmDisplay.ResumeLayout(false);
            this.tableLayoutPanel_background.ResumeLayout(false);
            this.tableLayoutPanel_menu.ResumeLayout(false);
            this.tableLayoutPanel_menu.PerformLayout();
            this.tableLayoutPanel_Main.ResumeLayout(false);
            this.stepdisplay_tabControl.ResumeLayout(false);
            this.tableLayoutPanel_Under.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage AlgorithmDisplay;
        private System.Windows.Forms.TabPage ParameterSetting;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_background;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_menu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox SelectSlot_cb;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Main;
        private System.Windows.Forms.Panel algorithmdisplay_panel;
        private System.Windows.Forms.TabControl stepdisplay_tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox selectprocess_cb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button runprocess_btn;
        private System.Windows.Forms.Button saveparameter_btn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Under;
        private System.Windows.Forms.Button test_btn;
        private System.Windows.Forms.Button apply_btn;
    }
}
