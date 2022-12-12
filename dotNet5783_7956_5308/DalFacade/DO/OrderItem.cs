
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace DO;

/// <summary>
/// Structure for the order-item relation between Orders and Products
/// </summary>
public struct OrderItem
{
    public OrderItem()
    {
        OrderID = 0;
        ProductID = 0;
        Amount = 0;
        Price = 0;
    }
    
    static int counter = 1000; //4 digit order-item ids

    /// <summary>
    /// Unique id for an order-item relation
    /// </summary>
    public int ID { get; init; } = counter++;
    /// <summary>
    /// Unique id of product in relation
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// Unique id of order in relation
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// Price of the order item
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Amount of products in order item
    /// </summary>
    public int Amount { get; set; }
    public override String ToString() => $@"
        OrderItem ID={ID}: 
        Product ID={ProductID}
        Order ID={OrderID}
        Price: {Price}
        Quantity: {Amount}
        ";

}
