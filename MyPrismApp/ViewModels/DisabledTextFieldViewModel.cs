using Prism.Mvvm;

namespace MyPrismApp.ViewModels
{
    public class DisabledTextFieldViewModel : BindableBase, ITextFieldViewModel
    {
        private readonly MainViewModel _mainViewModel;

        private string _textValue = string.Empty;
        public string TextValue
        {
            get { return _textValue; }
            set { SetProperty(ref _textValue, value); }
        }

        public DisabledTextFieldViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public void SetFocus()
        {
            _mainViewModel.SetFocusedViewModel(this);
        }
    }
}
