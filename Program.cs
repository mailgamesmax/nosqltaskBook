

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
            mongo.DbConnect();

            book.CreateBook();
            /*var nbook = book.CreateBook();

            mongo.AddBook(nbook);*/

/*            var createdPage = page.CreateFirstPage(100, "page is Maino");
            mongo.AddPage(createdPage);*/

            //test
            #region
            /*            book.CreateBook();
                        Console.WriteLine(" ---knygos ");
                        book.ShowAllBooks();
                        Console.WriteLine(" ---visi page ");
                        page.ShowAllPages();

                        page.AddPage();
                        Console.WriteLine(" ---visi page ");
                        page.ShowAllPages();
                        Console.WriteLine(" ---knygos ");
                        book.ShowAllBooks();

                        page.AddPage();
                        Console.WriteLine(" ---visi page ");
                        page.ShowAllPages();
                        Console.WriteLine(" ---knygos ");
                        book.ShowAllBooks();*/
            #endregion
            // end test

            //database filling            
            //mongodatabase.Insert()


        }
    }
}