using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProcess
{
    // INI 設定檔建構 Function
    public class IniManager
    {
        private string filePath;
        private StringBuilder lpReturnedString;
        private int bufferSize;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]

        private static extern uint GetPrivateProfileSection(string lpAppName, IntPtr lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string lpString, string lpFileName);

        [DllImport("kernel32")]
        private static extern uint GetPrivateProfileString(string section, string key, string lpDefault, [In, Out] char[] lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32")]
        private static extern uint GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, uint nSize, string lpFileName);

        public IniManager(string iniPath)
        {
            filePath = iniPath;
            bufferSize = 512;
            lpReturnedString = new StringBuilder(bufferSize);
        }

        // read ini date depend on section and key
        public string ReadIniFile(string section, string key, string defaultValue)
        {
            lpReturnedString.Clear();
            GetPrivateProfileString(section, key, defaultValue, lpReturnedString, bufferSize, filePath);
            return lpReturnedString.ToString();
        }

        // write ini data depend on section and key
        public void WriteIniFile(string section, string key, Object value)
        {
            WritePrivateProfileString(section, key, value.ToString(), filePath);
        }

        public string[] GetAllSectionNames(string path)
        {
            path = Path.GetFullPath(path);
            uint MAX_BUFFER = 32767;
            IntPtr pReturnedString = Marshal.AllocCoTaskMem((int)MAX_BUFFER);
            uint bytesReturned = GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, path);
            if (bytesReturned == 0)
                return null;
            string local = Marshal.PtrToStringAnsi(pReturnedString, (int)bytesReturned).ToString();
            Marshal.FreeCoTaskMem(pReturnedString);
            //use of Substring below removes terminating null for split
            return local.Substring(0, local.Length - 1).Split('\0');
        }

        public string[] GetINISectionAllItems(string path, string section)
        {
            uint MAX_BUFFER = 32767;

            string[] items = new string[0];

            IntPtr pReturnString = Marshal.AllocCoTaskMem((int)MAX_BUFFER * sizeof(char));

            uint byteReturned = GetPrivateProfileSection(section, pReturnString, MAX_BUFFER, path);

            if (!(byteReturned == MAX_BUFFER - 2) || byteReturned == 0)
            {
                string returnedString = Marshal.PtrToStringAnsi(pReturnString, (int)byteReturned);

                items = returnedString.Split(new char[] { '\0'}, StringSplitOptions.RemoveEmptyEntries);
            }

            Marshal.FreeCoTaskMem(pReturnString);

            return items;
        }

        public string[] GetINISectionAllKeys(string path, string section)
        {
            string[] value = new string[0];

            const int SIZE = 1024 * 10;

            char[] chars = new char[SIZE];

            uint byteReturned = GetPrivateProfileString(section, null, null, chars, SIZE, path);

            if (byteReturned != 0)
            {
                value = new string(chars).Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }

            chars = null;

            return value;
        }


        public void ClearSection(string path, string section)
        {
            path = Path.GetFullPath(path);
            WritePrivateProfileString(section, null, null, path);
        }
    }
}
