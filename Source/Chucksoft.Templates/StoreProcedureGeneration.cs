using System.Collections.Generic;
using Chucksoft.Entities;
using Chucksoft.Entities.Database;
using Chucksoft.Entities.Execptions;
using Chucksoft.Entities.GenerationTemplates;
using Chucksoft.Entities.GenerationTemplates.Dynamic;
using System;
using System.Linq;
using System.Text;

namespace Chucksoft.Templates
{
    public class StoreProcedureGeneration : DynamicSolutionAsset
    {
        //Generates one or many primaryKeys
        const string _author = "Chucksoft CodeGen";
        readonly string _createdate = DateTime.Now.ToLongDateString();

        //set the local content variable, so the stored procedure template does not get stepped on.
        private string _content; 

        public StoreProcedureGeneration()
        {
            FolderName = @"StoreProcedures\";
        }

        public override GenerationArtifact RenderTemplate(DatabaseTable table, CodeGenSettings settings)
        {
            //retrieve the embedded template
            string contents = Core.ResourceFileHelper.ConvertStreamResourceToUTF8String(typeof(Model), "Chucksoft.Templates.Templates.StoredProcedure.template");
            _content = contents.Trim();

            //Check for expected content, if not found, badness has happen.
            if (string.IsNullOrEmpty(contents))
            {
                throw new ContentNotFound("Can't find embeddedResource \"Chucksoft.Templates.Templates.Enity.template\"");
            }

            List<string> prodecures = GetProcedures(table, settings);
            StringBuilder contentBuilder = new StringBuilder();

            //append the procedures together and seperate them with a seperator
            foreach (string prodecure in prodecures)
            {
                contentBuilder.AppendLine(prodecure);
                contentBuilder.AppendLine(Environment.NewLine + "-----------------------------------------------------------" + Environment.NewLine);
            }

            //set the Generator object and return to calling method.
            GenerationArtifact artifact = new GenerationArtifact { FileName = string.Format("{0}.sql", table.Name), Content = contentBuilder.ToString() };
            return artifact;
        }

        private List<string> GetProcedures(DatabaseTable table, CodeGenSettings settings)
        {
            List<string> prodecures = new List<string>();

            //Add the generated Insert statement to the collection
            string insertProcedure = GenerateInsertStatement(table, settings);
            prodecures.Add(insertProcedure);

            const string deleteProcedureName = "Delete";
            string deleteSql = string.Format("\tDelete From {0}", table.Name);
            string deleteProcedure = GenerateDeleteStatementByPrimaryKey(table, deleteProcedureName, deleteSql);
            prodecures.Add(deleteProcedure);

            //Generate the SelectByPrimaryKey procedure
            string selectSql = string.Format("\tSelect {0} \r\n\tFrom {1}", GetColumns(table.Columns, false), table.Name);
            const string selectProcedureName = "SelectByPrimaryKey";
            string selectByPrimaryKey = GenerateDeleteStatementByPrimaryKey(table, selectProcedureName, selectSql);
            prodecures.Add(selectByPrimaryKey);

            //generate the Update procedure
            string updateSql = string.Format("\tUpdate {0} \r\n\tSET {1}", table.Name, GetUpdateColumns(table));
            string updateParameterList = GenerateParameterList(table, true, string.Empty);
            const string updateProcedureName = "Update";
            string updateProcedure = GenerateUpdateProcedure(table, updateProcedureName, updateSql, updateParameterList);
            prodecures.Add(updateProcedure);

            //generate the SelectAll procedure
            string selectAllSql = string.Format("\tSelect {0} \r\n\tFrom {1}", GetColumns(table.Columns, false), table.Name);
            const string selectAllProcedureName = "SelectAll";
            string selectAllProcedure = GenerateSelectAllProcedure(table, selectAllProcedureName,  selectAllSql);

            prodecures.Add(selectAllProcedure);
            return prodecures;
        }

        #region Insert Procedure

        /// <summary>
        /// Generates the InsertStatements
        /// </summary>
        /// <param name="table"></param>

