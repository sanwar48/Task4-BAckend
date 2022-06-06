using MongoDB.Driver;
using ChatApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace ChatApp.Validations
{
    public class SignupValivations : ISignupValidations
    {
        private readonly IMongoCollection<Signup> _signupCollection;

        public SignupValivations(IChatAppDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _signupCollection = database.GetCollection<Signup>(settings.SignupCollectionName);
        }

        public bool IsStrongPassword(string password)
        {
           int len = password.Length;
            bool UpperCase = false;
            bool LowerCase = false;
            bool Digit = false;
            bool SpecialChar = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    UpperCase = true;
                }
                else if (char.IsLower(c))
                {
                    LowerCase = true;
                }
                else if (char.IsDigit(c))
                {
                    Digit = true;
                }
                else
                {
                    SpecialChar = true;
                }
            }

            if(len>=8 &&(UpperCase && LowerCase && Digit && SpecialChar))return true;

            return false;
        }

        public bool IsUniqueEamil (string Email)
        {
            var filter = (Builders<Signup>.Filter.Eq("userEmail", Email));
            Signup?  UniquEmail = _signupCollection.Find(filter).FirstOrDefault();
            if(UniquEmail == null)
            {
                return true;
            }
            return false;
        }

        public bool IsUniqueUsername(string userName)
        {
            var filter = (Builders<Signup>.Filter.Eq("userName", userName));
            Signup? UniqueUserName = _signupCollection.Find(filter).FirstOrDefault();

            if(UniqueUserName == null)return true;

            return false;
        }

        public string PasswordEncryp256(string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hashValue;
            UTF8Encoding objUtf8 = new UTF8Encoding();
            hashValue = sha256.ComputeHash(objUtf8.GetBytes(password));
            string EncrypResult = Convert.ToBase64String(hashValue);
            return EncrypResult;
        }
    }
}
