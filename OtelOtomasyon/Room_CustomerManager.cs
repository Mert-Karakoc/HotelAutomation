using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon
{
    internal class Room_CustomerManager
    {
        public void Update(Customer customer, Room_Customer roomcustomer, Room room)
        {
            CustomerManager customerManager = new CustomerManager();
            customerManager.Update(customer);
            
            using (SqlConnection conn = new SqlConnection(DBHelper.strCon))
            {
                conn.Open();
                string strCmd = "SELECT * FROM Musteri WHERE MusteriTc = @musteritc";
                SqlCommand cmds = new SqlCommand(strCmd, conn);
                cmds.Parameters.Clear();
                cmds.Parameters.AddWithValue("@musteritc", customer.Tc);
                SqlDataReader reader = cmds.ExecuteReader();
                reader.Read();

                if (reader.HasRows)
                {
                    customer.CustomerID = (int)reader[0];
                }
                string strCmd2 = "UPDATE Oda_Musteri SET KisiSayisi = @kisisayisi, GirisTarihi = @giristarihi, Cikistarihi = @cikistarihi, KalinanGun = @kalinangun, GunlukFiyat = @gunlukfiyat, ToplamFiyat = @toplamfiyat WHERE Odaid = odaid";
                SqlCommand cmda = new SqlCommand(strCmd2, conn);
                conn.Open();
                cmda.Parameters.Clear();
                cmda.Parameters.AddWithValue("@musteriId", customer.CustomerID);
                cmda.Parameters.AddWithValue("@kisisayisi", roomcustomer.PersonCount);
                cmda.Parameters.AddWithValue("@giristarihi", roomcustomer.CheckInTime);
                cmda.Parameters.AddWithValue("@cikistarihi", roomcustomer.CheckOutTime);
                cmda.Parameters.AddWithValue("@kalinangun", roomcustomer.RentedDay);
                cmda.Parameters.AddWithValue("@gunlukfiyat", roomcustomer.PricePerDay);
                cmda.Parameters.AddWithValue("@toplamfiyat", roomcustomer.PriceSum);
 
                cmda.ExecuteNonQuery();
            }
        }
        public void Add(Customer customer, Room_Customer roomcustomer, Room room)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.strCon))
            {
                conn.Open();
                string strCmd = "SELECT * FROM Musteri WHERE MusteriTc = @musteritc";
                string strCmd2 = "INSERT INTO Oda_Musteri VALUES (@odaid, @musteriId, @kisisayisi, @giristarihi, @cikistarihi, @kalinangun, @gunlukfiyat, @toplamfiyat, @islemtarihi)";
                SqlCommand cmds = new SqlCommand(strCmd, conn) ;
                cmds.Parameters.Clear();
                cmds.Parameters.AddWithValue("@musteritc", customer.Tc);
                SqlDataReader reader = cmds.ExecuteReader();
                reader.Read();

                if (reader.HasRows)
                {
                    customer.CustomerID = (int)reader[0];
                }
                reader.Close();
                SqlCommand cmda = new SqlCommand(strCmd2, conn);              
                cmda.Parameters.Clear();
                cmda.Parameters.AddWithValue("@odaid", roomcustomer.RoomID);
                cmda.Parameters.AddWithValue("@musteriId", customer.CustomerID);
                cmda.Parameters.AddWithValue("@kisisayisi", roomcustomer.PersonCount);
                cmda.Parameters.AddWithValue("@giristarihi", roomcustomer.CheckInTime);
                cmda.Parameters.AddWithValue("@cikistarihi", roomcustomer.CheckOutTime);
                cmda.Parameters.AddWithValue("@kalinangun", roomcustomer.RentedDay);
                cmda.Parameters.AddWithValue("@gunlukfiyat", roomcustomer.PricePerDay);
                cmda.Parameters.AddWithValue("@toplamfiyat", roomcustomer.PriceSum);
                cmda.Parameters.AddWithValue("@islemtarihi", roomcustomer.RegisterDate);
                cmda.ExecuteNonQuery();
            }
        }
        public List<VMRoomCustomer> Search(int roomNo)
        {
            
            List<VMRoomCustomer> list = new List<VMRoomCustomer>();
            using (SqlConnection conn = new SqlConnection(DBHelper.strCon))
            {
                conn.Open();
                string strCmd = "IF (SELECT Oda_Musteri.GirisTarihi  FROM Oda_Musteri ) != null AND (SELECT Dolu FROM Oda) != 0  BEGIN SELECT Oda.OdaID, Oda.Kapasite, Oda.YatakTuru, Oda.YatakSayisi, Oda.AnlıkFiyat, Oda.Dolu, Oda.OdaTipi, Oda.OdaNo, Musteri.MusteriTc, Musteri.MusteriAd, Musteri.MusteriSoyad, Musteri.MusteriTelNo, Musteri.MusteriEmail, Oda_Musteri.KisiSayisi, Oda_Musteri.GirisTarihi, Oda_Musteri.CikisTarihi, 			  Oda_Musteri.KalinanGun, Oda_Musteri.GunlukFiyat, Oda_Musteri.ToplamFiyat, Oda_Musteri.İşlemTarihi              FROM Musteri RIGHT OUTER JOIN Oda_Musteri ON Musteri.MusteriID = Oda_Musteri.MusteriID RIGHT OUTER JOIN Oda ON Oda_Musteri.OdaID = Oda.OdaID WHERE OdaNo = 106 AND DATEDIFF(DD, Oda_Musteri.GirisTarihi, GETDATE()) > 0 AND DATEDIFF(DD, GETDATE(), Oda_Musteri.GirisTarihi) < 0 And Oda.Dolu = 1 END ELSE BEGIN SELECT Oda.OdaID, Oda.Kapasite, Oda.YatakTuru, Oda.YatakSayisi, Oda.AnlıkFiyat, Oda.Dolu, Oda.OdaTipi, Oda.OdaNo, Musteri.MusteriTc, Musteri.MusteriAd, Musteri.MusteriSoyad, Musteri.MusteriTelNo, Musteri.MusteriEmail,		  Oda_Musteri.KisiSayisi, Oda_Musteri.GirisTarihi, Oda_Musteri.CikisTarihi, Oda_Musteri.KalinanGun, Oda_Musteri.GunlukFiyat, Oda_Musteri.ToplamFiyat, Oda_Musteri.İşlemTarihi FROM Musteri RIGHT OUTER JOIN Oda_Musteri ON Musteri.MusteriID = Oda_Musteri.MusteriID RIGHT OUTER JOIN Oda ON Oda_Musteri.OdaID = Oda.OdaID WHERE OdaNo = @roomNumber END";

                SqlCommand cmd = new SqlCommand(strCmd, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@roomNumber", roomNo);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    VMRoomCustomer vmRoomCustomer = new VMRoomCustomer();
                    vmRoomCustomer.RoomID = Convert.ToInt32(reader[0]);
                    vmRoomCustomer.Capacity = (byte)reader[1];
                    vmRoomCustomer.BedType = reader[2].ToString();
                    vmRoomCustomer.BedCount = (byte)reader[3];
                    vmRoomCustomer.Price = Convert.ToDouble(reader[4]);
                    vmRoomCustomer.Occupied = (bool)reader[5];
                    vmRoomCustomer.RoomType = reader[6].ToString();
                    vmRoomCustomer.RoomNumber = (Int16)reader[7];
                    vmRoomCustomer.Tc = reader[8].ToString();
                    if (vmRoomCustomer.Tc != "")
                    {
                        vmRoomCustomer.Name = reader[9].ToString();
                        vmRoomCustomer.Surname = reader[10].ToString();
                        vmRoomCustomer.PhoneNumber = reader[11].ToString();
                        vmRoomCustomer.Email = reader[12].ToString();
                        vmRoomCustomer.PersonCount = (byte)reader[13];
                        vmRoomCustomer.CheckInTime = (DateTime)reader[14];
                        vmRoomCustomer.CheckOutTime = (DateTime)reader[15];
                        vmRoomCustomer.RentedDay = (Int16)reader[16];
                        vmRoomCustomer.PricePerDay = Convert.ToDouble(reader[17]);
                        vmRoomCustomer.PriceSum = Convert.ToDouble(reader[18]);
                        vmRoomCustomer.RegisterDate = (DateTime)reader[19];
                    }
                    list.Add(vmRoomCustomer);           
                }       
            }         
            return list;
        }
        DataTable dt = new DataTable();
        public DataTable GetList()
        {
            string strConn = DBHelper.strCon;
            SqlDataAdapter da = new SqlDataAdapter("SELECT Musteri.*, Oda.*, Oda_Musteri.* FROM Musteri INNER JOIN             Oda_Musteri ON Musteri.MusteriID = Oda_Musteri.MusteriID INNER JOIN Oda ON Oda_Musteri.OdaID = Oda.OdaID", strConn);

            da.Fill(dt);
            return dt;
        }
    }
}
