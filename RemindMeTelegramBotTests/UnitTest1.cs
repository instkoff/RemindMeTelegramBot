using System.Collections.Generic;
using System.Linq.Expressions;
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
        public void GetBotClientTest()
        {
            var botClient = new BotClient();
            Assert.That(botClient.Client, Is.Not.Null);

        }

        [Test]
        public async Task StartCommandTest()
        {
            var dbRepository = Substitute.For<IDbRepository<RemindEntity>>();
            var remindsList = new List<RemindEntity>();
            await dbRepository.GetListAsync(Arg.Any<Expression>()).ReturnsForAnyArgs(remindsList);
        }
    }
}