namespace AlgorithmProcess.Window
{
    partial class FromAlgorithmTest
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
            this.tableLayoutPanel_background = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel_menu = new System.Windows.Forms.TableLayoutPanel();
            this.runalgorithm_btn = new System.Windows.Forms.Button();
            this.SelectAlgorithm_cb = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SelectSlot_cb = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel_Main = new System.Windows.Forms.TableLayoutPanel();
            this.Image_tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Image_panel = new System.Windows.Forms.Panel();
            this.Parameter_panel = new System.Windows.Forms.Panel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel_background.SuspendLayout();
            this.tableLayoutPanel_menu.SuspendLayout();
            this.tableLayoutPanel_Main.SuspendLayout();
            this.Image_tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_background
            // 
            this.tableLayoutPanel_background.ColumnCount = 1;
            this.tableLayoutPanel_background.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_background.Controls.Add(this.tableLayoutPanel_menu, 0, 0);
            this.tableLayoutPanel_background.Controls.Add(this.tableLayoutPanel_Main, 0, 1);
            this.tableLayoutPanel_background.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_background.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_background.Name = "tableLayoutPanel_background";
            this.tableLayoutPanel_background.RowCount = 2;
            this.tableLayoutPanel_background.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel_background.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel_background.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_background.Size = new System.Drawing.Size(1918, 900);
            this.tableLayoutPanel_background.TabIndex = 1;
            // 
            // tableLayoutPanel_menu
            // 
            this.tableLayoutPanel_menu.ColumnCount = 5;
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_menu.Controls.Add(this.runalgorithm_btn, 4, 0);
            this.tableLayoutPanel_menu.Controls.Add(this.SelectAlgorithm_cb, 3, 0);
            this.tableLayoutPanel_menu.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel_menu.Controls.Add(this.SelectSlot_cb, 1, 0);
            this.tableLayoutPanel_menu.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel_menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_menu.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel_menu.Name = "tableLayoutPanel_menu";
            this.tableLayoutPanel_menu.RowCount = 1;
            this.tableLayoutPanel_menu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_menu.Size = new System.Drawing.Size(1912, 84);
            this.tableLayoutPanel_menu.TabIndex = 0;
            // 
            // runalgorithm_btn
            // 
            this.runalgorithm_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runalgorithm_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runalgorithm_btn.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runalgorithm_btn.Location = new System.Drawing.Point(1531, 3);
            this.runalgorithm_btn.Name = "runalgorithm_btn";
            this.runalgorithm_btn.Size = new System.Drawing.Size(378, 78);
            this.runalgorithm_btn.TabIndex = 14;
            this.runalgorithm_btn.Text = "Run Algorithm";
            this.runalgorithm_btn.UseVisualStyleBackColor = true;
            // 
            // SelectAlgorithm_cb
            // 
            this.SelectAlgorithm_cb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectAlgorithm_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectAlgorithm_cb.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectAlgorithm_cb.FormattingEnabled = true;
            this.SelectAlgorithm_cb.Location = new System.Drawing.Point(1149, 3);
            this.SelectAlgorithm_cb.Name = "SelectAlgorithm_cb";
            this.SelectAlgorithm_cb.Size = new System.Drawing.Size(376, 40);
            this.SelectAlgorithm_cb.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("標楷體", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(767, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(376, 84);
            this.label1.TabIndex = 11;
            this.label1.Text = "Select Algorithm";
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
            this.SelectSlot_cb.Location = new System.Drawing.Point(385, 3);
            this.SelectSlot_cb.Name = "SelectSlot_cb";
            this.SelectSlot_cb.Size = new System.Drawing.Size(376, 40);
            this.SelectSlot_cb.TabIndex = 10;
            this.SelectSlot_cb.SelectedIndexChanged += new System.EventHandler(this.SelectSlot_cb_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("標楷體", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(376, 84);
            this.label3.TabIndex = 9;
            this.label3.Text = "Select Slot";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel_Main
            // 
            this.tableLayoutPanel_Main.ColumnCount = 2;
            this.tableLayoutPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel_Main.Controls.Add(this.Image_tabControl, 0, 0);
            this.tableLayoutPanel_Main.Controls.Add(this.Parameter_panel, 1, 0);
            this.tableLayoutPanel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Main.Location = new System.Drawing.Point(3, 93);
            this.tableLayoutPanel_Main.Name = "tableLayoutPanel_Main";
            this.tableLayoutPanel_Main.RowCount = 1;
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Main.Size = new System.Drawing.Size(1912, 804);
            this.tableLayoutPanel_Main.TabIndex = 1;
            // 
            // Image_tabControl
            // 
            this.Image_tabControl.Controls.Add(this.tabPage1);
            this.Image_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Image_tabControl.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Image_tabControl.Location = new System.Drawing.Point(3, 3);
            this.Image_tabControl.Name = "Image_tabControl";
            this.Image_tabControl.SelectedIndex = 0;
            this.Image_tabControl.Size = new System.Drawing.Size(1141, 798);
            this.Image_tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Image_panel);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1133, 760);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Image_panel
            // 
            this.Image_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Image_panel.Location = new System.Drawing.Point(3, 3);
            this.Image_panel.Name = "Image_panel";
            this.Image_panel.Size = new System.Drawing.Size(1127, 754);
            this.Image_panel.TabIndex = 0;
            // 
            // Parameter_panel
            // 
            this.Parameter_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Parameter_panel.Location = new System.Drawing.Point(1150, 3);
            this.Parameter_panel.Name = "Parameter_panel";
            this.Parameter_panel.Size = new System.Drawing.Size(759, 798);
            this.Parameter_panel.TabIndex = 1;
            // 
            // FromAlgorithmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel_background);
            this.Name = "FromAlgorithmTest";
            this.Size = new System.Drawing.Size(1918, 900);
            this.tableLayoutPanel_background.ResumeLayout(false);
            this.tableLayoutPanel_menu.ResumeLayout(false);
            this.tableLayoutPanel_menu.PerformLayout();
            this.tableLayoutPanel_Main.ResumeLayout(false);
            this.Image_tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_background;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_menu;
        private System.Windows.Forms.Button runalgorithm_btn;
        private System.Windows.Forms.ComboBox SelectAlgorithm_cb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox SelectSlot_cb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Main;
        private System.Windows.Forms.TabControl Image_tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel Image_panel;
        private System.Windows.Forms.Panel Parameter_panel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}
