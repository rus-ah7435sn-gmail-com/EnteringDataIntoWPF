using Prism.Mvvm;
using Prism.Events;
using MyPrismApp.ViewModels.Events;

namespace MyPrismApp.ViewModels
{
    public class TextFieldViewModel1 : BindableBase, ITextFieldViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        public MainViewModel MainViewModel { get; set; }

        private string _textValue = string.Empty;
        public string TextValue
        {
            get { return _textValue; }
            set { SetProperty(ref _textValue, value); }
        }

        public TextFieldViewModel1(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<SharedInputTextChangedEvent>().Subscribe(OnSharedInputTextChanged, ThreadOption.UIThread);
        }

        private void OnSharedInputTextChanged(string newText)
        {
            if (MainViewModel.FocusedViewModel == this)
            {
                TextValue = newText;
            }
        }

        public void SetFocus()
        {
            MainViewModel.SetFocusedViewModel(this);
        }
    }
}
