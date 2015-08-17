using System.Text;
using Chucksoft.Entities;
using Chucksoft.Entities.Database;
using Chucksoft.Entities.Execptions;
using Chucksoft.Entities.GenerationTemplates.Dynamic;
using Chucksoft.Resources.Data;

using System.Linq;

namespace Chucksoft.Templates
{
    public class DynamicDbParametersResources : IGenerateMethods
    {
        private string _content;

        public string Render(DatabaseTable table, CodeGenSettings settings)
        {
            _content = Core.ResourceFileHelper.ConvertStreamResourceToUTF8String(typeof(Resources), "Chucksoft.Templates.Templates.ResourcesWithoutPopulate.template");

            //Check for expected content, if not found, badness has happen.
            if (string.IsNullOrEmpty(_content))
            {
                throw new ContentNotFound("Can't find embeddedResource \"Chucksoft.Templates.Templates.ResourcesWithoutPopulate.template\"");
            }

            //GetItem(table);
            GenerateDeleteMethods(table);
            GenerateInsertMethods(table, settings);
            GenerateUpdateMethods(table);
            GenerateSelectAll(table);
            GenerateSelectByPrimaryKey(table);
            //GeneratePopulateMethod(table);
            _content = _content.Replace("<%[TableName]%>", table.Name);
            _content = _content.Replace("<%[TableParameter]%>", table.Name.ToLower());


            return _content;
        }

        private void GenerateInsertMethods(DatabaseTable table, CodeGenSettings settings)
        {
            StringBuilder builder = new StringBuilder();

            if (settings.ReturnIdentityFromInserts)
            {
                builder.AppendLine(string.Format("\t\t\tList<DbParameter> parameters = database.GetParameters({0});", table.Name));
                builder.AppendLine("\r\n \t\t\tparameters.SelectIdentity();");
            }

            if (!settings.ReturnIdentityFromInserts)
            {
                builder.Append(string.Format("\t\t\treturn _database.NonQuery(\"{0}_Insert\", {1});", table.Name, table.Name.ToLower()));
            }
            else
            {
                builder.Append(string.Format("\r\n \t\t\t_database.NonQuery(\"{0}_Insert\", parameters); \r\n \t\t\t return parameters.Identity<int>(); ", table.Name));
            }

            _content = _content.Replace("<%[InsertMethod]%>", builder.ToString());
        }

        private void GenerateUpdateMethods(DatabaseTable table)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format("\t\t\treturn _database.NonQuery(\"{0}_Update\", {1});", table.Name, table.Name.ToLower()));

            _content = _content.Replace("<%[UpdateMethod]%>", builder.ToString());
        }

        private void GenerateDeleteMethods(DatabaseTable table)
        {
            DatabaseColumn primaryColumn = table.Columns.Where(c => c.IsPrimaryKey).SingleOrDefault();

            if (primaryColumn != null)
            {
                StringBuilder builder = new StringBuilder();
               builder.Append(string.Format("\t\t\treturn _database.NonQuery(\"{0}_Delete\", new {{ {1} }});", table.Name, table.Name.ToLower() + "." + primaryColumn.Name));

                _content = _content.Replace("<%[DeleteMethod]%>", builder.ToString());
            }
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


        private void GenerateSelectByPrimaryKey(DatabaseTable table)
        {
            string makePararmeterList = GetMakeParametersForOnlyPrimaryKeys(table);
            StringBuilder builder = new StringBuilder();

            DatabaseColumn primaryColumn = table.Columns.Where(c => c.IsPrimaryKey).SingleOrDefault();

            if (primaryColumn != null)
            {

                builder.AppendLine(string.Format("\r\n \t\t\treturn _database.PopulateItem(\"{0}_RetrieveByPrimaryKey\", new {{ {1} }}, _database.AutoPopulate<{0}>);", table.Name, primaryColumn.Name + " = key"));

            _content = _content.Replace("<%[RetreveByPrimaryKeyMethod]%>", builder.ToString());

            }
        }

        private void GenerateSelectAll(DatabaseTable table)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format("\r\n \t\t\treturn _database.PopulateCollection(\"{0}_RetrieveAll\", _database.AutoPopulate<{0}>);", table.Name));

            _content = _content.Replace("<%[RetreveAllMethod]%>", builder.ToString());
        }

    }
}
