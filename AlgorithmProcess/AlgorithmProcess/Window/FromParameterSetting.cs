using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorithmProcess.Window
{
    public partial class FromParameterSetting : UserControl
    {
        // =============================== function建立 ===============================

        private readonly ImageConverter _ImageConverter = new ImageConverter();

        private ReadROIINI RRI = null;

        private ReadAlgorithmParameterINI RAPI = null;

        private HalconImageConverter HC = null;

        private MappingModule MM = null;

        // =============================== function建立 ===============================

        // =============================== 事件與委託 ===============================

        public delegate void CallBackReturnLog(string type, string msg);
        public event CallBackReturnLog CallBackLog;

        // =============================== 事件與委託 ===============================

        // =============================== 全域變數宣告 ===============================

        private SettingInfo settingInfo = null;

        private ROIList rOIList = null;

        private MappingParameter mappingParameter = null;

        private List<string> SlotList = new List<string>();

        private List<string> CameraLocation = new List<string>() { "Left", "Middle", "Right" };

        private int ImageIndex = 1;

        private bool _isRunProcess = false;
        // =============================== 全域變數宣告 ===============================
        public FromParameterSetting()
        {
            InitializeComponent();
            InitializeImageLoad(settingInfo); // 初始化圖片載入
        }

        public void InitializeImageLoad(SettingInfo settingInfo)
        {
            this.settingInfo = settingInfo;

            if (this.settingInfo != null && this.settingInfo.DirImagePath != "")
            {
                SlotList = new List<string>();

                SelectSlot_cb.Items.Clear();

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

        private void selectprocess_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isRunProcess)
            {

            }
            else
            {
                CallBackLog?.Invoke("WARNING", $"先執行演算法");
            }
        }

        private void runprocess_btn_Click(object sender, EventArgs e)
        {
            int index = SelectSlot_cb.SelectedIndex;

            int SlotNumber = int.Parse(SelectSlot_cb.Items[index].ToString());

            if (SelectSlot_cb.SelectedIndex >= 0)
            {

                RRI = new ReadROIINI();

                RAPI = new ReadAlgorithmParameterINI();

                //把當前 UserControl 的 CallBackLog 直接轉接給 RAPI 的 CallBackLog
                RAPI.CallBackLog += (type, msg) =>
                {
                    this.CallBackLog?.Invoke(type, msg);
                };

                rOIList = new ROIList();

                mappingParameter = new MappingParameter();

                string[] files = Directory.GetFiles(settingInfo.DirImagePath, $"*Middle*", SearchOption.TopDirectoryOnly);

                if (files.Length > 0)
                {
                    rOIList = RRI.ReadRecipeINI(settingInfo.RecipePath + "\\ROI.ini", 3);

                    mappingParameter = RAPI.ReadRecipeINI(settingInfo.RecipePath + "\\AlgorithmParameter.ini", 3);
                   
                    OnMappingThreeCamera(SlotNumber);
                }
                else
                {
                    rOIList = RRI.ReadRecipeINI(settingInfo.RecipePath + "\\ROI.ini", 2);

                    mappingParameter = RAPI.ReadRecipeINI(settingInfo.RecipePath + "\\AlgorithmParameter.ini", 2);

                    OnMappingTwoCamera(SlotNumber);
                }

                _isRunProcess = true;
            }
            else
            {
                CallBackLog?.Invoke("WARNING", $"先選擇Slot");
            }
        }

        private void OnMappingThreeCamera(int SlotNumber)
        {
            Task.Run(() => {

                string LeftImageName = settingInfo.DirImagePath + "\\" + "Slot" + SlotNumber.ToString("00") + "_Left.bmp";

                string MiddleImageName = settingInfo.DirImagePath + "\\" + "Slot" + SlotNumber.ToString("00") + "_Middle.bmp";

                string RightImageName = settingInfo.DirImagePath + "\\" + "Slot" + SlotNumber.ToString("00") + "_Right.bmp";
            });
        }

        private void OnMappingTwoCamera(int SlotNumber)
        {
            HC = new HalconImageConverter();

            MM = new MappingModule();

            string LeftImageName = settingInfo.DirImagePath + "\\" + "Slot" + SlotNumber.ToString("00") + "_Left.bmp";

            string RightImageName = settingInfo.DirImagePath + "\\" + "Slot" + SlotNumber.ToString("00") + "_Right.bmp";

            Bitmap LeftImage = (Bitmap)_ImageConverter.ConvertFrom(File.ReadAllBytes(LeftImageName));

            Bitmap RightImage = (Bitmap)_ImageConverter.ConvertFrom(File.ReadAllBytes(RightImageName));

            HImage _hImageLeft = new HImage(), _hImageMiddle = new HImage(), _hImageRight = new HImage();

            HC.Bitmap2HImage(LeftImage, out _hImageLeft);
            HC.Bitmap2HImage(RightImage, out _hImageRight);

            int CountL = 0, CountR = 0, ResultType = 0;

            double Thickness = 99999, Warpage = 99999, LeftGap = 99999, RightGap = 99999;

            HImage _hSingleSlotStitch = new HImage();

            HTuple _hAllThickness = new HTuple();

            List<DebugResult> CountSliceResultList = new List<DebugResult>(), CalculateStackList = new List<DebugResult>(), CalculateThinknessList = new List<DebugResult>(), CalculateWarpageList = new List<DebugResult>(), CalculateGapList = new List<DebugResult>(), SingleSlotImageStitchResultList = new List<DebugResult>();

            MappingModule MM_Clone = MM.Clone();

            CountL = MM_Clone.CountSlice_TwoCamera(_hImageLeft, _hImageRight, mappingParameter, out CountR, out ResultType, out CountSliceResultList);


        }
    }
}
