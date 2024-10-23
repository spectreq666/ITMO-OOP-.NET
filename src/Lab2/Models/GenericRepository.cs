namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class GenericRepository<T> : IRepository<T> where T : IEntity
{
    private readonly List<T> _entities = new List<T>();

    public void Add(T entity)
    {
        _entities.Add(entity);
    }

    public void Remove(T? entity)
    {
        if (entity is not null)
        {
            _entities.Remove(entity);
        }
    }

    public T GetFromId(int id)
    {
        T? entity = _entities.FirstOrDefault(e => e.Id == id);
        if (entity == null)
        {
            throw new InvalidOperationException($"Entity with ID {id} does not exist.");
        }

        return entity;
    }

    public IReadOnlyCollection<T> GetAll()
    {
        if (_entities.Count == 0)
        {
            throw new InvalidOperationException("The repository is empty.");
        }

        return _entities.AsReadOnly();
    }
}