using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace AlgorithmProcess
{
    [Serializable]
    public class VectorRectangle
    {
        /// <summary>
        /// 旋轉矩形建構式
        /// </summary>
        /// <param name="vectorStartPt">向量起點</param>
        /// <param name="vectorEndPt">向量終點</param>
        /// <param name="vectorWidthEdgePt">向量線寬邊點</param>
        public VectorRectangle(PointF vectorStartPt, PointF vectorEndPt, PointF vectorWidthEdgePt)
        {
            //取得角度
            var angle = GetAngleFromTwoPoint(vectorStartPt, vectorEndPt);

            //取得長寬
            var len1 = GetDistance(vectorStartPt, vectorEndPt);
            var len2 = DistanceForPointToABLine(vectorWidthEdgePt, vectorStartPt, vectorEndPt);

            //設定屬性
            Row = vectorStartPt.Y;
            Column = vectorStartPt.X;
            Length1 = len1;
            Length2 = len2;
            Phi = angle * -1;

            VectorStartPt = vectorStartPt;
            VectorEndPt = vectorEndPt;
            VectorWidthEdgePt = vectorWidthEdgePt;
        }

        /// <summary>
        /// 向量起點
        /// </summary>
        public PointF VectorStartPt { get; set; }

        /// <summary>
        /// 向量終點
        /// </summary>
        public PointF VectorEndPt { get; set; }

        /// <summary>
        /// 向量線寬邊點
        /// </summary>
        public PointF VectorWidthEdgePt { get; set; }

        /// <summary>
        /// 矩形中心Row
        /// </summary>
        public float Row { get; set; }

        /// <summary>
        /// 矩形中心Column
        /// </summary>
        public float Column { get; set; }

        /// <summary>
        /// 旋轉角度
        /// </summary>
        public float Phi { get; set; }

        /// <summary>
        /// 向量長度
        /// </summary>
        public float Length1 { get; set; }

        /// <summary>
        /// 法向量長度
        /// </summary>
        public float Length2 { get; set; }

        /// <summary>
        /// 點到線距離
        /// </summary>
        /// <param name="targetPt">目標點</param>
        /// <param name="linePtA">線段A端點</param>
        /// <param name="linePtB">線段B端點</param>
        /// <returns>距離</returns>
        private static float DistanceForPointToABLine(PointF targetPt, PointF linePtA, PointF linePtB)//所在點到AB線段的垂線長度
        {
            float x = targetPt.X;
            float y = targetPt.Y;
            float x1 = linePtA.X;
            float y1 = linePtA.Y;
            float x2 = linePtB.X;
            float y2 = linePtB.Y;

            float reVal = 0f;
            bool retData = false;

            float cross = (x2 - x1) * (x - x1) + (y2 - y1) * (y - y1);
            if (cross <= 0)
            {
                reVal = (float)Math.Sqrt((x - x1) * (x - x1) + (y - y1) * (y - y1));
                retData = true;
            }

            float d2 = (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);
            if (cross >= d2)
            {
                reVal = (float)Math.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));
                retData = true;
            }

            if (!retData)
            {
                float r = cross / d2;
                float px = x1 + (x2 - x1) * r;
                float py = y1 + (y2 - y1) * r;
                reVal = (float)Math.Sqrt((x - px) * (x - px) + (py - y) * (py - y));
            }

            return reVal;

        }

        private float GetAngleFromTwoPoint(PointF vectorStartPt, PointF vectorEndPt)
        {
            return (float)Math.Atan2((vectorEndPt.Y - vectorStartPt.Y), (vectorEndPt.X - vectorStartPt.X)) * 180.0f / (float)Math.PI;
        }

        public static float GetDistance(PointF p1, PointF p2)
        {
            var dist = (float)Math.Sqrt(Math.Pow((double)(p2.X - p1.X), 2) + (double)Math.Pow((p2.Y - p1.Y), 2));
            return dist;
        }

        /// <summary>
        /// 轉換成Halcon的HRegion物件
        /// </summary>
        /// <returns>輸出的物件</returns>
        public HRegion ConvertToRegion()
        {
            HRegion rect2 = new HRegion();

            HTuple rad;
            HOperatorSet.TupleRad(new HTuple(Phi), out rad);
            rect2.GenRectangle2((double)Row, Column, rad.D, Length1, Length2);

            //rect2.WriteObject("D:\\TempRect.hobj");

            return rect2;
        }
    }

    public static class GrahpicsExtension
    {
        public static void DrawVectorRectangle(this Graphics g, VectorRectangle r, Pen pen, Brush b)
        {
            var rect = new RectangleF();

            rect.Width = r.Length1 * 2;
            rect.Height = r.Length2 * 2;

            rect.Width = 20 > rect.Width ? 20 : rect.Width;
            rect.Height = 20 > rect.Height ? 20 : rect.Height;

            rect.X = r.Column - rect.Width / 2;
            rect.Y = r.Row - rect.Height / 2;

            DrawRotatedRectangle(g, rect, r.Phi * -1, pen, b);
        }

        private static void DrawRotatedRectangle(this Graphics g, RectangleF r, float angle, Pen pen, Brush b)
        {
            using (Matrix m = new Matrix())
            {
                m.RotateAt(angle, new PointF(r.Left + (r.Width / 2),
                                          r.Top + (r.Height / 2)));
                g.Transform = m;

                if (null != b)
                    g.FillRectangle(b, Rectangle.Round(r));

                if (null != pen)
                    g.DrawRectangle(pen, Rectangle.Round(r));

                g.ResetTransform();
            }
        }
    }
}
