using BlApi;
namespace BlImplementation;

sealed public class Bl : IBl
{
    public IProducts products => new Products();
    public IOrder order => new Order();
    public IOrderItem orderItem => new OrderItem();
    public ICart cart => new Cart();
    public IOrderTracking orderTracking => new OrderTracking();
    public IOrderForList orderForList => new OrderForList();
    public IProductForList productForList => new ProductForList();
    public IProductItem productItem => new ProductItem();
}
