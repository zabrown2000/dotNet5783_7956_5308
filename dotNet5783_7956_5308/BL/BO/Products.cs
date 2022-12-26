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

    public override String ToString() => this.ToStringProperty();
}
