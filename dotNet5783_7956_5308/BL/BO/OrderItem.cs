using System.Diagnostics.Metrics;

namespace BO;

public class OrderItem
{
    /// <summary>
    /// unique id of the order item
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Unique id of product in relation
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// Price of the order item
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Amount of products in order item
    /// </summary>
    public int Amount { get; set; }

    public override string ToString() => $@"
        ID: {ID}
        Product ID: {ProductID}
        Price: {Price}
        Amount: {Amount}
        ";
    
}
