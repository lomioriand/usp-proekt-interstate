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
    public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();
        }

        private void Main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageClientsForm manageClientsForm = new ManageClientsForm();
            manageClientsForm.ShowDialog();
        }

        private void roomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageRoomsForm manageRoomsForm = new ManageRoomsForm();
            manageRoomsForm.ShowDialog();
        }

        private void reservationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageReservationsForm manageReservationsForm = new ManageReservationsForm();
            manageReservationsForm.ShowDialog();
        }
    }
}
