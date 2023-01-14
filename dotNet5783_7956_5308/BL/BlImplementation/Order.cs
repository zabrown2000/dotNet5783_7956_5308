using BlApi;
using Dal;
using BO;
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
        IEnumerable<DO.Order?> orders = dal?.dalOrder.ReadAll(); //getting all DO orders 
        IEnumerable<DO.OrderItem?> orderItems = dal?.dalOrderItem.ReadAll(); //getting all DO orderItems
        
        return from DO.Order? o in orders
               select new BO.OrderForList //for each order we have we add an entry to the OrderForList list
               {
                   ID = o?.ID ?? throw new BO.BOEntityDoesNotExistException("Missing order in list"), //if the order id is null then throw an exception
                   CustomerName = o?.CustomerName,
                   Status = GetOrderStatus(o.Value),
                   AmountOfItems = orderItems.Select(orderItems => orderItems?.ID == o?.ID).Count(), //counting amount of orders in the orderitem list and setting that for BO orderForList amount
                   TotalPrice = (double)orderItems.Sum(orderItems => orderItems?.Price) //summing up each orderitem for this new BO list entry
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
            order = (DO.Order)dal?.dalOrder.ReadId(orderId);//get right DO Order
        } catch
        {
            throw new BO.BOEntityDoesNotExistException("Order does not exist\n");
        }
        
        double tempPrice = 0;
        foreach (DO.OrderItem o in dal?.dalOrderItem.ReadAll())
        {
            if (o.OrderID == orderId)
            {
                tempPrice += o.Price; //add up all prices of all orders in orderItem lists
            }
        }
        if (order.ID == orderId) //at this point we know it exists
        {
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
            }; //new BO Order
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
            order = (DO.Order)dal?.dalOrder.ReadId(orderId); //get right DO Order
        }
        catch
        {
            throw new BO.BOEntityDoesNotExistException("Order does not exist\n");
        }
        if (order.ID == orderId && order.ShipDate != DateTime.MinValue) //if order exists and has not been shipped 
        {
            DO.Order o = new()
            {
                ID = orderId,
                CustomerAddress = order.CustomerAddress,
                CustomerEmail = order.CustomerEmail,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                ShipDate = DateTime.Now,
                DeliveryDate = DateTime.MinValue,
            }; //set new ship date in new DO Order
            dal.dalOrder.Update(o); //update the order in DO
            double tempPrice = 0;
            foreach (DO.OrderItem temp in dal?.dalOrderItem.ReadAll())
            {
                if (temp.OrderID == o.ID)
                {
                    tempPrice += temp.Price;//add up all of prices in the order
                }
            }
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
                DeliveryDate = DateTime.MinValue,
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
            order = (DO.Order)dal?.dalOrder.ReadId(orderId); //get right DO Order
        }
        catch
        {
            throw new BO.BOEntityDoesNotExistException("Order does not exist\n");
        }
        if (order.ID == orderId && order.DeliveryDate != DateTime.MinValue) //if order exists and has not been shipped (would have set it already if delivered) 
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
            dal?.dalOrder.Update(o);//update the order in DO
            double tempPrice = 0;
            foreach (DO.OrderItem temp in dal?.dalOrderItem.ReadAll())
            {
                if (temp.OrderID == o.ID)
                {
                    tempPrice += temp.Price; //add up all prices of this order in the orderItem list
                }
            }
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
}
