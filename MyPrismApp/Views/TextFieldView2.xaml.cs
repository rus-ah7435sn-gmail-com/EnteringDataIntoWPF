using System.Windows.Controls;
using System.Windows.Input;
using MyPrismApp.ViewModels;
using Prism.Mvvm;
using System.Windows;

namespace MyPrismApp.Views
{
    public partial class TextFieldView2 : UserControl
    {
        public TextFieldView2()
        {
            InitializeComponent();
            this.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.DataContext is TextFieldViewModel2 vm && vm.GetMainViewModel() is MainViewModel mainVM)
            {
                mainVM.SetFocusedViewModel(vm);
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DataContext is MyPrismApp.ViewModels.TextFieldViewModel2 viewModel)
            {
                var mainViewModel = viewModel.GetMainViewModel();
                mainViewModel?.SetFocusedViewModel(viewModel);
            }
        }
    }
}
