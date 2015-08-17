namespace Chucksoft.UI.CodeGeneration
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.rootNameSpaceTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.folderPickerDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.testConnectionButton = new System.Windows.Forms.Button();
            this.connectionStringTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.outputDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Solution Namespace";
            // 
            // rootNameSpaceTextBox
            // 
            this.rootNameSpaceTextBox.Location = new System.Drawing.Point(12, 25);
            this.rootNameSpaceTextBox.Name = "rootNameSpaceTextBox";
            this.rootNameSpaceTextBox.Size = new System.Drawing.Size(140, 20);
            this.rootNameSpaceTextBox.TabIndex = 17;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(314, 166);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(83, 29);
            this.saveButton.TabIndex = 23;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // testConnectionButton
            // 
            this.testConnectionButton.Location = new System.Drawing.Point(316, 125);
            this.testConnectionButton.Name = "testConnectionButton";
            this.testConnectionButton.Size = new System.Drawing.Size(82, 23);
            this.testConnectionButton.TabIndex = 28;
            this.testConnectionButton.Text = "Connect!";
            this.testConnectionButton.UseVisualStyleBackColor = true;
            this.testConnectionButton.Click += new System.EventHandler(this.testConnectionButton_Click);
            // 
            // connectionStringTextBox
            // 
            this.connectionStringTextBox.BackColor = System.Drawing.Color.White;
            this.connectionStringTextBox.Location = new System.Drawing.Point(12, 128);
            this.connectionStringTextBox.Name = "connectionStringTextBox";
            this.connectionStringTextBox.Size = new System.Drawing.Size(297, 20);
            this.connectionStringTextBox.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Database Connection String";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(356, 76);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(41, 23);
            this.browseButton.TabIndex = 29;
            this.browseButton.Text = "...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // outputDirectoryTextBox
            // 
            this.outputDirectoryTextBox.BackColor = System.Drawing.Color.White;
            this.outputDirectoryTextBox.Location = new System.Drawing.Point(12, 78);
            this.outputDirectoryTextBox.Name = "outputDirectoryTextBox";
            this.outputDirectoryTextBox.ReadOnly = true;
            this.outputDirectoryTextBox.Size = new System.Drawing.Size(338, 20);
            this.outputDirectoryTextBox.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Code Generation Directory";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 216);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.outputDirectoryTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.testConnectionButton);
            this.Controls.Add(this.connectionStringTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rootNameSpaceTextBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(443, 244);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(418, 244);
            this.Name = "Settings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox rootNameSpaceTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.FolderBrowserDialog folderPickerDialog;
        private System.Windows.Forms.Button testConnectionButton;
        private System.Windows.Forms.TextBox connectionStringTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox outputDirectoryTextBox;
        private System.Windows.Forms.Label label1;
    }
}