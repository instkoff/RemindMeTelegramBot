using System.Threading.Tasks;

namespace RemindMeTelegramBotv2.Models.Commands
{
    /// <summary>
    /// Абстрактный класс команды бота
    /// </summary>
    public abstract class Command
    {
        //Имя команды
        public abstract string Name { get; }
        //Показывает выполнена команда или ещё нет.
        public bool IsComplete;
        //Метод выполнения команды.
        public abstract Task ExecuteAsync(MessageDetails message);
        //Метод проверки совпадения по имени команды.
        public bool Contains(string command)
        {
            return command.Contains(this.Name) /*&& command.Contains(BotSettings.Name)*/;
        }
    }
}
