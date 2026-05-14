using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HalconDotNet;

namespace AlgorithmProcess
{
    public class GenerateFoup
    {
        private readonly ImageConverter _ImageConverter = new ImageConverter();

        private HalconImageConverter HC;

        private HImage _hImage = new HImage();

        private Bitmap FoupImage = null;

        public List<FoupGlassFeature> foupGlassFeaturesList = new List<FoupGlassFeature>();

        public List<FoupGlassFeature> foupGlassFeaturesListBack = new List<FoupGlassFeature>();

        private double foupGlassWidth = 0, foupGlassHeight = 0;

        public bool _IsDrawGlass = false, _IsFoupInitialFinish;

        public GenerateFoup()
        {
            HC = new HalconImageConverter();
        }


        public void ReadFoupImage(string path)
        {
            FoupImage = (Bitmap)_ImageConverter.ConvertFrom(File.ReadAllBytes(path));

            HC.Bitmap2HImage(FoupImage, out _hImage);
        }

        public void FindFishBones(int totalSlot)
        {
            if (FoupImage == null || !_hImage.IsInitialized())
            {
                return;
            }

            HRegion _hThreshRegion = new HRegion(), _hFillRegion = new HRegion(), _hDeferenceRegion = new HRegion();

            HRegion _hOpeningRegion = new HRegion(), _hErosionRegion = new HRegion(), _hConnectionRegion = new HRegion();

            HRegion _hSelectObjectRegion = new HRegion();

            HTuple _hObjectArea = new HTuple(), _hObjectRow = new HTuple(), _hObjectColumn = new HTuple();

            _hThreshRegion = Threshold(_hImage, 0, 50);

            _hFillRegion = _hThreshRegion.FillUp();

            _hDeferenceRegion = _hFillRegion.Difference(_hThreshRegion);

            if (totalSlot == 17)
            {
                _hOpeningRegion = _hDeferenceRegion.OpeningRectangle1(900, 1);
                _hErosionRegion = _hOpeningRegion.ErosionRectangle1(40, 5);
            }
            else if (0 < totalSlot && totalSlot < 17)
            {
                _hOpeningRegion = _hDeferenceRegion.OpeningRectangle1(900, 1);
                _hErosionRegion = _hOpeningRegion.ErosionRectangle1(40, 5);
            }
            else if (17 < totalSlot && totalSlot < 21)
            {
                _hOpeningRegion = _hDeferenceRegion.OpeningRectangle1(900, 1);
                _hErosionRegion = _hOpeningRegion.ErosionRectangle1(40, 5);
            }
            else if (20 < totalSlot && totalSlot < 26)
            {
                _hOpeningRegion = _hDeferenceRegion.OpeningRectangle1(900, 1);
                _hErosionRegion = _hOpeningRegion.ErosionRectangle1(40, 5);
            }


            _hConnectionRegion = _hErosionRegion.Connection();

            int number = _hConnectionRegion.CountObj();

            foupGlassFeaturesList = new List<FoupGlassFeature>();

            foupGlassWidth = 0;
            foupGlassHeight = 0;

            for (int i = 0; i < number - 1; i++)
            {
                _hSelectObjectRegion = _hConnectionRegion.SelectObj(i + 1);

                _hObjectArea = _hSelectObjectRegion.AreaCenter(out _hObjectRow, out _hObjectColumn);

                double width = _hSelectObjectRegion.RegionFeatures("width");
                double height = _hSelectObjectRegion.RegionFeatures("height");

                FoupGlassFeature foupGlassFeature = new FoupGlassFeature();

                double x1 = _hObjectColumn[0] - width / 2, y1 = _hObjectRow[0] - height / 2;
                double x2 = _hObjectColumn[0] + height / 2, y2 = _hObjectRow[0] + height / 2;

                foupGlassFeature._pt = new PointF((float)x1, (float)y1);
                foupGlassFeature.glassStatus = GlassStatus.EmptySlices;

                foupGlassWidth += width;
                foupGlassHeight += height;

                foupGlassFeaturesList.Add(foupGlassFeature);
            }

            foupGlassWidth = foupGlassWidth / (number - 1);
            foupGlassHeight = foupGlassHeight / (number - 1);

            for (int i = 0; i < foupGlassFeaturesList.Count; i++)
            {
                foupGlassFeaturesList[i].width = foupGlassWidth;
                foupGlassFeaturesList[i].height = foupGlassHeight;
            }

            foupGlassFeaturesListBack = new List<FoupGlassFeature>(foupGlassFeaturesList);

            _hThreshRegion.Dispose();
            _hFillRegion.Dispose();
            _hDeferenceRegion.Dispose();
            _hOpeningRegion.Dispose();
            _hErosionRegion.Dispose();
            _hConnectionRegion.Dispose();

            _hSelectObjectRegion.Dispose();

            _hObjectArea.Dispose();
            _hObjectRow.Dispose();
            _hObjectColumn.Dispose();
        }

