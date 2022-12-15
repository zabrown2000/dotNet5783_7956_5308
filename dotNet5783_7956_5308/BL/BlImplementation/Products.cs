using BlApi;
using DalApi;
using Dal;
using BO;
                        //ADD DOCUMENTATION
namespace BlImplementation;

internal class Products : BlApi.IProducts
{
    static IDal? dal = new DalList();
    public IEnumerable<BO.ProductForList?> GetProductsForList()
    {
        return from DO.Products? item in dal.dalProduct.ReadAll()
               where item != null 
               select new BO.ProductForList
               {
                   ID = item.Value.ID,
                   Name = item?.Name,
                   Price = (double)item?.Price,
                   Category = (BO.Enums.ProdCategory)item?.Category
               };

    }//returns a list of products for the manager


    public BO.Products ManagerProduct(int id)
    {
        BO.Products p = new BO.Products();//create a BO product
        DO.Products product = new DO.Products();//create a DO product
        try
        {
            product = dal.dalProduct.ReadId(id);//get the matching product for the ID
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
    }//return a BO product of DO product with id
    
    public void AddProduct(BO.Products p)
    {
        if (p.Name == "" || p.Price <= 0 || p.InStock < 0 || p.Category < BO.Enums.ProdCategory.Mixer || p.Category > BO.Enums.ProdCategory.Kettle)
        {
            throw new BO.InvalidInputException("Invalid field value");
        }
        //DO.Product prod = DOList.Product.GetById(p.ID);//get product with id
        //if (prod.ID == p.ID)//already exists 
        //    throw new BO.IdExistException();

        DO.Products newP = new DO.Products(); //create new DO product
        //newProduct.ID = 0;
        newP.Name = p.Name;
        newP.Price = p.Price;
        newP.InStock = p.InStock;
        newP.Category = (DO.Enums.Categories)p.Category;

        dal.dalProduct.Add(newP);//add to product list
    }//gets a BO product, check if right and add a DO product 
    
    public void DeleteProduct(int id)
    {
        var v = from ords in dal.dalOrder.ReadAll()
                where ords != null 
                select from oi in dal.dalOrderItem.ReadAll()
                       where oi != null && oi?.OrderID == ords?.ID && oi?.ProductID == id
                       select oi;
        if (v.Any() == false)//no matching order items were found
        {
            throw new BO.BOEntityDoesNotExistException();
            //throw new BO.UnfoundException();//id not found
        }
        dal.dalProduct.Delete(id);//delete product
        //DOList.Product.Delete(id);//remove orderItem
    }
    public void UpdateProduct(BO.Products p)
    {
        if (p.ID < 100 || p.Name == "" || p.Price <= 0 || p.InStock < 0)
        {
            throw new InvalidInputException("Invalid field value");
        }
        DO.Products tempP = new DO.Products();
        tempP.ID = p.ID;
        tempP.Name = p.Name;
        tempP.Price = p.Price;
        tempP.InStock = p.InStock;
        tempP.Category = (DO.Enums.Categories)p.Category;
        dal.dalProduct.Update(tempP);
    }//get BO product, check if right and updates DO product


    public IEnumerable<ProductItem?> GetCatalog()
    {
        var v = from prods in dal.dalProduct.ReadAll()
                where prods != null 
                select new ProductItem()
                {
                    ID = prods.Value.ID,
                    Name = prods?.Name,
                    Price = (double)prods?.Price,
                    Amount = (int)prods?.InStock,
                    Category = (BO.Enums.ProdCategory)prods?.Category
                };
        foreach (ProductItem item in v)
        {
            if (item.Amount > 0)
                item.InStock = true;
            item.InStock = false;
        }
        return v;
    }//go over DO products and build BO product item list 
}
