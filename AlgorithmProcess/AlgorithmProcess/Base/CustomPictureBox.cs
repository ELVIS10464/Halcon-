using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorithmProcess
{
    public class CustomPictureBox : PictureBox
    {
        public delegate void CallBackReturnShowMenu(CustomPictureBox pic);
        public event CallBackReturnShowMenu CallBackShowMenu;

        // 📢 新增：當 ROI 列表數據有任何變動（新增、刪除、滑鼠拖曳調整）時觸發此事件
        public event EventHandler RoiListChanged;

        private float _zoom = 1.0f;
        private PointF _pan = PointF.Empty;
        private float _fitZoom = 1.0f;
        private PointF _fitOffset = PointF.Empty;

        public List<Rectangle> RoiList { get; } = new List<Rectangle>();
        public int ActiveRoiIndex { get; set; } = -1;
        public bool AutoFitOnImageSet { get; set; } = true;

        // ROI 互動
        private bool _resizingRoi = false;
        private int _resizeHandle = -1;
        private Point _resizeStartScreen;
        private Rectangle _resizeStartRoiScreen;
        private const int HandleSize = 10;

        // 👉 圖像拖曳
        private bool _panning = false;
        private Point _panStartPoint;

        public event Action<Point> ImagePointClicked;

        private Bitmap _internalImage;

        // 📢 新增：控制目前是否允許圖片平移/移動與 ROI 互動
        public bool EnablePanAndRoi { get; set; } = true;

        public Point _ptMouse = Point.Empty;

        public PointF _HandFirstClickPt = Point.Empty, _HandEndClickPt = Point.Empty;

        public bool _IsCalcScale = false;

        public int Dist = 0;

        public ScaleArrow scaleArrow = ScaleArrow.Horizontal;


        public CustomPictureBox()
        {
            this.SizeMode = PictureBoxSizeMode.Normal;
            this.BackColor = Color.LightGray;
            this.MouseWheel += CustomPictureBox_MouseWheel;
            this.Resize += (s, e) => { UpdateFitZoom(); Invalidate(); };
            this.MouseDown += CustomPictureBox_MouseDown;
            this.MouseMove += CustomPictureBox_MouseMove;
            this.MouseUp += CustomPictureBox_MouseUp;
            this.MouseClick += CustomPictureBox_MouseClick;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            try
            {
                var img = _internalImage;
                if (img == null)
                {
                    return;
                }

                pe.Graphics.Clear(this.BackColor);
                pe.Graphics.TranslateTransform(_fitOffset.X + _pan.X, _fitOffset.Y + _pan.Y);
                pe.Graphics.ScaleTransform(_fitZoom * _zoom, _fitZoom * _zoom);
                pe.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                pe.Graphics.DrawImage(img, 0, 0, img.Width, img.Height);

                pe.Graphics.ResetTransform();

                for (int i = 0; i < RoiList.Count; i++)
                {
                    var roiRect = NormalizeRect(ImageRectToScreenRect(RoiList[i]));
                    using (var pen = new Pen(i == ActiveRoiIndex ? Color.Orange : Color.Red, 2))
                    {
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        pe.Graphics.DrawRectangle(pen, roiRect);
                    }
                    DrawRoiHandles(pe.Graphics, roiRect, i == ActiveRoiIndex);
                }

                DrawCross(pe.Graphics);

                DrawMeasureScale(pe.Graphics);
            }
            catch (Exception ex)
            {

            }
        }

        private void DrawRoiHandles(Graphics g, Rectangle roiRect, bool isActive)
        {
            var handles = GetRoiHandles(roiRect);
            for (int i = 0; i < handles.Length; i++)
            {
                var pt = handles[i];
                var rect = new Rectangle(pt.X - HandleSize / 2, pt.Y - HandleSize / 2, HandleSize, HandleSize);
                g.FillEllipse(i == 8 ? Brushes.Yellow : (isActive ? Brushes.White : Brushes.LightGray), rect);
                g.DrawEllipse(isActive ? Pens.Red : Pens.Gray, rect);
            }
        }

        private Point[] GetRoiHandles(Rectangle roiRect)
        {
            int cx = (roiRect.Left + roiRect.Right) / 2;
            int cy = (roiRect.Top + roiRect.Bottom) / 2;
            return new[]
            {
                new Point(roiRect.Left, roiRect.Top),
                new Point(cx, roiRect.Top),
                new Point(roiRect.Right, roiRect.Top),
                new Point(roiRect.Right, cy),
                new Point(roiRect.Right, roiRect.Bottom),
                new Point(cx, roiRect.Bottom),
                new Point(roiRect.Left, roiRect.Bottom),
                new Point(roiRect.Left, cy),
                new Point(cx, cy)
            };
        }

        private void UpdateFitZoom()
        {
            if (Image == null || this.Width == 0 || this.Height == 0)
                return;
            float zoomX = (float)this.Width / Image.Width;
            float zoomY = (float)this.Height / Image.Height;
            _fitZoom = Math.Min(zoomX, zoomY);
            float imgW = Image.Width * _fitZoom;
            float imgH = Image.Height * _fitZoom;
            _fitOffset = new PointF((this.Width - imgW) / 2f, (this.Height - imgH) / 2f);
        }

        public new Image Image
        {
            get => _internalImage;
            set
            {
                // 一定在 UI thread
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() => Image = value));
                    return;
                }

                // 釋放舊圖（安全）
                if (_internalImage != null)
                {
                    _internalImage.Dispose();
                    _internalImage = null;
                }

                // 深拷貝新圖
                if (value != null)
                {
                    _internalImage = new Bitmap(value);
                }

                base.Image = _internalImage;

                if (AutoFitOnImageSet)
                    FitWindow();
                else
                {
                    UpdateFitZoom();
                    Invalidate();
                }
            }
        }

        public void FitWindow()
        {
            UpdateFitZoom();
            _pan = PointF.Empty;
            _zoom = 1.0f;
            Invalidate();
        }

        public void AddRoi(Rectangle roiImageRect)
        {
            if (Image == null)
            {
                RoiList.Add(roiImageRect);
                ActiveRoiIndex = RoiList.Count - 1;

                RoiListChanged?.Invoke(this, EventArgs.Empty);
                return;
            }
                
            var imgRect = roiImageRect;
            try
            {
                imgRect.Intersect(new Rectangle(0, 0, Image.Width, Image.Height));
            }
            catch (Exception ex)
            {
                imgRect.Intersect(new Rectangle(0, 0, 100, 100));
            }
            
            if (imgRect.Width > 0 && imgRect.Height > 0)
            {
                RoiList.Add(imgRect);
                ActiveRoiIndex = RoiList.Count - 1;

                RoiListChanged?.Invoke(this, EventArgs.Empty);
            }
            Invalidate();
        }

        public void SetActiveRoi(int index)
        {
            if (index >= 0 && index < RoiList.Count)
                ActiveRoiIndex = index;
            else
                ActiveRoiIndex = -1;
            Invalidate();
        }

        public void DeleteActiveRoi()
        {
            if (ActiveRoiIndex >= 0 && ActiveRoiIndex < RoiList.Count)
            {
                RoiList.RemoveAt(ActiveRoiIndex);
                if (ActiveRoiIndex >= RoiList.Count)
                    ActiveRoiIndex = RoiList.Count - 1;
                Invalidate();
            }
        }

        public void ClearRoi()
        {
            // 直接清空整個 List，免去迴圈的麻煩與陷阱
            RoiList.Clear();

            // 索引歸零
            ActiveRoiIndex = -1;

            // 📢 關鍵：通知外部的 DataGridView 數據變更（變為 0 筆）
            RoiListChanged?.Invoke(this, EventArgs.Empty);

            // 重新繪製畫布（框就會消失）
            Invalidate();
        }

        private Rectangle ImageRectToScreenRect(Rectangle imgRect)
        {
            var pt1 = ImageToScreen(new PointF(imgRect.Left, imgRect.Top));
            var pt2 = ImageToScreen(new PointF(imgRect.Right, imgRect.Bottom));
            return new Rectangle(
                (int)Math.Round(pt1.X),
                (int)Math.Round(pt1.Y),
                (int)Math.Round(pt2.X - pt1.X),
                (int)Math.Round(pt2.Y - pt1.Y)
            );
        }

        private Rectangle NormalizeRect(Rectangle rect)
        {
            int x = rect.X;
            int y = rect.Y;
            int w = rect.Width;
            int h = rect.Height;
            if (w < 0) { x += w; w = -w; }
            if (h < 0) { y += h; h = -h; }
            return new Rectangle(x, y, w, h);
        }

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

        private void CustomPictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Image == null) return;
            var imgPtBefore = ScreenToImage(e.Location);
            if (e.Delta > 0)
                _zoom *= 1.5f;
            else
                _zoom /= 1.5f;
            _zoom = Math.Max(0.1f, Math.Min(500f, _zoom));
            var newScreenPt = ImageToScreen(imgPtBefore);
            _pan.X += e.Location.X - newScreenPt.X;
            _pan.Y += e.Location.Y - newScreenPt.Y;
            Invalidate();
        }

        private void CustomPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                CallBackShowMenu(this);

                return;
            }

            if (!EnablePanAndRoi)
            {
                return;
            }

            for (int i = RoiList.Count - 1; i >= 0; i--)
            {
                var roiRectScreen = NormalizeRect(ImageRectToScreenRect(RoiList[i]));
                int handle = HitTestRoiHandle(e.Location, roiRectScreen);
                if (handle >= 0)
                {
                    ActiveRoiIndex = i;
                    _resizingRoi = true;
                    _resizeHandle = handle;
                    _resizeStartScreen = e.Location;
                    _resizeStartRoiScreen = roiRectScreen;
                    Cursor = handle == 8 ? Cursors.SizeAll : Cursors.Cross;
                    Invalidate();
                    return;
                }
                if (roiRectScreen.Contains(e.Location))
                {
                    ActiveRoiIndex = i;
                    Invalidate();
                    return;
                }
            }

            ActiveRoiIndex = -1;
            Invalidate();

            // 👉 啟動圖像拖曳
            _panning = true;
            _panStartPoint = e.Location;
            Cursor = Cursors.Hand;
        }

        private void CustomPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_resizingRoi && ActiveRoiIndex >= 0 && ActiveRoiIndex < RoiList.Count)
            {
                var roiRect = _resizeStartRoiScreen;
                int dx = e.X - _resizeStartScreen.X;
                int dy = e.Y - _resizeStartScreen.Y;
                Rectangle newRect = roiRect;

                if (_resizeHandle == 8)
                {
                    newRect.Offset(dx, dy);
                }
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

                if (newRect.Width < 1) newRect.Width = 1;
                if (newRect.Height < 1) newRect.Height = 1;

                var imgPt1 = ScreenToImage(newRect.Location);
                var imgPt2 = ScreenToImage(new Point(newRect.Right, newRect.Bottom));
                int x = (int)Math.Round(Math.Min(imgPt1.X, imgPt2.X));
                int y = (int)Math.Round(Math.Min(imgPt1.Y, imgPt2.Y));
                int w = (int)Math.Round(Math.Abs(imgPt2.X - imgPt1.X));
                int h = (int)Math.Round(Math.Abs(imgPt2.Y - imgPt1.Y));
                var imgRect = new Rectangle(x, y, w, h);
                imgRect.Intersect(new Rectangle(0, 0, Image.Width, Image.Height));
                if (imgRect.Width > 0 && imgRect.Height > 0)
                {
                    RoiList[ActiveRoiIndex] = imgRect;

                    RoiListChanged?.Invoke(this, EventArgs.Empty);
                }

                Invalidate();
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
            else
            {
                for (int i = RoiList.Count - 1; i >= 0; i--)
                {
                    var roiRectScreen = NormalizeRect(ImageRectToScreenRect(RoiList[i]));
                    int handle = HitTestRoiHandle(e.Location, roiRectScreen);
                    if (handle >= 0)
                    {
                        Cursor = handle == 8 ? Cursors.SizeAll : Cursors.Cross;
                        return;
                    }
                    if (roiRectScreen.Contains(e.Location))
                    {
                        Cursor = Cursors.Default;
                        return;
                    }
                }

                if (Image != null)
                {
                    var imgPt = ScreenToImage(e.Location);
                    int x1 = (int)Math.Round(imgPt.X);
                    int y1 = (int)Math.Round(imgPt.Y);
                    if (x1 >= 0 && y1 >= 0 && x1 < Image.Width && y1 < Image.Height)
                    {
                        ImagePointClicked?.Invoke(new Point(x1, y1));
                    }
                }

                Cursor = Cursors.Default;
            }
        }

        private void CustomPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (_resizingRoi)
            {
                _resizingRoi = false;
                _resizeHandle = -1;
                Cursor = Cursors.Default;
            }

            if (_panning)
            {
                _panning = false;
                Cursor = Cursors.Default;
            }
        }

        private void CustomPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Image != null)
            {
                
            }
        }

        private int HitTestRoiHandle(Point mouse, Rectangle roiRect)
        {
            var handles = GetRoiHandles(roiRect);
            for (int i = 0; i < handles.Length; i++)
            {
                var rect = new Rectangle(handles[i].X - HandleSize / 2, handles[i].Y - HandleSize / 2, HandleSize, HandleSize);
                if (rect.Contains(mouse))
                    return i;
            }
            return -1;
        }




        private void DrawCross(Graphics g)
        {
            if (_ptMouse != Point.Empty && _IsCalcScale)
            {
                using (var p = new Pen(Color.MediumOrchid, 2))
                {
                    g.DrawLine(p, 0, _ptMouse.Y, Width, _ptMouse.Y);
                    g.DrawLine(p, _ptMouse.X, 0, _ptMouse.X, Height);
                }
            }
        }

        private void DrawMeasureScale(Graphics g)
        {
            if (_HandFirstClickPt != Point.Empty && _HandEndClickPt != Point.Empty)
            {
                Pen p = new Pen(Color.Blue, 2);

                var _pt1 = ImageToScreen(_HandFirstClickPt);
                var _pt2 = ImageToScreen(_HandEndClickPt);

                float NumPixel = 0;

                float x = _HandFirstClickPt.X - _HandEndClickPt.X;
                float y = _HandFirstClickPt.Y - _HandEndClickPt.Y;

                if (scaleArrow == ScaleArrow.Horizontal)
                {
                    NumPixel = Math.Abs(x);
                }
                else
                {
                    NumPixel = Math.Abs(y);
                }

                float pixelSize = Dist / NumPixel * 1000;

                PointF _pt11 = PointF.Empty, _pt12 = PointF.Empty, _pt21 = PointF.Empty, _pt22 = PointF.Empty;

                CalSideline(_HandFirstClickPt, _HandEndClickPt, 40, out _pt11, out _pt12, out _pt21, out _pt22);

                var _pt5 = ImageToScreen(_pt11);
                var _pt6 = ImageToScreen(_pt12);
                var _pt7 = ImageToScreen(_pt21);
                var _pt8 = ImageToScreen(_pt22);


                g.DrawLine(p, _pt1, _pt2);
                g.DrawLine(p, _pt5, _pt6);
                g.DrawLine(p, _pt7, _pt8);

                double rad = Math.Atan((_HandEndClickPt.Y - _HandFirstClickPt.Y) / (_HandEndClickPt.X - _HandFirstClickPt.X));

                float degrees = (float)(rad * (180 / Math.PI));

                string str = "PixelSize = " + pixelSize.ToString("F3") + " (um)";

                Font font;

                if ((int)(2 * _zoom) <= 0)
                {
                    font = new Font("微軟正黑體", (int)(1));
                }
                else
                {
                    font = new Font("微軟正黑體", (int)(2 * _zoom));
                }

                SizeF size = g.MeasureString(str, font);

                PointF _ptCenter = ImageToScreen(new PointF((_HandFirstClickPt.X + _HandEndClickPt.X) / 2, (_HandFirstClickPt.Y + _HandEndClickPt.Y) / 2));

                g.TranslateTransform(_ptCenter.X, _ptCenter.Y);

                g.RotateTransform(degrees);

                g.DrawString(str, font, Brushes.Blue, -(size.Width / 2 + 2 * _zoom), -(size.Height / 2 + 5 * _zoom));
            }
        }

        public int CalSideline(PointF Pa, PointF Pb, double L, out PointF a1, out PointF a2, out PointF b1, out PointF b2)
        {
            try
            {
                double m = (Pb.Y - Pa.Y) / (Pb.X - Pa.X);
                double Nm = 1 / -m;

                double LineLength = L / 2;
                double rad = Math.Atan(Nm); // 弧度
                double sin = Math.Sin(rad) * LineLength; // sin
                double cos = Math.Cos(rad) * LineLength; // cos

                a1 = new PointF(Pa.X + (float)cos, Pa.Y + (float)sin);
                a2 = new PointF(Pa.X - (float)cos, Pa.Y - (float)sin);

                b1 = new PointF(Pb.X + (float)cos, Pb.Y + (float)sin);
                b2 = new PointF(Pb.X - (float)cos, Pb.Y - (float)sin);
            }
            catch (Exception ex)
            {
                a1 = a2 = Pa;
                b1 = b2 = Pb;
                return -1;
            }

            return 0;
        }
    }
}