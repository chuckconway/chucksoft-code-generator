using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Chucksoft.Core.Windows;
using Chucksoft.Entities;
using Chucksoft.Entities.Database;
using Chucksoft.Resources.Data;
using Chucksoft.Logic;

namespace Chucksoft.UI.CodeGeneration
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void SetStatusText(string text)
        {
            statusHoverTipLabel.Text = text;
        }

        private void LoadSettings()
        {
            solutionNamespaceText.Text = User.Default.SolutionNamespace;
            databaseConnectionString.Text = User.Default.DatabaseConnectionString;
            codeGenerationPath.Text = User.Default.CodeGenerationDirectory;
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            List<DatabaseTable> databaseTables = new List<DatabaseTable>();

            
            //Retrieve Tables in generation listbox
            for(int index = 0; index < generationTablesListBox.Items.Count; index++ )
            {
                DatabaseTable table = new DatabaseTable();
                table.Name = Convert.ToString(generationTablesListBox.Items[index]);
                databaseTables.Add(table);
            }

            //Retrieve the columns in the selected tables
            DatabaseHelper helper = new DatabaseHelper(User.Default.DatabaseConnectionString);
            foreach (DatabaseTable table in databaseTables)
            {
                table.Columns = helper.RetrieveColumns(databasesComboBox.Text, table.Name);
            }


            //populate Settings class
            CodeGenSettings settings = new CodeGenSettings();
            settings.SolutionNamespace = User.Default.SolutionNamespace;
            settings.CodeGenerationDirectory = User.Default.CodeGenerationDirectory;
            settings.DatabaseConnectionString = User.Default.DatabaseConnectionString;
            settings.CompiledTemplateLocation = User.Default.CompiledTemplateLocation;
            settings.ReturnIdentityFromInserts = returnIdentityFromInserts.Checked;
            settings.UseDynamicParameters = useDynamicParameters.Checked;

            if (!string.IsNullOrEmpty(settings.CodeGenerationDirectory))
            {
                //Check that items have been added to the Generation Queue
                if (generationTablesListBox.Items.Count > 0)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        SolutionLogic.Generate(settings, databaseTables);

                        MessageBox.Show(this, "Generation Completed!", "Generation Completed!", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Cursor = Cursors.Arrow;
                    }
                }
                else
                {
                    MessageBox.Show(this, "Add tables to the generation queue", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(this, "A location to save the generated files is required. Please use the settings dialog to set the code generation path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void loadDatabaseDropDown_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            DatabaseHelper helper = new DatabaseHelper(User.Default.DatabaseConnectionString);
            LoadDropdownAndListBox(helper);

            Cursor = Cursors.Arrow;

            loadDatabaseDropDown.Text = "Refresh";
        }

        private void LoadDropdownAndListBox(DatabaseHelper helper)
        {
            databasesComboBox.DataSource = helper.RetrieveDatabaseNames();
            LoadListBox(helper);
        }

        private void LoadListBox(DatabaseHelper helper)
        {
            //Clear items from the ListBox
            databaseTablesListBox.Items.Clear();
            
            generationTablesListBox.Items.Clear();

            //Retrieve table names from Database and manually add them to the ListBox. DataBinding creates and error when the collection
            //is motified.
            List<string> tableNames = helper.RetrieveTables(databasesComboBox.Text);
            AddTableNamesToListBox(tableNames);
        }

        private void AddTableNamesToListBox(List<string> tableNames)
        {
            foreach (string name in tableNames)
            {
                databaseTablesListBox.Items.Add(name);
            }
        }
        
        private void MoveItem(ListBox from, ListBox to)
        {
            //collection of non-dupilcate items to be added or removed
            List<object> items = new List<object>();

            //iterate over all selected rows.
            for (int index = 0; index < from.SelectedItems.Count; index++)
            {
                object tableName = from.SelectedItems[index];
                bool hasDupulicate = false;
                
                //check for a duplicate
                for (int fromIndex = 0; fromIndex < to.Items.Count; fromIndex++)
                {
                    if(tableName.Equals(to.Items[fromIndex]))
                    {
                        hasDupulicate = true;
                        break;
                    }
                }

                //if it's not a dupicate then add to the other side.
                if(!hasDupulicate)
                {
                    items.Add(tableName);
                }
            }

            //added the non-duplicate items to the ListBox
            foreach (object obj in items)
            {
                from.Items.Remove(obj);
                to.Items.Add(obj);
            }

        }

        private void moveToLeft_Click(object sender, EventArgs e)
        {
            MoveItem(generationTablesListBox, databaseTablesListBox);
        }

        private void moveToRight_Click(object sender, EventArgs e)
        {
            MoveItem(databaseTablesListBox, generationTablesListBox);
        }


        /// <summary>
        /// Reloads the Listbox with the list of tables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void databasesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {DatabaseHelper helper = new DatabaseHelper(User.Default.DatabaseConnectionString);

            LoadListBox(helper);
        }

        /// <summary>
        /// opens the settings form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {  
            using(Settings settings = new Settings())
            {
                settings.ShowDialog(this);
            }

            LoadSettings();
        }

        #region Status Bar Tips
        private void moveToRight_MouseHover(object sender, EventArgs e)
        {
            SetStatusText("Add tables to the generation queue");
        }

        private void moveToLeft_MouseHover(object sender, EventArgs e)
        {
            SetStatusText("Remove tables from the generation queue");
        }

        private void ClearStatusText_MouseLeave(object sender, EventArgs e)
        {
            SetStatusText(string.Empty);
        }

        private void databaseTablesListBox_MouseHover(object sender, EventArgs e)
        {
            SetStatusText("List of database tables");
        }

        private void generationTablesListBox_MouseHover(object sender, EventArgs e)
        {
            SetStatusText("The generation queue");
        }

        private void loadDatabaseDropDown_MouseHover(object sender, EventArgs e)
        {
            SetStatusText("Loads or refreshes tables from the selected database");
        }

        private void databasesComboBox_MouseHover(object sender, EventArgs e)
        {
            SetStatusText("Collection of databases from selected database Server");
        }

        private void generateButton_MouseHover(object sender, EventArgs e)
        {
            SetStatusText("Click to generate code");
        } 
        #endregion

        private void generationDirectory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Explorer explorer = new Explorer(codeGenerationPath.Text);
            explorer.Open();
        }

    }
}
