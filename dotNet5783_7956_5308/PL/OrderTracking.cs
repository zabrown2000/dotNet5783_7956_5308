using System;
using BO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    internal class OrderTrackings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private int id;
        public int ID
        {
            get
            { return id; }
            set
            {
                id = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ID"));
                }
            }
        }

        private Enums.OrderStatus status;
        public Enums.OrderStatus Status
        {
            get
            { return status; }
            set
            {
                status = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Status"));
                }
            }
        }


        private List<Tuple<DateTime?, string>>? tracking;
        public List<Tuple<DateTime?, string>>? Tracking
        {
            get
            { return tracking; }
            set
            {
                tracking = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Tracking"));
                }
            }
        }
    }
}
