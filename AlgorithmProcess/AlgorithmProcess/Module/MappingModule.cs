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
            // ========================= 物件宣告與複製 =========================
            HImage _hImageL = _hImageLeft.CopyImage();
            HImage _hImageR = _hImageRight.CopyImage();

            HImage _hImageEdgeL = null;
            HImage _hImageEdgeR = null;

            // ========================= 返回參數初始化 =========================
            int LCount = 0;
            RCount = 0;
            Result = 0;
            CountSliceResultList = new List<DebugResult>();

            // ====================== 左側影像計算 ======================  
            double L_Edge_Panel_Col1 = rOIList.TwoCamera.CountSlice.LeftImage_EdgePanel.C1;
            double L_Edge_Panel_Col2 = rOIList.TwoCamera.CountSlice.LeftImage_EdgePanel.C2;
            double L_Edge_Panel_Row1 = rOIList.TwoCamera.CountSlice.LeftImage_EdgePanel.R1;
            double L_Edge_Panel_Row2 = rOIList.TwoCamera.CountSlice.LeftImage_EdgePanel.R2;

            if (mode == "teach")
            {
                Bitmap bmp = ShowImagePoint(_hImageL);
                DebugResult debugResult = GenerateDebugResult(bmp, "CountSlice_TwoCamera", "FindPanelEdgeCount", "Crop Left Image Position");

                debugResult.Variables.Add(new SingleVariableMetric("L_Edge_Panel_Col1", "ROI C1", L_Edge_Panel_Col1));
                debugResult.Variables.Add(new SingleVariableMetric("L_Edge_Panel_Col2", "ROI C2", L_Edge_Panel_Col2));
                debugResult.Variables.Add(new SingleVariableMetric("L_Edge_Panel_Row1", "ROI R1", L_Edge_Panel_Row1));
                debugResult.Variables.Add(new SingleVariableMetric("L_Edge_Panel_Row2", "ROI R2", L_Edge_Panel_Row2));

                CountSliceResultList.Add(debugResult);
            }

            _hImageEdgeL = _hImageL.CropPart(L_Edge_Panel_Row1, L_Edge_Panel_Col1, L_Edge_Panel_Col2 - L_Edge_Panel_Col1, L_Edge_Panel_Row2 - L_Edge_Panel_Row1);

            if (mode == "teach")
            {
                Bitmap bmp = ShowImagePoint(_hImageEdgeL);
                DebugResult debugResult = GenerateDebugResult(bmp, "CountSlice_TwoCamera", "FindPanelEdgeCount", "Crop Left Image");

                debugResult.Variables.Add(new SingleVariableMetric("L_Edge_Panel_Col1", "ROI C1", L_Edge_Panel_Col1));
                debugResult.Variables.Add(new SingleVariableMetric("L_Edge_Panel_Col2", "ROI C2", L_Edge_Panel_Col2));
                debugResult.Variables.Add(new SingleVariableMetric("L_Edge_Panel_Row1", "ROI R1", L_Edge_Panel_Row1));
                debugResult.Variables.Add(new SingleVariableMetric("L_Edge_Panel_Row2", "ROI R2", L_Edge_Panel_Row2));

                CountSliceResultList.Add(debugResult);
            }

            double LeftEdge_ThresholdMin = mappingParameter.TwoCamera.CountSlice.LeftEdge_ThresholdMin;
            double LeftEdge_ThresholdMax = mappingParameter.TwoCamera.CountSlice.LeftEdge_ThresholdMax;
            double LeftEdge_Area = mappingParameter.TwoCamera.CountSlice.LeftEdge_Area;
            double LeftEdge_AreaRowDistance = mappingParameter.TwoCamera.CountSlice.LeftEdge_AreaRowDistance;

            List<DebugResult> FindPanelCountResultListL; // 修正變數作用域，就近宣告
            int Count_L_Edge = FindPanelEdgeCount_TwoCamera(_hImageEdgeL, LeftEdge_ThresholdMin, LeftEdge_ThresholdMax, LeftEdge_Area, LeftEdge_AreaRowDistance, out FindPanelCountResultListL);

            int Count_L = (Count_L_Edge == 0) ? 0 : 1;

            if (mode == "teach")
            {
                CountSliceResultList.AddRange(FindPanelCountResultListL);
            }

            // ====================== 右側影像計算 ======================  
            double R_Edge_Panel_Col1 = rOIList.TwoCamera.CountSlice.RightImage_EdgePanel.C1;
            double R_Edge_Panel_Col2 = rOIList.TwoCamera.CountSlice.RightImage_EdgePanel.C2;
            double R_Edge_Panel_Row1 = rOIList.TwoCamera.CountSlice.RightImage_EdgePanel.R1;
            double R_Edge_Panel_Row2 = rOIList.TwoCamera.CountSlice.RightImage_EdgePanel.R2;

            if (mode == "teach")
            {
                Bitmap bmp = ShowImagePoint(_hImageR);
                DebugResult debugResult = GenerateDebugResult(bmp, "CountSlice_TwoCamera", "FindPanelEdgeCount", "Crop Right Image Position");

                debugResult.Variables.Add(new SingleVariableMetric("R_Edge_Panel_Col1", "ROI C1", R_Edge_Panel_Col1));
                debugResult.Variables.Add(new SingleVariableMetric("R_Edge_Panel_Col2", "ROI C2", R_Edge_Panel_Col2));
                debugResult.Variables.Add(new SingleVariableMetric("R_Edge_Panel_Row1", "ROI R1", R_Edge_Panel_Row1));
                debugResult.Variables.Add(new SingleVariableMetric("R_Edge_Panel_Row2", "ROI R2", R_Edge_Panel_Row2));

                CountSliceResultList.Add(debugResult);
            }

            _hImageEdgeR = _hImageR.CropPart(R_Edge_Panel_Row1, R_Edge_Panel_Col1, R_Edge_Panel_Col2 - R_Edge_Panel_Col1, R_Edge_Panel_Row2 - R_Edge_Panel_Row1);

            if (mode == "teach")
            {
                Bitmap bmp = ShowImagePoint(_hImageEdgeR);
                DebugResult debugResult = GenerateDebugResult(bmp, "CountSlice_TwoCamera", "FindPanelEdgeCount", "Crop Right Image");

                debugResult.Variables.Add(new SingleVariableMetric("R_Edge_Panel_Col1", "ROI C1", R_Edge_Panel_Col1));
                debugResult.Variables.Add(new SingleVariableMetric("R_Edge_Panel_Col2", "ROI C2", R_Edge_Panel_Col2));
                debugResult.Variables.Add(new SingleVariableMetric("R_Edge_Panel_Row1", "ROI R1", R_Edge_Panel_Row1));
                debugResult.Variables.Add(new SingleVariableMetric("R_Edge_Panel_Row2", "ROI R2", R_Edge_Panel_Row2));

                CountSliceResultList.Add(debugResult);
            }

            double RightEdge_ThresholdMin = mappingParameter.TwoCamera.CountSlice.RightEdge_ThresholdMin;
            double RightEdge_ThresholdMax = mappingParameter.TwoCamera.CountSlice.RightEdge_ThresholdMax;
            double RightEdge_Area = mappingParameter.TwoCamera.CountSlice.RightEdge_Area;
            double RightEdge_AreaRowDistance = mappingParameter.TwoCamera.CountSlice.RightEdge_AreaRowDistance;

            List<DebugResult> FindPanelCountResultListR; // 修正變數作用域，就近宣告
            int Count_R_Edge = FindPanelEdgeCount_TwoCamera(_hImageEdgeR, RightEdge_ThresholdMin, RightEdge_ThresholdMax, RightEdge_Area, RightEdge_AreaRowDistance, out FindPanelCountResultListR);

            int Count_R = (Count_R_Edge == 0) ? 0 : 1;

            if (mode == "teach")
            {
                CountSliceResultList.AddRange(FindPanelCountResultListR);
            }

            // ====================== 最終結果判定 ======================
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

            // ====================== 嚴格釋放 Halcon 資源 ======================
            _hImageL.Dispose();
            _hImageR.Dispose();

            // 💡 補上原本漏掉、極度危險的 Edge 影像釋放
            _hImageEdgeL?.Dispose();
            _hImageEdgeR?.Dispose();

            return LCount;
        }

        //private int FindPanelCount_TwoCamera(HImage _hMiddleImage, double Panel_threshold_value, double Panel_Col1, double PanelRowStart, double PanelRowEnd, double LineCount, double LineNext, MappingParameter mappingParameter, out HTuple _hReturn_AllRow, out HTuple _hReturn_AllCol, out HTuple _hReturn_NearRow, out HTuple _hReturn_NearCol, out HTuple _hReturn_N_m, out HTuple _hReturn_N_b, out HTuple _hReturn_N_A, out HTuple _hReturn_N_B, out HTuple _hReturn_N_C, out List<DebugResult> FindPanelCountResultList)
        //{
        //    HImage _hGrayImge = new HImage(), _hHighpass = new HImage();

        //    HTuple _hImageWidth = new HTuple(), _hImageHeight = new HTuple();

        //    HTuple _hAllRow = new HTuple(), _hAllCol = new HTuple(), _hAllMaxRow = new HTuple();

        //    HTuple _hNear_Row = new HTuple(), _hNear_Col = new HTuple(), _hN_m = new HTuple(), _hN_b = new HTuple(), _hN_A = new HTuple(), _hN_B = new HTuple(), _hN_C = new HTuple();

        //    HTuple _hDis = new HTuple(), _hDisIndices = new HTuple();

        //    // ========================= 返回參數 =========================

        //    _hReturn_AllRow = new HTuple();
        //    _hReturn_AllCol = new HTuple();
        //    _hReturn_NearRow = new HTuple();
        //    _hReturn_NearCol = new HTuple();

        //    _hReturn_N_m = new HTuple();
        //    _hReturn_N_b = new HTuple();
        //    _hReturn_N_A = new HTuple();
        //    _hReturn_N_B = new HTuple();
        //    _hReturn_N_C = new HTuple();

        //    FindPanelCountResultList = new List<DebugResult>();

        //    // ========================= 返回參數 =========================

        //    int Count = 0;

        //    _hGrayImge = _hMiddleImage.Rgb1ToGray();

        //    _hGrayImge.GetImageSize(out _hImageWidth, out _hImageHeight);

        //    _hHighpass = _hGrayImge.HighpassImage(_hImageWidth, 1);

        //    _hGrayImge = _hGrayImge.SubImage(_hHighpass, new HTuple(1), new HTuple(128));

        //    if (mode == "teach")
        //    {
        //        Bitmap bmp = ShowImagePoint(_hGrayImge);

        //        string Type = "Inspect Result";
        //        string Name = "PanelMeasurePoints";
        //        string Description = "Image Process";

        //        DebugResult debugResult = GenerateDebugResult(bmp, Type, Name, Description, false);

        //        FindPanelCountResultList.Add(debugResult);
        //    }

        //    double MinAmplitudeThreshold = mappingParameter.TwoCamera.CountSlice.MinAmplitudeThreshold;

        //    double AmplitudeThreshold = Panel_threshold_value;

        //    double LeastPoint = LineCount / 3 * 2;

        //    double LineLengthThreshold = LineCount * Math.Abs(LineNext) * 0.7;

        //    bool isFindLine = false;

        //    double Panel_Col2 = Panel_Col1 + LineCount * LineNext;

        //    int FindCount = (int)((AmplitudeThreshold - MinAmplitudeThreshold) / 3);

        //    for (int i = 0; i < FindCount; i++)
        //    {
        //        _hAllRow = PanelMeasurePoints(_hGrayImge, PanelRowStart, PanelRowEnd, Panel_Col1, LineCount, LineNext, AmplitudeThreshold, out _hAllCol);

        //        if (mode == "teach")
        //        {
        //            Bitmap bmp = ShowImagePoint(_hMiddleImage, _hAllRow, _hAllCol);

        //            string Type = "Inspect Result";
        //            string Name = "PanelMeasurePoints";
        //            string Description = "Panel Measure Pos - All position search results (Attempt " + (i + 1).ToString() + ")";
        //            List<List<Point>> PanelPointList = AddPointsFromHTuple(_hAllRow, _hAllCol);

        //            DebugResult debugResult = GenerateDebugResult(bmp, Type, Name, Description, false, PanelPointList);

        //            FindPanelCountResultList.Add(debugResult);
        //        }

        //        _hAllMaxRow = Find_MaxRow(_hGrayImge, _hAllRow, _hAllCol);

        //        isFindLine = Find_PanelLine_TwoCamera(_hGrayImge, _hAllMaxRow, _hAllRow, _hAllCol, Panel_Col1, LeastPoint, LineLengthThreshold, out _hNear_Row, out _hNear_Col, out _hN_m, out _hN_b, out _hN_A, out _hN_B, out _hN_C);

        //        if (mode == "teach")
        //        {
        //            Bitmap bmp = ShowImagePoint(_hMiddleImage, _hNear_Row, _hNear_Col);

        //            string Type = "Inspect Result";
        //            string Name = "Find_PanelLine";
        //            string Description = "Panel Nearby Point - Search Results (Attempt " + (i + 1).ToString() + ")";
        //            List<List<Point>> PanelPointList = AddPointsFromHTuple(_hNear_Row, _hNear_Col);

        //            DebugResult debugResult = GenerateDebugResult(bmp, Type, Name, Description, false, PanelPointList);

        //            FindPanelCountResultList.Add(debugResult);
        //        }

        //        if (isFindLine)
        //        {
        //            _hDis = (_hN_A * _hNear_Col + _hN_B * _hNear_Row + _hN_C).TupleAbs() / (_hN_A * _hN_A + _hN_B * _hN_B).TupleSqrt();

        //            _hDisIndices = FindOutlier(_hDis);

        //            _hNear_Row = _hNear_Row.TupleRemove(_hDisIndices);
        //            _hNear_Col = _hNear_Col.TupleRemove(_hDisIndices);

        //            if (mode == "teach")
        //            {
        //                Bitmap bmp = ShowImagePoint(_hMiddleImage, _hNear_Row, _hNear_Col);

        //                string Type = "Inspect Result";
        //                string Name = "Remove Outliers";
        //                string Description = "Panel nearby point search results after removing outliers";
        //                List<List<Point>> PanelPointList = AddPointsFromHTuple(_hNear_Row, _hNear_Col);

        //                DebugResult debugResult = GenerateDebugResult(bmp, Type, Name, Description, false, PanelPointList);

        //                FindPanelCountResultList.Add(debugResult);
        //            }

        //            Count = 1;

        //            break;
        //        }

        //        AmplitudeThreshold = AmplitudeThreshold - 3;
        //    }

        //    if (Count == 1)
        //    {
        //        _hReturn_AllRow = _hAllRow;
        //        _hReturn_AllCol = _hAllCol;
        //        _hReturn_NearRow = _hNear_Row;
        //        _hReturn_NearCol = _hNear_Col;
        //        _hReturn_N_m = _hN_m;
        //        _hReturn_N_b = _hN_b;
        //        _hReturn_N_A = _hN_A;
        //        _hReturn_N_B = _hN_B;
        //        _hReturn_N_C = _hN_C;
        //    }

        //    _hGrayImge.Dispose();

        //    _hImageWidth.Dispose();
        //    _hImageHeight.Dispose();
        //    _hAllRow.Dispose();
        //    _hAllCol.Dispose();
        //    _hAllMaxRow.Dispose();
        //    _hNear_Row.Dispose();
        //    _hNear_Col.Dispose();
        //    _hN_m.Dispose();
        //    _hN_b.Dispose();
        //    _hN_A.Dispose();
        //    _hN_B.Dispose();
        //    _hN_C.Dispose();
        //    _hDis.Dispose();
        //    _hDisIndices.Dispose();

        //    return Count;
        //}

        private int FindPanelEdgeCount_TwoCamera(HImage _hImage, double Edge_ThresholdMin, double Edge_ThresholdMax, double Edge_Area, double Edge_AreaRowDistance, out List<DebugResult> FindPanelCountResultList)
        {
            // ========================= 返回參數初始化 =========================
            int Count = 0;

            FindPanelCountResultList = new List<DebugResult>();

            // ========================= Halcon 物件宣告 =========================
            HImage _hImg = null;
            HImage _hGrayImage = null;
            HRegion _hRegion = null;
            HRegion _hConnectedRegions = null;
            HRegion _hSelectedRegions = null;

            HTuple _hSelRow = null, _hSelCol = null, _hSelArea = null;
            HTuple _hImageWidth = null;
            HTuple _hImageHeight = null;
            HTuple _hArea1 = null;
            HTuple _hRow1 = null;
            HTuple _hCol1 = null;

            double maxArea = 0, distance = 0;

            try
            {
                _hImg = _hImage.CopyImage();
                _hGrayImage = _hImg.Rgb1ToGray();

                _hImageWidth = new HTuple();
                _hImageHeight = new HTuple();
                _hGrayImage.GetImageSize(out _hImageWidth, out _hImageHeight);

                // 1. 二值化與連通域分析
                _hRegion = _hGrayImage.Threshold(new HTuple(Edge_ThresholdMin), new HTuple(Edge_ThresholdMax));
                _hConnectedRegions = _hRegion.Connection();

                // 計算各區域面積與中心
                _hArea1 = _hConnectedRegions.AreaCenter(out _hRow1, out _hCol1);

                // 💡 防錯保護：如果二值化完全沒抓到東西，TupleLength 會是 0，此時直接判定為 0 並結束
                if (_hArea1 == null || _hArea1.TupleLength() == 0)
                {
                    return 0;
                }

                // 2. 篩選最大面積的 Blob
                maxArea = _hArea1.TupleMax().D;
                _hSelectedRegions = _hConnectedRegions.SelectShape(new HTuple("area"), "and", new HTuple(maxArea), new HTuple("max"));

                // 3. 繪製並儲存 Teach 歷程圖片
                _hImg.OverpaintRegion(_hSelectedRegions, new HTuple(255, 0, 0), "fill");

                // 4. 判定邏輯 1：面積是否大於設定值
                if (maxArea > Edge_Area)
                {
                    Count = 1;
                }

                // 5. 判定邏輯 2：計算最大 Blob 中心與影像中心 Row 的絕對距離
                // 先獲取篩選後的單一 Region 的中心


                _hSelArea = _hSelectedRegions.AreaCenter(out _hSelRow, out _hSelCol);
                if (_hSelRow != null && _hSelRow.TupleLength() > 0)
                {
                    distance = Math.Abs(_hSelRow[0].D - (_hImageHeight[0].D / 2.0));
                    if (distance > Edge_AreaRowDistance)
                    {
                        Count = 0; // 偏離過遠，判定為無 Panel 或是雜訊
                    }
                }
            }
            catch (Exception ex)
            {
                // 💡 可透過 GPM 系統的 Log 框架拋出異常，避免直接死機
                // ReportLog($"Error in FindPanelEdgeCount_TwoCamera: {ex.Message}");
                Count = 0;
            }
            finally
            {

                if (mode == "teach")
                {

                    Bitmap bmp = ShowImagePoint(_hImg);

                    DebugResult debugResult = GenerateDebugResult(bmp, "CountSlice_TwoCamera", "FindPanelEdgeCount", "Result");

                    debugResult.Variables.Add(new SingleVariableMetric("Edge_ThresholdMin", "Threshold", Edge_ThresholdMin));
                    debugResult.Variables.Add(new SingleVariableMetric("Edge_ThresholdMax", "Threshold", Edge_ThresholdMax));
                    debugResult.Variables.Add(new SingleVariableMetric("maxArea", "Threshold Max Area", maxArea));
                    debugResult.Variables.Add(new SingleVariableMetric("Edge_Area", "Threshold Max Area Threshold", Edge_Area));
                    debugResult.Variables.Add(new SingleVariableMetric("_hSelRow", "Threshold Max Area Row", _hSelRow));
                    debugResult.Variables.Add(new SingleVariableMetric("_hSelCol", "Threshold Max Area Col", _hSelCol));
                    debugResult.Variables.Add(new SingleVariableMetric("distance", "Threshold Max Area Row Distance", distance));
                    debugResult.Variables.Add(new SingleVariableMetric("Count", "Panel Count", Count));

                    debugResult.Variables.Add(new SingleVariableMetric("Edge_Area", "Threshold Max Area Threshold", Edge_Area));

                    FindPanelCountResultList.Add(debugResult);
                }

                // ====================== 在 finally 區塊強制且安全地進行資源釋放 ======================
                _hImg?.Dispose();
                _hGrayImage?.Dispose();
                _hRegion?.Dispose();
                _hConnectedRegions?.Dispose();
                _hSelectedRegions?.Dispose();

                _hImageWidth?.Dispose();
                _hImageHeight?.Dispose();
                _hArea1?.Dispose();
                _hRow1?.Dispose();
                _hCol1?.Dispose();

                _hSelRow?.Dispose();
                _hSelCol?.Dispose();
                _hSelArea?.Dispose();
            }

            return Count;
        }

        //private int FindPanelThreshold(HImage _hMiddleImage, MappingParameter mappingParameter, out List<DebugResult> FindPanelCountResultList)
        //{
        //    HImage _hGrayImge = new HImage();

        //    HTuple _hImageWidth = new HTuple(), _hImageHeight = new HTuple();

        //    HRegion _hRegion = new HRegion(), _hConnectedRegion = new HRegion();

        //    HTuple _hArea = new HTuple(), _hRow = new HTuple(), _hCol = new HTuple();

        //    // ========================= 返回參數 =========================

        //    FindPanelCountResultList = new List<DebugResult>();

        //    // ========================= 返回參數 =========================

        //    int Count = 0;

        //    _hGrayImge = _hMiddleImage.Rgb1ToGray();

        //    _hGrayImge.GetImageSize(out _hImageWidth, out _hImageHeight);

        //    _hRegion = _hGrayImge.Threshold(new HTuple(mappingParameter.TwoCamera.CountSlice.ThresholdMin), new HTuple(mappingParameter.TwoCamera.CountSlice.ThresholdMax));

        //    _hConnectedRegion = _hRegion.Connection();

        //    _hArea = _hConnectedRegion.AreaCenter(out _hRow, out _hCol);

        //    _hMiddleImage.OverpaintRegion(_hRegion, new HTuple(255, 0, 0), "fill");

        //    if (mode == "teach")
        //    {
        //        Bitmap bmp = ShowImagePoint(_hMiddleImage);

        //        string Type = "Inspect Result";
        //        string Name = "Count Slice Threshold";
        //        string Description = "Threshold";

        //        DebugResult debugResult = GenerateDebugResult(bmp, Type, Name, Description, false);

        //        FindPanelCountResultList.Add(debugResult);
        //    }

        //    if (_hArea.TupleMax() > mappingParameter.TwoCamera.CountSlice.ThresholdArea)
        //    {
        //        Count = 1;
        //    }

        //    _hGrayImge.Dispose();

        //    _hImageWidth.Dispose();
        //    _hImageHeight.Dispose();

        //    _hRegion.Dispose();
        //    _hArea.Dispose();
        //    _hRow.Dispose();
        //    _hCol.Dispose();

        //    return Count;
        //}

        //private HTuple PanelMeasurePoints(HImage _hImage, double PanelRowStart, double PanelRowEnd, double Panel_Col1, double LineCount, double LineNext, double AmplitudeThreshold, out HTuple _hAllCol1)
        //{
        //    HTuple _hAllRow1 = new HTuple();

        //    _hAllCol1 = new HTuple();

        //    // ========================= 返回參數 =========================

        //    HTuple _hRow = new HTuple(), _hCol = new HTuple();
        //    HTuple _hAmplitude = new HTuple(), _hDistance = new HTuple();

        //    // ========================= 返回參數 =========================

        //    int RoiWidthLen2 = 5;

        //    for (int i = 0; i < (LineCount + 1); i++)
        //    {
        //        double RowStart = PanelRowStart;
        //        double ColStart = Panel_Col1 + LineNext * i;
        //        double RowEnd = PanelRowEnd;
        //        double ColEnd = Panel_Col1 + LineNext * i;

        //        _hRow = MeasurePosFindPoint(_hImage.CopyImage(), RowStart, ColStart, RowEnd, ColEnd, RoiWidthLen2, AmplitudeThreshold, out _hCol, out _hAmplitude, out _hDistance);

        //        _hAllRow1 = _hAllRow1.TupleConcat(_hRow);
        //        _hAllCol1 = _hAllCol1.TupleConcat(_hCol);
        //    }

        //    return _hAllRow1;
        //}

        //private HTuple MeasurePosFindPoint(HImage _hImage, double RowStart, double ColStart, double RowEnd, double ColEnd, double RoiWidthLen2, double AmplitudeThreshold, out HTuple _hCol, out HTuple _hAmplitude, out HTuple _hDistance)
        //{
        //    HMeasure MsrHandle_Measure_01_1 = new HMeasure();

        //    HTuple _hImageWidth = new HTuple(), _hImageHeight = new HTuple();

        //    // ========================= 返回參數 =========================

        //    HTuple _hRow = new HTuple();

        //    _hCol = new HTuple();
        //    _hAmplitude = new HTuple();
        //    _hDistance = new HTuple();

        //    // ========================= 返回參數 =========================

        //    _hImage.GetImageSize(out _hImageWidth, out _hImageHeight);


        //    if ((RowEnd - RowStart) < 5)
        //    {
        //        RowStart = RowStart - 2;

        //        RowEnd = RowEnd + 2;
        //    }

        //    double LineRowStart_Measure_01_1 = RowStart;
        //    double LineColumnStart_Measure_01_1 = ColStart;
        //    double LineRowEnd_Measure_01_1 = RowEnd;
        //    double LineColumnEnd_Measure_01_1 = ColEnd;

        //    double TmpCtrl_Row = 0.5 * (LineRowStart_Measure_01_1 + LineRowEnd_Measure_01_1);
        //    double TmpCtrl_Column = 0.5 * (LineColumnStart_Measure_01_1 + LineColumnEnd_Measure_01_1);
        //    double TmpCtrl_Dr = LineRowStart_Measure_01_1 - LineRowEnd_Measure_01_1;
        //    double TmpCtrl_Dc = LineColumnEnd_Measure_01_1 - LineColumnStart_Measure_01_1;

        //    double TmpCtrl_Phi = Math.Atan2(TmpCtrl_Dr, TmpCtrl_Dc);

        //    double TmpCtrl_Len1 = 0.5 * Math.Sqrt(TmpCtrl_Dr * TmpCtrl_Dr + TmpCtrl_Dc * TmpCtrl_Dc);
        //    double TmpCtrl_Len2 = RoiWidthLen2;

        //    MsrHandle_Measure_01_1.GenMeasureRectangle2(TmpCtrl_Row, TmpCtrl_Column, TmpCtrl_Phi, TmpCtrl_Len1, TmpCtrl_Len2, _hImageWidth[0], _hImageHeight[0], "nearest_neighbor");

        //    MsrHandle_Measure_01_1.MeasurePos(_hImage, 1, AmplitudeThreshold, "all", "all", out _hRow, out _hCol, out _hAmplitude, out _hDistance);

        //    return _hRow;
        //}

        //private HTuple Find_MaxRow(HImage _hImage, HTuple _hAllRow, HTuple _hAllCol)
        //{
        //    HTuple _hTempRow = new HTuple(), _hTempCol = new HTuple();

        //    // ========================= 返回參數 =========================

        //    HTuple _hAllMaxRow = new HTuple();

        //    // ========================= 返回參數 =========================


        //    _hTempRow = _hAllRow;
        //    _hTempCol = _hAllCol;
        //    for (; _hTempRow.TupleLength() != 0;)
        //    //while (_hTempRow.TupleLength() != 0)
        //    {
        //        HTuple _hMaxIndices = _hTempRow.TupleFind(_hTempRow.TupleMax());

        //        HTuple _hMaxRow = _hTempRow.TupleSelect(_hMaxIndices);
        //        HTuple _hMaxCol = _hTempCol.TupleSelect(_hMaxIndices);

        //        HTuple _hAngleMinRow = new HTuple();

        //        int Length1 = _hTempRow.TupleLength();

        //        for (int i = 0; i < Length1; i++)
        //        {
        //            HTuple _hAngle = (_hTempRow.TupleSelect(i) - _hMaxRow).TupleAbs().TupleAtan2((_hAllCol.TupleSelect(i) - _hMaxCol).TupleAbs());

        //            HTuple _hDeg = _hAngle.TupleDeg();

        //            _hAngleMinRow = _hAngleMinRow.TupleConcat(_hDeg);

        //            _hAngle.Dispose();
        //            _hDeg.Dispose();
        //        }

        //        HTuple _hDisIndex = (_hTempRow - _hMaxRow).TupleAbs().TupleLessEqualElem(10);

        //        HTuple _hAngleIndex = _hAngleMinRow.TupleLessEqualElem(8);

        //        HTuple _hNearIndex = _hDisIndex.TupleOr(_hAngleIndex);

        //        HTuple _hNearIndices = _hNearIndex.TupleFind(1);

        //        _hAllMaxRow = _hAllMaxRow.TupleConcat(_hMaxRow);

        //        _hTempRow = _hTempRow.TupleRemove(_hNearIndices);
        //        _hTempCol = _hTempCol.TupleRemove(_hNearIndices);


        //        _hMaxIndices.Dispose();
        //        _hMaxRow.Dispose();
        //        _hMaxCol.Dispose();
        //        _hAngleMinRow.Dispose();
        //        _hDisIndex.Dispose();
        //        _hAngleIndex.Dispose();
        //        _hNearIndex.Dispose();
        //        _hNearIndices.Dispose();
        //    }


        //    _hTempRow.Dispose();
        //    _hTempCol.Dispose();

        //    return _hAllMaxRow;
        //}

        //private bool Find_PanelLine_ThreeCamera(HImage _hImage, HTuple _hAllMaxRow, HTuple _hAllRow, HTuple _hAllCol, double Panel_Col1, double LeastPoint, double LineLengthThreshold, out HTuple _hReturn_AllRow, out HTuple _hReturn_AllCol, out HTuple _hN_m, out HTuple _hN_b, out HTuple _hN_A, out HTuple _hN_B, out HTuple _hN_C)
        //{
        //    HXLDCont _hN_Contour = new HXLDCont();

        //    HTuple _hMinIndices = new HTuple(), _hMinRow = new HTuple(), _hMinCol = new HTuple();

        //    HTuple _hAngleMinRow = new HTuple(), _hAngle = new HTuple(), _hDeg = new HTuple();

        //    HTuple _hDisIndex = new HTuple(), _hAngleIndex = new HTuple(), _hNearIndex = new HTuple();

        //    HTuple _hNearIndices = new HTuple(), _hNowLinePointRow = new HTuple(), _hNowLinePointCol = new HTuple();

        //    HTuple _hN_StartRow = new HTuple(), _hLineLength = new HTuple();


        //    int Length1 = _hAllMaxRow.TupleLength();
        //    int Length2 = _hAllRow.TupleLength();

        //    // ========================= 返回參數 =========================

        //    _hReturn_AllRow = new HTuple();
        //    _hReturn_AllCol = new HTuple();

        //    _hN_m = new HTuple();
        //    _hN_b = new HTuple();
        //    _hN_A = new HTuple();
        //    _hN_B = new HTuple();
        //    _hN_C = new HTuple();

        //    bool isFindLine = false;

        //    // ========================= 返回參數 =========================

        //    for (int i = 0; i < Length1; i++)
        //    {
        //        _hMinIndices = _hAllRow.TupleFind(_hAllMaxRow.TupleSelect(i));

        //        _hMinRow = _hAllRow.TupleSelect(_hMinIndices[0]);
        //        _hMinCol = _hAllCol.TupleSelect(_hMinIndices[0]);

        //        _hAngleMinRow = new HTuple();

        //        for (int j = 0; j < Length2; j++)
        //        {
        //            _hAngle = (_hAllRow.TupleSelect(j) - _hMinRow).TupleAbs().TupleAtan2((_hAllCol.TupleSelect(j) - _hMinCol).TupleAbs());

        //            _hDeg = _hAngle.TupleDeg();

        //            _hAngleMinRow = _hAngleMinRow.TupleConcat(_hDeg);
        //        }

        //        _hDisIndex = (_hAllRow - _hMinRow).TupleAbs().TupleLessEqualElem(10);

        //        _hAngleIndex = _hAngleMinRow.TupleLessEqualElem(3);

        //        _hNearIndex = _hDisIndex.TupleOr(_hAngleIndex);

        //        _hNearIndices = _hNearIndex.TupleFind(1);

        //        // youyi [因已調整ROI框縮小範圍，所以直接考慮所有點計算]
        //        //_hNowLinePointRow = _hAllRow.TupleSelect(_hNearIndices);
        //        //_hNowLinePointCol = _hAllCol.TupleSelect(_hNearIndices);

        //        _hNowLinePointRow = _hAllRow;
        //        _hNowLinePointCol = _hAllCol;

        //        int NowPointLength = _hNowLinePointRow.TupleUniq().TupleLength();

        //        bool CountPointsOK = false, LineLengthOK = false;

        //        if (NowPointLength > 3)
        //        {
        //            _hN_Contour = FitLineAndPointDistance(_hNowLinePointRow, _hNowLinePointCol, out _hN_m, out _hN_b, out _hN_A, out _hN_B, out _hN_C, out HTuple _hN_Dis, out HTuple _hN_Pos, out HTuple _hN_angle);

        //            _hN_StartRow = _hN_m * Panel_Col1 + _hN_b;

        //            CountPointsOK = NowPointLength > LeastPoint;

        //            _hLineLength = _hNowLinePointCol.TupleMax() - _hNowLinePointCol.TupleMin();

        //            LineLengthOK = _hLineLength > LineLengthThreshold;
        //        }

        //        if (CountPointsOK && LineLengthOK)
        //        {
        //            _hReturn_AllRow = _hNowLinePointRow;
        //            _hReturn_AllCol = _hNowLinePointCol;

        //            isFindLine = true;

        //            break;
        //        }
        //    }


        //    _hN_Contour.Dispose();

        //    _hMinIndices.Dispose();
        //    _hMinRow.Dispose();
        //    _hMinCol.Dispose();
        //    _hAngleMinRow.Dispose();
        //    _hAngle.Dispose();
        //    _hDeg.Dispose();
        //    _hDisIndex.Dispose();
        //    _hAngleIndex.Dispose();
        //    _hNearIndex.Dispose();
        //    _hNearIndices.Dispose();
        //    _hNowLinePointRow.Dispose();
        //    _hNowLinePointCol.Dispose();
        //    _hN_StartRow.Dispose();
        //    _hLineLength.Dispose();

        //    return isFindLine;
        //}

        //private bool Find_PanelLine_TwoCamera(HImage _hImage, HTuple _hAllMaxRow, HTuple _hAllRow, HTuple _hAllCol, double Panel_Col1, double LeastPoint, double LineLengthThreshold, out HTuple _hReturn_AllRow, out HTuple _hReturn_AllCol, out HTuple _hN_m, out HTuple _hN_b, out HTuple _hN_A, out HTuple _hN_B, out HTuple _hN_C)
        //{
        //    HXLDCont _hN_Contour = new HXLDCont();

        //    HTuple _hMinIndices = new HTuple(), _hMinRow = new HTuple(), _hMinCol = new HTuple();

        //    HTuple _hAngleMinRow = new HTuple(), _hAngle = new HTuple(), _hDeg = new HTuple();

        //    HTuple _hDisIndex = new HTuple(), _hAngleIndex = new HTuple(), _hNearIndex = new HTuple();

        //    HTuple _hNearIndices = new HTuple(), _hNowLinePointRow = new HTuple(), _hNowLinePointCol = new HTuple();

        //    HTuple _hN_StartRow = new HTuple(), _hLineLength = new HTuple();


        //    int Length1 = _hAllMaxRow.TupleLength();
        //    int Length2 = _hAllRow.TupleLength();

        //    // ========================= 返回參數 =========================

        //    _hReturn_AllRow = new HTuple();
        //    _hReturn_AllCol = new HTuple();

        //    _hN_m = new HTuple();
        //    _hN_b = new HTuple();
        //    _hN_A = new HTuple();
        //    _hN_B = new HTuple();
        //    _hN_C = new HTuple();

        //    bool isFindLine = false;

        //    // ========================= 返回參數 =========================

        //    for (int i = 0; i < Length1; i++)
        //    {
        //        _hMinIndices = _hAllRow.TupleFind(_hAllMaxRow.TupleSelect(i));

        //        _hMinRow = _hAllRow.TupleSelect(_hMinIndices[0]);
        //        _hMinCol = _hAllCol.TupleSelect(_hMinIndices[0]);

        //        _hAngleMinRow = new HTuple();

        //        for (int j = 0; j < Length2; j++)
        //        {
        //            _hAngle = (_hAllRow.TupleSelect(j) - _hMinRow).TupleAbs().TupleAtan2((_hAllCol.TupleSelect(j) - _hMinCol).TupleAbs());

        //            _hDeg = _hAngle.TupleDeg();

        //            _hAngleMinRow = _hAngleMinRow.TupleConcat(_hDeg);
        //        }

        //        _hDisIndex = (_hAllRow - _hMinRow).TupleAbs().TupleLessEqualElem(10);

        //        _hAngleIndex = _hAngleMinRow.TupleLessEqualElem(3);

        //        _hNearIndex = _hDisIndex.TupleOr(_hAngleIndex);

        //        _hNearIndices = _hNearIndex.TupleFind(1);


        //        //_hNowLinePointRow = _hAllRow.TupleSelect(_hNearIndices);
        //        //_hNowLinePointCol = _hAllCol.TupleSelect(_hNearIndices);

        //        // youyi [因已調整ROI框縮小範圍，所以直接考慮所有點計算]
        //        _hNowLinePointRow = _hAllRow;
        //        _hNowLinePointCol = _hAllCol;

        //        int NowPointLength = _hNowLinePointRow.TupleUniq().TupleLength();

        //        bool CountPointsOK = false, LineLengthOK = false;

        //        if (NowPointLength > 3)
        //        {
        //            _hN_Contour = FitLineAndPointDistance(_hNowLinePointRow, _hNowLinePointCol, out _hN_m, out _hN_b, out _hN_A, out _hN_B, out _hN_C, out HTuple _hN_Dis, out HTuple _hN_Pos, out HTuple _hN_angle);

        //            _hN_StartRow = _hN_m * Panel_Col1 + _hN_b;

        //            CountPointsOK = NowPointLength > LeastPoint;

        //            _hLineLength = _hNowLinePointCol.TupleMax() - _hNowLinePointCol.TupleMin();

        //            LineLengthOK = _hLineLength > LineLengthThreshold;
        //        }

        //        if (CountPointsOK && LineLengthOK)
        //        {
        //            _hReturn_AllRow = _hNowLinePointRow;
        //            _hReturn_AllCol = _hNowLinePointCol;

        //            isFindLine = true;

        //            break;
        //        }
        //    }


        //    _hN_Contour.Dispose();

        //    _hMinIndices.Dispose();
        //    _hMinRow.Dispose();
        //    _hMinCol.Dispose();
        //    _hAngleMinRow.Dispose();
        //    _hAngle.Dispose();
        //    _hDeg.Dispose();
        //    _hDisIndex.Dispose();
        //    _hAngleIndex.Dispose();
        //    _hNearIndex.Dispose();
        //    _hNearIndices.Dispose();
        //    _hNowLinePointRow.Dispose();
        //    _hNowLinePointCol.Dispose();
        //    _hN_StartRow.Dispose();
        //    _hLineLength.Dispose();

        //    return isFindLine;
        //}

        //private HXLDCont FitLineAndPointDistance(HTuple _hRow, HTuple _hCol, out HTuple _hm, out HTuple _hb, out HTuple _hA, out HTuple _hB, out HTuple _hC, out HTuple _hDis, out HTuple _hPos, out HTuple _hAngle)
        //{
        //    HXLDCont _hXLDcont = new HXLDCont();

        //    // ========================= 返回參數 =========================

        //    HXLDCont _hContour = new HXLDCont();

        //    _hm = new HTuple();
        //    _hb = new HTuple();
        //    _hA = new HTuple();
        //    _hB = new HTuple();
        //    _hC = new HTuple();
        //    _hDis = new HTuple();
        //    _hPos = new HTuple();
        //    _hAngle = new HTuple();

        //    // ========================= 返回參數 =========================

        //    try
        //    {
        //        _hXLDcont.GenContourPolygonXld(_hRow, _hCol);

        //        _hXLDcont.FitLineContourXld("huber", -1, 0, 5, 2, out HTuple _hRowBegin, out HTuple _hColBegin, out HTuple _hRowEnd, out HTuple _hColEnd, out HTuple _hM_Nr, out HTuple _hM_Nc, out HTuple _hDist);

        //        _hContour.GenContourPolygonXld(_hRowBegin.TupleConcat(_hRowEnd), _hColBegin.TupleConcat(_hColEnd));

        //        _hm = (_hRowEnd - _hRowBegin) / (_hColEnd - _hColBegin);
        //        _hb = _hRowBegin - _hColBegin * _hm;
        //        _hA = _hm;
        //        _hB = -1;
        //        _hC = _hb;
        //        _hDis = (_hA * _hCol + _hB * _hRow + _hC).TupleAbs() / (_hA * _hA + _hB * _hB).TupleSqrt();
        //        _hPos = _hA * _hCol + _hB * _hRow + _hC;
        //        _hAngle = _hM_Nr.TupleAtan2(_hM_Nc) - new HTuple(90).TupleRad();
        //        _hAngle = _hAngle.TupleDeg();
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return _hContour;
        //}

        //private HTuple FindOutlier(HTuple _hDis)
        //{
        //    HTuple _hMean = new HTuple(), _hDeviation = new HTuple(), _hMedian = new HTuple(), _hZ_score = new HTuple();

        //    HTuple _hMaxIndex = new HTuple(), _hMinIndex = new HTuple(), _hIndex = new HTuple();

        //    HTuple _hMAD = new HTuple(), _hRobust_Z_score = new HTuple();

        //    // ========================= 返回參數 =========================

        //    HTuple _hIndices = new HTuple();

        //    // ========================= 返回參數 =========================


        //    try
        //    {
        //        _hMean = _hDis.TupleMean();

        //        _hDeviation = _hDis.TupleDeviation();

        //        _hMedian = _hDis.TupleMedian();

        //        _hZ_score = (_hDis - _hMean) / _hDeviation;

        //        _hMaxIndex = _hZ_score.TupleGreaterEqualElem(3);

        //        _hMinIndex = _hZ_score.TupleLessEqualElem(-3);

        //        _hIndex = _hMaxIndex.TupleOr(_hMinIndex);

        //        _hIndices = _hIndex.TupleFind(1);

        //        _hMAD = (_hDis - _hMedian).TupleAbs().TupleMedian();

        //        _hRobust_Z_score = 0.6745 * (_hDis - _hMedian) / _hMAD;

        //        _hMaxIndex = _hRobust_Z_score.TupleGreaterEqualElem(3);

        //        _hMinIndex = _hRobust_Z_score.TupleLessEqualElem(-3);

        //        _hIndex = _hMaxIndex.TupleOr(_hMinIndex);

        //        _hIndices = _hIndex.TupleFind(1);
        //    }
        //    catch (Exception ex)
        //    {

        //    }


        //    _hMean.Dispose();
        //    _hDeviation.Dispose();
        //    _hMedian.Dispose();
        //    _hZ_score.Dispose();

        //    _hMaxIndex.Dispose();
        //    _hMinIndex.Dispose();
        //    _hIndex.Dispose();

        //    _hMAD.Dispose();
        //    _hRobust_Z_score.Dispose();

        //    return _hIndices;
        //}

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

        private DebugResult GenerateDebugResult(Bitmap tbmp, string type, string name, string description)
        {
            DebugResult debugResult = new DebugResult();

            debugResult.Image = (Bitmap)tbmp.Clone();
            debugResult.Type = type;
            debugResult.Name = name;
            debugResult.Description = description;

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
