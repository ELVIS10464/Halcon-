namespace AlgorithmProcess
{
    partial class MainForm
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.changeimagepath_btn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.menu_tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.parameter_btn = new System.Windows.Forms.Button();
            this.selectrecipe_btn = new System.Windows.Forms.Button();
            this.processstep_btn = new System.Windows.Forms.Button();
            this.runalgorithm_btn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.gui_tabPage = new System.Windows.Forms.TabPage();
            this.Foup_PictureBox = new System.Windows.Forms.PictureBox();
            this.stitch_tabPage = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.info_tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.Msg_RichTextBox = new System.Windows.Forms.RichTextBox();
            this.result_dataGridView = new System.Windows.Forms.DataGridView();
            this.Slot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Thickness = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Warpage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftGap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RightGap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Imagefile_lb = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.switchmode_cb = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.runmode_cb = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.menu_tableLayoutPanel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.gui_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Foup_PictureBox)).BeginInit();
            this.info_tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.result_dataGridView)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // changeimagepath_btn
            // 
            this.changeimagepath_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changeimagepath_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeimagepath_btn.Location = new System.Drawing.Point(3, 3);
            this.changeimagepath_btn.Name = "changeimagepath_btn";
            this.changeimagepath_btn.Size = new System.Drawing.Size(240, 257);
            this.changeimagepath_btn.TabIndex = 2;
            this.changeimagepath_btn.Text = "Change Image Path";
            this.changeimagepath_btn.UseVisualStyleBackColor = true;
            this.changeimagepath_btn.Click += new System.EventHandler(this.loadimage_btn_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(2564, 1410);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 7;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.Controls.Add(this.menu_tableLayoutPanel, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tabControl1, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel3, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel4, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.info_tableLayoutPanel, 6, 0);
            this.tableLayoutPanel3.Controls.Add(this.Imagefile_lb, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 83);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(2558, 1324);
            this.tableLayoutPanel3.TabIndex = 7;
            // 
            // menu_tableLayoutPanel
            // 
            this.menu_tableLayoutPanel.ColumnCount = 1;
            this.menu_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.menu_tableLayoutPanel.Controls.Add(this.parameter_btn, 0, 4);
            this.menu_tableLayoutPanel.Controls.Add(this.selectrecipe_btn, 0, 1);
            this.menu_tableLayoutPanel.Controls.Add(this.processstep_btn, 0, 3);
            this.menu_tableLayoutPanel.Controls.Add(this.runalgorithm_btn, 0, 2);
            this.menu_tableLayoutPanel.Controls.Add(this.changeimagepath_btn, 0, 0);
            this.menu_tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menu_tableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.menu_tableLayoutPanel.Name = "menu_tableLayoutPanel";
            this.menu_tableLayoutPanel.RowCount = 5;
            this.menu_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.menu_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.menu_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.menu_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.menu_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.menu_tableLayoutPanel.Size = new System.Drawing.Size(246, 1318);
            this.menu_tableLayoutPanel.TabIndex = 6;
            // 
            // parameter_btn
            // 
            this.parameter_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parameter_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parameter_btn.Location = new System.Drawing.Point(3, 1055);
            this.parameter_btn.Name = "parameter_btn";
            this.parameter_btn.Size = new System.Drawing.Size(240, 260);
            this.parameter_btn.TabIndex = 7;
            this.parameter_btn.Text = "Parameter Setting";
            this.parameter_btn.UseVisualStyleBackColor = true;
            // 
            // selectrecipe_btn
            // 
            this.selectrecipe_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectrecipe_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectrecipe_btn.Location = new System.Drawing.Point(3, 266);
            this.selectrecipe_btn.Name = "selectrecipe_btn";
            this.selectrecipe_btn.Size = new System.Drawing.Size(240, 257);
            this.selectrecipe_btn.TabIndex = 4;
            this.selectrecipe_btn.Text = "Select Recipe";
            this.selectrecipe_btn.UseVisualStyleBackColor = true;
            this.selectrecipe_btn.Click += new System.EventHandler(this.selectrecipe_btn_Click);
            // 
            // processstep_btn
            // 
            this.processstep_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processstep_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processstep_btn.Location = new System.Drawing.Point(3, 792);
            this.processstep_btn.Name = "processstep_btn";
            this.processstep_btn.Size = new System.Drawing.Size(240, 257);
            this.processstep_btn.TabIndex = 5;
            this.processstep_btn.Text = "Process Step";
            this.processstep_btn.UseVisualStyleBackColor = true;
            // 
            // runalgorithm_btn
            // 
            this.runalgorithm_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runalgorithm_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runalgorithm_btn.Location = new System.Drawing.Point(3, 529);
            this.runalgorithm_btn.Name = "runalgorithm_btn";
            this.runalgorithm_btn.Size = new System.Drawing.Size(240, 257);
            this.runalgorithm_btn.TabIndex = 4;
            this.runalgorithm_btn.Text = "Run Algorithm";
            this.runalgorithm_btn.UseVisualStyleBackColor = true;
            this.runalgorithm_btn.Click += new System.EventHandler(this.runalgorithm_btn_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.gui_tabPage);
            this.tabControl1.Controls.Add(this.stitch_tabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("新細明體", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControl1.Location = new System.Drawing.Point(653, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1132, 1318);
            this.tabControl1.TabIndex = 8;
            // 
            // gui_tabPage
            // 
            this.gui_tabPage.Controls.Add(this.Foup_PictureBox);
            this.gui_tabPage.Location = new System.Drawing.Point(4, 38);
            this.gui_tabPage.Name = "gui_tabPage";
            this.gui_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.gui_tabPage.Size = new System.Drawing.Size(1124, 1276);
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
            this.Foup_PictureBox.Size = new System.Drawing.Size(1118, 1270);
            this.Foup_PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Foup_PictureBox.TabIndex = 0;
            this.Foup_PictureBox.TabStop = false;
            // 
            // stitch_tabPage
            // 
            this.stitch_tabPage.Location = new System.Drawing.Point(4, 38);
            this.stitch_tabPage.Name = "stitch_tabPage";
            this.stitch_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.stitch_tabPage.Size = new System.Drawing.Size(1124, 1276);
            this.stitch_tabPage.TabIndex = 1;
            this.stitch_tabPage.Text = "Stitch";
            this.stitch_tabPage.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlText;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(255, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(3, 1318);
            this.panel2.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlText;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(643, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(4, 1318);
            this.panel3.TabIndex = 10;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ControlText;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(1791, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(4, 1318);
            this.panel4.TabIndex = 11;
            // 
            // info_tableLayoutPanel
            // 
            this.info_tableLayoutPanel.ColumnCount = 1;
            this.info_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.info_tableLayoutPanel.Controls.Add(this.panel5, 0, 1);
            this.info_tableLayoutPanel.Controls.Add(this.Msg_RichTextBox, 0, 2);
            this.info_tableLayoutPanel.Controls.Add(this.result_dataGridView, 6, 0);
            this.info_tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.info_tableLayoutPanel.Location = new System.Drawing.Point(1801, 3);
            this.info_tableLayoutPanel.Name = "info_tableLayoutPanel";
            this.info_tableLayoutPanel.RowCount = 3;
            this.info_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.info_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.info_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.info_tableLayoutPanel.Size = new System.Drawing.Size(754, 1318);
            this.info_tableLayoutPanel.TabIndex = 12;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ControlText;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 918);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(748, 4);
            this.panel5.TabIndex = 0;
            // 
            // Msg_RichTextBox
            // 
            this.Msg_RichTextBox.BackColor = System.Drawing.SystemColors.WindowText;
            this.Msg_RichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Msg_RichTextBox.Location = new System.Drawing.Point(3, 928);
            this.Msg_RichTextBox.Name = "Msg_RichTextBox";
            this.Msg_RichTextBox.Size = new System.Drawing.Size(748, 387);
            this.Msg_RichTextBox.TabIndex = 1;
            this.Msg_RichTextBox.Text = "";
            // 
            // result_dataGridView
            // 
            this.result_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.result_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Slot,
            this.Result,
            this.Thickness,
            this.Warpage,
            this.LeftGap,
            this.RightGap});
            this.result_dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.result_dataGridView.Location = new System.Drawing.Point(3, 3);
            this.result_dataGridView.Name = "result_dataGridView";
            this.result_dataGridView.RowHeadersWidth = 62;
            this.result_dataGridView.RowTemplate.Height = 31;
            this.result_dataGridView.Size = new System.Drawing.Size(748, 909);
            this.result_dataGridView.TabIndex = 2;
            // 
            // Slot
            // 
            this.Slot.HeaderText = "Slot";
            this.Slot.MinimumWidth = 8;
            this.Slot.Name = "Slot";
            this.Slot.Width = 50;
            // 
            // Result
            // 
            this.Result.HeaderText = "Result";
            this.Result.MinimumWidth = 8;
            this.Result.Name = "Result";
            this.Result.Width = 150;
            // 
            // Thickness
            // 
            this.Thickness.HeaderText = "Thickness";
            this.Thickness.MinimumWidth = 8;
            this.Thickness.Name = "Thickness";
            this.Thickness.Width = 80;
            // 
            // Warpage
            // 
            this.Warpage.HeaderText = "Warpage";
            this.Warpage.MinimumWidth = 8;
            this.Warpage.Name = "Warpage";
            this.Warpage.Width = 80;
            // 
            // LeftGap
            // 
            this.LeftGap.HeaderText = "Left Gap";
            this.LeftGap.MinimumWidth = 8;
            this.LeftGap.Name = "LeftGap";
            this.LeftGap.Width = 80;
            // 
            // RightGap
            // 
            this.RightGap.HeaderText = "Right Gap";
            this.RightGap.MinimumWidth = 8;
            this.RightGap.Name = "RightGap";
            this.RightGap.Width = 80;
            // 
            // Imagefile_lb
            // 
            this.Imagefile_lb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Imagefile_lb.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Imagefile_lb.FormattingEnabled = true;
            this.Imagefile_lb.ItemHeight = 24;
            this.Imagefile_lb.Location = new System.Drawing.Point(264, 3);
            this.Imagefile_lb.Name = "Imagefile_lb";
            this.Imagefile_lb.Size = new System.Drawing.Size(373, 1318);
            this.Imagefile_lb.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 73);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2558, 4);
            this.panel1.TabIndex = 8;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.Controls.Add(this.runmode_cb, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.switchmode_cb, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(2558, 64);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("標楷體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(420, 64);
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
            this.switchmode_cb.Location = new System.Drawing.Point(429, 3);
            this.switchmode_cb.Name = "switchmode_cb";
            this.switchmode_cb.Size = new System.Drawing.Size(420, 34);
            this.switchmode_cb.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("標楷體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(855, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(420, 64);
            this.label2.TabIndex = 2;
            this.label2.Text = "Run Mode";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.runmode_cb.Location = new System.Drawing.Point(1281, 3);
            this.runmode_cb.Name = "runmode_cb";
            this.runmode_cb.Size = new System.Drawing.Size(420, 34);
            this.runmode_cb.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2564, 1410);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "Main";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.menu_tableLayoutPanel.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.gui_tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Foup_PictureBox)).EndInit();
            this.info_tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.result_dataGridView)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button changeimagepath_btn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button runalgorithm_btn;
        private System.Windows.Forms.Button processstep_btn;
        private System.Windows.Forms.TableLayoutPanel menu_tableLayoutPanel;
        private System.Windows.Forms.Button selectrecipe_btn;
        private System.Windows.Forms.Button parameter_btn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage gui_tabPage;
        private System.Windows.Forms.TabPage stitch_tabPage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TableLayoutPanel info_tableLayoutPanel;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RichTextBox Msg_RichTextBox;
        private System.Windows.Forms.ListBox Imagefile_lb;
        private System.Windows.Forms.DataGridView result_dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Slot;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.DataGridViewTextBoxColumn Thickness;
        private System.Windows.Forms.DataGridViewTextBoxColumn Warpage;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeftGap;
        private System.Windows.Forms.DataGridViewTextBoxColumn RightGap;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox Foup_PictureBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox switchmode_cb;
        private System.Windows.Forms.ComboBox runmode_cb;
        private System.Windows.Forms.Label label2;
    }
}

