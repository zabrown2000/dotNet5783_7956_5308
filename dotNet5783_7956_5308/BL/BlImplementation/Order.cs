using BlApi;
using DalApi;
using Dal;
using BO;
//ADD DOCUMENTATION
namespace BlImplementation;


internal class Order : BlApi.IOrder
{
    static IDal? dal = new DalList(); 
    public IEnumerable<OrderForList?> GetAllOrderForList()
    {
        IEnumerable<DO.Order?> orders = dal.dalOrder.ReadAll();//get all orders from DO 
        IEnumerable<DO.OrderItem?> orderItems = dal.dalOrderItem.ReadAll();//get all orderItems from DO 
        return from DO.Order? ord in orders
               select new BO.OrderForList
               {
                   ID = ord?.ID ?? throw new BO.BOEntityDoesNotExistException(),
                   CustomerName = ord?.CustomerName,
                   Status = GetStatus(ord.Value),
                   AmountOfItems = orderItems.Select(orderItems => orderItems?.ID == ord?.ID).Count(),
                   TotalPrice = (double)orderItems.Sum(orderItems => orderItems?.Price)
               };

    }//calls get of DO order list, gets items for each order, and build orderItemlist

    private BO.Enums.OrderStatus GetStatus(DO.Order order)
    {

        return order.DeliveryDate != DateTime.MinValue ? BO.Enums.OrderStatus.Completed : order.ShipDate != DateTime.MinValue ?
            BO.Enums.OrderStatus.InProgress : BO.Enums.OrderStatus.New;
    }

    public BO.Order GetBoOrder(int id)
    {
        if (id < 0)//id is negative
        {
            throw new BO.BOEntityDoesNotExistException();
        }
        DO.Order ord = dal.dalOrder.ReadId(id);//get right DO Order
        double priceTemp = 0;
        foreach (DO.OrderItem o in dal.dalOrderItem.ReadAll())
        {
            if (o.OrderID == id)
            {
                priceTemp += o.Price;//add up all of prices in the order
            }
        }
        if (ord.ID == id)//if exists 
        {
            return new BO.Order
            {
                ID = id,
                CustomerAddress = ord.CustomerAddress,
                CustomerEmail = ord.CustomerEmail,
                CustomerName = ord.CustomerName,
                OrderDate = ord.OrderDate,
                ShipDate = ord.ShipDate,
                DeliveryDate = ord.DeliveryDate,
                Status = GetStatus(ord),
                TotalPrice = priceTemp,
            };//new BO Order
        }
        throw new BO.BOEntityDoesNotExistException("Order does not exist\n");
    }//get order number, check if exists, update date in DO order, and return BO order that has been "shipped"
    public BO.Order DeliveredUpdate(int orderId)
    {

        DO.Order oId = dal.dalOrder.ReadId(orderId);//get the order from DO of orderId-or catch exception
        if (oId.ID == orderId && oId.DeliveryDate < DateTime.Today)//if oId exists and has not been shipped 
        {
            DO.Order o = new()
            {
                ID = orderId,
                CustomerAddress = oId.CustomerAddress,
                CustomerEmail = oId.CustomerEmail,
                CustomerName = oId.CustomerName,
                OrderDate = oId.OrderDate,
                ShipDate = oId.ShipDate,
                DeliveryDate = DateTime.Now,//the only difference
            };//set new delivery date in new DO Order
            dal.dalOrder.Update(o);//update the order in DO
            double priceTemp = 0;
            foreach (DO.OrderItem temp in dal.dalOrderItem.ReadAll())
            {
                if (temp.OrderID == o.ID)
                {
                    priceTemp += temp.Price;//add up all of prices in the order
                }
            }
            return new BO.Order
            {
                ID = orderId,
                CustomerAddress = oId.CustomerAddress,
                CustomerEmail = oId.CustomerEmail,
                CustomerName = oId.CustomerName,
                OrderDate = oId.OrderDate,
                ShipDate = oId.ShipDate,
                DeliveryDate = DateTime.Now,
                Status = GetStatus(o),
                TotalPrice = priceTemp,
            };//new BO Order
        }
        throw new BO.BOEntityDoesNotExistException("Order does not exist\n");

    }//get order number, check if exists, update date in DO order, and return BO order that has been "delivered" 
    public BO.Order ShipUpdate(int orderId)
    {
        DO.Order oId = dal.dalOrder.ReadId(orderId);//get the order from DO of orderId-or catch exception
        if (oId.ID == orderId && oId.ShipDate < DateTime.Today)//if oId exists and has not been shipped 
        {
            DO.Order o = new()
            {
                ID = orderId,
                CustomerAddress = oId.CustomerAddress,
                CustomerEmail = oId.CustomerEmail,
                CustomerName = oId.CustomerName,
                OrderDate = oId.OrderDate,
                ShipDate = DateTime.Now,
                DeliveryDate = DateTime.MinValue,
            };//set new ship date in new DO Order
            dal.dalOrder.Update(o);//update the order in DO
            double priceTemp = 0;
            foreach (DO.OrderItem temp in dal.dalOrderItem.ReadAll())
            {
                if (temp.OrderID == o.ID)
                {
                    priceTemp += temp.Price;//add up all of prices in the order
                }
            }
            return new BO.Order
            {
                ID = orderId,
                CustomerAddress = oId.CustomerAddress,
                CustomerEmail = oId.CustomerEmail,
                CustomerName = oId.CustomerName,
                OrderDate = oId.OrderDate,
                ShipDate = DateTime.Now,
                Status = GetStatus(o),
                TotalPrice = priceTemp,
                DeliveryDate = DateTime.MinValue,
            };//new BO Order
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
