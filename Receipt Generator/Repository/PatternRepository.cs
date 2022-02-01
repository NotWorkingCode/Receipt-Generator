using iTextSharp.text;
using iTextSharp.text.pdf;
using Receipt_Generator.Data;
using System;
using System.IO;

namespace Receipt_Generator.Repository
{
    public class PatternRepository
    {

        private string _path;

        public PatternRepository(string path = "pattern.pdf")
        {
            _path = path;
        }

        public bool ExistsPattern()
        {
            return File.Exists(_path);
        }

        public bool ValidatePattern()
        {
            using (PdfReader reader = new PdfReader(_path))
            {
                int count = reader.AcroFields.Fields.Count;
                LogCore.Debug("Найдено " + count + " полей в шаблоне квитанции.");

                if (!reader.AcroFields.Fields.ContainsKey("OrganizationName")) return false;
                if (!reader.AcroFields.Fields.ContainsKey("INN")) return false;
                if (!reader.AcroFields.Fields.ContainsKey("KPP")) return false;
                if (!reader.AcroFields.Fields.ContainsKey("PaymentAccount")) return false;
                if (!reader.AcroFields.Fields.ContainsKey("BankName")) return false;
                if (!reader.AcroFields.Fields.ContainsKey("BIC")) return false;
                if (!reader.AcroFields.Fields.ContainsKey("Purpose")) return false;
                if (!reader.AcroFields.Fields.ContainsKey("Date")) return false;
                if (!reader.AcroFields.Fields.ContainsKey("OKTMO")) return false;
                if (!reader.AcroFields.Fields.ContainsKey("CorrespondentAccount")) return false;
                if (!reader.AcroFields.Fields.ContainsKey("Amount")) return false;
                if (!reader.AcroFields.Fields.ContainsKey("QR")) return false;
                if (!reader.AcroFields.Fields.ContainsKey("KBK")) return false;
                return true;
            }
        }

        public bool CreateReceipt(Receipt receipt)
        {
            string filename = "Receipts\\Квитанция от " + DateTime.Now.ToString("d") + " - " + receipt.UIN + ".pdf";
            using (FileStream outFile = new FileStream(filename, FileMode.OpenOrCreate))
            {
                BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                PdfReader pdfReader = new PdfReader("pattern.pdf");
                PdfStamper pdfStamper = new PdfStamper(pdfReader, outFile);

                AcroFields fields = pdfStamper.AcroFields;

                fields.SetFieldProperty("OrganizationName", "textfont", baseFont, null);
                fields.SetFieldProperty("INN", "textfont", baseFont, null);
                fields.SetFieldProperty("KPP", "textfont", baseFont, null);
                fields.SetFieldProperty("PaymentAccount", "textfont", baseFont, null);
                fields.SetFieldProperty("BankName", "textfont", baseFont, null);
                fields.SetFieldProperty("BIC", "textfont", baseFont, null);
                fields.SetFieldProperty("Purpose", "textfont", baseFont, null);
                fields.SetFieldProperty("Date", "textfont", baseFont, null);
                fields.SetFieldProperty("OKTMO", "textfont", baseFont, null);
                fields.SetFieldProperty("CorrespondentAccount", "textfont", baseFont, null);
                fields.SetFieldProperty("Amount", "textfont", baseFont, null);
                fields.SetFieldProperty("KBK", "textfont", baseFont, null);

                fields.SetField("OrganizationName", receipt.OrganizationName);
                fields.SetField("INN", receipt.INN);
                fields.SetField("KPP", receipt.KPP);
                fields.SetField("PaymentAccount", receipt.PaymentAccount);
                fields.SetField("BankName", receipt.BankName);
                fields.SetField("BIC", receipt.BIC);
                fields.SetField("Purpose", receipt.Purpose);
                fields.SetField("Date", receipt.Date);
                fields.SetField("OKTMO", receipt.OKTMO);
                fields.SetField("CorrespondentAccount", receipt.CorrespondentAccount);
                fields.SetField("Amount", receipt.AmountRub.ToString() + " руб. " + receipt.AmountKop.ToString() + " коп.");
                fields.SetField("KBK", receipt.KBK);

                PushbuttonField ad = fields.GetNewPushbuttonFromField("QR");

                ad.Layout = PushbuttonField.LAYOUT_ICON_ONLY;
                ad.ProportionalIcon = true;
                ad.Image = Image.GetInstance(receipt.QR, BaseColor.WHITE);

                fields.ReplacePushbuttonField("QR", ad.Field);

                pdfStamper.Close();
                pdfReader.Close();
            }
            return true;
        }
    }
}
