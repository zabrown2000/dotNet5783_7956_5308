using Dal;
using DO;
using System;
using static DO.Enums;
using static Dal.DalOrder;
using static Dal.DalOrderItem;
using static Dal.DalProducts;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using static Test.DalTesting;


namespace Test;

internal class DalTesting
{
    static void Main(String[] args)
    {
        //datasource is internal, does it start automatically when run?
        
        bool stopFlag = true;
        while (stopFlag) //does it make sense to do it this way?
        {
            Console.WriteLine("Please select one of the options below by clicking the appropiate number:\n" +
                "0. Exit\n" +
                "1. Check out Products\n" +
                "2. Check out Orders\n" +
                "3. Check out OrderItems\n");

            int entityChoice;
            while (!System.Int32.TryParse(Console.ReadLine(), out entityChoice)) ;
            Enums.EntityType entity = (Enums.EntityType)entityChoice;

            Enums.ActionType action;
            switch (entity)
            {
                case Enums.EntityType.Exit :
                    stopFlag = false;
                    Console.WriteLine("Goodbye\n");
                    break;

                case Enums.EntityType.Products :      //make a function that send entiry and it prints menu with appropiate entity inside
                                                      //see slides on how to print with vars. Read input, cast it to enum, return it
                    action = HelperFunctions.PrintMenu("product");
                    switch (action)
                    {
                        case Enums.ActionType.Add:
                            //add fn
                            try
                            {
                                
                                int id = ProductFunctions.AddP(); //
                                Console.WriteLine("Your new product ID is: " + id + "\n");
                            } catch 
                            {
                                Console.WriteLine("Product already exists\n"); //error

                            }
                            break;
                        case Enums.ActionType.Delete:
                            //add fn
                            try
                            {
                                ProductFunctions.DeleteP(HelperFunctions.ReadIntUser("Enter product ID:\n"));
                            }
                            catch
                            {
                                Console.WriteLine("Product does not exist\n");
                            }
                            break;
                        case Enums.ActionType.Update:
                            //add fn
                            try
                            {
                                
                                ProductFunctions.UpdateP(); //inside wrapper, call update on product get info from, then print product we got and end
                            }
                            catch
                            {
                                Console.WriteLine("The product you wish to update does not exist\n");
                            }
                            break;
                        case Enums.ActionType.ReadId:
                            //add fn
                            try
                            {
                                Console.WriteLine(ProductFunctions.ReadIdP(HelperFunctions.ReadIntUser("Enter Product ID\n")));
                            }
                            catch
                            {
                                Console.WriteLine("Product does not exist\n");
                            }
                            break;
                        case Enums.ActionType.ReadAll:
                            //print items of list
                            ProductFunctions.ReadAllP(); //iterate through list and print
                            break;
                    }
                    break;

                case Enums.EntityType.Orders:
                    //order menu
                    action = HelperFunctions.PrintMenu("order");
                    switch (action)
                    {
                        case Enums.ActionType.Add:
                            //add fn
                            try
                            {
                                int id = OrderFunctions.AddO();
                                Console.WriteLine("your new order ID is: " + id + "\n");
                            }
                            catch
                            {
                                Console.WriteLine("Order already exists");
                            }
                            break;
                        case Enums.ActionType.Delete:
                            //add fn
                            try
                            {
                                OrderFunctions.DeleteO(HelperFunctions.ReadIntUser("Enter Order ID: "));
                            }
                            catch
                            {
                                Console.WriteLine("Order does not exist\n");
                            }
                            break;
                        case Enums.ActionType.Update:
                            //add fn
                            try
                            {
                                OrderFunctions.UpdateO();
                            }
                            catch
                            {
                                Console.WriteLine("The order you wish to update does not exist\n");
                            }
                            break;
                        case Enums.ActionType.ReadId:
                            //add fn
                            try
                            {
                                Console.WriteLine(OrderFunctions.ReadIdO(HelperFunctions.ReadIntUser("Enter Order ID\n")));
                            }
                            catch
                            {
                                Console.WriteLine("The order does not exist\n");
                            }
                            break;
                        case Enums.ActionType.ReadAll:
                            OrderFunctions.ReadAllO();
                            break;
                    }
                    break;

                case Enums.EntityType.OrderItems:
                    //orderitems menu
                    action = HelperFunctions.PrintMenu("orderItem");
                    switch (action)
                    {
                        case Enums.ActionType.Add:
                            //add fn
                            try
                            {
                                int id = OrderItemFunctions.AddOI();
                                Console.WriteLine("your new order-item ID is: " + id + "\n");
                            }
                            catch
                            {
                                Console.WriteLine("OrderItem already exists\n");
                            }
                            break;
                        case Enums.ActionType.Delete:
                            //add fn
                            try
                            {
                                OrderItemFunctions.DeleteOI(HelperFunctions.ReadIntUser("Enter OrderItem ID: "));
                            }
                            catch
                            {
                                Console.WriteLine("OrderItem does not exist\n");
                            }
                            break;
                        case Enums.ActionType.Update:
                            //add fn
                            try
                            {
                                OrderItemFunctions.UpdateOI();
                            }
                            catch
                            {
                                Console.WriteLine("The orderItem you wish to update does not exist\n");
                            }
                            break;
                        case Enums.ActionType.ReadId:
                            //add fn
                            try
                            {
                                Console.WriteLine(OrderItemFunctions.ReadIdOI(HelperFunctions.ReadIntUser("Enter OrderItem ID\n")));
                            }
                            catch
                            {
                                Console.WriteLine("The orderItem does not exist\n");
                            }
                            break;
                        case Enums.ActionType.ReadAll:
                            OrderItemFunctions.ReadAllOI();
                            break;
                    }
                    break;

                default:
                    break;
            }
        }
    }

