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
        public void RecoverPagesFromMongo(int bookId, string content, int pageNr, string id)
        {
            var recoveredPage = new Page();
            recoveredPage = new Page(bookId, content, pageNr, id);
            AllBooksAllPages.Add(recoveredPage);
        }

        public void AddPage()
        {
            var mongo = new MongoConnection();
            Console.Write("knygos id? ");
            int inputBookID = int.Parse(Console.ReadLine());

            Console.WriteLine("kuriamas tekstas: ");
            string content = Console.ReadLine();

            var allPagesOfselectedBook = AllBooksAllPages.Where(p => p.BookId == inputBookID).ToList();
            if (allPagesOfselectedBook.Count > 0)
            {

                int newPageNr = allPagesOfselectedBook.Count;
                string newPageId = UniqIdPageGenerator(inputBookID, newPageNr);

                var newPage = new Page(inputBookID, content, newPageNr, newPageId);
                AllBooksAllPages.Add(newPage);
                
                var book = new Book();
                book.UpdatePageAmount(inputBookID, newPageNr);
                mongo.UpdateBookPagesToMongo(inputBookID, newPageNr);
     
                mongo.AddPageToMongo(newPage);

            }
            else
            {
                Console.WriteLine("-----nerasti page arba knyga------");
            }

        }

        public void CreateFirstPage(int bookID, string bookTitle)
        {
            string content = $"tai puiki knyga pavadinimu '{bookTitle}'\n";
            string firsPageId = bookID.ToString() + "." + "0";
            var createdPage = new Page(bookID, content, firsPageId);
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

        public string UniqIdPageGenerator(int bookId, int newPageNr)
        {
         string newPageID;
         newPageID = bookId.ToString() + "." + newPageNr.ToString();
         return newPageID;         
        }

        // constructors
        public Page() { }

        public Page(int bookId, string content, string pageUniqId)
        {
            BookId = bookId;
            Content = content;
            PageNr = 0;
            Id = pageUniqId;
        }

        public Page(int bookId, string content, int pageNr, string pageUniqId) : this(bookId, content, pageUniqId)
        {
            PageNr = pageNr;
            
        }
        
        public int BookId { get; set; }
        public int PageNr { get; set; }
        public string Content { get; set; }
        public string Id { get; set; }
        public static List<Page> AllBooksAllPages { get; set; } = new List<Page>();

        //pange end
    }
}
