using Library_Management.Entities;

namespace Library_Management.Services.LibraryServices
{
    public interface ILibraryService
    {   
        /// <summary>
        /// Add a book to the current book list.
        /// 
        /// </summary>
        /// <param name="books">Used to replicate a context for the app instance</param>
        /// <param name="book">A new book that will be added to the list</param>
        void AddBook(List<Book> books);

        /// <summary>
        /// Display the books
        /// </summary>
        /// <param name="books">Used to replicate a context for the app instance</param>
        void DisplayBooks(List<Book> books);

        /// <summary>
        /// Display the rented books, currently does not support filtering
        /// </summary>
        /// <param name="rentedBooks"></param>
        void DisplayRentedBooks(List<RentedBook> rentedBooks);

        /// <summary>
        /// Return the number of copies of a specified book.
        /// </summary>
        /// <param name="books">Used to replicate a context for the app instance</param>
        /// <param name="model">It contains ISBN and BookId, properties by which we are looking for the number of copies.</param>
        /// <returns></returns>
        int GetNumberOfAvailableCopies(List<Book> books);

        /// <summary>
        /// Rent a book.
        /// </summary>
        /// <param name="books">Used to replicate a context for the app instance</param>
        /// <param name="rentedBooks"></param>
        /// <param name="ISBN"></param>
        void RentBook(List<Book> books, List<RentedBook> rentedBooks, List<Person> renters);

        /// <summary>
        /// Return a book.
        /// </summary>
        /// <param name="books">Used to replicate a context for the app instance</param>
        /// <param name="rentedBooks"></param>
        /// <param name="renters"></param>
        /// <param name="ISBN"></param>
        void ReturnBook(List<Book> books, List<RentedBook> rentedBooks, List<Person> renters);

        /// <summary>
        /// Display the current renters in our library
        /// </summary>
        /// <param name="renters"></param>
        void DisplayListOfRenters(List<Person> renters);

        void DisplayListOfRentersWithPenalty(List<Book> books, List<RentedBook> rentedBooks, List<Person> renters);

        void ApplyPenaltyForUserForBook(List<Book> books, List<RentedBook> rentedBooks);

        void ApplyPenaltyForUserForBooks(List<Book> books, List<RentedBook> rentedBooks);
    }
}
