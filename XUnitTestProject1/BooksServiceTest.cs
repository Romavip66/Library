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
    public class BooksServiceTest
    {
        [Fact]
        public async Task AddTest()
        {
            var fake = Mock.Of<IBooksR>();
            var booksService = new BooksService(fake);

            var books = new Books() { Book_Name = "add-name", Author_Name = "add-author" };
            await booksService.AddAndSave(books);
        }
        [Fact]
        public async Task UpdateTest()
        {
            var fake = Mock.Of<IBooksR>();
            var booksService = new BooksService(fake);

            var books = new Books() { Book_Name = "update-name", Author_Name="update-author" };
            await booksService.Update(books);
        }
        [Fact]
        public async Task RemoveTest()
        {
            var fake = Mock.Of<IBooksR>();
            var booksService = new BooksService(fake);
            var books = new Books() { Book_Name = "delete-name", Author_Name="delete-author" };
            await booksService.Delete(books);
        }
        [Fact]
        public async Task DetailTest()
        {
            var fake = Mock.Of<IBooksR>();
            var booksService = new BooksService(fake);
            var id = 2;
            await booksService.DetailsBooks(id);
        }
        [Fact]
        public async Task GetBooksTest()
        {
            var books = new List<Books>
            {
                new Books() { Book_Name = "super-patient", Author_Name="super-patient2" },
                new Books() { Book_Name = "invalid-patient", Author_Name="invalid-patient2" },
            };

            var fakeRepositoryMock = new Mock<IBooksR>();
            fakeRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(books);


            var booksService = new BooksService(fakeRepositoryMock.Object);

            var resultBookss = await booksService.GetBooks();

            Assert.Collection(resultBookss, Books =>
            {
                Assert.Equal("super-patient", Books.Book_Name);
                Assert.Equal("super-patient2", Books.Author_Name);
            },
            Books =>
            {
                Assert.Equal("invalid-patient", Books.Book_Name);
                Assert.Equal("invalid-patient2", Books.Author_Name);
            });
        }
    }
}
