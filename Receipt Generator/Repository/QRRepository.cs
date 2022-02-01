using MessagingToolkit.QRCode.Codec;
using Receipt_Generator.Data;
using System.Drawing;
using System.Text;

namespace Receipt_Generator.Repository
{
    public class QRRepository
    {
        private Receipt _receipt;
        public QRRepository(Receipt receipt)
        {
            _receipt = receipt;
        }

        public Image GetQRCode()
        {
            string QRText = GetnerateQRText();
            QRCodeEncoder encoder = new QRCodeEncoder();
            return encoder.Encode(QRText);
        }

        private string GetnerateQRText()
        {
            StringBuilder QRText = new StringBuilder("ST00012");
            QRText.Append("|Name=");
            QRText.Append(_receipt.OrganizationName);
            QRText.Append("|PersonalAcc=");
            QRText.Append(_receipt.PaymentAccount);
            QRText.Append("|BankName=");
            QRText.Append(_receipt.BankName);
            QRText.Append("|BIC=");
            QRText.Append(_receipt.BIC);
            QRText.Append("|CorrespAcc=");
            QRText.Append(_receipt.CorrespondentAccount);
            QRText.Append("|KPP=");
            QRText.Append(_receipt.KPP);
            QRText.Append("|PayeeINN=");
            QRText.Append(_receipt.INN);
            QRText.Append("|Purpose=");
            QRText.Append(_receipt.Purpose);
            QRText.Append("|CBC=");
            QRText.Append(_receipt.KBK);
            QRText.Append("|OKTMO=");
            QRText.Append(_receipt.OKTMO);
            QRText.Append("|Sum=");
            QRText.Append(((_receipt.AmountRub * 100) + _receipt.AmountKop).ToString());
            QRText.Append("|UIN=");
            QRText.Append(_receipt.UIN);
            return QRText.ToString();
        }
    }
}
