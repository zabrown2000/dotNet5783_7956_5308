using System.Diagnostics;

namespace DO;

/// <summary>
/// Structure for an order entity
/// </summary>
public struct Order
{
    /// <summary>
    /// Unique id for an order 
    /// </summary>
    public int ID { get; set; }
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
    public override String ToString() => $@"
        Order ID={ID}: 
        Customer Details:   
                Name={CustomerName}    
                Email={CustomerEmail}    
                Address={CustomerAddress}     
        Date ordered: {OrderDate}
        Date shipped: {ShipDate}
        Date Delivered: {DeliveryDate}
        ";
}
