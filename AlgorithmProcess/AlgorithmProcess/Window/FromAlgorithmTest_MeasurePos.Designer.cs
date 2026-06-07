namespace AlgorithmProcess.Window
{
    partial class FromAlgorithmTest_MeasurePos
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
            this.tableLayoutPanel_Background = new System.Windows.Forms.TableLayoutPanel();
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Amplitude_trackBar = new System.Windows.Forms.TrackBar();
            this.ROIWidth_trackBar = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel_Select = new System.Windows.Forms.TableLayoutPanel();
            this.Position_comboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Direction_comboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel_Background.SuspendLayout();
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
            // tableLayoutPanel_Background
            // 
            this.tableLayoutPanel_Background.ColumnCount = 1;
            this.tableLayoutPanel_Background.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Background.Controls.Add(this.dataGridView_Point, 0, 3);
            this.tableLayoutPanel_Background.Controls.Add(this.tableLayoutPanel_Button, 0, 0);
            this.tableLayoutPanel_Background.Controls.Add(this.tableLayoutPanel_Extract, 0, 1);
            this.tableLayoutPanel_Background.Controls.Add(this.tableLayoutPanel_Select, 0, 2);
            this.tableLayoutPanel_Background.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Background.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Background.Name = "tableLayoutPanel_Background";
            this.tableLayoutPanel_Background.RowCount = 4;
            this.tableLayoutPanel_Background.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel_Background.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
            this.tableLayoutPanel_Background.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.18182F));
            this.tableLayoutPanel_Background.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.45454F));
            this.tableLayoutPanel_Background.Size = new System.Drawing.Size(519, 813);
            this.tableLayoutPanel_Background.TabIndex = 0;
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
            this.dataGridView_Point.Location = new System.Drawing.Point(3, 444);
            this.dataGridView_Point.Name = "dataGridView_Point";
            this.dataGridView_Point.RowHeadersVisible = false;
            this.dataGridView_Point.RowHeadersWidth = 62;
            this.dataGridView_Point.RowTemplate.Height = 31;
            this.dataGridView_Point.Size = new System.Drawing.Size(513, 366);
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
            this.tableLayoutPanel_Button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tableLayoutPanel_Button.Size = new System.Drawing.Size(513, 67);
            this.tableLayoutPanel_Button.TabIndex = 0;
            // 
            // DrawLine_btn
            // 
            this.DrawLine_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawLine_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawLine_btn.Location = new System.Drawing.Point(3, 3);
            this.DrawLine_btn.Name = "DrawLine_btn";
            this.DrawLine_btn.Size = new System.Drawing.Size(250, 61);
            this.DrawLine_btn.TabIndex = 0;
            this.DrawLine_btn.Text = "Draw Line";
            this.DrawLine_btn.UseVisualStyleBackColor = true;
            this.DrawLine_btn.Click += new System.EventHandler(this.Btn_Click);
            // 
            // Clear_btn
            // 
            this.Clear_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Clear_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clear_btn.Location = new System.Drawing.Point(259, 3);
            this.Clear_btn.Name = "Clear_btn";
            this.Clear_btn.Size = new System.Drawing.Size(251, 61);
            this.Clear_btn.TabIndex = 1;
            this.Clear_btn.Text = "Clear";
            this.Clear_btn.UseVisualStyleBackColor = true;
            this.Clear_btn.Click += new System.EventHandler(this.Btn_Click);
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
            this.tableLayoutPanel_Extract.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel_Extract.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel_Extract.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel_Extract.Controls.Add(this.Amplitude_trackBar, 1, 0);
            this.tableLayoutPanel_Extract.Controls.Add(this.ROIWidth_trackBar, 1, 2);
            this.tableLayoutPanel_Extract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Extract.Location = new System.Drawing.Point(3, 76);
            this.tableLayoutPanel_Extract.Name = "tableLayoutPanel_Extract";
            this.tableLayoutPanel_Extract.RowCount = 3;
            this.tableLayoutPanel_Extract.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_Extract.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_Extract.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_Extract.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_Extract.Size = new System.Drawing.Size(513, 215);
            this.tableLayoutPanel_Extract.TabIndex = 1;
            // 
            // ROIWidth_numericUpDown
            // 
            this.ROIWidth_numericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ROIWidth_numericUpDown.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ROIWidth_numericUpDown.Location = new System.Drawing.Point(412, 146);
            this.ROIWidth_numericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.ROIWidth_numericUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.ROIWidth_numericUpDown.Name = "ROIWidth_numericUpDown";
            this.ROIWidth_numericUpDown.Size = new System.Drawing.Size(98, 33);
            this.ROIWidth_numericUpDown.TabIndex = 8;
            // 
            // Sigma_numericUpDown
            // 
            this.Sigma_numericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sigma_numericUpDown.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sigma_numericUpDown.Location = new System.Drawing.Point(412, 75);
            this.Sigma_numericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.Sigma_numericUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.Sigma_numericUpDown.Name = "Sigma_numericUpDown";
            this.Sigma_numericUpDown.Size = new System.Drawing.Size(98, 33);
            this.Sigma_numericUpDown.TabIndex = 7;
            // 
            // Amplitude_numericUpDown
            // 
            this.Amplitude_numericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Amplitude_numericUpDown.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Amplitude_numericUpDown.Location = new System.Drawing.Point(412, 4);
            this.Amplitude_numericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.Amplitude_numericUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.Amplitude_numericUpDown.Name = "Amplitude_numericUpDown";
            this.Amplitude_numericUpDown.Size = new System.Drawing.Size(98, 33);
            this.Amplitude_numericUpDown.TabIndex = 6;
            // 
            // Sigma_trackBar
            // 
            this.Sigma_trackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sigma_trackBar.Location = new System.Drawing.Point(105, 74);
            this.Sigma_trackBar.Name = "Sigma_trackBar";
            this.Sigma_trackBar.Size = new System.Drawing.Size(301, 65);
            this.Sigma_trackBar.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 71);
            this.label1.TabIndex = 0;
            this.label1.Text = "Amplitude";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 71);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sigma";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 73);
            this.label3.TabIndex = 2;
            this.label3.Text = "ROI Width";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Amplitude_trackBar
            // 
            this.Amplitude_trackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Amplitude_trackBar.Location = new System.Drawing.Point(105, 3);
            this.Amplitude_trackBar.Name = "Amplitude_trackBar";
            this.Amplitude_trackBar.Size = new System.Drawing.Size(301, 65);
            this.Amplitude_trackBar.TabIndex = 3;
            // 
            // ROIWidth_trackBar
            // 
            this.ROIWidth_trackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ROIWidth_trackBar.Location = new System.Drawing.Point(105, 145);
            this.ROIWidth_trackBar.Name = "ROIWidth_trackBar";
            this.ROIWidth_trackBar.Size = new System.Drawing.Size(301, 67);
            this.ROIWidth_trackBar.TabIndex = 4;
            // 
            // tableLayoutPanel_Select
            // 
            this.tableLayoutPanel_Select.ColumnCount = 2;
            this.tableLayoutPanel_Select.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Select.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Select.Controls.Add(this.Position_comboBox, 1, 1);
            this.tableLayoutPanel_Select.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel_Select.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel_Select.Controls.Add(this.Direction_comboBox, 1, 0);
            this.tableLayoutPanel_Select.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Select.Location = new System.Drawing.Point(3, 297);
            this.tableLayoutPanel_Select.Name = "tableLayoutPanel_Select";
            this.tableLayoutPanel_Select.RowCount = 2;
            this.tableLayoutPanel_Select.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Select.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Select.Size = new System.Drawing.Size(513, 141);
            this.tableLayoutPanel_Select.TabIndex = 2;
            // 
            // Position_comboBox
            // 
            this.Position_comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Position_comboBox.FormattingEnabled = true;
            this.Position_comboBox.Location = new System.Drawing.Point(259, 73);
            this.Position_comboBox.Name = "Position_comboBox";
            this.Position_comboBox.Size = new System.Drawing.Size(251, 26);
            this.Position_comboBox.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(250, 71);
            this.label5.TabIndex = 5;
            this.label5.Text = "Position";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(250, 70);
            this.label4.TabIndex = 3;
            this.label4.Text = "Direction";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Direction_comboBox
            // 
            this.Direction_comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Direction_comboBox.FormattingEnabled = true;
            this.Direction_comboBox.Items.AddRange(new object[] {
            "All",
            "Positive",
            "Negative"});
            this.Direction_comboBox.Location = new System.Drawing.Point(259, 3);
            this.Direction_comboBox.Name = "Direction_comboBox";
            this.Direction_comboBox.Size = new System.Drawing.Size(251, 26);
            this.Direction_comboBox.TabIndex = 4;
            // 
            // FromAlgorithmTest_MeasurePos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel_Background);
            this.Name = "FromAlgorithmTest_MeasurePos";
            this.Size = new System.Drawing.Size(519, 813);
            this.tableLayoutPanel_Background.ResumeLayout(false);
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

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Background;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Button;
        private System.Windows.Forms.Button DrawLine_btn;
        private System.Windows.Forms.Button Clear_btn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Extract;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Select;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar Sigma_trackBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar Amplitude_trackBar;
        private System.Windows.Forms.TrackBar ROIWidth_trackBar;
        private System.Windows.Forms.NumericUpDown ROIWidth_numericUpDown;
        private System.Windows.Forms.NumericUpDown Sigma_numericUpDown;
        private System.Windows.Forms.NumericUpDown Amplitude_numericUpDown;
        private System.Windows.Forms.ComboBox Position_comboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Direction_comboBox;
        private System.Windows.Forms.DataGridView dataGridView_Point;
        private System.Windows.Forms.DataGridViewTextBoxColumn No_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Row;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column;
    }
}
