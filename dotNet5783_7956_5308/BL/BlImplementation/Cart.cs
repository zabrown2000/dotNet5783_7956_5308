using BlApi;
using DalApi;
using Dal;
using BO;
//ADD DOCUMENTATION
namespace BlImplementation;


internal class Cart : ICart
{
    static IDal? dal = new DalList();
    public BO.Cart AddToCart(BO.Cart myCart, int id)
    {
        int ind = myCart.Items.FindIndex(x => x != null && x.ID == id); //save index of order with ID in cart
        DO.Products? product = new DO.Products?();//create a DO product
        product = dal.dalProduct.ReadId(id);//get the matching product for the ID
        if (product?.InStock < 1)
        {
            throw new BO.OutOfStockException("Product unavailable");
        }
        if (ind != -1)//exists in cart
        {
            myCart.Items[ind].Amount++; //add another product to the cart
            myCart.Items[ind].Price += myCart.Items[ind].Price;//add to the total price 
            myCart.TotalPrice += myCart.Items[ind].Price;//update cart price
            return myCart;
        }
        BO.OrderItem oi = new BO.OrderItem//create new orderitem that is being added 
        {
            ID = id,
            Price = (double)product.Value.Price,
            Amount = 1,
            ProductID = product.Value.ID
        };
        myCart.Items.Add(oi);//add the orderitem to cart
        myCart.TotalPrice += oi.Price;//update price of the cart
        return myCart;

    }
    public BO.Cart UpdateCart(BO.Cart myCart, int id, int amount)
    {


        int ind = myCart.Items.FindIndex(x => x.ProductID == id); //save index of product with ID in cart
        DO.Products product = new DO.Products();//create a DO product
        try
        {
            product = dal.dalProduct.ReadId(id);//get the matching product for the ID
        }catch
        {
            throw new BO.BOEntityDoesNotExistException();
        }
        if (ind != -1)//if in cart
        {
            if (amount == 0)
            {
                BO.OrderItem temp = myCart.Items[ind];//save the orderitem with id
                myCart.Items.Remove(temp);//remove orderItem from cart
                myCart.TotalPrice -= myCart.Items[ind].Price;
                return myCart;
            }
            myCart.TotalPrice -= myCart.Items[ind].Price * myCart.Items[ind].Amount; //substract price of product from cart
            myCart.Items[ind].Amount = amount;//set new amount
            myCart.TotalPrice += myCart.Items[ind].Price * amount;//add the new price
            return myCart;
        }
        throw new BO.BOEntityDoesNotExistException();
    }
    public void MakeOrder(BO.Cart myCart, string CustomerName, string CustomerEmail, string CustomerAddress)
    {
        if (CustomerName == "" || CustomerEmail == "" || CustomerAddress == "")//check input
        {
            throw new BO.InvalidInputException("Incorrect Input");
        }
        DO.OrderItem oi = new();//create order item
        foreach (BO.OrderItem? item in myCart.Items)//go over orderItems in the cart
        {
            try
            {
                if (item.ProductID == dal.dalProduct.ReadId(item.ProductID).ID && item.Amount > 0 && item.Amount <= dal.dalProduct.ReadId(item.ProductID).InStock)//if orderItem exists and is instock
                {
                    DO.Order order = new DO.Order();//new DO order
                    order.OrderDate = DateTime.Now;//ordered now
                    int num = dal.dalOrder.Add(order);//add to DO orderlist and get order id
                    oi.ProductID = item.ProductID;//save product id
                    oi.OrderID = num;//save order id
                    dal.dalOrderItem.Add(oi);//add to DO order item list 
                    DO.Products p = dal.dalProduct.ReadId(oi.ProductID);//get matching product
                    p.InStock -= item.Amount;//subtract the amount of products in stock
                    dal.dalProduct.Update(p);//update product in DO
                }
            } catch
            {
                throw new BO.BOEntityDoesNotExistException("Cannot place order\n");

            }
        }

    }
}

