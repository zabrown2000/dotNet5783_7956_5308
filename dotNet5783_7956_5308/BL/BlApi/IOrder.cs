using BO;
namespace BlApi;

public interface IOrder
{
    //manager funcs 
    public IEnumerable<OrderForList?> GetAllOrderForList();//calls get of DO order list, gets items for each order, and build BO orderItem list
    public Order GetBoOrder(int id);//get order id, check if right, and return BO order using DO order, orderItem, and product
    public Order ShipUpdate(int orderId);//get order number, check if exists, update date in DO order, and return BO order that has been "shipped"
    public Order DeliveredUpdate(int orderId);//get order number, check if exists, update date in DO order, and return BO order that has been "delivered" 
    //public OrderTracking GetOrderTracking(int orderId);//get order id, check if exists, and build strings of dates and status in DO orders

}
