using Prism.Mvvm;

namespace MyPrismApp.ViewModels
{
    public class DisabledTextFieldViewModel : BindableBase, ITextFieldViewModel
    {
        public MainViewModel MainViewModel { get; set; }

        private string _textValue = string.Empty;
        public string TextValue
        {
            get { return _textValue; }
            set { SetProperty(ref _textValue, value); }
        }

        public DisabledTextFieldViewModel()
        {
        }

        public void SetFocus()
        {
            MainViewModel.SetFocusedViewModel(this);
        }
    }
}
