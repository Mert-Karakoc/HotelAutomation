using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OtelOtomasyon.Model;

namespace OtelOtomasyon.Manager
{
    internal class RoomManager
    {
        public void Add(Room room)
        {           
            using (SqlConnection conn = new SqlConnection(DBHelper.Conn))
            {
                string strCmd = "INSERT INTO Oda VALUES (@kapasite, @yatakturu, @yataksayisi, @anlikfiyat, @dolu";
                SqlCommand cmd = new SqlCommand(strCmd);

               
                cmd.Parameters.AddWithValue("@kapasite", room.RoomID);

            }     
        }
    }
}