    internal class HelperFunctions
    {
        static internal Enums.ActionType PrintMenu(String entity)
        {
            int actionChoice = 0;
            //print below with var enitity, read input, cast it, return it
            while (actionChoice <= 0 || actionChoice > 5)
            {
                Console.WriteLine("Please select one of the actions below by clicking the appropiate number:\n" +
                        "1. Add " + entity + " to the " + entity + "s list\n" +
                        "2. Delete " + entity + " from the " + entity + "s list\n" +
                        "3. Update " + entity + "\n" +
                        "4. Display " + entity + " by its ID\n" +
                        "5. Display all the " + entity + "s\n");

                while (!System.Int32.TryParse(Console.ReadLine(), out actionChoice)) ;
            }
            Enums.ActionType action = (Enums.ActionType)actionChoice;
            return action;
        }

        static internal int ReadIntUser(String output)
        {
            Console.WriteLine(output);
            int input;
            while (!System.Int32.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("ERROR format\n");//error
            }
            return input;
        }
    }

    internal class ProductFunctions
    {
        private static DalProducts _dalP = new DalProducts();
        private static Products product = new();

        static internal int AddP()
        {
            Console.WriteLine("Enter the product name:\n");
            product.Name = Console.ReadLine() ?? "";
            product.Price = HelperFunctions.ReadIntUser("Enter the product price:\n");
            product.Category = (Enums.Categories)HelperFunctions.ReadIntUser("Enter the product category:\n");
            product.InStock = HelperFunctions.ReadIntUser("Enter the product stock\n");

            return _dalP.Add(product);
        }

        static internal void DeleteP(int id)
        {
            _dalP.Delete(id);
        }

        static internal void UpdateP()
        {
            product.ID = HelperFunctions.ReadIntUser("Enter the product ID:\n");
            Console.WriteLine("Enter the product name:\n");
            product.Name = Console.ReadLine() ?? "";
            product.Price = HelperFunctions.ReadIntUser("Enter the product price:\n");
            product.Category = (Enums.Categories)HelperFunctions.ReadIntUser("Enter the product category:\n");
            product.InStock = HelperFunctions.ReadIntUser("Enter the product stock:\n");

            _dalP.Update(product);
            Console.WriteLine(product);

        }

        static internal Products ReadIdP(int id)
        {
            return _dalP.ReadId(id);
        }

        static internal void ReadAllP()
        {
            List<Products> products = _dalP.ReadAll(); 
            foreach (Products p in products)
            {
                Console.WriteLine(p);
                Console.WriteLine('\n');
            }//print the list
        }
    }

    internal class OrderFunctions
    {
        private static DalOrder _dalO = new DalOrder();
        private static Order order = new();

        static internal int AddO()
        {
            Console.WriteLine("enter customer name:\n");
            order.CustomerName = Console.ReadLine() ?? "";
            Console.WriteLine("enter customer mail:\n");
            order.CustomerEmail = Console.ReadLine() ?? "";
            Console.WriteLine("enter customer adress:\n");
            order.CustomerAddress = Console.ReadLine() ?? "";
            order.OrderDate = DateTime.Now;
            order.ShipDate = DateTime.MinValue;
            order.DeliveryDate = DateTime.MinValue;
            
            int id= _dalO.Add(order);
            return id;
        }

        static internal void DeleteO(int id)
        {
            _dalO.Delete(id);
        }

        static internal void UpdateO()
        {
            order.ID = HelperFunctions.ReadIntUser("enter order ID:\n");
            Console.WriteLine("enter customer name:\n");
            order.CustomerName = Console.ReadLine() ?? "";
            Console.WriteLine("enter customer mail:\n");
            order.CustomerEmail = Console.ReadLine() ?? "";
            Console.WriteLine("enter customer adress\n");
            order.CustomerAddress = Console.ReadLine() ?? "";
            order.OrderDate = DateTime.Now;
            order.ShipDate = DateTime.MinValue;
            order.DeliveryDate = DateTime.MinValue;

            _dalO.Update(order);
            Console.WriteLine(order);
        }

        static internal Order ReadIdO(int id)
        {
            return _dalO.ReadId(id);

        }

