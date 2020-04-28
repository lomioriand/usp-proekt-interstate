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
    public partial class ManageClientsForm : Form
    {
        CLIENT client = new CLIENT();

        public ManageClientsForm()
        {
            InitializeComponent();
            textBoxID.Enabled = false;
        }

        private void ManageClientsForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = client.getClients();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }       

        private void buttonClear_Click(object sender, EventArgs e)
        {
            clean();
        }

        private void clean()
        {
            textBoxID.Text = "";
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxPhone.Text = "";
            textBoxCountry.Text = "";
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            String fname = textBoxFirstName.Text;
            String lname = textBoxLastName.Text;
            String phone = textBoxPhone.Text;
            String country = textBoxCountry.Text;


            if(fname != "" && lname != "" && phone != "" && country != "")
            {

                if (client.insertClient(fname, lname, phone, country))
                {
                    dataGridView1.DataSource = client.getClients();
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
                MessageBox.Show("Please Fill All Fields","", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            String fname = textBoxFirstName.Text;
            String lname = textBoxLastName.Text;
            String phone = textBoxPhone.Text;
            String country = textBoxCountry.Text;


            if (fname != "" && lname != "" && phone != "" && country != "")
            {
                int id = Convert.ToInt32(textBoxID.Text);

                if (client.editClient(id, fname, lname, phone, country))
                {
                    dataGridView1.DataSource = client.getClients();
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
                MessageBox.Show("Please Fill All Fields", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxFirstName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString(); 
            textBoxLastName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBoxPhone.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBoxCountry.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if(textBoxID.Text != "")
            {
                int id = Convert.ToInt32(textBoxID.Text);

                if (client.removeClient(id))
                {
                    dataGridView1.DataSource = client.getClients();
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
                MessageBox.Show("Please select a client");
            }
        }



    }
}
