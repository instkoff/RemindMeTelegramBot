namespace RemindMeTelegramBotv2.Models
{
    public class MessageInfo
    {
        public int FromId { get; }
        public long ChatId { get; }
        public string Username { get; }
        public string MessageText { get; }

        public MessageInfo(int fromId, long chatId, string username, string messageText)
        {
            FromId = fromId;
            ChatId = chatId;
            Username = username;
            MessageText = messageText;
        }
    }
}
