using DO;
              //NEED TO ADD COMMENTS - should the 10,20,40 be parameters? Maor's fns are hardcoded with these, he says they should be arguments?
namespace Dal;

static internal class DataSource
{
    /*-------Class Fields-------*/
    /// <summary>
    /// Readonly static field for generating random numbers
    /// </summary>
    static internal readonly Random randNumGen = new Random();

    /*Note to Grader: Prof. Kelman said we can go straight to lists instead of doing the array now and list later*/

    /// <summary>
    /// List to hold the products
    /// </summary>
    static internal List<Products> _productList = new List<Products> { }; //total of 50 products

    /// <summary>
    ///List to hold the orders
    /// </summary>
    static internal List<Order> _orderList = new List<Order> { }; //total of 100 orders

    /// <summary>
    /// List to hold the orderItems
    /// </summary>
    static internal List<OrderItem> _orderItemList = new List<OrderItem> { }; //total of 200 order-items

    /// <summary>
    /// Internal class to help us automate the creation of order, product, and orderItem IDs
    /// </summary>
    internal static class Config
    {
        //Order numbers setup
        internal const int s_startOrderNumber = 1000; //decided to make order numbers 4 digits
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => ++s_nextOrderNumber; } //each time the value is "get-ed" it will increment the value by 1
        //Product numbers setup
        internal const int s_startProductNumber = 100000; //needs to be a 6 digit number, so this is the minimum it can be
        public static int s_nextProductNumber = s_startProductNumber; 
        internal static int NextProductNumber { get => ++s_nextProductNumber; } //each time the value is "get-ed" it will increment the value by 1
        //OrderItem numbers setup
        internal const int s_startOrderItemNumber = 0;
        public static int s_nextOrderItemNumber = s_startOrderItemNumber;
        internal static int NextOrderItemNumber { get => ++s_nextOrderItemNumber; } //each time the value is "get-ed" it will increment the value by 1
    }

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
            _productList.Add( new() //creating a new product and setting the values to go in our array
            {
                ID = Config.NextProductNumber,
                Price = randNumGen.Next(50, 3000),
                Name = NameOfApplicance[randNumGen.Next(NameOfApplicance.Length)],
                Category = (Enums.Categories)randNumGen.Next(0, 8),
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
            Order myOrder = new() //creating a new order and setting the values to go in our array
            {
                ID = Config.NextOrderNumber,
                CustomerName = CustomerName[randNumGen.Next(CustomerName.Length)],
                CustomerEmail = CustomerEmail[randNumGen.Next(CustomerEmail.Length)],
                CustomerAddress = CustomerAddress[randNumGen.Next(CustomerAddress.Length)],
                OrderDate = DateTime.Now - new TimeSpan(randNumGen.NextInt64(10L * 1000L * 3600L * 24L * 100L)), //using TimeSpan to add an interval chosen at random
                ShipDate = DateTime.MinValue,
                DeliveryDate = DateTime.MinValue
            };
            if (i < 4) //hardcoding the 20% of current orders to not have been shipped yet 
            {
               _orderList.Add(myOrder);
                return;
            }
            myOrder.ShipDate = myOrder.OrderDate + new TimeSpan(randNumGen.NextInt64(10L * 1000L * 3600L * 24L * 100L)); //using TimeSpan to add an interval chosen at random
            
            if (i >= 4 && i < 10) //hardcoding 40% of current shipped items to not have a delivery date yet
            {
                _orderList.Add(myOrder);
                return;
            }
            myOrder.DeliveryDate = myOrder.ShipDate + new TimeSpan(randNumGen.NextInt64(10L * 1000L * 3600L * 24L * 100L)); //using TimeSpan to add an interval chosen at random
            _orderList.Add(myOrder); 
        }
    }

    /// <summary>
    /// Initializing our orderItems list with the first 40 orderItems
    /// </summary>
    static private void PushOrderItems()
    {


        //Setting up initial 40 order-product pairs
        for (int i = 0; i < 40; i++)
        {
            Products product = _productList[randNumGen.Next(_productList.Count)]; 
            _orderItemList.Add( new OrderItem //creating a new orderItem and setting the values to go in our array
            {
                ID = Config.NextOrderItemNumber,
                ProductID = product.ID,
                OrderID = randNumGen.Next(Config.s_startOrderNumber, Config.s_startOrderNumber + _orderList.Count), 
                Price = product.Price,
                Amount = randNumGen.Next(5)
            });
        }
    }

}




