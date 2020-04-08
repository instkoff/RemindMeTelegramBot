using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Models;
using RemindMeTelegramBotv2.Models.Commands;
using Telegram.Bot;

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
        public async Task MyRemindsListCommandTest()
        {
            var dbRepository = Substitute.For<IDbRepository<RemindEntity>>();
            var command  = new MyRemindsListCommand();
            MessageDetails md = new MessageDetails(Arg.Any<int>(), Arg.Any<long>(), "instkoff", "/myremindslist");

            await command.ExecuteAsync(Substitute.For<TelegramBotClient>(), md, dbRepository);

            Assert.That(command.IsComplete, Is.True);

        }
    }
}