using System.Collections.Generic;
using System.Data.SqlClient;
using Chucksoft.Entities.Database;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace Chucksoft.Resources.Data
{
    public class DatabaseHelper
    {
        public Server Server;

        private DatabaseHelper() { }

        public DatabaseHelper(string connectionString)
        {
            if (Server == null)
            {
                Server = GetServer(connectionString);
            }
        }

        public bool IsValidConnectionString()
        {
            //Server _server = GetServer(connectionString);

            bool isValidConnectionString = (Server != null);
            return isValidConnectionString;
        }

        private Server GetServer(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            Server = new Server(new ServerConnection(connection));

            return Server;
        }

        public List<string> RetrieveDatabaseNames()
        {

            List<string> dbs = new List<string>();

            foreach (Database db in Server.Databases)
            {
                dbs.Add(db.Name);
            }

            return dbs;
        }

        public List<string> RetrieveTables(string databaseName)
        {
            Database database = Server.Databases[databaseName];
            List<string> tables = new List<string>();

            foreach (Table table in database.Tables)
            {
                tables.Add(table.Name);
            }

            return tables;
        }

        public List<DatabaseColumn> RetrieveColumns(string databaseName, string tableName)
        {
            List<DatabaseColumn> columns = new List<DatabaseColumn>();
            Database database = Server.Databases[databaseName];
            Table table = database.Tables[tableName];

            foreach (Column col in table.Columns)
            {
                DatabaseColumn column = new DatabaseColumn
                                            {
                                                Name = col.Name,
                                                SqlColumnType = col.DataType.SqlDataType.ToString().ToLower(),
                                                SqlColumnTypeAndSize = SqlTypeConversion.ConvertSqlTypeToSqlNativeType(col.DataType.SqlDataType.ToString(), col.DataType),
                                                ColumnType = SqlTypeConversion.ConvertSqlTypesToCSharpShorthand(col.DataType.SqlDataType.ToString()),
                                                IsPrimaryKey = col.InPrimaryKey,
                                                IsForeignKey = col.IsForeignKey,
                                                IsNullable = col.Nullable,
                                                SqlClientDbType = SqlTypeConversion.ConvertSqlTypesToSqlClientDbType(col.DataType.SqlDataType.ToString()),
                                                Size = col.DataType.MaximumLength
                                            };

                columns.Add(column);
            }
            
            return columns;
        }
    }
}
