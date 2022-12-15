using System.Diagnostics.Metrics;

namespace BO;
public class Products
{
    /// <summary>
    /// Unique id for a product
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Name of product
    /// </summary>
    public String Name { get; set; }
    /// <summary>
    /// Category of product
    /// </summary>
    public Enums.ProdCategory Category { get; set; }
    /// <summary>
    /// Cost of product
    /// </summary>
    public int Price { get; set; }
    /// <summary>
    /// Quantity of product in stock
    /// </summary>
    public int InStock { get; set; }

    public override string ToString() => $@"
        ID: {ID}
        Name: {Name}
        Category: {Category}
        Price: {Price}
        In Stock: {InStock}
        ";
    /*members inside the class will be defined as properties.
 i. automatic properties
 ii. public permissions
 iii. no initialization in the declaration
    No methods should be added except overloading the ToString() method. Namely,
no constructors, destructors or other methods. The reason is that we want to avoid
applying logic at this time.*/
}
