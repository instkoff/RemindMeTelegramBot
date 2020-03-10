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
                    var remindCommand = new RemindMeCommand();
                    if (update.Message.Text.Contains("Напомнить"))
                    {
                        
                        var message = update.Message;
                        var currentUser = _userRepository.Get(user => user.Name == update.Message.From.Username);
                        if (currentUser == null)
                        {
                            var newUser = new UserEntity(message.From.Username.ToString(), 1, message.From.Id.ToString());
                            await remindCommand.ExecuteAsync(_botClient.Client, update.Message, newUser);
                            newUser.Stage++;
                            _userRepository.Create(newUser);
                        }
                        else if(currentUser.Stage == 1)
                        {
                            await remindCommand.ExecuteAsync(_botClient.Client, update.Message, currentUser);
                            currentUser.Stage++;
                            _userRepository.Update(currentUser.Id,currentUser);
                        }
                    }
                    else
                    {
                        var message = update.Message;
                        var currentUser = _userRepository.Get(user=>user.Name==message.From.Username);
                        if (currentUser == null) break;
                        if (currentUser.Stage > 1)
                        {
                            await remindCommand.ExecuteAsync(_botClient.Client, update.Message, currentUser);
                            currentUser.Stage = currentUser.Stage < 4 ? currentUser.Stage++ : currentUser.Stage = 1;
                            _userRepository.Update(currentUser.Id, currentUser);
                        }
                        else
                        {
                            await _botClient.Client.SendTextMessageAsync(message.Chat.Id, "Что то пошло не так");
                        }

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
