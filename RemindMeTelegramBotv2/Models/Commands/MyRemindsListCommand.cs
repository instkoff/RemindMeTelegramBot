using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemindMeTelegramBotv2.DAL;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Models.Commands
{
    /// <summary>
    /// Команда получения списка напоминаний пользователя
    /// </summary>
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
            IsComplete = false;
            var remindsList = await _dbRepository.GetListAsync(r => r.TelegramUsernameId == message.FromId && r.State != RemindState.Completed);
            if (remindsList.Any())
            {
                int i = 1;
                StringBuilder remindsListString = new StringBuilder();
                remindsListString.Append($"Ваши напоминания {message.Username}: \n");
                foreach (var r in remindsList)
                {
                    var timeZone = TimeZoneInfo.FindSystemTimeZoneById(r.TimeZoneId);
                    var convertedEndTime = TimeZoneInfo.ConvertTimeFromUtc(r.EndTime, timeZone);
                    var utcOffsetDisplayName = timeZone.DisplayName.Remove(11);
                    remindsListString.Append($"{i++}) {convertedEndTime} {utcOffsetDisplayName} напомнить о: {r.RemindText} \n");
                }

                await _botClient.SendTextMessageAsync(message.ChatId, remindsListString.ToString());
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

