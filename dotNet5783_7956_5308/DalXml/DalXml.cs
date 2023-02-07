namespace Dal;
using DalApi;
using DO;
using System.Diagnostics;

sealed internal class DalXml : IDal
{
    #region singleton
    public static readonly IDal instance = new DalXml();
    public static IDal Instance { get => instance; }
    DalXml() { }
    static DalXml() { }
    #endregion
    public IProducts Product { get; } = new Dal.Products();
    public IOrder Order { get; } = new Dal.Order();
    public IOrderItem OrderItem { get; } = new Dal.OrderItem();
}