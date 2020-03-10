using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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

        public UpdateService(IBotClient botClient, IDbRepository<RemindEntity> remindRepository, IDbRepository<UserEntity> userRepository)
        {
            _remindRepository = remindRepository;
            _userRepository = userRepository;
            _botClient = botClient;
        }
        public async Task AnswerAsync(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    var messaage = update.Message;
                    var user = await _userRepository.GetByTlgId(update.Message.From.Username);
                    //v
                    if (update.Message.Text.Contains("Напомнить") &&  user.Stage  1)
                    {
                        var username = messaage.From.Username.ToString();
                        var tlgId = messaage.From.Id.ToString();
                        var newUser = new UserEntity(username, tlgId, 1);
                        var remindCommand = new RemindMeCommand();
                        await remindCommand.ExecuteAsync(_botClient.Client, update.Message, newUser.Stage);
                        _userRepository.Create(newUser);
                    }
                    else
                    {
                        var currentUser = _userRepository.GetByTlgId(messaage.From.Id.ToString());

                    }
                    break;
                case UpdateType.CallbackQuery:
                    foreach (var command in _botClient.Commands)
                    {
                        if (command.Contains(update.CallbackQuery.Data))
                            await command.ExecuteAsync(_botClient.Client, update.CallbackQuery.Message);
                    }
                    break;
                default:
                    await _botClient.Client.SendTextMessageAsync(update.Message.Chat.Id, "Команда не распознана", replyToMessageId: update.Message.MessageId);
                    break;
            }
        }
    }
}
