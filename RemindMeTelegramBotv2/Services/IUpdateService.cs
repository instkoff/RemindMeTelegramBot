﻿using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace RemindMeTelegramBotv2.Services
{
    public interface IUpdateService
    {
        Task AnswerOnMessageAsync(Message update);
        Task AnswerOnCallbackQueryAsync(CallbackQuery update);
    }
}
