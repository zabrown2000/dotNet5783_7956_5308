using BO;
namespace BlApi;
                    //REDO DOCUMENTATION
public interface IProducts
{
    //managerial functions
    public IEnumerable<ProductForList?> GetProductsForList();//returns a list of products for the manager
    public Products ManagerProduct(int id);//return a BO product of DO product with id
    public void AddProduct(Products product);//gets a BO product, check if right and add a DO product 
    public void DeleteProduct(int id);//check in every order that DO product is deleted 
    public void UpdateProduct(Products product);//get BO product, check if right and updates DO product

    //customer user functions
    public IEnumerable<ProductItem?> GetCatalog();//get product list of DO and and return productItem list of BO
    //? means allowing to be null   
}
