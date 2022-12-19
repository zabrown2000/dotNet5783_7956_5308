using BlApi;
namespace BlImplementation;

sealed public class Bl : IBl
{
    public IProducts products => new Products();
    public IOrder order => new Order();
    public ICart cart => new Cart();
}
