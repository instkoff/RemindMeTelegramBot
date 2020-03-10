using System.Collections.Generic;

namespace RemindMeTelegramBotv2.Models
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public int Stage { get; set; } = 1;

        public UserEntity(string name, string tlgId, int stage) : base(tlgId)
        {
            Name = name;
            Stage = stage;

        }
    }
}
