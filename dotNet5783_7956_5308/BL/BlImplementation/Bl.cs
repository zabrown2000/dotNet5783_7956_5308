using BlApi;
namespace BlImplementation;

sealed internal class Bl : IBl
{
    public IProducts products => new Products();
    public IOrder order => new Order();
    public ICart cart => new Cart();
}
