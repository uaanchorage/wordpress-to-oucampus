using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using PressSharper;

namespace WordPressToOUCampus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowseWordPressExportFile_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "WordPress Export Files|*.xml";
            var dialogResult = fileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                txtWordPressExportFile.Text = fileDialog.FileName;
            }
        }

        private void btnBrowseTemplateFile_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "OU Campus Template Files|*.pcf";
            var dialogResult = fileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                txtTemplateFile.Text = fileDialog.FileName;
            }
        }

        private void btnBrowseOutputDirectory_Click(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();
            var dialogResult = folderDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                txtOutputDirectory.Text = folderDialog.SelectedPath;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWordPressExportFile.Text))
            {
                MessageBox.Show("Please select a WordPress export file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtWordPressExportFile.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTemplateFile.Text))
            {
                MessageBox.Show("Please select a template file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTemplateFile.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtOutputDirectory.Text))
            {
                MessageBox.Show("Please select an output directory", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOutputDirectory.Focus();
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                if (!Directory.Exists(txtOutputDirectory.Text))
                    Directory.CreateDirectory(txtOutputDirectory.Text);

                string xml = File.ReadAllText(txtWordPressExportFile.Text);
                var blog = new Blog(xml);
                var posts = blog.GetPosts();

                string postTemplate = File.ReadAllText(txtTemplateFile.Text);

                foreach (var post in posts)
                {
                    string outputFile = postTemplate;

                    // replace template fields
                    outputFile = outputFile.Replace("{{post-title}}", post.Title);
                    outputFile = outputFile.Replace("{{post-date}}", post.PublishDate.ToString("MM/dd/yyyy hh:mm:ss tt"));
                    outputFile = outputFile.Replace("{{post-excerpt}}", post.Excerpt);
                    outputFile = outputFile.Replace("{{post-author}}", post.Author.DisplayName);
                    outputFile = outputFile.Replace("{{post-author-email}}", post.Author.Email);
                    outputFile = outputFile.Replace("{{post-categories}}", string.Join(",", post.Categories.Select(c => c.Name)));
                    outputFile = outputFile.Replace("{{post-image-display}}", post.FeaturedImage != null ? "img" : "none");
                    outputFile = outputFile.Replace("{{post-image-src}}", post.FeaturedImage?.Url);
                    outputFile = outputFile.Replace("{{post-image-alt}}", post.FeaturedImage?.Title);

                    // cleanup HTML encoding in body
                    string postBody = post.Body;
                    postBody = postBody.Replace(" & ", " &amp; ");
                    outputFile = outputFile.Replace("{{post-body}}", postBody);

                    // organize posts into directories based on (first) category
                    string category = post.Categories.FirstOrDefault()?.Slug ?? "uncategorized";
                    string postDirectory = txtOutputDirectory.Text + "\\posts\\" + category;
                    if (!Directory.Exists(postDirectory))
                        Directory.CreateDirectory(postDirectory);

                    // write .pcf file
                    string postSlug = post.Slug.Replace("_", "-");
                    string filePath = postDirectory + "\\" + postSlug + ".pcf";
                    File.WriteAllText(filePath, outputFile);
                    File.SetLastWriteTime(filePath, post.PublishDate);
                }
            }
            catch (Exception ex)
            {
                // TODO: handle error
                throw;
            }

            Cursor.Current = Cursors.Default;
        }
    }
}
