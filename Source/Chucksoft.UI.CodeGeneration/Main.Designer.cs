namespace Chucksoft.UI.CodeGeneration
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.generateButton = new System.Windows.Forms.Button();
            this.databaseTablesListBox = new System.Windows.Forms.ListBox();
            this.generationTablesListBox = new System.Windows.Forms.ListBox();
            this.moveToRight = new System.Windows.Forms.Button();
            this.moveToLeft = new System.Windows.Forms.Button();
            this.tables = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.loadDatabaseDropDown = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.databasesComboBox = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusHoverTipLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.codeGenerationPath = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.databaseConnectionString = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.solutionNamespaceText = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.useDynamicParameters = new System.Windows.Forms.CheckBox();
            this.returnIdentityFromInserts = new System.Windows.Forms.CheckBox();
            this.tables.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(518, 433);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(94, 32);
            this.generateButton.TabIndex = 0;
            this.generateButton.Tag = "";
            this.generateButton.Text = "Generate";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            this.generateButton.MouseLeave += new System.EventHandler(this.ClearStatusText_MouseLeave);
            this.generateButton.MouseHover += new System.EventHandler(this.generateButton_MouseHover);
            // 
            // databaseTablesListBox
            // 
            this.databaseTablesListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.databaseTablesListBox.ForeColor = System.Drawing.Color.Black;
            this.databaseTablesListBox.FormattingEnabled = true;
            this.databaseTablesListBox.ItemHeight = 16;
            this.databaseTablesListBox.Location = new System.Drawing.Point(17, 90);
            this.databaseTablesListBox.Name = "databaseTablesListBox";
            this.databaseTablesListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.databaseTablesListBox.Size = new System.Drawing.Size(229, 212);
            this.databaseTablesListBox.TabIndex = 5;
            this.databaseTablesListBox.MouseLeave += new System.EventHandler(this.ClearStatusText_MouseLeave);
            this.databaseTablesListBox.MouseHover += new System.EventHandler(this.databaseTablesListBox_MouseHover);
            // 
            // generationTablesListBox
            // 
            this.generationTablesListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generationTablesListBox.ForeColor = System.Drawing.Color.Black;
            this.generationTablesListBox.FormattingEnabled = true;
            this.generationTablesListBox.ItemHeight = 16;
            this.generationTablesListBox.Location = new System.Drawing.Point(351, 90);
            this.generationTablesListBox.Name = "generationTablesListBox";
            this.generationTablesListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.generationTablesListBox.Size = new System.Drawing.Size(230, 212);
            this.generationTablesListBox.TabIndex = 6;
            this.generationTablesListBox.MouseLeave += new System.EventHandler(this.ClearStatusText_MouseLeave);
            this.generationTablesListBox.MouseHover += new System.EventHandler(this.generationTablesListBox_MouseHover);
            // 
            // moveToRight
            // 
            this.moveToRight.Location = new System.Drawing.Point(270, 161);
            this.moveToRight.Name = "moveToRight";
            this.moveToRight.Size = new System.Drawing.Size(59, 23);
            this.moveToRight.TabIndex = 7;
            this.moveToRight.Text = "Add >>";
            this.moveToRight.UseVisualStyleBackColor = true;
            this.moveToRight.Click += new System.EventHandler(this.moveToRight_Click);
            this.moveToRight.MouseLeave += new System.EventHandler(this.ClearStatusText_MouseLeave);
            this.moveToRight.MouseHover += new System.EventHandler(this.moveToRight_MouseHover);
            // 
            // moveToLeft
            // 
            this.moveToLeft.Location = new System.Drawing.Point(270, 190);
            this.moveToLeft.Name = "moveToLeft";
            this.moveToLeft.Size = new System.Drawing.Size(59, 23);
            this.moveToLeft.TabIndex = 8;
            this.moveToLeft.Text = "Remove";
            this.moveToLeft.UseVisualStyleBackColor = true;
            this.moveToLeft.Click += new System.EventHandler(this.moveToLeft_Click);
            this.moveToLeft.MouseLeave += new System.EventHandler(this.ClearStatusText_MouseLeave);
            this.moveToLeft.MouseHover += new System.EventHandler(this.moveToLeft_MouseHover);
            // 
            // tables
            // 
            this.tables.Controls.Add(this.label7);
            this.tables.Controls.Add(this.label6);
            this.tables.Controls.Add(this.loadDatabaseDropDown);
            this.tables.Controls.Add(this.label4);
            this.tables.Controls.Add(this.databasesComboBox);
            this.tables.Controls.Add(this.databaseTablesListBox);
            this.tables.Controls.Add(this.moveToLeft);
            this.tables.Controls.Add(this.generationTablesListBox);
            this.tables.Controls.Add(this.moveToRight);
            this.tables.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tables.Location = new System.Drawing.Point(16, 105);
            this.tables.Name = "tables";
            this.tables.Size = new System.Drawing.Size(596, 322);
            this.tables.TabIndex = 9;
            this.tables.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(354, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Generation Queue";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(20, 71);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Tables";
            // 
            // loadDatabaseDropDown
            // 
            this.loadDatabaseDropDown.Location = new System.Drawing.Point(526, 36);
            this.loadDatabaseDropDown.Name = "loadDatabaseDropDown";
            this.loadDatabaseDropDown.Size = new System.Drawing.Size(55, 23);
            this.loadDatabaseDropDown.TabIndex = 11;
            this.loadDatabaseDropDown.Text = "Load";
            this.loadDatabaseDropDown.UseVisualStyleBackColor = true;
            this.loadDatabaseDropDown.Click += new System.EventHandler(this.loadDatabaseDropDown_Click);
            this.loadDatabaseDropDown.MouseLeave += new System.EventHandler(this.ClearStatusText_MouseLeave);
            this.loadDatabaseDropDown.MouseHover += new System.EventHandler(this.loadDatabaseDropDown_MouseHover);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Databases";
            // 
            // databasesComboBox
            // 
            this.databasesComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.databasesComboBox.FormattingEnabled = true;
            this.databasesComboBox.Location = new System.Drawing.Point(17, 35);
            this.databasesComboBox.Name = "databasesComboBox";
            this.databasesComboBox.Size = new System.Drawing.Size(503, 24);
            this.databasesComboBox.TabIndex = 9;
            this.databasesComboBox.SelectedIndexChanged += new System.EventHandler(this.databasesComboBox_SelectedIndexChanged);
            this.databasesComboBox.MouseLeave += new System.EventHandler(this.ClearStatusText_MouseLeave);
            this.databasesComboBox.MouseHover += new System.EventHandler(this.databasesComboBox_MouseHover);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusHoverTipLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 478);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(628, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusHoverTipLabel
            // 
            this.statusHoverTipLabel.Name = "statusHoverTipLabel";
            this.statusHoverTipLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(628, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.settingsToolStripMenuItem.Text = "Settings...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.codeGenerationPath);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.databaseConnectionString);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.solutionNamespaceText);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(596, 72);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // codeGenerationPath
            // 
            this.codeGenerationPath.AutoSize = true;
            this.codeGenerationPath.Location = new System.Drawing.Point(148, 50);
            this.codeGenerationPath.Name = "codeGenerationPath";
            this.codeGenerationPath.Size = new System.Drawing.Size(77, 13);
            this.codeGenerationPath.TabIndex = 8;
            this.codeGenerationPath.TabStop = true;
            this.codeGenerationPath.Text = "Open Location";
            this.codeGenerationPath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.generationDirectory_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Code Generation Directory:";
            // 
            // databaseConnectionString
            // 
            this.databaseConnectionString.AutoSize = true;
            this.databaseConnectionString.ForeColor = System.Drawing.Color.Gray;
            this.databaseConnectionString.Location = new System.Drawing.Point(149, 33);
            this.databaseConnectionString.Name = "databaseConnectionString";
            this.databaseConnectionString.Size = new System.Drawing.Size(35, 13);
            this.databaseConnectionString.TabIndex = 4;
            this.databaseConnectionString.Text = "label2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Database ConnectionString:";
            // 
            // solutionNamespaceText
            // 
            this.solutionNamespaceText.AutoSize = true;
            this.solutionNamespaceText.ForeColor = System.Drawing.Color.Gray;
            this.solutionNamespaceText.Location = new System.Drawing.Point(118, 17);
            this.solutionNamespaceText.Name = "solutionNamespaceText";
            this.solutionNamespaceText.Size = new System.Drawing.Size(35, 13);
            this.solutionNamespaceText.TabIndex = 1;
            this.solutionNamespaceText.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Solution Namespace:";
            // 
            // useDynamicParameters
            // 
            this.useDynamicParameters.AutoSize = true;
            this.useDynamicParameters.Location = new System.Drawing.Point(16, 433);
            this.useDynamicParameters.Name = "useDynamicParameters";
            this.useDynamicParameters.Size = new System.Drawing.Size(145, 17);
            this.useDynamicParameters.TabIndex = 16;
            this.useDynamicParameters.Text = "Use Dynamic Parameters";
            this.useDynamicParameters.UseVisualStyleBackColor = true;
            // 
            // returnIdentityFromInserts
            // 
            this.returnIdentityFromInserts.AutoSize = true;
            this.returnIdentityFromInserts.Location = new System.Drawing.Point(16, 457);
            this.returnIdentityFromInserts.Name = "returnIdentityFromInserts";
            this.returnIdentityFromInserts.Size = new System.Drawing.Size(152, 17);
            this.returnIdentityFromInserts.TabIndex = 17;
            this.returnIdentityFromInserts.Text = "Return Identity from Inserts";
            this.returnIdentityFromInserts.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 500);
            this.Controls.Add(this.returnIdentityFromInserts);
            this.Controls.Add(this.useDynamicParameters);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tables);
            this.Controls.Add(this.generateButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(644, 561);
            this.MinimizeBox = false;
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "C# Code Generator";
            this.tables.ResumeLayout(false);
            this.tables.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Button moveToLeft;
        private System.Windows.Forms.Button moveToRight;
        private System.Windows.Forms.ListBox generationTablesListBox;
        private System.Windows.Forms.ListBox databaseTablesListBox;
        private System.Windows.Forms.GroupBox tables;
        private System.Windows.Forms.Button loadDatabaseDropDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox databasesComboBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label solutionNamespaceText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label databaseConnectionString;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripStatusLabel statusHoverTipLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel codeGenerationPath;
        private System.Windows.Forms.CheckBox useDynamicParameters;
        private System.Windows.Forms.CheckBox returnIdentityFromInserts;
    }
}

