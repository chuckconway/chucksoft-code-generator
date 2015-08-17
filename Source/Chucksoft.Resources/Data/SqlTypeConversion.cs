using Microsoft.SqlServer.Management.Smo;

namespace Chucksoft.Resources.Data
{
    public static class SqlTypeConversion
    {
        public static string ConvertSqlTypesToCSharpShorthand(string sqlType)
        {
            switch(sqlType.ToLower())
            {
                case "nvarchar":
                    return "string";
                case "varchar":
                    return "string";
                case "nchar":
                    return "string";
                case "uniqueidentifier":
                    return "Guid";
                case "nvarcharmax":
                    return "string";
                case "bit":
                    return "bool";
                case "tinyint":
                    return "byte";
                case "smallint":
                    return "short";
                case "int":
                    return "int";
                case "bigint":
                    return "long";
                case "smallmoney":
                    return "decimal";
                case "money":
                    return "decimal";
                case "numeric":
                    return "decimal";
                case "decimal":
                    return "decimal";
                case "real":
                    return "float";
                case "float":
                    return "double";
                case "smalldatetime":
                    return "DateTime";
                case "datetime":
                    return "DateTime";
                default:
                    return sqlType;
            }
        }

        public static string FindConvertToMethodsBySqlType(string sqlType, string codeInjection )
        {
            switch (sqlType.ToLower())
            {
                case "nvarchar(max)":
                case "nvarchar":
                    return string.Format("reader.GetString(\"{0}\")", codeInjection);
                case "varchar":
                    return string.Format("reader.GetString(\"{0}\")", codeInjection);
                case "uniqueidentifier":
                    return string.Format("reader.GetGuid(\"{0}\")", codeInjection);
                case "bit":
                    return string.Format("reader.GetBoolean(\"{0}\")", codeInjection);
                case "tinyint":
                    return string.Format("reader.GetByte(\"{0}\")", codeInjection);
                case "smallint":
                    return string.Format("reader.GetInt16(\"{0}\")", codeInjection);
                case "int":
                    return string.Format("reader.GetInt32(\"{0}\")", codeInjection);
                case "bigint":
                    return string.Format("reader.GetInt64(\"{0}\")", codeInjection);
                case "smallmoney":
                    return string.Format("reader.GetDecimal(\"{0}\")", codeInjection);
                case "money":
                    return string.Format("reader.GetDecimal(\"{0}\")", codeInjection);
                case "numeric":
                    return string.Format("reader.GetDecimal(\"{0}\")", codeInjection);
                case "decimal":
                    return string.Format("reader.GetDecimal(\"{0}\")", codeInjection);
                case "real":
                    return string.Format("reader.GetSingle(\"{0}\")", codeInjection);
                case "float":
                    return string.Format("reader.GetDouble(\"{0}\")", codeInjection);
                case "smalldatetime":
                    return string.Format("reader.GetDateTime(\"{0}\")", codeInjection);
                case "datetime2":
                case "datetime":
                    return string.Format("reader.GetDateTime(\"{0}\")", codeInjection);
                default:
                    return sqlType;
            }
        }

        internal static string ConvertSqlTypeToSqlNativeType(string name, DataType type)
        {
            const string format = "{0}({1})";

            switch (name.ToLower())
            {
                case "nvarcharmax":
                    return "nvarchar(max)";
                case "nvarchar":
                    return string.Format(format, type.Name, type.MaximumLength);
                case "varchar":
                    return string.Format(format, type.Name, type.MaximumLength);
                case "uniqueidentifier":
                    return "uniqueidentifier";
                case "bit":
                    return "bit";
                case "tinyint":
                    return "tinyint";
                case "smallint":
                    return "smallint";
                case "int":
                    return "int";
                case "bigint":
                    return "bigint";
                case "smallmoney":
                    return "smallmoney";
                case "money":
                    return "money";
                case "numeric":
                    return "numeric";
                case "decimal":
                    return "decimal";
                case "real":
                    return "real";
                case "float":
                    return "float";
                case "smalldatetime":
                    return string.Format(format, type.Name, type.MaximumLength);
                case "datetime":
                    return "datetime";
                default:
                    return type.Name;
            }
        }

        public static string ConvertSqlTypesToSqlClientDbType(string sqlType)
        {
            switch (sqlType.ToLower())
            {
                case "nvarcharmax":
                case "nvarchar":
                    return "SqlDbType.NVarChar";
                case "varchar":
                    return "SqlDbType.VarChar";
                case "uniqueidentifier":
                    return "SqlDbType.UniqueIdentifier";
                case "bit":
                    return "SqlDbType.Bit";
                case "tinyint":
                    return "SqlDataType.TinyInt";
                case "smallint":
                    return "SqlDbType.SmallInt";
                case "int":
                    return "SqlDbType.Int";
                case "bigint":
                    return "SqlDbType.BigInt";
                case "smallmoney":
                    return "SqlDbType.SmallMoney";
                case "money":
                    return "SqlDbType.Money";
                case "numeric":
                    return "SqlDbType.Numeric";
                case "decimal":
                    return "SqlDbType.Decimal";
                case "real":
                    return "SqlDbType.Real";
                case "float":
                    return "SqlDbType.Float";
                case "smalldatetime":
                    return "SqlDbType.SmallDateTime";
                case "datetime":
                    return "SqlDbType.DateTime";
                default:
                    return sqlType;
            }
        }


    }
}
