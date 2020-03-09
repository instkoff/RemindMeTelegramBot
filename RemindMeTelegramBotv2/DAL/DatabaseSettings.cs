using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemindMeTelegramBotv2.DAL
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string RemindsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
