using Prism.Mvvm;

namespace MyPrismApp.ViewModels
{
    public class TextFieldViewModel3 : BindableBase
    {
        private string _textValue = string.Empty;
        public string TextValue
        {
            get { return _textValue; }
            set { SetProperty(ref _textValue, value); }
        }

        public TextFieldViewModel3()
        {
            // TextValue уже инициализирован string.Empty при объявлении
        }
    }
}
