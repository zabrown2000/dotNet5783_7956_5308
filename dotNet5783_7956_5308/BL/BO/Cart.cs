
namespace BO;

public class Cart
{
    /// <summary>
    /// Name of customer making cart
    /// </summary>
    public String? CustomerName { get; set; }
    /// <summary>
    /// Email of customer making cart
    /// </summary>
    public String? CustomerEmail { get; set; }
    /// <summary>
    /// Address of customer making cart
    /// </summary>
    public String? CustomerAddress { get; set; }
    /// <summary>
    /// list of order items in the cart
    /// </summary>
    public List<OrderItem?> Items { get; set; }
    /// <summary>
    /// total price of cart
    /// </summary>
    public double TotalPrice { get; set; }
    

    public override String ToString() => this.ToStringProperty(); 
}
