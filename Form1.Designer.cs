namespace WordPressToOUCampus
{
    partial class Form1
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
            this.txtWordPressExportFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowseWordPressExportFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOutputDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowseOutputDirectory = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnBrowsePostTemplate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPostTemplate = new System.Windows.Forms.TextBox();
            this.cbImportPosts = new System.Windows.Forms.CheckBox();
            this.cbImportPages = new System.Windows.Forms.CheckBox();
            this.btnBrowsePageTemplate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPageTemplate = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtWordPressExportFile
            // 
            this.txtWordPressExportFile.Location = new System.Drawing.Point(12, 31);
            this.txtWordPressExportFile.Name = "txtWordPressExportFile";
            this.txtWordPressExportFile.Size = new System.Drawing.Size(388, 20);
            this.txtWordPressExportFile.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "WordPress export file (.xml):";
            // 
            // btnBrowseWordPressExportFile
            // 
            this.btnBrowseWordPressExportFile.Location = new System.Drawing.Point(406, 29);
            this.btnBrowseWordPressExportFile.Name = "btnBrowseWordPressExportFile";
            this.btnBrowseWordPressExportFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseWordPressExportFile.TabIndex = 2;
            this.btnBrowseWordPressExportFile.Text = "Browse...";
            this.btnBrowseWordPressExportFile.UseVisualStyleBackColor = true;
            this.btnBrowseWordPressExportFile.Click += new System.EventHandler(this.btnBrowseWordPressExportFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 217);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Output directory:";
            // 
            // txtOutputDirectory
            // 
            this.txtOutputDirectory.Location = new System.Drawing.Point(13, 236);
            this.txtOutputDirectory.Name = "txtOutputDirectory";
            this.txtOutputDirectory.Size = new System.Drawing.Size(388, 20);
            this.txtOutputDirectory.TabIndex = 3;
            this.txtOutputDirectory.Text = "C:\\temp\\oucampus";
            // 
            // btnBrowseOutputDirectory
            // 
            this.btnBrowseOutputDirectory.Location = new System.Drawing.Point(407, 234);
            this.btnBrowseOutputDirectory.Name = "btnBrowseOutputDirectory";
            this.btnBrowseOutputDirectory.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseOutputDirectory.TabIndex = 5;
            this.btnBrowseOutputDirectory.Text = "Browse...";
            this.btnBrowseOutputDirectory.UseVisualStyleBackColor = true;
            this.btnBrowseOutputDirectory.Click += new System.EventHandler(this.btnBrowseOutputDirectory_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(407, 289);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 6;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnBrowsePostTemplate
            // 
            this.btnBrowsePostTemplate.Enabled = false;
            this.btnBrowsePostTemplate.Location = new System.Drawing.Point(406, 103);
            this.btnBrowsePostTemplate.Name = "btnBrowsePostTemplate";
            this.btnBrowsePostTemplate.Size = new System.Drawing.Size(75, 23);
            this.btnBrowsePostTemplate.TabIndex = 9;
            this.btnBrowsePostTemplate.Text = "Browse...";
            this.btnBrowsePostTemplate.UseVisualStyleBackColor = true;
            this.btnBrowsePostTemplate.Click += new System.EventHandler(this.btnBrowsePostTemplate_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "OU Campus post template file (.pcf):";
            // 
            // txtPostTemplate
            // 
            this.txtPostTemplate.Enabled = false;
            this.txtPostTemplate.Location = new System.Drawing.Point(12, 105);
            this.txtPostTemplate.Name = "txtPostTemplate";
            this.txtPostTemplate.Size = new System.Drawing.Size(388, 20);
            this.txtPostTemplate.TabIndex = 7;
            // 
            // cbImportPosts
            // 
            this.cbImportPosts.AutoSize = true;
            this.cbImportPosts.Location = new System.Drawing.Point(12, 66);
            this.cbImportPosts.Name = "cbImportPosts";
            this.cbImportPosts.Size = new System.Drawing.Size(83, 17);
            this.cbImportPosts.TabIndex = 10;
            this.cbImportPosts.Text = "Import posts";
            this.cbImportPosts.UseVisualStyleBackColor = true;
            this.cbImportPosts.CheckedChanged += new System.EventHandler(this.cbImportPosts_CheckedChanged);
            // 
            // cbImportPages
            // 
            this.cbImportPages.AutoSize = true;
            this.cbImportPages.Location = new System.Drawing.Point(12, 141);
            this.cbImportPages.Name = "cbImportPages";
            this.cbImportPages.Size = new System.Drawing.Size(87, 17);
            this.cbImportPages.TabIndex = 14;
            this.cbImportPages.Text = "Import pages";
            this.cbImportPages.UseVisualStyleBackColor = true;
            this.cbImportPages.CheckedChanged += new System.EventHandler(this.cbImportPages_CheckedChanged);
            // 
            // btnBrowsePageTemplate
            // 
            this.btnBrowsePageTemplate.Enabled = false;
            this.btnBrowsePageTemplate.Location = new System.Drawing.Point(406, 178);
            this.btnBrowsePageTemplate.Name = "btnBrowsePageTemplate";
            this.btnBrowsePageTemplate.Size = new System.Drawing.Size(75, 23);
            this.btnBrowsePageTemplate.TabIndex = 13;
            this.btnBrowsePageTemplate.Text = "Browse...";
            this.btnBrowsePageTemplate.UseVisualStyleBackColor = true;
            this.btnBrowsePageTemplate.Click += new System.EventHandler(this.btnBrowsePageTemplate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(180, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "OU Campus page template file (.pcf):";
            // 
            // txtPageTemplate
            // 
            this.txtPageTemplate.Enabled = false;
            this.txtPageTemplate.Location = new System.Drawing.Point(12, 180);
            this.txtPageTemplate.Name = "txtPageTemplate";
            this.txtPageTemplate.Size = new System.Drawing.Size(388, 20);
            this.txtPageTemplate.TabIndex = 11;
            // 
            // Form1
            // 
            this.AcceptButton = this.btnImport;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 322);
            this.Controls.Add(this.cbImportPages);
            this.Controls.Add(this.btnBrowsePageTemplate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPageTemplate);
            this.Controls.Add(this.cbImportPosts);
            this.Controls.Add(this.btnBrowsePostTemplate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPostTemplate);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnBrowseOutputDirectory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOutputDirectory);
            this.Controls.Add(this.btnBrowseWordPressExportFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtWordPressExportFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "WordPress to OU Campus Importer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtWordPressExportFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowseWordPressExportFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOutputDirectory;
        private System.Windows.Forms.Button btnBrowseOutputDirectory;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnBrowsePostTemplate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPostTemplate;
        private System.Windows.Forms.CheckBox cbImportPosts;
        private System.Windows.Forms.CheckBox cbImportPages;
        private System.Windows.Forms.Button btnBrowsePageTemplate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPageTemplate;
    }
}

