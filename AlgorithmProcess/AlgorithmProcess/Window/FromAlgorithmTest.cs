using AlgorithmProcess.Base;
using HalconDotNet;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorithmProcess.Window
{
    public partial class FromAlgorithmTest : UserControl
    {
        // =============================== function建立 ===============================
        private readonly ImageConverter _ImageConverter = new ImageConverter();
        private ReadROIINI RRI = null;
        private ReadAlgorithmParameterINI RAPI = null;
        private HalconImageConverter HC = new HalconImageConverter();
        private MappingModule MM = null;

        // =============================== 事件與委託 ===============================
        public delegate void CallBackReturnLog(string type, string msg);
        public event CallBackReturnLog CallBackLog;

        // =============================== 全域變數宣告 ===============================
        private SettingInfo settingInfo = null;
        private ROIList rOIList = null;
        private MappingParameter mappingParameterFile = null, mappingParameterApply = null;
        private List<string> SlotList = new List<string>();
        private List<string> CameraLocation = null;
        private int ImageIndex = 0;
        private AdvancedPictureBox currentPictureBox = null;

        // 🌟 全域變數：對齊 HALCON 官方預設值
        public double G_Amplitude = 30.0; // 官方預設值為 30.0
        public double G_Sigma = 1.0;     // 官方預設值為 1.0
        public double G_ROIWidth = 30.0;

        // 防止雙向綁定無窮迴圈的旗標
        private bool isUpdating = false;

        public FromAlgorithmTest()
        {
            InitializeComponent();
            InitializeImageLoad(settingInfo);
            InitParamControlsBinding();
            InitComboBoxItems();
        }

        public void InitializeImageLoad(SettingInfo settingInfo)
        {
            this.settingInfo = settingInfo;

            if (this.settingInfo != null && this.settingInfo.DirImagePath != "")
            {
                SlotList = new List<string>();
                SelectSlot_cb.Items.Clear();

                string[] files = Directory.GetFiles(settingInfo.DirImagePath, $"*Middle*", SearchOption.TopDirectoryOnly);

                if (files.Length > 0)
                {
                    CameraLocation = new List<string>() { "Left", "Middle", "Right" };
                }
                else
                {
                    CameraLocation = new List<string>() { "Left", "Right" };
                }

                Task.Run(() =>
                {
                    string[] FilePath = Directory.GetFiles(this.settingInfo.DirImagePath, "*.bmp");

                    for (int i = 0; i < this.settingInfo.SlotNumber; i++)
                    {
                        bool _IsFind = true;

                        for (int j = 0; j < CameraLocation.Count; j++)
                        {
                            string FileName = "Slot" + (i + 1).ToString("00") + "_" + CameraLocation[j] + ".bmp";

                            if (!FilePath.ToList().Contains(this.settingInfo.DirImagePath + "\\" + FileName) && j != 1)
                            {
                                _IsFind = false;
                                break;
                            }
                        }

                        if (_IsFind)
                        {
                            SlotList.Add((i + 1).ToString("00"));
                        }
                    }

                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        SelectSlot_cb.Items.Clear();
                        SelectSlot_cb.Items.AddRange(SlotList.ToArray());

                        if (SlotList.Count > 0)
                        {
                            SelectSlot_cb.SelectedIndex = 0;
                        }
                    }));
                });

                ImageIndex = 1;
            }
        }

        private void InitParamControlsBinding()
        {
            // 1. Amplitude (Threshold)：官方典型範圍 1 到 255，預設 30.0
            Amplitude_numericUpDown.Minimum = 1.0M;
            Amplitude_numericUpDown.Maximum = 255.0M;
            Amplitude_numericUpDown.Increment = 1.0M;
            Amplitude_numericUpDown.DecimalPlaces = 1;
            Amplitude_numericUpDown.Value = (decimal)G_Amplitude;

            Amplitude_trackBar.Minimum = 1;
            Amplitude_trackBar.Maximum = 255;
            Amplitude_trackBar.Value = (int)G_Amplitude;

            Amplitude_trackBar.Tag = new ParamBindInfo { NumericControl = Amplitude_numericUpDown, ParamName = "Amplitude", Scale = 1.0 };
            Amplitude_numericUpDown.Tag = new ParamBindInfo { TrackControl = Amplitude_trackBar, ParamName = "Amplitude", Scale = 1.0 };

            // 2. Sigma：官方限制最小 0.4，典型上限 100，預設 1.0
            Sigma_numericUpDown.Minimum = 0.4M; // 核心防呆：絕對不能小於 0.4
            Sigma_numericUpDown.Maximum = 100.0M;
            Sigma_numericUpDown.Increment = 0.1M;
            Sigma_numericUpDown.DecimalPlaces = 2;
            Sigma_numericUpDown.Value = (decimal)G_Sigma;

            Sigma_trackBar.Minimum = 40;   // 0.4 * 100
            Sigma_trackBar.Maximum = 10000; // 100.0 * 100
            Sigma_trackBar.Value = (int)(G_Sigma * 100.0);

            Sigma_trackBar.Tag = new ParamBindInfo { NumericControl = Sigma_numericUpDown, ParamName = "Sigma", Scale = 100.0 };
            Sigma_numericUpDown.Tag = new ParamBindInfo { TrackControl = Sigma_trackBar, ParamName = "Sigma", Scale = 100.0 };

            // 3. ROIWidth (Len2)
            ROIWidth_numericUpDown.Minimum = 1.0M;
            ROIWidth_numericUpDown.Maximum = 200.0M;
            ROIWidth_numericUpDown.Increment = 1.0M;
            ROIWidth_numericUpDown.DecimalPlaces = 1;
            ROIWidth_numericUpDown.Value = (decimal)G_ROIWidth;

            ROIWidth_trackBar.Minimum = 1;
            ROIWidth_trackBar.Maximum = 200;
            ROIWidth_trackBar.Value = (int)G_ROIWidth;

            ROIWidth_trackBar.Tag = new ParamBindInfo { NumericControl = ROIWidth_numericUpDown, ParamName = "ROIWidth", Scale = 1.0 };
            ROIWidth_numericUpDown.Tag = new ParamBindInfo { TrackControl = ROIWidth_trackBar, ParamName = "ROIWidth", Scale = 1.0 };

            // 統一事件註冊
            Amplitude_trackBar.Scroll += TrackBar_Scroll;
            Sigma_trackBar.Scroll += TrackBar_Scroll;
            ROIWidth_trackBar.Scroll += TrackBar_Scroll;

            Amplitude_numericUpDown.ValueChanged += NumericUpDown_ValueChanged;
            Sigma_numericUpDown.ValueChanged += NumericUpDown_ValueChanged;
            ROIWidth_numericUpDown.ValueChanged += NumericUpDown_ValueChanged;
        }

        private class ParamBindInfo
        {
            public TrackBar TrackControl { get; set; }
            public NumericUpDown NumericControl { get; set; }
            public string ParamName { get; set; }
            public double Scale { get; set; }
        }

        private void InitComboBoxItems()
        {
            Direction_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Direction_comboBox.Items.Clear();
            Direction_comboBox.Items.Add("all");
            Direction_comboBox.Items.Add("positive");
            Direction_comboBox.Items.Add("negative");
            Direction_comboBox.SelectedIndex = 0;

            Position_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Position_comboBox.Items.Clear();
            Position_comboBox.Items.Add("all");
            Position_comboBox.Items.Add("first");
            Position_comboBox.Items.Add("last");
            Position_comboBox.SelectedIndex = 0;

            Direction_comboBox.SelectedIndexChanged += Direction_Position_SelectedIndexChanged;
            Position_comboBox.SelectedIndexChanged += Direction_Position_SelectedIndexChanged;
        }

        private void SelectSlot_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = SelectSlot_cb.SelectedIndex;
            int ImageIndex = index + 1;

            Image_tabControl.TabPages.Clear();

            for (int i = 0; i < CameraLocation.Count; i++)
            {
                AdvancedPictureBox picImage = new AdvancedPictureBox
                {
                    Dock = DockStyle.Fill,
                };

                string ImageName = settingInfo.DirImagePath + "\\Slot" + ImageIndex.ToString("D2") + $"_{CameraLocation[i]}.bmp";

                // 🟢 修正：將真實圖片絕對路徑綁定在控制項的 Tag，供後續拉動參數條時，即時量測演算法能夠直接讀檔
                picImage.Tag = ImageName;

                using (Bitmap tempImg = (Bitmap)_ImageConverter.ConvertFrom(File.ReadAllBytes(ImageName)))
                {
                    picImage.Image = tempImg;
                }

                string currentCamLoc = CameraLocation[i];
                picImage.MeasureLineDrawn += (startPt, endPt) =>
                {
                    CallBackLog?.Invoke("INFO", $"[{currentCamLoc}] 接收到傳出卡尺線。起點:({startPt.X:F1}, {startPt.Y:F1}), 終點:({endPt.X:F1}, {endPt.Y:F1})");
                    ExecuteHalconMeasure(picImage, startPt, endPt);
                };

                TabPage newPage = new TabPage(CameraLocation[i]) { Name = $"{CameraLocation[i]}" };
                Panel containerPanel = new Panel
                {
                    Name = $"panel_StepContainer_{i}",
                    Dock = DockStyle.Fill,
                    BackColor = Color.Transparent
                };

                containerPanel.Controls.Add(picImage);
                newPage.Controls.Add(containerPanel);
                Image_tabControl.TabPages.Add(newPage);
            }
        }

        private void SelectAlgorithm_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectAlgorithm_cb.SelectedItem == null) return;
            string item = SelectAlgorithm_cb.SelectedItem.ToString();

            if (item == "MeasurePos")
            {
                AlgorithmMethod_tabControl.SelectedTab = AlgorithmMethod_tabControl.TabPages["MeasurePos"];
            }
        }

        private void MeasurePos_btn_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            if (btn == DrawLine_btn)
            {
                AdvancedPictureBox currentPic = GetCurrentActivePictureBox();
                if (currentPic != null)
                {
                    currentPic.CurrentMode = AdvancedPictureBox.InteractionMode.DrawMeasureLine;
                    CallBackLog?.Invoke("INFO", "表單A：已透過按鈕啟動卡尺劃線模式。");
                }
            }
            else if (btn == Clear_btn)
            {
                CallBackLog?.Invoke("INFO", "量測工具：已清除卡尺線段，圖片還原可移動狀態。");
            }
        }

        private void Image_tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPictureBox = GetCurrentActivePictureBox();
        }

        private HTuple MeasurePosFindPoint(HImage _hImage, double RowStart, double ColStart, double RowEnd, double ColEnd, out HTuple _hCol, out HTuple _hAmplitude, out HTuple _hDistance)
        {
            HTuple _hRow = new HTuple();
            _hCol = new HTuple();
            _hAmplitude = new HTuple();
            _hDistance = new HTuple();

            if (_hImage == null || !_hImage.IsInitialized()) return _hRow;

            HMeasure MsrHandle_Measure_01_1 = new HMeasure();

            try
            {
                HTuple _hImageWidth, _hImageHeight;
                _hImage.GetImageSize(out _hImageWidth, out _hImageHeight);
                CallBackLog?.Invoke("DEBUG", $"[HALCON 影像大小] Width: {_hImageWidth[0].I}, Height: {_hImageHeight[0].I}");
                CallBackLog?.Invoke("DEBUG", $"[C# 傳入卡尺線] 起點(Col, Row): ({ColStart:F2}, {RowStart:F2}) -> 終點(Col, Row): ({ColEnd:F2}, {RowEnd:F2})");

                double TmpCtrl_Row = 0.5 * (RowStart + RowEnd);
                double TmpCtrl_Column = 0.5 * (ColStart + ColEnd);

                double TmpCtrl_Dr = RowStart - RowEnd;
                double TmpCtrl_Dc = ColEnd - ColStart;

                double TmpCtrl_Phi = Math.Atan2(TmpCtrl_Dr, TmpCtrl_Dc);

                double TmpCtrl_Len1 = 0.5 * Math.Sqrt(TmpCtrl_Dr * TmpCtrl_Dr + TmpCtrl_Dc * TmpCtrl_Dc);
                double TmpCtrl_Len2 = G_ROIWidth;

                CallBackLog?.Invoke("DEBUG", $"[卡尺幾何參數] 中心(Col, Row): ({TmpCtrl_Column:F2}, {TmpCtrl_Row:F2}), 角度(弧度): {TmpCtrl_Phi:F4} (角度: {TmpCtrl_Phi * 180 / Math.PI:F1}°), 半長(Len1): {TmpCtrl_Len1:F2}, 半寬(Len2): {TmpCtrl_Len2:F2}");

                double localSigma = G_Sigma;
                if (localSigma >= 0.5 * TmpCtrl_Len1)
                {
                    localSigma = (0.5 * TmpCtrl_Len1) - 0.05;
                    if (localSigma < 0.4) localSigma = 0.4;

                    this.BeginInvoke(new Action(() => {
                        if (!isUpdating)
                        {
                            isUpdating = true;
                            Sigma_numericUpDown.Value = (decimal)localSigma;
                            Sigma_trackBar.Value = (int)(localSigma * 100.0);
                            isUpdating = false;
                        }
                    }));
                    CallBackLog?.Invoke("WARN", $"卡尺太短或Sigma過大！已動態調整 Sigma 為安全值: {localSigma:F2}");
                }

                MsrHandle_Measure_01_1.GenMeasureRectangle2(
                    TmpCtrl_Row, TmpCtrl_Column, TmpCtrl_Phi,
                    TmpCtrl_Len1, TmpCtrl_Len2,
                    _hImageWidth[0], _hImageHeight[0], "nearest_neighbor"
                );

                string transition = "all";
                string select = "all";

                this.Invoke(new Action(() => {
                    transition = Direction_comboBox.SelectedItem?.ToString() ?? "all";
                    select = Position_comboBox.SelectedItem?.ToString() ?? "all";
                }));

                MsrHandle_Measure_01_1.MeasurePos(
                    _hImage,
                    localSigma,
                    G_Amplitude,
                    transition,
                    select,
                    out _hRow, out _hCol, out _hAmplitude, out _hDistance
                );

                if (_hRow != null)
                {
                    CallBackLog?.Invoke("DEBUG", $"[HALCON 量測結束] 成功找到邊緣點數量: {_hRow.Length} 個");
                }
            }
            catch (Exception ex)
            {
                CallBackLog?.Invoke("ERROR", $"Halcon 卡尺內部運算異常: {ex.Message}");
            }
            finally
            {
                if (MsrHandle_Measure_01_1.IsInitialized())
                {
                    MsrHandle_Measure_01_1.Dispose();
                }
            }

            return _hRow;
        }

        private void ExecuteHalconMeasure(AdvancedPictureBox picBox, PointF imgStart, PointF imgEnd)
        {
            if (AlgorithmMethod_tabControl.SelectedTab == null || AlgorithmMethod_tabControl.SelectedTab.Name != "MeasurePos") return;
            if (picBox == null || picBox.Tag == null) return; // 🟢 改為判斷 Tag 是否有路徑

            string targetImagePath = picBox.Tag.ToString();
            if (!File.Exists(targetImagePath))
            {
                CallBackLog?.Invoke("ERROR", $"找不到指定路徑之影像檔案: {targetImagePath}");
                return;
            }

            CallBackLog?.Invoke("DEBUG", $"[UI 原始點] Start_X(Col): {imgStart.X}, Start_Y(Row): {imgStart.Y}");

            // 宣告於 try 外部以利於 finally 釋放
            HImage hImage = null;

            try
            {
                picBox.UpdateResultPoints(new List<PointF>());

                // 🟢 核心變更：不再抽取 picBox.Image，直接由路徑讀取最純淨的硬碟檔案至 Halcon
                hImage = new HImage();
                hImage.ReadImage(targetImagePath);

                HTuple hCol, hAmplitude, hDistance;
                HTuple hRow = MeasurePosFindPoint(
                    hImage,
                    imgStart.Y, imgStart.X,
                    imgEnd.Y, imgEnd.X,
                    out hCol, out hAmplitude, out hDistance
                );

                List<PointF> resultPoints = new List<PointF>();
                DataTable dtPoints = new DataTable();
                dtPoints.Columns.Add("No", typeof(int));
                dtPoints.Columns.Add("Row", typeof(string));
                dtPoints.Columns.Add("Column", typeof(string));

                if (hRow != null && hRow.Length > 0)
                {
                    CallBackLog?.Invoke("INFO", $"Halcon量測成功：找到 {hRow.Length} 個邊緣點。");

                    for (int k = 0; k < hRow.Length; k++)
                    {
                        float y = (float)hRow[k].D;
                        float x = (float)hCol[k].D;
                        resultPoints.Add(new PointF(x, y));

                        DataRow row = dtPoints.NewRow();
                        row["No"] = k + 1;
                        row["Column"] = x.ToString("F3");
                        row["Row"] = y.ToString("F3");
                        dtPoints.Rows.Add(row);
                    }
                }
                else
                {
                    CallBackLog?.Invoke("WARN", "量測完成，但目前參數配置下未尋找到任何符合的邊緣點。");
                }

                this.BeginInvoke(new Action(() =>
                {
                    picBox.UpdateResultPoints(resultPoints);
                    if (dataGridView_Point != null)
                    {
                        dataGridView_Point.DataSource = dtPoints;
                    }
                }));
            }
            catch (Exception ex)
            {
                CallBackLog?.Invoke("ERROR", $"卡尺運算過程中發生異常：{ex.Message}");
            }
            finally
            {
                // 🟢 確實釋放 HImage 本體記憶體
                if (hImage != null && hImage.IsInitialized())
                {
                    hImage.Dispose();
                }
            }
        }

        private void TrackBar_Scroll(object sender, EventArgs e)
        {
            if (isUpdating) return;

            TrackBar currentTrackBar = sender as TrackBar;
            if (currentTrackBar == null || currentTrackBar.Tag == null) return;

            ParamBindInfo info = currentTrackBar.Tag as ParamBindInfo;

            isUpdating = true;
            try
            {
                double actualValue = (double)currentTrackBar.Value / info.Scale;
                info.NumericControl.Value = (decimal)actualValue;
                UpdateGlobalVariable(info.ParamName, actualValue);
            }
            finally
            {
                isUpdating = false;
            }
        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (isUpdating) return;

            NumericUpDown currentNum = sender as NumericUpDown;
            if (currentNum == null || currentNum.Tag == null) return;

            ParamBindInfo info = currentNum.Tag as ParamBindInfo;

            isUpdating = true;
            try
            {
                double actualValue = (double)currentNum.Value;
                int trackValue = (int)Math.Round(actualValue * info.Scale);

                trackValue = Math.Max(info.TrackControl.Minimum, Math.Min(info.TrackControl.Maximum, trackValue));
                info.TrackControl.Value = trackValue;

                UpdateGlobalVariable(info.ParamName, actualValue);
            }
            finally
            {
                isUpdating = false;
            }
        }

        private void Direction_Position_SelectedIndexChanged(object sender, EventArgs e)
        {
            TriggerLiveMeasure();
        }

        private void UpdateGlobalVariable(string paramName, double value)
        {
            switch (paramName)
            {
                case "Amplitude": G_Amplitude = value; break;
                case "Sigma": G_Sigma = value; break;
                case "ROIWidth": G_ROIWidth = value; break;
            }

            TriggerLiveMeasure();
        }

        private void TriggerLiveMeasure()
        {
            AdvancedPictureBox currentPic = GetCurrentActivePictureBox();

            // 🟢 修正：此處原本判斷 currentPic.Image != null，現改為檢驗 Tag 中是否有綁定真實路徑
            if (currentPic != null && currentPic.Tag != null)
            {
                // 【防護鎖】如果起點與終點重合（代表使用者根本還沒拉線），直接 Return 阻斷
                if (currentPic.StartPoint == currentPic.EndPoint ||
                    (currentPic.StartPoint.X == 0 && currentPic.StartPoint.Y == 0))
                {
                    return;
                }

                ExecuteHalconMeasure(currentPic, currentPic.StartPoint, currentPic.EndPoint);
            }
        }

        private AdvancedPictureBox GetCurrentActivePictureBox()
        {
            if (Image_tabControl.SelectedTab == null) return null;

            if (Image_tabControl.SelectedTab.Controls.Count > 0)
            {
                Panel containerPanel = Image_tabControl.SelectedTab.Controls[0] as Panel;
                if (containerPanel != null && containerPanel.Controls.Count > 0)
                {
                    return containerPanel.Controls[0] as AdvancedPictureBox;
                }
            }
            return null;
        }
    }
}