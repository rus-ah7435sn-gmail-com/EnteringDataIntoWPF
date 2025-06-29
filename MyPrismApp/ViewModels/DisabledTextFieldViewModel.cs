using Prism.Mvvm;
using Prism.Events;
using MyPrismApp.ViewModels.Events;

namespace MyPrismApp.ViewModels
{
    public class DisabledTextFieldViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        // Ссылка на MainViewModel больше не нужна для логики обновления текста,
        // так как решение о передаче текста сюда принимается по событию TransferInputToDisabledEvent,
        // которое должно публиковаться InputFieldViewModel только в подходящих сценариях (когда фокус не на TextField1/2/3).
        // Однако, если бы DisabledTextFieldView должен был очищаться при фокусе на TextField1/2/3, ссылка бы понадобилась.
        // По текущему заданию, он просто принимает текст по Enter.

        private string _textValue = string.Empty;
        public string TextValue
        {
            get { return _textValue; }
            set { SetProperty(ref _textValue, value); }
        }

        public DisabledTextFieldViewModel(IEventAggregator eventAggregator) // Убрали MainViewModel из конструктора
        {
            _eventAggregator = eventAggregator;

            // Убираем подписку на SharedInputTextChangedEvent
            // _eventAggregator.GetEvent<SharedInputTextChangedEvent>().Subscribe(OnSharedInputTextChanged);

            _eventAggregator.GetEvent<TransferInputToDisabledEvent>().Subscribe(OnTransferInputToDisabledReceived);
        }

        // Этот метод больше не нужен, если DisabledTextFieldViewModel не должен динамически отображать ввод
        // private void OnSharedInputTextChanged(string newText)
        // {
        //     if (_mainViewModel.ActiveInputTarget == 0)
        //     {
        //         TextValue = newText;
        //     }
        // }

        private void OnTransferInputToDisabledReceived(string textToTransfer)
        {
            // Это событие приходит, когда Enter нажат в InputFieldViewModel.
            // Логика MainViewModel.ActiveInputTarget == 0 должна быть обеспечена тем,
            // кто вызывает TransferInputToDisabledEvent, или здесь должна быть проверка,
            // но по заданию "при нажатии энтер должно перенестись в DisabledTextFieldView",
            // если до этого ввод шел в "поле ввода" (которое не TextField1/2/3).
            TextValue = textToTransfer;
        }
    }
}
