namespace RemindMeTelegramBotv2.Models
{
    public interface IBaseEntity
    {
        string Id { get; set; }
        string TlgId { get; set; }
    }
}