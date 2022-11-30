﻿using Dal;
using DO;
using System;
using static DO.Enums;


namespace Test;

internal class DalTesting
{
    static void Main(String[] args)
    {
        Order order = new();
        OrderItem orderItem = new();
        Products product = new();
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
                    //product menu
                    action = HelperFunctions.PrintMenu("product");
                    //new switch based on submenu
                    break;
                case Enums.EntityType.Orders:
                    //order menu
                    action = HelperFunctions.PrintMenu("order");
                    //new switch based on submenu
                    break;
                case Enums.EntityType.OrderItems:
                    //orderitems menu
                    action = HelperFunctions.PrintMenu("orderItem");
                    //new switch based on submenu

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
            //print below with var enitity, read input, cast it, return it
            /*Console.WriteLine("Please select one of the actions below by clicking the appropiate number:\n" +
                        "1. Add product to the products list\n" +
                        "2. Delete product from the products list\n" +
                        "3. Update product\n" +
                        "4. Display product by its ID\n" +
                        "5. Display all the products\n");*/
            return 0;
        }
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
