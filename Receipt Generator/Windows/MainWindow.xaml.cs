using Receipt_Generator.Data;
using Receipt_Generator.MVP.Presentors;
using Receipt_Generator.MVP.Views;
using System.Windows;
using System.Windows.Media;

namespace Receipt_Generator.Windows
{
    public partial class MainWindow : Window, MainView
    {
        private MainPresenter _presenter;
        public MainWindow()
        {
            InitializeComponent();
            _presenter = new MainPresenter(this);
        }
        private void UpdateDate_Click(object sender, RoutedEventArgs e)
        {
            _presenter.UpdateCurrentDate();
        }
        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            _presenter.StartGenerate(InputDate.Text, InputUIN.Text, InputAmountRub.Text, InputAmountKop.Text, InputPurpose.Text);
        }
        public void SetDate(string date)
        {
            InputDate.Text = date;
        }
        public void UpdateStatus(Message message, int step)
        {
            UpdateStatus(message);
            UpdateStatus(step);
        }
        public void UpdateStatus(Message message)
        {
            switch (message.Type)
            {
                case MessageType.DEFAULT:
                    {
                        Status.Foreground = Brushes.Black;
                        break;
                    }
                case MessageType.SUCCESS:
                    {
                        Status.Foreground = Brushes.Green;
                        break;
                    }
                case MessageType.ERROR:
                    {
                        Status.Foreground = Brushes.Red;
                        break;
                    }
            }
            Status.Text = message.Text;
        }
        public void UpdateStatus(int step)
        {
            Progress.Value = step;
        }
        public void SetEnableButton(bool isEnable)
        {
            Generate.IsEnabled = isEnable;
        }
    }
}
