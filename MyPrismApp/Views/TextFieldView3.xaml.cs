using System.Windows.Controls;
using System.Windows.Input;
using MyPrismApp.ViewModels;
using Prism.Mvvm;

namespace MyPrismApp.Views
{
    public partial class TextFieldView3 : UserControl
    {
        public TextFieldView3()
        {
            InitializeComponent();
            this.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.DataContext is TextFieldViewModel3 vm && vm.GetMainViewModel() is MainViewModel mainVM)
            {
                mainVM.SetFocusedViewModel(vm);
            }
        }
    }
}
