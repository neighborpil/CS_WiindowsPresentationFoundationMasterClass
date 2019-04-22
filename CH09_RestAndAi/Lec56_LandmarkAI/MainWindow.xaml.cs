using System;
using System.Collections.Generic;
using System.Drawing;
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
using Lec56_LandmarkAI.Classes;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Lec56_LandmarkAI
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
            dialog.Filter = "Image files(*.png; *.jpg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;
                SelectedImage.Source = new BitmapImage(new Uri(filename));


                MakePredictionAsync(filename);
            }
        }

        private async void MakePredictionAsync(string filename)
        {
            string url =
                "https://japaneast.api.cognitive.microsoft.com/customvision/v3.0/Prediction/ed931ae6-54fc-411a-b47c-16f3a2369842/classify/iterations/ClassifyMonuments/image";
            string prediction_key = "ff9750493d3d42db8abada59a7d6b3dc";
            string content_type = "application/octet-stream";

            var file = File.ReadAllBytes(filename);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Prediction-Key", prediction_key);

                using (var content = new ByteArrayContent(file))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue(content_type);
                    var response = await client.PostAsync(url, content);

                    var responseString = await response.Content.ReadAsStringAsync();

                    List<Prediction> predictions = (JsonConvert.DeserializeObject<CustomVision>(responseString)).Predictions;
                    PredictioinListView.ItemsSource = predictions;
                }
            }

        }
    }
}
