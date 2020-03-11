using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Models;
using Telegram.Bot.Types;

namespace RemindMeTelegramBotv2.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotClient _botClient;
        private readonly IDbRepository<RemindEntity> _remindRepository;
        private readonly IDbRepository<UserEntity> _userRepository;

        private static Command fixedCommand;

        public UpdateService(IBotClient botClient, IDbRepository<RemindEntity> remindRepository, IDbRepository<UserEntity> userRepository)
        {
            _remindRepository = remindRepository;
            _userRepository = userRepository;
            _botClient = botClient;
        }
        public async Task AnswerAsync(Update update)
        {
            var message = update.Message;
            if (fixedCommand != null && fixedCommand.isComplete == false)
            {
                await fixedCommand.ExecuteAsync(_botClient.Client, message, _remindRepository);
            }
            else
            {
                fixedCommand = null;
                return;
            }

            if(fixedCommand == null)
            {
                foreach (var command in _botClient.Commands)
                {
                    if (command.Contains(message.Text))
                    {
                        fixedCommand = command;
                        await command.ExecuteAsync(_botClient.Client, message, _remindRepository);
                    }
                }
            }

        }
    }
}
