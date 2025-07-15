using System.Windows;
using System.Windows.Controls;
using MyPrismApp.ViewModels;

namespace MyPrismApp.Views
{
    public partial class DisabledTextFieldView : UserControl
    {
        public DisabledTextFieldView()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DataContext is DisabledTextFieldViewModel viewModel)
            {
                viewModel.SetFocus();
            }
        }
    }
}
