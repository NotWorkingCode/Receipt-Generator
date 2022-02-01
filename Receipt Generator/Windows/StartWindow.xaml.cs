using Receipt_Generator.Data;
using Receipt_Generator.MVP.Presentors;
using Receipt_Generator.MVP.Views;
using System.Windows;
using System.Windows.Media;

namespace Receipt_Generator.Windows
{
    public partial class StartWindow : Window, StartView
    {

        private StartPresenter _presenter;

        public StartWindow()
        {
            InitializeComponent();
            _presenter = new StartPresenter(this);
        }

        public void EnableNextStep()
        {
            NextStep.IsEnabled = true;
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

        private void NextStep_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
