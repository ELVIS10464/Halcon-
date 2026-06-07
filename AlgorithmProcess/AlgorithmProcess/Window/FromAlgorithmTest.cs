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
using static AlgorithmProcess.Window.FromAlgorithmTest_MeasurePos;

namespace AlgorithmProcess.Window
{
    public partial class FromAlgorithmTest : UserControl
    {
        // =============================== 視窗介面中的UserControl ===============================

        private FromAlgorithmTest_MeasurePos fromAlgorithmTest_MeasurePos = null;

        // =============================== 視窗介面中的UserControl ===============================

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

        // =============================== 全域變數宣告 ===============================

        public FromAlgorithmTest()
        {
            InitializeComponent();
            InitializeMainForm();
            InitializeImageLoad(settingInfo);
        }

        private void InitializeMainForm()
        {
            fromAlgorithmTest_MeasurePos = new FromAlgorithmTest_MeasurePos();
            fromAlgorithmTest_MeasurePos.Dock = DockStyle.Fill;
            Parameter_panel.Controls.Add(fromAlgorithmTest_MeasurePos);
            fromAlgorithmTest_MeasurePos.CallBackBtn += OnBtnReceived;

            //把當前 UserControl 的 CallBackLog 直接轉接給 stepUI 的 CallBackLog
            fromAlgorithmTest_MeasurePos.CallBackLog += (type, msg) =>
            {
                this.CallBackLog?.Invoke(type, msg);
            };

            fromAlgorithmTest_MeasurePos.BringToFront();
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
                    fromAlgorithmTest_MeasurePos.ExecuteHalconMeasure(picImage, startPt, endPt);
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
                fromAlgorithmTest_MeasurePos.BringToFront();
            }
        }

        private void OnBtnReceived(string type)
        {
            AdvancedPictureBox currentPic = GetCurrentActivePictureBox();

            if (type == "DrawLine")
            {
                if (currentPic != null)
                {
                    currentPic.CurrentMode = AdvancedPictureBox.InteractionMode.DrawMeasureLine;
                    CallBackLog?.Invoke("INFO", "表單A：已透過按鈕啟動卡尺劃線模式。");
                }
            }
            else if (type == "Clear")
            {
                if (currentPic != null)
                {

                    // 清除畫布線段與結果點
                    currentPic.CurrentMode = AdvancedPictureBox.InteractionMode.None; // 恢復常規移動/縮放模式
                    currentPic.ClearAllGraphics(); // 清空十字特徵點

                    // 假如主視窗上有 DataGridView 也可以在這裡一併清空
                    fromAlgorithmTest_MeasurePos.ClearDataGridView();

                    CallBackLog?.Invoke("INFO", "主視窗：已清除卡尺線段與量測結果。");
                }
            }
        }

        private void Image_tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPictureBox = GetCurrentActivePictureBox();

            if (currentPictureBox == null) return;

            // 3. 檢查這張影像是否已經有畫過線（防呆：起終點重合代表尚未畫線）
            bool hasLine = currentPictureBox.m_ImgStartPt != currentPictureBox.m_ImgEndPt &&
                           (currentPictureBox.m_ImgStartPt.X != 0 || currentPictureBox.m_ImgStartPt.Y != 0);


            if (hasLine)
            {
                // 情況 A：這張影像之前畫過線，切換回來時，帶入舊座標重新計算並更新 DataGridView
                CallBackLog?.Invoke("INFO", $"切換影像 Tab，重新計算 [{Image_tabControl.SelectedTab.Text}] 的量測數據。");
                fromAlgorithmTest_MeasurePos.ExecuteHalconMeasure(currentPictureBox, currentPictureBox.m_ImgStartPt, currentPictureBox.m_ImgEndPt);
            }
            else
            {
                // 情況 B：這張影像全新、還沒畫過線，直接清空 DataGridView，避免殘留上一張影像的數據
                fromAlgorithmTest_MeasurePos.ClearDataGridView();
                CallBackLog?.Invoke("INFO", $"切換至未量測影像 [{Image_tabControl.SelectedTab.Text}]，已清空數據表格。");
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