using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_System
{
    public partial class ManageReservationsForm : Form
    {
        ROOM room = new ROOM();
        RESERVATION reservation = new RESERVATION();

        public ManageReservationsForm()
        {
            InitializeComponent();
        }

        private void ManageReservationsForm_Load(object sender, EventArgs e)
        {
            buttonEdit.Hide();
            textBoxReservID.Enabled = false;
            
            comboBoxRoomType.DataSource = room.roomTypeList();
            comboBoxRoomType.DisplayMember = "label";
            comboBoxRoomType.ValueMember = "category_id";

            int type = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
            comboBoxRoomNum.DataSource = room.roomTypeList(type);
            comboBoxRoomNum.DisplayMember = "number";
            comboBoxRoomNum.ValueMember = "number";

            dataGridView1.DataSource = reservation.geAllReservs();
        }

        private void clean()
        {
            textBoxReservID.Text = "";
            textBoxClientID.Text = "";
            comboBoxRoomType.SelectedIndex = 0;
            comboBoxRoomNum.SelectedIndex = 0;
            dateTimePickerIN.Value = DateTime.Now;
            dateTimePickerOUT.Value = DateTime.Now;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            clean();
        }

        private void comboBoxRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int type = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
                comboBoxRoomNum.DataSource = room.roomTypeList(type);
                comboBoxRoomNum.DisplayMember = "number";
                comboBoxRoomNum.ValueMember = "number";

            } catch(Exception){ }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int clientId = Convert.ToInt32(textBoxClientID.Text);
                int roomNumber = Convert.ToInt32(comboBoxRoomNum.SelectedValue);
                DateTime dateIn = dateTimePickerIN.Value;
                DateTime dateOut = dateTimePickerOUT.Value;


                if (DateTime.Compare(dateIn.Date, DateTime.Now.Date) < 0 )
                {
                    throw new Exception("Date In Must be EQUAL or GREATER than Today");
                } 
                else if (DateTime.Compare(dateOut.Date, dateIn.Date) < 0)
                {
                    throw new Exception("Date Out Must be GREATER than Date In");
                }
                else
                {
                    if (reservation.addReserv(roomNumber, clientId, dateIn, dateOut))
                    {
                        room.setRoom(roomNumber, "No");
                        dataGridView1.DataSource = reservation.geAllReservs();
                        clean();
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong");
                    }

                }

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int reservID = Convert.ToInt32(textBoxReservID.Text);
                int clientId = Convert.ToInt32(textBoxClientID.Text);
                int roomNumber = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                DateTime dateIn = dateTimePickerIN.Value;
                DateTime dateOut = dateTimePickerOUT.Value;


                if (dateIn < DateTime.Today)
                {
                    throw new Exception("Date In Must be EQUAL or GREATER than Today");
                }
                else if (dateOut < dateIn)
                {
                    throw new Exception("Date Out Must be GREATER than Date In");
                }
                else
                {
                    if (reservation.editReserv(reservID, roomNumber, clientId, dateIn, dateOut))
                    {
                        room.setRoom(roomNumber, "No");
                        dataGridView1.DataSource = reservation.geAllReservs();
                        MessageBox.Show("Reservation Edited");
                        clean();
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxReservID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            //get room id 
            int roomID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString());
            
            //select the room type from combobox
            //comboBoxRoomType.SelectedValue = room.getRoomType(roomID);

            //select room number from combobox
            comboBoxRoomNum.SelectedValue = roomID;

            textBoxClientID.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            dateTimePickerIN.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[3].Value);
            dateTimePickerOUT.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[4].Value);

        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if(textBoxReservID.Text == "")
                {
                    throw new Exception("Reservation Not Selected");
                }
                else
                {
                    int reservID = Convert.ToInt32(textBoxReservID.Text);
                    int roomNumber = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    if (reservation.removeReserv(Convert.ToInt32(textBoxReservID.Text)))
                    {
                        dataGridView1.DataSource = reservation.geAllReservs();
                        room.setRoom(roomNumber, "Yes");
                    }
                    else
                    {
                        throw new Exception("Something went wrong. \nReservation is NOT deleted.");
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
