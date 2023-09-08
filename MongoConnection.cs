using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;

namespace BookNoSql
{
    internal class MongoConnection
    {
        public void DbConnect()
        {
            mongoClient = new MongoClient("mongodb+srv://booknosqlowner:Booknosql@cluster0.od3kscm.mongodb.net/?retryWrites=true&w=majority");
            //mongodb+srv://booknosqlowner:<123+Mymongo password>@cluster0.od3kscm.mongodb.net/?retryWrites=true&w=majority

            //List<string> databases = client.ListDatabaseNames().ToList();
        }

        public void importAllBooksFromMongo()
        {
            DbConnect();
            var bookCollection = mongoClient.GetDatabase("MyBooks").GetCollection<Book>("Book");
            var allBooks = bookCollection.Find(_ => true).ToList();

            var book = new Book();
            foreach (var oneBook in allBooks) 
            {
                book.RecoverBookFromMongo(oneBook.Id, oneBook.Title, oneBook.PagesAmount);
            }
        }

        public void importAllPagesFromMongo()
        {
            DbConnect();
            var pageCollection = mongoClient.GetDatabase("BooksPages").GetCollection<Page>("Page");
            var allPages = pageCollection.Find(_ => true).ToList();

            var page = new Page();
            foreach (var onePage in allPages)
            {
                page.RecoverPagesFromMongo(onePage.BookId, onePage.Content, onePage.PageNr, onePage.Id);
            }
        }

        public void AddBookToMongo(Book book) 
        {            
            DbConnect();

            var bookCollection = mongoClient.GetDatabase("MyBooks").GetCollection<Book>("Book");
            bookCollection.InsertOne(book);
        }

        public void AddPageToMongo(Page page)
        {
            DbConnect();

            var pageCollection = mongoClient.GetDatabase("BooksPages").GetCollection<Page>("Page");
            pageCollection.InsertOne(page);
        }
        //print databases
        /*foreach (string database in databases)
        {
            Console.WriteLine(database);
        }*/
        public void UpdateBookPagesToMongo(int bookId, int newPageNr) 
        {
            //var mongo = new MongoConnection();
            DbConnect();
            var bookCollection = mongoClient.GetDatabase("MyBooks").GetCollection<Book>("Book");

            var filterById = Builders<Book>.Filter.Eq("Id", bookId);
            var selectedBooks = bookCollection.Find(filterById).ToList();
            var updatePageAmount = Builders<Book>.Update.Set("PagesAmount", newPageNr);
            
            var updatesResult = bookCollection.UpdateMany(filterById, updatePageAmount);
            if (updatesResult.ModifiedCount > 0)
            {
                Console.WriteLine("Book updates done");
            }
            else
            {
                Console.WriteLine("Boooks uptades - something wrong");
            }

        }

        public List<Book> SelectBookById(int bookId)
        {
            var bookCollection = mongoClient.GetDatabase("MyBooks").GetCollection<Book>("Book");
            var selectedBook = Builders<Book>.Filter.Eq("Id", bookId);
            var results = bookCollection.Find(selectedBook).ToList();
            return results;
        }

        public List<Book> FatchBook(string bookName) 
        {
            var bookCollection = mongoClient.GetDatabase("MyBooks").GetCollection<Book>("Book");
            var selectedBook = Builders<Book>.Filter.Eq("Title", bookName);
            var results = bookCollection.Find(selectedBook).ToList();
            return results;
        }
        public MongoClient mongoClient { get; set; }
    }

}
