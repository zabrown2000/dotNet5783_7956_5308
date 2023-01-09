using System.Diagnostics.Metrics;

namespace DO;

/// <summary>
/// Structure for a product entity
/// </summary>
public struct Products
{
    public Products()
    {
        Name = "";
        Price = 0;
        Category = 0;
        InStock = 0;
    }

    public static int counter = 100; //3 digit product ids

    /// <summary>
    /// Unique id for a product
    /// </summary>
    public int ID { get; set; } = counter++;
    /// <summary>
    /// Name of product
    /// </summary>
    public String Name { get; set; }
    /// <summary>
    /// Category of product
    /// </summary>
    public Enums.Categories Category { get; set; }
    /// <summary>
    /// Cost of product
    /// </summary>
    public int Price { get; set; }
    /// <summary>
    /// Quantity of product in stock
    /// </summary>
    public int InStock { get; set; }
    


    public override String ToString() => $@"
        Product ID={ID}: {Name},
        Category - {Category}
        Price: {Price}
        Amount in stock: {InStock}
        ";
}
