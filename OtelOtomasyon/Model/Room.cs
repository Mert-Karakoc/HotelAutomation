using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Model
{
    public enum BedType { BirKisilik = 1, İkiKisilik = 2, Kralice = 3, Kral = 4, SuperKral = 5 };
    public enum RoomType { BirKisilik = 1, İkiKisilik = 2, ÜçKişilik = 3, DörtKişilik = 4, Suit = 5, Dubleks = 6, Aile = 7, KralDairesi = 8 };
    internal class Room
    {
        public int RoomID { get; set; }
        public int Capacity { get; set; }
        public BedType BedType { get; set; }
        public int BedCount { get; set; }
        public RoomType RoomType { get; set; }
        public double Price { get; set; }
    }
}
