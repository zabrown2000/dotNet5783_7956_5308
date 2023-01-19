namespace Dal;
using DO;
using System;
using DalApi;



internal class DalProducts : IProducts                
{
    /// <summary>
    /// Create function for products
    /// </summary>
    /// <param name="product">product to add to catalog</param>
    /// <returns>id of new product</returns>
    /// <exception cref="Exception">Exception if product exists or catalog is full</exception>
    public int Add(Products product)
    {
        if (DataSource._productList.Count() < 50) //checking that the product cap hasn't been reached
        {
            //Case 1: New product, need to initialize it and add it
            if (product.Name == null) //Product's default ctor makes the name empty string, so we want to make sure it's a new product to add to our catalog
            {
                //product.ID = DataSource.Config.NextProductNumber; //giving this new product a unique id
                DataSource._productList.Add(product); //add product to the Product list
                return product.ID; //Added the product, returning id
            }
            //Case 2: Item already exists, throw exception
            int index = DataSource._productList.FindIndex(x => x?.ID == product.ID); //getting the index of the product in the list (if it exists)
            if (index != -1) //item was found, so it exists and can't add again
            {
                throw new EntityAlreadyExistsException(product);
            }
            else
            {
                DataSource._productList.Add(product); //initialized product but not in catalog yet so adding it
                return product.ID;
            }
        } else
        {
            throw new EntityListIsFullException(product);
        }

        
    }

    /// <summary>
    /// Reading a particular product
    /// </summary>
    /// <param name="id">id of product to get</param>
    /// <returns>product that want to read</returns>
    /// <exception cref="Exception">Exception if product doesn't exist</exception>
    public Products ReadId(int id)
    {
        Products? item = DataSource._productList.Find(x => x?.ID == id); //checking to see if product exists
       
        if (item?.ID != id) //if not found the item id will be the default 0
        {
            throw new EntityDoesNotExistException(new Products());
        }
        return item ?? throw new EntityDoesNotExistException(new Products()); ;
    }

    /// <summary>
    /// Reading all products in catalog
    /// </summary>
    /// <returns>a list with all the products</returns>
    public IEnumerable<Products?> ReadAll (Func<Products?, bool>? filter = null)
    {
        if (filter == null)
        {
            return DataSource._productList.ToList(); //list of products
        }
        return from v in DataSource._productList//select with filter
               where filter(v)
               select v;
    }

    /// <summary>
    /// Delete function for products
    /// </summary>
    /// <param name="id">id of product to delete</param>
    /// <exception cref="Exception">Exception if product doesn't exist</exception>
    public void Delete(int id)
    {
        int index = DataSource._productList.FindIndex(x => x?.ID == id);

        if (index == -1) //if does not exist
            throw new EntityDoesNotExistException(new Products());
        DataSource._productList.RemoveAt(index);//remove from list
    }

    /// <summary>
    /// Update function for products
    /// </summary>
    /// <param name="product">product want to update</param>
    /// <exception cref="Exception">Exception if product doesn't exist</exception>
    public void Update(Products product)
    {
        int index = DataSource._productList.FindIndex(x => x?.ID == product.ID);

        if (index == -1) //if does not exist
            throw new EntityDoesNotExistException(new Products());
        DataSource._productList[index] = product; //updating item using same place in memory
    }

    /// <summary>
    /// method to get a product by using a filter
    /// </summary>
    /// <param name="filter">filter to search</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="Exception"></exception>
    public Products ReadByFilter(Func<Products?, bool>? filter)
    {
        if (filter == null)
        {
            throw new ArgumentNullException(nameof(filter));//filter is null
        }
        
        Products? p = DataSource._productList.FirstOrDefault(x => x != null && filter(x));
        
        if (p != null)
        {
            return (Products)p;
        }
        throw new Exception("The product requested does not exist\n");
    }
}
