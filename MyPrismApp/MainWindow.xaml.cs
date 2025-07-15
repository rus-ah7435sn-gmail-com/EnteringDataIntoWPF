using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MyPrismApp.ViewModels;
using MyPrismApp.Views;

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
            if (e.OriginalSource is not TextBox &&
                e.OriginalSource is not TextFieldView1 &&
                e.OriginalSource is not TextFieldView2 &&
                e.OriginalSource is not TextFieldView3 &&
                e.OriginalSource is not DisabledTextFieldView)
            {
                _mainViewModel.SetFocusedViewModel(null);
                Keyboard.ClearFocus();
            }
        }
    }
}
