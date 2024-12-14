namespace Lab5.Application.Models.Accounts;

public class AdminAccount
{
    public AdminAccount(Guid accountId, string name, string pinHash)
    {
        Id = accountId;
        Name = name;
        PinHash = pinHash;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string PinHash { get; set; }
}