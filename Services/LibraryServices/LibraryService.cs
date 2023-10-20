using Library_Management.Entities;
using Library_Management.Helpers;
using Library_Management.Models;

namespace Library_Management.Services.LibraryServices
{
    internal class LibraryService : ILibraryService 
    {
        private const float penaltyTaxPercent = 1;

        public void AddBook(List<Book> books)
        {
            Read.ReadCustomTypeFromConsole<Book>(out var book);

            var existingBook = books.FirstOrDefault(b => b.ISBN.Equals(book.ISBN));
        
            if (existingBook is not null)
            {
                existingBook.Quantity += 1;
                return;
            }

            books.Add(book);
        }

        public void DisplayBooks(List<Book> books)
        {
            Write.WriteToConsole(books);
        }

        public void DisplayRentedBooks(List<RentedBook> rentedBooks)
        {
            Write.WriteToConsole(rentedBooks.Where(rb => !rb.IsReturned).ToList());
        }
        
        public void RentBook(List<Book> books, List<RentedBook> rentedBooks, List<Person> renters)
        {
            Console.WriteLine($"Insert CNP: \n");
            Read.ReadPrimitiveTypeFromConsole<string>(out var CNP);
            if (string.IsNullOrEmpty(CNP) || CNP.Length != 13)
            {
                Console.WriteLine($"You have entered an invalid CNP");
                return;
            }

            var renter = renters.FirstOrDefault(r => r.CNP.Equals(CNP));
            if (renter is null)
            {
                Console.WriteLine("The person is not registered in our system, register now:\n");
                Read.ReadCustomTypeFromConsole<Person>(out renter);

                if (string.IsNullOrEmpty(renter.FirstName) || string.IsNullOrEmpty(renter.LastName) || string.IsNullOrEmpty(renter.CNP))
                {
                    Console.WriteLine("Invalid registration data");
                    return;
                }
            }else
            {
                var penaltyForBooks = rentedBooks.Where(rb => rb.PersonId == renter.Id && !rb.IsReturned && rb.HasApplicablePenalty).Select(x => x.BookId).ToList();
                var penaltyBooksToPay = books.Where(x => penaltyForBooks.Contains(x.Id)).ToList();
                if (penaltyBooksToPay.Count > 0)
                {
                    Console.WriteLine("You can't rent another book untill you pay the penalty.");
                    Console.WriteLine($"Your penalty is: {penaltyBooksToPay.Sum(p => p.RentingPrice)}\n for: \n");
                    Write.WriteToConsole(penaltyBooksToPay);
                    return;
                }
            }

            renters.Add(renter);

            Console.WriteLine("What book do you want to rent ?\n");
            Read.ReadCustomTypeFromConsole<RentBookModel>(out var model);

            if (string.IsNullOrEmpty(model.ISBN) && string.IsNullOrEmpty(model.BookName))
            {
                Console.WriteLine("Please provide a book name or ISBN");
                return;
            }

            Book? book = null;

            if (!string.IsNullOrEmpty(model.ISBN))
            {
                book = books.FirstOrDefault(b => b.ISBN == model.ISBN);
            }else if (!string.IsNullOrEmpty(model.BookName))
            {
                book = books.FirstOrDefault(b => b.BookName.ToLower().Contains(model.BookName.ToLower()));
            }

            if (book is null)
            {
                Console.WriteLine($"The book with the ISBN: {model.ISBN} / Book name: {model.BookName} does not exist in our library");
                return;
            }

            if (book.Quantity == 0)
            {
                Console.WriteLine($"We're sorry, but we don't have {book.BookName} does not exist in our stock.");
                return;
            }

            book.Quantity -= 1;

            rentedBooks.Add(new RentedBook
            {
                BookId = book.Id,
                PersonId = 1,
                RentalDate = DateTime.Now,
                DueToDate = DateTime.Now.AddDays(14),
            });
        }

        public void ReturnBook(List<Book> books, List<RentedBook> rentedBooks, List<Person> renters)
        {
            Console.WriteLine($"Insert CNP: \n");
            Read.ReadPrimitiveTypeFromConsole<string>(out var CNP);

            if (string.IsNullOrEmpty(CNP) || CNP.Length != 13)
            {
                Console.WriteLine($"You have entered an invalid CNP");
                return;
            }

            var renter = renters.FirstOrDefault(r => r.CNP.Equals(CNP));
            if (renter is null) // maybe add a rule for the CNP to look more nice
            {
                Console.WriteLine($"You are not registerd as the person who rented the book");
                return;
            }

            // Display the currently books rented by this person
            var rentedBooksByRenter = rentedBooks
                .Where(r => r.PersonId == renter.Id && !r.IsReturned)
                .Join(
                    books, 
                    rb => rb.BookId,
                    b => b.Id,
                    (br, b) => 
                        new 
                        {
                            b.BookName,
                            b.ISBN,
                            b.RentingPrice
                        }
                    )
                .ToList();
            
            if (rentedBooksByRenter.Count > 0)
            {
                Console.WriteLine($"You have {rentedBooksByRenter.Count} books rented. \n");
                Write.WriteToConsole(rentedBooksByRenter);
            }

            Console.WriteLine("What book do you want to return ?\n Inser ISBN: \n");
            Read.ReadPrimitiveTypeFromConsole<string>(out var ISBN);

            var book = books.FirstOrDefault(b => b.ISBN == ISBN);
            if (book is null)
            {
                Console.WriteLine($"The book with the ISBN: {ISBN} does not exist in our library");
                return;
            }

            book.Quantity += 1;

            var rentedBook = rentedBooks.FirstOrDefault(b => b.BookId == book.Id);
            if (rentedBook is null) // Probably won't happen
            {
                Console.WriteLine($"There is a problem in our system, please speak with an administrator." +
                                  $"The book with ISBN: {ISBN} does not exist in the rented books table.");
                return;
            }

            if (DateTime.Now.Date > rentedBook.DueToDate.Date)
            {
                rentedBook.HasApplicablePenalty = true;
                rentedBook.PenaltyDays = (DateTime.Now - rentedBook.DueToDate).Days;
                rentedBook.Penalty = penaltyTaxPercent / 100 * book.RentingPrice * rentedBook.PenaltyDays;
            }
            
            rentedBook.IsReturned = true;
        }
        
