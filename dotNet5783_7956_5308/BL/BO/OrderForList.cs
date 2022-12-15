

using System.Diagnostics.Metrics;

namespace BO;

public class OrderForList
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
    /// status of order
    /// </summary>
    public Enums.OrderStatus Status { get; set; }
    /// <summary>
    /// amount of items in the order
    /// </summary>
    public int AmountOfItems { get; set; }
    /// <summary>
    /// total price of order
    /// </summary>
    public double TotalPrice { get; set; }

    public override String ToString() => $@"
        ID: {ID}
        .....Customer Name: {CustomerName}
        Status: {Status}
        Amount of Items: {AmountOfItems}
        Total Price: {TotalPrice}
        ";
}
