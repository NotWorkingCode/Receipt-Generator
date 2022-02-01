using System.Drawing;

namespace Receipt_Generator.Data
{
    public class Receipt
    {
        public string OrganizationName { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string OKTMO { get; set; }
        public string KBK { get; set; }
        public string BankName { get; set; }
        public string PaymentAccount { get; set; }
        public string CorrespondentAccount { get; set; }
        public string BIC { get; set; }
        public string Date { get; set; }
        public int AmountRub { get; set; }
        public int AmountKop { get; set; }
        public string Purpose { get; set; }
        public string UIN { get; set; }
        public Image QR { get; set; }
    }
}
