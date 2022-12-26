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
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// Status of the order
    /// </summary>
    public Enums.OrderStatus Status { get; set; }
    /// <summary>
    /// Date order was shipped
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// Date order was delivered
    /// </summary>
    public DateTime? DeliveryDate { get; set; }
    /// <summary>
    /// list of order items that correspond to order
    /// </summary>
    public List<OrderItem> Items { get; set; }
    /// <summary>
    /// total price of order
    /// </summary>
    public double TotalPrice { get; set; }
    public override String ToString() => this.ToStringProperty();
}
