

using System.ComponentModel;

namespace BO;
public class OrderTrackings

{

    public int ID { set; get; }

    public BO.Enums.OrderStatus Status { set; get; }

    public List<Tuple<DateTime?, string>>? Tracking { set; get; }
    public override string ToString() => this.ToStringProperty();

}

