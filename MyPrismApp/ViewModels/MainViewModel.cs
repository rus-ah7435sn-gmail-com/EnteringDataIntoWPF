using Prism.Mvvm;
using Prism.Events; // Для EventAggregator
using MyPrismApp.ViewModels.Events; // Пространство имен для кастомных событий

namespace MyPrismApp.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        private string _sharedInputText = string.Empty;
        public string SharedInputText
        {
            get { return _sharedInputText; }
            set
            {
                if (SetProperty(ref _sharedInputText, value))
                {
                    // Публикуем событие, что текст в Поле Ввода изменился
                    _eventAggregator.GetEvent<SharedInputTextChangedEvent>().Publish(_sharedInputText);
                }
            }
        }

        // Свойство для отслеживания, активны ли основные поля ввода
        // 0 - не активны (ввод идет в Disabled)
        // 1 - активны TextFieldView1/2/3
        private int _activeInputTarget = 0; // 0 = Disabled, 1 = TextField1/2/3
        public int ActiveInputTarget
        {
            get => _activeInputTarget;
            set => SetProperty(ref _activeInputTarget, value);
        }


        public MainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            // Подписка на событие получения фокуса одним из TextFieldView 1/2/3
            _eventAggregator.GetEvent<TextFieldFocusChangedEvent>().Subscribe(OnTextFieldFocusChanged);
            // Подписка на событие ввода текста непосредственно в InputFieldViewModel
            _eventAggregator.GetEvent<InputFieldChangedEvent>().Subscribe(OnInputFieldChanged);
        }

        private void OnInputFieldChanged(string newText)
        {
            // Это событие будет вызываться из InputFieldViewModel
            // На его основе обновляем либо активные поля, либо DisabledTextFieldView
            SharedInputText = newText; // Обновляем наше свойство и публикуем SharedInputTextChangedEvent
        }

        private void OnTextFieldFocusChanged(bool hasFocus)
        {
            ActiveInputTarget = hasFocus ? 1 : 0;
            // При изменении фокуса, если текст уже есть в SharedInputText,
            // его нужно "перенаправить"
            _eventAggregator.GetEvent<SharedInputTextChangedEvent>().Publish(SharedInputText);
        }
    }
}
