using BlApi;
using DalApi;
using Dal;
using BO;
                        //ADD DOCUMENTATION
namespace BlImplementation;

internal class Products : BlApi.IProducts
{
    static IDal? dal = new DalList();
    /// <summary>
    /// Method to get a list of products for the manager
    /// </summary>
    /// <returns>List of ProductForList</returns>
    public IEnumerable<BO.ProductForList?> GetProductsForList()
    {
        return from DO.Products? prod in dal.dalProduct.ReadAll() //getting all DO products and taking details need for manager
               where prod != null 
               select new BO.ProductForList
               {
                   ID = prod.Value.ID,
                   Name = prod?.Name,
                   Price = (double)prod?.Price,
                   Category = (BO.Enums.ProdCategory)item?.Category
               };

    }

    /// <summary>
    /// Method to get BO product from DO product 
    /// </summary>
    /// <param name="id">id of DO product</param>
    /// <returns>new BO product</returns>
    /// <exception cref="BO.BOEntityDoesNotExistException"></exception>
    public BO.Products ManagerProduct(int id)
    {
        BO.Products p = new BO.Products();
        DO.Products product = new DO.Products();
        try
        {
            product = dal.dalProduct.ReadId(id); //getting the product from id
        } catch
        {
            throw new BO.BOEntityDoesNotExistException();
        }

        p.ID = id;
        p.Name = product.Name;
        p.Price = product.Price;
        p.Category = (BO.Enums.ProdCategory)product.Category;
        p.InStock = product.InStock;

        return p;
    }

    /// <summary>
    /// Method to create and add a DO product from a BO product 
    /// </summary>
    /// <param name="p">BO product</param>
    /// <exception cref="BO.InvalidInputException"></exception>
    /// <exception cref="BO.BOEntityAlreadyExistsException"></exception>
    public void AddProduct(BO.Products p)
    {
        if (p.Name == "" || p.Price <= 0 || p.InStock < 0 || p.Category < BO.Enums.ProdCategory.Mixer || p.Category > BO.Enums.ProdCategory.Kettle)
        {
            throw new BO.InvalidInputException("Invalid field value");
        }
        try
        {
            DO.Products prod = dal.dalProduct.ReadId(p.ID); //get product with id
        } catch
        {   //comes here is product didn't exist
            DO.Products newP = new DO.Products(); //create new DO product
            newP.Name = p.Name;
            newP.Price = p.Price;
            newP.InStock = p.InStock;
            newP.Category = (DO.Enums.Categories)p.Category;

            dal.dalProduct.Add(newP);//add to product list
            return;
        }
        throw new BO.BOEntityAlreadyExistsException(); //made it here then DO product already exists     
    }

    /// <summary>
    /// Method to delete a DO product after checking it doesn't show up in any existing order items
    /// </summary>
    /// <param name="id">id of product to delete</param>
    /// <exception cref="BO.BOEntityDoesNotExistException"></exception>
    public void DeleteProduct(int id)
    {
        var v = from ords in dal.dalOrder.ReadAll()
                where ords != null 
                select from oi in dal.dalOrderItem.ReadAll()
                       where oi != null && oi?.OrderID == ords?.ID && oi?.ProductID == id
                       select oi;
        if (v.Any() == false) //no matching order items were found containg this product
        {
            throw new BO.BOEntityDoesNotExistException("Product is not in any orders");
        }
        dal.dalProduct.Delete(id); //delete product (no need to catch since if made it here product was in an orderitem so exists) 
    }

    /// <summary>
    /// Updating a DO product based on BO product
    /// </summary>
    /// <param name="p">BO product providing update</param>
    /// <exception cref="InvalidInputException"></exception>
    public void UpdateProduct(BO.Products p)
    {
        if (p.ID < 100 || p.Name == "" || p.Price <= 0 || p.InStock < 0)
        {
            throw new InvalidInputException("Invalid field value");
        }
        DO.Products temp = new DO.Products();
        temp.ID = p.ID; //replacing auto-incremented id
        temp.Name = p.Name;
        temp.Price = p.Price;
        temp.InStock = p.InStock;
        temp.Category = (DO.Enums.Categories)p.Category;
        dal.dalProduct.Update(temp);
    }

    /// <summary>
    /// get product list of DO and and return productItem list of BO  
    /// </summary>
    /// <returns>list of product items</returns>
    public IEnumerable<ProductItem?> GetCatalog()
    {
        var v = from prods in dal.dalProduct.ReadAll() //creating new productItems based on existing DO products
                where prods != null
                select new ProductItem()
                {
                    ID = prods.Value.ID,
                    Name = prods?.Name,
                    Price = (double)prods?.Price,
                    Amount = (int)prods?.InStock,
                    InStock = (prods?.InStock > 0) ? true : false,
                    Category = (BO.Enums.ProdCategory)prods?.Category
                };
        return v;
    }
}
