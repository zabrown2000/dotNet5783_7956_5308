using System;
using System.Linq;
using DO;
namespace Dal;


public class DalOrderItem
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
            if (orderItem.ID == 0) //OrdersItem's default ctor makes the id 0, so we want to make sure it's a new orderItem to add to our list
            {
                orderItem.ID = DataSource.Config.NextOrderItemNumber; //giving this new orderItem a unique id
                DataSource._orderItemList.Add(orderItem); //add orderItem to the orderItem list
                return orderItem.ID; //Added the orderItem, returning id
            }
            //Case 2: orderItem already exists, throw exception
            int index = DataSource._orderItemList.FindIndex(x => x.ID == orderItem.ID); //getting the index of the orderItem in the list (if it exists)
            if (index != -1) //orderItem was found, so it exists and can't add again
            {
                throw new Exception("OrderItem already exists"); //error
            }
            else
            {
                DataSource._orderItemList.Add(orderItem); //initialized orderItem but not in list yet so adding it
                return orderItem.ID; //return the id
            }
        } else
        {
            throw new Exception("OrderItem list is full");
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
        OrderItem orderItem = DataSource._orderItemList.Find(x => x.ID == id); //checking to see if orderItem exists
        if (orderItem.ID != id)  //if not found the item id will be the default, 0, and not match the given id
            throw new Exception("The orderItem does not exist\n");
        return orderItem;
    }

    /// <summary>
    /// Reading all the orderItems in list
    /// </summary>
    /// <returns>list with all the orderItems</returns>
    public List<OrderItem> ReadAll()
    {
        return DataSource._orderItemList.ToList(); //list of orderItems
    }

    /// <summary>
    /// Delete function for orderItems
    /// </summary>
    /// <param name="id">id of orderitem to delete</param>
    /// <exception cref="Exception">Exception if orderItem doesn't exist</exception>
    public void Delete(int id)
    {
        int index = -1; //flag for checking if orderItem exists
        foreach (OrderItem orderItem in DataSource._orderItemList) //searching for orderItem in list based on ID
        {
            if (orderItem.ID == id) //if found id in the list
            {
                index = DataSource._orderItemList.IndexOf(orderItem);//save index of that orderItem
                break;
            }

        }
        if (index != -1) //check to make sure actually deleting orderItem that exists
        {
            OrderItem toDelete = DataSource._orderItemList[index]; //getting orderItem at index of id want to delete
            DataSource._orderItemList.Remove(toDelete); //removing orderItem from the list
        }
        else
        {
            throw new Exception("OrderItem does not exist\n");
        }
        
    }

    /// <summary>
    /// Update function for orderItem
    /// </summary>
    /// <param name="orderItem">orderItem want to update</param>
    /// <exception cref="Exception">Exception if orderItem doesn't exist</exception>
    public void Update(OrderItem orderItem)
    {
        int index = -1; //flag for checking if orderItem exists
        foreach (OrderItem ori in DataSource._orderItemList) //go over orderItem list
        {
            if (orderItem.ID == ori.ID) //if found id in the list
            {
                index = DataSource._orderItemList.IndexOf(ori); //save index of that orderItem
                break;
            }
        }
        if (index != -1)
        {
            DataSource._orderItemList[index] = orderItem; //updating orderItem using same place in memory
        }
        else
        {
            throw new Exception("The orderItem you wish to update does not exist");
        }
    }

    /// <summary>
    /// Function to get the list of items in an order, given the order ID
    /// </summary>
    /// <param name="orderId">id of order item with the products we want</param>
    /// <returns>returns a list of products (by id) in order number of id</returns>
    public List<int> ProductsInOrderItem (int orderId)   
    {
        List<int> productsInOrder = new List<int>(); //list to place ids of products
        foreach (OrderItem orderItem in DataSource._orderItemList) //go over OrderItem list
        {
            if (orderItem.OrderID == orderId) //if found a matching order id to the one sent
                productsInOrder.Append(orderItem.ProductID); //add id of product in that ordeItem to the list
        }
        return productsInOrder; //return the products
    }

    /// <summary>
    /// Get an OrderItem given two identifiers - product ID and order ID
    /// </summary>
    /// <param name="orderId">order id in the orderItem</param>
    /// <param name="productId">product id in the orderItem</param>
    /// <returns>Respective OrderItem record</returns>
    public OrderItem GetOrderItem (int orderId, int productId) //returns specific product from order of id
    {
        OrderItem returnItem = new();
        
        foreach (OrderItem orderItem in DataSource._orderItemList) //go over OrderItem list
        {
            if (orderItem.OrderID == orderId && orderItem.ProductID == productId) //if found and orderItem that matches the given ID and product
            {
                returnItem = orderItem; //save the orderItem
            }
        } 
        return returnItem; //return the OrderItem
    }
}