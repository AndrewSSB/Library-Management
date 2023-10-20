using Library_Management.Entities;
using Library_Management.Seeders;

namespace Library_Management.Menu
{
    internal class ApplicationMenu
    {
        private static readonly ApplicationMenu? instance;

        private ApplicationMenu() { }

        public static ApplicationMenu Instance
        {
            get
            {
                if (instance is null)
                    return new ApplicationMenu();

                return instance;
            }
        }

        public void InitApplication()
        {
            Library library = new();

            // Seeders
            library.Books.SeedBooks();
            library.Renters.SeedRenters();

            MenuHelper.HandleMenuCommands(ref library);
        }
    }
}
