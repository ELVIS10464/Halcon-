using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProcess
{
    public class ReadDefaultINI
    {
        private IniManager iniManager;

        public SettingInfo ReadINI(string INIPath)
        {
            iniManager = new IniManager(INIPath);

            SettingInfo settingInfo = new SettingInfo();

            settingInfo.LoadImagePath = iniManager.ReadIniFile("BaseConfig", "LoadImagePath", "default");

            settingInfo.RecipePath = iniManager.ReadIniFile("BaseConfig", "RecipePath", "default");

            settingInfo.SaveResultPath = iniManager.ReadIniFile("BaseConfig", "SaveResultPath", "default");

            settingInfo.SlotNumber = int.Parse(iniManager.ReadIniFile("BaseConfig", "SlotNumber", "0"));

            return settingInfo;
        }

        public void WriteINI(string INIPath, SettingInfo settingInfo)
        {
            iniManager = new IniManager(INIPath);

            iniManager.WriteIniFile("BaseConfig", "LoadImagePath", settingInfo.LoadImagePath);
            iniManager.WriteIniFile("BaseConfig", "RecipePath", settingInfo.RecipePath);
            iniManager.WriteIniFile("BaseConfig", "SaveResultPath", settingInfo.SaveResultPath);
            iniManager.WriteIniFile("BaseConfig", "SlotNumber", settingInfo.SlotNumber.ToString());
        }
    }
}
