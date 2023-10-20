using Library_Management.Entities;
using Library_Management.Enums;
using Library_Management.Helpers;
using Library_Management.Services.LibraryServices;

namespace Library_Management.Menu
{
    internal static class MenuHelper
    {
        public static void HandleMenuCommands(ref Library library)
        {
            string? code;
            ILibraryService libraryService = new LibraryService();

            while (true)
            {
                Write.WriteToConsole(MenuCommands.AddBook);
                Write.WriteToConsole(MenuCommands.AvailableBooks);
                Write.WriteToConsole(MenuCommands.RentedBooks);
                Write.WriteToConsole(MenuCommands.NumberOfCopiesOfABook);
                Write.WriteToConsole(MenuCommands.RentBook);
                Write.WriteToConsole(MenuCommands.ReturnBook);
                Write.WriteToConsole(MenuCommands.ListOfReters);
                Write.WriteToConsole(MenuCommands.ListOfRetersWithPenalties);
                Write.WriteToConsole(MenuCommands.AppluPenalty);
                Write.WriteToConsole(MenuCommands.ApplyPenalties);
                Write.WriteToConsole(MenuCommands.CloseApplication);

                code = Console.ReadLine();
                switch (code)
                {
                    case "1":
                        libraryService.AddBook(library.Books);
                        break;
                    case "2":
                        libraryService.DisplayBooks(library.Books);
                        break;
                    case "3":
                        libraryService.DisplayRentedBooks(library.RentedBooks);
                        break;
                    case "4":
                        libraryService.GetNumberOfAvailableCopies(library.Books);
                        break;
                    case "5":
                        libraryService.RentBook(library.Books, library.RentedBooks, library.Renters);
                        break;
                    case "6":
                        libraryService.ReturnBook(library.Books, library.RentedBooks, library.Renters);
                        break;
                    case "7":
                        libraryService.DisplayListOfRenters(library.Renters);
                        break;
                    case "8":
                        libraryService.DisplayListOfRentersWithPenalty(library.Books, library.RentedBooks, library.Renters);
                        break;
                    case "9":
                        libraryService.ApplyPenaltyForUserForBook(library.Books, library.RentedBooks);
                        break;
                    case "10":
                        libraryService.ApplyPenaltyForUserForBooks(library.Books, library.RentedBooks);
                        break;
                    case "x":
                    default: return;
                }
            }
        }
    }
}
