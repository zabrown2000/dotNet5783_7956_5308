
using System;
using DO;
using DalApi;
namespace Dal;

internal class DalOrder : IOrder                       
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
            if (order.CustomerName == null) //Orders's default name is empty string
            {
                //order.ID = DataSource.Config.NextOrderNumber;//giving this new order a unique id
                DataSource._orderList.Add(order); //add order to the order list
                return order.ID; //Added the order, returning id
            }
            //Case 2: Item already exists, throw exception
            int index = DataSource._orderList.FindIndex(x => x?.ID == order.ID); //getting the index of the order in the list (if it exists)
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
        Order? order = DataSource._orderList.Find(x => x?.ID == id); //checking to see if order exists
        if (order?.ID != id) //if not found the order id will be the default 0
            throw new EntityDoesNotExistException(new Order());
        return order ?? throw new EntityDoesNotExistException(new Order()); ;
    }

    /// <summary>
    /// Reading all orders in list
    /// </summary>
    /// <returns>a list with all the orders</returns>
    public IEnumerable<Order?> ReadAll(Func<Order?, bool>? filter = null)
    {
        if (filter == null) 
        {
            return DataSource._orderList.ToList(); //list of orders
        }

        return from v in DataSource._orderList//select with filter
               where filter(v)
               select v;
    }

    /// <summary>
    /// Delete function for order
    /// </summary>
    /// <param name="id">id of order to delete</param>
    /// <exception cref="Exception">Exception if order doesn't exist</exception>
    public void Delete(int id)
    {
        int index = DataSource._orderList.FindIndex(x => x?.ID == id);

        if (index == -1 )//if does not exist
            throw new EntityDoesNotExistException(new Order());
        DataSource._orderList.RemoveAt(index); //remove from list
    }

    /// <summary>
    /// Update function for orders
    /// </summary>
    /// <param name="order">order to update</param>
    /// <exception cref="Exception">Exception if order doesn't exist</exception>
    public void Update(Order order)
    {
        int index = DataSource._orderList.FindIndex(x => x?.ID == order.ID);

        if (index == -1)//if does not exist
            throw new EntityDoesNotExistException(new Order());
        DataSource._orderList[index] = order; //updating order using same place in memory
    }

    /// <summary>
    /// method to get an order by using a filter
    /// </summary>
    /// <param name="filter">filter to search</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="Exception"></exception>
    public Order ReadByFilter(Func<Order?, bool>? filter)
    {
        if (filter == null)
        {
            throw new ArgumentNullException(nameof(filter));//filter is null
        }
        Order? order = DataSource._orderList.Find(x => x != null && filter(x))!;
        if (order != null)
        {
            return (Order)order; //casting to non null value
        }
        throw new Exception("The order that was requested does not exist");
    }
}
