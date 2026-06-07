using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AlgorithmProcess.Base
{
    public class AdvancedPictureBox : PictureBox
    {
        // 🌟 1. 定義滑鼠互動模式
        public enum InteractionMode
        {
            None,             // 一般模式：允許滾輪縮放、按住滑鼠拖曳（Pan）
            DrawRoi,          // 劃分 ROI 模式
            DrawMeasureLine   // 劃分卡尺量測線模式
        }

        // 🌟 2. 外部觸發的狀態開關
        public InteractionMode CurrentMode { get; set; } = InteractionMode.None;

        // 🌟 3. 核心座標與縮放變數
        private float _zoom = 1.0f;
        private PointF _pan = PointF.Empty;
        private float _fitZoom = 1.0f;
        private PointF _fitOffset = PointF.Empty;
        private Bitmap _internalImage;

        // 🌟 4. 影像平移（Pan）狀態
        private bool _panning = false;
        private Point _panStartPoint;

        // 🌟 5. ROI 互動數據與狀態 (記錄於影像像素座標系)
        public List<Rectangle> RoiList { get; } = new List<Rectangle>();
        public int ActiveRoiIndex { get; set; } = -1;
        private bool _resizingRoi = false;
        private int _resizeHandle = -1;
        private Point _resizeStartScreen;
        private Rectangle _resizeStartRoiScreen;
        private const int HandleSize = 8;

        // 🌟 6. 卡尺劃線狀態 (記錄於影像像素座標系)
        private bool _isDrawingLine = false;
        public PointF m_ImgStartPt = PointF.Empty;
        public PointF m_ImgEndPt = PointF.Empty;

        // 🌟 7. 新增：Halcon 算出來的演算法結果點，供畫面上釘十字使用
        private List<PointF> m_ResultPoints = new List<PointF>();

        // 📢 8. 傳出事件（使用 Action 泛型委託，解耦架構）
        public event Action<Rectangle> RoiDrawn;                // ROI 畫完或調整完時傳出 (傳出真實像素矩陣)
        public event Action<PointF, PointF> MeasureLineDrawn;   // 卡尺線畫完時傳出 (傳出起點、終點像素座標)

        // 🌟 讓外部 Form 可以判斷：目前這張圖到底有沒有畫卡尺線
        public bool HasMeasureLine => m_ImgStartPt != PointF.Empty && m_ImgEndPt != PointF.Empty;

        // 🌟 讓外部 Form 可以直接抓取目前畫布上的卡尺線「影像像素起點」
        public PointF StartPoint => m_ImgStartPt;

        // 🌟 讓外部 Form 可以直接抓取目前畫布上的卡尺線「影像像素終點」
        public PointF EndPoint => m_ImgEndPt;

        public AdvancedPictureBox()
        {
            this.SizeMode = PictureBoxSizeMode.Normal;
            this.BackColor = Color.LightGray;

            // 註冊滑鼠事件
            this.MouseWheel += AdvancedPictureBox_MouseWheel;
            this.Resize += (s, e) => { UpdateFitZoom(); Invalidate(); };

            this.MouseDown += AdvancedPictureBox_MouseDown;
            this.MouseMove += AdvancedPictureBox_MouseMove;
            this.MouseUp += AdvancedPictureBox_MouseUp;
        }

        // 複寫 Image 屬性，自動打理記憶體深拷貝與自動置中
        public new Image Image
        {
            get => _internalImage;
            set
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() => Image = value));
                    return;
                }
                if (_internalImage != null) _internalImage.Dispose();
                _internalImage = (value != null) ? new Bitmap(value) : null;
                base.Image = _internalImage;

                FitWindow(); // 設圖時自動置中、鋪滿
            }
        }

        #region [ Public 外部控制方法：置中、清空、塞入結果點 ]

        /// <summary>
        /// 外部按鈕可呼叫：影像還原置中 (Fit Window)
        /// </summary>
        public void FitWindow()
        {
            UpdateFitZoom();
            _pan = PointF.Empty;
            _zoom = 1.0f;
            Invalidate();
        }

        /// <summary>
        /// 外部可呼叫：清除畫布上的卡尺、ROI 與演算法殘留點
        /// </summary>
        public void ClearAllGraphics()
        {
            RoiList.Clear();
            ActiveRoiIndex = -1;
            m_ImgStartPt = PointF.Empty;
            m_ImgEndPt = PointF.Empty;
            m_ResultPoints.Clear();
            Invalidate();
        }

        /// <summary>
        /// 外部算完 Halcon 後，可將點位送回來畫在畫面上
        /// </summary>
        public void UpdateResultPoints(List<PointF> pts)
        {
            m_ResultPoints.Clear();
            if (pts != null) m_ResultPoints.AddRange(pts);
            this.Invalidate();
        }

        #endregion

        #region [ 座標換算與矩陣變形 ]

        private void UpdateFitZoom()
        {
            if (Image == null || this.Width == 0 || this.Height == 0) return;
            float zoomX = (float)this.Width / Image.Width;
            float zoomY = (float)this.Height / Image.Height;
            _fitZoom = Math.Min(zoomX, zoomY);
            _fitOffset = new PointF((this.Width - Image.Width * _fitZoom) / 2f, (this.Height - Image.Height * _fitZoom) / 2f);
        }

        public PointF ImageToScreen(PointF imagePt)
        {
            if (Image == null) return PointF.Empty;
            return new PointF(
                _fitOffset.X + _pan.X + imagePt.X * _fitZoom * _zoom,
                _fitOffset.Y + _pan.Y + imagePt.Y * _fitZoom * _zoom
            );
        }

        public PointF ScreenToImage(Point screenPt)
        {
            if (Image == null) return PointF.Empty;
            return new PointF(
                (screenPt.X - _fitOffset.X - _pan.X) / (_fitZoom * _zoom),
                (screenPt.Y - _fitOffset.Y - _pan.Y) / (_fitZoom * _zoom)
            );
        }

        private Rectangle ImageRectToScreenRect(Rectangle imgRect)
        {
            var pt1 = ImageToScreen(new PointF(imgRect.Left, imgRect.Top));
            var pt2 = ImageToScreen(new PointF(imgRect.Right, imgRect.Bottom));
            return new Rectangle((int)Math.Round(pt1.X), (int)Math.Round(pt1.Y), (int)Math.Round(pt2.X - pt1.X), (int)Math.Round(pt2.Y - pt1.Y));
        }

        private Rectangle NormalizeRect(Rectangle rect)
        {
            int x = rect.X, y = rect.Y, w = rect.Width, h = rect.Height;
            if (w < 0) { x += w; w = -w; }
            if (h < 0) { y += h; h = -h; }
            return new Rectangle(x, y, w, h);
        }

        #endregion

        #region [ 滑鼠互動邏輯處理 ]

        private void AdvancedPictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Image == null) return;
            var imgPtBefore = ScreenToImage(e.Location);
            _zoom = (e.Delta > 0) ? _zoom * 1.25f : _zoom / 1.25f;
            _zoom = Math.Max(0.1f, Math.Min(200f, _zoom));

            var newScreenPt = ImageToScreen(imgPtBefore);
            _pan.X += e.Location.X - newScreenPt.X;
            _pan.Y += e.Location.Y - newScreenPt.Y;
            Invalidate();
        }

        private void AdvancedPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (Image == null || e.Button != MouseButtons.Left) return;

            // ── 模式 A：畫卡尺線 ──
            if (CurrentMode == InteractionMode.DrawMeasureLine)
            {
                _isDrawingLine = true;
                m_ImgStartPt = ScreenToImage(e.Location);
                m_ImgEndPt = m_ImgStartPt;
                return;
            }

            // ── 模式 B：畫新 ROI 或 拖曳現有 ROI ──
            if (CurrentMode == InteractionMode.DrawRoi)
            {
                // 優先檢查是否點到現有選定 ROI 的控制點 (Resize Handles)
                if (ActiveRoiIndex >= 0 && ActiveRoiIndex < RoiList.Count)
                {
                    var roiRectScreen = NormalizeRect(ImageRectToScreenRect(RoiList[ActiveRoiIndex]));
                    int handle = HitTestRoiHandle(e.Location, roiRectScreen);
                    if (handle >= 0)
                    {
                        _resizingRoi = true;
                        _resizeHandle = handle;
                        _resizeStartScreen = e.Location;
                        _resizeStartRoiScreen = roiRectScreen;
                        return;
                    }
                }

                // 若沒點到控制點，檢查是否點進任何 ROI 的框內
                for (int i = RoiList.Count - 1; i >= 0; i--)
                {
                    var roiRectScreen = NormalizeRect(ImageRectToScreenRect(RoiList[i]));
                    if (roiRectScreen.Contains(e.Location))
                    {
                        ActiveRoiIndex = i;
                        _resizingRoi = true;
                        _resizeHandle = 8; // 8 代表拖曳整個框移動
                        _resizeStartScreen = e.Location;
                        _resizeStartRoiScreen = roiRectScreen;
                        Invalidate();
                        return;
                    }
                }

                // 若點擊在空白處，代表使用者要「拉出一個全新 ROI」
                RoiList.Clear(); // 工業慣例通常單一區域只留一個主要 ROI，若需多個可將此行拿掉
                Rectangle newRoi = new Rectangle((int)ScreenToImage(e.Location).X, (int)ScreenToImage(e.Location).Y, 1, 1);
                RoiList.Add(newRoi);
                ActiveRoiIndex = RoiList.Count - 1;

                _resizingRoi = true;
                _resizeHandle = 4; // 模擬拉動右下角邊緣
                _resizeStartScreen = e.Location;
                _resizeStartRoiScreen = NormalizeRect(ImageRectToScreenRect(RoiList[ActiveRoiIndex]));
                Invalidate();
                return;
            }

            // ── 模式 C：None 一般模式下，允許滑鼠平移底圖 (Pan) ──
            if (CurrentMode == InteractionMode.None)
            {
                _panning = true;
                _panStartPoint = e.Location;
                Cursor = Cursors.Hand;
            }
        }

        private void AdvancedPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (Image == null) return;

            // 1. 卡尺劃線同步更新
            if (_isDrawingLine)
            {
                m_ImgEndPt = ScreenToImage(e.Location);
                Invalidate();
                return;
            }

            // 2. ROI 的大小調整與平移更新
            if (_resizingRoi && ActiveRoiIndex >= 0)
            {
                int dx = e.X - _resizeStartScreen.X;
                int dy = e.Y - _resizeStartScreen.Y;
                Rectangle newRect = _resizeStartRoiScreen;

                if (_resizeHandle == 8) newRect.Offset(dx, dy); // 移動整幅框
                else
                {
                    switch (_resizeHandle)
                    {
                        case 0: newRect.X += dx; newRect.Y += dy; newRect.Width -= dx; newRect.Height -= dy; break;
                        case 1: newRect.Y += dy; newRect.Height -= dy; break;
                        case 2: newRect.Width += dx; newRect.Y += dy; newRect.Height -= dy; break;
                        case 3: newRect.Width += dx; break;
                        case 4: newRect.Width += dx; newRect.Height += dy; break;
                        case 5: newRect.Height += dy; break;
                        case 6: newRect.X += dx; newRect.Width -= dx; newRect.Height += dy; break;
                        case 7: newRect.X += dx; newRect.Width -= dx; break;
                    }
                }
                if (newRect.Width < 2) newRect.Width = 2;
                if (newRect.Height < 2) newRect.Height = 2;

                var imgPt1 = ScreenToImage(newRect.Location);
                var imgPt2 = ScreenToImage(new Point(newRect.Right, newRect.Bottom));
                var imgRect = new Rectangle((int)Math.Round(imgPt1.X), (int)Math.Round(imgPt1.Y), (int)Math.Round(imgPt2.X - imgPt1.X), (int)Math.Round(imgPt2.Y - imgPt1.Y));

                imgRect.Intersect(new Rectangle(0, 0, Image.Width, Image.Height)); // 限制邊界不超出影像
                if (imgRect.Width > 0 && imgRect.Height > 0)
                {
                    RoiList[ActiveRoiIndex] = imgRect;
                }
                Invalidate();
                return;
            }

            // 3. 底圖拖曳平移更新
            if (_panning)
            {
                _pan.X += e.X - _panStartPoint.X;
                _pan.Y += e.Y - _panStartPoint.Y;
                _panStartPoint = e.Location;
                Invalidate();
                return;
            }

            // 4. 純移動滑鼠時變更 Cursor 做視覺提示
            if (CurrentMode == InteractionMode.DrawRoi && ActiveRoiIndex >= 0)
            {
                var roiRectScreen = NormalizeRect(ImageRectToScreenRect(RoiList[ActiveRoiIndex]));
                int handle = HitTestRoiHandle(e.Location, roiRectScreen);
                Cursor = (handle >= 0) ? ((handle == 8) ? Cursors.SizeAll : Cursors.Cross) : Cursors.Default;
            }
            else if (CurrentMode == InteractionMode.DrawMeasureLine) Cursor = Cursors.Cross;
            else Cursor = Cursors.Default;
        }

        private void AdvancedPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isDrawingLine)
            {
                _isDrawingLine = false;
                m_ImgEndPt = ScreenToImage(e.Location);

                // 在 ScreenToImage 轉換後，或是 Invoke 前加上限制：
                float clampedX = Math.Max(0, Math.Min(Image.Width - 1, m_ImgEndPt.X));
                float clampedY = Math.Max(0, Math.Min(Image.Height - 1, m_ImgEndPt.Y));
                m_ImgEndPt = new PointF(clampedX, clampedY);

                CurrentMode = InteractionMode.None; // 畫完自動切回一般模式
                Invalidate();

                // 🚀 拋出卡尺座標事件
                MeasureLineDrawn?.Invoke(m_ImgStartPt, m_ImgEndPt);
            }

            if (_resizingRoi)
            {
                _resizingRoi = false;
                _resizeHandle = -1;
                CurrentMode = InteractionMode.None; // 畫完/調完自動切回一般模式
                Invalidate();

                // 🚀 拋出 ROI 座標事件
                if (ActiveRoiIndex >= 0) RoiDrawn?.Invoke(RoiList[ActiveRoiIndex]);
            }

            if (_panning)
            {
                _panning = false;
                Cursor = Cursors.Default;
            }
        }

        private int HitTestRoiHandle(Point mouse, Rectangle roiRect)
        {
            var handles = GetRoiHandles(roiRect);
            for (int i = 0; i < handles.Length; i++)
            {
                var rect = new Rectangle(handles[i].X - HandleSize / 2, handles[i].Y - HandleSize / 2, HandleSize, HandleSize);
                if (rect.Contains(mouse)) return i;
            }
            return -1;
        }

        private Point[] GetRoiHandles(Rectangle roiRect)
        {
            int cx = (roiRect.Left + roiRect.Right) / 2;
            int cy = (roiRect.Top + roiRect.Bottom) / 2;
            return new[] {
                new Point(roiRect.Left, roiRect.Top),     // 0: 左上
                new Point(cx, roiRect.Top),              // 1: 中上
                new Point(roiRect.Right, roiRect.Top),    // 2: 右上
                new Point(roiRect.Right, cy),             // 3: 右中
                new Point(roiRect.Right, roiRect.Bottom), // 4: 右下
                new Point(cx, roiRect.Bottom),           // 5: 中下
                new Point(roiRect.Left, roiRect.Bottom),  // 6: 左下
                new Point(roiRect.Left, cy)              // 7: 左中
            };
        }

        #endregion

        #region [ OnPaint 畫布渲染繪製 ]

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (_internalImage == null) return;

            // 1. 畫縮放與平移後的影像底圖 (確保乾淨畫布)
            pe.Graphics.Clear(this.BackColor);

            // 🌟 核心修正：設定低階繪圖，防止因影像與螢幕 DPI 不一致導致 DrawImage 自動形變
            pe.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            GraphicsState gState = pe.Graphics.Save();
            pe.Graphics.TranslateTransform(_fitOffset.X + _pan.X, _fitOffset.Y + _pan.Y);
            pe.Graphics.ScaleTransform(_fitZoom * _zoom, _fitZoom * _zoom);
            pe.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // 🌟 [修正關鍵]：強制指定 1:1 像素對齊，不允許 .NET 框架私自進行 DPI 轉換
            Rectangle srcRect = new Rectangle(0, 0, _internalImage.Width, _internalImage.Height);
            pe.Graphics.DrawImage(_internalImage, srcRect, srcRect, GraphicsUnit.Pixel);

            pe.Graphics.Restore(gState);

            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // 2. 繪製 ROI 虛線框與九宮控制點
            for (int i = 0; i < RoiList.Count; i++)
            {
                var roiRect = NormalizeRect(ImageRectToScreenRect(RoiList[i]));
                using (var pen = new Pen(Color.Orange, 2f) { DashStyle = DashStyle.Dash })
                {
                    pe.Graphics.DrawRectangle(pen, roiRect);
                }

                // 畫出拖曳小點點
                var handles = GetRoiHandles(roiRect);
                foreach (var pt in handles)
                {
                    pe.Graphics.FillEllipse(Brushes.White, pt.X - HandleSize / 2, pt.Y - HandleSize / 2, HandleSize, HandleSize);
                    pe.Graphics.DrawEllipse(Pens.Orange, pt.X - HandleSize / 2, pt.Y - HandleSize / 2, HandleSize, HandleSize);
                }
            }

            // 3. 繪製卡尺量測線 (Cyan色箭頭)
            if (m_ImgStartPt != PointF.Empty && m_ImgEndPt != PointF.Empty)
            {
                PointF scrStart = ImageToScreen(m_ImgStartPt);
                PointF scrEnd = ImageToScreen(m_ImgEndPt);
                using (Pen p = new Pen(Color.Cyan, 3.0f) { EndCap = LineCap.ArrowAnchor })
                {
                    pe.Graphics.DrawLine(p, scrStart, scrEnd);
                }
                pe.Graphics.FillEllipse(Brushes.Lime, scrStart.X - 5, scrStart.Y - 5, 10, 10); // 起點綠點
            }

            // 4. 繪製外部送進來的 Halcon 結果邊緣點 (紅色十字)
            if (m_ResultPoints.Count > 0)
            {
                using (Pen resPen = new Pen(Color.Red, 2.0f))
                {
                    foreach (var pt in m_ResultPoints)
                    {
                        // 💡 注意：Halcon 回傳點若是 (Row, Col) 傳入此處，請確保傳入 pts 前已對齊 X=Col, Y=Row
                        PointF scrPt = ImageToScreen(pt);
                        float r = 6; // 稍微加大一點點，看起來更明確

                        // 繪製正十字 (工業常用) 與同心圓
                        pe.Graphics.DrawLine(resPen, scrPt.X - r, scrPt.Y, scrPt.X + r, scrPt.Y); // 水平線
                        pe.Graphics.DrawLine(resPen, scrPt.X, scrPt.Y - r, scrPt.X, scrPt.Y + r); // 垂直線
                        pe.Graphics.DrawEllipse(resPen, scrPt.X - 3, scrPt.Y - 3, 6, 6);
                    }
                }
            }
        }

        #endregion

    }
}