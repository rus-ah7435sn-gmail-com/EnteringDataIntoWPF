using System.Windows.Controls;
using System.Windows.Input;
using MyPrismApp.ViewModels; // Для доступа к MainViewModel и InputFieldViewModel
using Prism.Mvvm; // Для ViewModelLocator

namespace MyPrismApp.Views
{
    public partial class InputFieldView : UserControl
    {
        public InputFieldView()
        {
            InitializeComponent();
            this.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
        }

        private MainViewModel? GetMainViewModelFromDataContext()
        {
            // InputFieldViewModel теперь имеет ссылку на MainViewModel и метод GetMainViewModel()
            if (this.DataContext is InputFieldViewModel vm)
            {
                return vm.GetMainViewModel();
            }
            return null;
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // При клике на InputFieldView, мы хотим установить FocusedViewModel в null.
            // Это позволит вводу идти в DisabledTextFieldView по Enter (согласно логике в InputFieldViewModel.OnEnterKeyPressed).
            var mainVM = GetMainViewModelFromDataContext();
            if (mainVM != null)
            {
                mainVM.SetFocusedViewModel(null);
            }
            // Не ставим e.Handled = true, чтобы TextBox внутри мог получить реальный фокус клавиатуры,
            // и чтобы событие могло всплыть, если это необходимо.
        }
    }
}
