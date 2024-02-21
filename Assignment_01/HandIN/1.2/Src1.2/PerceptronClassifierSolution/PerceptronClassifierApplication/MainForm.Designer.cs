namespace PerceptronClassifierApplication
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tokenizeButton = new System.Windows.Forms.ToolStripButton();
            this.indexButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.initializeOptimizerButton = new System.Windows.Forms.ToolStripButton();
            this.startOptimizerButton = new System.Windows.Forms.ToolStripButton();
            this.stopOptimizerButton = new System.Windows.Forms.ToolStripButton();
            this.loadTrainingSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadValidationSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTestSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressListBox = new System.Windows.Forms.ListBox();
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
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadTrainingSetToolStripMenuItem,
            this.loadValidationSetToolStripMenuItem,
            this.loadTestSetToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tokenizeButton,
            this.indexButton,
            this.toolStripSeparator1,
            this.initializeOptimizerButton,
            this.startOptimizerButton,
            this.stopOptimizerButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tokenizeButton
            // 
            this.tokenizeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tokenizeButton.Enabled = false;
            this.tokenizeButton.Image = ((System.Drawing.Image)(resources.GetObject("tokenizeButton.Image")));
            this.tokenizeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tokenizeButton.Name = "tokenizeButton";
            this.tokenizeButton.Size = new System.Drawing.Size(56, 22);
            this.tokenizeButton.Text = "Tokenize";
            this.tokenizeButton.Click += new System.EventHandler(this.tokenizeButton_Click);
            // 
            // indexButton
            // 
            this.indexButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.indexButton.Enabled = false;
            this.indexButton.Image = ((System.Drawing.Image)(resources.GetObject("indexButton.Image")));
            this.indexButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.indexButton.Name = "indexButton";
            this.indexButton.Size = new System.Drawing.Size(133, 22);
            this.indexButton.Text = "Generated indexed sets";
            this.indexButton.Click += new System.EventHandler(this.indexButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // initializeOptimizerButton
            // 
            this.initializeOptimizerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.initializeOptimizerButton.Enabled = false;
            this.initializeOptimizerButton.Image = ((System.Drawing.Image)(resources.GetObject("initializeOptimizerButton.Image")));
            this.initializeOptimizerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.initializeOptimizerButton.Name = "initializeOptimizerButton";
            this.initializeOptimizerButton.Size = new System.Drawing.Size(107, 22);
            this.initializeOptimizerButton.Text = "Initialize optimizer";
            this.initializeOptimizerButton.Click += new System.EventHandler(this.initializeOptimizerButton_Click);
            // 
            // startOptimizerButton
            // 
            this.startOptimizerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.startOptimizerButton.Enabled = false;
            this.startOptimizerButton.Image = ((System.Drawing.Image)(resources.GetObject("startOptimizerButton.Image")));
            this.startOptimizerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startOptimizerButton.Name = "startOptimizerButton";
            this.startOptimizerButton.Size = new System.Drawing.Size(88, 22);
            this.startOptimizerButton.Text = "Start optimizer";
            this.startOptimizerButton.Click += new System.EventHandler(this.startOptimizerButton_Click);
            // 
            // stopOptimizerButton
            // 
            this.stopOptimizerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stopOptimizerButton.Enabled = false;
            this.stopOptimizerButton.Image = ((System.Drawing.Image)(resources.GetObject("stopOptimizerButton.Image")));
            this.stopOptimizerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopOptimizerButton.Name = "stopOptimizerButton";
            this.stopOptimizerButton.Size = new System.Drawing.Size(88, 22);
            this.stopOptimizerButton.Text = "Stop optimizer";
            this.stopOptimizerButton.Click += new System.EventHandler(this.stopOptimizerButton_Click);
            // 
            // loadTrainingSetToolStripMenuItem
            // 
            this.loadTrainingSetToolStripMenuItem.Name = "loadTrainingSetToolStripMenuItem";
            this.loadTrainingSetToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadTrainingSetToolStripMenuItem.Text = "Load training set";
            this.loadTrainingSetToolStripMenuItem.Click += new System.EventHandler(this.loadTrainingSetToolStripMenuItem_Click);
            // 
            // loadValidationSetToolStripMenuItem
            // 
            this.loadValidationSetToolStripMenuItem.Name = "loadValidationSetToolStripMenuItem";
            this.loadValidationSetToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadValidationSetToolStripMenuItem.Text = "Load validation set";
            this.loadValidationSetToolStripMenuItem.Click += new System.EventHandler(this.loadValidationSetToolStripMenuItem_Click);
            // 
            // loadTestSetToolStripMenuItem
            // 
            this.loadTestSetToolStripMenuItem.Name = "loadTestSetToolStripMenuItem";
            this.loadTestSetToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadTestSetToolStripMenuItem.Text = "Load test set";
            this.loadTestSetToolStripMenuItem.Click += new System.EventHandler(this.loadTestSetToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // progressListBox
            // 
            this.progressListBox.BackColor = System.Drawing.Color.Black;
            this.progressListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.progressListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressListBox.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressListBox.ForeColor = System.Drawing.Color.Lime;
            this.progressListBox.FormattingEnabled = true;
            this.progressListBox.ItemHeight = 11;
            this.progressListBox.Location = new System.Drawing.Point(0, 49);
            this.progressListBox.Name = "progressListBox";
            this.progressListBox.Size = new System.Drawing.Size(800, 401);
            this.progressListBox.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.progressListBox);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Perceptron classifier";
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
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tokenizeButton;
        private System.Windows.Forms.ToolStripButton indexButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton initializeOptimizerButton;
        private System.Windows.Forms.ToolStripButton startOptimizerButton;
        private System.Windows.Forms.ToolStripButton stopOptimizerButton;
        private System.Windows.Forms.ToolStripMenuItem loadTrainingSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadValidationSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTestSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ListBox progressListBox;
    }
}

