using Receipt_Generator.Data;

namespace Receipt_Generator.MVP.Views
{
    public interface StartView
    {
        void UpdateStatus(Message message, int step);
        void UpdateStatus(Message message);
        void UpdateStatus(int step);
        void EnableNextStep();
    }
}
