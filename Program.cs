

namespace BookNoSql
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, nosql...........");

            var mongo = new MongoConnection();
            var book = new Book();
            var page = new Page();

            mongo.importAllBooksFromMongo();
            mongo.importAllPagesFromMongo();
            
            //book.CreateBook();
            page.AddPage();
            Console.WriteLine("*");
            page.AddPage();
            Console.WriteLine("*");


            book.ShowAllBooks();
            Console.WriteLine("***");
            page.ShowAllPages();

            Console.WriteLine("******");


        }
        
    }
}