        static internal void ReadAllO()
        {
            List<Order> order = _dalO.ReadAll();
            foreach (Order o in order)
            {
                Console.WriteLine(o);
                Console.WriteLine('\n');
            }//print the list
        }
    }

    }

    internal class OrderItemFunctions
    {
        private static DalOrderItem _dalOI = new DalOrderItem();
        private static OrderItem orderItem = new();

        static internal int AddOI()
        {
        DalOrder dalOrder = new DalOrder();
        DalProducts dalProduct = new DalProducts();
        bool check = true;
        while (check)
        {
            try
            {
                orderItem.ProductID = HelperFunctions.ReadIntUser("enter product ID:\n");
                dalProduct.ReadId(orderItem.ProductID);
                check = false;//if not found
            }
            catch
            {
                Console.WriteLine("Could not find product ID, please enter new ID\n");
            }
        }

        check = true;//if found

        while (check)
        {
            try
            {
                orderItem.OrderID = HelperFunctions.ReadIntUser("enter order id:\n");
                dalOrder.ReadId(orderItem.OrderID);
                check = false;
            }
            catch
            {
                Console.WriteLine("Could not find order ID, please enter new ID\n");
            }
        }
        orderItem.Amount = HelperFunctions.ReadIntUser("enter amount:\n");
        orderItem.Price = dalProduct.ReadId(orderItem.ProductID).Price * orderItem.Amount;

        int id = _dalOI.Add(orderItem);
        return id;
        }

        static internal void DeleteOI(int id)
        {
        _dalOI.Delete(id);

    }

    static internal void UpdateOI()
        {
        DalOrder dalOrder = new();
        DalProducts dalProduct = new();
        DalOrderItem dalOrderItem = new();
        bool check = true;

        while (check)//if found
        {
            try
            {
                orderItem.ID = HelperFunctions.ReadIntUser("Enter orderItem ID\n");
                dalOrderItem.ReadId(orderItem.ID);
                check = false;//not found
            }
            catch
            {
                Console.WriteLine("Could not find orderItem ID, please enter new ID\n");
            }
        }

        check = true;

        while (check)
        {
            try
            {
                orderItem.ProductID = HelperFunctions.ReadIntUser("enter product ID:\n");
                dalProduct.ReadId(orderItem.ProductID);
                check = false;
            }
            catch
            {
                Console.WriteLine("Could not find product ID, please enter new ID\n");
            }
        }

        check = true;

        while (check)
        {
            try
            {
                orderItem.OrderID = HelperFunctions.ReadIntUser("enter order id:\n");
                dalOrder.ReadId(orderItem.OrderID);
                check = false;
            }
            catch
            {
                Console.WriteLine("Could not find product ID, please enter new ID\n");
            }
        }

        orderItem.Amount = HelperFunctions.ReadIntUser("enter amount:\n");
        orderItem.Price = dalProduct.ReadId(orderItem.ProductID).Price * orderItem.Amount;

        _dalOI.Update(orderItem);
        Console.WriteLine(orderItem);

    }

        static internal OrderItem ReadIdOI(int id)
        {
        return _dalOI.ReadId(id);

    }

    static internal void ReadAllOI()
    {
        List<OrderItem> orderItem = _dalOI.ReadAll();
        foreach (OrderItem oi in orderItem)
        {
            Console.WriteLine(oi);
            Console.WriteLine('\n');
        }//print the list

    }

    
}
/*The main program of stage1 should display a menu to the user. The menu options appear below.
Each team needs to analyze the entities and conclude the items (the levels) that are needed in each
option. Again, note: no logic should be implemented in the main program, as was the case in the
data entity methods.
The main menu will give the options for checking out every data entity. Choosing a specific entity will
open a sub-menu for performing entity operations. There should be a menu item for each
implemented method in the respective accessor class. For example:

0. Exit
1. Check out [entity-name-1]:
    a. Add an object to the entity list.
    b. Display and object using an object’s identifier.
    c. Display the object list.
    d. Update an object.
    e. Delete an object from the object list.
2. Check out [entity-name-2]:
…

Should an entity have methods outside CRUD their extra functionality may reflect in their respective
menu. It is mandatory to consider all the accessor methods of each class.
● The main program will be defined in project DalTest
● The main program’s classes should contain private fields for each of the accessor class
    ○ The fields should be initialized in the declaration, using an instantiated object of the respective class.
● The Main() method works as a polling loop. The menu is printed to the console and
    responds, by descending through the submenus, to input read from the console too.
● You are required to implement a separate method for each entity’s submenu that will contain:
    ○ Listing of the checks for the entity.
    ○ Accepts a user’s choice.
    ○ Accepts the relevant information for running the method test.
    ○ calls the requested testing method.
    ○ Should the tested method return a value - the value may be serialized and printed to the console.
    ○ An exception thrown by the tested method should be caught and its error message
     should be printed.
● Passing input to the create() methods: an object should be created using the (flat) input
from the user and passed as an object argument to create().
● Testing update() methods: receive an identifier, serialize and print the object that was located,
and then request information from the user for performing the update.
    ○ In the event that input is empty - no update should be performed.
● Printing values:
    ○ Entity objects will be serialized inside the ToString() method.
        ■ Do not call ToString() directly
    ○ An array should be printed using foreach.
● The program should not include any logic apart from the necessary input, data requests (method calling), and output.
*/
