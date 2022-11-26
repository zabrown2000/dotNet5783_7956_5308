using DO;

namespace Dal;

static internal class DataSource
{
    /*-------Class Fields-------*/
    /// <summary>
    /// Readonly static field for generating random numbers
    /// </summary>
    static readonly Random randNumGen = new Random(); //"This field should be initialized in the class’s declaration" - how?

    /// <summary>
    /// Array to hold list of products
    /// </summary>
    static internal Products[] _productList = new Products[50];

    /// <summary>
    /// Array to hold list of orders
    /// </summary>
    static internal Order[] _orderList = new Order[100];

    /// <summary>
    /// Array to hold list of orderItems
    /// </summary>
    static internal OrderItem[] _orderItemList = new OrderItem[200];

    /*○ Add a nested class Config, declared internal.
■ Add internal static fields to index the first available element in each entity
array. These should be initialized with zero values.
■ Add private static fields for the last available running (integer) identifier in
each object where an auto-incremental identifier field exists. These should be
initialized with the next available identifier in each entity array.
■ For every such index field, add only a get()interface that will increment the
field’s value by 1 with each call.*/


    /*--------Class Methods--------*/
    static public DataSource() { s_Initialize(); } 

    /// <summary>
    /// Method to fill up the data entities with initial values
    /// </summary>
    private void s_Initialize()
    {
        PushProducts();
        PushOrders();
        PushOrderItems();
    }

    private void PushProducts()
    {
        /*10 items
         ○ Each product should own a unique 6-digit identifier, chosen at random or manually.
         ○ The products should be grouped into categories.
         ○ Each product will carry a (reasonable) retail price, and a name
         ○ Each product will be stocked according to the following prescription: 5% of the
            products are out of stock (quantity=0). The remainder will be stocked at
            quantities greater than zero*/
    }

    private void PushOrders()
    {
        /*20 items
         ○ Each order should own a serial identifier (utilizing a Config class). Each
            order should also register a customer’s name, physical address and email.
            One customer may own several orders, however, please make sure that the
            system stores more than one customer with orders.
         ○ Dates will pre-date the current time DateTime.Now
         ○ All orders will have an order creation date.
         ○ For 80% of the orders, a shipping date should be registered to a later time
            than the order’s creation time. Otherwise no date.
         ○ A delivery date will be added to 60% of the shipped orders. Otherwise - no date.
        
         ○ Dates:
            ■ All missing values of type DateTime should be initialized with DateTime.MinValue
            ■ Ordered date values - use TimeSpan and add an interval chosen at random. Again, be reasonable with the interval range.
       */
    }

    private void PushOrderItems()
    {
        /*40 items
         ○ Each of these should own a serial identifier (utilizing a Config class).
         ○ Non-null order and product identifiers are mandatory in an OrderItem instance.
         ○ Each order should have between one and four products.
         ○ Product quantities should be random. Please set reasonable ranges.
         ○ The actual price of a product will be retrieved from its retail price.*/
    }

}

/*The DataSource class:
○ Be reasonable with initialized values. The values should be coordinated across per the
requirements listed in the mini-project’s general description.
 ○ Object identifiers
            ■ Implement auto-incrementing identifiers for new objects using a property in the
                internal class Config (see below)
            ■ (non-object) identifier values should be chosen at random as described in the
            project’s general description.
*/


