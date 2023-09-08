using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookNoSql
{
    internal class Page
    {
        public void RecoverPagesFromMongo(int bookId, string content, int pageNr, ObjectId id)
        {
            var recoveredPage = new Page();
            recoveredPage = new Page(bookId, content, pageNr, id);
            AllBooksAllPages.Add(recoveredPage);
        }

        public void AddPageFromApp()
        {
            var mongo = new MongoConnection();
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

                mongo.UpdateBookPagesToMongo(inputBookID, newPageNr);
            }
            else
            {
                Console.WriteLine("-----nerasti page arba knyga------");

            }

            var createdPage = new Page();
            createdPage = new Page(inputBookID, content, newPageNr);            
            mongo.AddPageToMongo(createdPage);
        }


        public void CreateFirstPage(int bookID, string bookTitle)
        {

            string content = $"tai puiki knyga pavadinimu '{bookTitle}'\n";
            var createdPage = new Page(bookID, content);
            AllBooksAllPages.Add(createdPage);

            var mongo = new MongoConnection();
            mongo.DbConnect();
            mongo.AddPageToMongo(createdPage);

        }

        public void ShowAllPages() // CONTROL ONLY
        {
            if (AllBooksAllPages.Count > 0)
            {
                foreach (var page in AllBooksAllPages)
                {
                    Console.WriteLine($"* lapo nr {page.PageNr}-\n knygos id {page.BookId} - lapo turinys {page.Content}");
                }
            }
            else
            {
                Console.WriteLine("------nera knygu, nera ir page-----");
            }

        }

        
        public void UniqIiGenerator(int bookId) 
        {
            string newPageID;
            var bookPages = AllBooksAllPages.Where(p => p.BookId == bookId).ToList();
            int bookPagesQ = bookPages.Count();
            if (bookPagesQ > 1) 
            {
                newPageID = bookId.ToString()+"."+(bookPagesQ+1).ToString();
            }
            else 
            {
                newPageID = bookId.ToString() + "."+"0";
            }
        }
        
        //public static List<int> BooksID = new List<int>()
        //public static Dictionary<int, int> BookPagesID = new Dictionary<int, int>

    

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

        public Page(int bookId, string content, int pageNr, ObjectId id) : this(bookId, content)
        {
            PageNr = pageNr;
            Id = id;
        }

        public ObjectId Id { get; set; }
        public int BookId { get; set; }
        public int PageNr { get; set; }
        public string Content { get; set; }
        public static List<Page> AllBooksAllPages { get; set; } = new List<Page>();

        //pange end
    }
}
