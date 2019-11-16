using LibraryCourse.Abstractions;
using LibraryCourse.Models;
using LibraryCourse.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class HistoryServiceTest
    {
        [Fact]
        public async Task AddTest()
        {
            var fake = Mock.Of<IHistoryR>();
            var historyService = new HistoryService(fake);

            var history = new History() { CardId = 1, BookId = 1};
            await historyService.AddAndSave(history);
        }
        [Fact]
        public async Task UpdateTest()
        {
            var fake = Mock.Of<IHistoryR>();
            var historyService = new HistoryService(fake);

            var history = new History() { CardId = 2, BookId =2};
            await historyService.Update(history);
        }
        [Fact]
        public async Task RemoveTest()
        {
            var fake = Mock.Of<IHistoryR>();
            var historyService = new HistoryService(fake);
            var history = new History() { CardId = 3, BookId= 3};
            await historyService.Delete(history);
        }
        [Fact]
        public async Task DetailTest()
        {
            var fake = Mock.Of<IHistoryR>();
            var historyService = new HistoryService(fake);
            var id = 2;
            await historyService.DetailsHistory(id);
        }
        [Fact]
        public async Task GetHistoryTest()
        {
            var history = new List<History>
            {
                new History() { CardId = 4, BookId=4 },
                new History() { CardId=5, BookId=5 },
        };

            var fakeRepositoryMock = new Mock<IHistoryR>();
            fakeRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(history);


            var historyService = new HistoryService(fakeRepositoryMock.Object);

            var resultHistory = await historyService.GetHistory();

            Assert.Collection(resultHistory, history =>
            {
                Assert.Equal(4, history.CardId);
                Assert.Equal(4, history.BookId);
            },
            history =>
            {
                Assert.Equal(5, history.CardId);
                Assert.Equal(5, history.BookId);

            });
        }
    }
}
