namespace Lab5.Application.Contracts.Accounts;

public record LoginResult
{
    private LoginResult() { }

    public sealed record Success : LoginResult;

    public sealed record Failed : LoginResult;
}