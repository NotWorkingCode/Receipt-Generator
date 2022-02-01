using Receipt_Generator.Data;
using Receipt_Generator.MVP.Views;
using Receipt_Generator.Repository;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Receipt_Generator.MVP.Presentors
{
    public class MainPresenter
    {
        private MainView _view;
        public MainPresenter(MainView view)
        {
            _view = view;
        }
        public void UpdateCurrentDate()
        {
            var date = DateTime.Now;
            _view.SetDate(date.ToString("d") + " " + date.ToString("t"));
        }
        public async void StartGenerate(string date, string uin, string amountRub, string amountKop, string purpose)
        {
            if
                (
                    date.Length == 0 ||
                    uin.Length == 0 ||
                    amountRub.Length == 0 ||
                    amountKop.Length == 0 ||
                    purpose.Length == 0
                )
            {
                _view.UpdateStatus(new Message("[ERROR] Заполните все поля!", MessageType.ERROR), 0);
                return;
            }

            int rub = 0;
            int kop = 0;

            try
            {
                rub = int.Parse(amountRub);
                kop = int.Parse(amountKop);
            } catch
            {
                _view.UpdateStatus(new Message("[ERROR] Введите коррекную сумму!", MessageType.ERROR), 0);
                return;
            }

            _view.SetEnableButton(false);

            LogCore.Info("Получаю информацию из файла конфигурации...");
            _view.UpdateStatus(new Message("Получаю информацию из файла конфигурации...", MessageType.DEFAULT), 1);

            var configRepository = new ConfigRepository("config.ini");

            Receipt receipt = await Task.Run(() => configRepository.LoadConfig());

            receipt.Date = date;
            receipt.UIN = uin;
            receipt.Purpose = purpose;
            receipt.AmountRub = rub;
            receipt.AmountKop = kop;

            LogCore.Info("Генерирую QR код...");
            _view.UpdateStatus(new Message("Генерирую QR код...", MessageType.DEFAULT), 2);

            var QRRepository = new QRRepository(receipt);

            receipt.QR = await Task.Run(() => QRRepository.GetQRCode());

            LogCore.Info("Создаю квитанцию...");
            _view.UpdateStatus(new Message("Создаю квитанцию...", MessageType.DEFAULT), 3);

            var patternRepository = new PatternRepository();
            patternRepository.CreateReceipt(receipt);

            LogCore.Info("Квитанция успешно создана!");
            _view.UpdateStatus(new Message("[SUCCESS] Квитанция успешно создана!", MessageType.SUCCESS), 3);
            _view.SetEnableButton(true);
        }
    }
}
