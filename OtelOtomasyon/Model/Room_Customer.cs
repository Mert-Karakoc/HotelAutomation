using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Model
{
    internal class Room_Customer
    {
        public int ID { get; set; }
        public int RoomID { get; set; }
        public int CustomerID { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public int RentedDay { get; set; }
        public int PersonNumber { get; set; }
        public double PricePerDay { get; set; }
        public double PriceSum { get; set; }

    }
}
