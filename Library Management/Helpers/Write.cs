using System.Collections;

namespace Library_Management.Helpers
{
    public static class Write
    {
        public static void WriteToConsole<T>(T obj)
        {
            var TList = obj as IList;

            if (TList is not null) 
            { 
                for (int i = 0; i < TList.Count; i++)
                {
                    Console.WriteLine(TList[i]?.ToString());
                    Console.WriteLine("\n");
                }
            }else
            {
                Console.WriteLine(obj);
            }
        }
    }
}
