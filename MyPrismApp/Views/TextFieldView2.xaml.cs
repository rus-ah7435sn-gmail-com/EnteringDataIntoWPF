using System.Windows.Controls;
using System.Windows.Input;
using MyPrismApp.ViewModels;
using Prism.Mvvm;

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
    }
}
