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
        // =============================== function建立 ===============================

        private ReadINI RI = new ReadINI();

        private ReadROIINI RRI = new ReadROIINI();

        private LogInfo LI = new LogInfo();

        private GenerateFoup GF = null;
        
        private MappingProcess MP = null;

        //private AlgorithmModule AM = null;

        // =============================== function建立 ===============================

        // =============================== 自定義設定檔路徑位置 ===============================

        private static string RootPath = "C:\\AlgorithmRecipe";

        private string ROIPath = RootPath + "\\ROI.ini";

        private string SettingPath = RootPath + "\\Setting.ini";

        private string Foup20Path = RootPath + "\\Image\\Foup_20.bmp";

        // =============================== 自定義設定檔路徑位置 ===============================

        // =============================== 全域變數宣告 ===============================

        private SettingInfo settingInfo = null;

        private ROIList rOIList = null;

        private bool _isLoadRecipe = false;

        // =============================== 全域變數宣告 ===============================

        public MainForm()
        {
            InitializeComponent();
            InitializeImageList();
            InitializeGUI();
            InitializeState();
        }

        private void InitializeImageList()
        {
            settingInfo = new SettingInfo();

            settingInfo = RI.ReadRecipeINI(SettingPath);

            // 檢查路徑是否存在，避免程式崩潰
            if (Directory.Exists(settingInfo.ImageParh))
            {
                // 清除舊有的項目
                loadImagefile_lb.Items.Clear();

                // 取得該路徑下的所有目錄資訊
                DirectoryInfo d = new DirectoryInfo(settingInfo.ImageParh);
                DirectoryInfo[] folders = d.GetDirectories();

                foreach (DirectoryInfo folder in folders)
                {
                    // 只將資料夾名稱（不含完整路徑）加入 ListBox
                    loadImagefile_lb.Items.Add(folder.Name);
                }
                LI.AddLog(Msg_RichTextBox, "INFO", "成功載入" + settingInfo.ImageParh);
            }
            else
            {
                LI.AddLog(Msg_RichTextBox, "WARNING", "找不到指定的路徑！");
            }
        }
        private void InitializeGUI()
        {
            GF = new GenerateFoup();
            GF._IsFoupInitialFinish = false;
            GF._IsDrawGlass = true;

            GF.ReadFoupImage(Foup20Path);

            Bitmap FoupImage = GF.DrawFoupGlass(-1);

            Foup_PictureBox.Image = (Bitmap)FoupImage.Clone();

            GF._IsFoupInitialFinish = true;
        }

        private void InitializeState()
        {
            switchmode_cb.SelectedIndex = 0;
            runmode_cb.SelectedIndex = 0;
        }

        private void loadimage_btn_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "請選擇目標資料夾";
                fbd.ShowNewFolderButton = true; // 允許建立新資料夾

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = fbd.SelectedPath;

                    settingInfo.ImageParh = folderPath;

                    RI.WriteRecipeINI(SettingPath, settingInfo);

                    LI.AddLog(Msg_RichTextBox, "INFO", "切換圖片路徑" + settingInfo.ImageParh);
                }
            }
        }

        private void runalgorithm_btn_Click(object sender, EventArgs e)
        {
            if (loadImagefile_lb.SelectedItem != null)
            {
                // 取得選取的文字
                string selectedItem = loadImagefile_lb.SelectedItem.ToString();
                LI.AddLog(Msg_RichTextBox, "INFO", $"當下選擇的項目是: {selectedItem}");

                if (!_isLoadRecipe)
                {
                    LI.AddLog(Msg_RichTextBox, "WARNING", "請先載入Recipe");
                }
                else
                {
                    MP = new MappingProcess();

                    if (switchmode_cb.SelectedIndex == 0)
                    {
                        // 兩相機
                        LI.AddLog(Msg_RichTextBox, "INFO", "Start Two Cameras Process");

                        //MP.OnMappingForTwoCamera(settingInfo.ImageParh + "\\" + selectedItem, rOIList);
                    }
                    else if(switchmode_cb.SelectedIndex == 1)
                    {
                        // 三相機
                        LI.AddLog(Msg_RichTextBox, "INFO", "Start Three Cameras Process");

                        MP.OnMappingForThreeCamera(settingInfo.ImageParh + "\\" + selectedItem, rOIList);
                    }
                }                                    
            }
            else
            {
                LI.AddLog(Msg_RichTextBox, "WARNING", "尚未選擇任何項目");
            }
        }

        private void selectrecipe_btn_Click(object sender, EventArgs e)
        {
            try
            {
                rOIList = new ROIList();

                rOIList = RRI.ReadRecipeINI(ROIPath, 2);

                _isLoadRecipe = true;

                LI.AddLog(Msg_RichTextBox, "INFO", $"載入Recipe成功");
            }
            catch (Exception ex)
            {
                LI.AddLog(Msg_RichTextBox, "WARNING", $"載入Recipe失敗: {ex.Message}");
            }
        }

        private void ImageCropforTraining_btn_Click(object sender, EventArgs e)
        {
            ImageCropForTraining ImageCropForTraining = new ImageCropForTraining();
            ImageCropForTraining.ShowDialog();
        }
    }
}
