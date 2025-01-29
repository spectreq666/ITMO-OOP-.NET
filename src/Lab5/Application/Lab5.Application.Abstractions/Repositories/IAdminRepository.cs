namespace Lab5.Application.Abstractions.Repositories;

public interface IAdminRepository
{
    string GetCurrentMasterKey();

    void SaveCurrentMasterKey(string masterKey);
}