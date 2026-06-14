using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProcess
{
    public class SettingInfo
    {
        public string LoadImagePath = string.Empty;
        public string RecipePath = string.Empty;
        public string SaveResultPath = string.Empty;

        public int SlotNumber = 0;
        public string SelectedImageItem = string.Empty;
        public string DirImagePath = string.Empty;
    }

    public class FoupGlassFeature
    {
        // Glass Coordinate Position
        public PointF _pt = PointF.Empty;

        public double width = 0;

        public double height = 0;

        // Default Flass Status

        public GlassStatus glassStatus = GlassStatus.EmptySlices;

        // Dedault DetectParameter

        public double LeftGap = 0;

        public double RightGap = 0;

        public double Warpage = 0;

        public double Thickness = 0;

        public int CountSliceL = 0;

        public int CountSliceR = 0;
    }

    public class ROI
    {
        public int R1 = 0;

        public int C1 = 0;

        public int R2 = 0;

        public int C2 = 0;
    }

    /// <summary>
    /// ROI框選
    /// </summary>
    public class ROIList
    {
        // 將不同相機模式分開，結構更清晰
        public ThreeCameraROI ThreeCamera { get; set; } = new ThreeCameraROI();
        public TwoCameraROI TwoCamera { get; set; } = new TwoCameraROI();
    }

    // 三相機
    public class ThreeCameraROI
    {
        public PanelROI CountSlice { get; set; } = new PanelROI();
        public PanelROI Stack { get; set; } = new PanelROI();
        public PanelROI Warpage { get; set; } = new PanelROI();
        public PanelROI Gap { get; set; } = new PanelROI();

        // 使用字典或清單來管理不同影像的 ROI 列表
        public Dictionary<string, List<ROI>> Thickness { get; set; } = new Dictionary<string, List<ROI>>
        {
            { "Left", new List<ROI>() },
            { "Middle", new List<ROI>() },
            { "Right", new List<ROI>() }
        };
    }

    // 兩相機
    public class TwoCameraROI
    {
        public PanelROI CountSlice { get; set; } = new PanelROI();
        public PanelROI Stack { get; set; } = new PanelROI();
        public PanelROI Warpage { get; set; } = new PanelROI();

        public Dictionary<string, List<ROI>> Thickness { get; set; } = new Dictionary<string, List<ROI>>
        {
            { "Left", new List<ROI>() },
            { "Right", new List<ROI>() }
        };
    }

    // 封裝常用的 Panel 組合
    public class PanelROI
    {
        //Panel
        public ROI LeftImage_Panel { get; set; } = new ROI();
        public ROI RightImage_Panel { get; set; } = new ROI();
        public ROI MiddleImage_LeftPanel { get; set; } = new ROI();
        public ROI MiddleImage_RightPanel { get; set; } = new ROI();

        // EdgePanel
        public ROI LeftImage_EdgePanel { get; set; } = new ROI();
        public ROI RightImage_EdgePanel { get; set; } = new ROI();

        // HBarPanel
        public ROI LeftImage_HBarPanel { get; set; } = new ROI();
        public ROI RightImage_HBarPanel { get; set; } = new ROI();

        // 💡 新增：自動將內部的所有 ROI 依照名稱關鍵字分類，並轉換成 Rectangle
        public Dictionary<string, List<Rectangle>> GetMappedRectangles()
        {
            var map = new Dictionary<string, List<Rectangle>>
        {
            { "Left", new List<Rectangle>() },
            { "Middle", new List<Rectangle>() },
            { "Right", new List<Rectangle>() }
        };

            // 收集所有屬性與它們對應的相機標籤
            var allRois = new[]
            {
            new { Roi = LeftImage_Panel, Cam = "Left" },
            new { Roi = LeftImage_EdgePanel, Cam = "Left" },
            new { Roi = LeftImage_HBarPanel, Cam = "Left" },

            new { Roi = RightImage_Panel, Cam = "Right" },
            new { Roi = RightImage_EdgePanel, Cam = "Right" },
            new { Roi = RightImage_HBarPanel, Cam = "Right" },

            new { Roi = MiddleImage_LeftPanel, Cam = "Middle" },
            new { Roi = MiddleImage_RightPanel, Cam = "Middle" }
        };

            foreach (var item in allRois)
            {
                var roi = item.Roi;
                // 驗證是否有值
                if (roi != null && roi.C2 > roi.C1 && roi.R2 > roi.R1)
                {
                    Rectangle rect = new Rectangle(roi.C1, roi.R1, roi.C2 - roi.C1, roi.R2 - roi.R1);
                    map[item.Cam].Add(rect);
                }
            }

            return map;
        }
    }

    /// <summary>
    /// 演算法Step顯示
    /// </summary>
    public class DebugResult
    {
        public Bitmap Image { get; set; }           // 檢測歷程圖
        public string Type { get; set; }            // 結果類型 (e.g., "EdgeCheck", "Alignment")
        public string Name { get; set; }            // 步驟名稱 (e.g., "LeftEdge_Measure")
        public string Description { get; set; }     // 詳細描述

        // ✨ 依照變數型態，只區分為這兩個清單
        public List<ParamMetric> Parameters { get; set; } = new List<ParamMetric>();
        public List<SingleVariableMetric> Variables { get; set; } = new List<SingleVariableMetric>();
        public List<ArrayMetric> Arrays { get; set; } = new List<ArrayMetric>();
    }

    /// <summary>
    /// 所有量測數據的基底
    /// <summary>
    public abstract class MetricBase
    {
        public string Name { get; set; }        // 變數名稱 (e.g., "M", "Intercept_B", "All_Row")
        public string Description { get; set; } // 變數描述 (e.g., "直線斜率", "量測到的 Y 座標陣列")
    }

    /// <summary>
    /// 類型一：參數（支援 int, double, string 等）
    /// <summary>
    public class ParamMetric : MetricBase
    {
        public double Value { get; set; }       // 儲存單一數值
        public ParamMetric(string name, string desc, double value)
        {
            this.Name = name;
            this.Description = desc;
            this.Value = value;
        }
        public override string ToString() => $"{Name} ({Description}) = {Value}";
    }

    /// <summary>
    /// 類型二：單一變數（支援 int, double, string 等）
    /// <summary>
    public class SingleVariableMetric : MetricBase
    {
        public object Value { get; set; }       // 儲存單一數值

        public SingleVariableMetric(string name, string desc, object value)
        {
            this.Name = name;
            this.Description = desc;
            this.Value = value;
        }

        public override string ToString() => $"{Name} ({Description}) = {Value}";
    }

    /// <summary>
    /// 類型三：陣列與點群（統一用原生的 List<object> 或 List<double>）
    /// </summary>
    public class ArrayMetric : MetricBase
    {
        public List<object> Values { get; set; } = new List<object>(); // 儲存陣列資料

        public ArrayMetric(string name, string desc)
        {
            this.Name = name;
            this.Description = desc;
        }

        // 💡 關鍵建構子：傳入 Halcon HTuple 陣列，直接拆箱轉成 C# List 並釋放指標
        public ArrayMetric(string name, string desc, HTuple hTuple) : this(name, desc)
        {
            if (hTuple == null || hTuple.TupleLength() == 0) return;

            int len = hTuple.TupleLength();
            for (int i = 0; i < len; i++)
            {
                // 自動識別 Halcon 內部型別轉入 C#
                if (hTuple[i].Type == HTupleType.DOUBLE)
                    Values.Add(hTuple[i].D);
                else if (hTuple[i].Type == HTupleType.INTEGER)
                    Values.Add(hTuple[i].I);
                else
                    Values.Add(hTuple[i].S);
            }
        }
    }

    /// <summary>
    /// 演算法參數設定
    /// </summary>
    public class MappingParameter
    {
        public ThreeCameraParameter ThreeCamera { get; set; } = new ThreeCameraParameter();
        public TwoCameraParameter TwoCamera { get; set; } = new TwoCameraParameter();
    }
    public class ThreeCameraParameter
    {
        public CountSliceParameter CountSlice { get; set; } = new CountSliceParameter();
        public StackParameter Stack { get; set; } = new StackParameter();
        public ThicknessParameter Thickness { get; set; } = new ThicknessParameter();
        public WarpageParameter Warpage { get; set; } = new WarpageParameter();
        public GapParameter Gap { get; set; } = new GapParameter();
        public StitchParameter Stitch { get; set; } = new StitchParameter();
        public SaveSetting SaveSetting { get; set; } = new SaveSetting();
        public AlgorithmByPass AlgorithmByPass { get; set; } = new AlgorithmByPass();
    }
    public class TwoCameraParameter
    {
        public CountSliceParameter CountSlice { get; set; } = new CountSliceParameter();
        public StackParameter Stack { get; set; } = new StackParameter();
        public ThicknessParameter Thickness { get; set; } = new ThicknessParameter();
        public WarpageParameter Warpage { get; set; } = new WarpageParameter();
        public StitchParameter Stitch { get; set; } = new StitchParameter();
        public SaveSetting SaveSetting { get; set; } = new SaveSetting();
        public AlgorithmByPass AlgorithmByPass { get; set; } = new AlgorithmByPass();
    }
    public class CountSliceParameter
    {
        public double L_Panel_threshold_value { get; set; } = 30;
        public double R_Panel_threshold_value { get; set; } = 30;
        public double ML_Panel_threshold_value { get; set; } = 30;
        public double MR_Panel_threshold_value { get; set; } = 30;
        public double L_ROI_MiddleRow_UpDistance { get; set; } = 35;
        public double L_ROI_MiddleRow_DownDistance { get; set; } = 50;
        public double R_ROI_MiddleRow_UpDistance { get; set; } = 40;
        public double R_ROI_MiddleRow_DownDistance { get; set; } = 70;
        public double L_Bar_threshold_value { get; set; } = 30;
        public double R_Bar_threshold_value { get; set; } = 30;

        public double Bar_Distance { get; set; } = 95;
        public double Slant_ABS_Value { get; set; } = 0.035;
        public double MinAmplitudeThreshold { get; set; } = 5;
        public double CutPoint1 { get; set; } = 450;
        public double CutPoint2 { get; set; } = 150;
        public double ThresholdMin { get; set; } = 100;
        public double ThresholdMax { get; set; } = 255;
        public double ThresholdArea { get; set; } = 70;
        public double LeftEdge_ThresholdMin { get; set; } = 0;
        public double LeftEdge_ThresholdMax { get; set; } = 50;
        public double RightEdge_ThresholdMin { get; set; } = 0;
        public double RightEdge_ThresholdMax { get; set; } = 90;
        public double LeftEdge_Area { get; set; } = 200;
        public double RightEdge_Area { get; set; } = 200;
        public double LeftEdge_AreaRowDistance { get; set; } = 7;
        public double RightEdge_AreaRowDistance { get; set; } = 7;
    }
    public class StackParameter
    {
        public double L_Panel_threshold_value { get; set; } = 30;
        public double R_Panel_threshold_value { get; set; } = 30;
        public double ML_Panel_threshold_value { get; set; } = 30;
        public double MR_Panel_threshold_value { get; set; } = 30;
        public double L_ROI_MiddleRow_UpDistance { get; set; } = 35;
        public double L_ROI_MiddleRow_DownDistance { get; set; } = 50;
        public double R_ROI_MiddleRow_UpDistance { get; set; } = 40;
        public double R_ROI_MiddleRow_DownDistance { get; set; } = 70;
        public double L_Bar_threshold_value { get; set; } = 30;
        public double R_Bar_threshold_value { get; set; } = 30;

        public double ChamferDistance { get; set; } = 15;
        public double PanelDistance { get; set; } = 10;
        public double Panel_threshold_value { get; set; } = 18;
        public double Binarization_threshold_Min_value { get; set; } = 80;
        public double Region_Width { get; set; } = 2;
        public double Region_Area { get; set; } = 5;
        public double MinAmplitudeThreshold { get; set; } = 5;
        public double Diff1_threshold_value { get; set; } = 30;
        public double Diff2_threshold_value { get; set; } = 30;
        public double hSecVal_threshold_value { get; set; } = 2;
        public double hDiffMiddleDis_threshold_value { get; set; } = 5;
    }
    public class ThicknessParameter
    {
        public double L_Panel_threshold_value { get; set; } = 30;
        public double R_Panel_threshold_value { get; set; } = 30;
        public double ML_Panel_threshold_value { get; set; } = 30;
        public double MR_Panel_threshold_value { get; set; } = 30;
        public double L_ROI_MiddleRow_UpDistance { get; set; } = 35;
        public double L_ROI_MiddleRow_DownDistance { get; set; } = 50;
        public double R_ROI_MiddleRow_UpDistance { get; set; } = 40;
        public double R_ROI_MiddleRow_DownDistance { get; set; } = 70;
        public double L_Bar_threshold_value { get; set; } = 30;
        public double R_Bar_threshold_value { get; set; } = 30;

        public double ChamferDistance { get; set; } = 10;
        public double MinAmplitudeThreshold { get; set; } = 5;
        public double PanelDistance { get; set; } = 10;
        public double Scale { get; set; } = 115;
    }
    public class WarpageParameter
    {
        public double L_Panel_threshold_value { get; set; } = 30;
        public double R_Panel_threshold_value { get; set; } = 30;
        public double ML_Panel_threshold_value { get; set; } = 30;
        public double MR_Panel_threshold_value { get; set; } = 30;
        public double L_ROI_MiddleRow_UpDistance { get; set; } = 35;
        public double L_ROI_MiddleRow_DownDistance { get; set; } = 50;
        public double R_ROI_MiddleRow_UpDistance { get; set; } = 40;
        public double R_ROI_MiddleRow_DownDistance { get; set; } = 70;
        public double L_Bar_threshold_value { get; set; } = 30;
        public double R_Bar_threshold_value { get; set; } = 30;

        public double Binarization_threshold_Case1_Min { get; set; } = 0;
        public double Binarization_threshold_Case1_Max { get; set; } = 100;
        public double Casel_Area { get; set; } = 100;
        public double Binarization_threshold_Case2_Min { get; set; } = 220;
        public double Binarization_threshold_Case2_Max { get; set; } = 255;
        public double Gray_threshold_value1 { get; set; } = 100;
        public double Gray_threshold_value2 { get; set; } = 130;
        public double White_Area { get; set; } = 250;
        public double White_Mean { get; set; } = 150;
        public double White_Width { get; set; } = 2;
        public double White_Height { get; set; } = 15;
        public double ScaleImage_Method1_Value { get; set; } = 3;
        public double Emphasize_Method3_Value { get; set; } = 5;
        public double Emphasize_Method4_Value { get; set; } = 2;
        public double MinThicknessError { get; set; } = 6;
        public double ChamferDistance { get; set; } = 10;
        public double MinAmplitudeThreshold { get; set; } = 5;
        public double Turn_ChamferDistance_threshold { get; set; } = 15;
        public double Turn_ChamferDistance { get; set; } = 3;
        public double Scale { get; set; } = 98;
        public double ThresholdMin { get; set; } = 0;
        public double ThresholdMax { get; set; } = 100;
    }
    public class GapParameter
    {
        public double L_Panel_threshold_value { get; set; } = 30;
        public double R_Panel_threshold_value { get; set; } = 30;
        public double ML_Panel_threshold_value { get; set; } = 30;
        public double MR_Panel_threshold_value { get; set; } = 30;
        public double L_ROI_MiddleRow_UpDistance { get; set; } = 35;
        public double L_ROI_MiddleRow_DownDistance { get; set; } = 50;
        public double R_ROI_MiddleRow_UpDistance { get; set; } = 40;
        public double R_ROI_MiddleRow_DownDistance { get; set; } = 70;
        public double L_Bar_threshold_value { get; set; } = 30;
        public double R_Bar_threshold_value { get; set; } = 30;

        public double Left_Bar_Slot_Row { get; set; } = 690;
        public double Left_Bar_Slot_Col { get; set; } = 1040;
        public double Right_Bar_Slot_Row { get; set; } = 667;
        public double Right_Bar_Slot_Col { get; set; } = 890;
        public double Left_HbarCol { get; set; } = 962;
        public double Right_HbarCol { get; set; } = 966;
        public double Left_Bar_Slot_Row_UpDistance { get; set; } = 15;
        public double Left_Bar_Slot_Row_DownDistance { get; set; } = 0;
        public double Left_HorEdgeCol_threshold_value { get; set; } = 10;
        public double Right_Bar_Slot_Row_UpDistance { get; set; } = 20;
        public double Right_Bar_Slot_Row_DownDistance { get; set; } = 0;
        public double Right_HorEdgeCol_threshold_value { get; set; } = 0;


        public double Binarization_threshold_Case1_Min { get; set; } = 0;
        public double Binarization_threshold_Case1_Max { get; set; } = 100;
        public double Case1_Area { get; set; } = 100;
        public double Binarization_threshold_Case2_Min { get; set; } = 220;
        public double Binarization_threshold_Case2_Max { get; set; } = 255;
        public double Gray_threshold_value1 { get; set; } = 100;
        public double Gray_threshold_value2 { get; set; } = 130;
        public double White_Area { get; set; } = 250;
        public double White_Mean { get; set; } = 150;
        public double White_Width { get; set; } = 2;
        public double White_Height { get; set; } = 15;
        public double ScaleImage_Method1_Value { get; set; } = 3;
        public double Emphasize_Method3_Value { get; set; } = 5;
        public double Emphasize_Method4_Value { get; set; } = 2;
        public double Scale { get; set; } = 98;
    }
    public class StitchParameter
    {
        public double SlotCount { get; set; } = 20;
        public double SingleSlotRowOffset1 { get; set; } = 0;
        public double SingleSlotRowOffset2 { get; set; } = -10;
        public double SingleSlotColOverlap1 { get; set; } = 217;
        public double SingleSlotColOverlap2 { get; set; } = 217;
        public double AllSlotCropHeight { get; set; } = 720;
        public double AllSlotNormalOverlap { get; set; } = 600;
        public double AllSlotSlantOverlap { get; set; } = 420;

        //public double[] AllSlotRowOverlap;
        public double AllSlotRowOverlap1 { get; set; } = 0;
        public double AllSlotRowOverlap2 { get; set; } = 0;
        public double AllSlotRowOverlap3 { get; set; } = 0;
        public double AllSlotRowOverlap4 { get; set; } = 0;
        public double AllSlotRowOverlap5 { get; set; } = 0;
        public double AllSlotRowOverlap6 { get; set; } = 0;
        public double AllSlotRowOverlap7 { get; set; } = 0;
        public double AllSlotRowOverlap8 { get; set; } = 0;
        public double AllSlotRowOverlap9 { get; set; } = 0;
        public double AllSlotRowOverlap10 { get; set; } = 0;
        public double AllSlotRowOverlap11 { get; set; } = 0;
        public double AllSlotRowOverlap12 { get; set; } = 0;
        public double AllSlotRowOverlap13 { get; set; } = 0;
        public double AllSlotRowOverlap14 { get; set; } = 0;
        public double AllSlotRowOverlap15 { get; set; } = 0;
        public double AllSlotRowOverlap16 { get; set; } = 0;
        public double AllSlotRowOverlap17 { get; set; } = 0;
        public double AllSlotRowOverlap18 { get; set; } = 0;
        public double AllSlotRowOverlap19 { get; set; } = 0;
        public double AllSlotRowOverlap20 { get; set; } = 0;
        public double AllSlotRowOverlap21 { get; set; } = 0;
        public double AllSlotRowOverlap22 { get; set; } = 0;
        public double AllSlotRowOverlap23 { get; set; } = 0;
        public double AllSlotRowOverlap24 { get; set; } = 0;
    }
    public class SaveSetting
    {
        public bool SaveLeftCameraImage { get; set; } = true;
        public bool SaveMiddleCameraImage { get; set; } = true;
        public bool SaveRightCameraImage { get; set; } = true;
        public bool SaveSingleSlotImage { get; set; } = true;
        public bool SaveAllSlotImage { get; set; } = true;
        public bool SaveRunTimeCsv { get; set; } = true;
        public bool SaveSlotResultCsv { get; set; } = true;
    }
    public class AlgorithmByPass
    {
        public bool ByPassThicknessAlgorithm { get; set; } = false;
        public bool ByPassWarpageAlgorithm { get; set; } = false;
        public bool ByPassGapAlgorithm { get; set; } = false;


        public bool Stack_isFindPanelPoint { get; set; } = true;
        public bool Stack_isGrayProject { get; set; } = true;
        public bool Stack_isThreshold { get; set; } = true;
        public bool Stack_isCalculateOutlier { get; set; } = true;

        public bool Thickness_isChamferCalculate { get; set; } = false;
    }
}
