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
            this.btnBrowseTemplateFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTemplateFile = new System.Windows.Forms.TextBox();
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
            this.label2.Location = new System.Drawing.Point(10, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Output directory:";
            // 
            // txtOutputDirectory
            // 
            this.txtOutputDirectory.Location = new System.Drawing.Point(12, 138);
            this.txtOutputDirectory.Name = "txtOutputDirectory";
            this.txtOutputDirectory.Size = new System.Drawing.Size(388, 20);
            this.txtOutputDirectory.TabIndex = 3;
            this.txtOutputDirectory.Text = "C:\\temp\\ou-blogs";
            // 
            // btnBrowseOutputDirectory
            // 
            this.btnBrowseOutputDirectory.Location = new System.Drawing.Point(406, 136);
            this.btnBrowseOutputDirectory.Name = "btnBrowseOutputDirectory";
            this.btnBrowseOutputDirectory.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseOutputDirectory.TabIndex = 5;
            this.btnBrowseOutputDirectory.Text = "Browse...";
            this.btnBrowseOutputDirectory.UseVisualStyleBackColor = true;
            this.btnBrowseOutputDirectory.Click += new System.EventHandler(this.btnBrowseOutputDirectory_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(406, 191);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 6;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnBrowseTemplateFile
            // 
            this.btnBrowseTemplateFile.Location = new System.Drawing.Point(406, 81);
            this.btnBrowseTemplateFile.Name = "btnBrowseTemplateFile";
            this.btnBrowseTemplateFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseTemplateFile.TabIndex = 9;
            this.btnBrowseTemplateFile.Text = "Browse...";
            this.btnBrowseTemplateFile.UseVisualStyleBackColor = true;
            this.btnBrowseTemplateFile.Click += new System.EventHandler(this.btnBrowseTemplateFile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "OU Campus template file (.pcf):";
            // 
            // txtTemplateFile
            // 
            this.txtTemplateFile.Location = new System.Drawing.Point(12, 83);
            this.txtTemplateFile.Name = "txtTemplateFile";
            this.txtTemplateFile.Size = new System.Drawing.Size(388, 20);
            this.txtTemplateFile.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 223);
            this.Controls.Add(this.btnBrowseTemplateFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTemplateFile);
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
        private System.Windows.Forms.Button btnBrowseTemplateFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTemplateFile;
    }
}

