namespace TodoList.Core.Base;

public interface IRepository<T>
{
    T Get(string id);
    IEnumerable<T> GetAll();
    void Create(T item);
    void Update(T item);
    void Delete(T item);
}
