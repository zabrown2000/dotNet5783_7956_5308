using BlApi;
using Dal;
using BO;
namespace BlImplementation;

internal class Products : BlApi.IProducts
{
    static DalApi.IDal? dal = DalApi.Factory.Get();
    /// <summary>
    /// Method to get a list of products for the manager
    /// </summary>
    /// <returns>List of ProductForList</returns>
    public IEnumerable<BO.ProductForList?> ReadProductsForList()
    {
        return from DO.Products? prod in dal?.dalProduct.ReadAll()! //getting all DO products and details needed for manager
               where prod != null
               select new BO.ProductForList  //add to ProductForList List
               {
                   ID = prod.Value.ID,
                   Name = prod?.Name!,
                   Price = (double)prod?.Price!,
                   Category = (BO.Enums.ProdCategory)prod?.Category!
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
        BO.Products p = new();
        DO.Products product;
        try
        {
            product = (DO.Products)(dal?.dalProduct.ReadId(id)!); //getting the product from id
        } catch
        {
            throw new BO.BOEntityDoesNotExistException("Product does not exist");
        }
        //add vals from the DOproduct to BOproduct
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
        //validate input
        if (p.Name == "" || p.Price <= 0 || p.InStock < 0 || p.Category < BO.Enums.ProdCategory.Mixer || p.Category > BO.Enums.ProdCategory.Kettle)
        {
            throw new BO.InvalidInputException("Invalid field value");
        }

        DO.Products newProduct = new()
        {
            ID = p.ID,
            Name = p.Name ?? "",
            Price = p.Price,
            InStock = p.InStock,
            Category = (DO.Enums.Categories)p.Category,
        }; //create new DO product

        try
        {
            newProduct.ID = (int)(dal?.dalProduct.Add(newProduct)!);//add to product list
        }
        catch (DO.EntityAlreadyExistsException)
        {
            throw new BO.BOEntityAlreadyExistsException("Product already exists");
        }


        /*try
        {
            DO.Products prod = (DO.Products)dal?.dalProduct.ReadId(p.ID); //get product with id
        } catch
        {   //comes here if product didn't exist
            DO.Products newP = new DO.Products(); //create new DO product
            newP.Name = p.Name;
            newP.Price = p.Price;
            newP.InStock = p.InStock;
            newP.Category = (DO.Enums.Categories)p.Category;

            dal?.dalProduct.Add(newP);//add to product list
            return;
        }
        throw new BO.BOEntityAlreadyExistsException("Product already exists"); //if made it here then DO product already exists   */
    }

    /// <summary>
    /// Method to delete a DO product after checking it doesn't show up in any existing order items
    /// </summary>
    /// <param name="id">id of product to delete</param>
    /// <exception cref="BO.BOEntityDoesNotExistException"></exception>
    public void DeleteProduct(int id)
    {
        //get the prod with matching id to given id
        var v = from ords in dal?.dalOrder.ReadAll()
                where ords != null 
                select from oi in dal?.dalOrderItem.ReadAll()
                       where oi != null && oi?.OrderID == ords?.ID && oi?.ProductID == id
                       select oi;
        if (v.Any() == false) //no matching order items were found containg this product
        {
            throw new BO.BOEntityDoesNotExistException("Product is not in any orders");
        }
        try
        {
            dal?.dalProduct.Delete(id); //delete product (no need to catch since if made it here product was in an orderitem so exists) 
        }
        catch (DO.EntityDoesNotExistException)
        {
            throw new BO.BOEntityDoesNotExistException("Product does not exist");
        }
    }

    /// <summary>
    /// Updating a DO product based on BO product
    /// </summary>
    /// <param name="p">BO product providing update</param>
    /// <exception cref="InvalidInputException"></exception>
    /// <exception cref="BOEntityDoesNotExistException"></exception>
    public void UpdateProduct(BO.Products p)
    {
        //validate input
        if (p.ID < 100 || p.Name == "" || p.Price <= 0 || p.InStock < 0)
        {
            throw new InvalidInputException("Invalid field value");
        }
        DO.Products temp = new DO.Products();
        temp.ID = p.ID; //replacing auto-incremented id
        temp.Name = p.Name ?? "";
        temp.Price = p.Price;
        temp.InStock = p.InStock;
        temp.Category = (DO.Enums.Categories)p.Category;
        try
        {
            dal?.dalProduct.Update(temp); //update product
        }
        catch
        {
            throw new BO.BOEntityDoesNotExistException("Product does not exist");
        }
    }

    /// <summary>
    /// get product list of DO and and return productItem list of BO  
    /// </summary>
    /// <returns>list of product items</returns>
    public IEnumerable<ProductItem> GetCatalog()
    {
        var v = from prods in dal?.dalProduct.ReadAll() //creating new productItems based on existing DO products
                where prods != null
                select new ProductItem()
                {
                    ID = prods?.ID ?? throw new BO.BOEntityDoesNotExistException("Product does not exist"),
                    Name = prods?.Name!,
                    Price = (double)prods?.Price!,
                    Amount = (int)prods?.InStock!,
                    InStock = (prods?.InStock > 0) ? true : false,
                    Category = (BO.Enums.ProdCategory)prods?.Category!
                };
        return v;
    }

    /// <summary>
    /// method to get next product id that will be used 
    /// </summary>
    /// <returns></returns>
    public int GetNextID()
    {
        return DO.Products.counter;
    }
}