        public void DisplayListOfRenters(List<Person> renters)
        {
            if (renters.Count == 0)
            {
                Console.WriteLine("There are no renters in our library.");
            }

            Write.WriteToConsole(renters);
        }

        public void DisplayListOfRentersWithPenalty(List<Book> books, List<RentedBook> rentedBooks, List<Person> renters)
        {
            var rentersIds = renters.Select(r => r.Id).ToList();

            var rentersWithPenalties = rentedBooks
                .Where(r => rentersIds.Contains(r.PersonId) && !r.IsReturned && r.HasApplicablePenalty)
                .Join(
                    renters,
                    rb => rb.PersonId,
                    re => re.Id,
                    (rb, re) =>
                        new RenterWithPenalties
                        {
                            PersonId = re.Id,
                            FullName = re.FullName,
                            CNP = re.CNP,
                        }
                    )
                .ToList();

            if (rentersWithPenalties.Count == 0)
            {
                Console.WriteLine("There are no renters with penalties.");
            }

            Write.WriteToConsole(rentersWithPenalties);
        }

        public int GetNumberOfAvailableCopies(List<Book> books)
        {
            Read.ReadCustomTypeFromConsole<GetNumberOfCopiesModel>(out var model);
            var numberOfCopies = 0;

            if (string.IsNullOrEmpty(model.ISBN) && model.BookId is null)
            {
                return 0;
            }

            if (model.BookId.HasValue)
            {
                numberOfCopies = books.Where(b => b.Id == model.BookId).Select(x => x.Quantity).FirstOrDefault();
            }

            if (!string.IsNullOrEmpty(model.ISBN))
            {

                numberOfCopies = books.Where(b => b.ISBN.Equals(model.ISBN)).Select(x => x.Quantity).FirstOrDefault();
            }

            Console.WriteLine($"There are {numberOfCopies} copies of the specified book.");
            return numberOfCopies;
        }


        // Trigger penalties
        public void ApplyPenaltyForUserForBook(List<Book> books, List<RentedBook> rentedBooks)
        {
            Console.WriteLine($"Insert person Id: \n");
            Read.ReadPrimitiveTypeFromConsole<int>(out var personId);
            
            Console.WriteLine($"Insert ISBN: \n");
            Read.ReadPrimitiveTypeFromConsole<string>(out var ISBN);

            Console.WriteLine($"Insert penalty days: \n");
            Read.ReadPrimitiveTypeFromConsole<int>(out var penaltyDays);

            var rentedBook = rentedBooks.FirstOrDefault(rb => rb.PersonId == personId);
            var book = books.FirstOrDefault(b => b.ISBN.Equals(ISBN));

            if (rentedBook is not null && book is not null)
            {
                rentedBook.HasApplicablePenalty = true;
                rentedBook.PenaltyDays = penaltyDays;
                rentedBook.Penalty = float.Round(penaltyTaxPercent / 100 * book.RentingPrice * penaltyDays, 2);
            }

            Console.WriteLine($"Penalty applied with success");
        }

        public void ApplyPenaltyForUserForBooks(List<Book> books, List<RentedBook> rentedBooks)
        {
            Console.WriteLine($"Insert person Id: \n");
            Read.ReadPrimitiveTypeFromConsole<int>(out var personId);

            Console.WriteLine($"Insert penalty days: \n");
            Read.ReadPrimitiveTypeFromConsole<int>(out var penaltyDays);

            var booksRentedByPerson = rentedBooks.Where(rb => rb.PersonId == personId).ToList();

            foreach (var item in booksRentedByPerson)
            {
                var book = books.FirstOrDefault(b => b.Id == item.BookId);

                if (book is null) continue;

                item.HasApplicablePenalty = true;
                item.PenaltyDays = penaltyDays;
                item.Penalty = penaltyTaxPercent / 100 * book.RentingPrice * penaltyDays;
            }

            Console.WriteLine($"Penalties applied with success");
        }
    }
}
