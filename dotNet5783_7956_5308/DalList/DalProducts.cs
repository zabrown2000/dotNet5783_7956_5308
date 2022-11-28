namespace Dal;
using DO;
using System;

//ADD COMMENTS


public class DalProducts
{
    public int Add(Products product)//add Product to a list and return its id
    {
        //Case 1: New product, need to initialize it and add it

        if (product.ID == 0) //Product's default ctor makes the id 0, so we want to make sure it's a new product to add to our catalog
        {
            product.ID = DataSource.Config.NextProductNumber; //giving this new product a unique id
            //come back later to see if need to initialize rest of fields
            DataSource._productList.Add(product);//add product to the Product list
            return product.ID; //Added the product, returning id
        }
        //Case 2: Item already exists, throw exception
        int index = DataSource._productList.FindIndex(x => x.ID == product.ID); //getting the index of the product in the list (if it exists)
        if (index != -1) //item was found, so it exists and can't add again
        {
            throw new Exception("Product already exists\n"); //error
        } else
        {
            DataSource._productList.Add(product); //initialized product but not in catalog yet so adding it
            return product.ID;
        }
    }

    /*○ Implement a read method for a single object: input - an entity’s identifier (not an array
        index!), output - an object.
    ○ Implement a read method for a list of objects stored in the entity array. No input
        arguments.
*/
    public Products ReadId(int id)
    {
        Products item = DataSource._productList.Find(x => x.ID == id); //checking to see if product exists
        if (item.ID != id) //if not found the item id will be the default, 0, and not match the given id
        {
            throw new Exception("Product does not exist\n");
        }
        return item;
    }

    public List<Products> ReadAll ()
    {
        return DataSource._productList.ToList();
    }

    public void Delete(int id)
    {
        int index = 0;
        foreach (Products product in DataSource._productList) //searching for product in list based on ID
        {
            if (product.ID == id) //if found id in the catalog
            {
                index = DataSource._productList.IndexOf(product);//save index of that Product
                break;
            }
                
        }
        Products toDelete = DataSource._productList[index]; //getting product at index of id want to delete
        DataSource._productList.Remove(toDelete); //removing product from the list
    }

    public void Update(Products product)
    {
        int index = -1;
        foreach (Products p in DataSource._productList)//go over Product list
        {
            if (product.ID == p.ID) //if found id in the catalog
            {
                index = DataSource._productList.IndexOf(product);//save index of that Product
                break;
            }
        }
        if (index != -1)
        {
            DataSource._productList[index] = product; //updating item using same place in memory
        } else
        {
            throw new Exception("The product you wish to update does not exist");
        }
    }
}
