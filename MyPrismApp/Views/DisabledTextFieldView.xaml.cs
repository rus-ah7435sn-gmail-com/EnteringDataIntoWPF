using System.Windows;
using System.Windows.Controls;
using MyPrismApp.ViewModels;

namespace MyPrismApp.Views
{
    /// <summary>
    /// Interaction logic for DisabledTextFieldView.xaml
    /// </summary>
    public partial class DisabledTextFieldView : UserControl
    {
        public DisabledTextFieldView()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DataContext is DisabledTextFieldViewModel viewModel &&
                (Application.Current as Prism.DryIoc.PrismApplication).Container.Resolve(typeof(MainViewModel)) is MainViewModel mainViewModel)
            {
                mainViewModel.SetFocusedViewModel(viewModel);
            }
        }
    }
}
