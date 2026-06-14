using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProcess
{
    public class ReadROIINI
    {
        private IniManager iniManager;        

        public ROIList ReadRecipeINI(string INIPath, int CameraNumber)
        {
            iniManager = new IniManager(INIPath);

            ROIList rOIList = new ROIList();

            // CountSlice 
            var leftCountSlice = ReadROIsFromIni("CountSlice_LeftImage", "CountSlice_LeftImage_Count");
            var rightCountSlice = ReadROIsFromIni("CountSlice_RightImage", "CountSlice_RightImage_Count");
            var middleCountSlice = ReadROIsFromIni("CountSlice_MiddleImage", "CountSlice_MiddleImage_Count");

            // Stack
            var leftStack = ReadROIsFromIni("Stack_LeftImage", "Stack_LeftImage_Count");
            var rightStack = ReadROIsFromIni("Stack_RightImage", "Stack_RightImage_Count");
            var middleStack = ReadROIsFromIni("Stack_MiddleImage", "Stack_MiddleImage_Count");

            // Warpage
            var leftWarpage = ReadROIsFromIni("Warpage_LeftImage", "Warpage_LeftImage_Count");
            var rightWarpage = ReadROIsFromIni("Warpage_RightImage", "Warpage_RightImage_Count");
            var middleWarpage = ReadROIsFromIni("Warpage_MiddleImage", "Warpage_MiddleImage_Count");

            // Gap
            var leftGap = ReadROIsFromIni("Gap_LeftImage", "Gap_LeftImage_Count");
            var rightGap = ReadROIsFromIni("Gap_RightImage", "Gap_RightImage_Count");
            var middleGap = ReadROIsFromIni("Gap_MiddleImage", "Gap_MiddleImage_Count");

            // --- 第二步：根據模式進行「語意對應」 ---
            if (CameraNumber == 3)
            {
                // CountSlice
                if (leftCountSlice.Count > 0)
                {
                    rOIList.ThreeCamera.CountSlice.LeftImage_Panel = ConvertToROI(leftCountSlice[0]);
                }

                if (middleCountSlice.Count > 0)
                {
                    rOIList.ThreeCamera.CountSlice.MiddleImage_LeftPanel = ConvertToROI(middleCountSlice[0]);
                    rOIList.ThreeCamera.CountSlice.MiddleImage_RightPanel = ConvertToROI(middleCountSlice[1]);
                }

                if (rightCountSlice.Count > 0)
                {
                    rOIList.ThreeCamera.CountSlice.RightImage_Panel = ConvertToROI(rightCountSlice[0]);
                }

                // Stack
                if (leftStack.Count > 0)
                {
                    rOIList.ThreeCamera.Stack.LeftImage_Panel = ConvertToROI(leftStack[0]);
                }

                if (middleStack.Count > 0)
                {
                    rOIList.ThreeCamera.Stack.MiddleImage_LeftPanel = ConvertToROI(middleStack[0]);
                    rOIList.ThreeCamera.Stack.MiddleImage_RightPanel = ConvertToROI(middleStack[1]);
                }

                if (rightStack.Count > 0)
                {
                    rOIList.ThreeCamera.Stack.RightImage_Panel = ConvertToROI(rightStack[0]);
                }

                // Thickness (List 型態) ---
                rOIList.ThreeCamera.Thickness["Left"] = ReadROIsFromIni("Thickness_LeftImage", "Thickness_LeftImage_Count").Select(r => ConvertToROI(r)).ToList();
                rOIList.ThreeCamera.Thickness["Middle"] = ReadROIsFromIni("Thickness_MiddleImage", "Thickness_MiddleImage_Count").Select(r => ConvertToROI(r)).ToList();
                rOIList.ThreeCamera.Thickness["Right"] = ReadROIsFromIni("Thickness_RightImage", "Thickness_RightImage_Count").Select(r => ConvertToROI(r)).ToList();

                // Warpage
                if (leftWarpage.Count > 0)
                {
                    rOIList.ThreeCamera.Warpage.LeftImage_Panel = ConvertToROI(leftWarpage[0]);
                }

                if (middleWarpage.Count > 0)
                {
                    rOIList.ThreeCamera.Warpage.MiddleImage_LeftPanel = ConvertToROI(middleWarpage[0]);
                    rOIList.ThreeCamera.Warpage.MiddleImage_RightPanel = ConvertToROI(middleWarpage[1]);
                }

                if (rightWarpage.Count > 0)
                {
                    rOIList.ThreeCamera.Warpage.RightImage_Panel = ConvertToROI(rightWarpage[0]);
                }

                // Gap
                if (leftGap.Count > 0)
                {
                    rOIList.ThreeCamera.Gap.LeftImage_Panel = ConvertToROI(leftWarpage[0]);
                }

                if (middleGap.Count > 0)
                {
                    rOIList.ThreeCamera.Gap.MiddleImage_LeftPanel = ConvertToROI(middleWarpage[0]);
                    rOIList.ThreeCamera.Gap.MiddleImage_RightPanel = ConvertToROI(middleWarpage[1]);
                }

                if (rightGap.Count > 0)
                {
                    rOIList.ThreeCamera.Gap.RightImage_Panel = ConvertToROI(rightWarpage[0]);
                }
            }
            else if (CameraNumber == 2)
            {
                // CountSlice
                if (leftCountSlice.Count > 0)
                {
                    rOIList.TwoCamera.CountSlice.LeftImage_EdgePanel = ConvertToROI(leftCountSlice[0]);
                    //rOIList.TwoCamera.CountSlice.LeftImage_Panel = ConvertToROI(leftCountSlice[1]);
                }

                if (rightCountSlice.Count > 0)
                {
                    rOIList.TwoCamera.CountSlice.RightImage_Panel = ConvertToROI(rightCountSlice[0]);
                    //rOIList.TwoCamera.CountSlice.RightImage_EdgePanel = ConvertToROI(rightCountSlice[1]);
                }

                // Stack
                if (leftStack.Count > 0)
                {
                    rOIList.TwoCamera.Stack.LeftImage_Panel = ConvertToROI(leftStack[0]);
                }

                if (rightStack.Count > 0)
                {
                    rOIList.TwoCamera.Stack.RightImage_Panel = ConvertToROI(rightStack[0]);
                }

                // Thickness (List 型態) ---
                rOIList.TwoCamera.Thickness["Left"] = ReadROIsFromIni("Thickness_LeftImage", "Thickness_LeftImage_Count").Select(r => ConvertToROI(r)).ToList();
                rOIList.TwoCamera.Thickness["Right"] = ReadROIsFromIni("Thickness_RightImage", "Thickness_RightImage_Count").Select(r => ConvertToROI(r)).ToList();

                // Warpage
                if (leftWarpage.Count > 0)
                {
                    rOIList.ThreeCamera.Warpage.LeftImage_EdgePanel = ConvertToROI(leftWarpage[0]);
                    rOIList.ThreeCamera.Warpage.LeftImage_Panel = ConvertToROI(leftWarpage[1]);
                }

                if (rightWarpage.Count > 0)
                {
                    rOIList.ThreeCamera.Warpage.RightImage_Panel = ConvertToROI(rightWarpage[0]);
                    rOIList.ThreeCamera.Warpage.RightImage_EdgePanel = ConvertToROI(rightWarpage[1]);
                }
            }



            return rOIList;
        }

        /// <summary>
        /// 封裝：讀取指定前綴的所有 ROI 並依照 X 排序
        /// </summary>
        private List<Rectangle> ReadROIsFromIni(string sectionPrefix, string countKey)
        {
            List<Rectangle> rois = new List<Rectangle>();

            // 讀取數量，失敗預設為 0
            string countStr = iniManager.ReadIniFile("ROIConfig", countKey, "0");
            if (!int.TryParse(countStr, out int count)) return rois;

            for (int i = 1; i <= count; i++)
            {
                string section = $"{sectionPrefix}{i:D2}"; // 例如 CountSlice_LeftImage01

                int x = GetIniInt(section, "ROI_X");
                int y = GetIniInt(section, "ROI_Y");
                int w = GetIniInt(section, "ROI_W");
                int h = GetIniInt(section, "ROI_H");

                rois.Add(new Rectangle(x, y, w, h));
            }

            // 統一排序
            rois.Sort((a, b) => a.X.CompareTo(b.X));
            return rois;
        }

        /// <summary>
        /// 輔助方法：讀取整數並處理異常
        /// </summary>
        private int GetIniInt(string section, string key, int defaultValue = 0)
        {
            string val = iniManager.ReadIniFile(section, key, defaultValue.ToString());
            return int.TryParse(val, out int result) ? result : defaultValue;
        }

        // 假設你自定義的 ROI 類別需要轉換
        private ROI ConvertToROI(Rectangle rect)
        {
            return new ROI { C1 = rect.X, R1 = rect.Y, C2 = rect.Width + rect.X, R2 = rect.Height + rect.Y };
        }

        public void WriteRecipeINI(string INIPath, SettingInfo settingInfo)
        {
            iniManager = new IniManager(INIPath);

            //iniManager.WriteIniFile("BaseConfig", "ImagePath", settingInfo.ImageParh);
        }
    }
}
