﻿using System;
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
                outputFile = outputFile.Replace("{{post-tags}}", string.Join(",", post.Tags.Select(t => t.Name)));
                outputFile = outputFile.Replace("{{post-image-display}}", post.FeaturedImage != null ? "img" : "none");
                outputFile = outputFile.Replace("{{post-image-src}}", post.FeaturedImage?.Url.Replace("localhost/greenandgold", "greenandgold.uaa.alaska.edu"));
                outputFile = outputFile.Replace("{{post-image-alt}}", post.FeaturedImage?.Title);

                // change category "University News" to Impact
                int catIndex = post.Categories.FindIndex(c => c.Name == "University News");
                if (catIndex >= 0)
                    post.Categories[catIndex] = new Category { Name = "Impact" };

                // change category "Seawolf Weekly" to "Seawolf Monthly"
                catIndex = post.Categories.FindIndex(c => c.Name == "Seawolf Weekly");
                if (catIndex >= 0)
                    post.Categories[catIndex] = new Category { Name = "Seawolf Monthly" };

                // map post categories
                var categories = new Dictionary<string, string>
                {
                    { "Announcements", "Seawolf Daily - Announcements" },
                    { "Special", "Seawolf Daily - Special" },
                    { "Classifieds", "Seawolf Daily - Classifieds" },
                    { "Graphic Advertisement", "Seawolf Daily - Graphics Advertisements" },
                    { "Human Resources", "Seawolf Daily - Human Resources" },
                    { "Opportunities", "Seawolf Daily - Opportunities" },
                    { "Student News", "Seawolf Student - Student News" },
                    { "Student Opportunities", "Seawolf Student - Student Opportunities" },
                    { "UAA News", "Seawolf Student - UAA News" },
                    { "Featured", "Web - Featured" },
                    { "Impact", "Web - Impact" },
                    { "I Am UAA", "Web - I am UAA" },
                    { "Research", "Web - Research" }
                };

                string postCategories = "";
                foreach (string key in categories.Keys)
                {
                    postCategories += $"<option value=\"{key}\" selected=\"{post.Categories.Any(c => c.Name.Equals(key, StringComparison.OrdinalIgnoreCase)).ToString().ToLower()}\">{categories[key]}</option>\n\t\t\t";
                }

                outputFile = outputFile.Replace("{{post-categories}}", postCategories);

                // map post publications
                var publications = new Dictionary<string, string>
                {
                    { "Alumni Howl", "Alumni Howl" },
                    { "College of Fellows", "College of Fellows" },
                    { "Seawolf Daily", "Seawolf Daily" },
                    { "Seawolf Student", "Seawolf Student" },
                    { "Seawolf Monthly", "Seawolf Monthly" },
                    { "Web", "Web" }
                };

                string postPublications = "";
                foreach (string key in publications.Keys)
                {
                    postPublications += $"<option value=\"{key}\" selected=\"{post.Categories.Any(c => c.Name.Equals(key, StringComparison.OrdinalIgnoreCase)).ToString().ToLower()}\">{publications[key]}</option>\n\t\t\t";
                }

                outputFile = outputFile.Replace("{{post-publications}}", postPublications);

                // TODO: add code to map post audience

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

                // remove empty paragraphs
                postBody = Regex.Replace(postBody, "\\r?\\n&nbsp;\\r?\\n", "");

                // fix missing <p> tags in body. temporarily insert as [p] so they don't interfere with other replacements.
                postBody = "[p]" + postBody + "[/p]";
                postBody = Regex.Replace(postBody, "\\r?\\n<(h2|h3|h4|h5|h6|ul|ol)>", "[/p]\r\n<$1>");
                postBody = Regex.Replace(postBody, "</(h2|h3|h4|h5|h6|ul|ol)>\\r?\\n", "</$1>\r\n[p]");
                postBody = Regex.Replace(postBody, "\\r?\\n\\r?\\n", "[/p]\r\n[p]");

                // finally, replace [p] with actual <p> tag
                postBody = postBody.Replace("[p]", "<p>");
                postBody = postBody.Replace("[/p]", "</p>");

                // remove any explicit height/width declarations
                postBody = Regex.Replace(postBody, "\\s+width=\"[0-9]+\"", "");
                postBody = Regex.Replace(postBody, "\\s+height=\"[0-9]+\"", "");

                // replace [caption] shortcodes with <div>
                if (postBody.Contains("[caption"))
                {
                    postBody = postBody.Replace("<p>[caption", "[caption");
                    postBody = postBody.Replace("[/caption]</p>", "[/caption]");
                    postBody = postBody.Replace("[caption", "<div");
                    postBody = Regex.Replace(postBody, "align=\"(?<align>[a-z]+)\"\\]", "class=\"${align} image-caption\">");
                    postBody = Regex.Replace(postBody, "</a>(?<caption>.*)\\[/caption\\]", "</a><p class=\"image-caption\"><small>${caption}</small></p></div>"); // image w/ link
                    postBody = Regex.Replace(postBody, "/>(?<caption>.*)\\[/caption\\]", "/><p class=\"image-caption\"><small>${caption}</small></p></div>"); // image w/o link
                }

                // change alignment classes
                postBody = postBody.Replace("class=\"align", "class=\"");

                outputFile = outputFile.Replace("{{post-body}}", postBody);

                //// organize posts into directories based on (first) category
                //string category = post.Categories.FirstOrDefault()?.Slug ?? "uncategorized";
                //string postDirectory = txtOutputDirectory.Text + "\\posts\\" + category;
                
                string postDirectory = txtOutputDirectory.Text + "\\posts";
                Directory.CreateDirectory(postDirectory);

                // get filename
                string postSlug = post.Slug.ToLower().Replace("_", "-");
                if (postSlug.StartsWith("-"))
                    postSlug = postSlug.Substring(1);

                // if slug is just the numeric post id, create slug from title
                int postId;
                if (int.TryParse(postSlug, out postId))
                {
                    postSlug = post.Title.ToLower().Replace(" ", "-");
                }

                // remove everything but alpha-numeric and hyphen
                postSlug = Regex.Replace(postSlug, @"[^0-9a-z\-]", "");

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
