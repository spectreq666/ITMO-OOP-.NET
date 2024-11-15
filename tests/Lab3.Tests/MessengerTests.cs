using Itmo.ObjectOrientedProgramming.Lab3.Entities;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Displays;
using Itmo.ObjectOrientedProgramming.Lab3.Factories;
using Itmo.ObjectOrientedProgramming.Lab3.Models;
using Moq;
using Xunit;

namespace Lab3.Tests;

public class MessengerTests
{
    [Fact]
    public void MarkMessageAsRead_ShouldChangeMessageStatus_Success()
    {
        var loggerMock = new Mock<ILogger>();
        var userFactory = new UserFactory("TestUser", loggerMock.Object);
        var user = (User)userFactory.CreateRecipient();

        var message = new Message("Test Title", "Test Body", ImportanceLevel.Medium);

        user.ReceiveMessage(message);

        user.MarkMessageAsRead(message);

        Assert.True(user.IsMessageRead(message));
    }

    [Fact]
    public void MarkMessageAsRead_ShouldChangeMessageStatusToRead_Failed()
    {
        var loggerMock = new Mock<Logger>();
        var userFactory = new UserFactory("TestUser", loggerMock.Object);
        var user = (User)userFactory.CreateRecipient();
        var message = new Message("Test Title", "Test Body", ImportanceLevel.Medium);
        user.ReceiveMessage(message);

        Assert.False(user.IsMessageRead(message));
    }

    [Fact]
    public void MarkMessageAsRead_ShouldThrowException_Success()
    {
        var loggerMock = new Mock<ILogger>();
        var userFactory = new UserFactory("TestUser", loggerMock.Object);
        var user = (User)userFactory.CreateRecipient();

        var message = new Message("Test Title", "Test Body", ImportanceLevel.Medium);

        user.ReceiveMessage(message);
        user.MarkMessageAsRead(message);

        Assert.Throws<InvalidOperationException>(() => user.MarkMessageAsRead(message));
    }

    [Fact]
    public void ReceiveMessageWithSetImportance_Success()
    {
        var driverMock = new Mock<IDisplayDriver>();
        var displayFactory = new DisplayFactory(driverMock.Object);

        IRecipient display = displayFactory.CreateRecipient();

        var messageLowImportance = new Message("Low Importance", "This is a low importance message.", ImportanceLevel.Low);
        var messageHighImportance = new Message("High Importance", "This is a high importance message.", ImportanceLevel.High);

        display.SetImportanceFilter(ImportanceLevel.High);

        display.ReceiveMessage(messageLowImportance);
        display.ReceiveMessage(messageHighImportance);

        driverMock.Verify(driver => driver.Clear(), Times.Once);
        driverMock.Verify(driver => driver.WriteText(It.Is<string>(text => text.Contains("High Importance"))), Times.Once);
        driverMock.Verify(driver => driver.WriteText(It.Is<string>(text => text.Contains("Low Importance"))), Times.Never);
    }

    [Fact]
    public void User_ShouldLogMessage_WhenReceived_Success()
    {
        var loggerMock = new Mock<ILogger>();
        var userFactory = new UserFactory("Tom", loggerMock.Object);
        IRecipient user = userFactory.CreateRecipient();

        var message = new Message("Test Message", "This is a test message.", ImportanceLevel.Medium);

        user.ReceiveMessage(message);

        loggerMock.Verify(logger => logger.Log(It.Is<string>(msg => msg.Contains($"получил сообщение: {message.Title}"))), Times.Once);
    }

    [Fact]
    public void Messenger_ShouldLogMessage_WhenSendMessageCalled_Success()
    {
        var loggerMock = new Mock<ILogger>();
        var messengerFactory = new MessengerFactory(loggerMock.Object);
        IRecipient messenger = messengerFactory.CreateRecipient();

        var message = new Message("Test Message", "This is a test message.", ImportanceLevel.Medium);

        messenger.ReceiveMessage(message);

        loggerMock.Verify(logger => logger.Log(It.Is<string>(msg => msg.Contains($"[Мессенджер] {message.Title}: {message.Body}"))), Times.Once);
    }

    [Fact]
    public void TwoUsersReceiveMessage_WithFilter_Success()
    {
        var loggerMock = new Mock<ILogger>();
        var userFactory = new UserFactory("Ben", loggerMock.Object);
        var userFactory2 = new UserFactory("Tom", loggerMock.Object);

        IRecipient user1 = userFactory.CreateRecipient();
        IRecipient user2 = userFactory2.CreateRecipient();

        user1.SetImportanceFilter(ImportanceLevel.Medium);

        var messageLowImportance = new Message("Low Importance", "This is a low importance message.", ImportanceLevel.Low);
        var messageHighImportance = new Message("High Importance", "This is a high importance message.", ImportanceLevel.High);

        user1.ReceiveMessage(messageLowImportance);
        user1.ReceiveMessage(messageHighImportance);

        user2.ReceiveMessage(messageLowImportance);
        user2.ReceiveMessage(messageHighImportance);

        loggerMock.Verify(logger => logger.Log(It.Is<string>(text => text.Contains($"Ben получил сообщение: {messageLowImportance.Title}"))), Times.Never);
        loggerMock.Verify(logger => logger.Log(It.Is<string>(text => text.Contains($"Tom получил сообщение: {messageHighImportance.Title}"))), Times.Once);

        loggerMock.Verify(logger => logger.Log(It.Is<string>(text => text.Contains($"Tom получил сообщение: {messageLowImportance.Title}"))), Times.Once);
        loggerMock.Verify(logger => logger.Log(It.Is<string>(text => text.Contains($"Tom получил сообщение: {messageHighImportance.Title}"))), Times.Once);

        loggerMock.Verify(logger => logger.Log(It.Is<string>(text => text.Contains($"Ben получил сообщение: {messageLowImportance.Title}"))), Times.Never);
    }
}