namespace Library_Management.Entities
{
    internal sealed class Book : BaseEntity
    {
        private static int NextId = 1;
        public string BookName { get; set; }
        public string ISBN { get; set; }
        public float RentingPrice { get; set; } 
        public int Quantity { get; set; }
        public string Currency {  get; set; }   

        public Book()
        {
            Id = NextId++;
            BookName = string.Empty;
            ISBN = string.Empty;
            Currency = "Lei";
        }

        public override string ToString() =>
            $"Book id: {Id}\n" +
                   $"Book name: {BookName}\n" +
                   $"ISBN: {ISBN}\n" +
                   $"Renting price: {RentingPrice} {Currency}\n" +
                   $"Quantity: {Quantity}";
    }
}
