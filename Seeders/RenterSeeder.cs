using Library_Management.Entities;

namespace Library_Management.Seeders
{
    internal static class RenterSeeder
    {
        public static List<Person> SeedRenters(this List<Person> renters)
        {
            var newRentest = new List<Person>
            {
                new Person
                {
                    CNP = "1111111111111",
                    FirstName = "Ene",
                    LastName = "Marius",
                    Email = "test@gmail.com",
                    PhoneNumber = "1234567890",
                },
                new Person
                {
                    CNP = "2222222222222",
                    FirstName = "Firicel",
                    LastName = "Ispilante",
                    Email = "ispilante@gmail.com",
                    PhoneNumber = "0987654321",
                }
            };

            renters.AddRange(newRentest);
        
            return renters;
        }
    }
}
