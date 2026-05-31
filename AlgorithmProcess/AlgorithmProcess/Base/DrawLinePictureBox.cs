using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AlgorithmProcess.Base
{
    public class DrawLinePictureBox : PictureBox
    {
        // 🌟 滑鼠模式定義
        public enum MouseMode
        {
            None,
            DrawMeasureLine
        }

        // 🌟 核心屬性與狀態變數
        public MouseMode CurrentMouseMode { get; set; } = MouseMode.None;
        public bool EnablePan { get; set; } = true;

        private float _zoom = 1.0f;
        private PointF _pan = PointF.Empty;
        private float _fitZoom = 1.0f;
        private PointF _fitOffset = PointF.Empty;

        private Bitmap _internalImage;
        private bool _panning = false;
        private Point _panStartPoint;

        // 🌟 卡尺線繪製狀態（記錄的是真實影像上的像素座標，支援縮放平移不移位）
        private bool _isDrawing = false;
        private PointF m_ImgStartPt = PointF.Empty;
        private PointF m_ImgEndPt = PointF.Empty;

        // 🚀 外部可選綁定：當線畫完後，可以通知主視窗去跑 Halcon 演算法
        public event Action<PointF, PointF> MeasureLineDrawn;

        // 🌟 1. 在類別內部上方，新增一個用來存放 Halcon 找出來的點的容器 (真實影像像素座標)
        private System.Collections.Generic.List<PointF> m_HalconResultPoints = new System.Collections.Generic.List<PointF>();

        // 🌟 2. 提供一個 Public 方法讓外部（主視窗）把算好的點傳進來
        public void UpdateHalconResultPoints(System.Collections.Generic.List<PointF> pts)
        {
            m_HalconResultPoints.Clear();
            if (pts != null)
            {
                m_HalconResultPoints.AddRange(pts);
            }
            this.Invalidate(); // 強制重繪，點就會立刻刷在畫面上！
        }

        public DrawLinePictureBox()
        {
            this.SizeMode = PictureBoxSizeMode.Normal;
            this.BackColor = Color.LightGray;

            // 在內部建構子中直接訂閱自己的事件（封裝）
            this.MouseWheel += DrawLinePictureBox_MouseWheel;
            this.Resize += (s, e) => { UpdateFitZoom(); Invalidate(); };

            this.MouseDown += DrawLinePictureBox_MouseDown;
            this.MouseMove += DrawLinePictureBox_MouseMove;
            this.MouseUp += DrawLinePictureBox_MouseUp;
            this.MouseEnter += DrawLinePictureBox_MouseEnter;
            this.MouseLeave += DrawLinePictureBox_MouseLeave;
        }

        // 🌟 複寫（Override）圖片屬性，實作深拷貝與自動適應視窗
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

                if (_internalImage != null)
                {
                    _internalImage.Dispose();
                    _internalImage = null;
                }

                if (value != null)
                {
                    _internalImage = new Bitmap(value);
                }

                base.Image = _internalImage;
                FitWindow();
            }
        }

        public void FitWindow()
        {
            UpdateFitZoom();
            _pan = PointF.Empty;
            _zoom = 1.0f;
            Invalidate();
        }

        private void UpdateFitZoom()
        {
            if (Image == null || this.Width == 0 || this.Height == 0) return;
            float zoomX = (float)this.Width / Image.Width;
            float zoomY = (float)this.Height / Image.Height;
            _fitZoom = Math.Min(zoomX, zoomY);
            float imgW = Image.Width * _fitZoom;
            float imgH = Image.Height * _fitZoom;
            _fitOffset = new PointF((this.Width - imgW) / 2f, (this.Height - imgH) / 2f);
        }

        // 🌟 座標轉換翻譯官
        public PointF ImageToScreen(PointF imagePt)
        {
            if (Image == null) return PointF.Empty;
            float x = _fitOffset.X + _pan.X + imagePt.X * _fitZoom * _zoom;
            float y = _fitOffset.Y + _pan.Y + imagePt.Y * _fitZoom * _zoom;
            return new PointF(x, y);
        }

        public PointF ScreenToImage(Point screenPt)
        {
            if (Image == null) return PointF.Empty;
            float x = (screenPt.X - _fitOffset.X - _pan.X) / (_fitZoom * _zoom);
            float y = (screenPt.Y - _fitOffset.Y - _pan.Y) / (_fitZoom * _zoom);
            return new PointF(x, y);
        }

        // 🌟 滾輪縮放邏輯
        private void DrawLinePictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Image == null) return;
            var imgPtBefore = ScreenToImage(e.Location);
            if (e.Delta > 0) _zoom *= 1.3f;
            else _zoom /= 1.3f;
            _zoom = Math.Max(0.1f, Math.Min(100f, _zoom));

            var newScreenPt = ImageToScreen(imgPtBefore);
            _pan.X += e.Location.X - newScreenPt.X;
            _pan.Y += e.Location.Y - newScreenPt.Y;
            Invalidate();
        }

        // 🌟 MouseDown：決定是「畫線」還是「拖曳平移圖片」
        private void DrawLinePictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (Image == null || e.Button != MouseButtons.Left) return;

            // 模式 A：正在拉卡尺量測線
            if (CurrentMouseMode == MouseMode.DrawMeasureLine)
            {
                _isDrawing = true;
                m_ImgStartPt = ScreenToImage(e.Location);
                m_ImgEndPt = m_ImgStartPt;
                Invalidate();
            }
            // 模式 B：一般狀態，允許按住滑鼠拖曳底圖
            else if (EnablePan)
            {
                _panning = true;
                _panStartPoint = e.Location;
                Cursor = Cursors.Hand;
            }
        }

        // 🌟 MouseMove：滑鼠移動時實時計算與繪製
        private void DrawLinePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (Image == null) return;

            if (_isDrawing)
            {
                m_ImgEndPt = ScreenToImage(e.Location);
                Invalidate(); // 高頻率重繪線條
            }
            else if (_panning)
            {
                int dx = e.X - _panStartPoint.X;
                int dy = e.Y - _panStartPoint.Y;
                _pan.X += dx;
                _pan.Y += dy;
                _panStartPoint = e.Location;
                Invalidate();
            }
        }

        // 🌟 MouseUp：放開滑鼠，完成線段繪製
        private void DrawLinePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isDrawing)
            {
                _isDrawing = false;
                m_ImgEndPt = ScreenToImage(e.Location);

                // 畫完後可以自動還原成 None 模式，並恢復平移
                CurrentMouseMode = MouseMode.None;

                Invalidate();

                // 🚀 通知外部（如主視窗）：這張圖的卡尺線畫好了，並傳出影像像素起終點
                MeasureLineDrawn?.Invoke(m_ImgStartPt, m_ImgEndPt);
            }

            if (_panning)
            {
                _panning = false;
                Cursor = (CurrentMouseMode == MouseMode.DrawMeasureLine) ? Cursors.Cross : Cursors.Default;
            }
        }

        // 🌟 MouseEnter：滑鼠進來時，如果是畫線模式就換成十字準星
        private void DrawLinePictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (CurrentMouseMode == MouseMode.DrawMeasureLine)
            {
                Cursor = Cursors.Cross;
            }
        }

        // 🌟 MouseLeave：滑鼠離開圖片時還原成預設游標
        private void DrawLinePictureBox_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        // 🌟 OnPaint：繪圖核心邏輯
        protected override void OnPaint(PaintEventArgs pe)
        {
            // 呼叫底層原生的繪圖（若有必要）
            base.OnPaint(pe);

            if (_internalImage == null) return;

            // 1. 繪製經過平移與縮放的底圖
            pe.Graphics.Clear(this.BackColor);

            // 暫存目前的變形矩陣狀態
            GraphicsState gState = pe.Graphics.Save();

            pe.Graphics.TranslateTransform(_fitOffset.X + _pan.X, _fitOffset.Y + _pan.Y);
            pe.Graphics.ScaleTransform(_fitZoom * _zoom, _fitZoom * _zoom);
            pe.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            pe.Graphics.DrawImage(_internalImage, 0, 0, _internalImage.Width, _internalImage.Height);

            // 還原矩陣，確保後續畫在 UI 上的疊加圖層座標正確
            pe.Graphics.Restore(gState);

            // 2. 繪製 Cyan 色卡尺量測線
            if (m_ImgStartPt != PointF.Empty && m_ImgEndPt != PointF.Empty)
            {
                pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // 即時換算為當前螢幕畫面上的點（完美跟隨縮放與拖曳）
                PointF screenStart = ImageToScreen(m_ImgStartPt);
                PointF screenEnd = ImageToScreen(m_ImgEndPt);

                using (Pen measurePen = new Pen(Color.Cyan, 3.0f))
                {
                    measurePen.DashStyle = DashStyle.Solid;
                    measurePen.EndCap = LineCap.ArrowAnchor; // 線條末端帶箭頭

                    // 畫量測主線
                    pe.Graphics.DrawLine(measurePen, screenStart, screenEnd);
                }

                // 畫起點綠色圓點（標示方向性）
                pe.Graphics.FillEllipse(Brushes.Lime, screenStart.X - 5, screenStart.Y - 5, 10, 10);
            }

            // 🌟 核心：繪製 Halcon 找出來的邊緣點 (用紅色或亮綠色 X 十字標記呈現)
            if (m_HalconResultPoints.Count > 0)
            {
                pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (Pen resultPen = new Pen(Color.Red, 2.0f)) // 邊緣點用紅色
                {
                    foreach (var imgPt in m_HalconResultPoints)
                    {
                        // 將真實影像的像素座標，轉換為當前畫面的螢幕座標（隨縮放平移連動）
                        PointF scrPt = ImageToScreen(imgPt);

                        // 畫一個大小為 12 像素的十字標記 (X)
                        float size = 6;
                        pe.Graphics.DrawLine(resultPen, scrPt.X - size, scrPt.Y - size, scrPt.X + size, scrPt.Y + size);
                        pe.Graphics.DrawLine(resultPen, scrPt.X - size, scrPt.Y + size, scrPt.X + size, scrPt.Y - size);

                        // 可選：在點旁邊畫個小圓圈更顯眼
                        pe.Graphics.DrawEllipse(resultPen, scrPt.X - 4, scrPt.Y - 4, 8, 8);
                    }
                }
            }

        }



        // 🚀 提供外部清除畫布線條的 Public 方法
        // 🌟 3. 修改之前的 ClearMeasureLine，讓清除時也順便清空點
        public void ClearMeasureLine()
        {
            m_ImgStartPt = PointF.Empty;
            m_ImgEndPt = PointF.Empty;
            m_HalconResultPoints.Clear(); // 清空點
            Invalidate();
        }
    }
}