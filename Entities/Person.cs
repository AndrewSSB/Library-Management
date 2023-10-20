using Library_Management.Helpers;

namespace Library_Management.Entities
{
    internal sealed class Person : BaseEntity
    {
        [IgnoreProperty]
        private static int NextId = 1;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [IgnoreProperty]
        public string FullName => $"{FirstName} {LastName}";
        public string CNP { get; set; }
        [IgnoreProperty]
        public string? Email { get; set; }  
        [IgnoreProperty]
        public string? PhoneNumber { get; set; }

        public Person() 
        { 
            Id = NextId++;

            FirstName = string.Empty; 
            LastName = string.Empty; 
            CNP = string.Empty;
        }

        public override string ToString()
        {
            return $"Person Id: {Id}\n" +
                   $"First name: {FirstName}\n" +
                   $"Last name: {LastName}\n" +
                   $"CNP: {CNP}\n" +
                   (!string.IsNullOrEmpty(Email) ? $"Email: {Email}\n" : string.Empty) +
                   (!string.IsNullOrEmpty(PhoneNumber) ? $"Phone number: {PhoneNumber}" : string.Empty);
        }

    }
}
