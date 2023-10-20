namespace Library_Management.Entities
{
    public sealed class Library : BaseEntity
    {
        private static int NextId = 1;
        public List<Book> Books { get; set; }
        public List<RentedBook> RentedBooks { get; set; }
        public List<Person> Renters { get; set; }
        
        public Library()
        {
            Id = NextId++;

            Books = new();
            RentedBooks = new();
            Renters = new();
        }
    }
}
