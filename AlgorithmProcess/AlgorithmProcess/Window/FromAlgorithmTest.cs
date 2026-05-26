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

        private List<string> CameraLocation = null;

        private int ImageIndex = 1;

        private int ProcessIndex = 0;

        private bool _isRunProcess = false;

        private int CameraMode = 0;

        List<List<DebugResult>> AllResultLists = null;

        List<DebugResult> CountSliceResultList = null, CalculateStackList = null, CalculateThinknessList = null, CalculateWarpageList = null, CalculateGapList = null, SingleSlotImageStitchResultList = null;

        // =============================== 全域變數宣告 ===============================

        // =============================== 視窗CustomPictureBox建立 ===============================

        // =============================== 視窗CustomPictureBox建立 ===============================

        public FromAlgorithmTest()
        {
            InitializeComponent();
            InitializePictureBox(); //視窗CustomPictureBox事件綁定
            InitializeImageLoad(settingInfo); // 初始化圖片載入
        }

        public void InitializePictureBox()
        {
            //Image
            
            //threeleft_panel.Controls.Add(picImage);            
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

        private void OnCallBackShowMenu(CustomPictureBox pic)
        {
            CreateRightButtonMenu(pic);
        }

        public void CreateRightButtonMenu(CustomPictureBox pictureBox)
        {
            ContextMenuStrip contextMenuStrip = new System.Windows.Forms.ContextMenuStrip();

            pictureBox.ContextMenuStrip = contextMenuStrip;

            // 選單1項 (Add ROI)

            ToolStripMenuItem toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem1.Text = "ROI Setting";
            contextMenuStrip.Items.Add(toolStripMenuItem1);

            ToolStripMenuItem toolStripMenuItem11 = new ToolStripMenuItem();
            toolStripMenuItem11.Text = "Add ROI";

            ToolStripMenuItem toolStripMenuItem12 = new ToolStripMenuItem();
            toolStripMenuItem12.Text = "Delete ROI";

            toolStripMenuItem1.DropDownItems.Add(toolStripMenuItem11);
            toolStripMenuItem1.DropDownItems.Add(toolStripMenuItem12);

            toolStripMenuItem11.Click += (sender11, e11) =>
            {
                if (pictureBox.Image == null)
                {
                    return;
                }

                int offset = 20 * pictureBox.RoiList.Count;
                int x = 100 + offset, y = 100 + offset, w = 300, h = 300;

                Rectangle rect = new Rectangle(x, y, w, h);
                pictureBox.AddRoi(rect);
            };

            toolStripMenuItem12.Click += (sender12, e12) =>
            {
                pictureBox.DeleteActiveRoi();
            };

            ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem2.Text = "Image Window Setting";
            contextMenuStrip.Items.Add(toolStripMenuItem2);

            ToolStripMenuItem toolStripMenuItem21 = new ToolStripMenuItem();
            toolStripMenuItem21.Text = "Fit Window";

            toolStripMenuItem2.DropDownItems.Add(toolStripMenuItem21);

            toolStripMenuItem21.Click += (sender21, e21) =>
            {
                pictureBox.FitWindow();
            };

            ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem3.Text = "Save Image";
            contextMenuStrip.Items.Add(toolStripMenuItem3);

            toolStripMenuItem3.Click += (sender3, e3) =>
            {
                saveFileDialog1.Filter = "PNG Image|*.png";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Bitmap bmp = (Bitmap)pictureBox.Image.Clone();

                    bmp.Save(saveFileDialog1.FileName);
                }

            };
        }


        private void SelectSlot_cb_SelectedIndexChanged(object sender, EventArgs e)
        {

            int index = SelectSlot_cb.SelectedIndex;

            int ImageIndex = index + 1;

            Image_tabControl.TabPages.Clear();

            string[] files = Directory.GetFiles(settingInfo.DirImagePath, $"*Middle*", SearchOption.TopDirectoryOnly);
            
            //if (files.Length > 0)
            //{
            //    CameraLocation = new List<string>() { "Left", "Middle", "Right" };
                
            //}
            //else
            //{
            //    CameraLocation = new List<string>() { "Left", "Right" };
            //}

            for(int i = 0; i < CameraLocation.Count; i++)
            {

                CustomPictureBox picImage = new CustomPictureBox
                {
                    Dock = DockStyle.Fill,
                };

                picImage.ImagePointClicked += pt =>
                {
                    PointF imgPt = pt;
                };

                picImage.CallBackShowMenu += new CustomPictureBox.CallBackReturnShowMenu(OnCallBackShowMenu);

                string ImageName = settingInfo.DirImagePath + "\\Slot" + ImageIndex.ToString("D2") + $"_{CameraLocation[i]}.bmp";

                Bitmap Image = (Bitmap)_ImageConverter.ConvertFrom(File.ReadAllBytes(ImageName));

                if (picImage.Image != null)
                {
                    picImage.Image.Dispose();
                }

                picImage.Image = (Bitmap)Image.Clone();
                picImage.FitWindow();
                        
                TabPage newPage = new TabPage(CameraLocation[i]);
                newPage.Name = $"{CameraLocation[i]}";

                Panel containerPanel = new Panel();
                containerPanel.Name = $"panel_StepContainer_{i}";
                containerPanel.Dock = DockStyle.Fill;            // 面板填滿整頁 TabPage
                containerPanel.BackColor = Color.Transparent;    // 讓底色隨佈景切換

                containerPanel.Controls.Add(picImage);

                newPage.Controls.Add(containerPanel);            // 將 Panel 加到 TabPage 上

                Image_tabControl.TabPages.Add(newPage);    // 將 TabPage 加到 TabControl 容器中
            }
            
        }
    }
}
