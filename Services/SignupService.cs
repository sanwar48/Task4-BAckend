using ChatApp.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;

namespace ChatApp.Services
{
    public class SignupService : ISignupService
    {
        private readonly IMongoCollection<Signup> _signupCollection;

        public SignupService(IChatAppDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _signupCollection = database.GetCollection<Signup>(settings.SignupCollectionName);
            
        }

        public Signup Create(Signup signup)
        {
          _signupCollection.InsertOne(signup);
            return signup;
        }

        public void Delete(string id)
        {
            _signupCollection.DeleteOne(Signup => Signup.Id == id);
        }

        public List<Signup> Get(int pageIndex)
        {
            var quareable = _signupCollection.AsQueryable();
            ///collection.AsQueryable().Sample(1).First().GetValue("name").ToString();
            return quareable.OrderBy(x => x.Id).Skip((pageIndex-1)*10).Take(10).ToList();
            //return quareable.Sample(1).First().GetValue("Email").ToList();
        }

        public Signup Get(string id)
        {
            return _signupCollection.Find(Signup => Signup.Id == id).FirstOrDefault();
        }

        public void Update(string id, Signup signup)
        {
            _signupCollection.ReplaceOne(Singup => Singup.Id == id, signup);
        }
    }
}
