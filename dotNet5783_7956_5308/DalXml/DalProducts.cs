namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class DalProducts : IProducts
{
    string productPath = @"Product.xml";
    string configPath = @"Config.xml";

    public int Add(DO.Products item)
    {
        XElement productRoot = XmlTools.LoadListFromXMLElement(productPath); //get all the elements from the file

        //check if the customer exists in th file
        var customerFromFile = (from prod in productRoot.Elements()
                                where (prod.Element("ID").Value == item.ID.ToString())
                                select prod).FirstOrDefault();

        //throw an exception
        if (customerFromFile != null)
            throw new DO.IDDoesNotExistException("the product already exists");

        //get running product ID number
        List<RunningNumber> runningList = XmlTools.LoadListFromXMLSerializer<RunningNumber>(configPath);

        var runningNum = (from number in runningList
                          where (number.typeOfnumber == "Product ID running number")
                          select number).FirstOrDefault();
        runningList.Remove(runningNum);//remove the saved number from list
        runningNum.numberSaved++;//add one to the saved number
        runningList.Add(runningNum);//add the number back to list
        int temp = (int)runningNum.numberSaved;//save the running number

        //add the customer to the root element
        productRoot.Add(
            new XElement("Product",
            //new XElement("ID", temp),
            new XElement("Name", item.Name),
            new XElement("Price", item.Price),
            new XElement("Category", item.Category),
            new XElement("InStock", item.InStock)));

        //save the root in the file
        XmlTools.SaveListToXMLElement(productRoot, productPath);
        return item.ID;
    }

    public void Delete(int id)
    {
        List<DO.Products?> productList = XmlTools.LoadListFromXMLSerializer<DO.Products?>(productPath);

        DO.Products temp = (from item in productList
                           where item != null && item?.ID == id
                           select (DO.Products)item).FirstOrDefault();

        if (temp.ID.Equals(default(Order)))
            throw new DO.IDDoesNotExistException("the product does not exist");

        productList.Remove(temp);

        XmlTools.SaveListToXMLSerializer(productList, productPath);
    }

    public IEnumerable<DO.Products?> ReadAll(Func<DO.Products?, bool>? filter = null)
    {
        List<DO.Products?> productList = XmlTools.LoadListFromXMLSerializer<DO.Products?>(productPath).ToList();

        return (from product in productList
                where filter(product)
                select product).ToList();
    }

    public DO.Products ReadByFilter(Func<DO.Products?, bool>? filter)
    {
        List<DO.Products?> productList = ReadAll().ToList();

        return (from item in productList
                where filter(item)
                select (DO.Products)item).FirstOrDefault();
    }

    public DO.Products ReadId(int id)
    {
        List<DO.Products?> productList = ReadAll().ToList();

        return (from item in productList
                where item != null && item?.ID == id
                select (DO.Products)item).FirstOrDefault();
        throw new DO.IDDoesNotExistException("the product requested does not exist");
    }

    public void Update(DO.Products item)
    {
        DO.Products? temp = ReadId(item.ID);//get the product requested to update 
        List<DO.Products?> productList = ReadAll().ToList();//get all product from ile
        productList.Remove(temp);//remove the existing product
        productList.Add(item);//add the updated product

        XmlTools.SaveListToXMLSerializer(productList, productPath);//save back into file    }
    }
}
