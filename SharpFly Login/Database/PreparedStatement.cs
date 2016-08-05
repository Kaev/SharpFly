using System;
using MySql.Data.MySqlClient;
using System.Diagnostics.Contracts;
using System.Collections.Generic;
using System.Data;

namespace SharpFly_Login.Database
{
    public class PreparedStatement
    {
        private MySqlCommand m_Command;

        public string Query { get; private set; }

        public PreparedStatement(string query, params MySqlParameter[] parameters)
        {
            Query = query;
            m_Command = new MySqlCommand(Query, MySQL.Connection);
            
            m_Command.Parameters.AddRange(parameters);
            m_Command.Prepare();
        }

        public DataTable Process(params string[] parameters)
        {
            try
            {
                if (parameters.Length != m_Command.Parameters.Count)
                    throw new ArgumentNullException(String.Format("The SQL query has {0} parameters, but you executed it with {1} parameters", m_Command.Parameters.Count.ToString(), parameters.Length.ToString()));

                for (int i = 0; i < parameters.Length; i++)
                    m_Command.Parameters[i].Value = parameters[i];

                using (MySqlDataReader reader = m_Command.ExecuteReader())
                {
                    DataTable retVal = new DataTable();
                    retVal.Load(reader);
                    return retVal;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
