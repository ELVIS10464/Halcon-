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
            this.home_btn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.menu_tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ImageCropData_btn = new System.Windows.Forms.Button();
            this.parameter_btn = new System.Windows.Forms.Button();
            this.selectrecipe_btn = new System.Windows.Forms.Button();
            this.processstep_btn = new System.Windows.Forms.Button();
            this.runalgorithm_btn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.Msg_RichTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.loadImagefile_lb = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.saveImagefile_lb = new System.Windows.Forms.ListBox();
            this.main_panel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel_menu = new System.Windows.Forms.TableLayoutPanel();
            this.recipename_tb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.changesetting_btn = new System.Windows.Forms.Button();
            this.loadimagepath_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveimagepath_tb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.menu_tableLayoutPanel.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel_menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // home_btn
            // 
            this.home_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.home_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.home_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.home_btn.Location = new System.Drawing.Point(3, 3);
            this.home_btn.Name = "home_btn";
            this.home_btn.Size = new System.Drawing.Size(201, 213);
            this.home_btn.TabIndex = 2;
            this.home_btn.Text = "Home";
            this.home_btn.UseVisualStyleBackColor = true;
            this.home_btn.Click += new System.EventHandler(this.Btn_ClickEvent);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel_menu, 0, 0);
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
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.421053F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.78947F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.78947F));
            this.tableLayoutPanel3.Controls.Add(this.menu_tableLayoutPanel, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel3, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.main_panel, 2, 0);
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
            this.menu_tableLayoutPanel.Controls.Add(this.ImageCropData_btn, 0, 5);
            this.menu_tableLayoutPanel.Controls.Add(this.parameter_btn, 0, 4);
            this.menu_tableLayoutPanel.Controls.Add(this.selectrecipe_btn, 0, 1);
            this.menu_tableLayoutPanel.Controls.Add(this.processstep_btn, 0, 3);
            this.menu_tableLayoutPanel.Controls.Add(this.runalgorithm_btn, 0, 2);
            this.menu_tableLayoutPanel.Controls.Add(this.home_btn, 0, 0);
            this.menu_tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menu_tableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.menu_tableLayoutPanel.Name = "menu_tableLayoutPanel";
            this.menu_tableLayoutPanel.RowCount = 6;
            this.menu_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66736F));
            this.menu_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66736F));
            this.menu_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66736F));
            this.menu_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66736F));
            this.menu_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66736F));
            this.menu_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6632F));
            this.menu_tableLayoutPanel.Size = new System.Drawing.Size(207, 1318);
            this.menu_tableLayoutPanel.TabIndex = 6;
            // 
            // ImageCropData_btn
            // 
            this.ImageCropData_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageCropData_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImageCropData_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImageCropData_btn.Location = new System.Drawing.Point(3, 1098);
            this.ImageCropData_btn.Name = "ImageCropData_btn";
            this.ImageCropData_btn.Size = new System.Drawing.Size(201, 217);
            this.ImageCropData_btn.TabIndex = 8;
            this.ImageCropData_btn.Text = "Image Crop Data";
            this.ImageCropData_btn.UseVisualStyleBackColor = true;
            this.ImageCropData_btn.Click += new System.EventHandler(this.Btn_ClickEvent);
            // 
            // parameter_btn
            // 
            this.parameter_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parameter_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.parameter_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parameter_btn.Location = new System.Drawing.Point(3, 879);
            this.parameter_btn.Name = "parameter_btn";
            this.parameter_btn.Size = new System.Drawing.Size(201, 213);
            this.parameter_btn.TabIndex = 7;
            this.parameter_btn.Text = "Parameter Setting";
            this.parameter_btn.UseVisualStyleBackColor = true;
            // 
            // selectrecipe_btn
            // 
            this.selectrecipe_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectrecipe_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectrecipe_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectrecipe_btn.Location = new System.Drawing.Point(3, 222);
            this.selectrecipe_btn.Name = "selectrecipe_btn";
            this.selectrecipe_btn.Size = new System.Drawing.Size(201, 213);
            this.selectrecipe_btn.TabIndex = 4;
            this.selectrecipe_btn.Text = "Select Recipe";
            this.selectrecipe_btn.UseVisualStyleBackColor = true;
            this.selectrecipe_btn.Click += new System.EventHandler(this.selectrecipe_btn_Click);
            // 
            // processstep_btn
            // 
            this.processstep_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processstep_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.processstep_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processstep_btn.Location = new System.Drawing.Point(3, 660);
            this.processstep_btn.Name = "processstep_btn";
            this.processstep_btn.Size = new System.Drawing.Size(201, 213);
            this.processstep_btn.TabIndex = 5;
            this.processstep_btn.Text = "Process Step";
            this.processstep_btn.UseVisualStyleBackColor = true;
            // 
            // runalgorithm_btn
            // 
            this.runalgorithm_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runalgorithm_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runalgorithm_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runalgorithm_btn.Location = new System.Drawing.Point(3, 441);
            this.runalgorithm_btn.Name = "runalgorithm_btn";
            this.runalgorithm_btn.Size = new System.Drawing.Size(201, 213);
            this.runalgorithm_btn.TabIndex = 4;
            this.runalgorithm_btn.Text = "Run Algorithm";
            this.runalgorithm_btn.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlText;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(216, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(3, 1318);
            this.panel2.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlText;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(2149, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(4, 1318);
            this.panel3.TabIndex = 10;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.Msg_RichTextBox, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(2159, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(396, 1318);
            this.tableLayoutPanel4.TabIndex = 16;
            // 
            // Msg_RichTextBox
            // 
            this.Msg_RichTextBox.BackColor = System.Drawing.SystemColors.WindowText;
            this.Msg_RichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Msg_RichTextBox.Location = new System.Drawing.Point(3, 661);
            this.Msg_RichTextBox.Name = "Msg_RichTextBox";
            this.Msg_RichTextBox.Size = new System.Drawing.Size(390, 654);
            this.Msg_RichTextBox.TabIndex = 4;
            this.Msg_RichTextBox.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.loadImagefile_lb);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 323);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load Image List";
            // 
            // loadImagefile_lb
            // 
            this.loadImagefile_lb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadImagefile_lb.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.loadImagefile_lb.FormattingEnabled = true;
            this.loadImagefile_lb.ItemHeight = 24;
            this.loadImagefile_lb.Location = new System.Drawing.Point(3, 31);
            this.loadImagefile_lb.Name = "loadImagefile_lb";
            this.loadImagefile_lb.Size = new System.Drawing.Size(384, 289);
            this.loadImagefile_lb.TabIndex = 15;
            this.loadImagefile_lb.SelectedIndexChanged += new System.EventHandler(this.loadImagefile_lb_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.saveImagefile_lb);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 332);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(390, 323);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Save Image List";
            // 
            // saveImagefile_lb
            // 
            this.saveImagefile_lb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveImagefile_lb.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.saveImagefile_lb.FormattingEnabled = true;
            this.saveImagefile_lb.ItemHeight = 24;
            this.saveImagefile_lb.Location = new System.Drawing.Point(3, 31);
            this.saveImagefile_lb.Name = "saveImagefile_lb";
            this.saveImagefile_lb.Size = new System.Drawing.Size(384, 289);
            this.saveImagefile_lb.TabIndex = 16;
            // 
            // main_panel
            // 
            this.main_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_panel.Location = new System.Drawing.Point(225, 3);
            this.main_panel.Name = "main_panel";
            this.main_panel.Size = new System.Drawing.Size(1918, 1318);
            this.main_panel.TabIndex = 17;
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
            // tableLayoutPanel_menu
            // 
            this.tableLayoutPanel_menu.ColumnCount = 7;
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.502483F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.02691F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.06688F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.02691F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.67494F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.02691F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.67494F));
            this.tableLayoutPanel_menu.Controls.Add(this.recipename_tb, 4, 0);
            this.tableLayoutPanel_menu.Controls.Add(this.label3, 3, 0);
            this.tableLayoutPanel_menu.Controls.Add(this.changesetting_btn, 0, 0);
            this.tableLayoutPanel_menu.Controls.Add(this.loadimagepath_tb, 2, 0);
            this.tableLayoutPanel_menu.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel_menu.Controls.Add(this.saveimagepath_tb, 6, 0);
            this.tableLayoutPanel_menu.Controls.Add(this.label2, 5, 0);
            this.tableLayoutPanel_menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_menu.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel_menu.Name = "tableLayoutPanel_menu";
            this.tableLayoutPanel_menu.RowCount = 1;
            this.tableLayoutPanel_menu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_menu.Size = new System.Drawing.Size(2558, 64);
            this.tableLayoutPanel_menu.TabIndex = 9;
            // 
            // recipename_tb
            // 
            this.recipename_tb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recipename_tb.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recipename_tb.Location = new System.Drawing.Point(1169, 3);
            this.recipename_tb.Name = "recipename_tb";
            this.recipename_tb.Size = new System.Drawing.Size(548, 35);
            this.recipename_tb.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("標楷體", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(887, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(276, 64);
            this.label3.TabIndex = 7;
            this.label3.Text = "Recipe Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // changesetting_btn
            // 
            this.changesetting_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changesetting_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.changesetting_btn.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changesetting_btn.Location = new System.Drawing.Point(3, 3);
            this.changesetting_btn.Name = "changesetting_btn";
            this.changesetting_btn.Size = new System.Drawing.Size(160, 58);
            this.changesetting_btn.TabIndex = 6;
            this.changesetting_btn.Text = "Change Setting";
            this.changesetting_btn.UseVisualStyleBackColor = true;
            this.changesetting_btn.Click += new System.EventHandler(this.changesetting_btn_Click);
            // 
            // loadimagepath_tb
            // 
            this.loadimagepath_tb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadimagepath_tb.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadimagepath_tb.Location = new System.Drawing.Point(451, 3);
            this.loadimagepath_tb.Name = "loadimagepath_tb";
            this.loadimagepath_tb.Size = new System.Drawing.Size(430, 35);
            this.loadimagepath_tb.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("標楷體", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(169, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 64);
            this.label1.TabIndex = 2;
            this.label1.Text = "Load Image Path";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saveimagepath_tb
            // 
            this.saveimagepath_tb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveimagepath_tb.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveimagepath_tb.Location = new System.Drawing.Point(2005, 3);
            this.saveimagepath_tb.Name = "saveimagepath_tb";
            this.saveimagepath_tb.Size = new System.Drawing.Size(550, 35);
            this.saveimagepath_tb.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("標楷體", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(1723, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(276, 64);
            this.label2.TabIndex = 3;
            this.label2.Text = "Save Image Path";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2564, 1410);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.menu_tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel_menu.ResumeLayout(false);
            this.tableLayoutPanel_menu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button home_btn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button runalgorithm_btn;
        private System.Windows.Forms.Button processstep_btn;
        private System.Windows.Forms.TableLayoutPanel menu_tableLayoutPanel;
        private System.Windows.Forms.Button selectrecipe_btn;
        private System.Windows.Forms.Button parameter_btn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button ImageCropData_btn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_menu;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ListBox saveImagefile_lb;
        private System.Windows.Forms.ListBox loadImagefile_lb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox Msg_RichTextBox;
        private System.Windows.Forms.TextBox saveimagepath_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox loadimagepath_tb;
        private System.Windows.Forms.Panel main_panel;
        private System.Windows.Forms.Button changesetting_btn;
        private System.Windows.Forms.TextBox recipename_tb;
        private System.Windows.Forms.Label label3;
    }
}

