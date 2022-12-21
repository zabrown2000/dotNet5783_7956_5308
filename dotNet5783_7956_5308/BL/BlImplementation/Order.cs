using BlApi;
using DalApi;
using Dal;
using BO;
//ADD DOCUMENTATION
namespace BlImplementation;


internal class Order : BlApi.IOrder
{
    static IDal? dal = new DalList();
    /// <summary>
    /// Getting all orders in data layer and forming a list of type OrderForList for the business layer
    /// </summary>
    /// <returns>List of OrderForList</returns>
    /// <exception cref="BO.BOEntityDoesNotExistException"></exception>
    public IEnumerable<OrderForList?> GetAllOrderForList()
    {
        IEnumerable<DO.Order?> orders = dal.dalOrder.ReadAll(); //getting all DO orders 
        IEnumerable<DO.OrderItem?> orderItems = dal.dalOrderItem.ReadAll(); //getting all DO orderItems
        
        return from DO.Order? o in orders
               select new BO.OrderForList //for each order we have we add an entry to the OrderForList list
               {
                   ID = o?.ID ?? throw new BO.BOEntityDoesNotExistException(), //if the order id is null then throw an exception
                   CustomerName = o?.CustomerName,
                   Status = GetStatus(o.Value),
                   AmountOfItems = orderItems.Select(orderItems => orderItems?.ID == o?.ID).Count(), //counting amount of orders in the orderitem list and setting that for BO orderForList amount
                   TotalPrice = (double)orderItems.Sum(orderItems => orderItems?.Price) //summing up each orderitem for this new BO list entry
               };
    }

    /// <summary>
    /// helper function to get status of a given Dal order
    /// </summary>
    /// <param name="order">order we want status of</param>
    /// <returns></returns>
    private BO.Enums.OrderStatus GetStatus(DO.Order order)
    {

        return order.DeliveryDate != DateTime.MinValue ? BO.Enums.OrderStatus.Completed : order.ShipDate != DateTime.MinValue ?
            BO.Enums.OrderStatus.InProgress : BO.Enums.OrderStatus.New; //determining order status by checking if the dates are set
    }

    /// <summary>
    /// Given a DO order ID will return a BO order
    /// </summary>
    /// <param name="orderId">id of DO order</param>
    /// <returns>new BO order</returns>
    /// <exception cref="BO.BOEntityDoesNotExistException"></exception>
    public BO.Order GetBoOrder(int orderId)
    {
        if (orderId < 0) //check if it exists
        {
            throw new BO.BOEntityDoesNotExistException();
        }
        DO.Order order;
        try
        {
            order = dal.dalOrder.ReadId(orderId);//get right DO Order
        } catch
        {
            throw new BO.BOEntityDoesNotExistException("Order does not exist\n");
        }
        
        double tempPrice = 0;
        foreach (DO.OrderItem o in dal.dalOrderItem.ReadAll())
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
                Status = GetStatus(order),
                TotalPrice = tempPrice,
            }; //new BO Order
        }
        throw new BO.BOEntityDoesNotExistException("Order does not exist\n"); //taking into account any errors we missed
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
            order = dal.dalOrder.ReadId(orderId); //get right DO Order
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
            dal.dalOrder.Update(o);//update the order in DO
            double tempPrice = 0;
            foreach (DO.OrderItem temp in dal.dalOrderItem.ReadAll())
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
                Status = GetStatus(o),
                TotalPrice = tempPrice,
            }; //new BO Order
        }
        throw new BO.BOEntityDoesNotExistException("Order does not exist\n");

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
            order = dal.dalOrder.ReadId(orderId); //get right DO Order
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
            foreach (DO.OrderItem temp in dal.dalOrderItem.ReadAll())
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
                Status = GetStatus(o),
                TotalPrice = tempPrice,
                DeliveryDate = DateTime.MinValue,
            }; //new BO Order
        }
        throw new BO.BOEntityDoesNotExistException("Order does not exist\n");
    }
    /*
     * DO WE NEED TO INCLUDE THIS FUNCTION??
    public OrderTracking GetOrderTracking(int orderId)
    {
        OrderTracking ot = new();//create new order tracking
        ot.Tracking = new();
        foreach (DO.Order? item in DOList.Order.GetAll())//go over all orders in DO
        {
            if (item?.ID == orderId)//if order exists 
            {
                ot.ID = orderId;//save id
                if (item?.DeliveryDate != null)//if order delivered 
                {
                    ot.Status = BO.Enums.Status.Shipped;//save status
                    ot.Tracking.Add(Tuple.Create(item?.OrderDate ?? throw new BO.UnfoundException(), "The order has been created\n"));//save tracking
                    ot.Tracking.Add(Tuple.Create(item?.ShipDate ?? throw new BO.UnfoundException(), "The order has been shipped\n"));//save tracking
                    ot.Tracking.Add(Tuple.Create(DateTime.Now, "The order has been delivered\n"));//save tracking
                    return ot;
                }
                if (item?.ShipDate != null)//if order shipped 
                {
                    ot.Status = BO.Enums.Status.Shipped;//save status
                    ot.Tracking.Add(Tuple.Create(item?.OrderDate ?? throw new BO.UnfoundException(), "The order has been created\n"));//save tracking
                    ot.Tracking.Add(Tuple.Create(DateTime.Now, "The order has been shipped\n"));//save tracking
                    //ot.Tracking.Add(Tuple.Create(null, "The order has been delivered\n"));//save tracking
                    return ot;
                }
                if (item?.OrderDate == DateTime.Now)//if order created now
                {
                    ot.Status = BO.Enums.Status.JustOrdered;//save status
                    ot.Tracking.Add(Tuple.Create(DateTime.Now, "The order has been created\n"));//save tracking
                    //ot.Tracking.Add(Tuple.Create (item?.ShipDate??throw new BO.UnfoundException(), "The order has been shipped\n"));//save tracking
                    //ot.Tracking.Add(Tuple.Create(item?.DeliveryDate ?? throw new BO.UnfoundException(), "The order has been delivered\n" ));//save tracking
                    return ot;
                }


            }
        }
        throw new BO.UnfoundException("Order does not exist\n");//order does not exist
    }//get order id, check if exists, and build strings of dates and status in DO orders

    */
}
