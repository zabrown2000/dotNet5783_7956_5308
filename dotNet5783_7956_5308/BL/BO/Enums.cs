namespace BO;
public class Enums
{
    //different order status options for our orders
    public enum OrderStatus { JustOrdered=1, Processing, Shipped, Arrived, Recieved }

    //types of products in our store
    public enum ProdCategory { Mixer = 1, Blender, Oven, Fridge, Freezer, Stove, Kettle, None }

    //options for the user to select from the main screen
    public enum BoEntityType { Exit, Cart, Order, Product };
}
