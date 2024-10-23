namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public interface IRepository<T> where T : IEntity
{
    void Add(T entity);

    void Remove(T? entity);

    T? GetFromId(int id);

    IReadOnlyCollection<T> GetAll();
}