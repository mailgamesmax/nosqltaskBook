using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookNoSql
{
    internal class Page
    {
        public void AddPage()
        {
            Console.Write("knygos id? ");
            int inputBookID = int.Parse(Console.ReadLine());

            Console.WriteLine("kuriamas tekstas: ");
            string content = Console.ReadLine();

            int newPageNr = 1;
            var allPagesOfselectedBook = AllBooksAllPages.Where(p => p.BookId == inputBookID).ToList();
            if (allPagesOfselectedBook.Count > 0)
            {

                newPageNr = allPagesOfselectedBook.Count;

                var newPage = new Page(inputBookID, content, newPageNr);
                AllBooksAllPages.Add(newPage);
                var book = new Book();
                book.UpdatePageAmount(inputBookID, newPageNr);
            }
            else
            {
                Console.WriteLine("-----nerasti page arba knyga------");

            }

            var createdPage = new Page();
            createdPage = new Page(inputBookID, content, newPageNr);
        }

        public void CreateFirstPage(int bookID, string bookTitle)
        {

            string content = $"tai puiki knyga pavadinimu '{bookTitle}'\n";
            var createdPage = new Page(bookID, content);
            AllBooksAllPages.Add(createdPage);

            var mongo = new MongoConnection();
            mongo.AddPage(createdPage);

        }

        public void ShowAllPages() // CONTROL ONLY
        {
            if (AllBooksAllPages.Count > 0)
            {
                foreach (var page in AllBooksAllPages)
                {
                    Console.WriteLine($"{page.PageNr}-\n {page.BookId} - {page.Content}");
                }
            }
            else
            {
                Console.WriteLine("------nera knygu, nera ir page-----");
            }

        }
        // constructors
        public Page() { }

        public Page(int bookId, string content)
        {
            BookId = bookId;
            Content = content;
            PageNr = 0;

        }

        public Page(int bookId, string content, int pageNr) : this(bookId, content)
        {
            PageNr = pageNr;
        }

        public int BookId { get; set; }
        public int PageNr { get; set; }
        public string Content { get; set; }
        public static List<Page> AllBooksAllPages { get; set; } = new List<Page>();

        //pange end
    }
}
