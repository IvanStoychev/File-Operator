using System;

namespace Backend
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start.\n");

            Startup.InitConfig();

            Console.WriteLine("\nEnd.");
#if DEBUG
            Console.Read();
#endif
        }
    }
}
