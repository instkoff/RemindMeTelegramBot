using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RemindMeTelegramBotv2.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotClient _botClient;
        private readonly IDbRepository<RemindEntity> _remindRepository;
        private readonly IDbRepository<UserEntity> _userRepository;
        static Dictionary<string, RemindEntity> remindsDictionary = new Dictionary<string, RemindEntity>();

        public UpdateService(IBotClient botClient, IDbRepository<RemindEntity> remindRepository, IDbRepository<UserEntity> userRepository)
        {
            _remindRepository = remindRepository;
            _userRepository = userRepository;
            _botClient = botClient;
        }
        public async Task AnswerAsync(Update update)
        {
            var message = update.Message;
            if (message.Text == "/addremind")
            {
                var remind = new RemindEntity(message.From.Id.ToString(),message.Chat.Id.ToString(),message.From.Username);
                remindsDictionary.Add(message.From.Id.ToString(), remind);
                var text = remind.StageText(remind.TelegramId);
                await _botClient.Client.SendTextMessageAsync(message.Chat.Id, text.Item1);
                return;
            }

            else if (remindsDictionary.ContainsKey(message.From.Id.ToString()))
            {
                var remind = remindsDictionary[message.From.Id.ToString()];
                remind.SetParam(message.Text);
                var text = remind.StageText(remind.TelegramId);
                await _botClient.Client.SendTextMessageAsync(message.Chat.Id, text.Item1);
            }


        }
    }
}