        /// <returns>A complete insert statement</returns>
        private string GenerateInsertStatement(DatabaseTable table, CodeGenSettings settings)
        { 
            List<DatabaseColumn> columns = table.Columns.Where(c => !c.IsPrimaryKey).ToList();

            string procedureName = table.Name + "_Insert";
            string insertColumns = GetColumns(columns, false);
            string insertParameters = GetColumns(columns, true);

            string outIdentity = string.Empty;

            
            //build the insert statement
            string procedureStatement = string.Format("\tINSERT INTO [dbo].[{0}] ({1}) \r\n\tVALUES ({2})", table.Name, insertColumns, insertParameters);

            if (settings.ReturnIdentityFromInserts)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("\r\n\t@Identity INT OUTPUT = null");

                outIdentity = builder.ToString();

                StringBuilder builder1 = new StringBuilder(procedureStatement);
                const string newLine = "\r\n\t";
                
                const string sql = newLine + newLine + "IF @Identity IS NOT NULL" +
                                   newLine + "BEGIN" +
                                   newLine + "\tSet @Identity = SELECT SCOPE_IDENTITY()" +
                                   newLine + "END";

                builder1.AppendLine(sql);
                procedureStatement = builder1.ToString();
                
            }

            string insertStatementSignatureParameters = GenerateParameterList(table, false, outIdentity);

            //replace tokens in the stored procedure template
            string content = ProcedureTokenReplacement(procedureName, procedureStatement, insertStatementSignatureParameters);

            return content;
        }

        /// <summary>
        /// Replaces all the tokens in the Stored Procedure Template
        /// </summary>

        /// <param name="procedureName">Name of the procedure, this is what the procedure will be called in the database</param>
        /// <param name="procedureStatement">The constructed statement, including all the variables</param>
        /// <param name="insertStatementSignatureParameters">the parameter list</param>
        /// <returns>the complete procedure</returns>
        private string ProcedureTokenReplacement(string procedureName, string procedureStatement, string insertStatementSignatureParameters)
        {
            string content = _content;

            //replace tokens in the stored procedure template
            content = content.Replace("<%[Author]%>", _author);
            content = content.Replace("<%[CreateDate]%>", _createdate);
            content = content.Replace("<%[ProcedureName]%>", procedureName);
            content = content.Replace("<%[ProcedureStatement]%>", procedureStatement);
            content = content.Replace("<%[ProcedureParameters]%>", insertStatementSignatureParameters);

            return content;
        }

        /// <summary>
        /// Builds the Insert Statment parmeters
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="generationPrimaryKey">if set to <c>true</c> [generation primary key].</param>
        /// <param name="appendToTheEnd">The append to the end.</param>
        /// <returns></returns>
        private static string GenerateParameterList(DatabaseTable table, bool generationPrimaryKey, string appendToTheEnd)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("( ");

