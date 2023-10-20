using Library_Management.Entities;

namespace Library_Management.Seeders
{
    public static class BooksSeeder
    {
        public static List<Book> SeedBooks(this List<Book> books)
        {
            var booksList = new List<Book>
            {
                new Book
                {
                    BookName = "Pro C# 10 with .Net 6: Foundational Principles and Practices in Programming",
                    Quantity = 7,
                    ISBN = "9781484278680",
                    RentingPrice = 20,
                }
                ,new Book
                {
                    BookName = "Pro C# 7: With .NET and .NET Core",
                    Quantity = 3,
                    ISBN = "9781484230176",
                    RentingPrice = 27,
                }
                ,new Book
                {
                    BookName = "A Living Remedy: A Memoir",
                    Quantity = 1,
                    ISBN = "9780063031616",
                    RentingPrice = 41.99f,
                }
                ,new Book
                {
                    BookName = "The Wager: A Tale of Shipwreck, Mutiny and Murder",
                    Quantity = 7,
                    ISBN = "9780385534260",
                    RentingPrice = 24.59f,
                }
                ,new Book
                {
                    BookName = "Y/N: A Novel",
                    Quantity = 1,
                    ISBN = "9781662601538",
                    RentingPrice = 10,
                }
                ,new Book
                {
                    BookName = "The Covenant of Water (Oprah's Book Club)",
                    Quantity = 12,
                    ISBN = "9780802162175",
                    RentingPrice = 19.99f,
                },
            };

            books.AddRange(booksList);

            return books;
        }
    }
}
