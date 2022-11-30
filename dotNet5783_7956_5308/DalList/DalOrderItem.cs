using System;
using DO;
namespace Dal;

/*Implementing the data layer for order details OrderItem: In addition to the classic access
methods (CRUD) we expect to find the following
● Get/Set an OrderItem given two identifiers - product ID and order ID. The return
value is the respective OrderItem record.
● Get/Set the list of items in an order, given the order ID*/
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

    public List<OrderItem> GetAll()
    {
        return DataSource._orderItemList.ToList();
    }

    public void Delete(int id)
    {
        int index = 0;
        foreach (OrderItem orderItem in DataSource._orderItemList) //searching for orderItem in list based on ID
        {
            if (orderItem.ID == id) //if found id in the list
            {
                index = DataSource._orderItemList.IndexOf(orderItem);//save index of that orderItem
                break;
            }

        }
        OrderItem toDelete = DataSource._orderItemList[index]; //getting orderItem at index of id want to delete
        DataSource._orderItemList.Remove(toDelete); //removing orderItem from the list
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

    public List<OrderItem> ItemsInOrder(int id)   //returns a list of products in order number of id
    {
        List<OrderItem> orderItems = new List<OrderItem>();
        foreach (OrderItem orderItem in DataSource._orderItemList) //go over OrderItem list
        {
            if (orderItem.ID == id) //if found a matching id to the one inputted
                orderItems.Append(orderItem); //add to the list
        } 
        return orderItems; //return the products
    }

    public OrderItem ItemOfOrder(int id, int productId) //returns specific product from order of id
    {
        OrderItem returnOI = new();
        
        foreach (OrderItem orderItem in DataSource._orderItemList) //go over OrderItem list
        {
            if (orderItem.OrderID == id && orderItem.ProductID == productId) //if found and orderItem that matches the given ID and product
            {
                    returnOI = orderItem; //save the orderItem
            }
        } //find the order of id with product
        return returnOI; //return the OrderItem
    }

}