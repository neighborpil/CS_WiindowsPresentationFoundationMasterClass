using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Newtonsoft.Json;
using Test_LandmarkAI.Classes;

namespace Test_LandmarkAI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dialog.Filter = "Image files(*.png, *jpg)|*.png;*.jpg;*.jpeg|All files(*.*)|*.*";

            var result = dialog.ShowDialog();

            if (result == null || result.Value == false)
            {
                return;
            }

            var fileName = dialog.FileName;
            SelectedImage.Source = new BitmapImage(new Uri(fileName));

            MakePredictionAsync(fileName);
        }

        private async void MakePredictionAsync(string fileName)
        {
            var url =
                "https://japaneast.api.cognitive.microsoft.com/customvision/v3.0/Prediction/1d0ab75f-d63b-4f39-9b56-8bc900a4d749/classify/iterations/MonumentClassification/image";
            var predictionKey = "ff9750493d3d42db8abada59a7d6b3dc";
            var contentType = "application/octet-stream";
            var file = File.ReadAllBytes(fileName);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Prediction-Key", predictionKey);

                using (var content = new ByteArrayContent(file))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    var response = await client.PostAsync(url, content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    List<Prediction> predictions =
                        JsonConvert.DeserializeObject<MonumentClassification>(responseString)?.Predictions;
                    PredictionListView.ItemsSource = predictions;
                }
            }

        }
    }
}
