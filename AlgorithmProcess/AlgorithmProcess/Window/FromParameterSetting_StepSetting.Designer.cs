namespace AlgorithmProcess.Window
{
    partial class FromParameterSetting_StepSetting
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

            // ✨ 1. 將變數名稱由 Name 改為 col_Name
            this.col_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView_Array = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flowLayoutPanel_Parament = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_SingleVariable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Array)).BeginInit();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(503, 624);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGridView_SingleVariable
            // 
            this.dataGridView_SingleVariable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_SingleVariable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_SingleVariable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Name, // ✨ 2. 這裡同步改名
            this.Value});
            this.dataGridView_SingleVariable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_SingleVariable.Location = new System.Drawing.Point(3, 211);
            this.dataGridView_SingleVariable.Name = "dataGridView_SingleVariable";
            this.dataGridView_SingleVariable.RowHeadersVisible = false;
            this.dataGridView_SingleVariable.RowHeadersWidth = 62;
            this.dataGridView_SingleVariable.RowTemplate.Height = 31;
            this.dataGridView_SingleVariable.Size = new System.Drawing.Size(497, 202);
            this.dataGridView_SingleVariable.TabIndex = 0;
            // 
            // col_Name
            // 
            this.col_Name.HeaderText = "Name"; // ✨ 3. 畫面顯示不受影響，依然顯示 "Name"
            this.col_Name.MinimumWidth = 8;
            this.col_Name.Name = "col_Name";
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.MinimumWidth = 8;
            this.Value.Name = "Value";
            // 
            // dataGridView_Array
            // 
            this.dataGridView_Array.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Array.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Array.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dataGridView_Array.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Array.Location = new System.Drawing.Point(3, 419);
            this.dataGridView_Array.Name = "dataGridView_Array";
            this.dataGridView_Array.RowHeadersVisible = false;
            this.dataGridView_Array.RowHeadersWidth = 62;
            this.dataGridView_Array.RowTemplate.Height = 31;
            this.dataGridView_Array.Size = new System.Drawing.Size(497, 202);
            this.dataGridView_Array.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // flowLayoutPanel_Parament
            // 
            this.flowLayoutPanel_Parament.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_Parament.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel_Parament.Name = "flowLayoutPanel_Parament";
            this.flowLayoutPanel_Parament.Size = new System.Drawing.Size(497, 202);
            this.flowLayoutPanel_Parament.TabIndex = 2;
            // 
            // FromParameterSetting_StepSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            // 💡 這裡的 this.Name 才是 WinForms 原生要設定的控制項名稱字串
            this.Name = "FromParameterSetting_StepSetting";
            this.Size = new System.Drawing.Size(503, 624);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_SingleVariable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Array)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView_SingleVariable;

        // ✨ 4. 這裡欄位宣告同步更名為 col_Name，避開關鍵字
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridView dataGridView_Array;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Parament;
    }
}