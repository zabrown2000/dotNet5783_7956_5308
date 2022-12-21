using BO;
namespace BlApi;

public interface IOrder
{
    //manager funcs 
    public IEnumerable<OrderForList?> GetAllOrderForList();
    public Order GetBoOrder(int orderId);
    public Order DeliveredUpdate(int orderId);
    public Order ShipUpdate(int orderId); 
    //public OrderTracking GetOrderTracking(int orderId);//get order id, check if exists, and build strings of dates and status in DO orders

}
