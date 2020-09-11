using System;
using System.Collections.Generic;

namespace EFCoreConsole
{
    class Program
    {
        public static void MainDB()
        {
            using (var db = new BloggingContext())
            {
                for (int idx = 0; idx < 1000; idx++)
                {
                    Random random = new Random();
                    int num = random.Next(1000);
                    Author myAuthor = new Author
                    {
                        FirstName = "Per" + num.ToString(),
                        LastName = "Madsen"
                    };

                    db.Authors.Add(myAuthor);

                    var count = db.SaveChanges();
                    Console.WriteLine("{0} records saved to database", count);

                    Console.WriteLine();
                    Console.WriteLine("All Authors in database:");
                    foreach (var author in db.Authors)
                    {
                        Console.WriteLine(" - {0}", author.FirstName);
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            MainDB();
            Console.WriteLine("Hello World: EFCoreConsole");
            Console.ReadLine();
        }
    }
}
