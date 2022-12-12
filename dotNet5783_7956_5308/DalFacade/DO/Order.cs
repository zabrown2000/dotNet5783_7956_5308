using System.Diagnostics;
using System.Xml.Linq;

namespace DO;

/// <summary>
/// Structure for an order entity
/// </summary>
public struct Order
{
    public Order()
    {
        CustomerName = "";
        CustomerEmail = "";
        CustomerAddress = "";
        OrderDate = DateTime.MinValue;
        ShipDate = DateTime.MinValue;
        DeliveryDate = DateTime.MinValue;
    }
    static int counter = 10; //2 digit order ids

    /// <summary>
    /// Unique id for an order 
    /// </summary>
    public int ID { get; init; } = counter++;
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
        ........Name={CustomerName}    
        ........Email={CustomerEmail}    
        ........Address={CustomerAddress}     
        Date ordered: {OrderDate}
        Date shipped: {ShipDate}
        Date Delivered: {DeliveryDate}
        ";
}
