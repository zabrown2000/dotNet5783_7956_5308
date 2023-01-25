using BO;
namespace BlApi;

public interface ICart
{
    //customer functions
    public BO.Cart AddToCart(BO.Cart myCart, int pID); 
    public BO.Cart UpdateCart(BO.Cart myCart, int pID, int newAmount); 
    public void MakeOrder(BO.Cart myCart, string CustomerName, string CustomerEmail, string CustomerAddress);

    public IEnumerable<BO.OrderItem> GetItems(BO.Cart cart);
    public BO.Cart IncreaseCart(BO.Cart cart, int ID);
    public BO.Cart DecreaseCart(BO.Cart cart, int ID);

}
