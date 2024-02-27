using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RenameWizard
{
    /// <summary>
    /// Lógica de interacción para NumberTextBlock.xaml
    /// </summary>
    public partial class NumberTextBlock : UserControl
    {
        public NumberTextBlock()
        {
            InitializeComponent();
            textBox.Text = Value.ToString();
        }

        public static readonly DependencyProperty ValueProperty =
       DependencyProperty.Register("Value", typeof(int), typeof(NumberTextBlock), new PropertyMetadata(0));

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private void IncrementButton_Click(object sender, RoutedEventArgs e)
        {
            Value++;
            textBox.Text = Value.ToString();
        }

        private void DecrementButton_Click(object sender, RoutedEventArgs e)
        {
            if (Value > 0)
            {
                Value--;
                textBox.Text = Value.ToString();
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new("[^0-9]+");
            return !regex.IsMatch(text);
        }
    }
}
