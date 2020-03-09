using System.Collections.Generic;

namespace RemindMeTelegramBotv2.Models
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public States State { get; private set; }
        public List<RemindEntity> RemindsList { get; set; }

        public UserEntity(string name, States state, List<RemindEntity> remindsList)
        {
            Name = name;
            State = state;
            RemindsList = remindsList;
        }
    }
    public enum States
    {
        S_START = 0,
        S_ENTER_REMIND_TEXT = 1,
        S_ENTER_ALARM_TIME = 2
    }
}
