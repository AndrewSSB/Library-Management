using System.Reflection;

namespace Library_Management.Helpers
{
    public static class Read
    {
        public static void ReadCustomTypeFromConsole<T>(out T instance) where T : class
        {
            var props = typeof(T).GetProperties();
            instance = Activator.CreateInstance<T>();

            foreach (var prop in props)
            {
                if (prop.GetCustomAttribute<IgnoreProperty>() != null) continue;

                var propMethod = prop.GetGetMethod();

                Console.WriteLine($"Insert {prop.Name} which is a type of {prop.PropertyType.Name.ToLower()}");

                var userInput = Console.ReadLine();

                if (string.IsNullOrEmpty(userInput)) continue;

                var value = Convert.ChangeType(userInput, prop.PropertyType);

                if (propMethod is not null && propMethod.IsStatic)
                {
                    prop.SetValue(null, value);
                    continue;
                }

                prop.SetValue(instance, value);
            }
        }

        public static void ReadPrimitiveTypeFromConsole<T>(out T? variable)
        {
            Console.WriteLine($"Insert a value of type {typeof(T).Name.ToLower()}");

            var value = Convert.ChangeType(Console.ReadLine(), typeof(T));

            variable = value is null ? default : (T)value;
        }
    }
}
