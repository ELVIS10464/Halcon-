using Sunny.UI.Win32;
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
using System.Drawing.Imaging;

namespace AlgorithmProcess.Window
{
    public partial class FromImageCrop : UserControl
    {
        // =============================== function建立 ===============================

        private readonly ImageConverter _ImageConverter = new ImageConverter();

        private ReadROIINI RRI = null;

        // =============================== function建立 ===============================

        // =============================== 事件與委託 ===============================

        public delegate void CallBackReturnLog(string type, string msg);
        public event CallBackReturnLog CallBackLog;

        // =============================== 事件與委託 ===============================

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

        private ROIList rOIList = null;

        private List<string> SlotList = new List<string>();

        private List<string> CameraLocation = new List<string>() { "Left", "Middle", "Right" };

        private int ImageIndex = 1;

        // =============================== 全域變數宣告 ===============================

        public FromImageCrop()
        {
            InitializeComponent();
            InitializePictureBox(); //視窗CustomPictureBox事件綁定
            InitializeImageLoad(settingInfo); // 初始化圖片載入
            InitializeDataGridViewStyle(); // 初始化 DataGridView 的外觀
        }


        public void InitializePictureBox()
        {
            //ThreeLeft
            picThreeLeft.ImagePointClicked += pt =>
            {
                PointF imgPt = pt;
            };

            picThreeLeft.CallBackShowMenu += new CustomPictureBox.CallBackReturnShowMenu(OnCallBackShowMenu);
            picThreeLeft.RoiListChanged += (s, e) => RefreshRoiDataGridView(picThreeLeft, threeleft_dgv);
            threeleft_panel.Controls.Add(picThreeLeft);

            //ThreeMiddle
            picThreeMiddle.ImagePointClicked += pt =>
            {
                PointF imgPt = pt;
            };

            picThreeMiddle.CallBackShowMenu += new CustomPictureBox.CallBackReturnShowMenu(OnCallBackShowMenu);
            picThreeMiddle.RoiListChanged += (s, e) => RefreshRoiDataGridView(picThreeMiddle, threemiddle_dgv);
            threemiddle_panel.Controls.Add(picThreeMiddle);

            //ThreeRight
            picThreeRight.ImagePointClicked += pt =>
            {
                PointF imgPt = pt;
            };

            picThreeRight.CallBackShowMenu += new CustomPictureBox.CallBackReturnShowMenu(OnCallBackShowMenu);
            picThreeRight.RoiListChanged += (s, e) => RefreshRoiDataGridView(picThreeRight, threeright_dgv);
            threeright_panel.Controls.Add(picThreeRight);

            //TwoLeft
            picTwoLeft.ImagePointClicked += pt =>
            {
                PointF imgPt = pt;
            };

            picTwoLeft.CallBackShowMenu += new CustomPictureBox.CallBackReturnShowMenu(OnCallBackShowMenu);
            picTwoLeft.RoiListChanged += (s, e) => RefreshRoiDataGridView(picTwoLeft, twoleft_dgv);
            twoleft_panel.Controls.Add(picTwoLeft);

            //TwoRight
            picTwoRight.ImagePointClicked += pt =>
            {
                PointF imgPt = pt;
            };

            picTwoRight.CallBackShowMenu += new CustomPictureBox.CallBackReturnShowMenu(OnCallBackShowMenu);
            picTwoRight.RoiListChanged += (s, e) => RefreshRoiDataGridView(picTwoRight, tworight_dgv);
            tworight_panel.Controls.Add(picTwoRight);
        }

