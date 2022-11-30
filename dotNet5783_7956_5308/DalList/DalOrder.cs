
using System;
using DO;
namespace Dal;


public class DalOrder
{
    public int Add(Order order)//add order to a list and return its id
    {
        //Case 1: New order, need to initialize it and add it

        if (order.ID == 0) //Orders's default ctor makes the id 0, so we want to make sure it's a new order to add to our list
        {
            order.ID = DataSource.Config.NextOrderNumber;//giving this new product a unique id
            DataSource._orderList.Add(order);             //add order to the order list
            return order.ID; //Added the order, returning id
        }
        //Case 2: Item already exists, throw exception

        int index = DataSource._orderList.FindIndex(x => x.ID == order.ID); //getting the index of the product in the list (if it exists)
        if (index != -1) //item was found, so it exists and can't add again
        {
            throw new Exception("Order already exists"); //error
        }
        else
        {
            DataSource._orderList.Add(order); //initialized product but not in catalog yet so adding it
            return order.ID; //return the id
        }
    }
    /*○ Implement a read method for a single object: input - an entity’s identifier (not an array
        index!), output - an object.
    ○ Implement a read method for a list of objects stored in the entity array. No input
        arguments.
*/
    public Order ReadId(int id)
    {
        Order order = DataSource._orderList.Find(x => x.ID == id); //checking to see if product exists
        if (order.ID != id) //if not found the item id will be the default, 0, and not match the given id
            throw new Exception("The order does not exist\n");
        return order;
    }

    public List<Order> GetAll()
    {
        return DataSource._orderList.ToList();
    }

    public void Delete(int id)
    {
        int index = 0;
        foreach (Order order in DataSource._orderList) //searching for order in list based on ID
        {
            if (order.ID == id) //if found id in the catalog
            {
                index = DataSource._orderList.IndexOf(order);//save index of that order
                break;
            }

        }
        Order toDelete = DataSource._orderList[index]; //getting order at index of id want to delete
        DataSource._orderList.Remove(toDelete); //removing order from the list
    }



    public void Update(Order order)
    {
        int index = -1;
        foreach (Order or in DataSource._orderList)//go over order list
        {
            if (order.ID == or.ID)//if found id in the list
            {
                index = DataSource._orderList.IndexOf(order); //save index of that order
                break;
            }
        }
        if (index != -1)
        {
            DataSource._orderList[index] = order; //updating item using same place in memory

        }
        else
        {
            throw new Exception("The order you wish to update does not exist");
        }
    }

}
