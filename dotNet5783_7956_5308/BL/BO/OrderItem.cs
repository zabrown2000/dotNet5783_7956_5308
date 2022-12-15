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
    
    /*members inside the class will be defined as properties.
i. automatic properties
ii. public permissions
iii. no initialization in the declaration
    No methods should be added except overloading the ToString() method. Namely,
no constructors, destructors or other methods. The reason is that we want to avoid
applying logic at this time.*/
}
