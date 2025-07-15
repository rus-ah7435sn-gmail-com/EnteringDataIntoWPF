using Prism.Mvvm;
using Prism.Commands;

namespace MyPrismApp.ViewModels
{
    public class InputFieldViewModel : BindableBase
    {
        private readonly MainViewModel _mainViewModel;

        private string _inputText = string.Empty;
        public string InputText
        {
            get { return _inputText; }
            set { SetProperty(ref _inputText, value); }
        }

        public DelegateCommand EnterKeyPressedCommand { get; private set; }

        public InputFieldViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            EnterKeyPressedCommand = new DelegateCommand(OnEnterKeyPressed);
        }

        private void OnEnterKeyPressed()
        {
            _mainViewModel.RelayInputToFocusedViewModel(InputText);
            InputText = string.Empty;
        }
    }
}
