using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RemindMeTelegramBotv2.DAL;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RemindMeTelegramBotv2.Models
{
    public class RemindMeCommand : Command
    {
        public override string Name => "/addremind";

        public override async Task ExecuteAsync(TelegramBotClient botClient, Message message, IDbRepository<RemindEntity> remindRepository)
        {
            RemindEntity.State state = RemindEntity.State.Start;
            var remindEntity = remindRepository.Get(r => r.TelegramUsernameId == message.From.Id.ToString());
            if(remindEntity != null)
                state = remindEntity.GetState();
            switch (state)
            {
                case RemindEntity.State.Start:

                    break;
                case RemindEntity.State.EnterText:
                    break;
                case RemindEntity.State.EnterDate:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
