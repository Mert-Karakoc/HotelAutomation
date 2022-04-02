using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon
{
    internal class Room
    {
        public int RoomID { get; set; }
        public int Capacity { get; set; }
        public string BedType { get; set; }
        public int BedCount { get; set; }
        public string RoomType { get; set; }
        public double Price { get; set; }
        public int RoomNumber { get; set; }
        public bool Occupied { get; set; }
    }
}
