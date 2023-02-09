namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class DalOrderItem : IOrderItem
{
    string orderItemPath = @"OrderItem.xml";
    string configPath = @"Config.xml";
    public int Add(DO.OrderItem item)
    {
        XElement orderItemRoot = XmlTools.LoadListFromXMLElement(orderItemPath); //get all the elements from the file

        //check if the orderItem exists in th file
        var orderItemFromFile = (from oi in orderItemRoot.Elements()
                                 where (oi != null && oi.Element("ID")!.Value == item.ID.ToString())
                                 select oi).FirstOrDefault();

        //throw an exception
        if (orderItemFromFile != null)
            throw new DO.IDDoesNotExistException("the order item already exists");

        //get running order item ID number
        List<RunningNumber> runningList = XmlTools.LoadListFromXMLSerializer<RunningNumber>(configPath);

        var runningNum = (from number in runningList
                          where (number.typeOfnumber == "Order item running number")
                          select number).FirstOrDefault();
        runningList.Remove(runningNum);//remove the saved number from list
        runningNum.numberSaved++;//add one to the saved number
        runningList.Add(runningNum);//add the number back to list
        int temp = (int)runningNum.numberSaved;//save the running number



        //add the orderItem to the root element
        orderItemRoot.Add(
            new XElement("OrderItem",
            new XElement("ID", temp),
            new XElement("ProductID", item.ProductID),
            new XElement("OrderID", item.OrderID),
            new XElement("Price", item.Price),
            new XElement("Amount", item.Amount)));

        //save the root in the file
        XmlTools.SaveListToXMLElement(orderItemRoot, orderItemPath);
        return item.ID;
    }

    public void Delete(int id)
    {
        List<DO.OrderItem?> orderItemList = XmlTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemPath);

        DO.OrderItem temp = (from item in orderItemList
                             where item != null && item?.ID == id
                             select (DO.OrderItem)item).FirstOrDefault();

        if (temp.ID.Equals(default(Order)))
            throw new DO.IDDoesNotExistException("the order item does not exist");

        orderItemList.Remove(temp);

        XmlTools.SaveListToXMLSerializer(orderItemList, orderItemPath);
    }

    public IEnumerable<DO.OrderItem?> ReadAll(Func<DO.OrderItem?, bool>? filter = null)
    {
        List<DO.OrderItem?> orderItemList = XmlTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemPath).ToList();

        return (from order in orderItemList
                where filter(order)
                select order).ToList();
    }

    public DO.OrderItem ReadByFilter(Func<DO.OrderItem?, bool>? filter)
    {
        List<DO.OrderItem?> orderItemList = ReadAll().ToList();

        return (from item in orderItemList
                where filter(item)
                select (DO.OrderItem)item).FirstOrDefault();
    }

    public DO.OrderItem ReadId(int id)
    {
        List<DO.OrderItem?> orderItemList = ReadAll().ToList();

        return (from item in orderItemList
                where item != null && item?.ID == id
                select (DO.OrderItem)item).FirstOrDefault();
        throw new DO.IDDoesNotExistException("the Order Item requested does not exist");
    }

    public DO.OrderItem GetOrderItem(int orderId, int productId)
    {
        List<DO.OrderItem?> orderItemList = ReadAll().ToList();

        return (from item in orderItemList
                where item != null && item?.ProductID == productId && item?.OrderID == orderId
                select (DO.OrderItem)item).FirstOrDefault();
        throw new DO.IDDoesNotExistException("the Order Item requested does not exist");
    }

    public IEnumerable<DO.OrderItem?> OrdersInOrderItem(int orderId)
    {
        List<DO.OrderItem?> orderItemList = ReadAll().ToList();

        return (from item in orderItemList
                where item?.OrderID == orderId
                select item).ToList();
    }

    public void SetByOrderItem(DO.OrderItem item)
    {
        DO.OrderItem? temp = ReadId(item.ID);//get the order item requested to update 
        List<DO.OrderItem?> orderItemList = ReadAll().ToList();//get all order items from file
        orderItemList.Remove(temp);//remove the existing order item
        orderItemList.Add(item);//add the updated order item

        XmlTools.SaveListToXMLSerializer(orderItemList, orderItemPath);//save back into file    
    }
    
    public void SetByOrdProdID(OrderItem orderItem)
    {
        List<DO.OrderItem?> orderItemList = ReadAll().ToList();//get all order items from file

        int index = orderItemList.FindIndex(x => ((x?.ID == orderItem.ID) && (x?.ProductID == orderItem.ProductID)));

        if (index == -1) //if does not exist
            throw new EntityDoesNotExistException(new OrderItem());

        orderItemList[index] = orderItem; //updating orderItem using same place in memory

        XmlTools.SaveListToXMLSerializer(orderItemList, orderItemPath);//save back into file  
    }

    public void Update(OrderItem orderItem) { }
}
