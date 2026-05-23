using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorithmProcess.Window
{    

    public partial class FromParameterSetting_StepDisplay : UserControl
    {

        // =============================== 視窗CustomPictureBox建立 ===============================

        CustomPictureBox picImageDisplay = new CustomPictureBox
        {
            Dock = DockStyle.Fill,
        };

        // =============================== 視窗CustomPictureBox建立 ===============================

        // =============================== 事件與委託 ===============================

        public delegate void CallBackReturnLog(string type, string msg);
        public event CallBackReturnLog CallBackLog;

        public delegate void CallBackReturnPointInfo(int x, int y, int gray);
        public event CallBackReturnPointInfo CallBackPointInfo;

        // =============================== 事件與委託 ===============================

        public FromParameterSetting_StepDisplay()
        {
            InitializeComponent();
            InitializePictureBox(); //視窗CustomPictureBox事件綁定
        }

        public void InitializePictureBox()
        {
            //演算法圖片顯示
            picImageDisplay.ImagePointClicked += pt =>
            {
                PointF imgPt = pt;
            };

            picImageDisplay.CallBackShowMenu += new CustomPictureBox.CallBackReturnShowMenu(OnCallBackShowMenu);
            // 在初始化時掛載事件
            picImageDisplay.MouseMove += PicImageDisplay_MouseMove;
            Image_Panel.Controls.Add(picImageDisplay);
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

        private void PicImageDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox picBox = sender as PictureBox;
            if (picBox == null || picBox.Image == null) return;

            try
            {
                // =================================================================
                // 1. 【核心數學公式】將控制項上的滑鼠座標 (e.X, e.Y) 反推回影像真實像素 (PixelX, PixelY)
                // =================================================================
                int imgWidth = picBox.Image.Width;
                int imgHeight = picBox.Image.Height;

                // 計算 PictureBox 與 實際影像 的縮放比例 (以 Zoom 模式為例)
                double ratioX = (double)picBox.ClientSize.Width / imgWidth;
                double ratioY = (double)picBox.ClientSize.Height / imgHeight;
                double ratio = Math.Min(ratioX, ratioY); // 取小值即為 Zoom 模式下的實際縮放比

                // 計算影像在 PictureBox 內部居中顯示時的左上角偏移量 (Offset)
                int offsetX = (int)((picBox.ClientSize.Width - (imgWidth * ratio)) / 2);
                int offsetY = (int)((picBox.ClientSize.Height - (imgHeight * ratio)) / 2);

                // 反推得到真實影像中的像素座標
                int pixelX = (int)((e.X - offsetX) / ratio);
                int pixelY = (int)((e.Y - offsetY) / ratio);

                // =================================================================
                // 2. 邊界防禦：確保滑鼠沒有滑出影像邊界之外
                // =================================================================
                if (pixelX >= 0 && pixelX < imgWidth && pixelY >= 0 && pixelY < imgHeight)
                {
                    // 3. 讀取該點的顏色 / 灰階值
                    Bitmap bmp = picBox.Image as Bitmap;
                    if (bmp != null)
                    {
                        // 💡 讀取 R, G, B 數值
                        Color pixelColor = bmp.GetPixel(pixelX, pixelY);

                        // 📐 標準標準工業灰階轉換公式：Gray = R * 0.299 + G * 0.587 + B * 0.114
                        int grayValue = (int)(pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114);

                        // 4. 即時更新到狀態列 (StatusStrip) 或指定的 Label 畫面上
                        // 顯示格式：X: 1024, Y: 768 | Gray: 185
                        CallBackPointInfo?.Invoke(pixelX, pixelY, grayValue); // 透過委託回傳座標與灰階值給 MainForm 顯示

                        // 如果是彩色相機，也可以一併噴出 RGB 資訊供調機參考
                        // lbl_StatusCoordinate.Text += $" (R: {pixelColor.R}, G: {pixelColor.G}, B: {pixelColor.B})";
                    }
                }
                else
                {
                    // 滑鼠移到影像外的邊緣空白處，顯示預設值
                    CallBackPointInfo?.Invoke(-1, -1, -1);
                }
            }
            catch (Exception ex)
            {
                // 防止滑鼠快速移動時，底層資源釋放衝突引發 Exception
                System.Diagnostics.Debug.WriteLine($"座標取值失敗: {ex.Message}");
            }
        }

        public void ShowResult(DebugResult result)
        {
            // =================================================================
            // 1. 圖片顯示與非託管資源釋放
            // =================================================================
            if (picImageDisplay.Image != null)
            {
                picImageDisplay.Image.Dispose();
                picImageDisplay.Image = null;
            }

            if (result != null && result.Image != null)
            {
                // 複製一份 Bitmap 給 UI 顯示，避免與演算法核心執行緒衝突
                picImageDisplay.Image = (Bitmap)result.Image.Clone();
                picImageDisplay.FitWindow();
            }

            // =================================================================
            // 2. 顯示參數
            // =================================================================

            // 先清空舊的參數控制項，防止重複執行時畫面重疊殘留
            flowLayoutPanel_Parament.Controls.Clear();

            // 開始根據參數數量動態生成
            for (int i = 0; i < result.Parameters.Count; i++)
            {
                var param = result.Parameters[i]; // 取得當前參數資料 (例如包含 Name, Value, Min, Max 等)

                // ✨ 核心關鍵：每次迴圈都要 new 一個獨立的 UI 實例！
                FromParameterSetting_StepSetting_Parameter parameterUI = new FromParameterSetting_StepSetting_Parameter();

                // 3. 呼叫你之前修改好安全名稱的 lbl_ParamName 進行給值 (假設你在該控制項內有寫 Init 方法)
                // 這裡示範直接或透過方法將資料填入控制項
                parameterUI.InitParameter(param.Name, param.Value);

                // 📐 1. 強制設定每一列橫條的精準高度（根據你的字體大小，38~42 通常最完美）
                parameterUI.Height = 10;
                // 這樣可以保證控制項完美填滿，且絕對不會長出水平滾動條！
                parameterUI.Width = flowLayoutPanel_Parament.ClientSize.Width - 25;

                // ✨ 4. 【正確寫法】直接使用 Controls.Add 將實例塞進 FlowLayoutPanel
                flowLayoutPanel_Parament.Controls.Add(parameterUI);
            }

            // =================================================================
            // 3. 顯示單一變數 (dataGridView_SingleVariable)
            // =================================================================
            try
            {
                // ✨ 先斬斷舊的資料繫結，防止 Grid 快取踩空
                dataGridView_SingleVariable.DataSource = null;

                // ✨ 禁用自動生成欄位！只使用你在 UI 畫好的 Name, Description, Value 欄位
                dataGridView_SingleVariable.AutoGenerateColumns = false;

                if (result != null && result.Variables != null && result.Variables.Count > 0)
                {
                    // 將 List 轉譯為匿名物件清單，屬性名稱（Name, Value）必須完全對應你 .Designer.cs 裡的 DataPropertyName
                    var variableBindingList = result.Variables.Select(v => new
                    {
                        Name = v.Name, // 結合名稱與描述
                        Description = v.Description,                 // 描述
                        Value = v.Value                       // 數值 (int, double, string...)
                    }).ToList();

                    dataGridView_SingleVariable.DataSource = variableBindingList;
                }
            }
            catch (Exception ex)
            {   
                CallBackLog?.Invoke("Error", $"dataGridView_SingleVariable 顯示失敗: {ex.Message}");               
            }

            // =================================================================
            // 4. 顯示陣列數據 (dataGridView_Array)
            // =================================================================
            try
            {
                // ✨ 同步斬斷舊的資料繫結
                dataGridView_Array.DataSource = null;

                if (result != null && result.Arrays != null && result.Arrays.Count > 0)
                {
                    // 💡 由於陣列可能有多組（例如 Near_Rows 與 Near_Cols）
                    // 這裡預設將「第一組陣列」展開垂直顯示。
                    var primaryArray = result.Arrays[0];

                    if (primaryArray.Values != null && primaryArray.Values.Count > 0)
                    {
                        var arrayBindingList = primaryArray.Values.Select((val, idx) => new
                        {
                            Name = $"{primaryArray.Name}[{idx}]", // 顯示如: Near_Rows[0]
                            Value = val                           // 陣列內部的數值
                        }).ToList();

                        dataGridView_Array.DataSource = arrayBindingList;
                    }
                }
            }
            catch (Exception ex)
            {   
                CallBackLog?.Invoke("Error", $"dataGridView_Array 顯示失敗: {ex.Message}");                
            }
        }

        /// <summary>
        /// 👑 【接力棒第二棒】收集並回傳目前面板上所有的動態參數
        /// </summary>
        public Dictionary<string, double> GetCurrentParameters()
        {
            var paramDict = new Dictionary<string, double>();

            // 遍歷自己肚子裡的 FlowLayoutPanel
            foreach (Control ctrl in this.flowLayoutPanel_Parament.Controls)
            {
                // 精準篩選出孫控制項
                if (ctrl is FromParameterSetting_StepSetting_Parameter paramUI)
                {
                    string name = paramUI.ParameterName;
                    double value = paramUI.CurrentValue;

                    // 防禦：防止有重複的關鍵字導致 Dictionary 崩潰
                    if (!paramDict.ContainsKey(name))
                    {
                        paramDict.Add(name, value);
                    }
                }
            }

            return paramDict; // 打包好，準備回傳給主畫面
        }
    }
}
