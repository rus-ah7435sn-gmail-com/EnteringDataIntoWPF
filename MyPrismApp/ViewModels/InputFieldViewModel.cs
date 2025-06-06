using Prism.Mvvm;
using Prism.Events;
using MyPrismApp.ViewModels.Events;
using Prism.Commands;
using System.Windows.Input;

namespace MyPrismApp.ViewModels
{
    public class InputFieldViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly MainViewModel _mainViewModel; // Добавлено

        private string _inputText = string.Empty;
        public string InputText
        {
            get { return _inputText; }
            set
            {
                if (SetProperty(ref _inputText, value))
                {
                    // Сообщаем MainViewModel об изменении текста в этом поле
                    _eventAggregator.GetEvent<InputFieldChangedEvent>().Publish(_inputText);
                }
            }
        }

        public DelegateCommand EnterKeyPressedCommand { get; private set; }

        public InputFieldViewModel(IEventAggregator eventAggregator, MainViewModel mainViewModel) // MainViewModel добавлен
        {
            _eventAggregator = eventAggregator;
            _mainViewModel = mainViewModel; // Сохраняем ссылку
            EnterKeyPressedCommand = new DelegateCommand(OnEnterKeyPressed);
        }

        private void OnEnterKeyPressed()
        {
            if (_mainViewModel.FocusedViewModel == null)
            {
                _eventAggregator.GetEvent<TransferInputToDisabledEvent>().Publish(InputText);
            }
            // Очищаем поле ввода в любом случае, когда нажат Enter в этом поле.
            // Если FocusedViewModel != null (т.е. фокус на TextFieldView1/2/3),
            // Enter просто очистит InputField, не передавая текст в DisabledTextFieldView.
            InputText = string.Empty;
        }

        public MainViewModel GetMainViewModel()
        {
            return _mainViewModel;
        }
    }
}
