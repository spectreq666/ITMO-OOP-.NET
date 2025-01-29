namespace Lab5.Application.Models.Accounts;

public class Account
{
    public Account(Guid accountId, int number, string pinHash, decimal balance)
    {
        Id = accountId;
        Number = number;
        PinHash = pinHash;
        Balance = balance;
    }

    public Guid Id { get; set; }

    public int Number { get; }

    public string PinHash { get; set; }

    public decimal Balance { get; set; }
}