using Itmo.ObjectOrientedProgramming.Lab3.Entities;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Displays;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Messengers;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Recipients;
using Itmo.ObjectOrientedProgramming.Lab3.Factories;
using Itmo.ObjectOrientedProgramming.Lab3.Models;
using Moq;
using Xunit;

namespace Lab3.Tests;

public class MessengerRecipientTests
{
    [Fact]
    public void MarkMessageAsRead_ShouldChangeMessageStatus_Success()
    {
        var loggerMock = new Mock<ILogger>();
        var user = new User("TestUser");
        var userFactory = new UserRecipientFactory(user, loggerMock.Object);
        var userRecipient = (UserRecipient)userFactory.CreateRecipient();

        var message = new Message("Test Title", "Test Body", ImportanceLevel.Medium);

        var filterProxy = new MessageFilter(userRecipient);
        filterProxy.SetImportanceFilter(ImportanceLevel.Medium);

        filterProxy.ReceiveMessage(message);

        userRecipient.MarkMessageAsRead(message);

        Assert.True(userRecipient.IsMessageRead(message));
    }

    [Fact]
    public void MarkMessageAsRead_ShouldChangeMessageStatusToRead_Failed()
    {
        var loggerMock = new Mock<ILogger>();
        var user = new User("TestUser");
        var userFactory = new UserRecipientFactory(user, loggerMock.Object);
        var userRecipient = (UserRecipient)userFactory.CreateRecipient();

        var message = new Message("Test Title", "Test Body", ImportanceLevel.Medium);

        var filterProxy = new MessageFilter(userRecipient);
        filterProxy.SetImportanceFilter(ImportanceLevel.Medium);

        filterProxy.ReceiveMessage(message);

        Assert.False(userRecipient.IsMessageRead(message));
    }

    [Fact]
    public void MarkMessageAsRead_ShouldThrowException_Success()
    {
        var loggerMock = new Mock<ILogger>();
        var user = new User("TestUser");
        var userFactory = new UserRecipientFactory(user, loggerMock.Object);
        var userRecipient = (UserRecipient)userFactory.CreateRecipient();

        var message = new Message("Test Title", "Test Body", ImportanceLevel.Medium);

        var filterProxy = new MessageFilter(userRecipient);
        filterProxy.SetImportanceFilter(ImportanceLevel.Medium);

        filterProxy.ReceiveMessage(message);
        userRecipient.MarkMessageAsRead(message);

        Assert.Throws<InvalidOperationException>(() => userRecipient.MarkMessageAsRead(message));
    }

    [Fact]
    public void ReceiveMessageWithSetImportance_Success()
    {
        var driverMock = new Mock<IDisplayDriver>();
        var display = new Display("Test Display", driverMock.Object);
        var displayFactory = new DisplayRecipientFactory(display);

        IRecipient displayRecipient = displayFactory.CreateRecipient();

        var messageLowImportance = new Message("Low Importance", "This is a low importance message.", ImportanceLevel.Low);
        var messageHighImportance = new Message("High Importance", "This is a high importance message.", ImportanceLevel.High);

        var importanceFilterProxy = new MessageFilter(displayRecipient);
        importanceFilterProxy.SetImportanceFilter(ImportanceLevel.High);

        importanceFilterProxy.ReceiveMessage(messageLowImportance);
        importanceFilterProxy.ReceiveMessage(messageHighImportance);

        driverMock.Verify(driver => driver.Clear(), Times.Once);
        driverMock.Verify(driver => driver.WriteText(It.Is<string>(text => text.Contains("High Importance"))), Times.Once);
        driverMock.Verify(driver => driver.WriteText(It.Is<string>(text => text.Contains("Low Importance"))), Times.Never);
    }

    [Fact]
    public void User_ShouldLogMessage_WhenReceived_Success()
    {
        var loggerMock = new Mock<ILogger>();
        var user = new User("Tom");
        var userRecipientFactory = new UserRecipientFactory(user, loggerMock.Object);
        IRecipient userRecipient = userRecipientFactory.CreateRecipient();

        var message = new Message("Test Message", "This is a test message.", ImportanceLevel.Medium);
        var filter = new MessageFilter(userRecipient);
        filter.SetImportanceFilter(ImportanceLevel.Medium);

        filter.ReceiveMessage(message);

        loggerMock.Verify(logger => logger.Log(It.Is<string>(msg => msg.Contains($"{user.Name} получил сообщение: {message.Title}"))), Times.Once);
    }

    [Fact]
    public void Messenger_ShouldLogMessage_WhenSendMessageCalled_Success()
    {
        var loggerMock = new Mock<ILogger>();
        var messenger = new Messenger("Test Messenger");
        var messengerFactory = new MessengerRecipientFactory(messenger, loggerMock.Object);
        IRecipient messengerRecipient = messengerFactory.CreateRecipient();

        var message = new Message("Test Message", "This is a test message.", ImportanceLevel.Medium);

        var filter = new MessageFilter(messengerRecipient);
        filter.SetImportanceFilter(ImportanceLevel.Medium);

        filter.ReceiveMessage(message);

        loggerMock.Verify(logger => logger.Log(It.Is<string>(msg => msg.Contains($"[Мессенджер {messenger.Name}] {message.Title}: {message.Body}"))), Times.Once);
    }

    [Fact]
    public void TwoUsersReceiveMessage_WithFilter_Success()
    {
        var loggerMock = new Mock<ILogger>();
        var user1 = new User("Ben");
        var user2 = new User("Tom");

        var userFactory1 = new UserRecipientFactory(user1, loggerMock.Object);
        var userFactory2 = new UserRecipientFactory(user2, loggerMock.Object);

        IRecipient userRecipient1 = userFactory1.CreateRecipient();
        IRecipient userRecipient2 = userFactory2.CreateRecipient();

        var filterProxy1 = new MessageFilter(userRecipient1);
        filterProxy1.SetImportanceFilter(ImportanceLevel.Medium);

        var filterProxy2 = new MessageFilter(userRecipient2);
        filterProxy2.SetImportanceFilter(ImportanceLevel.None);

        var messageLowImportance = new Message("Low Importance", "This is a low importance message.", ImportanceLevel.Low);
        var messageHighImportance = new Message("High Importance", "This is a high importance message.", ImportanceLevel.High);

        filterProxy1.ReceiveMessage(messageLowImportance);
        filterProxy1.ReceiveMessage(messageHighImportance);

        filterProxy2.ReceiveMessage(messageLowImportance);
        filterProxy2.ReceiveMessage(messageHighImportance);

        loggerMock.Verify(logger => logger.Log(It.Is<string>(text => text.Contains($"Ben получил сообщение: {messageLowImportance.Title}"))), Times.Never);
        loggerMock.Verify(logger => logger.Log(It.Is<string>(text => text.Contains($"Tom получил сообщение: {messageHighImportance.Title}"))), Times.Once);

        loggerMock.Verify(logger => logger.Log(It.Is<string>(text => text.Contains($"Tom получил сообщение: {messageLowImportance.Title}"))), Times.Once);
        loggerMock.Verify(logger => logger.Log(It.Is<string>(text => text.Contains($"Tom получил сообщение: {messageHighImportance.Title}"))), Times.Once);

        loggerMock.Verify(logger => logger.Log(It.Is<string>(text => text.Contains($"Ben получил сообщение: {messageLowImportance.Title}"))), Times.Never);
    }
}
