
namespace BO;

public class ProductItem
{
    /// <summary>
    /// Unique id for a product item
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Name of product item
    /// </summary>
    public String Name { get; set; }
    /// <summary>
    /// Category of product item
    /// </summary>
    public Enums.ProdCategory Category { get; set; }
    /// <summary>
    /// Cost of product item
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Flag if given product item is in stock
    /// </summary>
    public bool InStock { get; set; }
    /// <summary>
    /// Amount of product items
    /// </summary>
    public int Amount { get; set; }

    public override String ToString() => this.ToStringProperty();


}
