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
        static Dictionary<string, RemindEntity> remindsDictionary = new Dictionary<string, RemindEntity>();

        public override async Task ExecuteAsync(TelegramBotClient botClient, Message message, IDbRepository<RemindEntity> remindRepository)
        {
            if (message.Text == Name)
            {
                base.isComplete = false;
                var newRemind = new RemindEntity(message.From.Id.ToString(), message.From.Username);
                remindsDictionary.Add(message.From.Id.ToString(), newRemind);
                var stage1Text = newRemind.StageText(newRemind.TelegramUsernameId);
                await botClient.SendTextMessageAsync(message.Chat.Id, stage1Text.Item1);
                return;
            }
            else if (remindsDictionary.ContainsKey(message.From.Id.ToString()))
            {
                var remind = remindsDictionary[message.From.Id.ToString()];
                if (remind.SetParam(message.Text))
                {
                    var text = remind.StageText(remind.TelegramUsernameId);
                    await botClient.SendTextMessageAsync(message.Chat.Id, text.Item1);
                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Что то пошло не так, попробуем ещё раз?");
                    var text = remind.StageText(remind.TelegramUsernameId);
                    await botClient.SendTextMessageAsync(message.Chat.Id, text.Item1);
                }
                if (remind.stage == 3)
                {
                    remind.CurrentTime = DateTime.Now;
                    remindRepository.Create(remind);
                    remindsDictionary.Clear();
                    base.isComplete = true;
                }
            }
        }
    }
}
