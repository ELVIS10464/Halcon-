using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProcess
{
    internal class MappingModule
    {
        private HalconImageConverter HC = null;

        public string mode = "auto ";

        // =============================== 事件與委託 ===============================

        public delegate void CallBackReturnAlgorithmResult(string type, string msg);
        public event CallBackReturnAlgorithmResult CallBackAlgorithmResult;

        // =============================== 事件與委託 ===============================

        public MappingModule()
        {
            HC = new HalconImageConverter();
        }
        public MappingModule Clone()
        {
            return (MappingModule)this.MemberwiseClone();
        }
        public void SetMode(string mode)
        {
            this.mode = mode;
        }

        // ====================== 計算片數(判斷有片、無片、斜插) ======================
        public int CountSlice_TwoCamera(HImage _hImageLeft, HImage _hImageRight, ROIList rOIList, MappingParameter mappingParameter, out int RCount, out int Result, out List<DebugResult> CountSliceResultList)
        {
            HImage _hImageL = new HImage(), _hImageR = new HImage(), _hImagePart1 = new HImage(), _hImagePart3 = new HImage();

            HImage _hImageEdgeL = new HImage(), _hImageEdgeR = new HImage();

            HTuple _hImageWidth = new HTuple(), _hImageHeight = new HTuple();

            HTuple _hAll_NearRow = new HTuple(), _hRange = new HTuple();

            HTuple _hL_Min_Row = new HTuple(), _hL_Max_Row = new HTuple(), _hR_Min_Row = new HTuple(), _hR_Max_Row = new HTuple();

            HTuple _hAbs_L = new HTuple(), _hAbs_R = new HTuple();

            HTuple _hL_N_m_AF = new HTuple(), _hR_N_m_AF = new HTuple();

            _hImageL = _hImageLeft.CopyImage();
            _hImageR = _hImageRight.CopyImage();

            _hImageL.GetImageSize(out _hImageWidth, out _hImageHeight);

            double w = _hImageWidth[0], h = _hImageHeight[0];

            // ========================= 返回參數 =========================

            int LCount = 0;

            RCount = 0;

            Result = 0;

            CountSliceResultList = new List<DebugResult>();

            List<DebugResult> FindPanelCountResultList = new List<DebugResult>();
            // ========================= 返回參數 =========================

            double CutPoint1 = mappingParameter.TwoCamera.CountSlice.CutPoint1;
            double CutPoint2 = mappingParameter.TwoCamera.CountSlice.CutPoint2;


            // ====================== 左側相機 ROI ======================
            ROI leftCameraROI = new ROI();

            leftCameraROI.R1 = rOIList.TwoCamera.CountSlice.LeftImage_Panel.R1;
            leftCameraROI.C1 = rOIList.TwoCamera.CountSlice.LeftImage_Panel.C1;
            leftCameraROI.R2 = rOIList.TwoCamera.CountSlice.LeftImage_Panel.R2;
            leftCameraROI.C2 = rOIList.TwoCamera.CountSlice.LeftImage_Panel.C2;
            // ====================== 左側相機 ROI ======================


            // ====================== 右側相機 ROI ======================
            ROI rightCameraROI = new ROI();

            rightCameraROI.R1 = rOIList.TwoCamera.CountSlice.RightImage_Panel.R1;
            rightCameraROI.C1 = rOIList.TwoCamera.CountSlice.RightImage_Panel.C1;
            rightCameraROI.R2 = rOIList.TwoCamera.CountSlice.RightImage_Panel.R2;
            rightCameraROI.C2 = rOIList.TwoCamera.CountSlice.RightImage_Panel.C2;
            // ====================== 右側相機 ROI ======================           

            double LineNext = 20;

            double LineCount = 0;

            // ====================== 左側影像計算 ======================

            double L_Panel_Col1 = leftCameraROI.C1;
            double L_Panel_Col2 = leftCameraROI.C2;
            double L_Panel_Row1 = leftCameraROI.R1;
            double L_Panel_Row2 = leftCameraROI.R2;

            LineNext = 20;
            LineCount = (int)((L_Panel_Col2 - L_Panel_Col1) / LineNext - 1);

            // youyi [_hImagePart2_ML 改為 _hImagePart1]
            _hImagePart1 = _hImageL.CropPart(L_Panel_Row1, L_Panel_Col1, L_Panel_Col2 - L_Panel_Col1, L_Panel_Row2 - L_Panel_Row1);

            int Count_L = FindPanelCount_TwoCamera(_hImagePart1, mappingParameter.TwoCamera.CountSlice.L_Panel_threshold_value, 1, 1, L_Panel_Row2 - L_Panel_Row1, LineCount, LineNext, mappingParameter, out HTuple _hL_AllRow, out HTuple _hL_AllCol, out HTuple _hL_NearRow, out HTuple _hL_NearCol, out HTuple _hL_N_m, out HTuple _hL_N_b, out HTuple _hL_N_A, out HTuple _hL_N_B, out HTuple _hL_N_C, out FindPanelCountResultList);

            _hL_AllRow = _hL_AllRow + L_Panel_Row1;
            _hL_AllCol = _hL_AllCol + L_Panel_Col1;

            // youyi
            if (mode == "teach")
            {
                Bitmap bmp = null;
                DebugResult debugResult = null;

                ROI roi = new ROI
                {
                    R1 = (int)L_Panel_Row1,
                    C1 = (int)L_Panel_Col1,
                    R2 = (int)L_Panel_Row2,
                    C2 = (int)L_Panel_Col2
                };
                // youyi
                bmp = ShowImagePoint(_hImageL, null, null, null, null, null, null, roi);

                string Type = "Inspect Result";
                string Name = "Calculate of the left side of the Middle image";
                string Description = "Check if a panel exists on the left side of the Middle image of the current slot layer.";

                debugResult = GenerateDebugResult(bmp, Type, Name, Description, true);

                CountSliceResultList.Add(debugResult);

                CountSliceResultList.AddRange(FindPanelCountResultList);
                // youyi
                bmp = ShowImagePoint(_hImageL, _hL_AllRow, _hL_AllCol, null, null, null, null, roi);

                List<List<Point>> PanelPointList = AddPointsFromHTuple(_hL_AllRow, _hL_AllCol);

                List<Equation> EquationList = AddEquation(_hL_N_m, _hL_N_b);

                debugResult = GenerateDebugResult(bmp, Type, Name, Description, false, PanelPointList, null, EquationList, Count_L);

                CountSliceResultList.Add(debugResult);
            }

            int Count_L_Threshold = FindPanelThreshold(_hImagePart1, mappingParameter, out FindPanelCountResultList);


            if (Count_L_Threshold == 0)
            {
                Count_L = 0;
            }

            if (mode == "teach")
            {
                CountSliceResultList.AddRange(FindPanelCountResultList);
            }


            //20260508 youyi test
            double L_Edge_Panel_Col1 = rOIList.TwoCamera.CountSlice.LeftImage_EdgePanel.C1;
            double L_Edge_Panel_Col2 = rOIList.TwoCamera.CountSlice.LeftImage_EdgePanel.C2;
            double L_Edge_Panel_Row1 = rOIList.TwoCamera.CountSlice.LeftImage_EdgePanel.R1;
            double L_Edge_Panel_Row2 = rOIList.TwoCamera.CountSlice.LeftImage_EdgePanel.R2;
            string Direction = "L";

            _hImageEdgeL = _hImageL.CropPart(L_Edge_Panel_Row1, L_Edge_Panel_Col1, L_Edge_Panel_Col2 - L_Edge_Panel_Col1, L_Edge_Panel_Row2 - L_Edge_Panel_Row1);


            double LeftEdge_ThresholdMin = mappingParameter.TwoCamera.CountSlice.LeftEdge_ThresholdMin;
            double LeftEdge_ThresholdMax = mappingParameter.TwoCamera.CountSlice.LeftEdge_ThresholdMax;
            double LeftEdge_Area = mappingParameter.TwoCamera.CountSlice.LeftEdge_Area;
            double LeftEdge_AreaRowDistance = mappingParameter.TwoCamera.CountSlice.LeftEdge_AreaRowDistance;
            int Count_L_Edge = FindPanelEdgeCount_TwoCamera(_hImageEdgeL, LeftEdge_ThresholdMin, LeftEdge_ThresholdMax, LeftEdge_Area, LeftEdge_AreaRowDistance, out FindPanelCountResultList);

            if (Count_L_Edge == 0)
            {
                Count_L = 0;
            }
            else
            {
                Count_L = 1;
            }

            if (mode == "teach")
            {
                Bitmap bmp = null;
                DebugResult debugResult = null;

                ROI roi = new ROI
                {
                    R1 = (int)L_Edge_Panel_Row1,
                    C1 = (int)L_Edge_Panel_Col1,
                    R2 = (int)L_Edge_Panel_Row2,
                    C2 = (int)L_Edge_Panel_Col2
                };
                // youyi
                bmp = ShowImagePoint(_hImageL, null, null, null, null, null, null, roi);

                string Type = "Inspect Result";
                string Name = "Calculate of the left Edge side of the Middle image";
                string Description = "Check if a panel exists on the left Edge side .";

                debugResult = GenerateDebugResult(bmp, Type, Name, Description, true);

                CountSliceResultList.Add(debugResult);

                CountSliceResultList.AddRange(FindPanelCountResultList);
            }


            // ====================== 左側影像計算 ======================


            // ====================== 右側影像計算 ======================

            // youyi 調整 ROI 對應名稱，原本 [MR_Panel_Col1 = middleRightROI]，調整為 [MR_Panel_Col1 = rightCameraROI]
            double R_Panel_Col1 = rightCameraROI.C1;
            double R_Panel_Col2 = rightCameraROI.C2;
            double R_Panel_Row1 = rightCameraROI.R1;
            double R_Panel_Row2 = rightCameraROI.R2;

            LineNext = 20;
            LineCount = (int)((R_Panel_Col2 - R_Panel_Col1) / LineNext - 1);

            // youyi [_hImagePart2_MR 改為 _hImagePart3]
            _hImagePart3 = _hImageR.CropPart(R_Panel_Row1, R_Panel_Col1, R_Panel_Col2 - R_Panel_Col1, R_Panel_Row2 - R_Panel_Row1);

            int Count_R = FindPanelCount_TwoCamera(_hImagePart3, mappingParameter.TwoCamera.CountSlice.R_Panel_threshold_value, 1, 1, R_Panel_Row2 - R_Panel_Row1, LineCount, LineNext, mappingParameter, out HTuple _hR_AllRow, out HTuple _hR_AllCol, out HTuple _hR_NearRow, out HTuple _hR_NearCol, out HTuple _hR_N_m, out HTuple _hR_N_b, out HTuple _hR_N_A, out HTuple _hR_N_B, out HTuple _hR_N_C, out FindPanelCountResultList);

            _hR_AllRow = _hR_AllRow + R_Panel_Row1;
            _hR_AllCol = _hR_AllCol + R_Panel_Col1;

            // youyi
            if (mode == "teach")
            {
                Bitmap bmp = null;
                DebugResult debugResult = null;

                ROI roi = new ROI
                {
                    R1 = (int)R_Panel_Row1,
                    C1 = (int)R_Panel_Col1,
                    R2 = (int)R_Panel_Row2,
                    C2 = (int)R_Panel_Col2
                };
                // youyi
                bmp = ShowImagePoint(_hImageR, null, null, null, null, null, null, roi);

                string Type = "Inspect Result";
                string Name = "Calculate of the Right side";
                string Description = "Check if a panel exists on the left side of the Middle image of the current slot layer.";

                debugResult = GenerateDebugResult(bmp, Type, Name, Description, true);

                CountSliceResultList.Add(debugResult);

                CountSliceResultList.AddRange(FindPanelCountResultList);
                // youyi
                bmp = ShowImagePoint(_hImageR, _hR_AllRow, _hR_AllCol, null, null, null, null, roi);

                List<List<Point>> PanelPointList = AddPointsFromHTuple(_hR_AllRow, _hR_AllCol);

                List<Equation> EquationList = AddEquation(_hR_N_m, _hR_N_b);

                debugResult = GenerateDebugResult(bmp, Type, Name, Description, false, PanelPointList, null, EquationList, Count_R);

                CountSliceResultList.Add(debugResult);
            }

            int Count_R_Threshold = FindPanelThreshold(_hImagePart3, mappingParameter, out FindPanelCountResultList);

            if (Count_R_Threshold == 0)
            {
                Count_R = 0;
            }

            if (mode == "teach")
            {
                CountSliceResultList.AddRange(FindPanelCountResultList);
            }


            //20260508 youyi test
            double R_Edge_Panel_Col1 = rOIList.TwoCamera.CountSlice.RightImage_EdgePanel.C1;
            double R_Edge_Panel_Col2 = rOIList.TwoCamera.CountSlice.RightImage_EdgePanel.C2;
            double R_Edge_Panel_Row1 = rOIList.TwoCamera.CountSlice.RightImage_EdgePanel.R1;
            double R_Edge_Panel_Row2 = rOIList.TwoCamera.CountSlice.RightImage_EdgePanel.R2;
            Direction = "R";

            _hImageEdgeR = _hImageR.CropPart(R_Edge_Panel_Row1, R_Edge_Panel_Col1, R_Edge_Panel_Col2 - R_Edge_Panel_Col1, R_Edge_Panel_Row2 - R_Edge_Panel_Row1);

            double RightEdge_ThresholdMin = mappingParameter.TwoCamera.CountSlice.RightEdge_ThresholdMin;
            double RightEdge_ThresholdMax = mappingParameter.TwoCamera.CountSlice.RightEdge_ThresholdMax;
            double RightEdge_Area = mappingParameter.TwoCamera.CountSlice.RightEdge_Area;
            double RightEdge_AreaRowDistance = mappingParameter.TwoCamera.CountSlice.RightEdge_AreaRowDistance;
            int Count_R_Edge = FindPanelEdgeCount_TwoCamera(_hImageEdgeR, RightEdge_ThresholdMin, RightEdge_ThresholdMax, RightEdge_Area, RightEdge_AreaRowDistance, out FindPanelCountResultList);

            if (Count_R_Edge == 0)
            {
                Count_R = 0;
            }
            else
            {
                Count_R = 1;
            }

            if (mode == "teach")
            {
                Bitmap bmp = null;
                DebugResult debugResult = null;

                ROI roi = new ROI
                {
                    R1 = (int)R_Edge_Panel_Row1,
                    C1 = (int)R_Edge_Panel_Col1,
                    R2 = (int)R_Edge_Panel_Row2,
                    C2 = (int)R_Edge_Panel_Col2
                };
                // youyi
                bmp = ShowImagePoint(_hImageR, null, null, null, null, null, null, roi);

                string Type = "Inspect Result";
                string Name = "Calculate of the Right Edge side";
                string Description = "Check if a panel exists on the Right Edge side.";

                debugResult = GenerateDebugResult(bmp, Type, Name, Description, true);

                CountSliceResultList.Add(debugResult);

                CountSliceResultList.AddRange(FindPanelCountResultList);
            }


            // ====================== 右側影像計算 ======================

            bool _IsSlant = false;

            int Count_L_AF = 0, Count_R_AF = 0;            

            LCount = Count_L;
            RCount = Count_R;           

            if (LCount == 0 && RCount == 0)
            {
                Result = 0;
            }
            else if (LCount == -1 && RCount == -1)  // chengwei [新增 Unknow 狀態]
            {
                Result = -1;
            }
            else if (LCount == 1 && RCount == 1)
            {
                Result = 1;
            }
            else if ((LCount == 0 && RCount == 1) || (LCount == 1 && RCount == 0))
            {
                Result = 2;
            }

            _hImageL.Dispose();
            _hImageR.Dispose();
            _hImagePart1.Dispose();
            _hImagePart3.Dispose();

            _hImageWidth.Dispose();
            _hImageHeight.Dispose();

            _hAll_NearRow.Dispose();
            _hRange.Dispose();

            _hL_Min_Row.Dispose();
            _hL_Max_Row.Dispose();
            _hR_Min_Row.Dispose();
            _hR_Max_Row.Dispose();

            _hAbs_L.Dispose();
            _hAbs_R.Dispose();

            _hL_AllRow.Dispose();
            _hL_AllCol.Dispose();
            _hL_NearRow.Dispose();
            _hL_NearCol.Dispose();
            _hL_N_m.Dispose();
            _hL_N_b.Dispose();
            _hL_N_A.Dispose();
            _hL_N_B.Dispose();
            _hL_N_C.Dispose();

            _hR_AllRow.Dispose();
            _hR_AllCol.Dispose();
            _hR_NearRow.Dispose();
            _hR_NearCol.Dispose();
            _hR_N_m.Dispose();
            _hR_N_b.Dispose();
            _hR_N_A.Dispose();
            _hR_N_B.Dispose();
            _hR_N_C.Dispose();

            _hL_Min_Row.Dispose();
            _hL_Max_Row.Dispose();
            _hR_Min_Row.Dispose();
            _hR_Max_Row.Dispose();

            _hAbs_L.Dispose();
            _hAbs_R.Dispose();

            _hL_N_m_AF.Dispose();
            _hR_N_m_AF.Dispose();

            return LCount;
        }

        private int FindPanelCount_TwoCamera(HImage _hMiddleImage, double Panel_threshold_value, double Panel_Col1, double PanelRowStart, double PanelRowEnd, double LineCount, double LineNext, MappingParameter mappingParameter, out HTuple _hReturn_AllRow, out HTuple _hReturn_AllCol, out HTuple _hReturn_NearRow, out HTuple _hReturn_NearCol, out HTuple _hReturn_N_m, out HTuple _hReturn_N_b, out HTuple _hReturn_N_A, out HTuple _hReturn_N_B, out HTuple _hReturn_N_C, out List<DebugResult> FindPanelCountResultList)
        {
            HImage _hGrayImge = new HImage(), _hHighpass = new HImage();

            HTuple _hImageWidth = new HTuple(), _hImageHeight = new HTuple();

            HTuple _hAllRow = new HTuple(), _hAllCol = new HTuple(), _hAllMaxRow = new HTuple();

            HTuple _hNear_Row = new HTuple(), _hNear_Col = new HTuple(), _hN_m = new HTuple(), _hN_b = new HTuple(), _hN_A = new HTuple(), _hN_B = new HTuple(), _hN_C = new HTuple();

            HTuple _hDis = new HTuple(), _hDisIndices = new HTuple();

            // ========================= 返回參數 =========================

            _hReturn_AllRow = new HTuple();
            _hReturn_AllCol = new HTuple();
            _hReturn_NearRow = new HTuple();
            _hReturn_NearCol = new HTuple();

            _hReturn_N_m = new HTuple();
            _hReturn_N_b = new HTuple();
            _hReturn_N_A = new HTuple();
            _hReturn_N_B = new HTuple();
            _hReturn_N_C = new HTuple();

            FindPanelCountResultList = new List<DebugResult>();

            // ========================= 返回參數 =========================

            int Count = 0;

            _hGrayImge = _hMiddleImage.Rgb1ToGray();

            _hGrayImge.GetImageSize(out _hImageWidth, out _hImageHeight);

            _hHighpass = _hGrayImge.HighpassImage(_hImageWidth, 1);

            _hGrayImge = _hGrayImge.SubImage(_hHighpass, new HTuple(1), new HTuple(128));

            if (mode == "teach")
            {
                Bitmap bmp = ShowImagePoint(_hGrayImge);

                string Type = "Inspect Result";
                string Name = "PanelMeasurePoints";
                string Description = "Image Process";

                DebugResult debugResult = GenerateDebugResult(bmp, Type, Name, Description, false);

                FindPanelCountResultList.Add(debugResult);
            }

            double MinAmplitudeThreshold = mappingParameter.TwoCamera.CountSlice.MinAmplitudeThreshold;

            double AmplitudeThreshold = Panel_threshold_value;

            double LeastPoint = LineCount / 3 * 2;

            double LineLengthThreshold = LineCount * Math.Abs(LineNext) * 0.7;

            bool isFindLine = false;

            double Panel_Col2 = Panel_Col1 + LineCount * LineNext;

            int FindCount = (int)((AmplitudeThreshold - MinAmplitudeThreshold) / 3);

            for (int i = 0; i < FindCount; i++)
            {
                _hAllRow = PanelMeasurePoints(_hGrayImge, PanelRowStart, PanelRowEnd, Panel_Col1, LineCount, LineNext, AmplitudeThreshold, out _hAllCol);

                if (mode == "teach")
                {
                    Bitmap bmp = ShowImagePoint(_hMiddleImage, _hAllRow, _hAllCol);

                    string Type = "Inspect Result";
                    string Name = "PanelMeasurePoints";
                    string Description = "Panel Measure Pos - All position search results (Attempt " + (i + 1).ToString() + ")";
                    List<List<Point>> PanelPointList = AddPointsFromHTuple(_hAllRow, _hAllCol);

                    DebugResult debugResult = GenerateDebugResult(bmp, Type, Name, Description, false, PanelPointList);

                    FindPanelCountResultList.Add(debugResult);
                }

                _hAllMaxRow = Find_MaxRow(_hGrayImge, _hAllRow, _hAllCol);

                isFindLine = Find_PanelLine_TwoCamera(_hGrayImge, _hAllMaxRow, _hAllRow, _hAllCol, Panel_Col1, LeastPoint, LineLengthThreshold, out _hNear_Row, out _hNear_Col, out _hN_m, out _hN_b, out _hN_A, out _hN_B, out _hN_C);

                if (mode == "teach")
                {
                    Bitmap bmp = ShowImagePoint(_hMiddleImage, _hNear_Row, _hNear_Col);

                    string Type = "Inspect Result";
                    string Name = "Find_PanelLine";
                    string Description = "Panel Nearby Point - Search Results (Attempt " + (i + 1).ToString() + ")";
                    List<List<Point>> PanelPointList = AddPointsFromHTuple(_hNear_Row, _hNear_Col);

                    DebugResult debugResult = GenerateDebugResult(bmp, Type, Name, Description, false, PanelPointList);

                    FindPanelCountResultList.Add(debugResult);
                }

                if (isFindLine)
                {
                    _hDis = (_hN_A * _hNear_Col + _hN_B * _hNear_Row + _hN_C).TupleAbs() / (_hN_A * _hN_A + _hN_B * _hN_B).TupleSqrt();

                    _hDisIndices = FindOutlier(_hDis);

                    _hNear_Row = _hNear_Row.TupleRemove(_hDisIndices);
                    _hNear_Col = _hNear_Col.TupleRemove(_hDisIndices);

                    if (mode == "teach")
                    {
                        Bitmap bmp = ShowImagePoint(_hMiddleImage, _hNear_Row, _hNear_Col);

                        string Type = "Inspect Result";
                        string Name = "Remove Outliers";
                        string Description = "Panel nearby point search results after removing outliers";
                        List<List<Point>> PanelPointList = AddPointsFromHTuple(_hNear_Row, _hNear_Col);

                        DebugResult debugResult = GenerateDebugResult(bmp, Type, Name, Description, false, PanelPointList);

                        FindPanelCountResultList.Add(debugResult);
                    }

                    Count = 1;

                    break;
                }

                AmplitudeThreshold = AmplitudeThreshold - 3;
            }

            if (Count == 1)
            {
                _hReturn_AllRow = _hAllRow;
                _hReturn_AllCol = _hAllCol;
                _hReturn_NearRow = _hNear_Row;
                _hReturn_NearCol = _hNear_Col;
                _hReturn_N_m = _hN_m;
                _hReturn_N_b = _hN_b;
                _hReturn_N_A = _hN_A;
                _hReturn_N_B = _hN_B;
                _hReturn_N_C = _hN_C;
            }

            _hGrayImge.Dispose();

            _hImageWidth.Dispose();
            _hImageHeight.Dispose();
            _hAllRow.Dispose();
            _hAllCol.Dispose();
            _hAllMaxRow.Dispose();
            _hNear_Row.Dispose();
            _hNear_Col.Dispose();
            _hN_m.Dispose();
            _hN_b.Dispose();
            _hN_A.Dispose();
            _hN_B.Dispose();
            _hN_C.Dispose();
            _hDis.Dispose();
            _hDisIndices.Dispose();

            return Count;
        }

        private int FindPanelEdgeCount_TwoCamera(HImage _hImage, double Edge_ThresholdMin, double Edge_ThresholdMax, double Edge_Area, double Edge_AreaRowDistance, out List<DebugResult> FindPanelCountResultList)
        {
            HImage _hImg = new HImage(), _hGrayImage = new HImage(), _hSelectedRegionImage = new HImage();

            HRegion _hRegion = new HRegion(), _hConnectedRegions = new HRegion(), _hSelectedRegions = new HRegion();

            HTuple _hImageWidth = new HTuple(), _hImageHeight = new HTuple();

            HTuple _hArea1 = new HTuple(), _hRow1 = new HTuple(), _hCol1 = new HTuple(), _hDistance = new HTuple();

            // ========================= 返回參數 =========================

            int Count = 0;

            FindPanelCountResultList = new List<DebugResult>();

            // ========================= 返回參數 =========================

            _hImg = _hImage.CopyImage();

            _hGrayImage = _hImg.Rgb1ToGray();

            _hGrayImage.GetImageSize(out _hImageWidth, out _hImageHeight);

            _hRegion = _hGrayImage.Threshold(new HTuple(Edge_ThresholdMin), new HTuple(Edge_ThresholdMax));

            _hConnectedRegions = _hRegion.Connection();

            _hArea1 = _hConnectedRegions.AreaCenter(out _hRow1, out _hCol1);

            _hSelectedRegions = _hConnectedRegions.SelectShape(new HTuple("area"), "and", new HTuple(_hArea1.TupleMax()), new HTuple("max"));

            _hImg.OverpaintRegion(_hSelectedRegions, new HTuple(255, 0, 0), "fill");

            if (mode == "teach")
            {
                Bitmap bmp = ShowImagePoint(_hImage);

                string Type = "Inspect Result";
                string Name = "Image";
                string Description = "Image";

                DebugResult debugResult = GenerateDebugResult(bmp, Type, Name, Description, false);

                FindPanelCountResultList.Add(debugResult);
            }

            if (mode == "teach")
            {
                Bitmap bmp = ShowImagePoint(_hImg);

                string Type = "Inspect Result";
                string Name = "Threshold";
                string Description = "Threshold";

                DebugResult debugResult = GenerateDebugResult(bmp, Type, Name, Description, false);

                FindPanelCountResultList.Add(debugResult);
            }

            if (_hArea1.TupleMax() > Edge_Area)
            {
                Count = 1;
            }
            else
            {
                Count = 0;
            }

            _hArea1 = _hSelectedRegions.AreaCenter(out _hRow1, out _hCol1);

            _hDistance = (_hRow1 - (_hImageHeight / 2)).TupleAbs();
            if (_hDistance > Edge_AreaRowDistance)
            {
                Count = 0;
            }

            _hImg.Dispose();
            _hGrayImage.Dispose();

            _hRegion.Dispose();
            _hConnectedRegions.Dispose();
            _hSelectedRegions.Dispose();

            _hImageWidth.Dispose();
            _hImageHeight.Dispose();

            _hArea1.Dispose();
            _hRow1.Dispose();
            _hCol1.Dispose();

            return Count;
        }

        private int FindPanelThreshold(HImage _hMiddleImage, MappingParameter mappingParameter, out List<DebugResult> FindPanelCountResultList)
        {
            HImage _hGrayImge = new HImage();

            HTuple _hImageWidth = new HTuple(), _hImageHeight = new HTuple();

            HRegion _hRegion = new HRegion(), _hConnectedRegion = new HRegion();

            HTuple _hArea = new HTuple(), _hRow = new HTuple(), _hCol = new HTuple();

            // ========================= 返回參數 =========================

            FindPanelCountResultList = new List<DebugResult>();

            // ========================= 返回參數 =========================

            int Count = 0;

            _hGrayImge = _hMiddleImage.Rgb1ToGray();

            _hGrayImge.GetImageSize(out _hImageWidth, out _hImageHeight);

            _hRegion = _hGrayImge.Threshold(new HTuple(mappingParameter.TwoCamera.CountSlice.ThresholdMin), new HTuple(mappingParameter.TwoCamera.CountSlice.ThresholdMax));

            _hConnectedRegion = _hRegion.Connection();

            _hArea = _hConnectedRegion.AreaCenter(out _hRow, out _hCol);

            _hMiddleImage.OverpaintRegion(_hRegion, new HTuple(255, 0, 0), "fill");

            if (mode == "teach")
            {
                Bitmap bmp = ShowImagePoint(_hMiddleImage);

                string Type = "Inspect Result";
                string Name = "Count Slice Threshold";
                string Description = "Threshold";

                DebugResult debugResult = GenerateDebugResult(bmp, Type, Name, Description, false);

                FindPanelCountResultList.Add(debugResult);
            }

            if (_hArea.TupleMax() > mappingParameter.TwoCamera.CountSlice.ThresholdArea)
            {
                Count = 1;
            }

            _hGrayImge.Dispose();

            _hImageWidth.Dispose();
            _hImageHeight.Dispose();

            _hRegion.Dispose();
            _hArea.Dispose();
            _hRow.Dispose();
            _hCol.Dispose();

            return Count;
        }

        private HTuple PanelMeasurePoints(HImage _hImage, double PanelRowStart, double PanelRowEnd, double Panel_Col1, double LineCount, double LineNext, double AmplitudeThreshold, out HTuple _hAllCol1)
        {
            HTuple _hAllRow1 = new HTuple();

            _hAllCol1 = new HTuple();

            // ========================= 返回參數 =========================

            HTuple _hRow = new HTuple(), _hCol = new HTuple();
            HTuple _hAmplitude = new HTuple(), _hDistance = new HTuple();

            // ========================= 返回參數 =========================

            int RoiWidthLen2 = 5;

            for (int i = 0; i < (LineCount + 1); i++)
            {
                double RowStart = PanelRowStart;
                double ColStart = Panel_Col1 + LineNext * i;
                double RowEnd = PanelRowEnd;
                double ColEnd = Panel_Col1 + LineNext * i;

                _hRow = MeasurePosFindPoint(_hImage.CopyImage(), RowStart, ColStart, RowEnd, ColEnd, RoiWidthLen2, AmplitudeThreshold, out _hCol, out _hAmplitude, out _hDistance);

                _hAllRow1 = _hAllRow1.TupleConcat(_hRow);
                _hAllCol1 = _hAllCol1.TupleConcat(_hCol);
            }

            return _hAllRow1;
        }

        private HTuple MeasurePosFindPoint(HImage _hImage, double RowStart, double ColStart, double RowEnd, double ColEnd, double RoiWidthLen2, double AmplitudeThreshold, out HTuple _hCol, out HTuple _hAmplitude, out HTuple _hDistance)
        {
            HMeasure MsrHandle_Measure_01_1 = new HMeasure();

            HTuple _hImageWidth = new HTuple(), _hImageHeight = new HTuple();

            // ========================= 返回參數 =========================

            HTuple _hRow = new HTuple();

            _hCol = new HTuple();
            _hAmplitude = new HTuple();
            _hDistance = new HTuple();

            // ========================= 返回參數 =========================

            _hImage.GetImageSize(out _hImageWidth, out _hImageHeight);


            if ((RowEnd - RowStart) < 5)
            {
                RowStart = RowStart - 2;

                RowEnd = RowEnd + 2;
            }

            double LineRowStart_Measure_01_1 = RowStart;
            double LineColumnStart_Measure_01_1 = ColStart;
            double LineRowEnd_Measure_01_1 = RowEnd;
            double LineColumnEnd_Measure_01_1 = ColEnd;

            double TmpCtrl_Row = 0.5 * (LineRowStart_Measure_01_1 + LineRowEnd_Measure_01_1);
            double TmpCtrl_Column = 0.5 * (LineColumnStart_Measure_01_1 + LineColumnEnd_Measure_01_1);
            double TmpCtrl_Dr = LineRowStart_Measure_01_1 - LineRowEnd_Measure_01_1;
            double TmpCtrl_Dc = LineColumnEnd_Measure_01_1 - LineColumnStart_Measure_01_1;

            double TmpCtrl_Phi = Math.Atan2(TmpCtrl_Dr, TmpCtrl_Dc);

            double TmpCtrl_Len1 = 0.5 * Math.Sqrt(TmpCtrl_Dr * TmpCtrl_Dr + TmpCtrl_Dc * TmpCtrl_Dc);
            double TmpCtrl_Len2 = RoiWidthLen2;

            MsrHandle_Measure_01_1.GenMeasureRectangle2(TmpCtrl_Row, TmpCtrl_Column, TmpCtrl_Phi, TmpCtrl_Len1, TmpCtrl_Len2, _hImageWidth[0], _hImageHeight[0], "nearest_neighbor");

            MsrHandle_Measure_01_1.MeasurePos(_hImage, 1, AmplitudeThreshold, "all", "all", out _hRow, out _hCol, out _hAmplitude, out _hDistance);

            return _hRow;
        }

        private HTuple Find_MaxRow(HImage _hImage, HTuple _hAllRow, HTuple _hAllCol)
        {
            HTuple _hTempRow = new HTuple(), _hTempCol = new HTuple();

            // ========================= 返回參數 =========================

            HTuple _hAllMaxRow = new HTuple();

            // ========================= 返回參數 =========================


            _hTempRow = _hAllRow;
            _hTempCol = _hAllCol;
            for (; _hTempRow.TupleLength() != 0;)
            //while (_hTempRow.TupleLength() != 0)
            {
                HTuple _hMaxIndices = _hTempRow.TupleFind(_hTempRow.TupleMax());

                HTuple _hMaxRow = _hTempRow.TupleSelect(_hMaxIndices);
                HTuple _hMaxCol = _hTempCol.TupleSelect(_hMaxIndices);

                HTuple _hAngleMinRow = new HTuple();

                int Length1 = _hTempRow.TupleLength();

                for (int i = 0; i < Length1; i++)
                {
                    HTuple _hAngle = (_hTempRow.TupleSelect(i) - _hMaxRow).TupleAbs().TupleAtan2((_hAllCol.TupleSelect(i) - _hMaxCol).TupleAbs());

                    HTuple _hDeg = _hAngle.TupleDeg();

                    _hAngleMinRow = _hAngleMinRow.TupleConcat(_hDeg);

                    _hAngle.Dispose();
                    _hDeg.Dispose();
                }

                HTuple _hDisIndex = (_hTempRow - _hMaxRow).TupleAbs().TupleLessEqualElem(10);

                HTuple _hAngleIndex = _hAngleMinRow.TupleLessEqualElem(8);

                HTuple _hNearIndex = _hDisIndex.TupleOr(_hAngleIndex);

                HTuple _hNearIndices = _hNearIndex.TupleFind(1);

                _hAllMaxRow = _hAllMaxRow.TupleConcat(_hMaxRow);

                _hTempRow = _hTempRow.TupleRemove(_hNearIndices);
                _hTempCol = _hTempCol.TupleRemove(_hNearIndices);


                _hMaxIndices.Dispose();
                _hMaxRow.Dispose();
                _hMaxCol.Dispose();
                _hAngleMinRow.Dispose();
                _hDisIndex.Dispose();
                _hAngleIndex.Dispose();
                _hNearIndex.Dispose();
                _hNearIndices.Dispose();
            }


            _hTempRow.Dispose();
            _hTempCol.Dispose();

            return _hAllMaxRow;
        }

        private bool Find_PanelLine_ThreeCamera(HImage _hImage, HTuple _hAllMaxRow, HTuple _hAllRow, HTuple _hAllCol, double Panel_Col1, double LeastPoint, double LineLengthThreshold, out HTuple _hReturn_AllRow, out HTuple _hReturn_AllCol, out HTuple _hN_m, out HTuple _hN_b, out HTuple _hN_A, out HTuple _hN_B, out HTuple _hN_C)
        {
            HXLDCont _hN_Contour = new HXLDCont();

            HTuple _hMinIndices = new HTuple(), _hMinRow = new HTuple(), _hMinCol = new HTuple();

            HTuple _hAngleMinRow = new HTuple(), _hAngle = new HTuple(), _hDeg = new HTuple();

            HTuple _hDisIndex = new HTuple(), _hAngleIndex = new HTuple(), _hNearIndex = new HTuple();

            HTuple _hNearIndices = new HTuple(), _hNowLinePointRow = new HTuple(), _hNowLinePointCol = new HTuple();

            HTuple _hN_StartRow = new HTuple(), _hLineLength = new HTuple();


            int Length1 = _hAllMaxRow.TupleLength();
            int Length2 = _hAllRow.TupleLength();

            // ========================= 返回參數 =========================

            _hReturn_AllRow = new HTuple();
            _hReturn_AllCol = new HTuple();

            _hN_m = new HTuple();
            _hN_b = new HTuple();
            _hN_A = new HTuple();
            _hN_B = new HTuple();
            _hN_C = new HTuple();

            bool isFindLine = false;

            // ========================= 返回參數 =========================

            for (int i = 0; i < Length1; i++)
            {
                _hMinIndices = _hAllRow.TupleFind(_hAllMaxRow.TupleSelect(i));

                _hMinRow = _hAllRow.TupleSelect(_hMinIndices[0]);
                _hMinCol = _hAllCol.TupleSelect(_hMinIndices[0]);

                _hAngleMinRow = new HTuple();

                for (int j = 0; j < Length2; j++)
                {
                    _hAngle = (_hAllRow.TupleSelect(j) - _hMinRow).TupleAbs().TupleAtan2((_hAllCol.TupleSelect(j) - _hMinCol).TupleAbs());

                    _hDeg = _hAngle.TupleDeg();

                    _hAngleMinRow = _hAngleMinRow.TupleConcat(_hDeg);
                }

                _hDisIndex = (_hAllRow - _hMinRow).TupleAbs().TupleLessEqualElem(10);

                _hAngleIndex = _hAngleMinRow.TupleLessEqualElem(3);

                _hNearIndex = _hDisIndex.TupleOr(_hAngleIndex);

                _hNearIndices = _hNearIndex.TupleFind(1);

                // youyi [因已調整ROI框縮小範圍，所以直接考慮所有點計算]
                //_hNowLinePointRow = _hAllRow.TupleSelect(_hNearIndices);
                //_hNowLinePointCol = _hAllCol.TupleSelect(_hNearIndices);

                _hNowLinePointRow = _hAllRow;
                _hNowLinePointCol = _hAllCol;

                int NowPointLength = _hNowLinePointRow.TupleUniq().TupleLength();

                bool CountPointsOK = false, LineLengthOK = false;

                if (NowPointLength > 3)
                {
                    _hN_Contour = FitLineAndPointDistance(_hNowLinePointRow, _hNowLinePointCol, out _hN_m, out _hN_b, out _hN_A, out _hN_B, out _hN_C, out HTuple _hN_Dis, out HTuple _hN_Pos, out HTuple _hN_angle);

                    _hN_StartRow = _hN_m * Panel_Col1 + _hN_b;

                    CountPointsOK = NowPointLength > LeastPoint;

                    _hLineLength = _hNowLinePointCol.TupleMax() - _hNowLinePointCol.TupleMin();

                    LineLengthOK = _hLineLength > LineLengthThreshold;
                }

                if (CountPointsOK && LineLengthOK)
                {
                    _hReturn_AllRow = _hNowLinePointRow;
                    _hReturn_AllCol = _hNowLinePointCol;

                    isFindLine = true;

                    break;
                }
            }


            _hN_Contour.Dispose();

            _hMinIndices.Dispose();
            _hMinRow.Dispose();
            _hMinCol.Dispose();
            _hAngleMinRow.Dispose();
            _hAngle.Dispose();
            _hDeg.Dispose();
            _hDisIndex.Dispose();
            _hAngleIndex.Dispose();
            _hNearIndex.Dispose();
            _hNearIndices.Dispose();
            _hNowLinePointRow.Dispose();
            _hNowLinePointCol.Dispose();
            _hN_StartRow.Dispose();
            _hLineLength.Dispose();

            return isFindLine;
        }

        private bool Find_PanelLine_TwoCamera(HImage _hImage, HTuple _hAllMaxRow, HTuple _hAllRow, HTuple _hAllCol, double Panel_Col1, double LeastPoint, double LineLengthThreshold, out HTuple _hReturn_AllRow, out HTuple _hReturn_AllCol, out HTuple _hN_m, out HTuple _hN_b, out HTuple _hN_A, out HTuple _hN_B, out HTuple _hN_C)
        {
            HXLDCont _hN_Contour = new HXLDCont();

            HTuple _hMinIndices = new HTuple(), _hMinRow = new HTuple(), _hMinCol = new HTuple();

            HTuple _hAngleMinRow = new HTuple(), _hAngle = new HTuple(), _hDeg = new HTuple();

            HTuple _hDisIndex = new HTuple(), _hAngleIndex = new HTuple(), _hNearIndex = new HTuple();

            HTuple _hNearIndices = new HTuple(), _hNowLinePointRow = new HTuple(), _hNowLinePointCol = new HTuple();

            HTuple _hN_StartRow = new HTuple(), _hLineLength = new HTuple();


            int Length1 = _hAllMaxRow.TupleLength();
            int Length2 = _hAllRow.TupleLength();

            // ========================= 返回參數 =========================

            _hReturn_AllRow = new HTuple();
            _hReturn_AllCol = new HTuple();

            _hN_m = new HTuple();
            _hN_b = new HTuple();
            _hN_A = new HTuple();
            _hN_B = new HTuple();
            _hN_C = new HTuple();

            bool isFindLine = false;

            // ========================= 返回參數 =========================

            for (int i = 0; i < Length1; i++)
            {
                _hMinIndices = _hAllRow.TupleFind(_hAllMaxRow.TupleSelect(i));

                _hMinRow = _hAllRow.TupleSelect(_hMinIndices[0]);
                _hMinCol = _hAllCol.TupleSelect(_hMinIndices[0]);

                _hAngleMinRow = new HTuple();

                for (int j = 0; j < Length2; j++)
                {
                    _hAngle = (_hAllRow.TupleSelect(j) - _hMinRow).TupleAbs().TupleAtan2((_hAllCol.TupleSelect(j) - _hMinCol).TupleAbs());

                    _hDeg = _hAngle.TupleDeg();

                    _hAngleMinRow = _hAngleMinRow.TupleConcat(_hDeg);
                }

                _hDisIndex = (_hAllRow - _hMinRow).TupleAbs().TupleLessEqualElem(10);

                _hAngleIndex = _hAngleMinRow.TupleLessEqualElem(3);

                _hNearIndex = _hDisIndex.TupleOr(_hAngleIndex);

                _hNearIndices = _hNearIndex.TupleFind(1);


                //_hNowLinePointRow = _hAllRow.TupleSelect(_hNearIndices);
                //_hNowLinePointCol = _hAllCol.TupleSelect(_hNearIndices);

                // youyi [因已調整ROI框縮小範圍，所以直接考慮所有點計算]
                _hNowLinePointRow = _hAllRow;
                _hNowLinePointCol = _hAllCol;

                int NowPointLength = _hNowLinePointRow.TupleUniq().TupleLength();

                bool CountPointsOK = false, LineLengthOK = false;

                if (NowPointLength > 3)
                {
                    _hN_Contour = FitLineAndPointDistance(_hNowLinePointRow, _hNowLinePointCol, out _hN_m, out _hN_b, out _hN_A, out _hN_B, out _hN_C, out HTuple _hN_Dis, out HTuple _hN_Pos, out HTuple _hN_angle);

                    _hN_StartRow = _hN_m * Panel_Col1 + _hN_b;

                    CountPointsOK = NowPointLength > LeastPoint;

                    _hLineLength = _hNowLinePointCol.TupleMax() - _hNowLinePointCol.TupleMin();

                    LineLengthOK = _hLineLength > LineLengthThreshold;
                }

                if (CountPointsOK && LineLengthOK)
                {
                    _hReturn_AllRow = _hNowLinePointRow;
                    _hReturn_AllCol = _hNowLinePointCol;

                    isFindLine = true;

                    break;
                }
            }


            _hN_Contour.Dispose();

            _hMinIndices.Dispose();
            _hMinRow.Dispose();
            _hMinCol.Dispose();
            _hAngleMinRow.Dispose();
            _hAngle.Dispose();
            _hDeg.Dispose();
            _hDisIndex.Dispose();
            _hAngleIndex.Dispose();
            _hNearIndex.Dispose();
            _hNearIndices.Dispose();
            _hNowLinePointRow.Dispose();
            _hNowLinePointCol.Dispose();
            _hN_StartRow.Dispose();
            _hLineLength.Dispose();

            return isFindLine;
        }

        private HXLDCont FitLineAndPointDistance(HTuple _hRow, HTuple _hCol, out HTuple _hm, out HTuple _hb, out HTuple _hA, out HTuple _hB, out HTuple _hC, out HTuple _hDis, out HTuple _hPos, out HTuple _hAngle)
        {
            HXLDCont _hXLDcont = new HXLDCont();

            // ========================= 返回參數 =========================

            HXLDCont _hContour = new HXLDCont();

            _hm = new HTuple();
            _hb = new HTuple();
            _hA = new HTuple();
            _hB = new HTuple();
            _hC = new HTuple();
            _hDis = new HTuple();
            _hPos = new HTuple();
            _hAngle = new HTuple();

            // ========================= 返回參數 =========================

            try
            {
                _hXLDcont.GenContourPolygonXld(_hRow, _hCol);

                _hXLDcont.FitLineContourXld("huber", -1, 0, 5, 2, out HTuple _hRowBegin, out HTuple _hColBegin, out HTuple _hRowEnd, out HTuple _hColEnd, out HTuple _hM_Nr, out HTuple _hM_Nc, out HTuple _hDist);

                _hContour.GenContourPolygonXld(_hRowBegin.TupleConcat(_hRowEnd), _hColBegin.TupleConcat(_hColEnd));

                _hm = (_hRowEnd - _hRowBegin) / (_hColEnd - _hColBegin);
                _hb = _hRowBegin - _hColBegin * _hm;
                _hA = _hm;
                _hB = -1;
                _hC = _hb;
                _hDis = (_hA * _hCol + _hB * _hRow + _hC).TupleAbs() / (_hA * _hA + _hB * _hB).TupleSqrt();
                _hPos = _hA * _hCol + _hB * _hRow + _hC;
                _hAngle = _hM_Nr.TupleAtan2(_hM_Nc) - new HTuple(90).TupleRad();
                _hAngle = _hAngle.TupleDeg();
            }
            catch (Exception ex)
            {

            }

            return _hContour;
        }

        private HTuple FindOutlier(HTuple _hDis)
        {
            HTuple _hMean = new HTuple(), _hDeviation = new HTuple(), _hMedian = new HTuple(), _hZ_score = new HTuple();

            HTuple _hMaxIndex = new HTuple(), _hMinIndex = new HTuple(), _hIndex = new HTuple();

            HTuple _hMAD = new HTuple(), _hRobust_Z_score = new HTuple();

            // ========================= 返回參數 =========================

            HTuple _hIndices = new HTuple();

            // ========================= 返回參數 =========================


            try
            {
                _hMean = _hDis.TupleMean();

                _hDeviation = _hDis.TupleDeviation();

                _hMedian = _hDis.TupleMedian();

                _hZ_score = (_hDis - _hMean) / _hDeviation;

                _hMaxIndex = _hZ_score.TupleGreaterEqualElem(3);

                _hMinIndex = _hZ_score.TupleLessEqualElem(-3);

                _hIndex = _hMaxIndex.TupleOr(_hMinIndex);

                _hIndices = _hIndex.TupleFind(1);

                _hMAD = (_hDis - _hMedian).TupleAbs().TupleMedian();

                _hRobust_Z_score = 0.6745 * (_hDis - _hMedian) / _hMAD;

                _hMaxIndex = _hRobust_Z_score.TupleGreaterEqualElem(3);

                _hMinIndex = _hRobust_Z_score.TupleLessEqualElem(-3);

                _hIndex = _hMaxIndex.TupleOr(_hMinIndex);

                _hIndices = _hIndex.TupleFind(1);
            }
            catch (Exception ex)
            {

            }


            _hMean.Dispose();
            _hDeviation.Dispose();
            _hMedian.Dispose();
            _hZ_score.Dispose();

            _hMaxIndex.Dispose();
            _hMinIndex.Dispose();
            _hIndex.Dispose();

            _hMAD.Dispose();
            _hRobust_Z_score.Dispose();

            return _hIndices;
        }

        // ====================== 計算片數(判斷有片、無片、斜插) ======================

        private HTuple TupleFind(HTuple _hArray, HTuple _hNumber)
        {
            List<int> IndexList = new List<int>();
            int[] IntArray = _hArray.TupleRound().ToIArr();
            int IntNumber = (int)_hNumber.TupleRound();

            for (int Index = 0; Index < _hArray.TupleLength(); Index++)
            {
                if (IntArray[Index] == IntNumber)
                {
                    IndexList.Add(Index);
                }
            }
            if (IndexList.Count == 0)
            {
                return -1;
            }

            _hArray.Dispose();
            _hNumber.Dispose();

            return new HTuple(IndexList.ToArray());
        }

        private Bitmap ShowImagePoint(HImage _hImage, HTuple _hTopRow = null, HTuple _hTopCol = null, HTuple _hBottomRow = null, HTuple _hBottomCol = null, HTuple _hMiddleRow = null, HTuple _hMiddleCol = null, ROI roi = null)
        {
            Bitmap _bImage;

            int channel = _hImage.CountChannels();

            if (channel == 1)
            {
                _hImage = _hImage.Compose3(_hImage, _hImage);
            }

            _bImage = HC.HimageToBitmap(_hImage);

            using (Graphics graphic = Graphics.FromImage(_bImage))
            {
                int size = 10;

                Pen pen;

                if (_hTopRow != null && _hTopCol != null)
                {
                    for (int i = 0; i < _hTopRow.TupleLength(); i++)
                    {
                        pen = new Pen(Color.Blue, 1);

                        double val1 = _hTopRow[i];
                        double val2 = _hTopCol[i];

                        graphic.DrawLine(pen, (float)val2 - size, (float)val1, (float)val2 + size, (float)val1);
                        graphic.DrawLine(pen, (float)val2, (float)val1 - size, (float)val2, (float)val1 + size);
                    }
                }

                if (_hBottomRow != null && _hBottomCol != null)
                {
                    for (int i = 0; i < _hBottomRow.TupleLength(); i++)
                    {
                        pen = new Pen(Color.Yellow, 1);

                        double val1 = _hBottomRow[i];
                        double val2 = _hBottomCol[i];

                        graphic.DrawLine(pen, (float)val2 - size, (float)val1, (float)val2 + size, (float)val1);
                        graphic.DrawLine(pen, (float)val2, (float)val1 - size, (float)val2, (float)val1 + size);
                    }
                }

                if (_hMiddleRow != null && _hMiddleCol != null)
                {
                    for (int i = 0; i < _hMiddleRow.TupleLength(); i++)
                    {
                        pen = new Pen(Color.Green, 1);

                        double val1 = _hMiddleRow[i];
                        double val2 = _hMiddleCol[i];

                        graphic.DrawLine(pen, (float)val2 - size, (float)val1, (float)val2 + size, (float)val1);
                        graphic.DrawLine(pen, (float)val2, (float)val1 - size, (float)val2, (float)val1 + size);
                    }
                }

                if (roi != null)
                {
                    Rectangle Rect = new Rectangle(roi.C1, roi.R1, roi.C2 - roi.C1, roi.R2 - roi.R1);

                    graphic.DrawRectangle(Pens.Yellow, Rect);
                }
            }

            return _bImage;
        }

        private List<List<Point>> AddPointsFromHTuple(HTuple _hTopRow = null, HTuple _hTopCol = null, HTuple _hBottomRow = null, HTuple _hBottomCol = null, HTuple _hMiddleRow = null, HTuple _hMiddleCol = null)
        {
            List<List<Point>> PointList = new List<List<Point>>();
            List<Point> TopPointList = new List<Point>();
            List<Point> BottomPointList = new List<Point>();
            List<Point> MiddlePointList = new List<Point>();

            if (_hTopRow != null && _hTopCol != null)
            {
                PointList.Add(new List<Point>());

                for (int i = 0; i < _hTopRow.TupleLength(); i++)
                {
                    double val1 = _hTopRow[i];
                    double val2 = _hTopCol[i];

                    TopPointList.Add(new Point((int)val2, (int)val1));
                }

                PointList[0].AddRange(TopPointList);
            }

            if (_hBottomRow != null && _hBottomCol != null)
            {
                PointList.Add(new List<Point>());

                for (int i = 0; i < _hBottomRow.TupleLength(); i++)
                {
                    double val1 = _hBottomRow[i];
                    double val2 = _hBottomCol[i];

                    BottomPointList.Add(new Point((int)val2, (int)val1));
                }

                PointList[1].AddRange(BottomPointList);
            }

            if (_hMiddleRow != null && _hMiddleCol != null)
            {
                PointList.Add(new List<Point>());

                for (int i = 0; i < _hMiddleRow.TupleLength(); i++)
                {
                    double val1 = _hMiddleRow[i];
                    double val2 = _hMiddleCol[i];

                    MiddlePointList.Add(new Point((int)val2, (int)val1));
                }

                PointList[2].AddRange(MiddlePointList);
            }

            return PointList;
        }

        private List<Equation> AddEquation(HTuple _hT_m = null, HTuple _hT_b = null, HTuple _hB_m = null, HTuple _hB_b = null, HTuple _hM_m = null, HTuple _hM_b = null)
        {
            List<Equation> EquationList = new List<Equation>();

            Equation TopEquation = new Equation();
            Equation BottomEquation = new Equation();
            Equation MiddleEquation = new Equation();

            if (_hT_m != null && _hT_b != null && _hT_m.TupleLength() > 0 && _hT_b.TupleLength() > 0)
            {
                TopEquation.m = _hT_m.D.ToString();
                TopEquation.b = _hT_b.D.ToString();
            }
            else
            {
                TopEquation.m = "No Data";
                TopEquation.b = "No Data";
            }

            EquationList.Add(TopEquation);

            if (_hB_m != null && _hB_b != null && _hB_m.TupleLength() > 0 && _hB_b.TupleLength() > 0)
            {
                BottomEquation.m = _hB_m.D.ToString();
                BottomEquation.b = _hB_b.D.ToString();
            }
            else
            {
                BottomEquation.m = "No Data";
                BottomEquation.b = "No Data";
            }

            EquationList.Add(BottomEquation);

            if (_hM_m != null && _hM_b != null && _hM_m.TupleLength() > 0 && _hM_b.TupleLength() > 0)
            {
                MiddleEquation.m = _hM_m.D.ToString();
                MiddleEquation.b = _hM_b.D.ToString();
            }
            else
            {
                MiddleEquation.m = "No Data";
                MiddleEquation.b = "No Data";
            }

            EquationList.Add(MiddleEquation);

            return EquationList;
        }

        //private List<double> ShowThickness(HTuple _hTopRow = null, HTuple _hBottomRow = null)
        //{
        //    List<double> ThicknessList = new List<double>();

        //    if (_hTopRow != null && _hBottomRow != null)
        //    {
        //        for (int i = 0; i < _hTopRow.TupleLength(); i++)
        //        {
        //            double val1 = (_hBottomRow[i] - _hTopRow[i]) * mappingParameter.TwoCamera.Thickness.Scale / 1000.0;

        //            ThicknessList.Add(val1);
        //        }
        //    }

        //    return ThicknessList;
        //}

        private DebugResult GenerateDebugResult(Bitmap tbmp, string type, string name, string description, bool _isComBoBoxSelect, List<List<Point>> PanelPointList = null, List<double> ThicknessList = null, List<Equation> EquationList = null, int InspectResult = 99999, double[] projection_x = null, double[] projection_y = null)
        {
            DebugResult debugResult = new DebugResult();

            debugResult.Image = (Bitmap)tbmp.Clone();
            debugResult.Type = type;
            debugResult.Name = name;
            debugResult.Description = description;
            debugResult._isComBoBoxSelect = _isComBoBoxSelect;
            debugResult.PanelPointList = PanelPointList;
            debugResult.ThicknessList = ThicknessList;
            debugResult.EquationList = EquationList;
            debugResult.projection_x = projection_x;
            debugResult.projection_y = projection_y;

            if (InspectResult != 99999)
            {
                if (InspectResult == 0)
                {
                    debugResult.InspectResult = "Absence";
                }
                else if (InspectResult == 1)
                {
                    debugResult.InspectResult = "Presence";
                }
                else if (InspectResult == 3)
                {
                    debugResult.InspectResult = "Stack";
                }
            }
            else
            {
                debugResult.InspectResult = "No Data";
            }

            return debugResult;
        }

        public async Task SaveResult(string path, string[,] result, string recipename, string foupid)  //0204 jackyverify
        {
            using (var file = new StreamWriter(path))
            {
                file.WriteLine("Slot,State,Thickness(mm),Warpage(mm),LeftGap(mm),RightGap(mm)");

                string[] parts = path.Split('\\');   //0127 jacky verify
                string resulttimecheck = parts[4];
                string reomtecheck = parts[2];   //0212

                DateTime dt = DateTime.ParseExact(resulttimecheck, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);

                for (int i = 0; i < result.GetLength(0); i++)
                {
                    string res = "";

                    string type = string.Empty;

                    if (result[i, 1] == "0")
                    {
                        type = "Absence";
                    }
                    else if (result[i, 1] == "1")
                    {
                        type = "Presence";
                    }
                    else if (result[i, 1] == "2")
                    {
                        type = "Slant";
                    }
                    else if (result[i, 1] == "3")
                    {
                        type = "Stack";
                    }

                    res = result[i, 0] + "," + type + "," + result[i, 2].Replace("99999", "-") + "," + result[i, 3].Replace("99999", "-") + "," + result[i, 4].Replace("99999", "-") + "," + result[i, 5].Replace("99999", "-");
                    int count = 0;
                   
                    //file.WriteLine(res);
                    await file.WriteLineAsync(res);
                }
            }
        }
    }
}
