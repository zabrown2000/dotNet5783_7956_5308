namespace DalApi;
using DO;

public interface IDal
{
    IProducts dalProduct { get; }
    IOrder dalOrder { get; }
    IOrderItem dalOrderItem { get; }
}


