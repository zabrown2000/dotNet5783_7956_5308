using DO;
using DalApi;
using System;

namespace Dal;


internal class DalOrderItem : IOrderItem           
{
    /// <summary>
    /// Create function for orderitems
    /// </summary>
    /// <param name="orderItem">orderItem to add to list</param>
    /// <returns>id of new orderItem</returns>
    /// <exception cref="Exception">Exception if orderItem exists or list is full</exception>
    public int Add(OrderItem orderItem) //add OrderItem to the orderItem list and return its id
    {
        if (DataSource._orderItemList.Count() < 200) //checking that the orderItem cap hasn't been reached
        {
            //Case 1: New order, need to initialize it and add it
            if (orderItem.OrderID == 0) //OrdersItem's default ctor makes the orderId 0, so we want to make sure it's a new orderItem to add to our list
            {
                //orderItem.ID = DataSource.Config.NextOrderItemNumber; //giving this new orderItem a unique id
                DataSource._orderItemList.Add(orderItem); //add orderItem to the orderItem list
                return orderItem.ID; //Added the orderItem, returning id
            }
            //Case 2: orderItem already exists, throw exception
            int index = DataSource._orderItemList.FindIndex(x => x?.ID == orderItem.ID); //getting the index of the orderItem in the list (if it exists)
            if (index != -1) //orderItem was found, so it exists and can't add again
            {
                throw new EntityAlreadyExistsException(orderItem); //error
            }
            else
            {
                DataSource._orderItemList.Add(orderItem); //initialized orderItem but not in list yet so adding it
                return orderItem.ID; //return the id
            }
        } else
        {
            throw new EntityListIsFullException(orderItem);
        }       
    }

    /// <summary>
    /// Reading a particular orderItem
    /// </summary>
    /// <param name="id">if of orderItem want to get</param>
    /// <returns>orderItem wanted to read</returns>
    /// <exception cref="Exception">Exception if orderItem doesn't exist</exception>
    public OrderItem ReadId(int id)
    {
        OrderItem? orderItem = DataSource._orderItemList.Find(x => x?.ID == id); //checking to see if orderItem exists
        if (orderItem?.ID != id)  //if not found the item id will be the default 0
            throw new EntityDoesNotExistException(new OrderItem());
        return orderItem ?? throw new EntityDoesNotExistException(new OrderItem()); ;
    }

    /// <summary>
    /// Reading all the orderItems in list
    /// </summary>
    /// <returns>list with all the orderItems</returns>
    public IEnumerable<OrderItem?> ReadAll(Func<OrderItem?, bool>? filter = null)
    {
        if (filter == null)
        {
            return DataSource._orderItemList.ToList(); //list of orderItems
        }

        return from v in DataSource._orderItemList//select with filter
               where filter(v)
               select v;
    }

    /// <summary>
    /// Delete function for orderItems
    /// </summary>
    /// <param name="id">id of orderitem to delete</param>
    /// <exception cref="Exception">Exception if orderItem doesn't exist</exception>
    public void Delete(int id)
    {
        int index = DataSource._orderItemList.FindIndex(x => x?.ID == id);

        if (index == -1) //if does not exist
            throw new EntityDoesNotExistException(new OrderItem());
        DataSource._orderItemList.RemoveAt(index); //removing orderItem from the list
    }

    /// <summary>
    /// Update function for orderItem based on orderItem ID
    /// </summary>
    /// <param name="orderItem">orderItem want to update</param>
    /// <exception cref="Exception">Exception if orderItem doesn't exist</exception>
    public void SetByOrderItem(OrderItem orderItem)
    {
        int index = DataSource._orderItemList.FindIndex(x => x?.ID == orderItem.ID);

        if (index == -1) //if does not exist
            throw new EntityDoesNotExistException(new OrderItem());

        DataSource._orderItemList[index] = orderItem; //updating orderItem using same place in memory
    }
    
    /// <summary>
    /// update function for orderItem based on product ID and order ID
    /// </summary>
    /// <param name="orderItem"></param>
    /// <exception cref="Exception"></exception>
    public void SetByOrdProdID(OrderItem orderItem)
    {
        int index = DataSource._orderItemList.FindIndex(x => ((x?.ID == orderItem.ID) && (x?.ProductID == orderItem.ProductID)));

        if (index == -1) //if does not exist
            throw new EntityDoesNotExistException(new OrderItem());

        DataSource._orderItemList[index] = orderItem; //updating orderItem using same place in memory
    }

    /// <summary>
    /// Function to get the list of items in orderItems with a given order ID
    /// </summary>
    /// <param name="orderId">id of order item with the products we want</param>
    /// <returns>returns a list of products (by id) in order number of id</returns>
    public IEnumerable<OrderItem?> OrdersInOrderItem (int orderId)   
    {
        return from v in DataSource._orderItemList //go over OrderItem list
               where v?.OrderID == orderId //if found a matching order id to the one given
               select v;
    }

    /// <summary>
    /// Get an OrderItem given two identifiers - product ID and order ID
    /// </summary>
    /// <param name="orderId">order id in the orderItem</param>
    /// <param name="productId">product id in the orderItem</param>
    /// <returns>Respective OrderItem record</returns>
    public OrderItem GetOrderItem (int orderId, int productId) //returns specific product from order of id
    {
        OrderItem orderItem = new();

        orderItem = (DO.OrderItem)DataSource._orderItemList.FirstOrDefault(x => x != null && x?.OrderID == orderId && x?.ProductID == productId)!;
        if (orderItem.OrderID != orderId || orderItem.ProductID != productId)
        {
            throw new EntityDoesNotExistException(new OrderItem());
        }
        return orderItem; //return the OrderItem
    }

    public void Update(OrderItem orderItem) { }

    /// <summary>
    /// method to get an orderItem by using a filter
    /// </summary>
    /// <param name="filter">filter to search</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="Exception"></exception>
    public OrderItem ReadByFilter(Func<OrderItem?, bool>? filter)
    {
        if (filter == null)
        {
            throw new ArgumentNullException(nameof(filter));//filter is null
        }
        
        OrderItem? orderItem = DataSource._orderItemList.FirstOrDefault(x => x != null && filter(x));
        if (orderItem != null)
        {
            return (OrderItem)orderItem;
        }
        
        throw new Exception("The order item requested does not exist\n");
    }
}