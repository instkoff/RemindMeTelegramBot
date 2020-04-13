namespace RemindMeTelegramBotv2.DAL
{
    /// <summary>
    /// Класс настройки БД
    /// </summary>
    public class DatabaseSettings : IDatabaseSettings
    {
        public string RemindsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
