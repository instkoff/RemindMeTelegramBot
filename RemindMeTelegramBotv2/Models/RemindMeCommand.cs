using RemindMeTelegramBotv2.DAL;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RemindMeTelegramBotv2.Models
{
    public class RemindMeCommand : Command
    {
        private readonly IDbRepository<RemindEntity> _dbRepository;
        public RemindMeCommand(IDbRepository<RemindEntity> dbRepository)
        {
            _dbRepository = dbRepository;
        }
        public override string Name { get; } = "Напомнить";

        public override async Task ExecuteAsync(TelegramBotClient botClient, Message message)
        {
            var remind = new RemindEntity(DateTime.Now, DateTime.Today, "Тест");
            _dbRepository.Create(remind);
            await botClient.SendTextMessageAsync(message.Chat.Id, "О чём вам напомнить?");

        }
    }
}