        /// <summary>
        /// 一次性初始化所有 DataGridView 的欄位與樣式
        /// </summary>
        private void InitializeDataGridViewStyle()
        {
            // 5 個 DataGridView，名稱對應
            var dgvList = new List<DataGridView> { threeleft_dgv, threemiddle_dgv, threeright_dgv, twoleft_dgv, tworight_dgv };

            foreach (var dgv in dgvList)
            {
                if (dgv == null) continue;
                dgv.Columns.Clear();
                dgv.RowHeadersVisible = false;
                dgv.AllowUserToAddRows = false;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // 建立 ROI 的 4 個核心幾何欄位
                dgv.Columns.Add("No", "No.");
                dgv.Columns.Add("X", "X");
                dgv.Columns.Add("Y", "Y");
                dgv.Columns.Add("W", "Width");
                dgv.Columns.Add("H", "Height");
            }
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

        private void ImageSwitchBtn_ClickEvent(object sender, EventArgs e)
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
            else
            {
                CallBackLog?.Invoke("WARNING", $"已經是第一張或最後一張影像，無法切換");
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

            if(roiswitch_cb.SelectedIndex >= 0)
            {
                roiswitch_cb_SelectedIndexChanged(roiswitch_cb, EventArgs.Empty);
            }
        }

        /// <summary>
        /// ROI 改變時，動態更新對應的 DataGridView
        /// </summary>
        private void RefreshRoiDataGridView(CustomPictureBox pictureBox, DataGridView dgv)
        {
            if (dgv == null) return;

            if (dgv.InvokeRequired)
            {
                dgv.BeginInvoke(new Action(() => RefreshRoiDataGridView(pictureBox, dgv)));
                return;
            }

            var roiList = pictureBox.RoiList;

            // 狀況 A：如果數量不對（例如新增或刪除 ROI），直接重新建構所有 Rows
            if (dgv.Rows.Count != roiList.Count)
            {
                dgv.Rows.Clear(); // 這裡不會噴錯了
                for (int i = 0; i < roiList.Count; i++)
                {
                    Rectangle rect = roiList[i];
                    // 序號, X, Y, Width, Height
                    dgv.Rows.Add(i + 1, rect.X, rect.Y, rect.Width, rect.Height);
                }
            }
            // 狀況 B：數量一樣（通常是滑鼠在拖曳、縮放 ROI），只更新數值，不重造 Row
            else
            {
                for (int i = 0; i < roiList.Count; i++)
                {
                    Rectangle rect = roiList[i];

                    // 假設你的欄位順序是：Index 0=序號, 1=X, 2=Y, 3=Width, 4=Height
                    // 我們只更新數值變動的 Cells，這樣拉動時極度流暢、完全不卡頓
                    dgv.Rows[i].Cells[1].Value = rect.X;
                    dgv.Rows[i].Cells[2].Value = rect.Y;
                    dgv.Rows[i].Cells[3].Value = rect.Width;
                    dgv.Rows[i].Cells[4].Value = rect.Height;
                }
            }
        }

        private void roiswitch_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.settingInfo == null)
            {
                CallBackLog?.Invoke("WARNING", $"先選擇檔案");
                return;
            }
            ClearAllROI();

            int index = roiswitch_cb.SelectedIndex;

            if (index >= 0 && index < roiswitch_cb.Items.Count)
            {
                try
                {
                    RRI = new ReadROIINI();

                    rOIList = new ROIList();

                    string[] files = Directory.GetFiles(settingInfo.DirImagePath, $"*Middle*", SearchOption.TopDirectoryOnly);

                    if (files.Length > 0)
                    {
                        rOIList = RRI.ReadRecipeINI(settingInfo.RecipePath + "\\ROI.ini", 3);

                        var roiMap = new Dictionary<string, List<Rectangle>>();

                        if (index == 0) //CountSlice
                        {
                            roiMap = rOIList.ThreeCamera.CountSlice.GetMappedRectangles();
                        }
                        else if (index == 1) //Stack
                        {
                            roiMap = rOIList.ThreeCamera.Stack.GetMappedRectangles();
                        }

                        foreach (var rect in roiMap["Left"]) picThreeLeft.AddRoi(rect);
                        foreach (var rect in roiMap["Middle"]) picThreeMiddle.AddRoi(rect);
                        foreach (var rect in roiMap["Right"]) picThreeRight.AddRoi(rect);
                    }
                    else
                    {
                        rOIList = RRI.ReadRecipeINI(settingInfo.RecipePath + "\\ROI.ini", 2);

                        var roiMap = new Dictionary<string, List<Rectangle>>();

                        if (index == 0) //CountSlice
                        {
                            roiMap = rOIList.TwoCamera.CountSlice.GetMappedRectangles();
                        }
                        else if (index == 1) //Stack
                        {
                            roiMap = rOIList.TwoCamera.Stack.GetMappedRectangles();
                        }

                        foreach (var rect in roiMap["Left"]) picTwoLeft.AddRoi(rect);
                        foreach (var rect in roiMap["Right"]) picTwoRight.AddRoi(rect);
                    }

                    CallBackLog?.Invoke("INFO", $"載入Recipe成功");
                }
                catch (Exception ex)
                {
                    CallBackLog?.Invoke("WARNING", $"載入Recipe失敗: {ex.Message}");
                }
            }
        }

        private void ClearAllROI()
        {
            // 直接呼叫 ClearRoi()，一行搞定一台
            picThreeLeft.ClearRoi();
            picThreeMiddle.ClearRoi();
            picThreeRight.ClearRoi();

            picTwoLeft.ClearRoi();
            picTwoRight.ClearRoi();
        }

