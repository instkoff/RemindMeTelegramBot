namespace RemindMeTelegramBotv2.DAL
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string RemindsCollectionName { get; set; }
    }
}