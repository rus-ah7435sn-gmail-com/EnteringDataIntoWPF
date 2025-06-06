using Prism.Mvvm;
using Prism.Events;
using MyPrismApp.ViewModels.Events;

namespace MyPrismApp.ViewModels
{
    public class TextFieldViewModel1 : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private MainViewModel _mainViewModel;

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
            else
            {
                // Если это поле не является "фокусным", его текст должен быть очищен.
                // При смене FocusedViewModel в MainViewModel публикуется SharedInputTextChangedEvent,
                // тогда этот метод вызовется для всех TextFieldVM. Тот, кто был FocusedViewModel,
                // перестанет им быть и должен очиститься.
                // Тот, кто стал FocusedViewModel, обновится.
                TextValue = string.Empty;
            }
        }

        public MainViewModel GetMainViewModel()
        {
            return _mainViewModel;
        }
    }
}
