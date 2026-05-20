using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProcess
{
    internal class ReadAlgorithmParameterINI
    {
        private IniManager iniManager;

        // =============================== 事件與委託 ===============================

        public delegate void CallBackReturnLog(string type, string msg);
        public event CallBackReturnLog CallBackLog;

        // =============================== 事件與委託 ===============================

        public MappingParameter ReadRecipeINI(string INIPath, int CameraNumber)
        {
            iniManager = new IniManager(INIPath);

            MappingParameter mappingParameter = new MappingParameter();

            if (CameraNumber == 3)
            {
                // 💡 傳統寫法需要幾百行，現在每個測項只要一行「反射自動填入」！
                LoadParametersFromIni(mappingParameter.ThreeCamera.CountSlice, "CountSlice");
                LoadParametersFromIni(mappingParameter.ThreeCamera.Stack, "Stack");
                LoadParametersFromIni(mappingParameter.ThreeCamera.Thickness, "Thickness");
                LoadParametersFromIni(mappingParameter.ThreeCamera.Warpage, "Warpage");
                LoadParametersFromIni(mappingParameter.ThreeCamera.Gap, "Gap");
                LoadParametersFromIni(mappingParameter.ThreeCamera.Stitch, "Stitch");
                LoadParametersFromIni(mappingParameter.ThreeCamera.SaveSetting, "SaveSetting");
                LoadParametersFromIni(mappingParameter.ThreeCamera.AlgorithmByPass, "AlgorithmByPass");
            }
            else if (CameraNumber == 2)
            {
                LoadParametersFromIni(mappingParameter.TwoCamera.CountSlice, "CountSlice");
                LoadParametersFromIni(mappingParameter.TwoCamera.Stack, "Stack");
                LoadParametersFromIni(mappingParameter.TwoCamera.Thickness, "Thickness");
                LoadParametersFromIni(mappingParameter.TwoCamera.Warpage, "Warpage");
                LoadParametersFromIni(mappingParameter.TwoCamera.Stitch, "Stitch");
                LoadParametersFromIni(mappingParameter.TwoCamera.SaveSetting, "SaveSetting");
                LoadParametersFromIni(mappingParameter.TwoCamera.AlgorithmByPass, "AlgorithmByPass");
            }

            return mappingParameter;
        }

        /// <summary>
        /// 反射自動遍歷物件的所有屬性，並從 INI 讀取對應資料
        /// </summary>
        private void LoadParametersFromIni<T>(T parameterObj, string sectionName) where T : class
        {
            if (parameterObj == null) return;

            // 1. 取得該類別所有的公開屬性 (Properties)
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // 2. 用迴圈一次掃完所有屬性
            foreach (PropertyInfo prop in properties)
            {
                string keyName = prop.Name;

                // 💡 關鍵改動：給予一個特殊的「找不到標記」來偵測 INI 是否漏參數
                string notFoundSign = "__NOT_FOUND__";
                string iniValueStr = iniManager.ReadIniFile(sectionName, keyName, notFoundSign);

                // 🚨 判斷：當 INI 檔案完全沒有這個 Key 時
                if (iniValueStr == notFoundSign)
                {
                    // 輸出到視窗
                    CallBackLog?.Invoke("WARNING", $"INI 檔案遺失參數: [{sectionName}] -> {keyName}");

                    // 💡 建議：現場出機通常會同步彈窗、寫 Log 檔或丟給狀態列 (StatusStrip) 讓工程師知道

                    continue; // 跳過此參數的型態轉換，直接保留 C# 類別原本就有的 initial 預設值
                }

                try
                {
                    // 3. 根據屬性的型態，自動進行安全轉換與動態填入
                    if (prop.PropertyType == typeof(double))
                    {
                        if (double.TryParse(iniValueStr, out double dVal))
                            prop.SetValue(parameterObj, dVal);
                        else
                            CallBackLog?.Invoke("WARNING", $"參數型態轉換失敗(double): [{sectionName}] -> {keyName} = '{iniValueStr}'");
                    }
                    else if (prop.PropertyType == typeof(bool))
                    {
                        if (bool.TryParse(iniValueStr, out bool bVal))
                        {
                            prop.SetValue(parameterObj, bVal);
                        }
                        else if (iniValueStr == "1")
                        {
                            prop.SetValue(parameterObj, true);
                        }
                        else if (iniValueStr == "0") 
                        {
                            prop.SetValue(parameterObj, false);
                        } 
                        else
                        {
                            CallBackLog?.Invoke("WARNING", $"參數型態轉換失敗(bool): [{sectionName}] -> {keyName} = '{iniValueStr}'");
                        }
                            
                    }
                    else if (prop.PropertyType == typeof(int))
                    {
                        if (int.TryParse(iniValueStr, out int iVal))
                        {
                            prop.SetValue(parameterObj, iVal);
                        }
                        else
                        {
                            CallBackLog?.Invoke("WARNING", $"參數型態轉換失敗(int): [{sectionName}] -> {keyName} = '{iniValueStr}'");
                        }                            
                    }
                    else if (prop.PropertyType == typeof(string))
                    {
                        prop.SetValue(parameterObj, iniValueStr);
                    }
                }
                catch (Exception ex)
                {
                    CallBackLog?.Invoke("WARNING", $"參數處理異常: [{sectionName}] -> {keyName} = '{iniValueStr}', Exception: {ex.Message}");
                }
            }
        }
    }
}
