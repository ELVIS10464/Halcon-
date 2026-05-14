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
        public string ImageParh = string.Empty;
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

    //public class ROIList
    //{
    //    /*********三相機*********/
    //    //CountSlice_ThreeCamera
    //    public ROI ThreeCamera_CountSlice_LeftImage_Panel_ROI = new ROI();
    //    public ROI ThreeCamera_CountSlice_RightImage_Panel_ROI = new ROI();
    //    public ROI ThreeCamera_CountSlice_MiddleImage_LeftPanel_ROI = new ROI();
    //    public ROI ThreeCamera_CountSlice_MiddleImage_RightPanel_ROI = new ROI();
    //    //Stack_ThreeCamera
    //    public ROI ThreeCamera_Stack_LeftImage_Panel_ROI = new ROI();
    //    public ROI ThreeCamera_Stack_RightImage_Panel_ROI = new ROI();
    //    public ROI ThreeCamera_Stack_MiddleImage_LeftPanel_ROI = new ROI();
    //    public ROI ThreeCamera_Stack_MiddleImage_RightPanel_ROI = new ROI();
    //    //Thickness_ThreeCamera
    //    public List<ROI> ThreeCamera_Thickness_LeftImage_ROIList = new List<ROI>();
    //    public List<ROI> ThreeCamera_Thickness_MiddleImage_ROIList = new List<ROI>();
    //    public List<ROI> ThreeCamera_Thickness_RightImage_ROIList = new List<ROI>();

    //    /*********兩相機*********/
    //    //CountSlice_TwoCamera
    //    public ROI TwoCamera_CountSlice_LeftImage_Panel_ROI = new ROI();
    //    public ROI TwoCamera_CountSlice_RightImage_Panel_ROI = new ROI();
    //    public ROI TwoCamera_CountSlice_LeftImage_EdgePanel_ROI = new ROI();
    //    public ROI TwoCamera_CountSlice_RightImage_EdgePanel_ROI = new ROI();
    //    //Stack_TwoCamera
    //    public ROI TwoCamera_Stack_LeftImage_Panel_ROI = new ROI();
    //    public ROI TwoCamera_Stack_RightImage_Panel_ROI = new ROI();
    //    //Thickness_TwoCamera
    //    public List<ROI> TwoCamera_Thickness_LeftImage_ROIList = new List<ROI>();
    //    public List<ROI> TwoCamera_Thickness_RightImage_ROIList = new List<ROI>();
    //}


    /// <summary>
    /// ROI框選
    /// </summary>
    public class ROIList
    {
        // 將不同相機模式分開，結構更清晰
        public CameraModeThree ThreeCamera { get; set; } = new CameraModeThree();
        public CameraModeTwo TwoCamera { get; set; } = new CameraModeTwo();
    }

    // 三相機
    public class CameraModeThree
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
    public class CameraModeTwo
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
    }


    /// <summary>
    /// 演算法Step顯示
    /// </summary>
    public class Equation
    {
        public string m = string.Empty;
        public string b = string.Empty;
    }
    public class DebugResult
    {
        public string Type = string.Empty;

        public string Name = string.Empty;

        public string Description = string.Empty;

        public bool _isComBoBoxSelect = false;

        public List<List<Point>> PanelPointList = null;

        public List<double> ThicknessList = null;

        public List<Equation> EquationList = null;

        public string InspectResult = string.Empty;

        public Bitmap Image = null;

        public double[] projection_x = null;

        public double[] projection_y = null;
    }
}
