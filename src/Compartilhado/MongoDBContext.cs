using MongoDB.Driver;

namespace Compartilhado
{
    public class MongoDBContext
    {

        private IMongoDatabase MongoDataBase { get; set; }

        public MongoDBContext()
        {
            this.MongoDataBase = (new MongoClient("mongodb://localhost:27017")).GetDatabase("admin");
        }

        public IMongoCollection<Produto> Produtos { get => this.MongoDataBase.GetCollection<Produto>("Produtos");  }
    }
}
