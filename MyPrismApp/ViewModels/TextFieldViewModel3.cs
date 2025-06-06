using Prism.Mvvm;
using Prism.Events;
using MyPrismApp.ViewModels.Events;

namespace MyPrismApp.ViewModels
{
    public class TextFieldViewModel3 : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private MainViewModel _mainViewModel;

        private string _textValue = string.Empty;
        public string TextValue
        {
            get { return _textValue; }
            set { SetProperty(ref _textValue, value); }
        }

        private bool _isFocused;
        public bool IsFocused
        {
            get => _isFocused;
            set
            {
                if (SetProperty(ref _isFocused, value))
                {
                    _eventAggregator.GetEvent<TextFieldFocusChangedEvent>().Publish(_isFocused);
                }
            }
        }

        public TextFieldViewModel3(IEventAggregator eventAggregator, MainViewModel mainViewModel)
        {
            _eventAggregator = eventAggregator;
            _mainViewModel = mainViewModel;
            _eventAggregator.GetEvent<SharedInputTextChangedEvent>().Subscribe(OnSharedInputTextChanged);
        }

        private void OnSharedInputTextChanged(string newText)
        {
            if (_mainViewModel.ActiveInputTarget == 1)
            {
                TextValue = newText;
            }
        }
    }
}
