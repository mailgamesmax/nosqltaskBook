using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AddBook(Book book) 
        {
        
            var bookCollection = mongoClient.GetDatabase("MyBooks").GetCollection<Book>("Book");            
            bookCollection.InsertOne(book);
        }

        public void AddPage(Page page)
        {

            var pageCollection = mongoClient.GetDatabase("BooksPages").GetCollection<Page>("Page");
            pageCollection.InsertOne(page);
        }
        //print databases
        /*foreach (string database in databases)
        {
            Console.WriteLine(database);
        }*/

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
