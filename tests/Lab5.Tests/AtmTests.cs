using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Models.Accounts;
using Lab5.Application.Models.Transactions;
using Lab5.Application.Transactions;
using Moq;
using Xunit;

namespace Lab5.Tests;

public class AtmTests
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
    private readonly TransactionService _transactionService;

    public AtmTests()
    {
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _transactionRepositoryMock = new Mock<ITransactionRepository>();
        _transactionService = new TransactionService(_transactionRepositoryMock.Object, _accountRepositoryMock.Object);
    }

    [Fact]
    public void TransactionService_DepositMoney_IncreaseBalance_Success()
    {
        var account = new Account(Guid.Empty, 10, "abcabc", 0);

        _accountRepositoryMock
            .Setup(repo => repo.GetById(account.Id))
            .Returns(account);

        decimal depositAmount = 1000;

        _transactionService.CreateTransaction(account.Id, depositAmount, TransactionType.Deposit);

        _accountRepositoryMock.Verify(repo => repo.Save(It.Is<Account>(a => a.Balance == 1000)), Times.Once);
    }

    [Fact]
    public void TransactionService_WithdrawMoney_Success()
    {
        var account = new Account(Guid.Empty, 10, "abcabc", 10000);

        _accountRepositoryMock
            .Setup(repo => repo.GetById(account.Id))
            .Returns(account);

        decimal withdrawAmount = 5000;

        _transactionService.CreateTransaction(account.Id, withdrawAmount, TransactionType.Withdrawal);

        _accountRepositoryMock.Verify(repo => repo.Save(It.Is<Account>(a => a.Balance == 5000)), Times.Once);
    }

    [Fact]
    public void TransactionService_WithdrawMoney_Failed()
    {
        var account = new Account(Guid.Empty, 10, "abcabc", 1000);

        _accountRepositoryMock
            .Setup(repo => repo.GetById(account.Id))
            .Returns(account);

        decimal withdrawAmount = 5000;

        Assert.Throws<InvalidOperationException>(() =>
            _transactionService.CreateTransaction(account.Id, withdrawAmount, TransactionType.Withdrawal));
    }

    [Fact]
    public void TransactionService_DepositMoney2_IncreaseBalance_Success()
    {
        var account = new Account(Guid.Empty, 10, "abcabc", 0);

        _accountRepositoryMock
            .Setup(repo => repo.GetById(account.Id))
            .Returns(account);

        decimal depositAmount = 20000;

        _transactionService.CreateTransaction(account.Id, depositAmount, TransactionType.Deposit);

        _accountRepositoryMock.Verify(repo => repo.Save(It.Is<Account>(a => a.Balance == 20000)), Times.Once);
    }

    [Fact]
    public void TransactionService_WithdrawMoney2_Failed()
    {
        var account = new Account(Guid.Empty, 10, "abcabc", 30000);

        _accountRepositoryMock
            .Setup(repo => repo.GetById(account.Id))
            .Returns(account);

        decimal withdrawAmount = 40000;

        Assert.Throws<InvalidOperationException>(() =>
            _transactionService.CreateTransaction(account.Id, withdrawAmount, TransactionType.Withdrawal));
    }

    [Fact]
    public void TransactionService_DepositMoney_AccountNotFound_Failed()
    {
        var account = new Account(Guid.Empty, 10, "abcabc", 0);

        decimal depositAmount = 1000;

        Assert.Throws<InvalidOperationException>(() => _transactionService.CreateTransaction(account.Id, depositAmount, TransactionType.Deposit));
    }

    [Fact]
    public void TransactionService_Withdraw_AccountNotFound_Failed()
    {
        var account = new Account(Guid.Empty, 10, "abcabc", 0);

        decimal depositAmount = 1000;

        Assert.Throws<InvalidOperationException>(() => _transactionService.CreateTransaction(account.Id, depositAmount, TransactionType.Deposit));
    }
}