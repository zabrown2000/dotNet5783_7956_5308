using BO;
namespace BlApi;
public interface IProducts
{
    //managerial functions
    public IEnumerable<ProductForList?> ReadProductsForList(); 
    public Products ManagerProduct(int id);
    public void AddProduct(Products product);
    public void DeleteProduct(int id);
    public void UpdateProduct(Products product);

    //customer user functions
    public IEnumerable<ProductItem?> GetCatalog();
}
