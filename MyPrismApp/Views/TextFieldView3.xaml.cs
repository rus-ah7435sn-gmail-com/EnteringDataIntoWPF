using System.Windows;
using System.Windows.Controls;
using MyPrismApp.ViewModels;

namespace MyPrismApp.Views
{
    public partial class TextFieldView3 : UserControl
    {
        public TextFieldView3()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DataContext is TextFieldViewModel3 viewModel)
            {
                viewModel.SetFocus();
            }
        }
    }
}
