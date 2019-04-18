using System;
using System.Collections.Generic;
using System.Linq;
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
using MessageBox = System.Windows.MessageBox;

namespace Lec08_TheGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double lastNumber, result;
        SelectedOperator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();
            acButton.Click += AcButtonOnClick;
            negativeButton.Click += NegativeButtonOnClick;
            percentageButton.Click += PercentageButtonOnClick;
            equalButton.Click += EqualButtonOnClick;
        }

        private void EqualButtonOnClick(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(lastNumber,newNumber);
                        break;
                    case SelectedOperator.Subtraction:
                        result = SimpleMath.Subtract(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multiply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Divide(lastNumber, newNumber);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                resultLabel.Content = result;
            }
        }

        private void PercentageButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out double tempNumber))
            {
                tempNumber = tempNumber / 100;
                if (lastNumber != 0)
                    tempNumber *= lastNumber;
                resultLabel.Content = tempNumber.ToString();
            }
        }

        private void NegativeButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void AcButtonOnClick(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            result = 0;
            lastNumber = 0;
        }


        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
                resultLabel.Content = "0";

            if (sender == multiplicationButton)
                selectedOperator = SelectedOperator.Multiplication;
            else if(sender == divisionButton)
                selectedOperator = SelectedOperator.Division;
            else if (sender == additionButton)
                selectedOperator = SelectedOperator.Addition;
            else if (sender == subtractionButton)
                selectedOperator = SelectedOperator.Subtraction;
        }

        private void NumberButton_OnClick(object sender, RoutedEventArgs e)
        {
            int selectedValue = 0;

            if (sender == zeroButton)
                selectedValue = 0;

            selectedValue = int.Parse((sender as Button).Content.ToString());

            resultLabel.Content = resultLabel.Content.ToString() == "0" ? 
                $"{selectedValue.ToString()}" : 
                $"{resultLabel.Content}{selectedValue.ToString()}";

        }

        private void PointButton_OnClick(object sender, RoutedEventArgs e)
        {
            if(!resultLabel.Content.ToString().Contains("."))
                resultLabel.Content = $"{resultLabel.Content}.";
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    public class SimpleMath
    {
        public static double Add(double n1, double n2)
        {
            return n1 + n2;
        }

        public static double Subtract(double n1, double n2)
        {
            return n1 - n2;
        }

        public static double Multiply(double n1, double n2)
        {
            return n1 * n2;
        }

        public static double Divide(double n1, double n2)
        {
            if (n2 == 0)
            {
                MessageBox.Show("Division by 0 is not supported", 
                    "Wrong operation", 
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return 0;
            }

            return n1 / n2;
        }
    }
}
