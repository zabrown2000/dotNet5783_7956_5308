using System;
using System.Linq;
using DO;
namespace Dal;


public class DalOrderItem
{
    public int Add(OrderItem orderItem)//add OrderItem to the orderItem list and return its id
    {
        //Case 1: New order, need to initialize it and add it

        if (orderItem.ID == 0) //Orders's default ctor makes the id 0, so we want to make sure it's a new order to add to our list
        { 
            orderItem.ID = DataSource.Config.NextOrderItemNumber; //giving this new product a unique id
            DataSource._orderItemList.Add(orderItem); //add order to the orderItem list
            return orderItem.ID; //Added the order, returning id
        }
        //Case 2: Item already exists, throw exception

        int index = DataSource._orderItemList.FindIndex(x => x.ID == orderItem.ID); //getting the index of the product in the list (if it exists)
        if (index != -1) //item was found, so it exists and can't add again
        {
            throw new Exception("OrderItem already exists"); //error
        }
        else
        {
            DataSource._orderItemList.Add(orderItem); //initialized product but not in catalog yet so adding it
            return orderItem.ID; //return the id
        }
    }
    /*○ Implement a read method for a single object: input - an entity’s identifier (not an array
        index!), output - an object.
    ○ Implement a read method for a list of objects stored in the entity array. No input
        arguments.
*/
    public OrderItem ReadId(int id)
    {
        OrderItem orderItem = DataSource._orderItemList.Find(x => x.ID == id); //checking to see if product exists
        if (orderItem.ID != id)  //if not found the item id will be the default, 0, and not match the given id
            throw new Exception("The orderItem does not exist\n");
        return orderItem;
    }

    public List<OrderItem> ReadAll()
    {
        return DataSource._orderItemList.ToList();
    }

    public void Delete(int id)
    {
        int index = -1;
        foreach (OrderItem orderItem in DataSource._orderItemList) //searching for orderItem in list based on ID
        {
            if (orderItem.ID == id) //if found id in the list
            {
                index = DataSource._orderItemList.IndexOf(orderItem);//save index of that orderItem
                break;
            }

        }
        if (index != -1) //check to make sure actually deleting item that exists
        {
            OrderItem toDelete = DataSource._orderItemList[index]; //getting orderItem at index of id want to delete
            DataSource._orderItemList.Remove(toDelete); //removing orderItem from the list
        }
        else
        {
            throw new Exception("OrderItem does not exist\n");
        }
        
    }

    public void Update(OrderItem orderItem)
    {
        int index = -1;
        foreach (OrderItem or in DataSource._orderItemList)//go over orderItem list
        {
            if (orderItem.ID == or.ID)//if found id in the list
            {
                index = DataSource._orderItemList.IndexOf(orderItem); //save index of that orderItem
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
        List<int> productsInOrder = new List<int>();
        foreach (OrderItem orderItem in DataSource._orderItemList) //go over OrderItem list
        {
            if (orderItem.OrderID == orderId) //if found a matching id to the one inputted
                productsInOrder.Append(orderItem.ProductID); //add to the list
                //productsInOrder.Append(orderItem); //add to the list
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
        } //find the order of id with product
        return returnItem; //return the OrderItem
    }

}