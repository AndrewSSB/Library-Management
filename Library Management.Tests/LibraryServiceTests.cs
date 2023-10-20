using Library_Management.Entities;
using Library_Management.Seeders;
using Library_Management.Services.LibraryServices;

namespace Library_Management.Tests
{
    public class Tests
    {
        private ILibraryService _libraryService;
        private Library _library;

        [SetUp]
        public void Setup()
        {
            _libraryService = new LibraryService();
            _library= new Library();
        }

        [Test]
        public void SeedBooks()
        {
            _library.Books.SeedBooks();

            Assert.That(_library.Books, Is.Not.Empty);
        }

        [Test]
        public void SeedRenters()
        {
            _library.Renters.SeedRenters();

            Assert.That(_library.Renters, Is.Not.Empty);
        }
    }
}