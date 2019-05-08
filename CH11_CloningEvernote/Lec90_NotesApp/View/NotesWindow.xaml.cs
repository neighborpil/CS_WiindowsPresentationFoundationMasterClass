using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using Lec90_NotesApp.ViewModel;
using Path = System.IO.Path;

namespace Lec90_NotesApp.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        private SpeechRecognitionEngine recognizer;
        private NotesVM viewModel;

        public NotesWindow()
        {
            InitializeComponent();

            viewModel = new NotesVM();
            Container.DataContext = viewModel;
            viewModel.SelectedNoteChanged += (sender, e) =>
            {
                ContentRichTextBox.Document.Blocks.Clear();
                if (!string.IsNullOrWhiteSpace(viewModel.SelectedNote.FileLocation))
                {
                    var fileStream = new FileStream(viewModel.SelectedNote.FileLocation, FileMode.Open);
                    TextRange range = new TextRange(ContentRichTextBox.Document.ContentStart, ContentRichTextBox.Document.ContentEnd);
                    range.Load(fileStream, DataFormats.Rtf);
                }
            };

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

            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            FontFamilyComboBox.ItemsSource = fontFamilies;
            //FontFamilyComboBox.SelectedIndex = 0;
            List<double> fontSizes = new List<double>{8, 9, 10, 11, 12,14,16, 20, 24, 28, 32, 48, 72};
            FontSizeComboBox.ItemsSource = fontSizes;
            //FontSizeComboBox.SelectedIndex = 0;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if(string.IsNullOrWhiteSpace(App.UserId))
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
            }
                
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
            var selectedWeight = ContentRichTextBox.Selection.GetPropertyValue(FontWeightProperty);
            BoldButton.IsChecked = (selectedWeight != DependencyProperty.UnsetValue) && (selectedWeight.Equals(FontWeights.Bold));

            var selectedStyle = ContentRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            ItalicButton.IsChecked = (selectedStyle != DependencyProperty.UnsetValue) &&
                                     (selectedStyle.Equals(FontStyles.Italic));

            var selectedDecoration = ContentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            UnderlineButton.IsChecked = (selectedDecoration != DependencyProperty.UnsetValue) &&
                                        (selectedDecoration.Equals(TextDecorations.Underline));

            FontFamilyComboBox.SelectedItem = ContentRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            FontSizeComboBox.Text = (ContentRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty)).ToString();

        }

        private void ItalicButton_OnClick(object sender, RoutedEventArgs e)
        {
            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonEnabled)
                ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            else
                ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
        }

        private void UnderlineButton_OnClick(object sender, RoutedEventArgs e)
        {
            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;

            if(isButtonEnabled)
                ContentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            else
            {
                (ContentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as
                    TextDecorationCollection).TryRemove(TextDecorations.Underline,
                    out TextDecorationCollection textDecorations);
                ContentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);
            }
        }

        private void FontFamilyComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilyComboBox.SelectedItem != null)
            {
                ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, FontFamilyComboBox.SelectedItem);
            }
        }

        private void FontSizeComboBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, FontSizeComboBox.Text);
        }

        private void SaveFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            string rtfFile = Path.Combine(Environment.CurrentDirectory, $"{viewModel.SelectedNote.Id}.rtf");
            viewModel.SelectedNote.FileLocation = rtfFile;

            FileStream fileStream = new FileStream(rtfFile, FileMode.Create);
            TextRange range = new TextRange(ContentRichTextBox.Document.ContentStart,
                ContentRichTextBox.Document.ContentEnd);
            range.Save(fileStream, DataFormats.Rtf);

            viewModel.UpdateSelectedNote();
        }
    }
}
