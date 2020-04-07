using NUnit.Framework;
using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Models;
using RemindMeTelegramBotv2.Services;

namespace RemindMeTelegramBotTests
{
    public class Tests
    {
        private UpdateService _updateService;
        [SetUp]
        public void Setup()
        {
            _updateService = new UpdateService(new BotClient(), new DbRepository<RemindEntity>(new DbContext(new DatabaseSettings())), new RemindService());
        }
        [TearDown]
        public void Teardown()
        {
            _updateService = null;

        }
        [Test]
        public void Test1()
        {
            MessageDetails message = new MessageDetails(154546,56487, "instkoff","/start");
            //Assert.That(_updateService.AnswerOnMessageAsync(message), );
        }
    }
}