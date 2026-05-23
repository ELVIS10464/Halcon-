using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorithmProcess.Window
{
    public partial class FromParameterSetting_StepSetting_Parameter : UserControl
    {
        private bool isUpdating = false;

        // 📐 動態放大倍率：如果是小數，就把 TrackBar 放大一千倍來滑動
        private double scaleFactor = 1.0;

        public string ParameterName => this.lbl_ParamName.Text;

        // 🔓 確保外層讀取的 CurrentValue 與 NumericUpDown 的真實浮點數完全一致
        public double CurrentValue => (double)this.numericUpDown1.Value;

        public FromParameterSetting_StepSetting_Parameter()
        {
            InitializeComponent();
            this.trackBar1.Scroll += TrackBar1_Scroll;
            this.numericUpDown1.ValueChanged += NumericUpDown1_ValueChanged;
        }

        public void InitParameter(string name, double value)
        {
            // 預設為一般灰階 0~255 的整數範圍
            InitParameter(name, value, 0, 255);
        }

        public void InitParameter(string name, double value, double min, double max)
        {
            isUpdating = true; // 👑 初始化防禦鎖

            this.lbl_ParamName.Text = name;

            // 1. ✨ 自動判斷是否為小數參數 (例如最大值小於等於 5，且有小數點)
            // 如果是小數，我們把 TrackBar 放大 1000 倍來對應
            if (max <= 5.0 && (max % 1 != 0 || min % 1 != 0 || value % 1 != 0))
            {
                scaleFactor = 1000.0;
                this.numericUpDown1.DecimalPlaces = 3; // 顯示小數點後三位
                this.numericUpDown1.Increment = 0.001M; // 點擊上下紐時按 0.001 微調
            }
            else
            {
                scaleFactor = 1.0;
                this.numericUpDown1.DecimalPlaces = 0; // 整數
                this.numericUpDown1.Increment = 1M;
            }

            // 2. 設定 NumericUpDown 上下限與數值
            this.numericUpDown1.Minimum = (decimal)min;
            this.numericUpDown1.Maximum = (decimal)max;
            this.numericUpDown1.Value = (decimal)value;

            // 3. 設定 TrackBar 放大後的整數上下限與數值
            this.trackBar1.Minimum = (int)Math.Round(min * scaleFactor);
            this.trackBar1.Maximum = (int)Math.Round(max * scaleFactor);

            int trackValue = (int)Math.Round(value * scaleFactor);
            if (trackValue > this.trackBar1.Maximum) trackValue = this.trackBar1.Maximum;
            if (trackValue < this.trackBar1.Minimum) trackValue = this.trackBar1.Minimum;
            this.trackBar1.Value = trackValue;

            isUpdating = false;
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            if (isUpdating) return;

            isUpdating = true;
            try
            {
                // ✨ 將 TrackBar 的數值除回倍率，精準同步給實數輸入框
                double actualValue = this.trackBar1.Value / scaleFactor;
                this.numericUpDown1.Value = (decimal)actualValue;
            }
            finally
            {
                isUpdating = false;
            }
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (isUpdating) return;

            isUpdating = true;
            try
            {
                // ✨ 計算對應的 TrackBar 放大後整數值
                int trackValue = (int)Math.Round((double)this.numericUpDown1.Value * scaleFactor);

                // 防禦邊界
                if (trackValue > this.trackBar1.Maximum) trackValue = this.trackBar1.Maximum;
                if (trackValue < this.trackBar1.Minimum) trackValue = this.trackBar1.Minimum;

                this.trackBar1.Value = trackValue;
            }
            finally
            {
                isUpdating = false;
            }
        }
    }
}
