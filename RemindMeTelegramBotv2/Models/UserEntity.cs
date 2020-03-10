using System.Collections.Generic;

namespace RemindMeTelegramBotv2.Models
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public int Stage { get; set; } = 1;
        public string Tlg_Id { get; set; }

        public UserEntity(string name, int stage, string tlg_Id)
        {
            Name = name;
            Stage = stage;
            Tlg_Id = tlg_Id;
        }
    }
}
