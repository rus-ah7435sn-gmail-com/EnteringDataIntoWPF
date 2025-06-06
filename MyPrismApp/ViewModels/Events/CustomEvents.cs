using Prism.Events;

namespace MyPrismApp.ViewModels.Events
{
    // Событие: текст в общем "Поле ввода" изменился
    public class SharedInputTextChangedEvent : PubSubEvent<string> { }

    // Событие: фокус на одном из TextFieldView1/2/3 изменился (true = получил фокус, false = потерял)
    public class TextFieldFocusChangedEvent : PubSubEvent<bool> { }

    // Событие: текст в InputFieldView изменился (для прямой передачи в MainViewModel)
    public class InputFieldChangedEvent : PubSubEvent<string> { }

    // Событие: команда на перенос текста из InputField в DisabledField (по нажатию Enter)
    public class TransferInputToDisabledEvent : PubSubEvent<string> { }
}
