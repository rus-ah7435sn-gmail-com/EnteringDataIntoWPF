using Prism.Events;

namespace MyPrismApp.ViewModels.Events
{
    // Событие: текст в общем "Поле ввода" изменился
    public class SharedInputTextChangedEvent : PubSubEvent<string> { }

    // Событие: текст в InputFieldView изменился (для прямой передачи в MainViewModel)
    public class InputFieldChangedEvent : PubSubEvent<string> { }

    // Событие: команда на перенос текста из InputField в DisabledField (по нажатию Enter)
    public class TransferInputToDisabledEvent : PubSubEvent<string> { }
}
