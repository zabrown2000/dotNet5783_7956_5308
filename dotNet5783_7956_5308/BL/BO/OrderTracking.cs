

namespace BO;

public class OrderTracking
{
    /// <summary>
    /// unique identifier of the order tracking
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// status of order
    /// </summary>
    public Enums.OrderStatus Status { get; set; }

    public override String ToString() => this.ToStringProperty();
}
