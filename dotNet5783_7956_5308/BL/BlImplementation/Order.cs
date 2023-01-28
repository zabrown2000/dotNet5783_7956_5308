using BlApi;
using Dal;
using BO;
using System.Security.Cryptography;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    static DalApi.IDal? dal = DalApi.Factory.Get();
    /// <summary>
    /// Getting all orders in data layer and forming a list of type OrderForList for the business layer
    /// </summary>
    /// <returns>List of OrderForList</returns>
    /// <exception cref="BO.BOEntityDoesNotExistException"></exception>
    public IEnumerable<OrderForList?> ReadAllOrderForList()
    {
        IEnumerable<DO.Order?> orders = dal?.dalOrder.ReadAll()!; //getting all DO orders 
        IEnumerable<DO.OrderItem?> orderItems = dal?.dalOrderItem.ReadAll()!; //getting all DO orderItems
        
        return from DO.Order? o in orders
               select new BO.OrderForList //for each order we have we add an entry to the OrderForList list
               {
                   ID = o?.ID ?? throw new BO.BOEntityDoesNotExistException("Missing order in list"), //if the order id is null then throw an exception
                   CustomerName = o?.CustomerName,
                   Status = GetOrderStatus(o.Value),
                   AmountOfItems = orderItems.Select(orderItems => orderItems?.ID == o?.ID).Count(), //counting amount of orders in the orderitem list and setting that for BO orderForList amount
                   TotalPrice = (double)(orderItems.Sum(orderItems => orderItems?.Price)!) //summing up each orderitem for this new BO list entry
               };
    }

    /// <summary>
    /// helper function to get status of a given Dal order
    /// </summary>
    /// <param name="order">order we want status of</param>
    /// <returns></returns>
    private BO.Enums.OrderStatus GetOrderStatus(DO.Order order)
    {   
        return order.DeliveryDate != null ? BO.Enums.OrderStatus.Recieved : order.ShipDate != null ?
            BO.Enums.OrderStatus.Shipped : BO.Enums.OrderStatus.JustOrdered; //determining order status by checking if the dates are set
    }

    /// <summary>
    /// Given a DO order ID will return a BO order
    /// </summary>
    /// <param name="orderId">id of DO order</param>
    /// <returns>new BO order</returns>
    /// <exception cref="BO.BOEntityDoesNotExistException"></exception>
    public BO.Order ReadBoOrder(int orderId)
    {
        if (orderId < 0) //check if it exists
        {
            throw new BO.BOEntityDoesNotExistException("Order does not exist");
        }
        DO.Order order;
        try
        {
            order = (DO.Order)dal?.dalOrder.ReadId(orderId)!;//get right DO Order
        } catch
        {
            throw new BO.BOEntityDoesNotExistException("Order does not exist\n");
        }
        
        double tempPrice = 0;
        tempPrice = (double)(dal?.dalOrderItem.ReadAll()!.Where(x => x != null && x?.OrderID == orderId).Sum(x => x?.Price)!);

        if (order.ID == orderId)//if exists 
        {
            List<DO.OrderItem?> temp = dal?.dalOrderItem.ReadAll()!.Where(x => x != null && x?.OrderID == orderId).ToList()!; //get matching dal orderItems
            List<BO.OrderItem> bList = new();
            foreach (DO.OrderItem o in temp)
            {
                bList.Add(new BO.OrderItem()
                {
                    Amount = o.Amount,
                    ID = o.ID,
                    ProductID = o.ProductID,
                    Price = o.Price,
                }); //populate bo orderItem list with dal orderItems
            }

            return new BO.Order
            {
                ID = orderId,
                CustomerAddress = order.CustomerAddress,
                CustomerEmail = order.CustomerEmail,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                DeliveryDate = order.DeliveryDate,
                Status = GetOrderStatus(order),
                TotalPrice = tempPrice,
                Items = bList!
            };//new BO Order
        }
        throw new BO.BOEntityDoesNotExistException("Order does not exist\n"); //taking into account any errors we missed
    }
    
    /// <summary>
    /// Given a DO order ID will update the order to be shipped
    /// </summary>
    /// <param name="orderId">id order to update</param>
    /// <returns>updated BO order</returns>
    /// <exception cref="BO.BOEntityDoesNotExistException"></exception>
    public BO.Order ShipUpdate(int orderId)
    {
        DO.Order order;
        try
        {
            order = (DO.Order)dal?.dalOrder.ReadId(orderId)!; //get right DO Order
        }
        catch (DO.EntityDoesNotExistException)
        {
            throw new BO.BOEntityDoesNotExistException("Order does not exist\n");
        }
        if (order.ID == orderId /*&& order.ShipDate != DateTime.MinValue*/) //if order exists and has not been shipped 
        {
            DO.Order o = new()
            {
                ID = orderId,
                CustomerAddress = order.CustomerAddress,
                CustomerEmail = order.CustomerEmail,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                ShipDate = DateTime.Now,
                DeliveryDate = null,
            }; //set new ship date in new DO Order
            dal.dalOrder.Update(o); //update the order in DO
            double tempPrice = 0;
            tempPrice = (double)(dal?.dalOrderItem.ReadAll()!.Where(x => x != null && x?.OrderID == o.ID).Sum(x => x?.Price)!);
            return new BO.Order
            {
                ID = orderId,
                CustomerAddress = order.CustomerAddress,
                CustomerEmail = order.CustomerEmail,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                ShipDate = DateTime.Now,
                Status = GetOrderStatus(o),
                TotalPrice = tempPrice,
                DeliveryDate = null,
            }; //new BO Order
        }
        throw new BO.BOEntityDoesNotExistException("Order has already been shipped\n");
    }
    /// <summary>
    /// Given a DO order ID will update the order to be delivered
    /// </summary>
    /// <param name="orderId">id of order want to update</param>
    /// <returns>the order just updated</returns>
    /// <exception cref="BO.BOEntityDoesNotExistException"></exception>
    public BO.Order DeliveredUpdate(int orderId)
    {
        DO.Order order;
        try
        {
            order = (DO.Order)dal?.dalOrder.ReadId(orderId)!; //get right DO Order
        }
        catch (DO.EntityDoesNotExistException)
        {
            throw new BO.BOEntityDoesNotExistException("Order does not exist\n");
        }
        if (order.ID == orderId /*&& order.DeliveryDate != DateTime.MinValue*/) //if order exists and has not been shipped (would have set it already if delivered) 
        {
            DO.Order o = new()
            {
                ID = orderId,
                CustomerAddress = order.CustomerAddress,
                CustomerEmail = order.CustomerEmail,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                DeliveryDate = DateTime.Now, //the only difference, setting updated delivery date
            };
            try
            {
                dal?.dalOrder.Update(o);//update the order in DO
            }
            catch (DO.EntityDoesNotExistException)
            {
                throw new BO.BOEntityDoesNotExistException("Order does not exist\n");
            }
            double tempPrice = 0;
            tempPrice = (double)(dal?.dalOrderItem.ReadAll()!.Where(x => x != null && x?.OrderID == o.ID).Sum(x => x?.Price)!);
           
            return new BO.Order
            {
                ID = orderId,
                CustomerAddress = order.CustomerAddress,
                CustomerEmail = order.CustomerEmail,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                DeliveryDate = DateTime.Now,
                Status = GetOrderStatus(o),
                TotalPrice = tempPrice,
            }; //new BO Order
        }
        throw new BO.BOEntityDoesNotExistException("Order does not exist\n");
    }

    public OrderTrackings GetOrderTracking(int orderId)
    {
        DO.Order order = new();
        try
        {
            order = (DO.Order)dal?.dalOrder.ReadId(orderId)!;//get the requested order from dal
        }
        catch
        {
            throw new BO.BOEntityDoesNotExistException("The order requested does not exist\n");//order does not exist
        }
        return new OrderTrackings()
        {
            ID = orderId,
            Status = GetOrderStatus(order),
            Tracking = new List<Tuple<DateTime?, string>> { new Tuple<DateTime?, string>(order.OrderDate, "Approved"), new Tuple<DateTime?, string>(order.ShipDate, "Sent"),
            new Tuple<DateTime?, string>(order.DeliveryDate, "Delivered")}
        };//create new order tracking


    }
}
