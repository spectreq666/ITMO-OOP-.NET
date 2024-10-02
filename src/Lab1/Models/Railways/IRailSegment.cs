using Itmo.ObjectOrientedProgramming.Lab1.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.Models.DTO;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Railways;

public interface IRailSegment
{
    ProcessTrainDto ProcessTrain(Train train);
}