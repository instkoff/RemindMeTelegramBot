using System.Collections.Generic;
using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Models;
using System.Threading.Tasks;
using RemindMeTelegramBotv2.Models.Commands;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RemindMeTelegramBotv2.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotClient _botClient;
        private readonly IDbRepository<RemindEntity> _remindRepository;
        private readonly IRemindService _remindService;

        private static Dictionary<int,Command> _fixedCommands = new Dictionary<int, Command>();


        public UpdateService(IBotClient botClient, IDbRepository<RemindEntity> remindRepository, IRemindService remindService)
        {
            _remindRepository = remindRepository;
            _botClient = botClient;
            _remindService = remindService;
        }
        public async Task AnswerOnMessageAsync(Message message)
        {
            if (message == null)
                return;
            var messageInfo = new MessageInfo(message.From.Id, message.Chat.Id, message.From.Username, message.Text);

            if (message.Text == "/start" && !(_fixedCommands.ContainsKey(message.From.Id)))
            {
                await new StartCommand().ExecuteAsync(_botClient.Client, messageInfo, _remindRepository);
                return;
            }

            if (_fixedCommands.ContainsKey(messageInfo.FromId))
            {
                if (_fixedCommands[messageInfo.FromId].IsComplete == false)
                {
                    await _fixedCommands[messageInfo.FromId].ExecuteAsync(_botClient.Client, messageInfo, _remindRepository);
                }
                if (_fixedCommands[messageInfo.FromId].IsComplete)
                    _fixedCommands.Remove(messageInfo.FromId);
            }

        }
        public async Task AnswerOnCallbackQueryAsync(CallbackQuery callback)
        {
            if (callback == null)
                return;
            var username = callback.From.Username ?? callback.From.FirstName;
            var messageInfo = new MessageInfo(callback.From.Id, callback.Message.Chat.Id, username, callback.Data);

            var command = FindCommand(messageInfo.MessageText);

            if (command != null)
            {
                if (_fixedCommands.ContainsKey(messageInfo.FromId))
                {
                    if (_fixedCommands[messageInfo.FromId].IsComplete == false)
                    {
                        await _botClient.Client.SendTextMessageAsync(messageInfo.ChatId,
                            "Вы повторно ввели команду, закончите или сбросьте текущую");
                        return;
                    }
                    if (_fixedCommands[messageInfo.FromId].IsComplete)
                        _fixedCommands.Remove(messageInfo.FromId);
                }


                _fixedCommands.Add(messageInfo.FromId, command);
                await command.ExecuteAsync(_botClient.Client, messageInfo, _remindRepository);

            }
        }

        private Command FindCommand(string commandText)
        {
            if (commandText == "/addremind")
            {
                return new RemindMeCommand(_remindService);
            }

            foreach (var command in _botClient.Commands)
            {
                if (command.Contains(commandText))
                {
                    return command;
                }
            }
            return null;
        }

    }
}
