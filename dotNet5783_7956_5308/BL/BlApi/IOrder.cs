using BO;
namespace BlApi;

public interface IOrder
{
    //manager funcs 
    public IEnumerable<OrderForList?> ReadAllOrderForList();
    public Order ReadBoOrder(int orderId);
    public Order DeliveredUpdate(int orderId);
    public Order ShipUpdate(int orderId);

    public BO.OrderTrackings GetOrderTracking(int orderId);
}
