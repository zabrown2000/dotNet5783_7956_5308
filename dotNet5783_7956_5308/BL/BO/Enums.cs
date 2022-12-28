namespace BO;
//ADD DOCUMENTATION
public class Enums
{
    public enum OrderStatus { JustOrdered=1, Processing, Shipped, Arrived, Recieved }
    public enum ProdCategory { Mixer = 1, Blender, Oven, Fridge, Freezer, Stove, Kettle }
    public enum BoEntityType { Exit, Cart, Order, Product };
}
