using System.Windows;
using System.Windows.Controls;
using MyPrismApp.ViewModels;

namespace MyPrismApp.Views
{
    public partial class TextFieldView1 : UserControl
    {
        public TextFieldView1()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DataContext is TextFieldViewModel1 viewModel)
            {
                viewModel.SetFocus();
            }
        }
    }
}
