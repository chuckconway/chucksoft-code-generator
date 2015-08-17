using System.Text;
using Chucksoft.Entities;
using Chucksoft.Entities.Database;
using Chucksoft.Entities.Execptions;
using Chucksoft.Entities.GenerationTemplates.Dynamic;
using Chucksoft.Resources.Data;

namespace Chucksoft.Templates
{
    public class StaticDbParametersResources :  IGenerateMethods
    {
        private string _content;

        public string Render(DatabaseTable table, CodeGenSettings settings)
        {
            _content = Core.ResourceFileHelper.ConvertStreamResourceToUTF8String(typeof(Resources), "Chucksoft.Templates.Templates.Resources.template");

            //Check for expected content, if not found, badness has happen.
            if (string.IsNullOrEmpty(_content))
            {
                throw new ContentNotFound("Can't find embeddedResource \"Chucksoft.Templates.Templates.Resources.template\"");
            }

            GetItem(table);
            GenerateDeleteMethods(table);
            GenerateInsertMethods(table, settings);
            GenerateUpdateMethods(table);
            GenerateSelectAll(table);
            GenerateSelectByPrimaryKey(table);
            GeneratePopulateMethod(table);
            _content = _content.Replace("<%[TableName]%>", table.Name);
            _content = _content.Replace("<%[TableParameter]%>", table.Name.ToLower());


            return _content;
        }

        private void GenerateInsertMethods(DatabaseTable table, CodeGenSettings settings)
        {
            //<%[InsertMethod]%> -- Token
            string makePararmeterList = GetMakeParameters(table, false);
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("\t\t\tList<DbParameter> parameters = new List<DbParameter> ");
            builder.AppendLine("\t\t\t{");
            builder.AppendLine(makePararmeterList);
            builder.AppendLine("\t\t\t};");

            if (settings.ReturnIdentityFromInserts)
            {
                builder.AppendLine("\r\n \t\t\tparameters.SelectIdentity();");
            }

            if (!settings.ReturnIdentityFromInserts)
            {
                builder.AppendLine(string.Format("\r\n \t\t\treturn _database.NonQuery(\"{0}_Insert\", parameters);", table.Name));
            }
            else
            {
                builder.AppendLine(string.Format("\r\n \t\t\t_database.NonQuery(\"{0}_Insert\", parameters); \r\n \t\t\t return parameters.Identity<int>(); ", table.Name));
            }

            _content = _content.Replace("<%[InsertMethod]%>", builder.ToString());
        }

        private void GenerateUpdateMethods(DatabaseTable table)
        {
            //<%[UpdateMethod]%> -- Token
            string makePararmeterList = GetMakeParameters(table, true);
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("\t\t\tList<DbParameter> parameters = new List<DbParameter> ");
            builder.AppendLine("\t\t\t{");
            builder.AppendLine(makePararmeterList);
            builder.AppendLine("\t\t\t};");
            builder.AppendLine(string.Format("\r\n \t\t\treturn _database.NonQuery(\"{0}_Update\", parameters);", table.Name));

            _content = _content.Replace("<%[UpdateMethod]%>", builder.ToString());
        }

        private void GenerateDeleteMethods(DatabaseTable table)
        {
            //<%[UpdateMethod]%> -- Token
            string makePararmeterList = GetMakeParametersForOnlyPrimaryKeys(table);
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("\t\t\tList<DbParameter> parameters = new List<DbParameter> ");
            builder.AppendLine("\t\t\t{");
            builder.AppendLine(makePararmeterList);
            builder.AppendLine("\t\t\t};");
            builder.AppendLine(string.Format("\r\n \t\t\treturn _database.NonQuery(\"{0}_Delete\", );", table.Name));

            _content = _content.Replace("<%[DeleteMethod]%>", builder.ToString());
        }

