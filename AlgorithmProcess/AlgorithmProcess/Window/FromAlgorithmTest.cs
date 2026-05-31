using AlgorithmProcess.Base;
using HalconDotNet;
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
        private CustomPictureBox currentPictureBox = null;

        // 宣告滑鼠操作模式
        private enum MouseMode { None, DrawMeasureLine }
        private MouseMode m_CurrentMouseMode = MouseMode.None;

        // 紀錄畫線的起點與終點（控制項 UI 座標，用於 Paint 繪圖）
        private Point m_StartPoint = Point.Empty;
        private Point m_EndPoint = Point.Empty;
        private Point _ptMouseEnter = Point.Empty;
        private bool m_IsDrawing = false;

        // 💡 紀錄轉換後的「真實影像像素座標」，供給 Halcon 演算法使用
        private PointF m_ImgStartPt = PointF.Empty;
        private PointF m_ImgEndPt = PointF.Empty;

        public FromAlgorithmTest()
        {
            InitializeComponent();
            InitializePictureBox();
            InitializeImageLoad(settingInfo);
        }

        public void InitializePictureBox()
        {
            // 這裡留空或做其他初始化
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

        private void SelectSlot_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = SelectSlot_cb.SelectedIndex;
            int ImageIndex = index + 1;

            Image_tabControl.TabPages.Clear();

            // 🎯 外部迴圈程式碼大幅精簡：
            for (int i = 0; i < CameraLocation.Count; i++)
            {
                DrawLinePictureBox picImage = new DrawLinePictureBox
                {
                    Dock = DockStyle.Fill,
                };

                string ImageName = settingInfo.DirImagePath + "\\Slot" + ImageIndex.ToString("D2") + $"_{CameraLocation[i]}.bmp";

                // 安全讀取與釋放檔案暫存
                using (Bitmap tempImg = (Bitmap)_ImageConverter.ConvertFrom(File.ReadAllBytes(ImageName)))
                {
                    picImage.Image = tempImg; // 觸發自訂控制項內部的深拷貝與 FitWindow
                }

                // 🌟 核心：訂閱當線畫好、滑鼠放開時「傳出來」的像素座標事件
                string currentCamLoc = CameraLocation[i]; // 避免 Closure 變數陷阱
                picImage.MeasureLineDrawn += (startPt, endPt) =>
                {
                    // 當使用者在該張圖上放開滑鼠時，會自動跑到這裡：
                    CallBackLog?.Invoke("INFO", $"[{currentCamLoc}] 接收到傳出卡尺線。起點:({startPt.X:F1}, {startPt.Y:F1}), 終點:({endPt.X:F1}, {endPt.Y:F1})");

                    // 🚀 執行您的 Halcon 量測運算
                    ExecuteHalconMeasure(picImage, startPt, endPt);
                };

                // 建立 Tab 頁面與 Panel 容器
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
                // 取得當前畫面上 Tab 顯示的那個 PictureBox
                if (Image_tabControl.SelectedTab != null && Image_tabControl.SelectedTab.Controls.Count > 0)
                {
                    Panel panel = Image_tabControl.SelectedTab.Controls[0] as Panel;
                    if (panel != null && panel.Controls.Count > 0)
                    {
                        DrawLinePictureBox currentPic = panel.Controls[0] as DrawLinePictureBox;
                        if (currentPic != null)
                        {
                            // 🎯 啟動劃線模式！此時滑鼠移入會自動變十字準星，按住即可拉卡尺
                            currentPic.CurrentMouseMode = DrawLinePictureBox.MouseMode.DrawMeasureLine;
                        }
                    }
                }

            }
            else if (btn == Clear_btn)
            {
                

                CallBackLog?.Invoke("INFO", "量測工具：已清除卡尺線段，圖片還原可移動狀態。");
            }
        }



        /// <summary>
        /// 🔧 輔助工具：撈出當前 Image_tabControl 頁籤內正在顯示的 CustomPictureBox
        /// </summary>
        private void Image_tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Image_tabControl.SelectedTab != null)
            {
                currentPictureBox = Image_tabControl.SelectedTab.Controls.OfType<Panel>()
                    .FirstOrDefault()?.Controls.OfType<CustomPictureBox>().FirstOrDefault();
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

        private void ExecuteHalconMeasure(DrawLinePictureBox picBox, PointF imgStart, PointF imgEnd)
        {
            // 檢查目前分頁是否是量測卡尺對應的 Tab
            if (AlgorithmMethod_tabControl.SelectedTab == null || AlgorithmMethod_tabControl.SelectedTab.Name != "MeasurePos")
            {
                return;
            }

            if (picBox.Image == null) return;

            try
            {
                HImage hImage = new HImage();
                // 1. 將 C# Bitmap 轉換為 Halcon 影像物件
                HC.Bitmap2HImage(picBox.Image as Bitmap, out hImage);

                HTuple hCol, hAmplitude, hDistance;

                // 2. 呼叫您原有的卡尺量測函式（直接傳入傳出來的真實影像浮點座標）
                // 註：Halcon 的 Row對應 Y, Column對應 X
                HTuple hRow = MeasurePosFindPoint(
                    hImage,
                    imgStart.Y, imgStart.X,
                    imgEnd.Y, imgEnd.X,
                    10, 40, // 這裡的卡尺寬高參數可依您的實際 UI 欄位調整 (如 txt_RoiWidth.Text)
                    out hCol, out hAmplitude, out hDistance
                );

                System.Collections.Generic.List<PointF> resultPoints = new System.Collections.Generic.List<PointF>();

                // 🌟 2. 建立一個暫存的 DataTable 結構，用來更新給 DataGridView
                DataTable dtPoints = new DataTable();
                dtPoints.Columns.Add("No", typeof(int));
                dtPoints.Columns.Add("Column", typeof(string));
                dtPoints.Columns.Add("Row", typeof(string));

                if (hRow != null && hRow.Length > 0)
                {
                    CallBackLog?.Invoke("INFO", $"Halcon量測成功：找到 {hRow.Length} 個邊緣點。");

                    // 迴圈將 Halcon 座標組裝成 PointF (注意：Halcon 的 Row=Y, Col=X)
                    for (int k = 0; k < hRow.Length; k++)
                    {
                        float y = (float)hRow[k].D;
                        float x = (float)hCol[k].D;
                        resultPoints.Add(new PointF(x, y));

                        // 丟給 DataGridView 顯示用的資料列 (取到小數點後第3位)
                        DataRow row = dtPoints.NewRow();
                        row["No"] = k + 1;
                        row["Column"] = x.ToString("F3");
                        row["Row"] = y.ToString("F3");
                        dtPoints.Rows.Add(row);
                    }
                }

                // 🌟 3. 同步到 UI 執行緒更新控制項
                this.BeginInvoke(new Action(() =>
                {
                    // A. 讓圖片立刻重繪，在影像畫面上釘上紅色 Cross 十字
                    picBox.UpdateHalconResultPoints(resultPoints);

                    // B. 讓 dataGridView_Point 表格立刻刷出數據
                    dataGridView_Point.DataSource = dtPoints;
                }));

                // 4. 記得釋放 Halcon 物件避免記憶體洩漏 (Memory Leak)
                hImage.Dispose();
            }
            catch (Exception ex)
            {
                CallBackLog?.Invoke("ERROR", $"卡尺運算過程中發生異常：{ex.Message}");
            }
        }
    }
}