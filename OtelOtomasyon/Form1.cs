using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace OtelOtomasyon
{
    public partial class Form1 : Form
    {       

        public Form1()
        {
            InitializeComponent();     
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {     
            Room room = new Room();
            RoomManager roomManager = new RoomManager();
            
            room.RoomNumber = int.Parse(txtRoomNumber.Text);
            room.Capacity = int.Parse(txtCapacity.Text);
            room.BedType = cmbBedType.SelectedItem.ToString();
            room.RoomType = cmbRoomType.SelectedItem.ToString();
            room.BedCount = int.Parse(txtBedCount.Text);
            room.Price = Convert.ToDouble(txtPrice.Text);
            room.Occupied = chkOccupied.Checked;

            roomManager.Add(room);
            Button button = new Button();
            Image image = Image.FromFile(@"Icon\1.png");
            
            button.Width = 40;
            button.Height = 40;
            button.Image = image;
            button.Text = room.RoomNumber.ToString();
            button.Name = room.RoomNumber.ToString();
            button.Click += Button_Click;

            flwPnlRooms.Controls.Add(button);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            gridCustomer.Rows.Clear();
            gridCustomer.Enabled = true;
            Room_CustomerManager room_CustomerManager = new Room_CustomerManager();
            List<VMRoomCustomer> list = room_CustomerManager.Search(int.Parse(((Button)sender).Text));

            for (int i = 0; i < list.Count; i++)
            {
                gridCustomer.Rows.Add();
                lblC.Text = list[i].Capacity.ToString();
                lblBT.Text = list[i].BedType.ToString();
                lblBC.Text = list[i].BedCount.ToString();
                lblP.Text = list[i].Price.ToString();
                chkOcu.Checked = list[i].Occupied;
                txtRoomId.Text = list[i].RoomID.ToString();
                lblRT.Text = list[i].RoomType.ToString();
                lblRN.Text = list[i].RoomNumber.ToString();
                txtDailyPrice.Text = list[i].Price.ToString();
                if (list[i].Tc != "" && list[i].Occupied == true)
                {
                    txtDailyPrice.Text = list[i].Price.ToString();
                    txtCusNumber.Text = list[i].PersonCount.ToString();
                    dtpChkInDate.Value = list[i].CheckInTime;
                    dtpChkOutDate.Value = list[i].CheckOutTime;
                    dtpRegisterDate.Value = list[i].RegisterDate;
                    
                    txtPriceSum.Text = list[i].PriceSum.ToString();
                    gridCustomer.Rows[i].Cells[0].Value = list[i].Tc.ToString();
                    gridCustomer.Rows[i].Cells[1].Value = list[i].Name.ToString();
                    gridCustomer.Rows[i].Cells[2].Value = list[i].Surname.ToString();
                    gridCustomer.Rows[i].Cells[3].Value = list[i].PhoneNumber.ToString();
                    gridCustomer.Rows[i].Cells[4].Value = list[i].Email.ToString();
                }
            }
        }                 

        private void Form1_Load(object sender, EventArgs e)
        {           
            RoomManager roomManager = new RoomManager();
            List<string> bedType = new List<string>();
            bedType.Add("Bir Kişilik");
            bedType.Add("İki Kişilik");
            bedType.Add("Kraliçe");
            bedType.Add("Kral");
            bedType.Add("Süper Kral");
            List<string> roomType = new List<string>();
            roomType.Add("Bir Kişilik");
            roomType.Add("iki Kişilik");
            roomType.Add("Üç Kişilik");
            roomType.Add("Dört Kişilik");
            roomType.Add("Suit");
            roomType.Add("Dubleks");
            roomType.Add("Aile");
            roomType.Add("Kral Dairesi");

            cmbBedType.DataSource = bedType;
            cmbRoomType.DataSource = roomType;
            
            Search("SELECT OdaId, Kapasite, YatakTuru, YatakSayisi, AnlıkFiyat, Dolu, OdaTipi, OdaNo FROM Oda");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Room room = new Room();
            RoomManager roomManager = new RoomManager();
            room.RoomID = int.Parse(txtRoomId.Text);
            room.RoomNumber = int.Parse(txtRoomNumber.Text);
            room.Capacity = int.Parse(txtCapacity.Text);
            room.BedType = cmbBedType.SelectedItem.ToString();
            room.RoomType = cmbRoomType.SelectedItem.ToString();
            room.BedCount = int.Parse(txtBedCount.Text);
            room.Price = Convert.ToDouble(txtPrice.Text);
            room.Occupied = chkOccupied.Checked;
            roomManager.Update(room);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Room room = new Room();
            RoomManager roomManager = new RoomManager();
            room.RoomID = int.Parse(txtRoomId.Text);
            roomManager.Delete(room);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RoomManager roomManager = new RoomManager();
            Room room = new Room();
            DBHelper dBHelper = new DBHelper();
            room = roomManager.Search(int.Parse(txtRoomNumber.Text));
            
            txtRoomId.Text = room.RoomID.ToString();
            txtCapacity.Text = room.Capacity.ToString();
            cmbBedType.SelectedItem = room.BedType.ToString();
            txtBedCount.Text = room.BedCount.ToString();
            txtPrice.Text = room.Price.ToString();
            chkOccupied.Checked = room.Occupied;
            cmbRoomType.SelectedItem = room.RoomType.ToString();
            txtRoomNumber.Text = room.RoomNumber.ToString();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Room room = new Room();
            Customer customer = new Customer();
            CustomerManager customerManager = new CustomerManager();          
            Room_Customer room_Customer = new Room_Customer();
            Room_CustomerManager room_CustomerManager = new Room_CustomerManager();
            TimeSpan time = dtpChkOutDate.Value - dtpChkInDate.Value;
            string strDay = time.ToString().Split('.')[0];
            for (int i = 0; i < gridCustomer.Rows.Count; i++)
            {
                if(gridCustomer.Rows[i].Cells[0].Value != null) 
                {
                    customer.Tc = gridCustomer.Rows[i].Cells[0].Value.ToString();
                    customer.Name = gridCustomer.Rows[i].Cells[1].Value.ToString();
                    customer.Surname = gridCustomer.Rows[i].Cells[2].Value.ToString();
                    customer.PhoneNumber = gridCustomer.Rows[i].Cells[3].Value.ToString();
                    customer.Email = gridCustomer.Rows[i].Cells[4].Value.ToString();
                    customerManager.Add(customer);
                    
                    room_Customer.RoomID = Convert.ToInt32(txtRoomId.Text);
                    room_Customer.PersonCount = int.Parse(txtCusNumber.Text);
                    room_Customer.CheckInTime = dtpChkInDate.Value;
                    room_Customer.CheckOutTime = dtpChkOutDate.Value;
                    room_Customer.RentedDay = int.Parse(strDay);
                    room_Customer.PricePerDay = Convert.ToDouble(txtDailyPrice.Text);
                    room_Customer.PriceSum = Convert.ToDouble(txtPriceSum.Text);
                    room_Customer.RegisterDate = DateTime.Now;
                    room_CustomerManager.Add(customer, room_Customer, room); 
                }
            }
            room.RoomNumber = int.Parse(lblRN.Text);
            foreach (Button button in flwPnlRooms.Controls)
            {
                if (button.Text == room.RoomNumber.ToString())
                    button.BackColor = Color.Red;
            }
        }

    
        public void ClickSearch(Button sender)
        {
            RoomManager roomManager = new RoomManager();

            DBHelper dBHelper = new DBHelper();
            Room room = roomManager.Search(Convert.ToInt32(((Button)sender).Text));

            lblRN.Text = room.RoomNumber.ToString();
            lblRT.Text = room.RoomType.ToString();
            lblBT.Text = room.BedType.ToString();
            lblBC.Text = room.BedCount.ToString();
            lblC.Text = room.Capacity.ToString();
            lbl.Text = room.Price.ToString();
            chkOcu.Checked = room.Occupied;
        }
        private Room Search(string strCmd)
        {
            DBHelper dbHelper = new DBHelper();
            Room room = new Room();
            Image img = Image.FromFile("../../Image/1.png");
            using (SqlConnection conn = new SqlConnection(DBHelper.strCon))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(strCmd, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    room.RoomNumber = Convert.ToInt16(reader[7]);
                    room.RoomID = Convert.ToInt32(reader[0]);
                    room.Occupied = (bool)reader[5];
                    Button button = new Button();

                    button.Width = 45;
                    button.Height = 45;
                    button.Text = room.RoomNumber.ToString();
                    button.Name = room.RoomNumber.ToString();
                    button.Tag = room.RoomID;
                    button.Image = img;
                    
                    if (room.Occupied == true)

                        button.BackColor = Color.Red;
                    else
                        button.BackColor = Color.Green;
                    button.Click += Button_Click;

                    flwPnlRooms.Controls.Add(button);
                }
            }
            return room;
        }

        private void dtpChkOutDate_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan time = dtpChkOutDate.Value - dtpChkInDate.Value;      
            string strDay = (int.Parse(time.ToString().Split('.')[0]) + 1).ToString();
            txtPriceSum.Text = (int.Parse(strDay) * int.Parse(txtDailyPrice.Text)).ToString();
        }

        private void btnUpdt_Click(object sender, EventArgs e)
        {
            Room room = new Room();
            Customer customer = new Customer();
            CustomerManager customerManager = new CustomerManager();
            Room_Customer room_Customer = new Room_Customer();
            Room_CustomerManager room_CustomerManager = new Room_CustomerManager();
            TimeSpan time = dtpChkOutDate.Value - dtpChkInDate.Value;
            string strDay = time.ToString().Split('.')[0];
            for (int i = 0; i < gridCustomer.Rows.Count; i++)
            {
                customer.Tc = gridCustomer.Rows[i].Cells[1].Value.ToString();
                customer.Name = gridCustomer.Rows[i].Cells[2].Value.ToString();
                customer.Surname = gridCustomer.Rows[i].Cells[3].Value.ToString();
                customer.PhoneNumber = gridCustomer.Rows[i].Cells[4].Value.ToString();
                customer.Email = gridCustomer.Rows[i].Cells[5].Value.ToString();
                customerManager.Add(customer);
                room.RoomID = int.Parse(txtRoomId.Text);
                room_Customer.RoomID = room.RoomID;
                room_Customer.PersonCount = int.Parse(txtCusNumber.Text);
                room_Customer.CheckInTime = dtpChkInDate.Value;
                room_Customer.CheckOutTime = dtpChkOutDate.Value;
                room_Customer.RentedDay = int.Parse(strDay);
                room_Customer.PricePerDay = Convert.ToDouble(txtDailyPrice.Text);
                room_Customer.PriceSum = Convert.ToDouble(txtPriceSum.Text);
                room_Customer.RegisterDate = DateTime.Now;
                room_CustomerManager.Add(customer, room_Customer, room);
            }
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            Room room = new Room();
            RoomManager roomManager = new RoomManager();
            room.RoomNumber = int.Parse(lblRN.Text);
            room.Occupied = false;
            room.RoomID = int.Parse(txtRoomId.Text);
            roomManager.Leave(room);
            foreach (Button button in flwPnlRooms.Controls)
            {
                if (button.Text == room.RoomNumber.ToString())
                    button.BackColor = Color.Green;
            }
        }

        private void gridCustomer_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            txtCusNumber.Text = (gridCustomer.RowCount - 1).ToString();
        }

        private void gridCustomer_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (gridCustomer.Rows[e.RowIndex].Cells[e.ColumnIndex] == gridCustomer.Rows[e.RowIndex].Cells[0])
            {
                if (gridCustomer.Rows[e.RowIndex].Cells[0].Value.ToString().Length != 11 || gridCustomer.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    MessageBox.Show("Girdiğiniz değer 11 karakter uzunluğunda olmalı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    gridCustomer.Rows[e.RowIndex].Cells[0].Value = "";
                }
            }
            else if (gridCustomer.Rows[e.RowIndex].Cells[e.ColumnIndex] == gridCustomer.Rows[e.RowIndex].Cells[3])
            {
                if (gridCustomer.Rows[e.RowIndex].Cells[3].Value.ToString().Length != 10 || gridCustomer.Rows[e.RowIndex].Cells[3].Value == null)
                {
                    MessageBox.Show("Girdiğiniz değer 10 karakter uzunluğunda olmalı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    gridCustomer.Rows[e.RowIndex].Cells[3].Value = "";
                }
            }
        }

        private void tabList_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tbpCustomer)
            {
                CustomerManager customerManager = new CustomerManager();
                gridList.DataSource = customerManager.GetList();
            }
            else if (e.TabPage == tbpRoom)
            {
                RoomManager roomManager = new RoomManager();
                gridList.DataSource = roomManager.GetList();
            }
            else if (e.TabPage == tbpLog)
            {
                Room_CustomerManager room_CustomerManager = new Room_CustomerManager();
                gridList.DataSource = room_CustomerManager.GetList();
            }
        }

        private void chkFTc_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFTc.Checked)
            {
                txtFTc.Visible = false;
            }
            else
                txtFTc.Visible = true;
        }

        private void chkFName_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFName.Checked)
            {
                txtFName.Visible = false;
            }
            else
                txtFName.Visible = true;
        }

        private void chkFSur_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFSur.Checked)
            {
                txtFSur.Visible = false;
            }
            else
                txtFSur.Visible = true;
        }

        private void chkFPhone_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFPhone.Checked)
            {
                txtFPhone.Visible = false;
            }
            else
                txtFPhone.Visible = true;
        }

        private void chkFEmail_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFEmail.Checked)
            {
                txtFEmail.Visible = false;
            }
            else
                txtFEmail.Visible = true;
        }

        private void btnCusFilter_Click(object sender, EventArgs e)
        {
            string str = "";
            if (chkFTc.Checked)
            {
                str += "MusteriTc = '" + txtFTc.Text + "' AND ";
            }
            if (chkFName.Checked)
            {
                str += "MusteriAd = '" + txtFName.Text + "' AND ";
            }
            if (chkFSur.Checked)
            {
                str += "MusteriSoyad = '" + txtFSur.Text + "' AND ";
            }
            if (chkFPhone.Checked)
            {
                str += "MusteriTelNo = '" + txtFPhone.Text + "' AND ";
            }
            if (chkFEmail.Checked)
            {
                str += "MusteriEmail = '" + txtFEmail.Text + "' AND ";

            }
            str = str.Remove(str.LastIndexOf('A') - 1);
            CustomerManager customerManager = new CustomerManager();
            DataTable dt = customerManager.GetList();
            dt.DefaultView.RowFilter = str;
            gridList.DataSource = dt.DefaultView.ToTable();
        }

        private void chkFBedT_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFBedT.Checked)
            {
                txtFBedT.Visible = false;
            }
            else
                txtFBedT.Visible = true;
        }

        private void chkFCap_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFCap.Checked)
            {
                txtFCap.Visible = false;
            }
            else
                txtFCap.Visible = true;
        }

        private void chkFBedC_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFBedC.Checked)
            {
                txtFBedC.Visible = false;
            }
            else
                txtFBedC.Visible = true;
        }

        private void chkFRoomT_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFRoomT.Checked)
            {
                txtFRoomT.Visible = false;
            }
            else
                txtFRoomT.Visible = true;
        }

        private void chkFPrice_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFPrice.Checked)
            {
                txtFPrice.Visible = false;
            }
            else
                txtFPrice.Visible = true;
        }

        private void chkFRoomNum_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFRoomNum.Checked)
            {
                txtFRoomNum.Visible = false;
            }
            else
                txtFRoomNum.Visible = true;
        }

        private void btnRoomFilter_Click(object sender, EventArgs e)
        {
            string str = "";
            if (chkFBedT.Checked)
            {
                str += "YatakTuru = '" + txtFBedT.Text + "' AND ";
            }
            if (chkFCap.Checked)
            {
                str += "Kapasite = '" + txtFCap.Text + "' AND ";
            }
            if (chkFBedC.Checked)
            {
                str += "YatakSayisi = '" + txtFBedC.Text + "' AND ";
            }
            if (chkFRoomT.Checked)
            {
                str += "OdaTipi = '" + txtFRoomT.Text + "' AND ";
            }
            if (chkFPrice.Checked)
            {
                str += "AnlıkFiyat = '" + txtFPrice.Text + "' AND ";
            }
            if (chkFRoomNum.Checked)
            {
                str+= "OdaNo = '" + txtFRoomNum.Text + "' AND ";
            }

            str = str.Remove(str.LastIndexOf('A') - 1);
            CustomerManager customerManager = new CustomerManager();
            DataTable dt = customerManager.GetList();
            dt.DefaultView.RowFilter = str;
            gridList.DataSource = dt.DefaultView.ToTable();
        }

        private void chkFLTc_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFLTc.Checked)
            {
                txtFLTc.Visible = false;
            }
            else
                txtFLTc.Visible = true;
        }

        private void chkFLName_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFLName.Checked)
            {
                txtFLName.Visible = false;
            }
            else
                txtFLName.Visible = true;
        }

        private void chkFLSur_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFLSur.Checked)
            {
                txtFLSur.Visible = false;
            }
            else
                txtFLSur.Visible = true;
        }

        private void chkFLPhone_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFLPhone.Checked)
            {
                txtFLPhone.Visible = false;
            }
            else
                txtFLPhone.Visible = true;
        }

        private void chkFLMail_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFLMail.Checked)
            {
                txtFLMail.Visible = false;
            }
            else
                txtFLMail.Visible = true;
        }

        private void chkFLRoomNum_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFLRoomNum.Checked)
            {
                txtFLRoomNum.Visible = false;
            }
            else
                txtFLRoomNum.Visible = true;
        }

        private void btnFLog_Click(object sender, EventArgs e)
        {
            string str = "";
            if (chkFLTc.Checked)
            {
                str += "MusteriTc = '" + txtFLTc.Text + "' AND ";
            }
            if (chkFLName.Checked)
            {
                str += "MusteriAd = '" + txtFLName.Text + "' AND ";
            }
            if (chkFLSur.Checked)
            {
                str += "MusteriSoyad = '" + txtFLSur.Text + "' AND ";
            }
            if (chkFLPhone.Checked)
            {
                str += "MusteriTelNo = '" + txtFLPhone.Text + "' AND ";
            }
            if (chkFLMail.Checked)
            {
                str += "MusteriEmail = '" + txtFLMail.Text + "' AND ";
            }
            if (chkFLRoomNum.Checked)
            {
                str += "OdaNo = '" + txtFLRoomNum.Text + "' AND ";
            }
            str = str.Remove(str.LastIndexOf('A') - 1);
            Room_CustomerManager room_CustomerManager = new Room_CustomerManager();
            DataTable dt = room_CustomerManager.GetList();
            dt.DefaultView.RowFilter = str;
            gridList.DataSource = dt.DefaultView.ToTable();
        }
    }
}
