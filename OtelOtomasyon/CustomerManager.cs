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
    internal class CustomerManager
    {
        public void Add(Customer customer)
        {    
            using (SqlConnection conn = new SqlConnection(DBHelper.strCon))
            {
                string strCmd = "sp_MusteriEkle";
                SqlCommand cmd = new SqlCommand(strCmd, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@tc", customer.Tc);
                cmd.Parameters.AddWithValue("@ad", customer.Name);
                cmd.Parameters.AddWithValue("@soyad", customer.Surname);
                cmd.Parameters.AddWithValue("@telno", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.ExecuteNonQuery();
            }
        }
        public Customer Search(int customertc)
        {
            Customer customer = new Customer();
            using (SqlConnection conn = new SqlConnection(DBHelper.strCon))
            {
                conn.Open();
                string strCmd = "SELECT * FROM Musteri WHERE MusteriTc = @musteritc";
                SqlCommand cmd = new SqlCommand(strCmd, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@musteritc", customertc);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                if (reader.HasRows)
                {
                    customer.CustomerID = (int)reader[0];
                    customer.Tc = (string)reader[1];
                    customer.Name = (string)reader[2];
                    customer.Surname = (string)reader[3];
                    customer.PhoneNumber = (string)reader[4];
                    customer.Email = (string)reader[5];
                }
            }
            return customer;
        }
        public void Update(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.strCon))
            {
                conn.Open();
                string strCmd = "UPDATE Musteri SET MusteriTc = @tc, MusteriAd = @ad, MusteriSoyad = @soyad, MusteriTelNo = @tel, MusteriEmail = @mail WHERE MusteriID = @id";
                SqlCommand cmd = new SqlCommand(strCmd, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", customer.CustomerID);
                cmd.Parameters.AddWithValue("@tc", customer.Tc);
                cmd.Parameters.AddWithValue("@ad", customer.Name);
                cmd.Parameters.AddWithValue("@soyad", customer.Surname);
                cmd.Parameters.AddWithValue("@tel", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@mail", customer.Email);
                
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(Customer customer)
        {
            DialogResult dr = MessageBox.Show("Silmek İstiyor Musunuz?", "SİL", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.strCon))
                {
                    conn.Open();
                    string strCmd = "DELETE  FROM Musteri WHERE MusteriID = @id";
                    SqlCommand cmd = new SqlCommand(strCmd, conn);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", customer.CustomerID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        DataTable dt = new DataTable();
        public DataTable GetList()
        {
            string strConn = DBHelper.strCon;
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Musteri", strConn);

            da.Fill(dt);
            return dt;
        }
    }
}
