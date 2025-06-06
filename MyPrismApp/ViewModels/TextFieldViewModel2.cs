using Prism.Mvvm;
using Prism.Events;
using MyPrismApp.ViewModels.Events;

namespace MyPrismApp.ViewModels
{
    public class TextFieldViewModel2 : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private MainViewModel _mainViewModel;

        private string _textValue = string.Empty;
        public string TextValue
        {
            get { return _textValue; }
            set { SetProperty(ref _textValue, value); }
        }

        public TextFieldViewModel2(IEventAggregator eventAggregator, MainViewModel mainViewModel)
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
            else
            {
                TextValue = string.Empty;
            }
        }

        public MainViewModel GetMainViewModel()
        {
            return _mainViewModel;
        }
    }
}
