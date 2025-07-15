using Prism.Mvvm;
using Prism.Events;
using MyPrismApp.ViewModels.Events;

namespace MyPrismApp.ViewModels
{
    public class TextFieldViewModel1 : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly MainViewModel _mainViewModel;

        private string _textValue = string.Empty;
        public string TextValue
        {
            get { return _textValue; }
            set { SetProperty(ref _textValue, value); }
        }

        public TextFieldViewModel1(IEventAggregator eventAggregator, MainViewModel mainViewModel)
        {
            _eventAggregator = eventAggregator;
            _mainViewModel = mainViewModel;

            _eventAggregator.GetEvent<SharedInputTextChangedEvent>().Subscribe(OnSharedInputTextChanged, ThreadOption.UIThread);
        }

        private void OnSharedInputTextChanged(string newText)
        {
            if (_mainViewModel.FocusedViewModel == this)
            {
                TextValue = newText;
            }
        }

        public void SetFocus()
        {
            _mainViewModel.SetFocusedViewModel(this);
        }
    }
}
