using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLP;
using NLP.NGrams;
using NLP.Tokenization;
using NLP.NGramDataSet;
using NLP.Autocompletion;
using System.Threading;

namespace AutocompleteApplication
{
    public partial class MainForm : Form
    {
        private const string TEXT_FILE_FILTER = "Text files (*.txt)|*.txt";
        private string dataSetString;
        private NGramDataSet dataSet;
        private Autocompletion autocompleter;
        List<(string, string)> suggestions;
        List<string> tokens;
        Thread tokenizationThread;



        public MainForm()
        {
            InitializeComponent();
        }

        // Write this method (and, of course, all other relevant methods, placed in
        // appropriately named classes, placed in a suitable folder.
        //
        // Note: To add a folder, right-click on the project in the Solution Explorer,
        // (e.g., NLP), then select Add - New Folder. 
        // Do NOT add folders externally (outside Visual Studio).
        //
        // Here, no class labels are needed. Instead you simply
        // need to read text and then tokenize it, after which you can generate
        // the n-grams (for n = 1,2, and 3).

        private string LoadDataSet()
        {
            string dataSet = null;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = TEXT_FILE_FILTER;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StreamReader dataReader = new StreamReader(openFileDialog.FileName);
                    dataSet = dataReader.ReadToEnd().Replace("\t", " ").Replace("\n", " ");
                    dataReader.Close();
                }
            }
            return dataSet;
        }
        private void loadDataSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataSetString = LoadDataSet();
            if ((dataSetString != null)) { tokenizeButton.Enabled = true; }
            loadDataSetToolStripMenuItem.Enabled = true; // To avoid accidentally reloading the training set instead of the validation set...
        }

        private async void tokenizeButton_Click(object sender, EventArgs e)
        {
            // place the tokenization process in a separate thread
            //tokenizationThread = new Thread(new ThreadStart(() => TokenizationLoop()));
            // tokenizationThread.Start();
            tokenizeButton.Enabled = false;
            await Task.Run(() => TokenizationLoop());

            dataSet = new NGramDataSet(tokens);

            generateNGramsButton.Enabled = true;


        }

        private async void generateNGramsButton_Click(object sender, EventArgs e)
        {
            await Task.Run(() => ComputeNGramsLoop());
            generateNGramsButton.Enabled = false;
            AutocompletingTextBox.Enabled = true;
            AutocompletingTextBox.Visible = true;
            suggestionListBox.Visible = true;
            
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void progressListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AutocompletingTextBox_TextChanged(object sender, EventArgs e)
        {
            string textBoxContent = AutocompletingTextBox.Text;

            // Tokenize the string
            Tokenizer tokenizer = new Tokenizer();
            List<string> tokenizedInput = tokenizer.Tokenize(textBoxContent);

            // limit the tokenized input to the last 2 words
            if (tokenizedInput.Count > 2)
            {
                tokenizedInput = tokenizedInput.GetRange(tokenizedInput.Count - 2, 2);
            }

            suggestions = autocompleter.GetSuggestions(tokenizedInput);
            suggestionListBox.Items.Clear();
            foreach (var suggestion in suggestions)
            {
                suggestionListBox.Items.Add(suggestion.Item1);
            }

            /*
                        suggestionListBox.Items.Clear();
            suggestionListBox.Items.AddRange(suggestions.ToArray());
            */
        }

        private void AutocompletingTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                
                // Check if there are suggestions and the first one is not null
                if (suggestions != null && suggestions.Count > 0)
                {
                    // Prevent the default behavior of the Tab key
                    e.SuppressKeyPress = true;
                    e.Handled = true;

                    // Add the first suggestion to the text box
                    string selectedSuggestion = suggestions[0].Item2 + " " + suggestions[0].Item1;
                    AutocompletingTextBox.Text += "" + selectedSuggestion;

                    // Move the cursor to the end of the text box
                    AutocompletingTextBox.SelectionStart = AutocompletingTextBox.Text.Length;

                    AutocompletingTextBox.Focus();
                }
            }
        }

        private void suggestionListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Control_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Return)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void TokenizationLoop()
        {
            Tokenizer tokenizer = new Tokenizer();
            ThreadSafeShowProgress("Starting tokenizing.");
          

            tokens = tokenizer.Tokenize(dataSetString);

            string progressInformation = "Tokenization complete, " + tokens.Count + " tokens generated";
            ThreadSafeShowProgress(progressInformation);
            ThreadSafeEnableGenerateNGramButton();
            ThreadSafeHandleDone();
            
        }

        private void ComputeNGramsLoop()
        {
            ThreadSafeShowProgress("Generating n-Grams...");
            dataSet.computeTheListsOfNGrams();
            dataSet.computeNGrams();
            int numberOfUnigrams = dataSet.unigrams.Count;
            int numberOfBigrams = dataSet.bigrams.Count;
            int numberOfTrigrams = dataSet.trigrams.Count;
            string progressInformation = ("#Unigrams: " + numberOfUnigrams + " #Bigrams: " + numberOfBigrams + " #Trigrams: " + numberOfTrigrams);
            autocompleter = new Autocompletion(dataSet.trigrams, dataSet.bigrams);
            ThreadSafeShowProgress(progressInformation);
            ThreadSafeEnableTextBoxes();
            ThreadSafeHandleDone();

        }

        private void ThreadSafeEnableGenerateNGramButton() 
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => generateNGramsButton.Enabled = true));
            }
            else
            {
                generateNGramsButton.Enabled = true;
            }
        }

        private void ThreadSafeEnableTextBoxes()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => AutocompletingTextBox.Enabled = true));
                BeginInvoke(new MethodInvoker(() => suggestionListBox.Visible = true));
                BeginInvoke(new MethodInvoker(() => AutocompletingTextBox.Visible = true));
            }
            else
            {
                AutocompletingTextBox.Enabled = true;
                suggestionListBox.Visible = true;
                AutocompletingTextBox.Visible = true;

            }
        }

        private void ThreadSafeShowProgress(string progessInformation)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => progressListBox.Items.Add(progessInformation)));
            }
            else
            {
                progressListBox.Items.Add(progessInformation);
            }
        }

        private void ThreadSafeHandleDone()
        {
            if (InvokeRequired) { BeginInvoke(new MethodInvoker(() => HandleDone()));  }
            else { HandleDone(); }
        }
        private void HandleDone()
        {
            progressListBox.Items.Add("Done!");
        }

    }
}
