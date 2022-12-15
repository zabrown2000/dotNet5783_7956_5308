using BO;
namespace BlApi;
                    //ADD DOCUMENTATION
                    //cart, order, product
public interface IBl
{
    public IProducts products { get; }
    public IOrder order { get; }
    //public IOrderItem orderItem { get; }
    public ICart cart { get; }
    //public IOrderTracking orderTracking { get; }
    //public IOrderForList orderForList { get; }
    //public IProductForList productForList { get; }
    //public IProductItem productItem { get; }
}
