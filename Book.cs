using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace BookNoSql
{
    internal class Book
    {
        public void RecoverBookFromMongo(int id, string title, int pageAmount)
        {
            var createdBook = new Book();        
            createdBook = new Book(id, title, pageAmount);
            AllBooks.Add(createdBook);
        }

        public void CreateBook()
        {
            var createdBook = new Book();
            Console.Write("knygos pav? ");
            string newBookTitle = Console.ReadLine();
            Console.Write("naujos knygos ID? ");
            int createdID = int.Parse(Console.ReadLine());
            createdBook = new Book(createdID, newBookTitle);
            AllBooks.Add(createdBook);

            var mongo = new MongoConnection();
            mongo.AddBookToMongo(createdBook);

            var page = new Page();
            page.CreateFirstPage(createdID, newBookTitle);

            //return createdBook;
                      
        }

        public void UpdatePageAmount(int bookId, int pageNr) 
        {
            var selectBook = AllBooks.FirstOrDefault(b => b.Id == bookId);
            selectBook.PagesAmount = pageNr;
        }

        public void ShowAllBooks() 
        {
            if (AllBooks.Count>0) 
            {
            int i = 1;
            foreach (var book in AllBooks) 
            {                
                Console.WriteLine($"{i}): {book.Id} knygoj '{book.Title}' yra lapu: {book.PagesAmount}");
                    i++;
            }
            }
            else
            {
                Console.WriteLine("------nera knygu-----");
            }
        
        }


        public Book() { }

        public Book(int id, string title)
        {
            Id = id;
            Title = title;
            PagesAmount = 0;
        }
        public Book(int id, string title, int pageAmount)
        {
            Id = id;
            Title = title;
            PagesAmount = pageAmount;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int PagesAmount { get; set; }
        
        //public List<Page> Pages { get; set; }
        public static List<Book> AllBooks { get; set; } = new List<Book>();

        //book end
    }
 





}
