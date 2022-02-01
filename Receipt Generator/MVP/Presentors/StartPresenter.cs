using Receipt_Generator.Data;
using Receipt_Generator.MVP.Views;
using Receipt_Generator.Repository;
using System.IO;
using System.Threading.Tasks;

namespace Receipt_Generator.MVP.Presentors
{
    public class StartPresenter
    {
        private StartView _view;
        public StartPresenter(StartView view)
        {
            _view = view;
            CheckSystem();
        }

        public async void CheckSystem()
        {
            var configRepository = new ConfigRepository("config.ini");

            LogCore.Info("Начинаю валидировать конфиг...");
            _view.UpdateStatus(new Message("Начинаю валидировать конфиг...", MessageType.DEFAULT), 1);

            bool existsConfig = await Task.Run(() => configRepository.ExistsConfig());

            if (!existsConfig)
            {
                LogCore.Error("Не удалось найти файл конфигурации!");
                _view.UpdateStatus(new Message("[ERROR] Не удалось найти файл конфигурации!", MessageType.ERROR), 1);
                return;
            }

            LogCore.Info("Файл конфигурации найден!");
            _view.UpdateStatus(2);

            bool validConfig = await Task.Run(() => configRepository.ValidateConfig());

            if (!validConfig)
            {
                LogCore.Error("Файл конфигурации поврежден!");
                _view.UpdateStatus(new Message("[ERROR] Файл конфигурации поврежден!", MessageType.ERROR), 2);
                return;
            }

            LogCore.Info("Файл конфигурации валиден!");
            _view.UpdateStatus(3);

            var patternRepository = new PatternRepository();

            LogCore.Info("Начинаю валидировать шаблон квитанции...");
            _view.UpdateStatus(new Message("Начинаю валидировать шаблон квитанции...", MessageType.DEFAULT), 4);

            bool existsPattern = await Task.Run(() => patternRepository.ExistsPattern());

            if (!existsPattern)
            {
                LogCore.Error("Не удалось найти шаблон квитанции!");
                _view.UpdateStatus(new Message("[ERROR] Не удалось найти шаблон квитанции!", MessageType.ERROR), 4);
                return;
            }

            LogCore.Info("Шаблон квитанции конфигурации найден!");
            _view.UpdateStatus(5);

            bool validPattern = await Task.Run(() => patternRepository.ValidatePattern());

            if (!validPattern)
            {
                LogCore.Error("Шаблон квитанции поврежден!");
                _view.UpdateStatus(new Message("[ERROR] Шаблон квитанции поврежден!", MessageType.ERROR), 5);
                return;
            }

            LogCore.Info("Шаблон квитанции валиден!");
            _view.UpdateStatus(6);

            if (!Directory.Exists("Receipts"))
                Directory.CreateDirectory("Receipts");

            LogCore.Info("Система готова к работе!");
            _view.UpdateStatus(new Message("[SUCCESS] Система готова к работе!", MessageType.SUCCESS), 7);

            _view.EnableNextStep();
        }     
    }
}
