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
        bool stopFlag = true; //have nested conditionals so need to know when to leave outer whileloop
        while (stopFlag) //will be changed to false if user chooses 0
        {
            Console.WriteLine("Please select one of the options below by clicking the appropiate number:\n" +
                "0. Exit\n" +
                "1. Check out Products\n" +
                "2. Check out Orders\n" +
                "3. Check out OrderItems\n");

            int entityChoice;
            while (!System.Int32.TryParse(Console.ReadLine(), out entityChoice)) ; //checking input to make sure its an int

            Enums.EntityType entity = (Enums.EntityType)entityChoice; //casting int to be corresponding enum type
            Enums.ActionType action;

            switch (entity) //outer menu handling based on entity types
            {
                case Enums.EntityType.Exit:
                    stopFlag = false;
                    Console.WriteLine("Goodbye\n");
                    break;

                case Enums.EntityType.Products:
                    action = HelperFunctions.PrintMenu("product"); //printing menu for products
                    switch (action) //inner menu for product cases
                    {
                        case Enums.ActionType.Add:
                            try
                            {

                                int id = ProductFunctions.AddP(); //calling wrapper function for adding a product
                                Console.WriteLine("Your new product ID is: " + id + "\n");
                            }
                            catch
                            {
                                Console.WriteLine("Product already exists\n"); //error
                            }
                            break;

                        case Enums.ActionType.Delete:
                            try
                            {
                                ProductFunctions.DeleteP(HelperFunctions.ReadIntUser("Enter product ID:\n")); //printing and getting input from user and sending it to wrapper delete function
                            }
                            catch
                            {
                                Console.WriteLine("Product does not exist\n");
                            }
                            break;

                        case Enums.ActionType.Update:
                            try
                            {

                                ProductFunctions.UpdateP(); //calling wrapper function for updating a product
                            }
                            catch
                            {
                                Console.WriteLine("The product you wish to update does not exist\n");
                            }
                            break;

                        case Enums.ActionType.ReadId:
                            try
                            {
                                Console.WriteLine(ProductFunctions.ReadIdP(HelperFunctions.ReadIntUser("Enter Product ID\n"))); //printing and getting input from user and sending it to wrapper readId function
                            }
                            catch
                            {
                                Console.WriteLine("Product does not exist\n");
                            }
                            break;

                        case Enums.ActionType.ReadAll:
                            ProductFunctions.ReadAllP(); //calling wrapper function for reading a product list
                            break;
                    }
                    break;

                case Enums.EntityType.Orders:
                    action = HelperFunctions.PrintMenu("order"); //printing menu for orders
                    switch (action) //inner menu for order cases
                    {
                        case Enums.ActionType.Add:
                            try
                            {
                                int id = OrderFunctions.AddO(); //calling wrapper function for adding an order
                                Console.WriteLine("your new order ID is: " + id + "\n");
                            }
                            catch
                            {
                                Console.WriteLine("Order already exists");
                            }
                            break;

                        case Enums.ActionType.Delete:
                            try
                            {
                                OrderFunctions.DeleteO(HelperFunctions.ReadIntUser("Enter Order ID: ")); //printing and getting input from user and sending it to wrapper delete function
                            }
                            catch
                            {
                                Console.WriteLine("Order does not exist\n");
                            }
                            break;

                        case Enums.ActionType.Update:
                            try
                            {
                                OrderFunctions.UpdateO(); //calling wrapper function for updating an order
                            }
                            catch
                            {
                                Console.WriteLine("The order you wish to update does not exist\n");
                            }
                            break;

                        case Enums.ActionType.ReadId:
                            try
                            {
                                Console.WriteLine(OrderFunctions.ReadIdO(HelperFunctions.ReadIntUser("Enter Order ID\n"))); //printing and getting input from user and sending it to wrapper readId function
                            }
                            catch
                            {
                                Console.WriteLine("The order does not exist\n");
                            }
                            break;

                        case Enums.ActionType.ReadAll:
                            OrderFunctions.ReadAllO(); //calling wrapper function for reading an order list
                            break;
                    }
                    break;

                case Enums.EntityType.OrderItems:
                    action = HelperFunctions.PrintMenu("orderItem"); //printing menu for orderItems
                    switch (action) //inner menu for orderItem cases
                    {
                        case Enums.ActionType.Add:
                            try
                            {
                                int id = OrderItemFunctions.AddOI(); //calling wrapper function for adding an orderItem
                                Console.WriteLine("your new orderItem ID is: " + id + "\n");
                            }
                            catch
                            {
                                Console.WriteLine("OrderItem already exists\n");
                            }
                            break;

                        case Enums.ActionType.Delete:
                            try
                            {
                                OrderItemFunctions.DeleteOI(HelperFunctions.ReadIntUser("Enter OrderItem ID: ")); //printing and getting input from user and sending it to wrapper delete function
                            }
                            catch
                            {
                                Console.WriteLine("OrderItem does not exist\n");
                            }
                            break;

                        case Enums.ActionType.Update:
                            try
                            {
                                //Ask if they want to choose an orderItem to update using a product and order Id or just order Id and then bring to the correct menu and call those functions
                                int num = HelperFunctions.ReadIntUser("Enter 1 to set orderItem from order ID and 2 to set OrderItem from order ID and product ID\n");
                                if (num == 1)
                                {
                                    OrderItemFunctions.SetByOrderItem();
                                }
                                else if (num == 2)
                                {
                                    OrderItemFunctions.SetByOrdProdID();
                                }
                                else
                                {
                                    throw new Exception("invalid integer entered\n");
                                }
                            }
                            catch
                            {
                                Console.WriteLine("The orderItem does not exist\n");
                            }
                            break;


                        case Enums.ActionType.ReadId:
                            try
                            {
                                //Ask if they want to choose an orderItem to get using a product and order Id or just order Id and then bring to the correct menu and call those functions
                                int num = HelperFunctions.ReadIntUser("Enter 1 to get orderItem list from order ID and 2 to get OrderItem from order ID and product ID\n");
                                if (num == 1)
                                {
                                    OrderItemFunctions.ReadOrderID();
                                }
                                else if (num == 2)
                                {
                                    Console.WriteLine(OrderItemFunctions.ReadOrdProdID());
                                }
                                else
                                {
                                    throw new Exception("invalid integer entered\n");
                                }
                            }
                            catch
                            {
                                Console.WriteLine("The orderItem does not exist\n");
                            }
                            break;

                        case Enums.ActionType.ReadAll:
                            OrderItemFunctions.ReadAllOI(); //calling wrapper function for reading an orderItem list
                            break;
                    }
                    break;

                default: //if user didn't add correct menu option, will break and do another iteration (flag is still true)
                    break;
            }
        }
    }

    /// <summary>
    /// Internal class that contains general helper functions for main program
    /// </summary>
    internal class HelperFunctions
    {
        /// <summary>
        /// Function to print a menu given a string
        /// </summary>
        /// <param name="entity">which entity's menu will be displayed</param>
        /// <returns>menu choice from user</returns>
        static internal Enums.ActionType PrintMenu(String entity)
        {
            int actionChoice = 0;

            while (actionChoice <= 0 || actionChoice > 5) //checking user entered correct menu item
            {
                Console.WriteLine("Please select one of the actions below by clicking the appropiate number:\n" +
                        "1. Add " + entity + " to the " + entity + "s list\n" +
                        "2. Delete " + entity + " from the " + entity + "s list\n" +
                        "3. Update " + entity + "\n" +
                        "4. Display " + entity + " by its ID\n" +
                        "5. Display all the " + entity + "s\n");

                while (!System.Int32.TryParse(Console.ReadLine(), out actionChoice)) ; //checking user entered an int
            }
            Enums.ActionType action = (Enums.ActionType)actionChoice; //casting input to correct enum type
            return action;
        }

        /// <summary>
        /// Function to print a string to the user and read in an int
        /// </summary>
        /// <param name="output">string to display to user</param>
        /// <returns>int user returned</returns>
        static internal int ReadIntUser(String output)
        {
            Console.WriteLine(output);
            int input;
            while (!System.Int32.TryParse(Console.ReadLine(), out input)) //keep going until user prints an int
            {
                Console.WriteLine("ERROR format! Please enter an integer\n"); //error
            }
            return input;
        }
    }

    /// <summary>
    /// Internal class that contains helper functions dealing with Product actions
    /// </summary>
    internal class ProductFunctions
    {
        /// <summary>
        /// field to access product CRUD methods
        /// </summary>
        private static DalProducts _dalP = new DalProducts();
        /// <summary>
        /// Product will use for below functions (instead of creating new one each time)
        /// </summary>
        private static Products product = new();

        /// <summary>
        /// Wrapper function for CRUD add function
        /// </summary>
        /// <returns>id of product added</returns>
        static internal int AddP()
        {
            Console.WriteLine("Enter the product name:\n");
            product.Name = Console.ReadLine() ?? "";
            product.Price = HelperFunctions.ReadIntUser("Enter the product price:\n");
            product.Category = (Enums.Categories)HelperFunctions.ReadIntUser("Enter the product category (1-7):\n");
            product.InStock = HelperFunctions.ReadIntUser("Enter the product stock\n");

            return _dalP.Add(product); //calling CRUD add
        }

        /// <summary>
        /// Wrapper function for CRUD delete function
        /// </summary>
        /// <param name="id">id of product to delete</param>
        static internal void DeleteP(int id)
        {
            _dalP.Delete(id); //calling CRUD delete
        }

        /// <summary>
        /// Wrapper function for CRUD update function
        /// </summary>
        static internal void UpdateP()
        {
            product.ID = HelperFunctions.ReadIntUser("Enter the product ID:\n");
            Console.WriteLine("Enter the product name:\n");
            product.Name = Console.ReadLine() ?? "";
            product.Price = HelperFunctions.ReadIntUser("Enter the product price:\n");
            product.Category = (Enums.Categories)HelperFunctions.ReadIntUser("Enter the product category (1-7):\n");
            product.InStock = HelperFunctions.ReadIntUser("Enter the product stock:\n");

            _dalP.Update(product); //calling CRUD update
            Console.WriteLine(product); //update was successful, print new product

        }

        /// <summary>
        /// Wrapper function for CRUD readId function
        /// </summary>
        /// <param name="id">id of product to read</param>
        /// <returns>product to read</returns>
        static internal Products ReadIdP(int id)
        {
            return _dalP.ReadId(id); //calling CRUD readId
        }

        /// <summary>
        /// Wrapper function for CRUD readAll function
        /// </summary>
        static internal void ReadAllP()
        {
            List<Products> products = _dalP.ReadAll(); //calling CRUD readAll
            foreach (Products p in products) //printing the list
            {
                Console.WriteLine(p);
                Console.WriteLine('\n');
            }
        }
    }

    /// <summary>
    /// Internal class that contains helper functions dealing with Order actions
    /// </summary>
    internal class OrderFunctions
    {
        /// <summary>
        /// field to access order CRUD methods
        /// </summary>
        private static DalOrder _dalO = new DalOrder();
        /// <summary>
        /// Order will use for below functions (instead of creating new one each time)
        /// </summary>
        private static Order order = new();

        /// <summary>
        /// Wrapper function for CRUD add function
        /// </summary>
        /// <returns>id of order added</returns>
        static internal int AddO()
        {
            Console.WriteLine("enter customer name:\n");
            order.CustomerName = Console.ReadLine() ?? "";
            Console.WriteLine("enter customer mail:\n");
            order.CustomerEmail = Console.ReadLine() ?? "";
            Console.WriteLine("enter customer address:\n");
            order.CustomerAddress = Console.ReadLine() ?? "";
            order.OrderDate = DateTime.Now;
            order.ShipDate = DateTime.MinValue;
            order.DeliveryDate = DateTime.MinValue;

            int id = _dalO.Add(order); //calling CRUD add
            return id;
        }

        /// <summary>
        /// Wrapper function for CRUD delete function
        /// </summary>
        /// <param name="id">id of order to delete</param>
        static internal void DeleteO(int id)
        {
            _dalO.Delete(id); //calling CRUD delete
        }

        /// <summary>
        /// Wrapper function for CRUD update function
        /// </summary>
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

            _dalO.Update(order); //calling CRUD update
            Console.WriteLine(order); //update was successful, print new product
        }

        /// <summary>
        /// Wrapper function for CRUD readId function
        /// </summary>
        /// <param name="id">id of order to read</param>
        /// <returns>order want to read</returns>
        static internal Order ReadIdO(int id)
        {
            return _dalO.ReadId(id); //calling CRUD readId

        }

        /// <summary>
        /// Wrapper function for CRUD readAll function
        /// </summary>
        static internal void ReadAllO()
        {
            List<Order> order = _dalO.ReadAll(); //calling CRUD readAll
            foreach (Order o in order) //printing the list
            {
                Console.WriteLine(o);
                Console.WriteLine('\n');
            }
        }
    }

    /// <summary>
    /// Internal class that contains helper functions dealing with OrderItems actions
    /// </summary>
    internal class OrderItemFunctions
    {
        /// <summary>
        /// field to access order CRUD methods
        /// </summary>
        private static DalOrderItem _dalOI = new DalOrderItem();
        /// <summary>
        /// OrderItem will use for below functions (instead of creating new one each time)
        /// </summary>
        private static OrderItem orderItem = new();

        /// <summary>
        /// Wrapper function for CRUD add function
        /// </summary>
        /// <returns>id of orderItem added</returns>
        static internal int AddOI()
        {
            DalOrder dalOrder = new DalOrder();
            DalProducts dalProduct = new DalProducts();
            bool check = true;
            //need to check product id to go in orderItem exists
            while (check)
            {
                try
                {
                    orderItem.ProductID = HelperFunctions.ReadIntUser("enter product ID:\n");
                    dalProduct.ReadId(orderItem.ProductID);
                    check = false; //if not found
                }
                catch
                {
                    Console.WriteLine("Could not find product ID, please enter new ID\n");
                }
            }

            check = true; //if found

            //need to check order id to go in orderItem exists
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

            int id = _dalOI.Add(orderItem); //calling CRUD add
            return id;
        }

        /// <summary>
        /// Wrapper function for CRUD delete function
        /// </summary>
        /// <param name="id">id of orderItem to be deleted</param>
        static internal void DeleteOI(int id)
        {
            _dalOI.Delete(id); //calling CRUD delete

        }

        /// <summary>
        /// Wrapper function for CRUD update/SetByOrdProdId function based on order Id and product Id
        /// </summary>
        static internal void SetByOrdProdID()
        {
            DalOrder dalOrder = new();
            DalProducts dalProduct = new();
            DalOrderItem dalOrderItem = new();
            bool check = true;

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
                    Console.WriteLine("Could not find order ID, please enter new ID\n");
                }
            }
            orderItem.Amount = HelperFunctions.ReadIntUser("enter new amount:\n");
            orderItem.Price = dalProduct.ReadId(orderItem.ProductID).Price * orderItem.Amount;

            _dalOI.SetByOrdProdID(orderItem);
            Console.WriteLine(orderItem);
        }
        /// <summary>
        /// wrapper function for CRUD update/setByOrderItem function based on just orderItem id
        /// </summary>
        static internal void SetByOrderItem()
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
            /*
            //we don't care about order or product number here. we only want to know about the orderItem id so remove these lines
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
                    Console.WriteLine("Could not find order ID, please enter new ID\n");
                }
            }
            */
            orderItem.Amount = HelperFunctions.ReadIntUser("enter new amount:\n");
            orderItem.Price = dalProduct.ReadId(orderItem.ProductID).Price * orderItem.Amount;

            _dalOI.SetByOrderItem(orderItem);
            Console.WriteLine(orderItem);

        }


        /// <summary>
        /// Wrapper function for CRUD GetOrderItem function 
        /// </summary>
        /// <param name="id">id of orderItem to read</param>
        /// <returns>orderItem to read</returns>
        static internal OrderItem ReadOrdProdID()
        {
           
            int ordNum = HelperFunctions.ReadIntUser("Enter the order ID:\n");
            int prodNum = HelperFunctions.ReadIntUser("Enter the product ID:\n");
            return _dalOI.GetOrderItem(ordNum, prodNum);
           
        }
        /// <summary>
        /// Wrapper function for CRUD OrdersInOrderItem function to return a list of orderItems with a given order Id
        /// </summary>
        static internal void ReadOrderID()
        {
            
            int num = HelperFunctions.ReadIntUser("Enter the order ID:\n");
            List<OrderItem> orderItem = _dalOI.OrdersInOrderItem(num); 
            foreach (OrderItem oi in orderItem) //printing the list
            {
                Console.WriteLine(oi);
                Console.WriteLine('\n');
            }
        }

        /// <summary>
        /// Wrapper function for CRUD readAll function
        /// </summary>
        static internal void ReadAllOI()
        {
            List<OrderItem> orderItem = _dalOI.ReadAll(); //calling CRUD readAll
            foreach (OrderItem oi in orderItem) //printing the list
            {
                Console.WriteLine(oi);
                Console.WriteLine('\n');
            }
        }
    }
}
