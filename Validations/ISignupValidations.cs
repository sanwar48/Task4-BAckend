using ChatApp.Models;
namespace ChatApp.Validations
{
    public interface ISignupValidations
    {
         bool IsUniqueEamil(string Email);
         bool IsUniqueUsername(string userName);
         bool IsStrongPassword(string password);
         string PasswordEncryp256(string password);
    }
}