        private void SaveBtn_ClickEvent(object sender, EventArgs e)
        {
            string SavePath = "";

            var btn = sender as Button;

            if (btn == absence_btn)
            {
                if (!Directory.Exists(settingInfo.SaveResultPath + "\\Absence"))
                {
                    Directory.CreateDirectory(settingInfo.SaveResultPath + "\\Absence"); // 自動建立多層級資料夾
                }
                SavePath = settingInfo.SaveResultPath + "\\Absence\\" + settingInfo.SelectedImageItem + "_Slot" + ImageIndex.ToString("D2") + "_";

                CallBackLog?.Invoke("INFO", $"儲存影像類別: Absence");
            }
            else if (btn == stack_btn)
            {
                if (!Directory.Exists(settingInfo.SaveResultPath + "\\Stack"))
                {
                    Directory.CreateDirectory(settingInfo.SaveResultPath + "\\Stack"); // 自動建立多層級資料夾
                }
                SavePath = settingInfo.SaveResultPath + "\\Stack\\" + settingInfo.SelectedImageItem + "_Slot" + ImageIndex.ToString("D2") + "_";

                CallBackLog?.Invoke("INFO", $"儲存影像類別: Stack");
            }
            else if (btn == presence_btn)
            {
                if (!Directory.Exists(settingInfo.SaveResultPath + "\\Presence"))
                {
                    Directory.CreateDirectory(settingInfo.SaveResultPath + "\\Presence"); // 自動建立多層級資料夾
                }
                SavePath = settingInfo.SaveResultPath + "\\Presence\\" + settingInfo.SelectedImageItem + "_Slot" + ImageIndex.ToString("D2") + "_";

                CallBackLog?.Invoke("INFO", $"儲存影像類別: Presence");
            }
            else if (btn == slant_btn)
            {
                if (!Directory.Exists(settingInfo.SaveResultPath + "\\Slant"))
                {
                    Directory.CreateDirectory(settingInfo.SaveResultPath + "\\Slant"); // 自動建立多層級資料夾
                }
                SavePath = settingInfo.SaveResultPath + "\\Slant\\" + settingInfo.SelectedImageItem + "_Slot" + ImageIndex.ToString("D2") + "_";

                CallBackLog?.Invoke("INFO", $"儲存影像類別: Slant");
            }

            SaveBitmapSafe(picTwoLeft, "Left", SavePath, ImageFormat.Bmp);
            SaveBitmapSafe(picTwoRight, "Right", SavePath, ImageFormat.Bmp);
            SaveBitmapSafe(picThreeLeft, "Left", SavePath, ImageFormat.Bmp);
            SaveBitmapSafe(picThreeMiddle, "Middle", SavePath, ImageFormat.Bmp);
            SaveBitmapSafe(picThreeRight, "Right", SavePath, ImageFormat.Bmp);

            ImageSwitchBtn_ClickEvent(nextimage_btn, EventArgs.Empty);
        }

        private void SaveBitmapSafe(CustomPictureBox pic, string Location, string SavePath, ImageFormat format)
        {
            // 檢查是否有選取的 ROI
            if (pic.ActiveRoiIndex >= 0 && pic.ActiveRoiIndex < pic.RoiList.Count)
            {
                // 檢查圖像是否存在
                if (pic.Image != null)
                {
                    // 💡 為了效能，先把來源影像轉成 Bitmap（在迴圈外宣告一次就好）
                    using (Bitmap srcBitmap = new Bitmap(pic.Image))
                    {
                        for (int i = 0; i < pic.RoiList.Count; i++)
                        {
                            // 💡 修正 1：依據迴圈的 i，依序取得不同的 ROI 框
                            Rectangle roi = pic.RoiList[i];

                            // 防呆：確保矩形寬高合法
                            if (roi.Width <= 0 || roi.Height <= 0) continue;

                            // 💡 修正 2：使用 using 包覆裁剪出來的圖片，確保一做完立刻釋放記憶體控制代碼
                            using (Bitmap croppedImage = srcBitmap.Clone(roi, srcBitmap.PixelFormat))
                            {
                                string fullPath = SavePath + Location + "_" + (i + 1).ToString("D2") + ".bmp";

                                // 💡 修正 3：使用安全儲存方法，繞過 GDI+ 底層的路徑鎖定 Bug
                                if (croppedImage == null) return;

                                // 先確認資料夾是否存在
                                string dir = Path.GetDirectoryName(fullPath);
                                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                                {
                                    Directory.CreateDirectory(dir);
                                }

                                // 利用記憶體串流避開 GDI+ 直接操作檔案產生的泛型錯誤
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    croppedImage.Save(ms, ImageFormat.Bmp);
                                    byte[] imageBytes = ms.ToArray();
                                    File.WriteAllBytes(fullPath, imageBytes); // 徹底由 .NET 核心寫入，絕對不會噴 GDI+ 錯誤
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
