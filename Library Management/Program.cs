using Library_Management.Menu;

namespace Library_Management
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var menu = ApplicationMenu.Instance;

            menu.InitApplication();
        }
    }
}
