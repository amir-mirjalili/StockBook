using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using StockBook.Models;

namespace StockBook.Service
{
    public class BooksService
    {
        private readonly IMongoCollection<BookModel> _booksCollection;
        public BooksService(IOptions<StockBookDataBaseSettings> stockBookDataBaseSettings)
        {
            var mongoClient = new MongoClient(stockBookDataBaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(stockBookDataBaseSettings.Value.DatabaseName);
            _booksCollection = mongoDatabase.GetCollection<BookModel>(stockBookDataBaseSettings.Value.BooksCollectionName);
        }
        public async Task<List<BookModel>> GetAll() => await _booksCollection.Find(_ => true).ToListAsync();
        public async Task<BookModel?> GetById(string id) => await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task Create(BookModel book) => await _booksCollection.InsertOneAsync(book);
        public async Task Replace(string id, BookModel newBook)=>await _booksCollection.ReplaceOneAsync(x=>x.Id==id, newBook);
        public async Task Remove(string id)=>await _booksCollection.DeleteOneAsync(x=>x.Id==id); 

    }
}
