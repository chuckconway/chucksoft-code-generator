using System;
using System.Windows.Forms;
using Chucksoft.Resources.Data;

namespace Chucksoft.UI.CodeGeneration
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            rootNameSpaceTextBox.Text = User.Default.SolutionNamespace;
            outputDirectoryTextBox.Text = User.Default.CodeGenerationDirectory;
            connectionStringTextBox.Text = User.Default.DatabaseConnectionString;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
           User.Default.SolutionNamespace = rootNameSpaceTextBox.Text;
           User.Default.CodeGenerationDirectory = outputDirectoryTextBox.Text;
           User.Default.DatabaseConnectionString = connectionStringTextBox.Text;

            User.Default.Save();
            Close();
        }

        private void SetFolderPath(TextBox textbox)
        {
            folderPickerDialog.ShowNewFolderButton = true;
            DialogResult result = folderPickerDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                textbox.Text = folderPickerDialog.SelectedPath;
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            SetFolderPath(outputDirectoryTextBox);
        }

        private void testConnectionButton_Click(object sender, EventArgs e)
        {
            string message = "Connection Successful!";

            try
            {
                DatabaseHelper helper = new DatabaseHelper(connectionStringTextBox.Text);
                helper.IsValidConnectionString();

            }
            catch (Exception ex)
            {
                message = ex.Message;
                MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            MessageBox.Show(this, message, "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
