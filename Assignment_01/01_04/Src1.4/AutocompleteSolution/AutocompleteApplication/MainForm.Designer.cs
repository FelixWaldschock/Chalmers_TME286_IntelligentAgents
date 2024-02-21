
namespace AutocompleteApplication
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
            this.loadDataSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tokenizeButton = new System.Windows.Forms.ToolStripButton();
            this.generateNGramsButton = new System.Windows.Forms.ToolStripButton();
            this.generateTrigramsButton = new System.Windows.Forms.ToolStripButton();
            this.progressListBox = new System.Windows.Forms.ListBox();
            this.AutocompletingTextBox = new System.Windows.Forms.TextBox();
            this.suggestionListBox = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(684, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDataSetToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadDataSetToolStripMenuItem
            // 
            this.loadDataSetToolStripMenuItem.Name = "loadDataSetToolStripMenuItem";
            this.loadDataSetToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.loadDataSetToolStripMenuItem.Text = "Load data set";
            this.loadDataSetToolStripMenuItem.Click += new System.EventHandler(this.loadDataSetToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tokenizeButton,
            this.generateNGramsButton,
            this.generateTrigramsButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(684, 25);
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
            // generateNGramsButton
            // 
            this.generateNGramsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.generateNGramsButton.Enabled = false;
            this.generateNGramsButton.Image = ((System.Drawing.Image)(resources.GetObject("generateNGramsButton.Image")));
            this.generateNGramsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.generateNGramsButton.Name = "generateNGramsButton";
            this.generateNGramsButton.Size = new System.Drawing.Size(81, 22);
            this.generateNGramsButton.Text = "Gen. NGrams";
            this.generateNGramsButton.Click += new System.EventHandler(this.generateNGramsButton_Click);
            // 
            // generateTrigramsButton
            // 
            this.generateTrigramsButton.Name = "generateTrigramsButton";
            this.generateTrigramsButton.Size = new System.Drawing.Size(23, 22);
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
            this.progressListBox.Size = new System.Drawing.Size(684, 412);
            this.progressListBox.TabIndex = 5;
            this.progressListBox.TabStop = false;
            this.progressListBox.SelectedIndexChanged += new System.EventHandler(this.progressListBox_SelectedIndexChanged);
            // 
            // AutocompletingTextBox
            // 
            this.AutocompletingTextBox.Enabled = false;
            this.AutocompletingTextBox.Location = new System.Drawing.Point(430, 65);
            this.AutocompletingTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.AutocompletingTextBox.Name = "AutocompletingTextBox";
            this.AutocompletingTextBox.Size = new System.Drawing.Size(243, 20);
            this.AutocompletingTextBox.TabIndex = 6;
            this.AutocompletingTextBox.TabStop = false;
            this.AutocompletingTextBox.Visible = false;
            this.AutocompletingTextBox.TextChanged += new System.EventHandler(this.AutocompletingTextBox_TextChanged);
            this.AutocompletingTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AutocompletingTextBox_KeyDown);
            // 
            // suggestionListBox
            // 
            this.suggestionListBox.FormattingEnabled = true;
            this.suggestionListBox.Location = new System.Drawing.Point(430, 95);
            this.suggestionListBox.Margin = new System.Windows.Forms.Padding(2);
            this.suggestionListBox.Name = "suggestionListBox";
            this.suggestionListBox.Size = new System.Drawing.Size(243, 355);
            this.suggestionListBox.TabIndex = 7;
            this.suggestionListBox.TabStop = false;
            this.suggestionListBox.Visible = false;
            this.suggestionListBox.SelectedIndexChanged += new System.EventHandler(this.suggestionListBox_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.suggestionListBox);
            this.Controls.Add(this.AutocompletingTextBox);
            this.Controls.Add(this.progressListBox);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Autocompletion";
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
        private System.Windows.Forms.ToolStripMenuItem loadDataSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tokenizeButton;
        private System.Windows.Forms.ListBox progressListBox;
        private System.Windows.Forms.TextBox AutocompletingTextBox;
        private System.Windows.Forms.ToolStripButton generateNGramsButton;
        private System.Windows.Forms.ToolStripButton generateTrigramsButton;
        private System.Windows.Forms.ListBox suggestionListBox;
    }
}

