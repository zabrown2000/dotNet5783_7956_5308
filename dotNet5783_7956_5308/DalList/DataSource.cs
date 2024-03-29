﻿using DO;
              
namespace Dal;

static internal class DataSource
{
    /*-------Class Fields-------*/
    /// <summary>
    /// Readonly static field for generating random numbers
    /// </summary>
    static internal readonly Random randNumGen = new Random(1);

    /*Note to Grader: Prof. Kelman said we can go straight to lists instead of doing the array now and list later*/

    /// <summary>
    /// List to hold the products
    /// </summary>
    static internal List<Products?> _productList = new List<Products?> { }; //total of 50 products

    /// <summary>
    ///List to hold the orders
    /// </summary>
    static internal List<Order?> _orderList = new List<Order?> { }; //total of 100 orders

    /// <summary>
    /// List to hold the orderItems
    /// </summary>
    static internal List<OrderItem?> _orderItemList = new List<OrderItem?> { }; //total of 200 order-items

    

    /*--------Class Methods--------*/
    /// <summary>
    /// Default constructor
    /// </summary>
    static DataSource() { s_Initialize(); } 

    /// <summary>
    /// Method to fill up the data entities with initial values
    /// </summary>
    static private void s_Initialize()
    {
        //calling functions to initialize each entity
        PushProducts();
        PushOrders();
        PushOrderItems();
    }

    /// <summary>
    /// Initializing our products list with the first 10 products
    /// </summary>
    static private void PushProducts()
    {
        //Setting up the initial 10 products in our store
        String[] NameOfApplicance = { "Kitchen Aid Mixer", "Ninja Blender", "Masimo Oven", "LG Fridge", "Samsung Freezer", "Electra Stove", "GoldLine Kettle" }; //The names of our products
        
        for (int i = 0; i < 10; i++)
        {
            _productList.Add( new() //creating a new product and setting the values to go in our list
            {   //using random generator to fill most of the fields
                //ID = Config.NextProductNumber,
                Price = randNumGen.Next(50, 3000),
                Name = NameOfApplicance[randNumGen.Next(NameOfApplicance.Length)],
                Category = (Enums.Categories)randNumGen.Next(1, 8),
                InStock = (i < 3) ? 0 : randNumGen.Next(19, 48) //hardcoding first 5% of products to have 0 stock
            });
        }
    }

    /// <summary>
    /// Initializing our orders list with the first 20 orders
    /// </summary>
    static private void PushOrders()
    {
        //Setting up initial 20 orders for our store
        String[] CustomerName = { "Adam", "Boris", "Cara", "David", "Edgar", "Franny", "Greg", "Hannah", "Iris", "Joey", "Kate", "Luke", "Morgan", "Nancy", "Oswald", "Peter", "Queeny", "Roberta", "Shira", "Tevye", "Uriel", "Violet", "Walter", "Xena", "Yuri", "Zahava"  };
        String[] CustomerEmail = {"aaa@mail.com", "bbb@mail.com", "ccc@mail.com", "ddd@mail.com", "eee@mail.com", "fff@mail.com", "ggg@mail.com", "hhh@mail.com",  "iii@mail.com", "jjj@mail.com", "kkk@mail.com", "lll@mail.com",
                                 "mmm@mail.com", "ooo@mail.com", "ppp@mail.com", "qqq@mail.com", "rrr@mail.com", "sss@mail.com","ttt@mail.com", "uuu@mail.com", "vvv@mail.com", "www@mail.com", "xxx@mail.com", "yyy@mail.com", "zzz@mail.com"};
        String[] CustomerAddress = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

        
        for (int i = 0; i < 20; i++)
        {
            Order myOrder = new() //creating a new order and setting the values to go in our list
            {   //using random generator to fill most of the fields
                CustomerName = CustomerName[randNumGen.Next(CustomerName.Length)],
                CustomerEmail = CustomerEmail[randNumGen.Next(CustomerEmail.Length)],
                CustomerAddress = CustomerAddress[randNumGen.Next(CustomerAddress.Length)],
                OrderDate = DateTime.Now - new TimeSpan(randNumGen.NextInt64(10L * 1000L * 3600L * 24L * 100L)), //using TimeSpan to add an interval chosen at random
                ShipDate = null,
                DeliveryDate = null
            };
            if (i < 4) //hardcoding the 20% of current orders to not have been shipped yet 
            {
               _orderList.Add(myOrder);
                
            }
           //we want the rest of them to have shipping
                myOrder.ShipDate = myOrder.OrderDate + new TimeSpan(randNumGen.NextInt64(10L * 1000L * 3600L * 24L * 100L)); //using TimeSpan to add an interval chosen at random

                if (i >= 4 && i < 10) //hardcoding 40% of current shipped items to not have a delivery date yet
                {
                    _orderList.Add(myOrder);
                }
            if (i >= 10)
            {
                //we want delivery date to apply only to 
                myOrder.DeliveryDate = myOrder.ShipDate + new TimeSpan(randNumGen.NextInt64(10L * 1000L * 3600L * 24L * 100L)); //using TimeSpan to add an interval chosen at random
                _orderList.Add(myOrder);
            }
        }
    }

    /// <summary>
    /// Initializing our orderItems list with the first 40 orderItems
    /// </summary>
    static private void PushOrderItems()
    {


        //Setting up initial 40 Order-Product pairs
        for (int i = 0; i < 40; i++)
        {
            Products? product = _productList[randNumGen.Next(_productList.Count)]; 
            _orderItemList.Add( new OrderItem //creating a new orderItem and setting the values to go in our list
            {   //using random generator to fill most of the fields
                ProductID = product.Value.ID,
                OrderID = randNumGen.Next(10, 10 + _orderList.Count), //order ids are all 2 digits
                Price = product.Value.Price,
                Amount = randNumGen.Next(5)
            });
        }
    }

}




