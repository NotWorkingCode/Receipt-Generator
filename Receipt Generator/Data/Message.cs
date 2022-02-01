namespace Receipt_Generator.Data
{
    public class Message
    {
        public string Text { get; private set; }
        public MessageType Type { get; private set; }
        public Message(string text, MessageType type)
        {
            Text = text;
            Type = type;
        }
    }
}
