namespace Library_Management.Models
{
    public class RenterWithPenalties
    {
        public int PersonId { get; set; }
        public string FullName { get; set; }
        public string CNP { get; set; }

        public override string ToString()
        {
            return $"Person Id: {PersonId}\n" +
                   $"Full name: {FullName}\n" +
                   $"CNP: {CNP} \n";
        }
    }
}
