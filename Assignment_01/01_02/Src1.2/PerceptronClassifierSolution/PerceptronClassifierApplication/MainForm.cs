using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLP;
using NLP.TextClassification;
using NLP.Tokenization;

namespace PerceptronClassifierApplication
{
    public partial class MainForm : Form
    {
        private const string TEXT_FILE_FILTER = "Text files (*.txt)|*.txt";
        private const string FILEPATH_CORRECT_CLASSIFIED = "CorrectClassifiedReviews.csv";
        private const string FILEPATH_FALSE_CLASSIFIED = "FalseClassifiedReviews.csv";
        private const bool SAVE_CLASSIFIED_SENTENCES_TO_CSV = true;
        private const int MAXIMAL_EPOCHS = 100000;

        private PerceptronClassifier perceptronClassifier = null;
        private PerceptronEvaluator perceptronEvaluator = null;
        private PerceptronOptimizer perceptronOptimizer = null;
        private Vocabulary vocabulary = null;
        private TextClassificationDataSet trainingSet = null;
        private TextClassificationDataSet validationSet = null;
        private TextClassificationDataSet testSet = null;

        private Thread OptimizationOfClassifierThread;



        public MainForm()
        {
            InitializeComponent();
        }

        private TextClassificationDataSet LoadDataSet()
        {
            TextClassificationDataSet dataSet = null;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = TEXT_FILE_FILTER;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    dataSet = new TextClassificationDataSet();
                    StreamReader dataReader = new StreamReader(openFileDialog.FileName);    
                    while (!dataReader.EndOfStream)
                    {
                        string line = dataReader.ReadLine();
                        List<string> lineSplit = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        TextClassificationDataItem item = new TextClassificationDataItem();
                        item.Text = lineSplit[0].ToLower();
                        item.ClassLabel = int.Parse(lineSplit[1]);  
                        dataSet.ItemList.Add(item); 
                    }
                    dataReader.Close();
                    int count0 = dataSet.ItemList.Count(i => i.ClassLabel == 0);
                    int count1 = dataSet.ItemList.Count(i => i.ClassLabel == 1);
                    string fileName = Path.GetFileName(openFileDialog.FileName); // File name without the file path.
                    progressListBox.Items.Add("Loaded data file \"" + fileName + "\" with " + count0.ToString() +
                        " negative reviews and " + count1.ToString() + " positive reviews.");
                }
            }
            return dataSet; 
        }

        private void loadTrainingSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trainingSet = LoadDataSet();
            if ((trainingSet != null) && (validationSet != null) && (testSet != null)) { tokenizeButton.Enabled = true; }
            loadTrainingSetToolStripMenuItem.Enabled = false; // To avoid accidentally reloading the training set instead of the validation set...
        }

        private void loadValidationSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            validationSet = LoadDataSet();
            if ((trainingSet != null) && (validationSet != null) && (testSet != null)) { tokenizeButton.Enabled = true; }
            loadValidationSetToolStripMenuItem.Enabled = false;
        }

        private void loadTestSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            testSet = LoadDataSet();
            if ((trainingSet != null) && (validationSet != null) && (testSet != null)) { tokenizeButton.Enabled = true; }
            loadTestSetToolStripMenuItem.Enabled = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GenerateVocabulary(TextClassificationDataSet dataSet)
        {
            // Write a method that generates the vocabulary. Note that this
            // should ONLY be done for the training set!
            vocabulary = new Vocabulary(dataSet.getTokenList());

            progressListBox.Items.Add("Vocabulary generated.");
            progressListBox.Items.Add("Vocabulary size: " + vocabulary.GetVocabulary().Count.ToString());

            // You must generate an instance of the Vocabulary class,
            // which you must also implement (a skeleton is available
            // in the NLP library)
        }

        private void tokenizeButton_Click(object sender, EventArgs e)
        {
            // Write code here for tokenizing the text. That is,
            // implement the Tokenize() method in the Tokenizer class.

            List<string> abbreviations = new List<string>{"i.e.", "e.g.", "etc.", "mr.", "mrs."};

            Tokenizer tokenizer = new Tokenizer(abbreviations);

            // First tokenize the training set:
            List<Token> trainingTokens = new List<Token>();
            //loop over the trainingSet and tokenize from each item the text
            foreach (TextClassificationDataItem item in trainingSet.ItemList)
            {
                List<Token> tokens = tokenizer.Tokenize(item.Text);
                item.TokenList = tokens;
                trainingTokens.AddRange(tokens);
            }
            trainingSet.CompleteSetTokenList = trainingTokens;

            // Then build the vocabulary from the training set:
            GenerateVocabulary(trainingSet);

            // Next, tokenize the validation set:
            List<Token> validationTokens = new List<Token>();
            foreach (TextClassificationDataItem item in validationSet.ItemList)
            {
                List<Token> tokens = tokenizer.Tokenize(item.Text);
                item.TokenList = tokens;
                validationTokens.AddRange(tokens);
            }
            validationSet.CompleteSetTokenList = validationTokens;


            // Finally, tokenize the test set:
            List<Token> testTokens = new List<Token>();
            foreach (TextClassificationDataItem item in testSet.ItemList)
            {
                List<Token> tokens = tokenizer.Tokenize(item.Text);
                item.TokenList = tokens;
                testTokens.AddRange(tokens);
            }
            testSet.CompleteSetTokenList = testTokens;

            progressListBox.Items.Add("Tokenization completed.");
            progressListBox.Items.Add("Training set size: " + trainingSet.CompleteSetTokenList.Count.ToString());
            progressListBox.Items.Add("Validation set size: " + validationSet.CompleteSetTokenList.Count.ToString());
            progressListBox.Items.Add("Test set size: " + testSet.CompleteSetTokenList.Count.ToString());
             

            indexButton.Enabled = true;
            tokenizeButton.Enabled = false;
        }

        private void indexButton_Click(object sender, EventArgs e)
        {
            // Write code here for indexing the data sets to generate

            // Index the training set, using the *training* set vocabulary (see above)

            // Index the validation set, using the *training* set vocabulary (see above)
            // Assign index = -1 if a given word does not exist in the training set.

            // Index the test set, using the *training* set vocabulary (see above)
            // Assign index = -1 if a given word does not exist in the training set.

            if (vocabulary == null)
            {
                MessageBox.Show("Vocabulary not generated yet. Please generate the vocabulary first.");
                return;
            }

            // put this computation in a new thread -> Appendix A.4

            Thread ComputationOfIndexListsThread = new Thread(new ThreadStart(() => ComputationOfIndexLists()));
            ComputationOfIndexListsThread.Start();
           
            indexButton.Enabled = false;
            initializeOptimizerButton.Enabled = true;





        }

        private void initializeOptimizerButton_Click(object sender, EventArgs e)
        {
            // Write code here for initializing a perceptron optimizer, which
            // you must also write (i.e. a class called PerceptronOptimizer).
            // Moreover, as mentioned in the assignment text,
            // it might be a good idea to define an evaluator class (e.g. PerceptronEvaluator)
            // You should place both classes in the TextClassification folder in the NLP library.

            // get size of vocabulary
            int sizeOfVocabulary = vocabulary.SizeOfVocabulary;


            perceptronClassifier = new PerceptronClassifier();
            perceptronClassifier.Initialize(sizeOfVocabulary);
            perceptronEvaluator = new PerceptronEvaluator();
            perceptronOptimizer = new PerceptronOptimizer();

            progressListBox.Items.Add("Perceptron classifier, evaluator and optimizer initialized!");

            startOptimizerButton.Enabled = true;
        }

        private void startOptimizerButton_Click(object sender, EventArgs e)
        {
            startOptimizerButton.Enabled = false;
            progressListBox.Items.Clear();

            // Start the optimizer here.

            OptimizationOfClassifierThread = new Thread(new ThreadStart(() => ThreadPerceptronOptimizer()));
            OptimizationOfClassifierThread.Start();



            // For every epoch, the optimizer should (after the epoch has been completed)
            // trigger an event that prints the current accuracy (over the training set
            // and the validation set) of the perceptron classifier (in a thread-safe
            // manner, and with proper (clear) formatting). Do *not* involve
            // the test set here.

            stopOptimizerButton.Enabled = true;
        }

        private void stopOptimizerButton_Click(object sender, EventArgs e)
        {
            stopOptimizerButton.Enabled = false;
            startOptimizerButton.Enabled = true;

            progressListBox.Items.Clear();

            // Stop the optimizer here.
            OptimizationOfClassifierThread.Abort();

            perceptronClassifier.WeightList = perceptronClassifier.getBestWeights();

            //progressListBox.Items.Clear();
            progressListBox.Items.Add("------------------------");
            progressListBox.Items.Add("Optimizer stopped.");
            progressListBox.Items.Add("Load best weights and evaluate classifier over training, validation and test set.");

            // set the best weights to the perceptron classifier
            perceptronClassifier.WeightList = perceptronClassifier.getBestWeights();

            perceptronClassifier.setBestWeightsAsWeights();
            
            // get accuracy of the classifier over the validation set
            double validationAccuracy = perceptronEvaluator.evaluatePerceptron(perceptronClassifier, validationSet);
            progressListBox.Items.Add("Validation set accuracy: " + validationAccuracy.ToString("F3"));

            // get accuracy of the classifier over the training set
            double trainingAccuracy = perceptronEvaluator.evaluatePerceptron(perceptronClassifier, trainingSet);
            progressListBox.Items.Add("Training set accuracy: " + trainingAccuracy.ToString("F3"));

            // get accuracy of the classifier over the test set
            // activate the tracker
            perceptronEvaluator.ActivateTestReviewTracker();
            double testAccuracy = perceptronEvaluator.evaluatePerceptron(perceptronClassifier, testSet);
            progressListBox.Items.Add("Test set accuracy: " + testAccuracy.ToString("F3"));


            
        
            // Get the most positive and negative tokens -> for this create a duplicate of the perceptrons weights
            // and sort it in descending order. Take the first 10 and last 10 values

            
            Dictionary<int, double> weightDictionary = new Dictionary<int, double>();

            for (int i = 0; i < perceptronClassifier.WeightList.Count; i++)
            {
                weightDictionary.Add(i, perceptronClassifier.WeightList[i]);
            }

            // Sort the weightDictionary by values in descending order
            var sortedWeightDictionary = weightDictionary.OrderByDescending(weight => weight.Value)
                                                        .ToDictionary(weight => weight.Key, weight => weight.Value);

            progressListBox.Items.Add("Most positive words: \nSpelling\tWeight ");

            // Print the 10 most positive words -> largest weights
            List<string> mostPositiveWordsList = new List<string>();
            int count = 0;

            foreach (var kvp in sortedWeightDictionary)
            {
                mostPositiveWordsList.Add(vocabulary.vocabulary[kvp.Key]);
                progressListBox.Items.Add($"{vocabulary.vocabulary[kvp.Key]}\t{kvp.Value}");

                count++;

                if (count >= 10)
                    break;
            }


            // Sort the weightDictionary by values in ascending order
            sortedWeightDictionary = weightDictionary.OrderBy(weight => weight.Value)
                                                        .ToDictionary(weight => weight.Key, weight => weight.Value);

            progressListBox.Items.Add("Most negative words: \nSpelling\tWeight ");

            // Print the 10 most positive words -> largest weights
            List<string> mostNegativeWordsList = new List<string>();
            count = 0;

            foreach (var kvp in sortedWeightDictionary)
            {
                mostNegativeWordsList.Add(vocabulary.vocabulary[kvp.Key]);
                progressListBox.Items.Add($"{vocabulary.vocabulary[kvp.Key]}\t{kvp.Value}");

                count++;

                if (count >= 10)
                    break;
            }


            List<int> firstCorrectExampleIndeces = perceptronEvaluator.TestReviewTrackerCorretlyClassified[0];
            List<int> secondCorrectExampleIndeces = perceptronEvaluator.TestReviewTrackerCorretlyClassified[1];
            List<int> firstFalseExampleIndeces = perceptronEvaluator.TestReviewTrackerFalselyClassified[0];
            List<int> secondFalseExampleIndeces = perceptronEvaluator.TestReviewTrackerFalselyClassified[1];
            
            // convert indexed reviews to string lists
            string firstCorrectExample = vocabulary.GetReviewFromTokenIndexList(firstCorrectExampleIndeces);
            string secondCorrectExample = vocabulary.GetReviewFromTokenIndexList(secondCorrectExampleIndeces);

            string firstFalseExample = vocabulary.GetReviewFromTokenIndexList(firstFalseExampleIndeces);
            string secondFalseExample = vocabulary.GetReviewFromTokenIndexList(secondFalseExampleIndeces);
        
            if(SAVE_CLASSIFIED_SENTENCES_TO_CSV)
            { 
                // clear the two CSV files
                File.WriteAllText(FILEPATH_CORRECT_CLASSIFIED, string.Empty);
                File.WriteAllText(FILEPATH_FALSE_CLASSIFIED, string.Empty);
            

                string allCorrectlyClassifiedReviews = "";
                // write all correct classified reviews in a CSV file
                foreach(List<int> review in perceptronEvaluator.TestReviewTrackerCorretlyClassified)
                {
                    //allCorrectlyClassifiedReviews += vocabulary.GetReviewFromTokenIndexList(review) + "\n";               
                }
                WriteReviewsToCSV(allCorrectlyClassifiedReviews, true);
            

                // write all false classified reviews in a CSV file
                string allFalslyClassifiedReviews = "";
                foreach (List<int> review in perceptronEvaluator.TestReviewTrackerFalselyClassified)
                {
                    allFalslyClassifiedReviews += vocabulary.GetReviewFromTokenIndexList(review) + "\n";

                }
                WriteReviewsToCSV(allFalslyClassifiedReviews, false);
            }


            // Print them to the console
            Console.WriteLine("Correct classified sentences: \n");
            Console.WriteLine(firstCorrectExample);
            Console.WriteLine(secondCorrectExample);

            Console.WriteLine("\nFalse classified sentences: \n");
            Console.WriteLine(firstFalseExample);
            Console.WriteLine(secondFalseExample);

            // Specify the path for the CSV file
            string csvFilePath = "AccuracyTracker.csv";

            // Write the lists to the CSV file
            WriteToCsv(perceptronClassifier.trackerValidationAccuracy, perceptronClassifier.trackerTestingAccuracy, perceptronClassifier.trackerTrainingAccuracy, csvFilePath);

            progressListBox.Items.Add("CSV file has been created successfully.");

            // For simplicity (even though one may perhaps resume the optimizer), at this
            // point, evaluate the best classifier (= best validation performance) over
            // the *test* set, and print the accuracy to the screen (in a thread-safe
            // manner, and with proper (clear) formatting).

            stopOptimizerButton.Enabled = true; // A bit ugly, should wait for the
            // optimizer to actually stop, but that's OK, it will stop quickly.
        }


        private void ComputationOfIndexLists(){
            int status = 0;
            int totalNumberOfIndexation = trainingSet.ItemList.Count + validationSet.ItemList.Count + testSet.ItemList.Count;

            ThreadSafeShowProgress("Indexing: " + (status.ToString()) + "/" + totalNumberOfIndexation.ToString());
            // training set
            foreach (TextClassificationDataItem item in trainingSet.ItemList)
            {
                item.indexingTokens(vocabulary.GetVocabulary());
                status++;
                if (status % 300 == 0)
                {
                    ThreadSafeShowProgress("Indexing: " + (status.ToString()) + "/" + totalNumberOfIndexation.ToString());
                }
            }
            // validation set
            foreach (TextClassificationDataItem item in validationSet.ItemList)
            {
                item.indexingTokens(vocabulary.GetVocabulary());
                status++;
                if (status % 300 == 0)
                {
                    ThreadSafeShowProgress("Indexing: " + (status.ToString()) + "/" + totalNumberOfIndexation.ToString());
                }
            }
            // test set
            foreach (TextClassificationDataItem item in testSet.ItemList)
            {
                item.indexingTokens(vocabulary.GetVocabulary());
                status++;
                if (status % 300 == 0)
                {
                    ThreadSafeShowProgress("Indexing: " + (status.ToString()) + "/" + totalNumberOfIndexation.ToString());
                }
                
            }
            ThreadSafeShowProgress("Indexing: " + (status.ToString()) + "/" + totalNumberOfIndexation.ToString());
            ThreadSafeShowProgress("Indexing done!");
            


        }
        private void ThreadSafeShowProgress(string progessInformation){
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
            if (InvokeRequired) { BeginInvoke(new MethodInvoker(() => HandleDone())); }
            else { HandleDone(); }
        }

        private void HandleDone()
        {
            progressListBox.Items.Add("Done!");
        }

        // Thread for perceptron optimization
        private void ThreadPerceptronOptimizer()
        {
            ThreadSafeShowProgress("Starting optimizer");
            int i = 0;

            while (i < MAXIMAL_EPOCHS) {
                perceptronOptimizer.trainClassifier(perceptronClassifier, trainingSet);
                double testingAccuracyEpoch = perceptronEvaluator.evaluatePerceptron(perceptronClassifier, testSet);
                double validationAccuracyEpoch = perceptronEvaluator.evaluatePerceptron(perceptronClassifier, validationSet);
                double trainingAccuracyEpoch = perceptronEvaluator.evaluatePerceptron(perceptronClassifier, trainingSet);


                if (validationAccuracyEpoch > perceptronClassifier.bestValidationAccuracy)
                {
                    ThreadSafeShowProgress("Epoch " + perceptronOptimizer.trainingEpochs + " completed. \t\t--> NEW BEST found!");
                    ThreadSafeShowProgress("Validation set accuracy:\t" + validationAccuracyEpoch.ToString("0.000"));
                    // ThreadSafeShowProgress("Test set accuracy:\t\t" + testingAccuracyEpoch.ToString("0.000"));
                    // ThreadSafeShowProgress("Training set accuracy:\t\t" + trainingAccuracyEpoch.ToString("0.000"));

                    // set the bestWeights to the weights
                    perceptronClassifier.setWeightAsBestWeights();
                    ThreadSafeShowProgress("Updating best weights");
                    // set the bestValidationAccuracy to the validationAccuracy
                    perceptronClassifier.bestValidationAccuracy = validationAccuracyEpoch;

                }

                // add accuracies to the trackers
                perceptronClassifier.trackerTestingAccuracy.Add(testingAccuracyEpoch);
                perceptronClassifier.trackerValidationAccuracy.Add(validationAccuracyEpoch);
                perceptronClassifier.trackerTrainingAccuracy.Add(trainingAccuracyEpoch);

                if (i % 1000 == 0)
                {
                    ThreadSafeShowProgress("Training still running \t Epoch: " + i.ToString());
                }

                i++;
   
            }
        }

        static void WriteToCsv(List<double> list1, List<double> list2, List<double> list3, string filePath)
        {
            // Create a StreamWriter to write to the CSV file
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the header
                writer.WriteLine("ValidationAccuracy, TestingAccuracy, TrainingAccuracy");

                // Write the data
                int length = Math.Max(list1.Count, list2.Count);
                length = Math.Max(length, list3.Count);
                for (int i = 0; i < length; i++)
                {
                    string line = $"{(i < list1.Count ? list1[i].ToString() : "")},{(i < list2.Count ? list2[i].ToString() : "")},{(i < list3.Count ? list3[i].ToString() : "")}";
                    writer.WriteLine(line);
                }
            }
        }

        static void WriteReviewsToCSV(string review,  bool classification)
        {
            string csvFilePath;
            // Specify the path for the CSV file
            if (classification)
            {
                csvFilePath = FILEPATH_CORRECT_CLASSIFIED;
            }
            else
            {
                csvFilePath = FILEPATH_FALSE_CLASSIFIED;
            }

            // Create a StreamWriter to write to the CSV file
            using (StreamWriter writer = new StreamWriter(csvFilePath))
            {
                // Write the header
                // writer.WriteLine("Review");

                // Write the data
                string line = $"{review}";
                writer.WriteLine(line);
            }


        }
    }
}
