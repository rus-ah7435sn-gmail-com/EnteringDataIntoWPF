using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
            var textBox = FindParent<TextBox>(e.OriginalSource as DependencyObject);

            if (textBox != null)
            {
                if (textBox.DataContext is ITextFieldViewModel vm)
                {
                    _mainViewModel.SetFocusedViewModel(vm as Prism.Mvvm.BindableBase);
                }
            }
            else
            {
                _mainViewModel.SetFocusedViewModel(null);
                Keyboard.ClearFocus();
            }
        }

        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
                return null;

            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }
    }
}
