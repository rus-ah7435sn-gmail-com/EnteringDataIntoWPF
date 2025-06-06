using System.Windows.Controls;
using System.Windows.Input;
using MyPrismApp.ViewModels; // Для доступа к MainViewModel и TextFieldViewModel1
using Prism.Mvvm; // Для ViewModelLocator
using System.Windows;

namespace MyPrismApp.Views
{
    public partial class TextFieldView1 : UserControl
    {
        public TextFieldView1()
        {
            InitializeComponent();
            this.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.DataContext is TextFieldViewModel1 vm && vm.GetMainViewModel() is MainViewModel mainVM)
            {
                mainVM.SetFocusedViewModel(vm);
                // Остановить дальнейшую обработку события не нужно, чтобы клик мог, например,
                // инициировать потерю фокуса у другого элемента, если это необходимо системе.
                // e.Handled = true; // - возможно, не нужно
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DataContext is MyPrismApp.ViewModels.TextFieldViewModel1 viewModel)
            {
                var mainViewModel = viewModel.GetMainViewModel();
                mainViewModel?.SetFocusedViewModel(viewModel);
            }
        }
    }
}
