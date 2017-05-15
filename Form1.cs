using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

        private void cbImportPosts_CheckedChanged(object sender, EventArgs e)
        {
            txtPostTemplate.Enabled = cbImportPosts.Checked;
            btnBrowsePostTemplate.Enabled = cbImportPosts.Checked;
        }

        private void cbImportPages_CheckedChanged(object sender, EventArgs e)
        {
            txtPageTemplate.Enabled = cbImportPages.Checked;
            btnBrowsePageTemplate.Enabled = cbImportPages.Checked;
        }

        private void btnBrowsePostTemplate_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog { Filter = "OU Campus Template Files|*.pcf" };
            var dialogResult = fileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                txtPostTemplate.Text = fileDialog.FileName;
            }
        }

        private void btnBrowsePageTemplate_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog { Filter = "OU Campus Template Files|*.pcf" };
            var dialogResult = fileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                txtPageTemplate.Text = fileDialog.FileName;
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

            if (!cbImportPosts.Checked && !cbImportPages.Checked)
            {
                MessageBox.Show("You must choose whether to import posts or pages (or both)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbImportPosts.Checked && string.IsNullOrWhiteSpace(txtPostTemplate.Text))
            {
                MessageBox.Show("Please select a post template", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPostTemplate.Focus();
                return;
            }

            if (cbImportPages.Checked && string.IsNullOrWhiteSpace(txtPageTemplate.Text))
            {
                MessageBox.Show("Please select a page template", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPageTemplate.Focus();
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
                string xml = File.ReadAllText(txtWordPressExportFile.Text);
                var blog = new Blog(xml);

                if (cbImportPosts.Checked)
                    ImportPosts(blog);

                if (cbImportPages.Checked)
                    ImportPages(blog);
            }
            catch (Exception ex)
            {
                // TODO: handle error
                throw;
            }

            Cursor.Current = Cursors.Default;
        }

        private void ImportPosts(Blog blog)
        {
            string postTemplate = File.ReadAllText(txtPostTemplate.Text);
            List<Post> posts = blog.GetPosts().ToList();
            
            foreach (var post in posts)
            {
                string outputFile = postTemplate;

                // replace template fields
                outputFile = outputFile.Replace("{{post-date}}", post.PublishDate.ToString("MM/dd/yyyy hh:mm:ss tt"));
                outputFile = outputFile.Replace("{{post-excerpt}}", post.Excerpt);
                outputFile = outputFile.Replace("{{post-author}}", post.Author.DisplayName);
                outputFile = outputFile.Replace("{{post-author-email}}", post.Author.Email);
                outputFile = outputFile.Replace("{{post-tags}}", string.Join(",", post.Tags.Select(c => c.Name)));
                outputFile = outputFile.Replace("{{post-image-display}}", post.FeaturedImage != null ? "img" : "none");
                outputFile = outputFile.Replace("{{post-image-src}}", post.FeaturedImage?.Url.Replace("localhost/greenandgold", "greenandgold.uaa.alaska.edu"));
                outputFile = outputFile.Replace("{{post-image-alt}}", post.FeaturedImage?.Title);

                // add code to map post audience

                // add code to map post categories

                // cleanup HTML encoding in title
                string postTitle = post.Title;
                postTitle = postTitle.Replace(" & ", " &amp; ");
                outputFile = outputFile.Replace("{{post-title}}", postTitle);

                // cleanup HTML encoding & other special characters in body
                string postBody = post.Body;
                postBody = postBody.Replace(" & ", " &amp; ");
                postBody = postBody.Replace('\u2013', '-');
                postBody = postBody.Replace('\u2014', '-');
                postBody = postBody.Replace('\u2015', '-');
                postBody = postBody.Replace('\u2017', '_');
                postBody = postBody.Replace('\u2018', '\'');
                postBody = postBody.Replace('\u2019', '\'');
                postBody = postBody.Replace('\u201a', ',');
                postBody = postBody.Replace('\u201b', '\'');
                postBody = postBody.Replace('\u201c', '\"');
                postBody = postBody.Replace('\u201d', '\"');
                postBody = postBody.Replace('\u201e', '\"');
                postBody = postBody.Replace("\u2026", "...");
                postBody = postBody.Replace('\u2032', '\'');
                postBody = postBody.Replace('\u2033', '\"');

                // remove any explicit height/width declarations
                postBody = Regex.Replace(postBody, "\\s+width=\"[0-9]+\"", "");
                postBody = Regex.Replace(postBody, "\\s+height=\"[0-9]+\"", "");

                // replace [caption] shortcodes with <div>
                if (postBody.Contains("[caption"))
                {
                    postBody = postBody.Replace("[caption", "<div");
                    postBody = Regex.Replace(postBody, "align=\"(?<align>[a-z]+)\"\\]", "class=\"${align} image-caption\">");
                    postBody = Regex.Replace(postBody, "</a>(?<caption>.*)\\[/caption\\]", "</a><p class=\"image-caption\"><small>${caption}</small></p></div>"); // image w/ link
                    postBody = Regex.Replace(postBody, "/>(?<caption>.*)\\[/caption\\]", "/><p class=\"image-caption\"><small>${caption}</small></p></div>"); // image w/o link
                }

                // change alignment classes
                postBody = postBody.Replace("class=\"align", "class=\"");

                outputFile = outputFile.Replace("{{post-body}}", postBody);

                // organize posts into directories based on (first) category
                string category = post.Categories.FirstOrDefault()?.Slug ?? "uncategorized";
                string postDirectory = txtOutputDirectory.Text + "\\posts\\" + category;
                Directory.CreateDirectory(postDirectory);

                // get filename
                string postSlug = post.Slug.Replace("_", "-");
                int postId;
                // if slug is just the numeric post id, create slug from title
                if (int.TryParse(postSlug, out postId))
                {
                    postSlug = post.Title.ToLower().Replace(" ", "-");
                }
                postSlug = Regex.Replace(postSlug, @"[^0-9a-zA-Z\-]", "");

                // write .pcf file
                string filePath = postDirectory + "\\" + postSlug + ".pcf";
                File.WriteAllText(filePath, outputFile);
                File.SetLastWriteTime(filePath, post.PublishDate);
            }
        }

        private void ImportPages(Blog blog)
        {
            string pageTemplate = File.ReadAllText(txtPageTemplate.Text);
            List<Page> pages = blog.GetPages().ToList();

            foreach (var page in pages)
            {
                string outputFile = pageTemplate;

                // cleanup HTML encoding in title
                string pageTitle = page.Title;
                pageTitle = pageTitle.Replace(" & ", " &amp; ");
                outputFile = outputFile.Replace("{{page-title}}", pageTitle);

                // cleanup HTML encoding in body
                string postBody = page.Body;
                postBody = postBody.Replace(" & ", " &amp; ");
                outputFile = outputFile.Replace("{{page-body}}", postBody);

                // create page directory hierarchy
                bool hasParent = page.ParentId != null;
                bool hasChildren = pages.Any(p => p.ParentId == page.Id);
                string pageDirectory = "";

                if (hasParent)
                {
                    Page current = page;

                    // build up parent page hierarchy in reverse order
                    while (true)
                    {
                        Page parent = pages.SingleOrDefault(p => p.Id == current.ParentId);
                        if (parent == null)
                            break;

                        pageDirectory = parent.Slug + "\\" + pageDirectory;
                        current = parent;
                    }
                }

                pageDirectory = txtOutputDirectory.Text + "\\pages\\" + pageDirectory;
                string filePath = pageDirectory;

                if (hasChildren)
                {
                    Directory.CreateDirectory(filePath + "\\" + page.Slug);
                    filePath += "\\" + page.Slug + "\\index.pcf";
                }
                else
                {
                    Directory.CreateDirectory(filePath);
                    filePath += "\\" + page.Slug + ".pcf";
                }
                
                // write .pcf file
                File.WriteAllText(filePath, outputFile);
                File.SetLastWriteTime(filePath, page.PublishDate);
            }
        }
    }
}
