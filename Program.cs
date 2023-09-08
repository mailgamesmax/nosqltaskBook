

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

            //book.CreateBook();
            //page.AddPageFromApp();
            mongo.importAllBooksFromMongo();
            mongo.importAllPagesFromMongo();


            book.ShowAllBooks();
            Console.WriteLine("***");
            page.ShowAllPages();

            Console.WriteLine("******");
            string a = UniqIdGenerator(2);

            Console.WriteLine(a);


        }
        public static string UniqIdGenerator(int bookId)
        {

            string newPageID;
            var bookPages = Page.AllBooksAllPages.Where(p => p.BookId == bookId).ToList();
            int bookPagesQ = bookPages.Count();
            if (bookPagesQ > 1)
            {
                newPageID = bookId.ToString() + "." + (bookPagesQ + 1).ToString();
                return newPageID;
            }
            else
            {
                newPageID = bookId.ToString() + "." + "0";
                return newPageID;
            }
        }
    }
}