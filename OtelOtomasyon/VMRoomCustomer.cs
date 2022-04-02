using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon
{
    internal class VMRoomCustomer
    {
        public int Capacity { get; set; }
        public string BedType { get; set; }
        public int BedCount { get; set; }
        public string RoomType { get; set; }
        public int RoomID { get; set; }
        public double Price { get; set; }
        public int RoomNumber { get; set; }
        public bool Occupied { get; set; }
        public string Tc { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public int RentedDay { get; set; }
        public int PersonCount { get; set; }
        public double PricePerDay { get; set; }
        public double PriceSum { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
