using Prism.Mvvm;

namespace MyPrismApp.ViewModels
{
    public class DisabledTextFieldViewModel : BindableBase
    {
        private string _textValue = string.Empty;
        public string TextValue
        {
            get { return _textValue; }
            set { SetProperty(ref _textValue, value); }
        }

        public DisabledTextFieldViewModel()
        {
        }

        public void AppendText(string text)
        {
            TextValue += text;
        }
    }
}
