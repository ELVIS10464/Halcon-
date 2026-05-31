using HalconDotNet;
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
    public partial class FromAlgorithmTest_MeasurePos : UserControl
    {

        // =============================== 事件與委託 ===============================

        public delegate void CallBackReturnBtn(string btn);
        public event CallBackReturnBtn CallBackBtn;

        // =============================== 事件與委託 ===============================

        public FromAlgorithmTest_MeasurePos()
        {
            InitializeComponent();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            if(btn == DrawLine_btn)
            {
                CallBackBtn?.Invoke("DrawLine");
            }
            else if(btn == Clear_btn)
            {
                CallBackBtn?.Invoke("Clear");
            }         
        }

        private HTuple MeasurePosFindPoint(HImage _hImage, double RowStart, double ColStart, double RowEnd, double ColEnd, double RoiWidthLen2, double AmplitudeThreshold, out HTuple _hCol, out HTuple _hAmplitude, out HTuple _hDistance)
        {
            HMeasure MsrHandle_Measure_01_1 = new HMeasure();
            HTuple _hImageWidth = new HTuple(), _hImageHeight = new HTuple();

            HTuple _hRow = new HTuple();
            _hCol = new HTuple();
            _hAmplitude = new HTuple();
            _hDistance = new HTuple();

            _hImage.GetImageSize(out _hImageWidth, out _hImageHeight);

            double LineRowStart_Measure_01_1 = RowStart;
            double LineColumnStart_Measure_01_1 = ColStart;
            double LineRowEnd_Measure_01_1 = RowEnd;
            double LineColumnEnd_Measure_01_1 = ColEnd;

            double TmpCtrl_Row = 0.5 * (LineRowStart_Measure_01_1 + LineRowEnd_Measure_01_1);
            double TmpCtrl_Column = 0.5 * (LineColumnStart_Measure_01_1 + LineColumnEnd_Measure_01_1);
            double TmpCtrl_Dr = LineRowStart_Measure_01_1 - LineRowEnd_Measure_01_1;
            double TmpCtrl_Dc = LineColumnEnd_Measure_01_1 - LineColumnStart_Measure_01_1;

            double TmpCtrl_Phi = Math.Atan2(TmpCtrl_Dr, TmpCtrl_Dc);
            double TmpCtrl_Len1 = 0.5 * Math.Sqrt(TmpCtrl_Dr * TmpCtrl_Dr + TmpCtrl_Dc * TmpCtrl_Dc);
            double TmpCtrl_Len2 = RoiWidthLen2;

            MsrHandle_Measure_01_1.GenMeasureRectangle2(TmpCtrl_Row, TmpCtrl_Column, TmpCtrl_Phi, TmpCtrl_Len1, TmpCtrl_Len2, _hImageWidth[0], _hImageHeight[0], "nearest_neighbor");
            MsrHandle_Measure_01_1.MeasurePos(_hImage, 1, AmplitudeThreshold, "all", "all", out _hRow, out _hCol, out _hAmplitude, out _hDistance);

            return _hRow;
        }

    }
}
