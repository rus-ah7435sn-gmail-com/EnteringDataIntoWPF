using Prism.Mvvm;

namespace MyPrismApp.ViewModels
{
    public class DisabledTextFieldViewModel : BindableBase
    {
        private string _textValue = string.Empty;
        public string TextValue
        {
            get { return _textValue; }
            // Сеттер делаем приватным или защищенным, если изменение только через AppendText
            // Для простоты примера оставим публичным, но учтем, что Mode=OneWay в View
            set { SetProperty(ref _textValue, value); }
        }

        public DisabledTextFieldViewModel()
        {
            // TextValue уже инициализирован string.Empty при объявлении
        }

        public void AppendText(string text)
        {
            TextValue += text;
        }
    }
}
