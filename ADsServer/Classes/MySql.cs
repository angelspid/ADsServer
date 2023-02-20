using MySql.Data.MySqlClient;
using Mysqlx.Prepare;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;

namespace ADsServer.Classes
{
    internal class MySql
    {
        MySqlConnection mySqlConnection;
        public MySql() 
        {
            string connStr = String.Format("server={0}; user id={1}; password={2}; database={3}; pooling=false; Character Set={4}", "localhost", "root", "", "spareparts", "utf8mb4");
            mySqlConnection= new MySqlConnection(connStr);
        }
        /// <summary>
        /// Get DataTable form MySQL
        /// </summary>
        /// <param name="query">MySQL query</param>
        /// <param name="parms">Array of columns in query</param>
        /// <param name="values">Array of values for columns</param>
        /// <returns>DataTable</returns>
        public DataTable GetTable(string query, string[]? parms = null, string[]? values = null)
        {
            DataTable table = new DataTable();
            MySqlCommand command = new MySqlCommand(query, mySqlConnection);
            if (parms != null && values != null)
                if (parms.Length == values.Length)                
                    for (int i = 0; i< parms.Length; i++)
                        command.Parameters.Add(new MySqlParameter(parms[i], values[i]));
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(table);
            return table;
        }
    }
}