        private static string GetMakeParametersForOnlyPrimaryKeys(DatabaseTable table)
        {
            StringBuilder builder = new StringBuilder();

            for (int index = 0; index < table.Columns.Count; index++)
            {
                bool isLastItem = (index == table.Columns.Count - 1);
                string columnName = "@" + table.Columns[index].Name;

                if (table.Columns[index].IsPrimaryKey)
                {
                    builder.AppendLine(string.Format("\t\t\t\t_database.MakeParameter(\"{0}\",{1}){2}", columnName, table.Name.ToLower() + "." + table.Columns[index].Name, (isLastItem ? string.Empty : ",")));
                }
            }

            return builder.ToString();
        }

        private  void GetItem(DatabaseTable table)
        {
            StringBuilder builder = new StringBuilder();

            foreach (DatabaseColumn column in table.Columns)
            {
                builder.AppendLine(string.Format("\t\t\t{0}.{1} = {2};", table.Name.ToLower(), column.Name, SqlTypeConversion.FindConvertToMethodsBySqlType(column.SqlColumnType, "reader[\"" + column.Name + "\"]")));
            }

            _content = _content.Replace("<%[GetItemObjectPopulation]%>", builder.ToString());
        }

        private void GenerateSelectByPrimaryKey(DatabaseTable table)
        {
            string makePararmeterList = GetMakeParametersForOnlyPrimaryKeys(table);
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("\t\t\tList<DbParameter> parameters = new List<DbParameter> ");
            builder.AppendLine("\t\t\t{");
            builder.AppendLine(makePararmeterList);
            builder.AppendLine("\t\t\t};");
            builder.AppendLine(string.Format("\r\n \t\t\treturn _database.PopulateItem(\"{0}_RetrieveByPrimaryKey\", parameters, o => Populate(o));", table.Name));

            _content = _content.Replace("<%[RetreveByPrimaryKeyMethod]%>", builder.ToString());
        }

        private void GenerateSelectAll(DatabaseTable table)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format("\r\n \t\t\treturn _database.PopulateCollection(\"{0}_RetrieveAll\", o => Populate(o));", table.Name));

            _content = _content.Replace("<%[RetreveAllMethod]%>", builder.ToString());
        }

        /// <summary>
        /// Generates the populate method.
        /// </summary>
        /// <param name="table">The table.</param>
        private void GeneratePopulateMethod(DatabaseTable table)
        {
            _content = _content.Replace("<%[LowerCaseTableName]%>", table.Name.ToLower());
            StringBuilder builder = new StringBuilder();

            for (int index = 0; index < table.Columns.Count; index++)
            {
                bool isLastItem = (index == table.Columns.Count - 1);

                const string populate = "\r\n \t\t\t\t\t\t\t\t\t\t\t\t\t\t{0} = {1}{2}";
                builder.Append(string.Format(populate, table.Columns[index].Name, SqlTypeConversion.FindConvertToMethodsBySqlType(table.Columns[index].SqlColumnType, table.Columns[index].Name), (isLastItem ? string.Empty : ",")));
            }

            _content = _content.Replace("<%[PopulateProperities]%>", builder.ToString());
        }

        private static string GetMakeParameters(DatabaseTable table, bool generatePrimaryKey)
        {
            StringBuilder builder = new StringBuilder();

            for(int index = 0; index < table.Columns.Count; index++)
            {                
                bool isLastItem = (index == table.Columns.Count - 1);
                string columnName = "@" + table.Columns[index].Name;

                if(generatePrimaryKey)
                {
                    builder.AppendLine(string.Format("\t\t\t\t\t_database.MakeParameter(\"{0}\",{1}){2}", columnName, table.Name.ToLower() + "." + table.Columns[index].Name, (isLastItem ? string.Empty : ",")));
                }
                else
                {
                    if(!table.Columns[index].IsPrimaryKey)
                    {
                        builder.AppendLine(string.Format("\t\t\t\t\t_database.MakeParameter(\"{0}\",{1}){2}", columnName, table.Name.ToLower() + "." + table.Columns[index].Name, (isLastItem ? string.Empty : ","))); 
                    }
                }
            }

            return builder.ToString();
        }
    }
}
