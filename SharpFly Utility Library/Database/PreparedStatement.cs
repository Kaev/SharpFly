using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace SharpFly_Utility_Library.Database
{
    public class PreparedStatement
    {
        private MySqlCommand m_Command;

        public string Query { get; private set; }

        public PreparedStatement(Database database, string query, params MySqlParameter[] parameters)
        {
            Query = query;
            m_Command = new MySqlCommand(Query, database.Connection.Connection);

            m_Command.Parameters.AddRange(parameters);
            m_Command.Prepare();
        }

        public DataTable Process(params object[] parameters)
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
