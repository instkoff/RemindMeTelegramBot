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
        public void StartCommandTest()
        {
            MessageDetails message = new MessageDetails(Arg.Any<int>(),Arg.Any<long>(),"instkoff", "/start");
            var botClient = new BotClient();
            var command = new StartCommand(botClient);
            command.ExecuteAsync(message).GetAwaiter().GetResult();
            Assert.That(command.IsComplete, Is.True);
        }
    }
}