using DalApi;
namespace Dal;

sealed internal class DalList : IDal
{
    //singleton instance
    public static IDal Instance { get; } = new DalList();

    private DalList() { }
    
    public IProducts dalProduct => new DalProducts();
    public IOrder dalOrder => new DalOrder();
    public IOrderItem dalOrderItem => new DalOrderItem();
}
