using Itmo.ObjectOrientedProgramming.Lab3.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities;

public interface IRecipient
{
    void ReceiveMessage(Message message);

    void SetImportanceFilter(ImportanceLevel importanceLevel);
}