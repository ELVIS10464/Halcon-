using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorithmProcess.Window
{
    public partial class FromImageCrop : UserControl
    {
        // =============================== function建立 ===============================

        private readonly ImageConverter _ImageConverter = new ImageConverter();

        // =============================== function建立 ===============================

        // =============================== 視窗CustomPictureBox建立 ===============================
        CustomPictureBox picThreeLeft = new CustomPictureBox
        {
            Dock = DockStyle.Fill,
        };

        CustomPictureBox picThreeMiddle = new CustomPictureBox
        {
            Dock = DockStyle.Fill,
        };

        CustomPictureBox picThreeRight = new CustomPictureBox
        {
            Dock = DockStyle.Fill,
        };

        CustomPictureBox picTwoLeft = new CustomPictureBox
        {
            Dock = DockStyle.Fill,
        };

        CustomPictureBox picTwoRight = new CustomPictureBox
        {
            Dock = DockStyle.Fill,
        };

        // =============================== 視窗CustomPictureBox建立 ===============================

        // =============================== 全域變數宣告 ===============================

        private SettingInfo settingInfo = null;

        private List<string> SlotList = new List<string>();

        private List<string> CameraLocation = new List<string>() { "Left", "Middle", "Right" };

        private int ImageIndex = 1;

        // =============================== 全域變數宣告 ===============================

        public FromImageCrop()
        {
            InitializeComponent();
            InitializePictureBox(); //視窗CustomPictureBox事件綁定
            InitializeImageLoad(settingInfo);
        }


        public void InitializePictureBox()
        {
            //ThreeLeft
            picThreeLeft.ImagePointClicked += pt =>
            {
                PointF imgPt = pt;
            };

            picThreeLeft.CallBackShowMenu += new CustomPictureBox.CallBackReturnShowMenu(OnCallBackShowMenu);

            threeleft_panel.Controls.Add(picThreeLeft);

            //ThreeMiddle
            picThreeMiddle.ImagePointClicked += pt =>
            {
                PointF imgPt = pt;
            };

            picThreeMiddle.CallBackShowMenu += new CustomPictureBox.CallBackReturnShowMenu(OnCallBackShowMenu);

            threemiddle_panel.Controls.Add(picThreeMiddle);

            //ThreeRight
            picThreeRight.ImagePointClicked += pt =>
            {
                PointF imgPt = pt;
            };

            picThreeRight.CallBackShowMenu += new CustomPictureBox.CallBackReturnShowMenu(OnCallBackShowMenu);

            threeright_panel.Controls.Add(picThreeRight);

            //TwoLeft
            picTwoLeft.ImagePointClicked += pt =>
            {
                PointF imgPt = pt;
            };

            picTwoLeft.CallBackShowMenu += new CustomPictureBox.CallBackReturnShowMenu(OnCallBackShowMenu);

            twoleft_panel.Controls.Add(picTwoLeft);

            //TwoRight
            picTwoRight.ImagePointClicked += pt =>
            {
                PointF imgPt = pt;
            };

            picTwoRight.CallBackShowMenu += new CustomPictureBox.CallBackReturnShowMenu(OnCallBackShowMenu);

            tworight_panel.Controls.Add(picTwoRight);
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
                ShowImage();
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

            if ((index - 1) == 0)
            {
                lastimage_btn.Visible = false;
            }
            else
            {
                lastimage_btn.Visible = true;
            }

            if ((index + 1) == settingInfo.SlotNumber)
            {
                nextimage_btn.Visible = false;
            }
            else
            {
                nextimage_btn.Visible = true;
            }

            ImageIndex = index + 1;

            ShowImage();
        }

        private void Btn_ClickEvent(object sender, EventArgs e)
        {
            var btn = sender as Button;

            if (btn == lastimage_btn && ImageIndex > 1)
            {
                ImageIndex--; // 簡寫：同等於 ImageIndex = ImageIndex - 1;
                ShowImage();
                
                lastimage_btn.Visible = (ImageIndex != 1);                
                nextimage_btn.Visible = true;
            }
            else if (btn == nextimage_btn && ImageIndex < settingInfo.SlotNumber)
            {
                ImageIndex++; // 簡寫：同等於 ImageIndex = ImageIndex + 1;
                ShowImage();

                nextimage_btn.Visible = (ImageIndex != settingInfo.SlotNumber);               
                lastimage_btn.Visible = true;
            }
        }

        private void ShowImage()
        {
            SelectSlot_cb.Text = ImageIndex.ToString("00");
            string[] files = Directory.GetFiles(settingInfo.DirImagePath, $"*Middle*", SearchOption.TopDirectoryOnly);

            if (files.Length > 0)
            {
                tabControl1.SelectedIndex = 0;

                string LeftImageName = settingInfo.DirImagePath + "\\Slot" + ImageIndex.ToString("D2") + "_Left.bmp";
                string MiddleImageName = settingInfo.DirImagePath + "\\Slot" + ImageIndex.ToString("D2") + "_Middle.bmp";
                string RightImageName = settingInfo.DirImagePath + "\\Slot" + ImageIndex.ToString("D2") + "_Right.bmp";

                Bitmap LeftImage = (Bitmap)_ImageConverter.ConvertFrom(File.ReadAllBytes(LeftImageName));
                Bitmap MiddleImage = (Bitmap)_ImageConverter.ConvertFrom(File.ReadAllBytes(MiddleImageName));
                Bitmap RightImage = (Bitmap)_ImageConverter.ConvertFrom(File.ReadAllBytes(RightImageName));

                if (picThreeLeft.Image != null)
                {
                    picThreeLeft.Image.Dispose();
                }

                picThreeLeft.Image = (Bitmap)LeftImageName.Clone();
                picThreeLeft.FitWindow();

                if (picThreeMiddle.Image != null)
                {
                    picThreeMiddle.Image.Dispose();
                }

                picThreeMiddle.Image = (Bitmap)MiddleImage.Clone();
                picThreeMiddle.FitWindow();

                if (picThreeRight.Image != null)
                {
                    picThreeRight.Image.Dispose();
                }

                picThreeRight.Image = (Bitmap)RightImage.Clone();
                picThreeRight.FitWindow();
            }
            else
            {
                tabControl1.SelectedIndex = 1;

                string LeftImageName = settingInfo.DirImagePath + "\\Slot" + ImageIndex.ToString("D2") + "_Left.bmp";
                string RightImageName = settingInfo.DirImagePath + "\\Slot" + ImageIndex.ToString("D2") + "_Right.bmp";

                Bitmap LeftImage = (Bitmap)_ImageConverter.ConvertFrom(File.ReadAllBytes(LeftImageName));
                Bitmap RightImage = (Bitmap)_ImageConverter.ConvertFrom(File.ReadAllBytes(RightImageName));

                if (picTwoLeft.Image != null)
                {
                    picTwoLeft.Image.Dispose();
                }

                picTwoLeft.Image = (Bitmap)LeftImage.Clone();
                picTwoLeft.FitWindow();

                if (picTwoRight.Image != null)
                {
                    picTwoRight.Image.Dispose();
                }

                picTwoRight.Image = (Bitmap)RightImage.Clone();
                picTwoRight.FitWindow();
            }
        }

        private void ShowROIData()
        {

        }
    }
}