        public void ResetFoupGlassFeatures()
        {
            foupGlassFeaturesList = new List<FoupGlassFeature>(foupGlassFeaturesListBack);
        }

        public int FindSelectGlass(PointF _pt)
        {
            int index = -1;

            for (int i = 0; i < foupGlassFeaturesList.Count; i++)
            {
                FoupGlassFeature foupGlassFeature = foupGlassFeaturesList[i];

                if (_pt.X > foupGlassFeature._pt.X && _pt.X < (foupGlassFeature._pt.X + foupGlassWidth) && _pt.Y > foupGlassFeature._pt.Y && _pt.Y < (foupGlassFeature._pt.Y + foupGlassHeight))
                {
                    index = i;

                    break;
                }
            }

            return index;
        }

        private HRegion Threshold(HImage _hImg, double minThreshold, double maxThreshold)
        {
            HRegion _hRegion = new HRegion();

            _hRegion = _hImg.Threshold(new HTuple(minThreshold), new HTuple(maxThreshold));

            return _hRegion;
        }

        public Bitmap DrawFoupGlass(int selectIndex, int currentCount = 0, int totalCount = 0)
        {
            Bitmap bmp = (Bitmap)FoupImage.Clone(); //複製

            using (var graphics = Graphics.FromImage(bmp))
            {
                for (int i = 0; i < foupGlassFeaturesList.Count; i++)
                {
                    FoupGlassFeature foupGlassFeature = foupGlassFeaturesList[i];

                    RectangleF GlassRectF = new RectangleF(foupGlassFeature._pt.X, foupGlassFeature._pt.Y, (float)foupGlassWidth, (float)foupGlassHeight);

                    /*  // chengwei [註解畫紅框邏輯]
                    if (_IsDrawGlass)
                    {
                        if (selectIndex != -1 && selectIndex == i && foupGlassFeature.glassStatus != GlassStatus.ObliqueSlice)
                        {
                            Pen p = new Pen(Color.Red, 10);

                            graphics.DrawRectangle(p, Rectangle.Round(GlassRectF));
                        }
                    }
                    */

                    if (foupGlassFeature.glassStatus == GlassStatus.EmptySlices) // 空片
                    {
                        if (_IsFoupInitialFinish)
                        {
                            graphics.FillRectangle(Brushes.White, Rectangle.Round(GlassRectF));
                        }
                    }
                    else if (foupGlassFeature.glassStatus == GlassStatus.NormalSlices) // 正常片
                    {
                        RectangleF GlassRectFNew = GlassRectF;

                        if (_IsDrawGlass)
                        {
                            double gapOffset = Math.Abs(foupGlassFeature.LeftGap - foupGlassFeature.RightGap);

                            if (foupGlassFeature.LeftGap > foupGlassFeature.RightGap && gapOffset >= 0.1) // UI顯示偏移
                            {
                                GlassRectFNew.X = GlassRectF.X + 15;
                            }
                            else if (foupGlassFeature.LeftGap < foupGlassFeature.RightGap && gapOffset >= 0.1)
                            {
                                GlassRectFNew.X = GlassRectF.X - 15;
                            }
                        }

                        graphics.FillRectangle(Brushes.LimeGreen, Rectangle.Round(GlassRectFNew));
                    }
                    else if (foupGlassFeature.glassStatus == GlassStatus.LaminatedSlice) // 疊片
                    {
                        RectangleF GlassRectTop = new RectangleF(foupGlassFeature._pt.X, foupGlassFeature._pt.Y, (float)foupGlassWidth, (float)foupGlassHeight / 2);
                        RectangleF GlassRectBottom = new RectangleF(foupGlassFeature._pt.X, foupGlassFeature._pt.Y + (float)foupGlassHeight / 2, (float)foupGlassWidth, (float)foupGlassHeight / 2);

                        graphics.FillRectangle(Brushes.LightCoral, Rectangle.Round(GlassRectF));

                        graphics.DrawRectangle(Pens.Black, Rectangle.Round(GlassRectTop));
                        graphics.DrawRectangle(Pens.Black, Rectangle.Round(GlassRectBottom));
                    }

                    else if (foupGlassFeature.glassStatus == GlassStatus.ObliqueSlice || foupGlassFeature.glassStatus == GlassStatus.Unknow) // 斜插片, Unknow
                    {
                        // 計數完成時才畫
                        if (currentCount != totalCount)
                        {
                            continue;
                        }

                        if (totalCount == 0)
                        {
                            continue;
                        }

                        if (i >= foupGlassFeaturesList.Count - 1)
                        {
                            break;
                        }

                        if (foupGlassFeaturesList[i].glassStatus == GlassStatus.Unknow && foupGlassFeaturesList[i + 1].glassStatus == GlassStatus.ObliqueSlice)
                        {
                            foupGlassFeaturesList[i].glassStatus = foupGlassFeaturesList[i + 1].glassStatus;
                            foupGlassFeaturesList[i].CountSliceL = foupGlassFeaturesList[i + 1].CountSliceL;
                            foupGlassFeaturesList[i].CountSliceR = foupGlassFeaturesList[i + 1].CountSliceR;

                            foupGlassFeature = foupGlassFeaturesList[i];
                        }
                        else if (foupGlassFeaturesList[i].glassStatus == GlassStatus.ObliqueSlice && foupGlassFeaturesList[i + 1].glassStatus == GlassStatus.Unknow)
                        {
                            foupGlassFeaturesList[i + 1].glassStatus = foupGlassFeaturesList[i].glassStatus;
                            foupGlassFeaturesList[i + 1].CountSliceL = foupGlassFeaturesList[i].CountSliceL;
                            foupGlassFeaturesList[i + 1].CountSliceR = foupGlassFeaturesList[i].CountSliceR;
                        }

                        FoupGlassFeature nextfoupGlassFeature = foupGlassFeaturesList[i + 1];

                        PointF _ptL = PointF.Empty, _ptR = PointF.Empty;

                        if (foupGlassFeature.CountSliceL > foupGlassFeature.CountSliceR)
                        {
                            _ptL.X = foupGlassFeature._pt.X;
                            _ptL.Y = foupGlassFeature._pt.Y + (float)foupGlassHeight / 2;

                            _ptR.X = nextfoupGlassFeature._pt.X + (float)foupGlassWidth;
                            _ptR.Y = nextfoupGlassFeature._pt.Y + (float)foupGlassHeight / 2;
                        }
                        else
                        {
                            _ptL.X = nextfoupGlassFeature._pt.X;
                            _ptL.Y = nextfoupGlassFeature._pt.Y + (float)foupGlassHeight / 2;

                            _ptR.X = foupGlassFeature._pt.X + (float)foupGlassWidth;
                            _ptR.Y = foupGlassFeature._pt.Y + (float)foupGlassHeight / 2;
                        }

                        double degree = CalculateDegree(_ptL, _ptR);

                        PointF Center = new PointF((_ptL.X + _ptR.X) / 2, (_ptL.Y + _ptR.Y) / 2);
                        PointF Right = new PointF(Center.X + (float)foupGlassWidth / 2, Center.Y);
                        PointF RightBottom = new PointF(Center.X + (float)foupGlassWidth / 2, Center.Y + (float)foupGlassHeight / 2);

                        Point _FirstClickPt = Point.Round(RotatePoint(Center, Center, (float)degree));
                        Point _SecondClickPt = Point.Round(RotatePoint(Right, Center, (float)degree));
                        Point _ThirdClickPt = Point.Round(RotatePoint(RightBottom, Center, (float)degree));

                        var vectorRect = new VectorRectangle(_FirstClickPt, _SecondClickPt, _ThirdClickPt);

                        /*  // chengwei [先註解畫紅框邏輯]
                        if (_IsDrawGlass)
                        {
                            if (selectIndex != -1 && (selectIndex == i || selectIndex == (i - 1)))
                            {
                                Pen p = new Pen(Color.Red, 1);

                                graphics.DrawVectorRectangle(vectorRect, p, Brushes.Red);
                            }
                            else
                            {
                                graphics.DrawVectorRectangle(vectorRect, Pens.White, Brushes.Red);
                            }
                        }
                        */

                        if (i == foupGlassFeaturesList.Count - 1 || nextfoupGlassFeature.glassStatus != GlassStatus.ObliqueSlice)
                        {
                            graphics.FillRectangle(Brushes.Yellow, Rectangle.Round(GlassRectF));    //畫 Unknow
                        }
                        else
                        {
                            graphics.DrawVectorRectangle(vectorRect, Pens.White, Brushes.Red);    //畫 斜插片

                            i++;
                        }
                    }
                }
            }

            return bmp;
        }


        private double CalculateDegree(PointF _ptL, PointF _ptR)
        {
            float x1 = _ptL.X, y1 = _ptL.Y;
            float x2 = _ptR.X, y2 = _ptR.Y;

            double dx = x2 - x1;
            double dy = y2 - y1;

            double radians = Math.Atan2(dy, dx);

            double degrees = radians * (180.0 / Math.PI);

            return degrees;
        }

        public PointF RotatePoint(PointF point, PointF center, float angleDegrees)
        {
            // 角度轉弧度
            double angle = angleDegrees * Math.PI / 180.0;

            double cosA = Math.Cos(angle);
            double sinA = Math.Sin(angle);

            // 平移至原點
            double dx = point.X - center.X;
            double dy = point.Y - center.Y;

            // 旋轉
            double xNew = dx * cosA - dy * sinA;
            double yNew = dx * sinA + dy * cosA;

            // 平移回中心
            return new PointF((float)(xNew + center.X), (float)(yNew + center.Y));
        }
    }
}
