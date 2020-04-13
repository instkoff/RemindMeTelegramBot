namespace RemindMeTelegramBotv2.Models
{
    /// <summary>
    /// Детали пришедшего сообщения
    /// </summary>
    public class MessageDetails
    {
        public int FromId { get; }
        public long ChatId { get; }
        public string Username { get; }
        public string MessageText { get; }

        public MessageDetails(int fromId, long chatId, string username, string messageText)
        {
            FromId = fromId;
            ChatId = chatId;
            Username = username;
            MessageText = messageText;
        }
    }
}
