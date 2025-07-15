using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MyPrismApp.ViewModels;

namespace MyPrismApp
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _mainViewModel;

        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
            _mainViewModel = mainViewModel;
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is TextBox textBox)
            {
                if (textBox.DataContext is ITextFieldViewModel vm)
                {
                    _mainViewModel.SetFocusedViewModel(vm as BindableBase);
                }
            }
            else
            {
                _mainViewModel.SetFocusedViewModel(null);
                Keyboard.ClearFocus();
            }
        }
    }
}
