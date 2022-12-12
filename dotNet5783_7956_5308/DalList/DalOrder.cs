
using System;
using DO;
using DalApi;
namespace Dal;

internal class DalOrder : IOrder                       //need new way to check if new or not
{
    /// <summary>
    /// Create function for orders
    /// </summary>
    /// <param name="order">order to add to list</param>
    /// <returns>id of new order</returns>
    /// <exception cref="Exception">Exception if order exists or list is full</exception>
    public int Add(Order order)
    {
        if (DataSource._orderList.Count() < 100) //checking that the order cap hasn't been reached
        {
            //Case 1: New order, need to initialize it and add it
            if (order.ID == 0) //Orders's default ctor makes the id 0, so we want to make sure it's a new order to add to our list
            {
                //order.ID = DataSource.Config.NextOrderNumber;//giving this new order a unique id
                DataSource._orderList.Add(order); //add order to the order list
                return order.ID; //Added the order, returning id
            }
            //Case 2: Item already exists, throw exception
            int index = DataSource._orderList.FindIndex(x => x.ID == order.ID); //getting the index of the order in the list (if it exists)
            if (index != -1) //order was found, so it exists and can't add again
            {
                throw new EntityAlreadyExistsException(order); 
            }
            else
            {
                DataSource._orderList.Add(order); //initialized order but not in list yet so adding it
                return order.ID; //return the id
            }
        } else
        {
            throw new EntityListIsFullException(order);
        }

        
    }

    /// <summary>
    /// Reading a particular order
    /// </summary>
    /// <param name="id">id of order want to get</param>
    /// <returns>order wanted to read</returns>
    /// <exception cref="Exception">Exception if order doesn't exist</exception>
    public Order ReadId(int id)
    {
        Order order = DataSource._orderList.Find(x => x.ID == id); //checking to see if order exists
        if (order.ID == 0) //if not found the order id will be the default 0
            throw new EntityDoesNotExistException(new Order());
        return order;
    }

    /// <summary>
    /// Reading all orders in list
    /// </summary>
    /// <returns>a list with all the orders</returns>
    public IEnumerable<Order> ReadAll()
    {
        return DataSource._orderList.ToList(); //list of orders
    }

    /// <summary>
    /// Delete function for order
    /// </summary>
    /// <param name="id">id of order to delete</param>
    /// <exception cref="Exception">Exception if order doesn't exist</exception>
    public void Delete(int id)
    {
        int index = -1; //flag for checking if order exists
        foreach (Order order in DataSource._orderList) //searching for order in list based on ID
        {
            if (order.ID == id) //if found id in the list
            {
                index = DataSource._orderList.IndexOf(order);//save index of that order
                break;
            }
        }
        if (index != -1) //check to make sure actually deleting order that exists
        {
            Order toDelete = DataSource._orderList[index]; //getting order at index of id want to delete
            DataSource._orderList.Remove(toDelete); //removing order from the list
        }
        else
        {
            throw new EntityDoesNotExistException(new Order());
        }
    }

    /// <summary>
    /// Update function for orders
    /// </summary>
    /// <param name="order">order to update</param>
    /// <exception cref="Exception">Exception if order doesn't exist</exception>
    public void Update(Order order)
    {
        int index = -1; //flag for checking if order exists
        foreach (Order or in DataSource._orderList)//go over order list
        {
            if (order.ID == or.ID)//if found id in the list
            {
                index = DataSource._orderList.IndexOf(or); //save index of that order
                break;
            }
        }
        if (index != -1)
        {
            DataSource._orderList[index] = order; //updating order using same place in memory

        }
        else
        {
            throw new EntityDoesNotExistException(order);
        }
    }
}
