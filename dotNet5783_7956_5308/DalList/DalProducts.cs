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
            if (product.Name == "") //Product's default ctor makes the name empty string, so we want to make sure it's a new product to add to our catalog
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
       
        if (item?.ID == 0) //if not found the item id will be the default 0
        {
            throw new EntityDoesNotExistException(new Products());
        }
        return item.Value;
    }

    /// <summary>
    /// Reading all products in catalog
    /// </summary>
    /// <returns>a list with all the products</returns>
    public IEnumerable<Products?> ReadAll (Func<Products?, bool>? filter = null)
    {
        return DataSource._productList.ToList(); //list of products
    }

    /// <summary>
    /// Delete function for products
    /// </summary>
    /// <param name="id">id of product to delete</param>
    /// <exception cref="Exception">Exception if product doesn't exist</exception>
    public void Delete(int id)
    {
        int index = -1; //flag for checking if product exists
        foreach (Products product in DataSource._productList) //searching for product in list based on ID
        {
            if (product.ID == id) //if found id in the catalog
            {
                index = DataSource._productList.IndexOf(product); //save index of that Product
                break;
            }
                
        }
        if (index != -1) //check to make sure actually deleting item that exists
        {
            Products? toDelete = DataSource._productList[index]; //getting product at index of id want to delete
            DataSource._productList.Remove(toDelete); //removing product from the list
        } else
        {
            throw new EntityDoesNotExistException(new Products());
        }
    }

    /// <summary>
    /// Update function for products
    /// </summary>
    /// <param name="product">product want to update</param>
    /// <exception cref="Exception">Exception if product doesn't exist</exception>
    public void Update(Products product)
    {
        int index = -1; //flag for checking if product exists
        foreach (Products p in DataSource._productList) //go over Product list
        {
            if (product.ID == p.ID) //if found id in the catalog
            { 
                index = DataSource._productList.IndexOf(p); //save index of that Product
                break;
            }
        }
        if (index != -1)
        {
            DataSource._productList[index] = product; //updating item using same place in memory
        } else
        {
            throw new EntityDoesNotExistException(product);
        }
    }
}
