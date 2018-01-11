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

namespace Recursive_WPF_Calculator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TextBoxs = new List<TextBox>();
        }

        static public Dictionary<String,Double> constantDeclarations { get; private set; }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            string result = "";
            try
            {
                constantDeclarations = new Dictionary<string, double>();
                foreach(var tb in TextBoxs)
                {
                    if(tb.Text != "")
                    {
                        var keyAndValue = tb.Text.Split('=');
                        string key = keyAndValue[0].Trim();
                        double value = Convert.ToDouble(keyAndValue[1].Trim());
                        constantDeclarations.Add(key, value);
                    }

                }
                result = Equation.EquationResult(equationInput.Text);
            }
            catch(Exception)
            {
                result = "Syntax Error";
            }
            finally
            {
                equationOutput.Text = result;
            }
        }

        public List<TextBox> TextBoxs {get; private set; }

        private void NewConstantButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox tb = new TextBox();
            tb.Text = "a = 9";
            tb.TextAlignment = TextAlignment.Center;
            TextBoxs.Add(tb);
            ConstantsContainer.Children.Add(tb);
        }
    }
}
