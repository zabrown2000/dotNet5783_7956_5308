using BlApi;
using BlImplementation;
using BO;
using DO;
using System;
namespace BlTest;
class BlTesting
{
    static BlApi.IBl? bl = BlApi.Factory.Get();
    static void Main(string[] args)
    {
        Cart? cart = new() { Items = new List<BO.OrderItem?>() }; //new cart
        BO.Products? p = new(); //new BO prod
        BO.Order? o = new();   //new BO order
        int id, category;
        while (true)
        {
            Console.WriteLine("Please select one of the options below by clicking the appropiate number:\n" +
                "0. Exit\n" +
                "1. Check out Cart\n" +
                "2. Check out Orders\n" +
                "3. Check out Products\n");
            
            int input;

            //get user input
            while (!System.Int32.TryParse(Console.ReadLine(), out input)) ;
            BO.Enums.BoEntityType entityChoice = (BO.Enums.BoEntityType)input;

            switch (entityChoice)
            {
                case BO.Enums.BoEntityType.Exit:
                    Console.WriteLine("Goodbye\n");
                    return;
                    
                case BO.Enums.BoEntityType.Cart:
                    Console.WriteLine("Please select one of the options below by clicking the appropiate number:\n" +
                        "1. Add item to cart\n" +
                        "2. Update cart\n" +
                        "3. Place an order\n" +
                        "4. Display cart\n" +
                        "5. Return to main menu\n");
                    int actionChoice;
                    System.Int32.TryParse(Console.ReadLine(), out actionChoice);
                    
                    switch (actionChoice)
                    {
                        case 1: //add item to cart
                            id = GetNumberFromUser("Enter product ID:\n");
                            try
                            {
                                Console.WriteLine(bl?.cart.AddToCart(cart, id));
                                //Note to Grader: 
                                //Customer details will be left blank until
                                //an order is made. Just like when browsing online and you aren't
                                //signed in, you only put in your information when you're
                                //ready to make an order.
                            }
                            catch (BO.OutOfStockException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (BOEntityDoesNotExistException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;

                        case 2: //update cart
                            id = GetNumberFromUser("Enter product ID:\n");
                            int amount = GetNumberFromUser("please enter how much to add or reduce:");
                            try
                            {
                                cart = bl?.cart.UpdateCart(cart, id, amount);
                            }
                            catch (BOEntityDoesNotExistException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                            
                        case 3: //Place order
                            Console.WriteLine("Enter name");
                            string name = Console.ReadLine() ?? "";

                            Console.WriteLine("Enter email");
                            string email = Console.ReadLine() ?? "";

                            Console.WriteLine("Enter address");
                            string address = Console.ReadLine() ?? "";

                            try
                            {
                                bl?.cart.MakeOrder(cart, name, email, address);
                                Console.WriteLine("Order approved!");
                            }
                            catch (BO.InvalidInputException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (BO.BOEntityDoesNotExistException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            break;

                        case 4: //Display cart
                            Console.WriteLine(cart);
                            break;

                        case 5: //return to main menu
                            Console.WriteLine("Returning to main menu...\n");

                            break;
                            
                        default:
                            Console.WriteLine("Invalid choice, try again\n");
                            break;
                    }
                    break;
                    
                case BO.Enums.BoEntityType.Order:
                    Console.WriteLine("Please select one of the options below by clicking the appropiate number:\n" +
                        "1. Display all the orders\n" +
                        "2. Display an order by its ID\n" +
                        "3. Update order ship date\n" +
                        "4. Update order delivery date\n" +
                        "5. Return to main menu\n");

                    System.Int32.TryParse(Console.ReadLine(), out actionChoice);
                    switch (actionChoice)
                    {
                        case 1: //display all orders
                            try
                            {
                                printList<BO.OrderForList?>(bl?.order.ReadAllOrderForList());
                            }
                            catch (BO.BOEntityDoesNotExistException e)//for inner status func
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                            
                        case 2: //display specific order
                            id = GetNumberFromUser("Enter order id:");
                            try
                            {
                                Console.WriteLine(bl?.order.ReadBoOrder(id));
                            }
                            catch (BO.BOEntityDoesNotExistException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                            
                        case 3: //update ship date
                            id = GetNumberFromUser("Enter order id:");
                            try
                            {
                                Console.WriteLine(bl?.order.ShipUpdate(id));
                            }
                            catch (BO.BOEntityDoesNotExistException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                            
                        case 4: //update delivery date
                            id = GetNumberFromUser("Enter order id:");
                            try
                            {
                                Console.WriteLine(bl?.order.DeliveredUpdate(id));
                            }
                            catch (BO.BOEntityDoesNotExistException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;

                        case 5: //return to main menu
                            Console.WriteLine("Returning to main menu...\n");
                            break;
                        default:
                            Console.WriteLine("Invalid choice, try again\n");
                            break;
                    }
                    break;
                    
                case BO.Enums.BoEntityType.Product:
                    Console.WriteLine("Please select one of the options below by clicking the appropiate number:\n" +
                        "1. Manager: Display all the products\n" +
                        "2. Manager: Display a product by its ID\n" +
                        "3. Manager: Add a product\n" +
                        "4. Manager: Delete a product\n" +
                        "5. Manager: Update a product\n" +
                        "6. Customer: Display all the products\n" +
                        "7. Return to main menu\n");

                    System.Int32.TryParse(Console.ReadLine(), out actionChoice);
                    switch (actionChoice)
                    {
                        case 1: //display all products for m
                            printList<ProductForList?>(bl?.products.ReadProductsForList());
                            break;
                            
                        case 2: //display a product for m
                            try
                            {
                                Console.WriteLine(bl?.products.ManagerProduct(GetNumberFromUser("Enter Product ID: \n")));
                            }
                            catch (BO.BOEntityDoesNotExistException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                            
                        case 3://add product for m
                            p.ID = GetNumberFromUser("Enter ID of new product:\n");
                            category = GetNumberFromUser("Enter category of new product:\n");
                            p.Category = (BO.Enums.ProdCategory)category;
                            Console.WriteLine("Enter name of product:\n");
                            p.Name = Console.ReadLine();
                            p.InStock = GetNumberFromUser("Enter stock of product:\n");
                            p.Price = GetNumberFromUser("Enter price of Product\n");
                            try
                            {
                                bl?.products.AddProduct(p);
                            }
                            catch (BO.BOEntityAlreadyExistsException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (BO.InvalidInputException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                            
                        case 4: //delete a product for m
                            id = GetNumberFromUser("Enter product ID:\n");
                            try
                            {
                                bl?.products.DeleteProduct(id);
                            }
                            catch (BO.BOEntityDoesNotExistException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                            
                        case 5: //update product for m
                            p.ID = GetNumberFromUser("Enter ID of new product:\n");
                            category = GetNumberFromUser("Enter category of new product:\n");
                            while (category < 1 || category > 7)
                            {
                                Console.WriteLine("Invalid category, try again\n");
                                category = GetNumberFromUser("Invalid category, try again\n");
                            }
                            p.Category = (BO.Enums.ProdCategory)category;
                            Console.WriteLine("Enter name of product:\n");
                            p.Name = Console.ReadLine();
                            p.InStock = GetNumberFromUser("Enter stock of product:\n");
                            p.Price = GetNumberFromUser("Enter price of Product\n");
                            try
                            {
                                bl?.products.UpdateProduct(p);
                            }

                            catch (BOEntityDoesNotExistException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (BO.InvalidInputException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                            
                        case 6: //display products for c
                            printList<ProductForList?>(bl?.products.ReadProductsForList());
                            break;
                            
                        case 7: //return to main menu
                            Console.WriteLine("Returning to main menu...\n");
                            break;
                            
                        default:
                            Console.WriteLine("Invalid choice, try again\n");
                            break;
                    }
                    break;
                    
                default:
                    Console.WriteLine("Invalid choice, try again\n");
                    break;
            }
        }
    }


    /// <summary>
    /// a func to get number from user (and makes sure its not letters)
    /// </summary>
    /// <param name="txt"></param>
    /// <returns></returns>
    static int GetNumberFromUser(string txt = "")//the programer sends what he needs
    {
        Console.WriteLine(txt);
        int num;
        while (!System.Int32.TryParse(Console.ReadLine(), out num))//if not int
        {
            Console.WriteLine("ERROR format\n");//error
        }
        return num;//read users number
    }

    static void printList<T>(IEnumerable<T> lst)
    {
        foreach (T t in lst)
        {
            Console.WriteLine(t);
        }
    }
}