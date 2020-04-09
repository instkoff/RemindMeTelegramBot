using NSubstitute;
using NUnit.Framework;
using RemindMeTelegramBotv2.Models;
using RemindMeTelegramBotv2.Models.Commands;
using RemindMeTelegramBotv2.Services;

namespace RemindMeTelegramBotTests
{
    public class CommnadsTests
    {
        //[SetUp]
        //public void Setup()
        //{
            
        //}
        //[TearDown]
        //public void Teardown()
        //{
            

        //}
        [Test]
        public void GetBotClientTest()
        {
            var botClient = new BotClient();
            Assert.That(botClient.Client, Is.Not.Null);

        }

        [Test]
        public void CommandsCreatorTest()
        {
            var remindService = Substitute.For<IRemindService>();
            var commandCreator = new CommandsCreator(remindService);
            var cmd = commandCreator.CreateCommand("/start");
            Assert.That(cmd, Is.TypeOf<StartCommand>());
        }
    }
}