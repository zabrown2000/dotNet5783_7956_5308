using DO;
namespace DalApi;

public interface IOrderItem : ICrud<OrderItem> //got rid of update fn, a problem?
{
    public void SetByOrderItem(OrderItem orderItem);
    public void SetByOrdProdID(OrderItem orderItem);
    public IEnumerable<OrderItem> OrdersInOrderItem(int orderId);
    public OrderItem GetOrderItem(int orderId, int productId);
}
