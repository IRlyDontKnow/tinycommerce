namespace TinyCommerce.Modules.BackOffice.Domain.Administrators
{
    public interface IAdministratorCounter
    {
        int CountByEmail(string email);
    }
}
