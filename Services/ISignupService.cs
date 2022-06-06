using ChatApp.Models;

namespace ChatApp.Services
{
    public interface ISignupService
    {
        List<Signup> Get(int pageIndex);
        Signup Get(string id);
        Signup Create(Signup signup);
        void Update(string id, Signup signup);
        void Delete(string id);

    }
}
