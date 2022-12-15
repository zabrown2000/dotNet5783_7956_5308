

namespace BO;

public class ProductForList
{
    /// <summary>
    /// Unique id for a product for list
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Name of product for list
    /// </summary>
    public String Name { get; set; }
    /// <summary>
    /// Category of product for list
    /// </summary>
    public Enums.ProdCategory Category { get; set; }
    /// <summary>
    /// Cost of product for list
    /// </summary>
    public double Price { get; set; }

    public override String ToString() => $@"
        ID: {ID}
        Name: {Name}
        Category: {Category}
        Price: {Price}
        ";
}
