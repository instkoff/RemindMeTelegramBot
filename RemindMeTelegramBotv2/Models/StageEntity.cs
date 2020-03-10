namespace RemindMeTelegramBotv2.Models
{
    public class StageEntity : BaseEntity
    {
        public int stage_num { get; set; }
        public UserEntity UserName { get; set; }

        public StageEntity(string tlgId) : base(tlgId)
        {
        }
    }
}