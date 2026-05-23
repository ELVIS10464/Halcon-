namespace AlgorithmProcess.Window
{
    partial class FromParameterSetting_StepDisplay
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView_SingleVariable = new System.Windows.Forms.DataGridView();
            this.dataGridView_Array = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel_Parament = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Image_Panel = new System.Windows.Forms.Panel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_SingleVariable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Array)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView_SingleVariable, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView_Array, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel_Parament, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(553, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(362, 618);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGridView_SingleVariable
            // 
            this.dataGridView_SingleVariable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_SingleVariable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_SingleVariable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Name,
            this.Description,
            this.Value});
            this.dataGridView_SingleVariable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_SingleVariable.Location = new System.Drawing.Point(3, 208);
            this.dataGridView_SingleVariable.Name = "dataGridView_SingleVariable";
            this.dataGridView_SingleVariable.RowHeadersVisible = false;
            this.dataGridView_SingleVariable.RowHeadersWidth = 62;
            this.dataGridView_SingleVariable.RowTemplate.Height = 31;
            this.dataGridView_SingleVariable.Size = new System.Drawing.Size(356, 199);
            this.dataGridView_SingleVariable.TabIndex = 0;
            // 
            // dataGridView_Array
            // 
            this.dataGridView_Array.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Array.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Array.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dataGridView_Array.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Array.Location = new System.Drawing.Point(3, 413);
            this.dataGridView_Array.Name = "dataGridView_Array";
            this.dataGridView_Array.RowHeadersVisible = false;
            this.dataGridView_Array.RowHeadersWidth = 62;
            this.dataGridView_Array.RowTemplate.Height = 31;
            this.dataGridView_Array.Size = new System.Drawing.Size(356, 202);
            this.dataGridView_Array.TabIndex = 1;
            // 
            // flowLayoutPanel_Parament
            // 
            this.flowLayoutPanel_Parament.AutoScroll = true;
            this.flowLayoutPanel_Parament.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_Parament.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_Parament.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel_Parament.Name = "flowLayoutPanel_Parament";
            this.flowLayoutPanel_Parament.Size = new System.Drawing.Size(356, 199);
            this.flowLayoutPanel_Parament.TabIndex = 2;
            this.flowLayoutPanel_Parament.WrapContents = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.Image_Panel, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(918, 624);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // Image_Panel
            // 
            this.Image_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Image_Panel.Location = new System.Drawing.Point(3, 3);
            this.Image_Panel.Name = "Image_Panel";
            this.Image_Panel.Size = new System.Drawing.Size(544, 618);
            this.Image_Panel.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Description";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Value";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // col_Name
            // 
            this.col_Name.DataPropertyName = "Name";
            this.col_Name.HeaderText = "Name";
            this.col_Name.MinimumWidth = 8;
            this.col_Name.Name = "col_Name";
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.MinimumWidth = 8;
            this.Description.Name = "Description";
            // 
            // Value
            // 
            this.Value.DataPropertyName = "Value";
            this.Value.HeaderText = "Value";
            this.Value.MinimumWidth = 8;
            this.Value.Name = "Value";
            // 
            // FromParameterSetting_StepDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "FromParameterSetting_StepDisplay";
            this.Size = new System.Drawing.Size(918, 624);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_SingleVariable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Array)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView_SingleVariable;
        private System.Windows.Forms.DataGridView dataGridView_Array;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Parament;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel Image_Panel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
    }
}