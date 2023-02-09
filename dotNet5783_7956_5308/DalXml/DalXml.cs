namespace Dal;
using DalApi;
using DO;
using System.Diagnostics;

public struct RunningNumber
{
    public double numberSaved { get; set; }
    public string typeOfnumber { get; set; }
}

sealed internal class DalXml : IDal
{
    //#region singleton
    //public static readonly IDal instance = new DalXml();
    //public static IDal Instance { get => instance; }
    //DalXml() { }
    //static DalXml() { }
    //#endregion

    private DalXml() { }
    public static IDal Instance { get; } = new DalXml();
    public IProducts dalProduct { get; } = new DalProducts();
    public IOrder dalOrder { get; } = new DalOrder();
    public IOrderItem dalOrderItem { get; } = new DalOrderItem();
}