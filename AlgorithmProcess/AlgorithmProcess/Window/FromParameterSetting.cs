using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorithmProcess.Window
{
    public partial class FromParameterSetting : UserControl
    {
        // =============================== 視窗介面中的UserControl ===============================

        List<FromParameterSetting_StepDisplay> ListStepUI = null;

        // =============================== 視窗介面中的UserControl ===============================

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

        private MappingParameter mappingParameterFile = null, mappingParameterApply = null;

        private List<string> SlotList = new List<string>();

        private List<string> CameraLocation = new List<string>() { "Left", "Middle", "Right" };

        private int ImageIndex = 1;

        private int ProcessIndex = 0;

        private bool _isRunProcess = false;

        private int CameraMode = 0;

        List<List<DebugResult>> AllResultLists = null;

        List<DebugResult> CountSliceResultList = null, CalculateStackList = null, CalculateThinknessList = null, CalculateWarpageList = null, CalculateGapList = null, SingleSlotImageStitchResultList = null;

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

        private void apply_btn_Click(object sender, EventArgs e)
        {
            if (this.ListStepUI == null || this.ListStepUI.Count == 0)
            {
                CallBackLog?.Invoke("ERROR", $"錯誤：未載入任何步驟 UI 清單，無法套用參數。");
                return;
            }

            int totalUpdated = 0;

            // 🚀 1. 遍歷每一個 StepDisplay 畫面
            foreach (var stepUI in this.ListStepUI)
            {
                // 撈出該步驟目前畫面上所有的 Dictionary 參數
                Dictionary<string, double> uiParams = stepUI.GetCurrentParameters();
                if (uiParams.Count == 0) continue;

                // 💡 2. 取得目前的步驟類型名稱 (例如: "CountSlice"、"Stack")
                // 🔍 提示：請確保你在 new 出 stepUI 時，有將其 Name 或 Tag 設為對應的字串
                string stepType = stepUI.Name;

                // 3. 🎯 依據相機模式，先透過你的 switch-case 抓到那一個特定的「子參數物件」
                object targetSubParam = null;
                if (CameraMode == 3)
                {
                    targetSubParam = GetThreeCameraSubParameter(mappingParameterApply.ThreeCamera, stepType);
                }
                else if (CameraMode == 2)
                {
                    targetSubParam = GetTwoCameraSubParameter(mappingParameterApply.TwoCamera, stepType);
                }

                // 4. ✨ 將 UI 參數精準灌入這個找到的子參數物件中
                if (targetSubParam != null)
                {
                    // 呼叫我們剛剛寫的精準對接工具
                    int matchCount = BindToTarget(targetSubParam, uiParams);
                    totalUpdated += matchCount;

                    CallBackLog?.Invoke("INFO", $"步驟 [{stepType}] 成功對接同步 {matchCount} 筆參數。");
                }
                else
                {
                    CallBackLog?.Invoke("WARNING", $"步驟 [{stepType}] 無法找到對應的子參數物件，請確認步驟名稱與結構定義是否一致。");
                }
            }

            // 5. UI 狀態回報
            if (totalUpdated > 0)
            {
                CallBackLog?.Invoke("INFO", $"參數套用完成，共成功同步 {totalUpdated} 筆參數到對應結構中。");
                // TODO: 這裡可以直接呼叫寫入 INI/XML 存檔
            }
            else
            {
                CallBackLog?.Invoke("WARNING", $"警告：無任何參數成功寫入結構。");
            }
        }

        /// <summary>
        /// 依據步驟名稱，分流取得三相機架構下的子參數物件
        /// </summary>
        private object GetThreeCameraSubParameter(ThreeCameraParameter threeCam, string stepType)
        {
            switch (stepType)
            {
                case "CountSlice": return threeCam.CountSlice;
                case "Stack": return threeCam.Stack;
                case "Thickness": return threeCam.Thickness;
                case "Warpage": return threeCam.Warpage;
                case "Gap": return threeCam.Gap;
                case "Stitch": return threeCam.Stitch;
                default: return null;
            }
        }

        /// <summary>
        /// 依據步驟名稱，分流取得雙相機架構下的子參數物件
        /// </summary>
        private object GetTwoCameraSubParameter(TwoCameraParameter twoCam, string stepType)
        {
            switch (stepType)
            {
                case "CountSlice": return twoCam.CountSlice;
                case "Stack": return twoCam.Stack;
                case "Thickness": return twoCam.Thickness;
                case "Warpage": return twoCam.Warpage;
                case "Stitch": return twoCam.Stitch;
                default: return null;
            }
        }

        /// <summary>
        /// ✨ 精準反射：將 UI 的參數字典，只寫入指定的單一子參數實體中
        /// </summary>
        /// <param name="targetSubParam">特定的子參數物件 (例如 CountSlice 或 Stack)</param>
        /// <param name="uiParameters">該步驟 UI 傳回來的 Dictionary</param>
        public static int BindToTarget(object targetSubParam, Dictionary<string, double> uiParameters)
        {
            if (targetSubParam == null || uiParameters == null) return 0;
            int successCount = 0;

            // 1. 取得該子參數物件所有的公開屬性 (例如 L_Panel_threshold_value 等)
            PropertyInfo[] properties = targetSubParam.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // 2. 遍歷 UI 傳進來的每一個 [Key, Value]
            foreach (var kvp in uiParameters)
            {
                // 3. 在目標物件中尋找有沒有名字一模一樣的屬性
                PropertyInfo targetProp = targetSubParam.GetType().GetProperty(kvp.Key, BindingFlags.Public | BindingFlags.Instance);

                // ✨ 4. 找到了就精準寫入該物件，絕不外流給其他子類別！
                if (targetProp != null && targetProp.CanWrite)
                {
                    try
                    {
                        // 依據目標屬性的實際型別進行安全轉換 (支援 double, int, bool 等)
                        object convertedValue = Convert.ChangeType(kvp.Value, targetProp.PropertyType);
                        targetProp.SetValue(targetSubParam, convertedValue);

                        System.Diagnostics.Debug.WriteLine($"[精準寫入] {targetSubParam.GetType().Name}.{targetProp.Name} = {convertedValue}");
                        successCount++;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"[轉型失敗] 欄位 {kvp.Key} 寫入失敗: {ex.Message}");
                    }
                }
            }

            return successCount;
        }

        private void test_btn_Click(object sender, EventArgs e)
        {
            stepdisplay_tabControl.TabPages.Clear();

            SelectProcess_cb.Items.Clear();

            int index = SelectSlot_cb.SelectedIndex;

            int SlotNumber = int.Parse(SelectSlot_cb.Items[index].ToString());

            if (CameraMode == 3)
            {             
                OnMappingThreeCamera(SlotNumber, mappingParameterApply);
            }
            else if (CameraMode == 2)
            {
                OnMappingTwoCamera(SlotNumber, mappingParameterApply);
            }

            if(SelectProcess_cb.Items.Count > 0)
            {
                SelectProcess_cb.SelectedIndex = 0;

                SelectProcess_cb_SelectedIndexChanged(this, EventArgs.Empty);
            }
        }

        private void runprocess_btn_Click(object sender, EventArgs e)
        {
            stepdisplay_tabControl.TabPages.Clear(); 

            SelectProcess_cb.Items.Clear();

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

                mappingParameterFile = new MappingParameter();

                mappingParameterApply = new MappingParameter();

                string[] files = Directory.GetFiles(settingInfo.DirImagePath, $"*Middle*", SearchOption.TopDirectoryOnly);

                if (files.Length > 0)
                {
                    CameraMode = 3;

                    rOIList = RRI.ReadRecipeINI(settingInfo.RecipePath + "\\ROI.ini", 3);

                    mappingParameterFile = RAPI.ReadRecipeINI(settingInfo.RecipePath + "\\AlgorithmParameter.ini", 3);

                    OnMappingThreeCamera(SlotNumber, mappingParameterFile);
                }
                else
                {
                    CameraMode = 2;

                    rOIList = RRI.ReadRecipeINI(settingInfo.RecipePath + "\\ROI.ini", 2);

                    mappingParameterFile = RAPI.ReadRecipeINI(settingInfo.RecipePath + "\\AlgorithmParameter.ini", 2);

                    OnMappingTwoCamera(SlotNumber, mappingParameterFile);
                }

                mappingParameterApply = mappingParameterFile;

                _isRunProcess = true;

                if (SelectProcess_cb.Items.Count > 0)
                {
                    SelectProcess_cb.SelectedIndex = 0;

                    SelectProcess_cb_SelectedIndexChanged(this, EventArgs.Empty);
                }
            }
            else
            {
                CallBackLog?.Invoke("WARNING", $"先選擇Slot");
            }
        }

        private void OnMappingThreeCamera(int SlotNumber, MappingParameter mappingparameter)
        {
            Task.Run(() =>
            {

                string LeftImageName = settingInfo.DirImagePath + "\\" + "Slot" + SlotNumber.ToString("00") + "_Left.bmp";

                string MiddleImageName = settingInfo.DirImagePath + "\\" + "Slot" + SlotNumber.ToString("00") + "_Middle.bmp";

                string RightImageName = settingInfo.DirImagePath + "\\" + "Slot" + SlotNumber.ToString("00") + "_Right.bmp";
            });
        }

        private void OnMappingTwoCamera(int SlotNumber, MappingParameter mappingparameter)
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

            MappingModule MM_Clone = MM.Clone();

            MM_Clone.SetMode("teach");

            AllResultLists = new List<List<DebugResult>>();

            CountSliceResultList = new List<DebugResult>();

            CountL = MM_Clone.CountSlice_TwoCamera(_hImageLeft, _hImageRight, rOIList, mappingparameter, out CountR, out ResultType, out CountSliceResultList);

            SelectProcess_cb.Items.Add("CountSlice");

            AllResultLists.Add(CountSliceResultList);

            int a = 1;
        }

        private void SelectProcess_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = SelectProcess_cb.SelectedIndex;

            string itemText = SelectProcess_cb.SelectedItem?.ToString() ?? "None";

            if (index >= 0 && index < AllResultLists.Count)
            {
                List<DebugResult> selectedResultList = AllResultLists[index];

                ShowResultList(selectedResultList);

                ProcessIndex = index;
            }
        }

        public void ShowResultList(List<DebugResult> ResultList)
        {
            if (ResultList == null || ResultList.Count == 0) return;

            stepdisplay_tabControl.TabPages.Clear();

            ListStepUI = new List<FromParameterSetting_StepDisplay>();

            for (int i = 0; i < ResultList.Count; i++)
            {
                var result = ResultList[i];
                string tabTitle = result.Name;

                // 2. 建立新分頁 (TabPage)
                TabPage newPage = new TabPage(tabTitle);
                newPage.Name = $"tabPage_Step_{i}";
                newPage.Padding = new Padding(3); // 設定分頁內縮邊距

                // 3. 建立中間緩衝面板 (Panel)
                Panel containerPanel = new Panel();
                containerPanel.Name = $"panel_StepContainer_{i}";
                containerPanel.Dock = DockStyle.Fill;            // 面板填滿整頁 TabPage
                containerPanel.BackColor = Color.Transparent;    // 讓底色隨佈景切換

                // 💡 現場調機小技巧：可以開啟 Panel 的 AutoScroll，萬一小螢幕解析度跑掉，畫面不會被切掉
                //containerPanel.AutoScroll = true;

                // 4. 實例化數據顯示控制項 (UserControl)
                FromParameterSetting_StepDisplay stepUI = new FromParameterSetting_StepDisplay();
                stepUI.Dock = DockStyle.Fill;                    // 控制項填滿整個 Panel
                stepUI.Name = result.Type;                       // 設定控制項名稱為步驟名稱，方便後續識別
                stepUI.ShowResult(result);                       // 餵入演算法數據

                //把當前 UserControl 的 CallBackLog 直接轉接給 stepUI 的 CallBackLog
                stepUI.CallBackLog += (type, msg) =>
                {
                    this.CallBackLog?.Invoke(type, msg);
                };

                stepUI.CallBackPointInfo += OnPointInfoReceived;

                // 5. 【關鍵層級組合】
                containerPanel.Controls.Add(stepUI);             // A. 將 UserControl 加到 Panel 上
                newPage.Controls.Add(containerPanel);            // B. 將 Panel 加到 TabPage 上

                stepdisplay_tabControl.TabPages.Add(newPage);    // C. 將 TabPage 加到 TabControl 容器中

                ListStepUI.Add(stepUI);
            }
        }

        private void OnPointInfoReceived(int x, int y, int gray)
        {
            // 💡 加上執行緒安全保護（Invoke），防止演算法或背景重繪時跨執行緒引發 0x8000ffff 災難
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => OnPointInfoReceived(x, y, gray)));
                return;
            }

            if (x == -1 || y == -1)
            {
                // 移出範圍
                lbl_StatusCoordinate.Text = "X: --, Y: -- | Gray: --";
            }
            else
            {
                // 精準顯示於主畫面的 StatusStrip 標籤
                lbl_StatusCoordinate.Text = $"X: {x}, Y: {y} | Gray: {gray.ToString().PadLeft(3, ' ')}";
            }
        }
    }
}
