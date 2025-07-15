using System.Windows;
using System.Windows.Controls;
using MyPrismApp.ViewModels;

namespace MyPrismApp.Views
{
    public partial class InputFieldView : UserControl
    {
        public InputFieldView()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DataContext is InputFieldViewModel viewModel)
            {
                viewModel.SetFocus();
            }
        }
    }
}
