namespace DalApi;

public interface ICrud<T> where T : struct
{
    int Add(T item);
    T ReadId(int id);
    void Update(T item);
    void Delete(int id);

    IEnumerable<T> ReadAll();
}
