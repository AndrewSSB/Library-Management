namespace Library_Management.Entities
{
    public sealed class RentedBook : BaseEntity
    {
        private static int NextId = 1;
        public int PersonId { get; set; }
        public int BookId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime DueToDate { get; set; }
        public bool IsReturned { get; set; }
        public bool HasApplicablePenalty { get; set; }
        
        public float? Penalty { get; set; }
        public int? PenaltyDays { get; set; }

        public RentedBook() { 
            Id = NextId++;
        }

        public override string ToString() =>
            $"Person who rented the book: {PersonId}\n" +
            $"The book that was rent: {BookId}\n" +
            $"Rental date: {RentalDate}\n" +
            $"Is rented due to: {DueToDate}\n" +
            $"The book {(IsReturned ? "is" : "is not")} currently returned\n" +
            $"Days since the book should've been returnd: {PenaltyDays}\n" +
            $"Current penalty: {Penalty}";
    }
}
