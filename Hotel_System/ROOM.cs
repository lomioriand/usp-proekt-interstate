using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Hotel_System
{
    class ROOM
    {
        CONNECT conn = new CONNECT();

        public DataTable roomTypeList()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM rooms_category;", conn.getConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        // list of rooms by type
        public DataTable roomTypeList(int type)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM rooms WHERE type=@typ and free='Yes';", conn.getConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            command.Parameters.Add("@typ", MySqlDbType.VarChar).Value = type;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }


        //Returns roomType id
        public int getRoomType(int number)
        {
            MySqlCommand command = new MySqlCommand("SELECT type FROM rooms WHERE number=@num;", conn.getConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            //@num
            command.Parameters.Add("@num", MySqlDbType.VarChar).Value = number;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return Convert.ToInt32(table.Rows[0][0].ToString());
        }


        public bool addRoom(int number, int type, String phone, String free)
        {
            MySqlCommand command = new MySqlCommand();
            String insertQuery = "INSERT INTO rooms(number, type, phone, free) VALUES (@num, @tp, @phn, @fr);";

            //@num, @tp, @phn, @fr
            command.Parameters.Add("@num", MySqlDbType.VarChar).Value = number;
            command.Parameters.Add("@tp", MySqlDbType.VarChar).Value = type;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@fr", MySqlDbType.VarChar).Value = free;

            command.CommandText = insertQuery;
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

        public DataTable getRooms()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM rooms;", conn.getConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }


        public bool editRoom(int number, int type, String phone, String free)
        {
            MySqlCommand command = new MySqlCommand();
            String updateQuery = "UPDATE rooms SET type=@tp, phone=@phn, free=@fr WHERE number=@num;";

            //@num, @tp, @phn, @fr
            command.Parameters.Add("@num", MySqlDbType.VarChar).Value = number;
            command.Parameters.Add("@tp", MySqlDbType.VarChar).Value = type;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@fr", MySqlDbType.VarChar).Value = free;

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


        public bool removeRoom(int number)
        {
            MySqlCommand command = new MySqlCommand();
            String removeQuery = "DELETE FROM rooms WHERE number=@num;";

            command.Parameters.Add("@num", MySqlDbType.Int32).Value = number;

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


        public bool setRoom(int number, string free)
        {
            MySqlCommand command = new MySqlCommand("UPDATE rooms SET free=@fr WHERE number=@num;", conn.getConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            //@num, @fr
            command.Parameters.Add("@num", MySqlDbType.Int32).Value = number;
            command.Parameters.Add("@fr", MySqlDbType.VarChar).Value = free;

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
