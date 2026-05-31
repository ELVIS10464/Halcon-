#define DrawingCacheImg
#define DrawingVisibleRange
#define CacheImgSize

using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing;
using System;
using System.Runtime.InteropServices;

namespace AlgorithmProcess
{
    /// <summary>
	/// A PictureBox with configurable interpolation mode.
	/// </summary>
	[ToolboxBitmap(typeof(ScalePictureBox)), ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms"), Description("Simple Scalable Image Viewer")]

    public partial class ScalePictureBox : PictureBox
    {
        public enum eOriginType
        {
            Manual = -1,
            LeftTop = 0,
            MiddleTop,
            RightTop,
            RightMiddle,
            RightBottom,
            MiddleBottom,
            LeftBottom,
            LeftMiddle,
            Body,
        }
        public float m_ZoomFactor = 0.0f, m_InitZoomFactor = 0.0f;  // 20230915 yochen private -> public
        private int m_PtX, m_PtY, m_StartX, m_StartY, m_ptAnchorX, m_ptAnchorY;
        private bool m_MousePressed = false;
        private Point m_MouseDownPt;
        private Image m_Image;
        private bool m_ShowCrosshair = true, m_ShowOrigin = true;
        private int m_CrosshairSize = 20;
        private bool m_IsInitView = false;
        private bool m_IsDisableCtrl = false;
        public delegate void ImgPaintHander(Graphics g);
        public delegate void ImgSourceChgDel(Bitmap bmp);
        public ImgSourceChgDel OnImageSourceChanged;
        private int m_ImageW, m_ImageH;
        private bool m_bRefrash = false;
        public bool Refresh { set; get; }

        public float m_ZoomStep = 0.3F;    //20200630:Edison add
        private eOriginType m_OriType = eOriginType.Body;
        private Point m_ptOrigin = new Point();
        private RectangleF m_rctfView = new RectangleF();
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageViewer"/> class.
        /// </summary>
        public ScalePictureBox()
        {
            // Set default.
            InterpolationMode = InterpolationMode.Default;
            this.DoubleBuffered = true;
            FitImage = false;

            m_PtX = 0;
            m_PtY = 0;
            m_ZoomFactor = 0.0f;
            m_InitZoomFactor = 0.0f;
            m_IsInitView = true;
        }
        #endregion

        /// <summary>
        /// Gets or sets a value indicating whether the button should fade in and fade out when it's getting and loosing the focus.
        /// </summary>
        /// <value><c>true</c> if fading with changing the focus; otherwise, <c>false</c>.</value>
        [DefaultValue(false), Category("Appearance"), Description("Select Bitmap as image")]
        public new Image Image
        {
            get { return m_Image; }
            set
            {
                m_Image = value;


                if (null != value)
                {
                    m_ImageW = m_Image.Width;
                    m_ImageH = m_Image.Height;
                }
                else
                {
                    m_ImageW = 0;
                    m_ImageH = 0;
                }
                //CalculateScale();


                if (null != OnImageSourceChanged)
                    OnImageSourceChanged((Bitmap)value);

                CreateHandle();

                if (IsHandleCreated && Refresh)
                {
                    Invalidate();

                }
                if (FitImage || (m_ZoomFactor > 0.001f && m_ZoomFactor < 0.1f))
                {
                    FitWindow();
                    FitImage = false;
                }
            }
        }
        public new Image ImageBase
        {
            get { return m_Image; }
            set
            {
                Image = value;

                if (null != value)
                {
                    m_ImageW = m_Image.Width;
                    m_ImageH = m_Image.Height;
                }
                else
                {
                    m_ImageW = 0;
                    m_ImageH = 0;
                }


            }
        }
        #region Properties
        /// <summary>
        /// Gets or sets the interpolation mode.
        /// </summary>
        /// <value>The interpolation mode.</value>
        [DefaultValue(InterpolationMode.Default), Category("Behavior")]
        public InterpolationMode InterpolationMode { get; set; }

        [DefaultValue(0.0f), Category("Appearance"), Description("Set/Get the scale factor of display image")]
        public float ScaleFactor
        {
            get { return m_ZoomFactor; }
            set
            {
                if (m_ZoomFactor != value)
                {
                    m_ZoomFactor = value;
                    if (IsHandleCreated)
                    {
                        Invalidate();
                    }
                }
            }
        }

        [DefaultValue(true), Category("Appearance"), Description("Show axis-system origin point")]
        public bool ShowOrigin
        {
            get { return m_ShowOrigin; }
            set
            {
                if (m_ShowOrigin != value)
                {
                    m_ShowOrigin = value;
                    if (IsHandleCreated)
                    {
                        Invalidate();
                    }
                }
            }
        }

        [DefaultValue(true), Category("Appearance"), Description("Show crosshair at image center")]
        public bool ShowCrosshair
        {
            get { return m_ShowCrosshair; }
            set
            {
                if (m_ShowCrosshair != value)
                {
                    m_ShowCrosshair = value;
                    if (IsHandleCreated)
                    {
                        Invalidate();
                    }
                }
            }
        }

        [DefaultValue(10), Category("Appearance"), Description("Set/Get crosshair size")]
        public int CrosshairSize
        {
            get { return m_CrosshairSize; }
            set
            {
                if (m_CrosshairSize != value)
                {
                    m_CrosshairSize = value;
                    if (IsHandleCreated)
                    {
                        Invalidate();
                    }
                }
            }
        }

        [Browsable(true), Category("Action"), Description("Draw graphics upon image")]
        public event ImgPaintHander ImgPaint;

        //public void CalculateScale()
        //{
        //	if (m_ImageW >= m_ImageH)
        //	{
        //		m_ZoomFactor = ((float)Width / (float)m_ImageW) /** (m_Image.HorizontalResolution / pe.Graphics.DpiX)*/;
        //		int dispImgWidth = (int)(((float)m_ImageW) * m_ZoomFactor);
        //		int dispImgHeight = (int)(((float)m_ImageH) * m_ZoomFactor);

        //		if (Height < dispImgHeight)
        //		{
        //			m_ZoomFactor = ((float)Height / (float)m_ImageH)/* * (m_Image.VerticalResolution / pe.Graphics.DpiY)*/;
        //			dispImgWidth = (int)(((float)m_ImageW) * m_ZoomFactor);
        //			m_PtX = (int)((float)((Width - dispImgWidth) / 2) * (1.0F / m_ZoomFactor));
        //		}
        //		else
        //		{
        //			m_PtY = (int)((float)((Height - dispImgHeight) / 2) * (1.0F / m_ZoomFactor));
        //		}

        //	}
        //	else
        //	{
        //		m_ZoomFactor = ((float)Height / (float)m_ImageH) /** (m_Image.VerticalResolution / pe.Graphics.DpiY)*/;
        //		int dispImgWidth = (int)(((float)m_ImageW) * m_ZoomFactor);
        //		int dispImgHeight = (int)(((float)m_ImageH) * m_ZoomFactor);
        //		int a = Height;
        //		if (Width < dispImgWidth)
        //		{
        //			m_ZoomFactor = ((float)Width / (float)m_ImageW)/* * (m_Image.HorizontalResolution / pe.Graphics.DpiX)*/;
        //			dispImgHeight = (int)(((float)m_ImageH) * m_ZoomFactor);
        //			m_PtY = (int)((float)((Height - dispImgHeight) / 2) * (1.0F / m_ZoomFactor));
        //		}
        //		else
        //		{
        //			m_PtX = (int)((float)((Width - dispImgWidth) / 2) * (1.0F / m_ZoomFactor));
        //		}

        //	}
        //}

        public PointF Offset
        {
            get { return new PointF(m_PtX, m_PtY); }
            set
            {
                if (Offset != value)
                {
                    m_PtX = (int)value.X;
                    m_PtY = (int)value.Y;
                }
            }
        }

        public Point ptOrigin
        {
            set { m_ptOrigin = value; }
            get { return m_ptOrigin; }
        }

        public RectangleF View
        {
            get => m_rctfView;
        }
        public bool FitImage
        {
            set;
            get;
        }
        #endregion

        private Object m_PaintLocker = new Object();

        #region Overrides of PictureBox
        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {

                try
                {
                    base.CreateHandle();
                }

                catch { }

                finally
                {
                    if (!IsHandleCreated)
                    {
                        base.RecreateHandle();
                    }
                }
            }
        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"/> event.
        /// </summary>
        /// <param name="pe">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data. </param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            if (0 == m_ImageW || 0 == m_ImageH)
                return;

            lock (m_PaintLocker)
            {
                if (m_IsInitView && null != m_Image)
                {
                    pe.Graphics.Clear(base.BackColor);

                    if (m_ImageW >= m_ImageH)
                    {
                        m_ZoomFactor = ((float)Width / (float)m_ImageW) * (m_Image.HorizontalResolution / pe.Graphics.DpiX);
                        int dispImgWidth = (int)(((float)m_ImageW) * m_ZoomFactor);
                        int dispImgHeight = (int)(((float)m_ImageH) * m_ZoomFactor);

                        if (Height < dispImgHeight)
                        {
                            m_ZoomFactor = ((float)Height / (float)m_ImageH) * (m_Image.VerticalResolution / pe.Graphics.DpiY);

                            dispImgWidth = (int)(((float)m_ImageW) * m_ZoomFactor);
                            m_ptAnchorX = m_PtX = (int)((float)((Width - dispImgWidth) / 2) * (1.0F / m_ZoomFactor));
                        }
                        else
                        {
                            m_ptAnchorY = m_PtY = (int)((float)((Height - dispImgHeight) / 2) * (1.0F / m_ZoomFactor));
                        }

                    }
                    else
                    {
                        m_ZoomFactor = ((float)Height / (float)m_ImageH) * (m_Image.VerticalResolution / pe.Graphics.DpiY);
                        int dispImgWidth = (int)(((float)m_ImageW) * m_ZoomFactor);
                        int dispImgHeight = (int)(((float)m_ImageH) * m_ZoomFactor);
                        int a = Height;
                        if (Width < dispImgWidth)
                        {
                            m_ZoomFactor = ((float)Width / (float)m_ImageW) * (m_Image.HorizontalResolution / pe.Graphics.DpiX);
                            dispImgHeight = (int)(((float)m_ImageH) * m_ZoomFactor);

                            m_PtY = (int)((float)((Height - dispImgHeight) / 2) * (1.0F / m_ZoomFactor));
                        }
                        else
                        {
                            m_PtX = (int)((float)((Width - dispImgWidth) / 2) * (1.0F / m_ZoomFactor));
                        }

                    }

                    m_InitZoomFactor = m_ZoomFactor;
                    m_IsInitView = false;
                }
                else if (0.5F <= Math.Abs(m_ZoomFactor - m_InitZoomFactor))
                    CheckBound(new Point(m_PtX, m_PtY));

                if (m_rctfView != null)
                {
                    //20230424 add by xk4
                    PointF ptf = ImgPtToDispPt(new PointF(m_PtX, m_PtY));

                    float x = (m_PtX) / m_ZoomFactor;
                    float y = (m_PtY) / m_ZoomFactor;
                    float fw = Width / m_ZoomFactor;
                    float fh = Height / m_ZoomFactor;
                    float foffsetx = (fw - m_ImageW) / 2;
                    float foffsetY = (fh - m_ImageH) / 2;
                    if (ptf.X >= 0)
                    {
                        x = 0;
                        fw = fw - x;
                    }
                    else
                    {
                        x = x * -m_ZoomFactor;
                    }

                    if (ptf.Y >= 0)
                    {
                        y = 0;
                        fh = fh - y;
                    }
                    else
                    {
                        y = y * -m_ZoomFactor;
                    }


                    //float w = (m_ImageW + m_PtX) / m_ZoomFactor;
                    //float h = (m_ImageH + m_PtY) / m_ZoomFactor;
                    m_rctfView = new RectangleF(x, y, fw, fh);
                }
                pe.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

                /*Draw upon Image Begin*/
                //using (Graphics imgG = Graphics.FromImage(m_DispImage))
                {
                    pe.Graphics.ScaleTransform(m_ZoomFactor, m_ZoomFactor);
                    pe.Graphics.TranslateTransform(m_PtX, m_PtY);


                    //if (m_IsNeedRedraw)
                    {
                        //PointF imgStartPt = DispPtToImgPt(new Point(0, 0));
                        //PointF imgEndPt = DispPtToImgPt(new Point(this.ClientSize.Width, this.ClientSize.Height));

                        //RectangleF imgRect = PointsToRectangleF(imgStartPt, imgEndPt);
                        //RectangleF dispRect = PointsToRectangleF(new PointF(0, 0), new PointF(this.ClientSize.Width, this.ClientSize.Height));

                        pe.Graphics.DrawImage(m_Image, 0, 0, m_ImageW, m_ImageH);
                        //pe.Graphics.DrawImage(m_Image, dispRect, imgRect, GraphicsUnit.Pixel);
                    }

                    if (null != ImgPaint)
                        ImgPaint(pe.Graphics);

                    if (m_ShowCrosshair)
                    {
                        /*float crosshairSize = (float)m_ImageW / 10.0F;

                        PointF ptX1 = new PointF((float)m_ImageW / 2.0F - crosshairSize, (float)m_ImageH / 2.0F);
                        PointF ptX2 = new PointF((float)m_ImageW / 2.0F + crosshairSize, (float)m_ImageH / 2.0F);
                        PointF ptY1 = new PointF((float)m_ImageW / 2.0F, (float)m_ImageH / 2.0F - crosshairSize);
                        PointF ptY2 = new PointF((float)m_ImageW / 2.0F, (float)m_ImageH / 2.0F + crosshairSize);*/

                        PointF ptX1 = new PointF((float)m_ImageW / 2.0F, (float)0);
                        PointF ptX2 = new PointF((float)m_ImageW / 2.0F, (float)m_ImageH);
                        PointF ptY1 = new PointF((float)0, (float)m_ImageH / 2.0F);
                        PointF ptY2 = new PointF((float)m_ImageW, (float)m_ImageH / 2.0F);

                        pe.Graphics.DrawLine(new Pen(Color.Red, 5), ptX1, ptX2);
                        pe.Graphics.DrawLine(new Pen(Color.Red, 5), ptY1, ptY2);
                    }

                    if (m_ShowOrigin)
                    {
                        //pe.Graphics.DrawLine(new Pen(Color.Lime, 3), new Point(1, 0), new Point(1, m_ImageH / 10));
                        //pe.Graphics.DrawLine(new Pen(Color.Lime, 3), new Point(0, 1), new Point(m_ImageW / 10, 1));
                        DrawOrigin(pe.Graphics);
                    }

                    pe.Graphics.ResetTransform();
                }

                /*Draw upon Image End*/
                //pe.Graphics.ScaleTransform(m_ZoomFactor, m_ZoomFactor);


                //pe.Graphics.DrawImage(m_Image, dispRect, imgRect, GraphicsUnit.Pixel);
            }

            //pe.Graphics.DrawImage(m_DispImage, m_PtX, m_PtY);

            //pe.Graphics.ResetTransform();


            /*Draw upon Viewer Begin*/
            base.OnPaint(pe);
            /*Draw upon Viewer End*/
        }
        private void DrawOrigin(Graphics g)
        {
            Point[] pts = new Point[4];

            switch (m_OriType)
            {
                case eOriginType.Manual:
                    pts[0].X = m_ptOrigin.X - m_ImageW / 10;
                    pts[1].X = m_ptOrigin.X + m_ImageW / 10;
                    pts[0].Y = m_ptOrigin.Y;
                    pts[1].Y = m_ptOrigin.Y;

                    pts[2].X = m_ptOrigin.X;
                    pts[3].X = m_ptOrigin.X;
                    pts[2].Y = m_ptOrigin.Y - m_ImageH / 10;
                    pts[3].Y = m_ptOrigin.Y + m_ImageH / 10;

                    break;
                case eOriginType.LeftTop:
                    break;
                case eOriginType.MiddleTop:

                    break;
                case eOriginType.RightTop:
                    break;
                case eOriginType.RightMiddle:
                    break;
                case eOriginType.RightBottom:
                    break;
                case eOriginType.MiddleBottom:
                    break;
                case eOriginType.LeftBottom:
                    break;
                case eOriginType.LeftMiddle:
                    break;
                case eOriginType.Body:
                    break;
            }
            g.DrawLine(new Pen(Color.Lime, 3), pts[0], pts[1]);
            g.DrawLine(new Pen(Color.Lime, 3), pts[2], pts[3]);
        }
        protected override void OnMouseEnter(System.EventArgs e)
        {
            //base.Focus();
            base.OnMouseEnter(e);
        }

        protected override void OnClick(System.EventArgs e)
        {
            base.Focus();
            base.OnClick(e);
        }

        protected override void OnDoubleClick(System.EventArgs e)
        {
            //20221021 add 避免同時動到主畫面
            if (!base.Focused)
            {
                return;
            }
            if (!m_IsDisableCtrl)
                FitWindow();
            base.OnDoubleClick(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //20221021 add 避免同時動到主畫面
            if (!base.Focused)
            {
                return;
            }

            if (null == m_Image || m_IsDisableCtrl)
            {
                base.OnMouseWheel(e);
                return;
            }
            float oldzoom = m_ZoomFactor;

            if (e.Delta > 0)
            {
                m_ZoomFactor *= (1 + m_ZoomStep);   //20200630:Edison edit 0.5 -> 0.02
            }

            else if (e.Delta < 0)
            {
                if ((m_ZoomFactor /*- m_ZoomStep*/) > 0.05)
                    m_ZoomFactor *= (1 - m_ZoomStep);
            }

            MouseEventArgs mouse = e as MouseEventArgs;
            Point mousePosNow = mouse.Location;

            int x = mousePosNow.X - Location.X;    // Where location of the mouse in the pictureframe
            int y = mousePosNow.Y - Location.Y;

            int oldimagex = (int)(x / oldzoom);  // Where in the IMAGE is it now
            int oldimagey = (int)(y / oldzoom);

            int newimagex = (int)(x / m_ZoomFactor);     // Where in the IMAGE will it be when the new zoom i made
            int newimagey = (int)(y / m_ZoomFactor);

            m_PtX = newimagex - oldimagex + m_PtX;  // Where to move image to keep focus on one point
            m_PtY = newimagey - oldimagey + m_PtY;

            if (m_ImageW >= m_ImageH && 0 >= Math.Abs(m_ZoomFactor - m_InitZoomFactor))
            {
                //FitWindow();
                /*
                m_PtX = 0;
                int dispImgHeight = (int)(((float)m_ImageH) * m_ZoomFactor);

                m_PtY = 0;//(int)((float)((Height - dispImgHeight) / 2 - 4) * (1.0F / m_ZoomFactor));
                 */
            }
            else if (m_ImageW < m_ImageH && 0 >= Math.Abs(m_ZoomFactor - m_InitZoomFactor))
            {
                //FitWindow();
                /*
                m_PtY = 0;
                int dispImgWidth = (int)(((float)m_ImageW) * m_ZoomFactor);

                m_PtX = 0;//(int)((float)((Width - dispImgWidth) / 2 - 4) * (1.0F / m_ZoomFactor));
                 */
            }

            Invalidate();  // calls imageBox_Paint
            base.OnMouseWheel(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.Focus();//20221021 add 避免同時動到主畫面
            if (null == m_Image || m_IsDisableCtrl)
            {
                base.OnMouseDown(e);
                return;
            }

            // 20230915 yochen OM是用右鍵拖移圖片
            if (e.Button == MouseButtons.Left /*&& 0.5F <= Math.Abs(m_ZoomFactor - m_InitZoomFactor)*/) //2016-12-7
            {
                if (!m_MousePressed)
                {
                    m_MousePressed = true;
                    m_MouseDownPt = e.Location;
                    m_StartX = m_PtX;
                    m_StartY = m_PtY;

                    //this.Cursor = Cursors.Hand;
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            //20221021 add 避免同時動到主畫面
            if (!base.Focused)
            {
                return;
            }
            if (null == m_Image || m_IsDisableCtrl)
            {
                base.OnMouseMove(e);
                return;
            }
            // 20230915 yochen OM是用右鍵拖移圖片
            if (e.Button == MouseButtons.Left && m_MousePressed /*&& 0.5F <= Math.Abs(m_ZoomFactor - m_InitZoomFactor)*/) //2016-12-7
            {
                Point mousePosNow = e.Location;

                int deltaX = mousePosNow.X - m_MouseDownPt.X; // the distance the mouse has been moved since mouse was pressed
                int deltaY = mousePosNow.Y - m_MouseDownPt.Y;

                m_PtX = (int)(m_StartX + (deltaX / m_ZoomFactor));  // calculate new offset of image based on the current zoom factor
                m_PtY = (int)(m_StartY + (deltaY / m_ZoomFactor));


                Invalidate();

            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (null == m_Image || m_IsDisableCtrl)
            {
                base.OnMouseUp(e);
                return;
            }
            // 20230915 yochen OM是用右鍵拖移圖片
            if (e.Button == MouseButtons.Left)
            {
                m_MousePressed = false;
                //this.Cursor = Cursors.Default;
            }

            base.OnMouseUp(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (null == m_Image || m_IsDisableCtrl)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                switch (keyData)
                {
                    case Keys.Right:
                        //if (m_InitZoomFactor != m_ZoomFactor)
                        {
                            m_PtX -= (int)(Width * 0.1F / m_ZoomFactor);
                            Invalidate();
                        }
                        break;

                    case Keys.Left:
                        //if (m_InitZoomFactor != m_ZoomFactor)
                        {
                            m_PtX += (int)(Width * 0.1F / m_ZoomFactor);
                            Invalidate();
                        }
                        break;

                    case Keys.Down:
                        //if (m_InitZoomFactor != m_ZoomFactor)
                        {
                            m_PtY -= (int)(Height * 0.1F / m_ZoomFactor);
                            Invalidate();
                        }
                        break;

                    case Keys.Up:
                        //if (m_InitZoomFactor != m_ZoomFactor)
                        {
                            m_PtY += (int)(Height * 0.1F / m_ZoomFactor);
                            Invalidate();
                        }
                        break;

                    case Keys.PageDown:
                        m_ZoomFactor += 0.5F;
                        Invalidate();
                        break;

                    case Keys.PageUp:
                        m_ZoomFactor = Math.Max(m_ZoomFactor - 0.5F, m_InitZoomFactor);

                        if (m_ImageW >= m_ImageH && 0 >= Math.Abs(m_ZoomFactor - m_InitZoomFactor))
                        {
                            FitWindow();
                            /*
                            m_PtX = 0;
                            int dispImgHeight = (int)(((float)m_ImageH) * m_ZoomFactor);

                            m_PtY = 0;//(int)((float)((Height - dispImgHeight) / 2 - 4) * (1.0F / m_ZoomFactor));
                            */
                        }
                        else if (m_ImageW < m_ImageH && 0 >= Math.Abs(m_ZoomFactor - m_InitZoomFactor))
                        {
                            FitWindow();
                            /*
                            m_PtY = 0;
                            int dispImgWidth = (int)(((float)m_ImageW) * m_ZoomFactor);

                            m_PtX = 0;//(int)((float)((Width - dispImgWidth) / 2 - 4) * (1.0F / m_ZoomFactor));
                            */
                        }
                        Invalidate();
                        break;
                }
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        public void FitROI(RectangleF ROIRect)
        {
            if (null == m_Image)
                return;

            //m_PtX = (int)ROIRect.X + (int)(ROIRect.Width / 2.0F);
            //m_PtY = (int)ROIRect.Y + (int)(ROIRect.Height / 2.0F);

            ////Move the start position to the origin point
            //m_PtX = -1 * (int)(m_PtX);
            //m_PtY = -1 * (int)(m_PtY);

            //int dispImgWidth = (int)(((float)m_ImageW) * m_ZoomFactor);
            //int dispImgHeight = (int)(((float)m_ImageH) * m_ZoomFactor);

            ////Move to the screen center
            //m_PtX += (int)((float)dispImgWidth / 2.0F);
            //m_PtY += (int)((float)dispImgHeight / 2.0F);

            m_PtX = (int)ROIRect.X;
            m_PtY = (int)ROIRect.Y;

            //Move the start position to the origin point
            m_PtX = -1 * (int)(m_PtX);
            m_PtY = -1 * (int)(m_PtY);

            Graphics g = Graphics.FromImage(m_Image);

            //Compute ROI fit the window
            if (ROIRect.Width > ROIRect.Height)
            {
                m_ZoomFactor = ((float)Width / ROIRect.Width) * (m_Image.HorizontalResolution / g.DpiX);
                int dispImgWidth = (int)((ROIRect.Width) * m_ZoomFactor);
                int dispImgHeight = (int)((ROIRect.Height) * m_ZoomFactor);

                if (Height < dispImgHeight)
                {
                    m_ZoomFactor = ((float)Height / (float)ROIRect.Height) * (m_Image.VerticalResolution / g.DpiY);
                    dispImgWidth = (int)(((float)ROIRect.Width) * m_ZoomFactor);
                    m_PtX += (int)((float)((Width - dispImgWidth) / 2) * (1.0F / m_ZoomFactor));
                }
                else
                {
                    m_PtY += (int)((float)((Height - dispImgHeight) / 2) * (1.0F / m_ZoomFactor));
                }
            }
            else
            {
                m_ZoomFactor = ((float)Height / ROIRect.Height) * (m_Image.VerticalResolution / g.DpiY);
                int dispImgWidth = (int)(((float)ROIRect.Width) * m_ZoomFactor);
                int dispImgHeight = (int)(((float)ROIRect.Height) * m_ZoomFactor);

                if (Width < dispImgWidth)
                {
                    m_ZoomFactor = ((float)Width / (float)ROIRect.Width) * (m_Image.HorizontalResolution / g.DpiX);
                    dispImgHeight = (int)(((float)ROIRect.Height) * m_ZoomFactor);
                    m_PtY += (int)((float)((Height - dispImgHeight) / 2) * (1.0F / m_ZoomFactor));
                }
                else
                {
                    m_PtX += (int)((float)((Width - dispImgWidth) / 2) * (1.0F / m_ZoomFactor));
                }
            }

            Invalidate();
        }

        public void FitWindow()
        {
            FitImage = true;

            if (null == m_Image)
                return;

            m_ZoomFactor = m_InitZoomFactor;

            //Compute ROI fit the window
            if (m_Image.Width > m_Image.Height)
            {
                m_ZoomFactor = (float)Width / m_Image.Width;
                int dispImgWidth = (int)((m_Image.Width) * m_ZoomFactor);
                int dispImgHeight = (int)((m_Image.Height) * m_ZoomFactor);

                if (Height < dispImgHeight)
                {
                    m_ZoomFactor = (float)Height / (float)m_Image.Height;
                    dispImgWidth = (int)(((float)m_Image.Width) * m_ZoomFactor);
                    m_PtX = (int)((float)((Width - dispImgWidth) / 2) * (1.0F / m_ZoomFactor));
                    m_PtY = 0;
                }
                else
                {
                    m_PtY = (int)((float)((Height - dispImgHeight) / 2) * (1.0F / m_ZoomFactor));
                    m_PtX = 0;
                }
            }
            else
            {
                m_ZoomFactor = (float)Height / m_Image.Height;
                int dispImgWidth = (int)(((float)m_Image.Width) * m_ZoomFactor);
                int dispImgHeight = (int)(((float)m_Image.Height) * m_ZoomFactor);

                if (Width < dispImgWidth)
                {
                    m_ZoomFactor = (float)Width / (float)m_Image.Width;
                    dispImgHeight = (int)(((float)m_Image.Height) * m_ZoomFactor);
                    m_PtY = (int)((float)((Height - dispImgHeight) / 2) * (1.0F / m_ZoomFactor));
                    m_PtX = 0;
                }
                else
                {
                    m_PtX = (int)((float)((Width - dispImgWidth) / 2) * (1.0F / m_ZoomFactor));
                    m_PtY = 0;
                }
            }

            m_IsInitView = false;
            Invalidate();
        }

        public void ScaleImage(int scalePercent)
        {
            if (null == m_Image || 0 >= scalePercent)
                return;

            float zoomFactor = (float)scalePercent / 100.0F;

            if (zoomFactor >= m_InitZoomFactor)
                m_ZoomFactor = zoomFactor;

            Invalidate();
        }

        public void RedrawSource()
        {
            Invalidate();
        }

        public void SaveImage(String fileName)
        {
            try
            {
                m_Image.Save(fileName);
            }
            catch
            {

            }
        }

        public void ReplaceImage(Image img)
        {
            if (null == m_Image && null != img)
            {
                m_Image = img;

                m_PtX = 0;
                m_PtY = 0;
                m_ZoomFactor = 0.0f;
                m_InitZoomFactor = 0.0f;
                m_IsInitView = true;
                if (IsHandleCreated)
                {
                    Invalidate();
                }
            }
            else if (null != m_Image && null != img && (m_ImageW != img.Width || m_ImageH != img.Height))
            {
                m_Image = img;

                m_PtX = 0;
                m_PtY = 0;
                m_ZoomFactor = 0.0f;
                m_InitZoomFactor = 0.0f;
                m_IsInitView = true;
                if (IsHandleCreated)
                {
                    Invalidate();
                }
            }
            else
            {
                m_Image = img;
            }

            if (null != img)
            {
                m_ImageW = img.Width;
                m_ImageH = img.Height;
            }
            else
            {
                m_ImageW = 0;
                m_ImageH = 0;
            }

            if (null != OnImageSourceChanged)
                OnImageSourceChanged((Bitmap)img);

            Invalidate();
        }

        /// <summary>
        /// 換算成縮放後的坐標系
        /// imageX = (pt.X - shiftX) / Zoom = pt.X / Zoom - shiftX / Zoom;  m_PtX = shiftX / Zoom; shiftX = (背景X - 縮放後的X) / 2  (pixel)
        /// </summary>
        /// <param name="pt">給UI上的Point</param>
        /// <returns></returns>
        public Point DispPtToImgPt(Point pt)
        {
            Point imgPt = new Point();

            int imageX = (int)(pt.X / m_ZoomFactor) - m_PtX;
            int imageY = (int)(pt.Y / m_ZoomFactor) - m_PtY;

            imgPt = new Point(imageX, imageY);

            return imgPt;
        }

        /// <summary>
        /// 換算成縮放後的坐標系
        /// </summary>
        /// <param name="pt">給UI上的Point</param>
        /// <returns></returns>
        public PointF DispPtToImgPt(PointF pt)
        {
            PointF imgPt = new PointF();
            float imageX = (pt.X / m_ZoomFactor) - (float)m_PtX;
            float imageY = (pt.Y / m_ZoomFactor) - (float)m_PtY;

            imgPt = new PointF(imageX, imageY);

            return imgPt;
        }

        /// <summary>
        /// 換算成UI上的坐標系
        /// </summary>
        /// <param name="pt">給縮放後Image上的Point</param>
        /// <returns></returns>
        public Point ImgPtToDispPt(Point pt)
        {
            Point dispPt = new Point();
            int dispX = (int)((pt.X + m_PtX) * m_ZoomFactor);
            int dispY = (int)((pt.Y + m_PtY) * m_ZoomFactor);

            dispPt = new Point(dispX, dispY);
            return dispPt;
        }

        public PointF ImgPtToDispPt(PointF pt)
        {
            PointF dispPt = new PointF();
            float dispX = ((pt.X + (float)m_PtX) * m_ZoomFactor);
            float dispY = ((pt.Y + (float)m_PtY) * m_ZoomFactor);

            dispPt = new PointF(dispX, dispY);
            return dispPt;
        }

        public void DisableControl()
        {
            FitWindow();
            m_IsDisableCtrl = true;
        }

        public void EnableControl()
        {
            FitWindow();
            m_IsDisableCtrl = false;
        }

        public void CheckBound(Point pt)
        {
            //int PtX = pt.X;
            //int PtY = pt.Y;

            //Point BRDispEndPt = ImgPtToDispPt(new Point(m_ImageW, m_ImageH));
            //Point BRImgBeginPt = DispPtToImgPt(new Point(BRDispEndPt.X - Width, BRDispEndPt.Y - Height));

            //if (0 >= PtX && BRImgBeginPt.X >= PtX * -1)
            //    m_PtX = PtX;
            //else if (0 < PtX)
            //    m_PtX = 0;
            //else if (BRImgBeginPt.X < PtX * -1)
            //    m_PtX = BRImgBeginPt.X * -1;


            //if (0 >= PtY && BRImgBeginPt.Y >= PtY * -1)
            //    m_PtY = PtY;
            //else if (0 < PtY)
            //    m_PtY = 0;
            //else if (BRImgBeginPt.Y < PtY * -1)
            //    m_PtY = BRImgBeginPt.Y * -1;
        }

        private static RectangleF PointsToRectangleF(PointF pt1, PointF pt2)
        {
            RectangleF rectF = new RectangleF(new PointF(Math.Min(pt1.X, pt2.X), Math.Min(pt1.Y, pt2.Y)), new SizeF(Math.Abs(pt1.X - pt2.X), Math.Abs(pt1.Y - pt2.Y)));

            return rectF;
        }

        public static Bitmap CloneBitmap(int width, int height, IntPtr ptr)
        {
            Bitmap resBmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            System.Drawing.Imaging.BitmapData resBmpData = resBmp.LockBits(new System.Drawing.Rectangle(0, 0, width, height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            int bytes = Math.Abs(resBmpData.Stride) * height;

            //Byte[] rgbValues = new Byte[bytes];

            //Marshal.Copy(ptr, rgbValues, 0, bytes);
            //Marshal.Copy(rgbValues, 0, resBmpData.Scan0, bytes);
            memcpy(resBmpData.Scan0, ptr, bytes);
            resBmp.UnlockBits(resBmpData);

            //Array.Clear(rgbValues, 0, rgbValues.Length);
            //GC.Collect();

            return resBmp;
        }

        public static Bitmap CloneBitmap8(int width, int height, IntPtr ptr)
        {
            Bitmap resBmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            System.Drawing.Imaging.BitmapData resBmpData = resBmp.LockBits(new System.Drawing.Rectangle(0, 0, width, height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            int bytes = Math.Abs(resBmpData.Stride) * height;

            //Byte[] rgbValues = new Byte[bytes];

            //Marshal.Copy(ptr, rgbValues, 0, bytes);
            //Marshal.Copy(rgbValues, 0, resBmpData.Scan0, bytes);
            memcpy(resBmpData.Scan0, ptr, bytes);
            resBmp.UnlockBits(resBmpData);

            //Array.Clear(rgbValues, 0, rgbValues.Length);
            //GC.Collect();

            return resBmp;
        }

        public static Bitmap CloneBitmap(Bitmap targetBitmap, Bitmap sourceBitmap)
        {
            int width = sourceBitmap.Width;
            int height = sourceBitmap.Height;

            Bitmap resBMP;

            if (null == targetBitmap ||
               (null != targetBitmap && targetBitmap.Size != sourceBitmap.Size))
            {
                resBMP = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
            }
            else
            {
                resBMP = targetBitmap;
            }

            System.Drawing.Imaging.BitmapData bmpData = sourceBitmap.LockBits(new System.Drawing.Rectangle(0, 0, width, height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            System.Drawing.Imaging.BitmapData resBmpData = resBMP.LockBits(new System.Drawing.Rectangle(0, 0, width, height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            int bytes = Math.Abs(resBmpData.Stride) * height;
            IntPtr ptr = bmpData.Scan0;

            //Byte[] rgbValues = new Byte[bytes];

            //Marshal.Copy(ptr, rgbValues, 0, bytes);
            //Marshal.Copy(rgbValues, 0, resBmpData.Scan0, bytes);
            memcpy(resBmpData.Scan0, ptr, bytes);
            resBMP.UnlockBits(resBmpData);
            sourceBitmap.UnlockBits(bmpData);

            //Array.Clear(rgbValues, 0, rgbValues.Length);
            //GC.Collect();

            return targetBitmap;
        }

        public static Bitmap CloneBitmap(Bitmap bmp)
        {
            int width = bmp.Width;
            int height = bmp.Height;

            Bitmap resBmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(new System.Drawing.Rectangle(0, 0, width, height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            System.Drawing.Imaging.BitmapData resBmpData = resBmp.LockBits(new System.Drawing.Rectangle(0, 0, width, height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            int bytes = Math.Abs(resBmpData.Stride * height);
            IntPtr ptr = bmpData.Scan0;

            //Byte[] rgbValues = new Byte[bytes];

            //Marshal.Copy(ptr, rgbValues, 0, bytes);
            //Marshal.Copy(rgbValues, 0, resBmpData.Scan0, bytes);
            memcpy(resBmpData.Scan0, ptr, bytes);

            resBmp.UnlockBits(resBmpData);
            bmp.UnlockBits(bmpData);

            //Array.Clear(rgbValues, 0, rgbValues.Length);
            //GC.Collect();

            return resBmp;
        }

        public void MoveToImgPos(Point pt)
        {
            m_PtX = (int)pt.X;
            m_PtY = (int)pt.Y;

            //Move the start position to the origin point
            m_PtX = -1 * (int)(m_PtX);
            m_PtY = -1 * (int)(m_PtY);

            //Move to the center
            m_PtX += (int)(Width / 2 / m_ZoomFactor);
            m_PtY += (int)(Height / 2 / m_ZoomFactor);

            Refresh();
        }

        public void SetmZoomStep(float value)
        {
            m_ZoomStep = value;
        }

        [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        internal static extern IntPtr memcpy(IntPtr dest, IntPtr src, int count);
    }
}
