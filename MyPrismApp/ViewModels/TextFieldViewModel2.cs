using Prism.Mvvm;

namespace MyPrismApp.ViewModels
{
    public class TextFieldViewModel2 : BindableBase
    {
        private string _textValue = string.Empty;
        public string TextValue
        {
            get { return _textValue; }
            set { SetProperty(ref _textValue, value); }
        }

        public TextFieldViewModel2()
        {
            // TextValue уже инициализирован string.Empty при объявлении
        }
    }
}
