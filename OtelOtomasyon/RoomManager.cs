using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace OtelOtomasyon
{
    internal class RoomManager
    {
        public void Add(Room room)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.strCon))
            {            
                string strCmd = "INSERT INTO Oda VALUES (@kapasite, @yatakturu, @yataksayisi, @anlikfiyat, @dolu, @odatipi, @odano)";
                SqlCommand cmd = new SqlCommand(strCmd,conn);
                conn.Open();
                cmd.Parameters.Clear();      
                cmd.Parameters.AddWithValue("@kapasite", room.Capacity);
                cmd.Parameters.AddWithValue("@yatakturu", room.BedType);
                cmd.Parameters.AddWithValue("@yataksayisi", room.BedCount);
                cmd.Parameters.AddWithValue("@anlikfiyat", room.Price);
                cmd.Parameters.AddWithValue("@dolu", room.Occupied);
                cmd.Parameters.AddWithValue("odatipi", room.RoomType);             
                cmd.Parameters.AddWithValue("@odano", room.RoomNumber);
                cmd.ExecuteNonQuery();   
            }     
        }

        public Room Search(int roomNo)
        {
            Room room = new Room();
            using (SqlConnection conn = new SqlConnection(DBHelper.strCon))
            {
                conn.Open();
                string strCmd = "SELECT * FROM Oda WHERE OdaNo = @odano";
                SqlCommand cmd = new SqlCommand(strCmd, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@odano", roomNo);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                if (reader.HasRows)
                {
                    room.RoomID = (int)reader[0];
                    room.Capacity = (byte)reader[1];
                    room.BedType = (string)reader[2];
                    room.BedCount = (byte)reader[3];
                    room.Price = Convert.ToDouble(reader[4]);
                    room.Occupied = (bool)reader[5];
                    room.RoomType = (string)reader[6];
                    room.RoomNumber = (Int16)reader[7];
                }
            }
            return room;
        }
        public void Update(Room room)
        {   
            using (SqlConnection conn = new SqlConnection(DBHelper.strCon))
            {           
                conn.Open();            
                string strCmd = "UPDATE Oda SET Kapasite = @kapasite, YatakTuru = @yatakturu, YatakSayisi = @yataksayisi, AnlıkFiyat = @anlikfiyat, Dolu = @dolu, OdaTipi = @odatipi, @OdaNo = @odano WHERE OdaID = @odaid";
                SqlCommand cmd = new SqlCommand(strCmd, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@odaid", room.RoomID);
                cmd.Parameters.AddWithValue("@kapasite", room.Capacity);
                cmd.Parameters.AddWithValue("@yatakturu", room.BedType);
                cmd.Parameters.AddWithValue("@yataksayisi", room.BedCount);
                cmd.Parameters.AddWithValue("@anlikfiyat", room.Price);
                cmd.Parameters.AddWithValue("@dolu", room.Occupied);
                cmd.Parameters.AddWithValue("odatipi", room.RoomType);
                cmd.Parameters.AddWithValue("@odano", room.RoomNumber);              
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(Room room)
        {
            DialogResult dr = MessageBox.Show("Silmek İstiyor Musunuz?", "SİL", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {   
                using (SqlConnection conn = new SqlConnection(DBHelper.strCon))
                {
                    conn.Open();
                    string strCmd = "DELETE  FROM Oda WHERE OdaID = @odaid";
                    SqlCommand cmd = new SqlCommand(strCmd, conn);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@odaid", room.RoomID);               
                    cmd.ExecuteNonQuery();
                }
            }
        }      
        public void Leave(Room room)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.strCon))
            {
                conn.Open();
                string strCmd = "UPDATE Oda SET Dolu = @dolu WHERE OdaId = @odaid";
                SqlCommand cmd = new SqlCommand(strCmd, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@dolu", room.Occupied);
                cmd.Parameters.AddWithValue("@odaid", room.RoomID);
                cmd.ExecuteNonQuery();
            }
        }
        DataTable dt = new DataTable();
        public DataTable GetList()
        {
            Form1 form = new Form1();
            string strConn = DBHelper.strCon;
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Oda", strConn);

            da.Fill(dt);
            return dt;
        }
    }
}
