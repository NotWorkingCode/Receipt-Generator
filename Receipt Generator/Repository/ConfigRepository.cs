using Receipt_Generator.Data;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Receipt_Generator.Repository
{
    public class ConfigRepository
    {
        private string _path;
        private string _EXE = Assembly.GetExecutingAssembly().GetName().Name;

        public string EXE { get; }

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public ConfigRepository(string IniPath = null)
        {
            _path = new FileInfo(IniPath ?? EXE + ".ini").FullName;
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, _path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, _path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }

        public bool ExistsConfig()
        {
            return File.Exists(_path);
        }

        public bool ValidateConfig()
        {
            if (!KeyExists("Name", "Organization"))         return false;
            if (!KeyExists("INN", "Organization"))          return false;
            if (!KeyExists("KPP", "Organization"))          return false;
            if (!KeyExists("OKTMO", "Organization"))        return false;
            if (!KeyExists("KBK", "Organization"))          return false;
            if (!KeyExists("Name", "Bank"))                 return false;
            if (!KeyExists("PaymentAccount", "Bank"))       return false;
            if (!KeyExists("CorrespondentAccount", "Bank")) return false;
            if (!KeyExists("BIC", "Bank"))                  return false;
            return true;
        }
        public Receipt LoadConfig()
        {
            var receipt = new Receipt();

            receipt.OrganizationName = Read("Name", "Organization");
            receipt.INN = Read("INN", "Organization");
            receipt.KPP = Read("KPP", "Organization");
            receipt.OKTMO = Read("OKTMO", "Organization");
            receipt.KBK = Read("KBK", "Organization");
            receipt.BankName = Read("Name", "Bank");
            receipt.PaymentAccount = Read("PaymentAccount", "Bank");
            receipt.CorrespondentAccount = Read("CorrespondentAccount", "Bank");
            receipt.BIC = Read("BIC", "Bank");
            
            return receipt;
        }
    }
}
