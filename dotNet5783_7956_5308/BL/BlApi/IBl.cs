using BO;
namespace BlApi;
                    //ADD DOCUMENTATION
                    //cart, order, product
public interface IBl
{
    /// <summary>
    /// field to hold the products interface
    /// </summary>
    public IProducts products { get; }
    /// <summary>
    /// field to hold the order interface
    /// </summary>
    public IOrder order { get; }
    /// <summary>
    /// field to hold the cart interface
    /// </summary>
    public ICart cart { get; }

}
