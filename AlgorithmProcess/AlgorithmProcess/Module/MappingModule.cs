using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProcess
{
    internal class MappingModule
    {
        public MappingModule Clone()
        {
            return (MappingModule)this.MemberwiseClone();
        }


        public int CountSlice_TwoCamera(HImage _hImageLeft, HImage _hImageRight, MappingParameter mappingParameter, out int RCount, out int Result, out List<DebugResult> CountSliceResultList)
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

            double CutPoint1 = mappingParameter.CountSlice_CutPoint1;
            double CutPoint2 = mappingParameter.CountSlice_CutPoint2;


            // ====================== 左側相機 ROI ======================
            ROI leftCameraROI = new ROI();

            leftCameraROI.R1 = CountSlice_LeftCameraROI.R1;
            leftCameraROI.C1 = CountSlice_LeftCameraROI.C1;
            leftCameraROI.R2 = CountSlice_LeftCameraROI.R2;
            leftCameraROI.C2 = CountSlice_LeftCameraROI.C2;
            // ====================== 左側相機 ROI ======================


            // ====================== 右側相機 ROI ======================
            ROI rightCameraROI = new ROI();

            rightCameraROI.R1 = CountSlice_RightCameraROI.R1;
            rightCameraROI.C1 = CountSlice_RightCameraROI.C1;
            rightCameraROI.R2 = CountSlice_RightCameraROI.R2;
            rightCameraROI.C2 = CountSlice_RightCameraROI.C2;
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

            int Count_L = FindPanelCount_TwoCamera(_hImagePart1, mappingParameter.L_Panel_threshold_value, 1, 1, L_Panel_Row2 - L_Panel_Row1, LineCount, LineNext, mappingParameter, out HTuple _hL_AllRow, out HTuple _hL_AllCol, out HTuple _hL_NearRow, out HTuple _hL_NearCol, out HTuple _hL_N_m, out HTuple _hL_N_b, out HTuple _hL_N_A, out HTuple _hL_N_B, out HTuple _hL_N_C, out FindPanelCountResultList);

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
            double L_Edge_Panel_Col1 = Warpage_LeftEdgeCameraROI.C1;
            double L_Edge_Panel_Col2 = Warpage_LeftEdgeCameraROI.C2;
            double L_Edge_Panel_Row1 = Warpage_LeftEdgeCameraROI.R1;
            double L_Edge_Panel_Row2 = Warpage_LeftEdgeCameraROI.R2;
            string Direction = "L";

            _hImageEdgeL = _hImageL.CropPart(L_Edge_Panel_Row1, L_Edge_Panel_Col1, L_Edge_Panel_Col2 - L_Edge_Panel_Col1, L_Edge_Panel_Row2 - L_Edge_Panel_Row1);


            double LeftEdge_ThresholdMin = mappingParameter.CountSlice_LeftEdge_ThresholdMin;
            double LeftEdge_ThresholdMax = mappingParameter.CountSlice_LeftEdge_ThresholdMax;
            double LeftEdge_Area = mappingParameter.CountSlice_LeftEdge_Area;
            double LeftEdge_AreaRowDistance = mappingParameter.CountSlice_LeftEdge_AreaRowDistance;
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

            int Count_R = FindPanelCount_TwoCamera(_hImagePart3, mappingParameter.R_Panel_threshold_value, 1, 1, R_Panel_Row2 - R_Panel_Row1, LineCount, LineNext, mappingParameter, out HTuple _hR_AllRow, out HTuple _hR_AllCol, out HTuple _hR_NearRow, out HTuple _hR_NearCol, out HTuple _hR_N_m, out HTuple _hR_N_b, out HTuple _hR_N_A, out HTuple _hR_N_B, out HTuple _hR_N_C, out FindPanelCountResultList);

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
            double R_Edge_Panel_Col1 = Warpage_RightEdgeCameraROI.C1;
            double R_Edge_Panel_Col2 = Warpage_RightEdgeCameraROI.C2;
            double R_Edge_Panel_Row1 = Warpage_RightEdgeCameraROI.R1;
            double R_Edge_Panel_Row2 = Warpage_RightEdgeCameraROI.R2;
            Direction = "R";

            _hImageEdgeR = _hImageR.CropPart(R_Edge_Panel_Row1, R_Edge_Panel_Col1, R_Edge_Panel_Col2 - R_Edge_Panel_Col1, R_Edge_Panel_Row2 - R_Edge_Panel_Row1);

            double RightEdge_ThresholdMin = mappingParameter.CountSlice_RightEdge_ThresholdMin;
            double RightEdge_ThresholdMax = mappingParameter.CountSlice_RightEdge_ThresholdMax;
            double RightEdge_Area = mappingParameter.CountSlice_RightEdge_Area;
            double RightEdge_AreaRowDistance = mappingParameter.CountSlice_RightEdge_AreaRowDistance;
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

            //if (Count_L == 1 && Count_R == 1)
            //{
            //    _hAll_NearRow = _hL_AllRow.TupleConcat(_hR_AllRow);

            //    _hRange = _hAll_NearRow.TupleMax() - _hAll_NearRow.TupleMin();

            //    _hL_Min_Row = _hL_AllRow.TupleMin();
            //    _hL_Max_Row = _hL_AllRow.TupleMax();
            //    _hR_Min_Row = _hR_AllRow.TupleMin();
            //    _hR_Max_Row = _hR_AllRow.TupleMax();

            //    _hAbs_L = _hL_N_m.TupleAbs();
            //    _hAbs_R = _hR_N_m.TupleAbs();

            //    // ====================== 滿足條件才是斜插 ======================
            //    if (_hAbs_L > mappingParameter.CountSlice_Slant_ABS_Value && _hAbs_R > mappingParameter.CountSlice_Slant_ABS_Value)
            //    {
            //        _IsSlant = true;

            //        if (_hL_N_m > 0 && _hR_N_m > 0)
            //        {
            //            // 左高 右低

            //            LCount = 1;
            //            RCount = 0;
            //        }
            //        else if (_hL_N_m < 0 && _hR_N_m < 0)
            //        {
            //            // 右高 左低

            //            LCount = 0;
            //            RCount = 1;
            //        }
            //    }
            //}
            //else
            //{
            //    _IsSlant = false;

            //    // ====================== 左低 右高 ======================
            //    // youyi
            //    if (Count_L == 1 && Count_R == 0)
            //    {
            //        double R_ROI_Row1 = 0, R_ROI_Col1 = 0, R_ROI_Row2 = 0, R_ROI_Col2 = 0;

            //        if (_hL_N_m < 0)
            //        {
            //            R_ROI_Row1 = rightCameraROI.R1 - mappingParameter.CountSlice_Bar_Distance;

            //            R_ROI_Col1 = rightCameraROI.C1;

            //            R_ROI_Row2 = rightCameraROI.R2 - mappingParameter.CountSlice_Bar_Distance;

            //            R_ROI_Col2 = rightCameraROI.C2;   
            //        }
            //        else if (_hL_N_m > 0)
            //        {
            //            R_ROI_Row1 = rightCameraROI.R1 + mappingParameter.CountSlice_Bar_Distance;

            //            R_ROI_Col1 = rightCameraROI.C1;   

            //            R_ROI_Row2 = rightCameraROI.R2 + mappingParameter.CountSlice_Bar_Distance;

            //            R_ROI_Col2 = rightCameraROI.C2;   
            //        }

            //        R_Panel_Col1 = R_ROI_Col1;
            //        R_Panel_Col2 = R_ROI_Col2;
            //        R_Panel_Row1 = R_ROI_Row1;
            //        R_Panel_Row2 = R_ROI_Row2;

            //        LineNext = 20;
            //        LineCount = (double)(R_ROI_Col2 - R_Panel_Col1) / LineNext - 1;

            //        _hImagePart3 = _hImageR.CropPart(R_Panel_Row1, R_Panel_Col1, R_Panel_Col2 - R_Panel_Col1, R_Panel_Row2 - R_Panel_Row1);

            //        Count_R_AF = FindPanelCount_TwoCamera(_hImagePart3, mappingParameter.R_Panel_threshold_value, 1, 1, R_Panel_Row2 - R_Panel_Row1, LineCount, LineNext, mappingParameter, out _hR_AllRow, out _hR_AllCol, out _hR_NearRow, out _hR_NearCol, out _hR_N_m, out _hR_N_b, out _hR_N_A, out _hR_N_B, out _hR_N_C, out FindPanelCountResultList);

            //        _hR_AllRow = _hR_AllRow + R_Panel_Row1;
            //        _hR_AllCol = _hR_AllCol + R_Panel_Col1;

            //        if (mode == "teach")
            //        {
            //            Bitmap bmp = null;
            //            DebugResult debugResult = null;

            //            ROI roi = new ROI
            //            {
            //                R1 = (int)R_Panel_Row1,
            //                C1 = (int)R_Panel_Col1,
            //                R2 = (int)R_Panel_Row2,
            //                C2 = (int)R_Panel_Col2
            //            };

            //            bmp = ShowImagePoint(_hImageR, null, null, null, null, null, null, roi);

            //            string Type = "Inspect Result";
            //            string Name = "Second Calculate of the Right side of the Middle image";
            //            string Description = "If left is lower and right is higher, recheck the presence of a panel on the right side of the slot’s Middle image at this layer." + " Count: " + Count_R_AF.ToString() + " Slope: " + _hR_N_m.ToString();

            //            debugResult = GenerateDebugResult(bmp, Type, Name, Description, true);

            //            CountSliceResultList.Add(debugResult);

            //            CountSliceResultList.AddRange(FindPanelCountResultList);


            //            bmp = ShowImagePoint(_hImageR, _hR_AllRow, _hR_AllCol, null, null, null, null, roi);

            //            List<List<Point>> PanelPointList = AddPointsFromHTuple(_hR_AllRow, _hR_AllCol);

            //            List<Equation> EquationList = AddEquation(_hR_N_m, _hR_N_b);

            //            debugResult = GenerateDebugResult(bmp, Type, Name, Description, false, PanelPointList, null, EquationList, Count_R_AF);

            //            CountSliceResultList.Add(debugResult);
            //        }
            //    }
            //    // ====================== 左高 右低 ======================
            //    // youyi 
            //    else if (Count_L == 0 && Count_R == 1)
            //    {
            //        double L_ROI_Row1 = 0, L_ROI_Col1 = 0, L_ROI_Row2 = 0, L_ROI_Col2 = 0;

            //        if (_hR_N_m > 0)
            //        {
            //            L_ROI_Row1 = leftCameraROI.R1 - mappingParameter.CountSlice_Bar_Distance;

            //            L_ROI_Col1 = leftCameraROI.C1;

            //            L_ROI_Row2 = leftCameraROI.R2 - mappingParameter.CountSlice_Bar_Distance;

            //            L_ROI_Col2 = leftCameraROI.C2;
            //        }
            //        else if (_hR_N_m < 0)
            //        {
            //            L_ROI_Row1 = leftCameraROI.R1 + mappingParameter.CountSlice_Bar_Distance;

            //            L_ROI_Col1 = leftCameraROI.C1;    

            //            L_ROI_Row2 = leftCameraROI.R2 + mappingParameter.CountSlice_Bar_Distance;

            //            L_ROI_Col2 = leftCameraROI.C2;   
            //        }

            //        L_Panel_Col1 = L_ROI_Col1;
            //        L_Panel_Col2 = L_ROI_Col2;
            //        L_Panel_Row1 = L_ROI_Row1;
            //        L_Panel_Row2 = L_ROI_Row2;

            //        LineNext = 20;
            //        LineCount = (L_ROI_Col2 - L_Panel_Col1) / LineNext - 1;

            //        // youyi
            //        _hImagePart1 = _hImageL.CropPart(L_Panel_Row1, L_Panel_Col1, L_Panel_Col2 - L_Panel_Col1, L_Panel_Row2 - L_Panel_Row1);

            //        Count_L_AF = FindPanelCount_TwoCamera(_hImagePart1, mappingParameter.L_Panel_threshold_value, 1, 1, L_Panel_Row2 - L_Panel_Row1, LineCount, LineNext, mappingParameter, out _hL_AllRow, out _hL_AllCol, out _hL_NearRow, out _hL_NearCol, out _hL_N_m, out _hL_N_b, out _hL_N_A, out _hL_N_B, out _hL_N_C, out FindPanelCountResultList);

            //        _hL_AllRow = _hL_AllRow + L_Panel_Row1;
            //        _hL_AllCol = _hL_AllCol + L_Panel_Col1;

            //        // youyi
            //        if (mode == "teach")
            //        {
            //            Bitmap bmp = null;
            //            DebugResult debugResult = null;

            //            ROI roi = new ROI
            //            {
            //                R1 = (int)L_Panel_Row1,
            //                C1 = (int)L_Panel_Col1,
            //                R2 = (int)L_Panel_Row2,
            //                C2 = (int)L_Panel_Col2
            //            };
            //            // youyi
            //            bmp = ShowImagePoint(_hImageL, null, null, null, null, null, null, roi);

            //            string Type = "Inspect Result";
            //            string Name = "Second Calculate of the left side of the Middle image";
            //            string Description = "If left is higher and right is lower, recheck the presence of a panel on the left side of the slot’s Middle image at this layer." + " Count: " + Count_L_AF.ToString() + "Slope:" + _hL_N_m.ToString();

            //            debugResult = GenerateDebugResult(bmp, Type, Name, Description, true);

            //            CountSliceResultList.Add(debugResult);

            //            CountSliceResultList.AddRange(FindPanelCountResultList);

            //            // youyi
            //            bmp = ShowImagePoint(_hImageL, _hL_AllRow, _hL_AllCol, null, null, null, null, roi);

            //            List<List<Point>> PanelPointList = AddPointsFromHTuple(_hL_AllRow, _hL_AllCol);

            //            List<Equation> EquationList = AddEquation(_hL_N_m, _hL_N_b);

            //            debugResult = GenerateDebugResult(bmp, Type, Name, Description, false, PanelPointList, null, EquationList, Count_L_AF);

            //            CountSliceResultList.Add(debugResult);
            //        }
            //    }
            //    else if (Count_L == 0 && Count_R == 0)
            //    {
            //        LCount = 0;
            //        RCount = 0;
            //    }             
            //}

            if (Count_L == 1 && Count_R == 0)
            {
                LCount = Count_L;
                RCount = Count_R;
            }

            else if (Count_L == 0 && Count_R == 1)
            {
                LCount = Count_L;
                RCount = Count_R;
            }

            if (Count_L == 1 && Count_R == 1)
            {
                if (!_IsSlant)
                {
                    // 正常片計算流程

                    LCount = Count_L;
                    RCount = Count_R;
                }
            }

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

    }
}
