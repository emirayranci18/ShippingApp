using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace yazlabdeneme
{
    class MongoDeneme
    {
         public MongoDeneme()
        {
            try
            {
                Console.WriteLine("Geldi");
                var client = new MongoClient("mongodb+srv://fatihtekin:123@cluster0.stzil.mongodb.net/movies?retryWrites=true&w=majority");
                var db = client.GetDatabase("admin");
                foreach (var item in db.ListCollectionsAsync().Result.ToListAsync<BsonDocument>().Result)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Gitti");
                throw ex;
            }
        }
    }
}
