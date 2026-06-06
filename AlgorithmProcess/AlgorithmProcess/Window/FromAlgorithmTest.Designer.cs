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
            this.AlgorithmMethod_tabControl = new System.Windows.Forms.TabControl();
            this.MeasurePos = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView_Point = new System.Windows.Forms.DataGridView();
            this.No_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Row = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel_Button = new System.Windows.Forms.TableLayoutPanel();
            this.DrawLine_btn = new System.Windows.Forms.Button();
            this.Clear_btn = new System.Windows.Forms.Button();
            this.tableLayoutPanel_Extract = new System.Windows.Forms.TableLayoutPanel();
            this.ROIWidth_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.Sigma_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.Amplitude_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.Sigma_trackBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Amplitude_trackBar = new System.Windows.Forms.TrackBar();
            this.ROIWidth_trackBar = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel_Select = new System.Windows.Forms.TableLayoutPanel();
            this.Position_comboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Direction_comboBox = new System.Windows.Forms.ComboBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel_background.SuspendLayout();
            this.tableLayoutPanel_menu.SuspendLayout();
            this.tableLayoutPanel_Main.SuspendLayout();
            this.Image_tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.Parameter_panel.SuspendLayout();
            this.AlgorithmMethod_tabControl.SuspendLayout();
            this.MeasurePos.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Point)).BeginInit();
            this.tableLayoutPanel_Button.SuspendLayout();
            this.tableLayoutPanel_Extract.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ROIWidth_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sigma_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amplitude_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sigma_trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amplitude_trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ROIWidth_trackBar)).BeginInit();
            this.tableLayoutPanel_Select.SuspendLayout();
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
            this.SelectAlgorithm_cb.Items.AddRange(new object[] {
            "Measure Pos",
            "Binarization Threshold"});
            this.SelectAlgorithm_cb.Location = new System.Drawing.Point(1149, 3);
            this.SelectAlgorithm_cb.Name = "SelectAlgorithm_cb";
            this.SelectAlgorithm_cb.Size = new System.Drawing.Size(376, 40);
            this.SelectAlgorithm_cb.TabIndex = 12;
            this.SelectAlgorithm_cb.SelectedIndexChanged += new System.EventHandler(this.SelectAlgorithm_cb_SelectedIndexChanged);
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
            this.Image_tabControl.SelectedIndexChanged += new System.EventHandler(this.Image_tabControl_SelectedIndexChanged);
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
            this.Parameter_panel.Controls.Add(this.AlgorithmMethod_tabControl);
            this.Parameter_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Parameter_panel.Location = new System.Drawing.Point(1150, 3);
            this.Parameter_panel.Name = "Parameter_panel";
            this.Parameter_panel.Size = new System.Drawing.Size(759, 798);
            this.Parameter_panel.TabIndex = 1;
            // 
            // AlgorithmMethod_tabControl
            // 
            this.AlgorithmMethod_tabControl.Controls.Add(this.MeasurePos);
            this.AlgorithmMethod_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AlgorithmMethod_tabControl.Location = new System.Drawing.Point(0, 0);
            this.AlgorithmMethod_tabControl.Name = "AlgorithmMethod_tabControl";
            this.AlgorithmMethod_tabControl.SelectedIndex = 0;
            this.AlgorithmMethod_tabControl.Size = new System.Drawing.Size(759, 798);
            this.AlgorithmMethod_tabControl.TabIndex = 0;
            // 
            // MeasurePos
            // 
            this.MeasurePos.Controls.Add(this.tableLayoutPanel1);
            this.MeasurePos.Location = new System.Drawing.Point(4, 28);
            this.MeasurePos.Name = "MeasurePos";
            this.MeasurePos.Padding = new System.Windows.Forms.Padding(3);
            this.MeasurePos.Size = new System.Drawing.Size(751, 766);
            this.MeasurePos.TabIndex = 0;
            this.MeasurePos.Text = "MeasurePos";
            this.MeasurePos.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView_Point, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel_Button, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel_Extract, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel_Select, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.18182F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.45454F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(745, 760);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // dataGridView_Point
            // 
            this.dataGridView_Point.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Point.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Point.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No_Name,
            this.Row,
            this.Column});
            this.dataGridView_Point.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Point.Location = new System.Drawing.Point(3, 417);
            this.dataGridView_Point.Name = "dataGridView_Point";
            this.dataGridView_Point.RowHeadersVisible = false;
            this.dataGridView_Point.RowHeadersWidth = 62;
            this.dataGridView_Point.RowTemplate.Height = 31;
            this.dataGridView_Point.Size = new System.Drawing.Size(739, 340);
            this.dataGridView_Point.TabIndex = 3;
            // 
            // No_Name
            // 
            this.No_Name.DataPropertyName = "No";
            this.No_Name.HeaderText = "No";
            this.No_Name.MinimumWidth = 8;
            this.No_Name.Name = "No_Name";
            // 
            // Row
            // 
            this.Row.DataPropertyName = "Row";
            this.Row.HeaderText = "Row";
            this.Row.MinimumWidth = 8;
            this.Row.Name = "Row";
            // 
            // Column
            // 
            this.Column.DataPropertyName = "Column";
            this.Column.HeaderText = "Column";
            this.Column.MinimumWidth = 8;
            this.Column.Name = "Column";
            // 
            // tableLayoutPanel_Button
            // 
            this.tableLayoutPanel_Button.ColumnCount = 2;
            this.tableLayoutPanel_Button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Button.Controls.Add(this.DrawLine_btn, 0, 0);
            this.tableLayoutPanel_Button.Controls.Add(this.Clear_btn, 1, 0);
            this.tableLayoutPanel_Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Button.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel_Button.Name = "tableLayoutPanel_Button";
            this.tableLayoutPanel_Button.RowCount = 1;
            this.tableLayoutPanel_Button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tableLayoutPanel_Button.Size = new System.Drawing.Size(739, 63);
            this.tableLayoutPanel_Button.TabIndex = 0;
            // 
            // DrawLine_btn
            // 
            this.DrawLine_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawLine_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawLine_btn.Location = new System.Drawing.Point(3, 3);
            this.DrawLine_btn.Name = "DrawLine_btn";
            this.DrawLine_btn.Size = new System.Drawing.Size(363, 57);
            this.DrawLine_btn.TabIndex = 0;
            this.DrawLine_btn.Text = "Draw Line";
            this.DrawLine_btn.UseVisualStyleBackColor = true;
            this.DrawLine_btn.Click += new System.EventHandler(this.MeasurePos_btn_Click);
            // 
            // Clear_btn
            // 
            this.Clear_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Clear_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clear_btn.Location = new System.Drawing.Point(372, 3);
            this.Clear_btn.Name = "Clear_btn";
            this.Clear_btn.Size = new System.Drawing.Size(364, 57);
            this.Clear_btn.TabIndex = 1;
            this.Clear_btn.Text = "Clear";
            this.Clear_btn.UseVisualStyleBackColor = true;
            this.Clear_btn.Click += new System.EventHandler(this.MeasurePos_btn_Click);
            // 
            // tableLayoutPanel_Extract
            // 
            this.tableLayoutPanel_Extract.ColumnCount = 3;
            this.tableLayoutPanel_Extract.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel_Extract.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel_Extract.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel_Extract.Controls.Add(this.ROIWidth_numericUpDown, 2, 2);
            this.tableLayoutPanel_Extract.Controls.Add(this.Sigma_numericUpDown, 2, 1);
            this.tableLayoutPanel_Extract.Controls.Add(this.Amplitude_numericUpDown, 2, 0);
            this.tableLayoutPanel_Extract.Controls.Add(this.Sigma_trackBar, 1, 1);
            this.tableLayoutPanel_Extract.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel_Extract.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel_Extract.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel_Extract.Controls.Add(this.Amplitude_trackBar, 1, 0);
            this.tableLayoutPanel_Extract.Controls.Add(this.ROIWidth_trackBar, 1, 2);
            this.tableLayoutPanel_Extract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Extract.Location = new System.Drawing.Point(3, 72);
            this.tableLayoutPanel_Extract.Name = "tableLayoutPanel_Extract";
            this.tableLayoutPanel_Extract.RowCount = 3;
            this.tableLayoutPanel_Extract.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_Extract.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_Extract.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_Extract.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_Extract.Size = new System.Drawing.Size(739, 201);
            this.tableLayoutPanel_Extract.TabIndex = 1;
            // 
            // ROIWidth_numericUpDown
            // 
            this.ROIWidth_numericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ROIWidth_numericUpDown.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ROIWidth_numericUpDown.Location = new System.Drawing.Point(593, 138);
            this.ROIWidth_numericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.ROIWidth_numericUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.ROIWidth_numericUpDown.Name = "ROIWidth_numericUpDown";
            this.ROIWidth_numericUpDown.Size = new System.Drawing.Size(143, 33);
            this.ROIWidth_numericUpDown.TabIndex = 8;
            this.ROIWidth_numericUpDown.ValueChanged += new System.EventHandler(this.NumericUpDown_ValueChanged);
            // 
            // Sigma_numericUpDown
            // 
            this.Sigma_numericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sigma_numericUpDown.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sigma_numericUpDown.Location = new System.Drawing.Point(593, 71);
            this.Sigma_numericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.Sigma_numericUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.Sigma_numericUpDown.Name = "Sigma_numericUpDown";
            this.Sigma_numericUpDown.Size = new System.Drawing.Size(143, 33);
            this.Sigma_numericUpDown.TabIndex = 7;
            this.Sigma_numericUpDown.ValueChanged += new System.EventHandler(this.NumericUpDown_ValueChanged);
            // 
            // Amplitude_numericUpDown
            // 
            this.Amplitude_numericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Amplitude_numericUpDown.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Amplitude_numericUpDown.Location = new System.Drawing.Point(593, 4);
            this.Amplitude_numericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.Amplitude_numericUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.Amplitude_numericUpDown.Name = "Amplitude_numericUpDown";
            this.Amplitude_numericUpDown.Size = new System.Drawing.Size(143, 33);
            this.Amplitude_numericUpDown.TabIndex = 6;
            this.Amplitude_numericUpDown.ValueChanged += new System.EventHandler(this.NumericUpDown_ValueChanged);
            // 
            // Sigma_trackBar
            // 
            this.Sigma_trackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sigma_trackBar.Location = new System.Drawing.Point(150, 70);
            this.Sigma_trackBar.Name = "Sigma_trackBar";
            this.Sigma_trackBar.Size = new System.Drawing.Size(437, 61);
            this.Sigma_trackBar.TabIndex = 5;
            this.Sigma_trackBar.Scroll += new System.EventHandler(this.TrackBar_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 67);
            this.label2.TabIndex = 0;
            this.label2.Text = "Amplitude";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 67);
            this.label4.TabIndex = 1;
            this.label4.Text = "Sigma";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 67);
            this.label5.TabIndex = 2;
            this.label5.Text = "ROI Width";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Amplitude_trackBar
            // 
            this.Amplitude_trackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Amplitude_trackBar.Location = new System.Drawing.Point(150, 3);
            this.Amplitude_trackBar.Maximum = 255;
            this.Amplitude_trackBar.Name = "Amplitude_trackBar";
            this.Amplitude_trackBar.Size = new System.Drawing.Size(437, 61);
            this.Amplitude_trackBar.TabIndex = 3;
            this.Amplitude_trackBar.Scroll += new System.EventHandler(this.TrackBar_Scroll);
            // 
            // ROIWidth_trackBar
            // 
            this.ROIWidth_trackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ROIWidth_trackBar.Location = new System.Drawing.Point(150, 137);
            this.ROIWidth_trackBar.Name = "ROIWidth_trackBar";
            this.ROIWidth_trackBar.Size = new System.Drawing.Size(437, 61);
            this.ROIWidth_trackBar.TabIndex = 4;
            this.ROIWidth_trackBar.Scroll += new System.EventHandler(this.TrackBar_Scroll);
            // 
            // tableLayoutPanel_Select
            // 
            this.tableLayoutPanel_Select.ColumnCount = 2;
            this.tableLayoutPanel_Select.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Select.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Select.Controls.Add(this.Position_comboBox, 1, 1);
            this.tableLayoutPanel_Select.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel_Select.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel_Select.Controls.Add(this.Direction_comboBox, 1, 0);
            this.tableLayoutPanel_Select.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Select.Location = new System.Drawing.Point(3, 279);
            this.tableLayoutPanel_Select.Name = "tableLayoutPanel_Select";
            this.tableLayoutPanel_Select.RowCount = 2;
            this.tableLayoutPanel_Select.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Select.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Select.Size = new System.Drawing.Size(739, 132);
            this.tableLayoutPanel_Select.TabIndex = 2;
            // 
            // Position_comboBox
            // 
            this.Position_comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Position_comboBox.FormattingEnabled = true;
            this.Position_comboBox.Location = new System.Drawing.Point(372, 69);
            this.Position_comboBox.Name = "Position_comboBox";
            this.Position_comboBox.Size = new System.Drawing.Size(364, 26);
            this.Position_comboBox.TabIndex = 6;
            this.Position_comboBox.SelectedIndexChanged += new System.EventHandler(this.Direction_Position_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(363, 66);
            this.label6.TabIndex = 5;
            this.label6.Text = "Position";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(363, 66);
            this.label7.TabIndex = 3;
            this.label7.Text = "Direction";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Direction_comboBox
            // 
            this.Direction_comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Direction_comboBox.FormattingEnabled = true;
            this.Direction_comboBox.Items.AddRange(new object[] {
            "All",
            "Positive",
            "Negative"});
            this.Direction_comboBox.Location = new System.Drawing.Point(372, 3);
            this.Direction_comboBox.Name = "Direction_comboBox";
            this.Direction_comboBox.Size = new System.Drawing.Size(364, 26);
            this.Direction_comboBox.TabIndex = 4;
            this.Direction_comboBox.SelectedIndexChanged += new System.EventHandler(this.Direction_Position_SelectedIndexChanged);
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
            this.Parameter_panel.ResumeLayout(false);
            this.AlgorithmMethod_tabControl.ResumeLayout(false);
            this.MeasurePos.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Point)).EndInit();
            this.tableLayoutPanel_Button.ResumeLayout(false);
            this.tableLayoutPanel_Extract.ResumeLayout(false);
            this.tableLayoutPanel_Extract.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ROIWidth_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sigma_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amplitude_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sigma_trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amplitude_trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ROIWidth_trackBar)).EndInit();
            this.tableLayoutPanel_Select.ResumeLayout(false);
            this.tableLayoutPanel_Select.PerformLayout();
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
        private System.Windows.Forms.TabControl AlgorithmMethod_tabControl;
        private System.Windows.Forms.TabPage MeasurePos;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView_Point;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Button;
        private System.Windows.Forms.Button DrawLine_btn;
        private System.Windows.Forms.Button Clear_btn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Extract;
        private System.Windows.Forms.NumericUpDown ROIWidth_numericUpDown;
        private System.Windows.Forms.NumericUpDown Sigma_numericUpDown;
        private System.Windows.Forms.NumericUpDown Amplitude_numericUpDown;
        private System.Windows.Forms.TrackBar Sigma_trackBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar Amplitude_trackBar;
        private System.Windows.Forms.TrackBar ROIWidth_trackBar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Select;
        private System.Windows.Forms.ComboBox Position_comboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox Direction_comboBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn No_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Row;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column;
    }
}
