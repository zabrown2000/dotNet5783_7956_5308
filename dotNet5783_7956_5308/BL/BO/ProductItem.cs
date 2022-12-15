
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
    public int Price { get; set; }
    /// <summary>
    /// Quantity of product item in stock
    /// </summary>
    public int InStock { get; set; }
    /// <summary>
    /// Amount of product items
    /// </summary>
    public int Amount { get; set; }

    public override String ToString() => $@"
        ID: {ID}
        Name: {Name}
        Category: {Category}
        Price: {Price}
        In Stock: {InStock}
        Amount: {Amount}
        ";


}
