namespace TinyCommerce.Modules.Customers.Application.Authentication
{
    public interface IPasswordHasher
    {
        string HashPassword(string rawPassword);

        bool VerifyHashedPassword(string hashedPassword, string password);
    }
}