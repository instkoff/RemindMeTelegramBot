using System.Text;
using System.Threading.Tasks;
using RemindMeTelegramBotv2.DAL;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Models.Commands
{
    /// <summary>
    /// Команда получения списка напоминаний пользователя
    /// </summary>
    //ToDo Добавить часовые пояса
    public class MyRemindsListCommand : Command
    {
        public override string Name { get; } = "/myremindslist";
        private readonly TelegramBotClient _botClient;
        private readonly IDbRepository<RemindEntity> _dbRepository;

        public MyRemindsListCommand(TelegramBotClient botClient, IDbRepository<RemindEntity> dbRepository)
        {
            _botClient = botClient;
            _dbRepository = dbRepository;
        }

        public override async Task ExecuteAsync(MessageDetails message)
        {
            var remindsList = await _dbRepository.GetListAsync(r => r.TelegramUsernameId == message.FromId && r.State != RemindState.Completed);
            if (remindsList.Count != 0)
            {
                int i=1;
                StringBuilder remindsBuilder = new StringBuilder();
                remindsBuilder.Append($"Ваши напоминания {message.Username}: \n");
                remindsList.ForEach(r => { remindsBuilder.Append($"{i++}) {r.EndTime} напомнить о: {r.RemindText} \n"); });
                await _botClient.SendTextMessageAsync(message.ChatId, remindsBuilder.ToString());
                IsComplete = true;
            }
            else
            {
                await _botClient.SendTextMessageAsync(message.ChatId, "Список пуст");
                IsComplete = true;
            }

        }
    }
}
