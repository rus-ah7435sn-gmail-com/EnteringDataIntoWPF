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

        public InputFieldViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            EnterKeyPressedCommand = new DelegateCommand(OnEnterKeyPressed);
        }

        private void OnEnterKeyPressed()
        {
            // Публикуем событие для переноса текста и очистки
            _eventAggregator.GetEvent<TransferInputToDisabledEvent>().Publish(InputText);
            InputText = string.Empty; // Очищаем поле ввода
        }
    }
}
