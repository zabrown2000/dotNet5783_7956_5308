using System.Diagnostics.Metrics;

namespace BO;

public class Order
{
    /// <summary>
    /// Unique id for an order 
    /// </summary>
    public int ID { get; init; }
    /// <summary>
    /// Name of customer making order
    /// </summary>
    public String CustomerName { get; set; }
    /// <summary>
    /// Email of customer making order
    /// </summary>
    public String CustomerEmail { get; set; }
    /// <summary>
    /// Address of customer making order
    /// </summary>
    public String CustomerAddress { get; set; }
    /// <summary>
    /// Date order was made
    /// </summary>
    public DateTime OrderDate { get; set; }
    /// <summary>
    /// Date order was shipped
    /// </summary>
    public DateTime ShipDate { get; set; }
    /// <summary>
    /// Date order was delivered
    /// </summary>
    public DateTime DeliveryDate { get; set; }
    /// <summary>
    /// list of order items that correspond to order
    /// </summary>
    public List<OrderItem> Items { get; set; }
    /// <summary>
    /// total price of order
    /// </summary>
    public double TotalPrice { get; set; }
    /*members inside the class will be defined as properties.
i. automatic properties
ii. public permissions
iii. no initialization in the declaration
    No methods should be added except overloading the ToString() method. Namely,
no constructors, destructors or other methods. The reason is that we want to avoid
applying logic at this time.*/
    public override String ToString() => $@"
       ID: {ID}
       .....Customer Name: {CustomerName}
       .....Customer Email: {CustomerEmail}
       .....Customer Address: {CustomerAddress}
       Order Date: {OrderDate}
       Ship Date: {ShipDate}
       Delivery Date: {DeliveryDate}
       Items: {Items}
       Total Price: {TotalPrice}
       ";
}
