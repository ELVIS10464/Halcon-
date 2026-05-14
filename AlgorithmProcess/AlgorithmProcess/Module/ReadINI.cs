using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProcess
{
    public class ReadINI
    {
        private IniManager iniManager;

        public SettingInfo ReadRecipeINI(string INIPath)
        {
            iniManager = new IniManager(INIPath);

            SettingInfo settingInfo = new SettingInfo();

            settingInfo.ImageParh = iniManager.ReadIniFile("BaseConfig", "ImagePath", "default");

            return settingInfo;
        }

        public void WriteRecipeINI(string INIPath, SettingInfo settingInfo)
        {
            iniManager = new IniManager(INIPath);

            iniManager.WriteIniFile("BaseConfig", "ImagePath", settingInfo.ImageParh);
        }
    }
}
