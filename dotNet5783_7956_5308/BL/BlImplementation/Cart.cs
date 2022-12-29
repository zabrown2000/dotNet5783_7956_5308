﻿using BlApi;
using DalApi;
using Dal;
using BO;
namespace BlImplementation;

internal class Cart : ICart
{
    static IDal? dal = new DalList();
    
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
            product = dal.dalProduct.ReadId(pID); //getting the actual product for corresponding id user wants to add
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
            myCart.Items[index].Amount++; //adding another product to the cart
            double unitPrice = product.Value.Price; //getting price of just one of that product
            myCart.Items[index].Price += unitPrice; //adding to the total price of order item
            myCart.TotalPrice += unitPrice;//updating cart price
            return myCart;
        }
        //product not in cart yet
        BO.OrderItem oi = new BO.OrderItem //creating new orderitem to contain the product to add 
        {
            ID = pID,
            Price = (double)product.Value.Price,
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
        DO.Products product = new DO.Products(); //create a new DO product
        try
        {
            product = dal.dalProduct.ReadId(pID); //getting product based on id
        }catch
        {
            throw new BO.BOEntityDoesNotExistException("Product does not exist\n");
        }
        if (index != -1) //product in cart already
        {
            if (myCart.Items[index].Amount + amount <= 0) //want to remove item
            {
                BO.OrderItem oi = myCart.Items[index]; //save the orderitem with id
                myCart.TotalPrice -= myCart.Items[index].Price;
                myCart.Items.Remove(oi); //remove orderItem from cart
                return myCart;
            }
            product = dal.dalProduct.ReadId(myCart.Items[index].ProductID); //get product from the orderItem in Items to check stock
            if (myCart.Items[index].Amount + amount > product.InStock)      //if amount to add is more than available stock throw exception
            {
                throw new BO.OutOfStockException("Amount entered is more than amount in stock.");
            }
            else
            {
                double unitPrice = product.Price;
                myCart.Items[index].Amount += amount;//set new amount (adds or subtracts)
                myCart.Items[index].Price += unitPrice * amount; // add/subtract to make the price reflect addition/subtraction of items to cart 
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
    public void MakeOrder(BO.Cart myCart, string CustomerName, string CustomerEmail, string CustomerAddress)
    {
        if (CustomerName == "" || CustomerEmail == "" || CustomerAddress == "") //validating input
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
                if (item.ProductID == dal.dalProduct.ReadId(item.ProductID).ID && item.Amount > 0 && item.Amount <= dal.dalProduct.ReadId(item.ProductID).InStock) //if orderItem exists and is instock
                {
                    DO.Order order = new DO.Order(); //new DO order
                    order.OrderDate = DateTime.Now; //ordered now
                    int num = dal.dalOrder.Add(order); //add to DO orderlist and get order id
                    DO.OrderItem oi = new(); //creating new DO order item
                    oi.ProductID = item.ProductID; //save product id
                    oi.OrderID = num; //save order id
                    dal.dalOrderItem.Add(oi); //add to DO order item list 
                    DO.Products p = dal.dalProduct.ReadId(oi.ProductID); //get matching product
                    p.InStock -= item.Amount; //subtract the amount of products in stock
                    dal.dalProduct.Update(p); //update product in DO
                }
            } catch
            {
                throw new BO.BOEntityDoesNotExistException("Cannot place order\n");

            }
        }
        Console.WriteLine(myCart);  
        //resetting cart values
        myCart.Items.Clear(); 
        myCart.TotalPrice = 0;
        myCart.CustomerAddress = "";
        myCart.CustomerEmail = "";
        myCart.CustomerName = "";
    }
}

