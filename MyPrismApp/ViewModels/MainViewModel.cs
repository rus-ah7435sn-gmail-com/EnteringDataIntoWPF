using Prism.Mvvm;
using Prism.Events;
using MyPrismApp.ViewModels.Events;
using System; // For Nullable reference types

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
                    // Публикуем событие, что текст в Поле Ввода (SharedInputText) изменился
                    _eventAggregator.GetEvent<SharedInputTextChangedEvent>().Publish(_sharedInputText);
                }
            }
        }

        private BindableBase? _focusedViewModel;
        public BindableBase? FocusedViewModel
        {
            get { return _focusedViewModel; }
            private set
            {
                // Сравниваем не просто по ссылке, а учитываем null
                if (!object.Equals(_focusedViewModel, value))
                {
                    // var oldFocusedViewModel = _focusedViewModel; // Сохраняем старое значение если нужно для события
                    _focusedViewModel = value;
                    RaisePropertyChanged(nameof(FocusedViewModel)); // Уведомляем об изменении

                    // После изменения FocusedViewModel, нужно инициировать обновление текста.
                    // Публикация SharedInputTextChangedEvent заставит всех подписчиков перепроверить свое состояние.
                    _eventAggregator.GetEvent<SharedInputTextChangedEvent>().Publish(SharedInputText);

                    // Можно также отправить специфическое событие о смене FocusedViewModel, если это нужно другим частям системы.
                    // _eventAggregator.GetEvent<FocusedViewModelChangedEvent>().Publish(new FocusedViewModelChangedEventArgs(oldFocusedViewModel, _focusedViewModel));
                    // Пока это не требуется по плану.
                }
            }
        }

        public MainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            // Подписка на событие ввода текста непосредственно в InputFieldViewModel
            _eventAggregator.GetEvent<InputFieldChangedEvent>().Subscribe(OnInputFieldChanged);
            // Старая подписка на TextFieldFocusChangedEvent удалена, так как фокус управляется через FocusedViewModel
        }

        private void OnInputFieldChanged(string newText)
        {
            // Этот текст пришел из InputFieldView. Записываем его в SharedInputText.
            // SharedInputText затем вызовет SharedInputTextChangedEvent.
            // Подписчики (TextFieldViewModels и DisabledTextFieldViewModel) решат, что с ним делать,
            // основываясь на FocusedViewModel.
            SharedInputText = newText;
        }

        public void SetFocusedViewModel(BindableBase? viewModel)
        {
            // Этот метод будет вызываться извне (например, из обработчиков кликов в Views или VM)
            FocusedViewModel = viewModel;
        }
    }
}
