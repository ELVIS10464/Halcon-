namespace AlgorithmProcess.Window
{
    partial class DisplayResult
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel_menu = new System.Windows.Forms.TableLayoutPanel();
            this.runalgorithm_btn = new System.Windows.Forms.Button();
            this.runmode_cb = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.switchmode_cb = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel_display = new System.Windows.Forms.TableLayoutPanel();
            this.result_dataGridView = new System.Windows.Forms.DataGridView();
            this.Slot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Thickness = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Warpage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftGap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RightGap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.gui_tabPage = new System.Windows.Forms.TabPage();
            this.Foup_PictureBox = new System.Windows.Forms.PictureBox();
            this.stitch_tabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel_menu.SuspendLayout();
            this.tableLayoutPanel_display.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.result_dataGridView)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.gui_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Foup_PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel_menu, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel_display, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1918, 1318);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel_menu
            // 
            this.tableLayoutPanel_menu.ColumnCount = 5;
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.22222F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.22222F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.22222F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.22222F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel_menu.Controls.Add(this.runalgorithm_btn, 4, 0);
            this.tableLayoutPanel_menu.Controls.Add(this.runmode_cb, 3, 0);
            this.tableLayoutPanel_menu.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel_menu.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel_menu.Controls.Add(this.switchmode_cb, 1, 0);
            this.tableLayoutPanel_menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_menu.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel_menu.Name = "tableLayoutPanel_menu";
            this.tableLayoutPanel_menu.RowCount = 1;
            this.tableLayoutPanel_menu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_menu.Size = new System.Drawing.Size(1912, 125);
            this.tableLayoutPanel_menu.TabIndex = 10;
            // 
            // runalgorithm_btn
            // 
            this.runalgorithm_btn.BackColor = System.Drawing.Color.Transparent;
            this.runalgorithm_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.runalgorithm_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runalgorithm_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runalgorithm_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runalgorithm_btn.Location = new System.Drawing.Point(1699, 3);
            this.runalgorithm_btn.Name = "runalgorithm_btn";
            this.runalgorithm_btn.Size = new System.Drawing.Size(210, 119);
            this.runalgorithm_btn.TabIndex = 5;
            this.runalgorithm_btn.Text = "Run";
            this.runalgorithm_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.runalgorithm_btn.UseVisualStyleBackColor = false;
            // 
            // runmode_cb
            // 
            this.runmode_cb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runmode_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runmode_cb.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runmode_cb.FormattingEnabled = true;
            this.runmode_cb.Items.AddRange(new object[] {
            "Single Cycle",
            "Auto Cycle"});
            this.runmode_cb.Location = new System.Drawing.Point(1275, 3);
            this.runmode_cb.Name = "runmode_cb";
            this.runmode_cb.Size = new System.Drawing.Size(418, 34);
            this.runmode_cb.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("標楷體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(851, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(418, 125);
            this.label2.TabIndex = 2;
            this.label2.Text = "Run Mode";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("標楷體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(418, 125);
            this.label1.TabIndex = 0;
            this.label1.Text = "Switch Mode";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // switchmode_cb
            // 
            this.switchmode_cb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.switchmode_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.switchmode_cb.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.switchmode_cb.FormattingEnabled = true;
            this.switchmode_cb.Items.AddRange(new object[] {
            "Two Camera",
            "Three Camera"});
            this.switchmode_cb.Location = new System.Drawing.Point(427, 3);
            this.switchmode_cb.Name = "switchmode_cb";
            this.switchmode_cb.Size = new System.Drawing.Size(418, 34);
            this.switchmode_cb.TabIndex = 1;
            // 
            // tableLayoutPanel_display
            // 
            this.tableLayoutPanel_display.ColumnCount = 2;
            this.tableLayoutPanel_display.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel_display.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel_display.Controls.Add(this.result_dataGridView, 1, 0);
            this.tableLayoutPanel_display.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel_display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_display.Location = new System.Drawing.Point(3, 134);
            this.tableLayoutPanel_display.Name = "tableLayoutPanel_display";
            this.tableLayoutPanel_display.RowCount = 1;
            this.tableLayoutPanel_display.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_display.Size = new System.Drawing.Size(1912, 1181);
            this.tableLayoutPanel_display.TabIndex = 11;
            // 
            // result_dataGridView
            // 
            this.result_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.result_dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.result_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.result_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Slot,
            this.Result,
            this.Thickness,
            this.Warpage,
            this.LeftGap,
            this.RightGap});
            this.result_dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.result_dataGridView.Location = new System.Drawing.Point(1150, 3);
            this.result_dataGridView.Name = "result_dataGridView";
            this.result_dataGridView.RowHeadersVisible = false;
            this.result_dataGridView.RowHeadersWidth = 62;
            this.result_dataGridView.RowTemplate.Height = 31;
            this.result_dataGridView.Size = new System.Drawing.Size(759, 1175);
            this.result_dataGridView.TabIndex = 10;
            // 
            // Slot
            // 
            this.Slot.HeaderText = "Slot";
            this.Slot.MinimumWidth = 8;
            this.Slot.Name = "Slot";
            // 
            // Result
            // 
            this.Result.HeaderText = "Result";
            this.Result.MinimumWidth = 8;
            this.Result.Name = "Result";
            // 
            // Thickness
            // 
            this.Thickness.HeaderText = "Thickness";
            this.Thickness.MinimumWidth = 8;
            this.Thickness.Name = "Thickness";
            // 
            // Warpage
            // 
            this.Warpage.HeaderText = "Warpage";
            this.Warpage.MinimumWidth = 8;
            this.Warpage.Name = "Warpage";
            // 
            // LeftGap
            // 
            this.LeftGap.HeaderText = "Left Gap";
            this.LeftGap.MinimumWidth = 8;
            this.LeftGap.Name = "LeftGap";
            // 
            // RightGap
            // 
            this.RightGap.HeaderText = "Right Gap";
            this.RightGap.MinimumWidth = 8;
            this.RightGap.Name = "RightGap";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.gui_tabPage);
            this.tabControl1.Controls.Add(this.stitch_tabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("新細明體", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1141, 1175);
            this.tabControl1.TabIndex = 9;
            // 
            // gui_tabPage
            // 
            this.gui_tabPage.Controls.Add(this.Foup_PictureBox);
            this.gui_tabPage.Location = new System.Drawing.Point(4, 38);
            this.gui_tabPage.Name = "gui_tabPage";
            this.gui_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.gui_tabPage.Size = new System.Drawing.Size(1133, 1133);
            this.gui_tabPage.TabIndex = 0;
            this.gui_tabPage.Text = "GUI";
            this.gui_tabPage.UseVisualStyleBackColor = true;
            // 
            // Foup_PictureBox
            // 
            this.Foup_PictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Foup_PictureBox.Location = new System.Drawing.Point(3, 3);
            this.Foup_PictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.Foup_PictureBox.Name = "Foup_PictureBox";
            this.Foup_PictureBox.Size = new System.Drawing.Size(1127, 1127);
            this.Foup_PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Foup_PictureBox.TabIndex = 0;
            this.Foup_PictureBox.TabStop = false;
            // 
            // stitch_tabPage
            // 
            this.stitch_tabPage.Location = new System.Drawing.Point(4, 38);
            this.stitch_tabPage.Name = "stitch_tabPage";
            this.stitch_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.stitch_tabPage.Size = new System.Drawing.Size(1133, 1133);
            this.stitch_tabPage.TabIndex = 1;
            this.stitch_tabPage.Text = "Stitch";
            this.stitch_tabPage.UseVisualStyleBackColor = true;
            // 
            // DisplayResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DisplayResult";
            this.Size = new System.Drawing.Size(1918, 1318);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel_menu.ResumeLayout(false);
            this.tableLayoutPanel_menu.PerformLayout();
            this.tableLayoutPanel_display.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.result_dataGridView)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.gui_tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Foup_PictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_menu;
        private System.Windows.Forms.ComboBox runmode_cb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox switchmode_cb;
        private System.Windows.Forms.Button runalgorithm_btn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_display;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage gui_tabPage;
        private System.Windows.Forms.PictureBox Foup_PictureBox;
        private System.Windows.Forms.TabPage stitch_tabPage;
        private System.Windows.Forms.DataGridView result_dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Slot;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.DataGridViewTextBoxColumn Thickness;
        private System.Windows.Forms.DataGridViewTextBoxColumn Warpage;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeftGap;
        private System.Windows.Forms.DataGridViewTextBoxColumn RightGap;
    }
}
