using AlgorithmProcess.Window;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorithmProcess
{
    public partial class MainForm : Form
    {
        // =============================== 視窗介面中的UserControl ===============================
        private FromDisplayResult fromDisplayResult = null;

        private FromImageCrop fromImageCrop = null;

        private FromParameterSetting fromParameterSetting = null;
        // =============================== 視窗介面中的UserControl ===============================


        // =============================== function建立 ===============================

        private ReadDefaultINI RI = new ReadDefaultINI();

        private ReadROIINI RRI = new ReadROIINI();

        private ReadAlgorithmParameterINI RAPI = new ReadAlgorithmParameterINI();

        private LogInfo LI = new LogInfo();

        private GenerateFoup GF = null;

        private MappingProcess MP = null;

        //private AlgorithmModule AM = null;

        // =============================== function建立 ===============================

        // =============================== 事件與委託 ===============================

        //public delegate void CallBackReturnLog(LogInfo LI);
        //public event CallBackReturnLog CallBackLog;

        // =============================== 事件與委託 ===============================

        // =============================== 自定義設定檔路徑位置 ===============================

        private static string RootPath = "C:\\AlgorithmRecipe";

        //private string ROIPath = RootPath + "\\ROI.ini";

        private string SettingPath = RootPath + "\\Setting.ini";

        private string Foup20Path = RootPath + "\\Image\\Foup_20.bmp";

        // =============================== 自定義設定檔路徑位置 ===============================

        // =============================== 全域變數宣告 ===============================
        private List<Button> BtnList = new List<Button>();

        private SettingInfo settingInfo = null;     

        private bool _isLoadRecipe = false;

        // =============================== 全域變數宣告 ===============================

        public MainForm()
        {
            InitializeComponent();
            InitializeDefaultPathSetting(); //預設影像、儲存與Recipe路徑
            InitializeMainForm();
            //InitializeGUI();
            InitializeState();
        }

        private void InitializeDefaultPathSetting()
        {
            settingInfo = new SettingInfo();

            settingInfo = RI.ReadINI(SettingPath);

            // LoadImagePath
            if (Directory.Exists(settingInfo.LoadImagePath))
            {
                // 清除舊有的項目
                loadImagefile_lb.Items.Clear();

                // 取得該路徑下的所有目錄資訊
                DirectoryInfo d = new DirectoryInfo(settingInfo.LoadImagePath);
                DirectoryInfo[] folders = d.GetDirectories();

                foreach (DirectoryInfo folder in folders)
                {
                    // 只將資料夾名稱（不含完整路徑）加入 ListBox
                    loadImagefile_lb.Items.Add(folder.Name);
                }

                LI.AddLog(Msg_RichTextBox, "INFO", "成功載入" + settingInfo.LoadImagePath);

                loadimagepath_tb.Text = settingInfo.LoadImagePath;
            }
            else
            {
                LI.AddLog(Msg_RichTextBox, "WARNING", "找不到指定的路徑！");
            }

            // RecipePath
            if (Directory.Exists(settingInfo.RecipePath))
            {
                LI.AddLog(Msg_RichTextBox, "INFO", "成功載入" + settingInfo.RecipePath);

                recipename_tb.Text = settingInfo.RecipePath;
            }
            else
            {
                LI.AddLog(Msg_RichTextBox, "WARNING", "找不到指定的路徑！");
            }

            // SaveResultPath
            if (Directory.Exists(settingInfo.SaveResultPath))
            {
                // 清除舊有的項目
                saveImagefile_lb.Items.Clear();

                // 取得該路徑下的所有目錄資訊
                DirectoryInfo d = new DirectoryInfo(settingInfo.SaveResultPath);
                DirectoryInfo[] folders = d.GetDirectories();

                foreach (DirectoryInfo folder in folders)
                {
                    // 只將資料夾名稱（不含完整路徑）加入 ListBox
                    saveImagefile_lb.Items.Add(folder.Name);
                }

                LI.AddLog(Msg_RichTextBox, "INFO", "成功載入" + settingInfo.SaveResultPath);

                saveimagepath_tb.Text = settingInfo.SaveResultPath;
            }
            else
            {
                LI.AddLog(Msg_RichTextBox, "WARNING", "找不到指定的路徑！");
            }
        }

        private void InitializeMainForm()
        {
            fromDisplayResult = new FromDisplayResult();
            fromDisplayResult.Dock = DockStyle.Fill;
            main_panel.Controls.Add(fromDisplayResult);

            fromImageCrop = new FromImageCrop();
            fromImageCrop.Dock = DockStyle.Fill;
            main_panel.Controls.Add(fromImageCrop);
            fromImageCrop.CallBackLog += OnLogReceived;

            fromParameterSetting = new FromParameterSetting();
            fromParameterSetting.Dock = DockStyle.Fill;
            main_panel.Controls.Add(fromParameterSetting);
            fromParameterSetting.CallBackLog += OnLogReceived;

            fromDisplayResult.BringToFront();
        }
        //private void InitializeGUI()
        //{
        //    GF = new GenerateFoup();
        //    GF._IsFoupInitialFinish = false;
        //    GF._IsDrawGlass = true;

        //    GF.ReadFoupImage(Foup20Path);

        //    Bitmap FoupImage = GF.DrawFoupGlass(-1);

        //    Foup_PictureBox.Image = (Bitmap)FoupImage.Clone();

        //    GF._IsFoupInitialFinish = true;
        //}

        private void InitializeState()
        {
            RAPI.CallBackLog += OnLogReceived;
        }

        private void Btn_ClickEvent(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == home_btn)
            {
                fromDisplayResult.BringToFront();
            }
            else if (btn == ImageCropData_btn)
            {
                fromImageCrop.BringToFront();
            }
            else if (btn == parameter_btn)
            {
                fromParameterSetting.BringToFront();
            }
        }


        //private void runalgorithm_btn_Click(object sender, EventArgs e)
        //{
        //    if (loadImagefile_lb.SelectedItem != null)
        //    {
        //        // 取得選取的文字
        //        string selectedItem = loadImagefile_lb.SelectedItem.ToString();
        //        LI.AddLog(Msg_RichTextBox, "INFO", $"當下選擇的項目是: {selectedItem}");

        //        if (!_isLoadRecipe)
        //        {
        //            LI.AddLog(Msg_RichTextBox, "WARNING", "請先載入Recipe");
        //        }
        //        else
        //        {
        //            MP = new MappingProcess();

        //            if (switchmode_cb.SelectedIndex == 0)
        //            {
        //                // 兩相機
        //                LI.AddLog(Msg_RichTextBox, "INFO", "Start Two Cameras Process");

        //                //MP.OnMappingForTwoCamera(settingInfo.ImageParh + "\\" + selectedItem, rOIList);
        //            }
        //            else if(switchmode_cb.SelectedIndex == 1)
        //            {
        //                // 三相機
        //                LI.AddLog(Msg_RichTextBox, "INFO", "Start Three Cameras Process");

        //                MP.OnMappingForThreeCamera(settingInfo.ImageParh + "\\" + selectedItem, rOIList);
        //            }
        //        }                                    
        //    }
        //    else
        //    {
        //        LI.AddLog(Msg_RichTextBox, "WARNING", "尚未選擇任何項目");
        //    }
        //}

        private void changesetting_btn_Click(object sender, EventArgs e)
        {

        }

        private void OnLogReceived(string type, string msg)
        {
            // 這裡可以直接呼叫 MainForm 的方法來更新 Log
            LI.AddLog(Msg_RichTextBox, type, msg);
        }

        private void loadImagefile_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1. 安全檢查：防範使用者點到空白處導致 Index 為 -1
            if (loadImagefile_lb.SelectedIndex == -1) return;

            // 2. 取得選取項目的資訊 (根據你塞進 ListBox 的內容調整)
            string selectedImageItem = loadImagefile_lb.SelectedItem.ToString();

            settingInfo.DirImagePath = settingInfo.LoadImagePath + "\\" + selectedImageItem + "\\Image";

            OnLogReceived("INFO", $"選擇的影像資料夾: {selectedImageItem}");

            // 3. *** 關鍵步驟：直接呼叫 UserControl 的公開方法，把路徑丟過去 ***
            fromImageCrop.InitializeImageLoad(settingInfo);

            fromParameterSetting.InitializeImageLoad(settingInfo);
        }
    }
}
