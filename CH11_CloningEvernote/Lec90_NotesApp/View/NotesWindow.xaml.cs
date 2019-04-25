using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lec90_NotesApp.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        private SpeechRecognitionEngine recognizer;

        public NotesWindow()
        {
            InitializeComponent();

            var currentCulture = SpeechRecognitionEngine.InstalledRecognizers()
                .Where(r => r.Culture.Equals(Thread.CurrentThread.CurrentCulture))
                .FirstOrDefault();

            //var currentCulture = SpeechRecognitionEngine.InstalledRecognizers()
            //    .Where(r => r.Culture.TwoLetterISOLanguageName.Equals("en"))
            //    .FirstOrDefault();

            recognizer = new SpeechRecognitionEngine(currentCulture);

            var builder = new GrammarBuilder();
            builder.AppendDictation();
            Grammar grammar = new Grammar(builder);
            recognizer.LoadGrammar(grammar);
            recognizer.SetInputToDefaultAudioDevice();
            recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
        }


        private void SpeechButton_OnClick(object sender, RoutedEventArgs e)
        {
            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;
            if(!isButtonEnabled)
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
            else
                recognizer.RecognizeAsyncStop();
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            var recognizedText = e.Result.Text;
            
            ContentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(recognizedText)));
        }

        private void ContentRichTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            int amountOfCharacters = new TextRange(
                ContentRichTextBox.Document.ContentStart,
                ContentRichTextBox.Document.ContentEnd).Text.Length;

            StatusTextBlock.Text = $"Document length: {amountOfCharacters} characters.";
        }

        private void BoldButton_OnClick(object sender, RoutedEventArgs e)
        {
            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;
            if (isButtonEnabled)
                ContentRichTextBox.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Bold);
            else
                ContentRichTextBox.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Normal);

            //var fontWeight = FontWeights.Bold;

            //var isBold = ContentRichTextBox.Selection.GetPropertyValue(FontWeightProperty)
            //    .Equals(FontWeights.Bold);
            //if (isBold)
            //    fontWeight = FontWeights.Normal;

            //ContentRichTextBox.Selection.ApplyPropertyValue(FontWeightProperty, fontWeight);
        }

        private void ContentRichTextBox_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedState = ContentRichTextBox.Selection.GetPropertyValue(FontWeightProperty);
            BoldButton.IsChecked = (selectedState != DependencyProperty.UnsetValue) && (selectedState.Equals(FontWeights.Bold));

        }
    }
}
