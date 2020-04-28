using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Hotel_System
{
    class RESERVATION
    {
        CONNECT conn = new CONNECT();

        public DataTable geAllReservs()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM reservations;", conn.getConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        public bool addReserv(int number, int clientId, DateTime dateIn, DateTime dateOut)
        {
            MySqlCommand command = new MySqlCommand();
            String addQuery = "INSERT INTO reservations(roomNumber, clientId, DateIn, DateOut) VALUES (@rnm, @cid, @din, @dout);";

            //@rnm, @cid, @din, @dout
            command.Parameters.Add("@rnm", MySqlDbType.VarChar).Value = number;
            command.Parameters.Add("@cid", MySqlDbType.VarChar).Value = clientId;
            command.Parameters.Add("@din", MySqlDbType.DateTime).Value = dateIn;
            command.Parameters.Add("@dout", MySqlDbType.DateTime).Value = dateOut;

            command.CommandText = addQuery;
            command.Connection = conn.getConnection();

            conn.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                conn.closeConnection();
                return true;
            }
            else
            {
                conn.closeConnection();
                return false;
            }

        }

        public bool editReserv(int reservId, int number, int clientId, DateTime dateIn, DateTime dateOut)
        {
            MySqlCommand command = new MySqlCommand();
            String updateQuery = "UPDATE rooms SET type=@tp, phone=@phn, free=@fr WHERE number=@num;";

            //@rvid, @rnm, @cid, @din, @dout
            command.Parameters.Add("@rvid", MySqlDbType.VarChar).Value = reservId;
            command.Parameters.Add("@rnm", MySqlDbType.VarChar).Value = number;
            command.Parameters.Add("@cid", MySqlDbType.VarChar).Value = clientId;
            command.Parameters.Add("@din", MySqlDbType.DateTime).Value = dateIn;
            command.Parameters.Add("@dout", MySqlDbType.DateTime).Value = dateOut;

            command.CommandText = updateQuery;
            command.Connection = conn.getConnection();

            conn.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                conn.closeConnection();
                return true;
            }
            else
            {
                conn.closeConnection();
                return false;
            }

        }


        public bool removeReserv(int reservId)
        {
            MySqlCommand command = new MySqlCommand();
            String removeQuery = "DELETE FROM reservations WHERE reservId=@rsvid;";

            command.Parameters.Add("@rsvid", MySqlDbType.Int32).Value = reservId;

            command.CommandText = removeQuery;
            command.Connection = conn.getConnection();

            conn.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                conn.closeConnection();
                return true;
            }
            else
            {
                conn.closeConnection();
                return false;
            }
        }

    }
}



/*
*------------------------------------------------------------------------------------------------*
Only Existing clients: 
 
    ALTER TABLE rooms ADD CONSTRAINT fk_type_id FOREIGN KEY (type)
        REFERENCES rooms_category(category_id) ON UPDATE CASCADE on DELETE CASCADE;
 
    ALTER TABLE reservations ADD CONSTRAINT fk_room_number FOREIGN KEY (roomNumber)
        REFERENCES rooms(number) ON UPDATE CASCADE on DELETE CASCADE;
  
    ALTER TABLE reservations ADD CONSTRAINT fk_client_id FOREIGN KEY (clientId) 
        REFERENCES clients(id) ON UPDATE CASCADE on DELETE CASCADE;

*------------------------------------------------------------------------------------------------*
The room is not available:
    SELECT * FROM rooms WHERE type=@typ and free='Yes';
 */
