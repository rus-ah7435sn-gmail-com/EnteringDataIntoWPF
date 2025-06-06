using Prism.Mvvm;
using Prism.Events;
using MyPrismApp.ViewModels.Events;

namespace MyPrismApp.ViewModels
{
    public class TextFieldViewModel1 : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private MainViewModel _mainViewModel; // Нужна ссылка или способ узнать ActiveInputTarget

        private string _textValue = string.Empty;
        public string TextValue
        {
            get { return _textValue; }
            set { SetProperty(ref _textValue, value); }
        }

        // Свойство для привязки к IsFocused из View
        private bool _isFocused;
        public bool IsFocused
        {
            get => _isFocused;
            set
            {
                if (SetProperty(ref _isFocused, value))
                {
                    // Сообщаем MainViewModel об изменении фокуса
                    _eventAggregator.GetEvent<TextFieldFocusChangedEvent>().Publish(_isFocused);
                }
            }
        }

        public TextFieldViewModel1(IEventAggregator eventAggregator, MainViewModel mainViewModel)
        {
            _eventAggregator = eventAggregator;
            _mainViewModel = mainViewModel; // Инъекция MainViewModel (нужно настроить в DI контейнере)

            _eventAggregator.GetEvent<SharedInputTextChangedEvent>().Subscribe(OnSharedInputTextChanged);
        }

        private void OnSharedInputTextChanged(string newText)
        {
            // Обновляемся только если фокус на нас ИЛИ если мы часть группы активных полей
            if (_mainViewModel.ActiveInputTarget == 1) // 1 означает, что TextField1/2/3 активны
            {
                TextValue = newText;
            }
        }
    }
}