            //iterate through columns
            for (int index = 0; index < table.Columns.Count; index++)
            {
                bool isLastColumn = (index == table.Columns.Count - 1);

                if (generationPrimaryKey)
                {
                    builder.AppendLine(GenrateParameterColumn(table.Columns[index], isLastColumn));
                }
                else
                {
                    if(isLastColumn && !string.IsNullOrEmpty(appendToTheEnd))
                    {
                        string column = GenerateColumsWithoutPrimaryKey(table.Columns[index], false);
                        column += appendToTheEnd;

                        builder.AppendLine(column);
                    }
                    else
                    {
                        string column = GenerateColumsWithoutPrimaryKey(table.Columns[index], isLastColumn);

                        if(!string.IsNullOrEmpty(column))
                        {
                            builder.AppendLine(column);
                        }
                    }

                }
            }
            
            
            builder.AppendLine(") ");
            return builder.ToString();
        }

        private static string GenerateColumsWithoutPrimaryKey(DatabaseColumn databaseColumn, bool isLastColumn)
        {
            string column = string.Empty;

            //Ignore primary keys. I'm assuming they will be auto generated
            if (!databaseColumn.IsPrimaryKey)
            {
                column = GenrateParameterColumn(databaseColumn, isLastColumn);
            }

            return column;
        }

        private static string GenrateParameterColumn(DatabaseColumn databaseColumn, bool isLastColumn)
        {
            //Last item in the collection 
            string column = (isLastColumn ? string.Format("\t@{0} {1}", databaseColumn.Name, databaseColumn.SqlColumnTypeAndSize) : string.Format("\t@{0} {1},", databaseColumn.Name, databaseColumn.SqlColumnTypeAndSize));
            return column;
        }

        private static string GetColumns(List<DatabaseColumn> columns, bool addparameterSymbol)
        {
            StringBuilder builder = new StringBuilder();
            string parameterSymbol = (addparameterSymbol ? "@" : string.Empty);

            for (int index = 0; index < columns.Count; index++)
            {
                //determine whether to add or not to add the trialing comma
                if (index == 0)
                {
                    builder.AppendFormat("{0}{1}", parameterSymbol, columns[index].Name);
                }
                else
                {
                    builder.AppendFormat(", {0}{1}", parameterSymbol, columns[index].Name);
                }
            }

            return builder.ToString();
        } 
        #endregion

        private string GenerateDeleteStatementByPrimaryKey(DatabaseTable table, string procedureName, string statement)
        {
            List<DatabaseColumn> primaryKeyColumns = RetrievePrimaryKeys(table);

            //Generates one or many primaryKeys
            string primaryKey = GeneratePrimaryKeyParameterList(primaryKeyColumns);
            string _procedureName = string.Format("{0}_{1}", table.Name, procedureName);
            statement += GenerateWhereClause(primaryKeyColumns);

            string content = ProcedureTokenReplacement( _procedureName, statement, primaryKey);
            return content;
            
        }

        private static string GetUpdateColumns(DatabaseTable table)
        {
            StringBuilder builder = new StringBuilder();

            for (int index = 0; index < table.Columns.Count; index++)
            {
                bool isLastColumn = (index == table.Columns.Count - 1);
                builder.AppendLine(string.Format("\t{0} = @{1}{2}", table.Columns[index].Name, table.Columns[index].Name, (!isLastColumn ? "," : string.Empty)));

            }

            return builder.ToString();
        }

        private  string GenerateUpdateProcedure(DatabaseTable table, string procedureName, string statement, string parameterList)
        {
            List<DatabaseColumn> primaryKeyColumns = RetrievePrimaryKeys(table);

            //Generates one or many primaryKeys
            string _procedureName = string.Format("{0}_{1}", table.Name, procedureName);
            statement += GenerateWhereClause(primaryKeyColumns);
            string content = ProcedureTokenReplacement(_procedureName, statement, parameterList);

            return content;

        }

        private string GenerateSelectAllProcedure(DatabaseTable table, string procedureName, string statement)
        {
            //set the local content variable, so the stored procedure template does not get stepped on.
            string _procedureName = string.Format("{0}_{1}", table.Name, procedureName);
            string content = ProcedureTokenReplacement(_procedureName, statement, string.Empty);

            return content;
        }

        /// <summary>
        /// Generates primary Key Where clause
        /// </summary>
        /// <param name="primaryKeyColumns"></param>
        /// <returns></returns>
        private static string GenerateWhereClause(IList<DatabaseColumn> primaryKeyColumns)
        {
            string whereClause = string.Empty;

            //no columns in the collection, then no love
            if(primaryKeyColumns.Count > 0)
            {
                StringBuilder builder = new StringBuilder();

                //Add a tab and new line
                builder.Append(" \r\n\tWhere ");

                for(int index = 0; index < primaryKeyColumns.Count; index++)
                {
                    bool isLastColumn = (index == primaryKeyColumns.Count - 1);

                    //formatting goodness
                    string clause = string.Format("{0} = @{1} ", primaryKeyColumns[index].Name, primaryKeyColumns[index].Name);
                    builder.Append(clause);

                    //If there is more than one, we need a "AND"
                    if(!isLastColumn)
                    {
                        builder.Append("And");
                    }
                }

                whereClause = builder.ToString();
            }

            return whereClause;
        }

        /// <summary>
        /// Extract the primary keys from the table.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private static List<DatabaseColumn> RetrievePrimaryKeys(DatabaseTable table)
        {
            List<DatabaseColumn> primaryKeyColumns = new List<DatabaseColumn>();
            
            //loop through the columns
            foreach (DatabaseColumn column in table.Columns)
            {
                //it's pretty simple, if it's a primary key, then add it to the collection
                if(column.IsPrimaryKey)
                {
                    primaryKeyColumns.Add(column);
                }
            }

            return primaryKeyColumns;
        }


        /// <summary>
        /// Generates the primary keys as parameters
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        private static string GeneratePrimaryKeyParameterList(IList<DatabaseColumn> columns)
        {
            StringBuilder builder = new StringBuilder();

            //If there are no primary keys, then the rest us useless
            if (columns.Count > 0)
            {
                //the opening bracket for the parameter list
                builder.AppendLine("( ");

                for (int index = 0; index < columns.Count; index++)
                {
                    //check for the last column, last column does not get a comma appended
                    bool isLastColumn = (index == columns.Count - 1);

                    //again it must be a primary key
                    if (columns[index].IsPrimaryKey)
                    {
                        builder.AppendLine(GenrateParameterColumn(columns[index], isLastColumn));
                    }
                }

                //the closing bracket for the parameterList
                builder.AppendLine(") ");
            }

            return builder.ToString();
        }
    }
}
