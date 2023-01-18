using BlApi;
using Dal;
using BO;
namespace BlImplementation;

internal class Cart : ICart
{
    static DalApi.IDal? dal = DalApi.Factory.Get();

    /// <summary>
    /// Method to add product to cart
    /// </summary>
    /// <param name="myCart">current user's cart</param>
    /// <param name="pID">id of product to add to cart</param>
    /// <returns>updated cart</returns>
    /// <exception cref="BO.OutOfStockException">exception if product user wants to add is out of stock</exception>
    /// <exception cref="BO.BOEntityDoesNotExistException"></exception>
    public BO.Cart AddToCart(BO.Cart myCart, int pID)
    {
        int index = myCart.Items.FindIndex(x => x != null && x.ProductID == pID); //getting index of the product in the cart (if it's there)
        DO.Products? product = new DO.Products?();//creating new DO product
        try
        {
            product = dal?.dalProduct.ReadId(pID); //getting the actual product for corresponding id user wants to add
        }
        catch
        {
            throw new BO.BOEntityDoesNotExistException("Product does not exist\n");
        }
        
        if (product?.InStock < 1)
        {
            throw new BO.OutOfStockException("Product out of stock");
        }
        if (index != -1) //index wasn't -1 so exists in cart
        {
            myCart.Items[index]!.Amount++; //adding another product to the cart
            double unitPrice = product!.Value.Price; //getting price of just one of that product
            myCart.Items[index]!.Price += unitPrice; //adding to the total price of order item
            myCart.TotalPrice += unitPrice;//updating cart price
            return myCart;
        }
        //product not in cart yet
        BO.OrderItem oi = new BO.OrderItem //creating new orderitem to contain the product to add 
        {
            ID = pID,
            Price = (double)product!.Value.Price,
            Amount = 1,
            ProductID = product.Value.ID
        };
        myCart.Items.Add(oi); //adding the orderitem to cart
        myCart.TotalPrice += oi.Price; //updating price of the cart
        return myCart;

    }
    /// <summary>
    /// Method to update the cart for a specific product
    /// </summary>
    /// <param name="myCart">current user's cart</param>
    /// <param name="pID">id of product to change in cart</param>
    /// <param name="amount"></param>
    /// <returns>updated cart</returns>
    /// <exception cref="BO.BOEntityDoesNotExistException"></exception>
    public BO.Cart UpdateCart(BO.Cart myCart, int pID, int amount)
    {
        int index = myCart.Items.FindIndex(x => x.ProductID == pID); //getting index of the product in the cart (if it's there)
        DO.Products? product = new DO.Products(); //create a new DO product
        try
        {
            product = dal?.dalProduct.ReadId(pID); //getting product based on id
        }catch
        {
            throw new BO.BOEntityDoesNotExistException("Product does not exist\n");
        }
        if (index != -1) //product in cart already
        {
            if (myCart.Items[index]!.Amount + amount <= 0) //want to remove item
            {
                BO.OrderItem oi = myCart.Items[index]!; //save the orderitem with id
                myCart.TotalPrice -= myCart.Items[index]!.Price;
                myCart.Items.Remove(oi); //remove orderItem from cart
                return myCart;
            }
            product = dal!.dalProduct.ReadId(myCart.Items[index]!.ProductID); //get product from the orderItem in Items to check stock
            if (myCart.Items[index]!.Amount + amount > product?.InStock)      //if amount to add is more than available stock throw exception
            {
                throw new BO.OutOfStockException("Amount entered is more than amount in stock.");
            }
            else
            {
                double unitPrice = (double)product?.Price!;
                myCart.Items[index]!.Amount += amount;//set new amount (adds or subtracts)
                myCart.Items[index]!.Price += unitPrice * amount; // add/subtract to make the price reflect addition/subtraction of items to cart 
                myCart.TotalPrice += unitPrice * amount;//add/subtract the new total cart price
                return myCart;
            }
            
        }
        throw new BO.BOEntityDoesNotExistException("Product does not exist in cart\n");
    }
    /// <summary>
    /// Method to make checkout cart and make new order
    /// </summary>
    /// <param name="myCart">current user's cart</param>
    /// <param name="CustomerName">name of customer</param>
    /// <param name="CustomerEmail">email of customer</param>
    /// <param name="CustomerAddress">address of customer</param>
    /// <exception cref="BO.InvalidInputException"></exception>
    /// <exception cref="BO.BOEntityDoesNotExistException"></exception>
    public void MakeOrder(BO.Cart myCart, string CustomerName, string CustomerEmail, string CustomerAddress)      //edit comments to be what was before
    {
        if (myCart.CustomerName == "" || myCart.CustomerEmail == "" || myCart.CustomerAddress == "")//check input
        {
            throw new BO.InvalidInputException("Incorrect Input");
        }
        IEnumerable<DO.Products?> productList = dal?.dalProduct.ReadAll()!;//get all products from dal
        IEnumerable<string> checkOrderItem = from BO.OrderItem item in myCart.Items!
                                             let product = productList.FirstOrDefault(x => x?.ID == item.ID)
                                             where item.Amount < 1 || product?.InStock < item.Amount
                                             select "Product ID: " + item.ProductID + " is not in stock\n";//check if all of the products in cart are in stock
        if (checkOrderItem.Any()) //if no products are available 
            throw new BO.BOEntityDoesNotExistException("The product requested does not exist\n");

        int? orderId = dal?.dalOrder.Add(new DO.Order()
        {
            CustomerAddress = myCart.CustomerAddress!,
            CustomerEmail = myCart.CustomerEmail!,
            CustomerName = myCart.CustomerName!,
            OrderDate = DateTime.Now
        });//add a new order for the cart and get order ID
        try
        {
            myCart.Items!.ForEach(x => dal?.dalOrderItem.Add(new DO.OrderItem()
            {
                Amount = (int)x?.Amount!,
                ID = x.ID,
                OrderID = (int)orderId!,
                Price = x.Price,
                ProductID = x.ProductID
            }));//go over cart order items and add each to dal
        }
        catch (DO.Exceptions ex)
        {
            throw new BO.Exceptions(ex.Message);
        }
        catch (DO.EntityDoesNotExistException ex)
        {
            throw new BO.Exceptions(ex.Message);
        }
        
        IEnumerable<DO.Products?> products;
        try
        {
            products = from item in myCart.Items
                       select dal?.dalProduct.ReadId(item.ProductID);//list of products in cart
        }
        catch (DO.EntityDoesNotExistException)
        {
            throw new BO.BOEntityDoesNotExistException("The product requested does not exist\n");
        }

        DO.OrderItem oi = new();//create order item
        foreach (BO.OrderItem? item in myCart.Items!) //go over orderItems in the cart
        {
            try
            {
                if (item!.ProductID == dal?.dalProduct.ReadId(item.ProductID).ID && item.Amount > 0 && item.Amount <= dal?.dalProduct.ReadId(item.ProductID).InStock)//if orderItem exists and is instock
                {
                    DO.Order order = new DO.Order();//new DO order
                    order.OrderDate = DateTime.Now;//ordered now
                    int num;
                    try
                    {
                        num = (int)(dal?.dalOrder.Add(order)!);//add to DO orderlist and get order id
                    }
                    catch (DO.EntityDoesNotExistException)
                    {
                        throw new BO.BOEntityDoesNotExistException("The product requested does not exist\n");
                    }
                    oi.ProductID = item.ProductID;//save product id
                    oi.OrderID = num;//save order id
                    try
                    {
                        dal?.dalOrderItem.Add(oi);//add to DO order item list 
                    }
                    catch (DO.EntityDoesNotExistException)
                    {
                        throw new BO.BOEntityDoesNotExistException("The product requested does not exist");
                    }
                    DO.Products p;
                    try
                    {
                        p = (DO.Products)(dal?.dalProduct.ReadId(oi.ProductID)!);//get matching product
                    }
                    catch (DO.EntityDoesNotExistException)
                    {
                        throw new BO.BOEntityDoesNotExistException("The product requested does not exist\n");
                    }
                    p.InStock -= item.Amount;//subtract the amount of products in stock
                    try
                    {
                        dal?.dalProduct.Update(p);//update product in DO
                    }
                    catch (DO.EntityDoesNotExistException)
                    {
                        throw new BO.BOEntityDoesNotExistException("The product requested does not exist\n");
                    }
                }
            }
            catch (DO.EntityDoesNotExistException)
            {
                throw new BO.BOEntityDoesNotExistException("The product requested does not exist\n");
            }
            catch
            {
                throw new BO.Exceptions("Cannot place order");
            }
            //
        }
        //return (int)orderId!;

        /*if (CustomerName == "" || CustomerEmail == "" || CustomerAddress == "") //validating input
        {
            throw new BO.InvalidInputException("Incorrect Input");
        }
        myCart.CustomerAddress = CustomerAddress;
        myCart.CustomerEmail = CustomerEmail;
        myCart.CustomerName = CustomerName;
        foreach (BO.OrderItem? item in myCart.Items) //go over orderItems in the cart and make sure all info is correct
        {
            try
            {
                if (item.ProductID == dal?.dalProduct.ReadId(item.ProductID).ID && item.Amount > 0 && item.Amount <= dal.dalProduct.ReadId(item.ProductID).InStock) //if orderItem exists and is instock
                {
                    DO.Order order = new DO.Order(); //new DO order
                    order.OrderDate = DateTime.Now; //ordered now
                    int num = dal.dalOrder.Add(order); //add to DO orderlist and get order id
                    DO.OrderItem oi = new(); //creating new DO order item
                    oi.ProductID = item.ProductID; //save product id
                    oi.OrderID = num; //save order id
                    dal?.dalOrderItem.Add(oi); //add to DO order item list 
                    DO.Products p = (DO.Products)dal?.dalProduct.ReadId(oi.ProductID); //get matching product
                    p.InStock -= item.Amount; //subtract the amount of products in stock
                    dal?.dalProduct.Update(p); //update product in DO
                }
            } catch
            {
                throw new BO.BOEntityDoesNotExistException("Cannot place order\n");

            }
        }*/
        Console.WriteLine(myCart);  
        //resetting cart values
        myCart.Items.Clear(); 
        myCart.TotalPrice = 0;
        myCart.CustomerAddress = "";
        myCart.CustomerEmail = "";
        myCart.CustomerName = "";
    }
}

