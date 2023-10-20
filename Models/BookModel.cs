namespace Library_Management.Models
{
    public class BookModel
    {
        public string BookName { get; set; }
        public string ISBN { get; set; }

        public override string ToString()
        {
            return $"Book name: {BookName}\n" +
                   $"ISBN: {ISBN}";
        }
    }
}
