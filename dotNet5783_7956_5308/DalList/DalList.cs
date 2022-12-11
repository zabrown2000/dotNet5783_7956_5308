using DalApi;
namespace Dal;

sealed public class DalList : IDal
{
    public IProducts dalProduct => new DalProducts();
    public IOrder dalOrder => new DalOrder();
    public IOrderItem dalOrderItem => new DalOrderItem();
}
