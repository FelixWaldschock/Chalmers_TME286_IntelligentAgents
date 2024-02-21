
namespace POSTaggingApplication
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPOSCorpusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTagConversionDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.convertPOSTagsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.splitDataSetButton = new System.Windows.Forms.ToolStripButton();
            this.splitFractionTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.generateStatisticsButton = new System.Windows.Forms.ToolStripButton();
            this.generateUnigramTaggerButton = new System.Windows.Forms.ToolStripButton();
            this.runUnigramTaggerButton = new System.Windows.Forms.ToolStripButton();
            this.resultsListBox = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(739, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadPOSCorpusToolStripMenuItem,
            this.loadTagConversionDataToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadPOSCorpusToolStripMenuItem
            // 
            this.loadPOSCorpusToolStripMenuItem.Name = "loadPOSCorpusToolStripMenuItem";
            this.loadPOSCorpusToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.loadPOSCorpusToolStripMenuItem.Text = "Load POS corpus";
            this.loadPOSCorpusToolStripMenuItem.Click += new System.EventHandler(this.loadPOSCorpusToolStripMenuItem_Click);
            // 
            // loadTagConversionDataToolStripMenuItem
            // 
            this.loadTagConversionDataToolStripMenuItem.Name = "loadTagConversionDataToolStripMenuItem";
            this.loadTagConversionDataToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.loadTagConversionDataToolStripMenuItem.Text = "Load tag conversion data";
            this.loadTagConversionDataToolStripMenuItem.Click += new System.EventHandler(this.loadTagConversionDataToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertPOSTagsButton,
            this.toolStripSeparator1,
            this.splitDataSetButton,
            this.splitFractionTextBox,
            this.toolStripSeparator2,
            this.generateStatisticsButton,
            this.generateUnigramTaggerButton,
            this.runUnigramTaggerButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(739, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // convertPOSTagsButton
            // 
            this.convertPOSTagsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.convertPOSTagsButton.Enabled = false;
            this.convertPOSTagsButton.Image = ((System.Drawing.Image)(resources.GetObject("convertPOSTagsButton.Image")));
            this.convertPOSTagsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.convertPOSTagsButton.Name = "convertPOSTagsButton";
            this.convertPOSTagsButton.Size = new System.Drawing.Size(215, 22);
            this.convertPOSTagsButton.Text = "Convert POS tags (Brown -> Universal)";
            this.convertPOSTagsButton.Click += new System.EventHandler(this.convertPOSTagsButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // splitDataSetButton
            // 
            this.splitDataSetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.splitDataSetButton.Enabled = false;
            this.splitDataSetButton.Image = ((System.Drawing.Image)(resources.GetObject("splitDataSetButton.Image")));
            this.splitDataSetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.splitDataSetButton.Name = "splitDataSetButton";
            this.splitDataSetButton.Size = new System.Drawing.Size(34, 22);
            this.splitDataSetButton.Text = "Split";
            this.splitDataSetButton.Click += new System.EventHandler(this.splitDataSetButton_Click);
            // 
            // splitFractionTextBox
            // 
            this.splitFractionTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.splitFractionTextBox.Name = "splitFractionTextBox";
            this.splitFractionTextBox.Size = new System.Drawing.Size(40, 25);
            this.splitFractionTextBox.Text = "0.80";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // generateStatisticsButton
            // 
            this.generateStatisticsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.generateStatisticsButton.Enabled = false;
            this.generateStatisticsButton.Image = ((System.Drawing.Image)(resources.GetObject("generateStatisticsButton.Image")));
            this.generateStatisticsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.generateStatisticsButton.Name = "generateStatisticsButton";
            this.generateStatisticsButton.Size = new System.Drawing.Size(106, 22);
            this.generateStatisticsButton.Text = "Generate statistics";
            this.generateStatisticsButton.Click += new System.EventHandler(this.generateStatisticsButton_Click);
            // 
            // generateUnigramTaggerButton
            // 
            this.generateUnigramTaggerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.generateUnigramTaggerButton.Enabled = false;
            this.generateUnigramTaggerButton.Image = ((System.Drawing.Image)(resources.GetObject("generateUnigramTaggerButton.Image")));
            this.generateUnigramTaggerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.generateUnigramTaggerButton.Name = "generateUnigramTaggerButton";
            this.generateUnigramTaggerButton.Size = new System.Drawing.Size(143, 22);
            this.generateUnigramTaggerButton.Text = "Generate unigram tagger";
            this.generateUnigramTaggerButton.Click += new System.EventHandler(this.generateUnigramTaggerButton_Click);
            // 
            // runUnigramTaggerButton
            // 
            this.runUnigramTaggerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.runUnigramTaggerButton.Enabled = false;
            this.runUnigramTaggerButton.Image = ((System.Drawing.Image)(resources.GetObject("runUnigramTaggerButton.Image")));
            this.runUnigramTaggerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runUnigramTaggerButton.Name = "runUnigramTaggerButton";
            this.runUnigramTaggerButton.Size = new System.Drawing.Size(117, 22);
            this.runUnigramTaggerButton.Text = "Run unigram tagger";
            this.runUnigramTaggerButton.Click += new System.EventHandler(this.runUnigramTaggerButton_Click);
            // 
            // resultsListBox
            // 
            this.resultsListBox.BackColor = System.Drawing.Color.Black;
            this.resultsListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.resultsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsListBox.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultsListBox.ForeColor = System.Drawing.Color.Lime;
            this.resultsListBox.FormattingEnabled = true;
            this.resultsListBox.ItemHeight = 11;
            this.resultsListBox.Location = new System.Drawing.Point(0, 49);
            this.resultsListBox.Name = "resultsListBox";
            this.resultsListBox.Size = new System.Drawing.Size(739, 540);
            this.resultsListBox.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 589);
            this.Controls.Add(this.resultsListBox);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS tagging application";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadPOSCorpusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTagConversionDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton convertPOSTagsButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton generateStatisticsButton;
        private System.Windows.Forms.ToolStripButton generateUnigramTaggerButton;
        private System.Windows.Forms.ToolStripButton runUnigramTaggerButton;
        private System.Windows.Forms.ListBox resultsListBox;
        private System.Windows.Forms.ToolStripButton splitDataSetButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripTextBox splitFractionTextBox;
    }
}

