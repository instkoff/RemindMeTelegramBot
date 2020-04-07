using System.Collections.Generic;
using RemindMeTelegramBotv2.Models;

namespace RemindMeTelegramBotv2.Services
{
    public interface IRemindService
    {
        List<RemindEntity> CurrentReminds { get; }
        void TryAddToRemindsSequence(RemindEntity newRemindEntity);
    }
}