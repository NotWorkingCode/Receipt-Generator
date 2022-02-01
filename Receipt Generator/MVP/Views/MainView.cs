using Receipt_Generator.Data;

namespace Receipt_Generator.MVP.Views
{
    public interface MainView
    {
        void SetDate(string date);
        void UpdateStatus(Message message, int step);
        void UpdateStatus(Message message);
        void UpdateStatus(int step);
        void SetEnableButton(bool isEnable);
    }
}
