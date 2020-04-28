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
    public partial class ManageRoomsForm : Form
    {
        public ManageRoomsForm()
        {
            InitializeComponent();
        }

        private void clean()
        {
            textBoxID.Text = "";
            textBoxPhone.Text = "";
            comboBoxRoomType.SelectedIndex = 0;
            radioButton1.Checked = true;
        }
        
        private void buttonClear_Click(object sender, EventArgs e)
        {
            clean();
        }

        ROOM room = new ROOM();
        private void ManageRoomsForm_Load(object sender, EventArgs e)
        {
            comboBoxRoomType.DataSource = room.roomTypeList();
            comboBoxRoomType.DisplayMember = "label";
            comboBoxRoomType.ValueMember = "category_id";
            dataGridView1.DataSource = room.getRooms();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            String s_id = textBoxID.Text;
            int type = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
            String phone = textBoxPhone.Text;

            string free = "";

            if (radioButton1.Checked == true)
            {
                free = "Yes";
            }
            else if (radioButton2.Checked == true)
            {
                free = "No";
            }


            if (s_id != "" && phone != "")
            {
                try
                {

                    if (room.addRoom(Convert.ToInt32(s_id), type, phone, free))
                    {
                        dataGridView1.DataSource = room.getRooms();
                        clean();
                    }
                    else
                    {
                        MessageBox.Show("Something Went Wrong");
                        clean();
                    }

                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message); // Duplicate room entry
                }

            }
            else
            {
                MessageBox.Show("Please Fill All Fields", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBoxRoomType.SelectedIndex = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value)-1;
            textBoxPhone.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            String flag = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            if (flag.Equals("Yes"))
            {
                radioButton1.Checked = true;
            } else if (flag.Equals("No"))
            {
                radioButton2.Checked = true;
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if( (textBoxID.Text != "" && textBoxPhone.Text != "") &&
                (radioButton1.Checked == true || radioButton2.Checked == true))
            {

                try
                {
                    int number = Convert.ToInt32(textBoxID.Text);
                    int type = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
                    String phone = textBoxPhone.Text;
                    String free = "";

                    if (radioButton1.Checked == true)
                    {
                        free = "Yes";
                    }
                    else if (radioButton2.Checked == true)
                    {
                        free = "No";
                    }

                    if (room.editRoom(number, type, phone, free))
                    {
                        dataGridView1.DataSource = room.getRooms();
                        clean();
                    }
                    else
                    {
                        MessageBox.Show("Something Went Wrong");
                        clean();
                    }

                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Enter all the required data");
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if(textBoxID.Text != "")
            {
                int number = Convert.ToInt32(textBoxID.Text);

                if (room.removeRoom(number))
                {
                    dataGridView1.DataSource = room.getRooms();
                    clean();
                }
                else
                {
                    MessageBox.Show("Something Went Wrong");
                    clean();
                }
            }
            else
            {
                MessageBox.Show("Please Select a Room");
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
