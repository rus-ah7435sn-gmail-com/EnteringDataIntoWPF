using Prism.Mvvm;
using Prism.Events;
using MyPrismApp.ViewModels.Events;
using System;
using System.Linq;

namespace MyPrismApp.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly TextFieldViewModel1 _textFieldViewModel1;
        private readonly TextFieldViewModel2 _textFieldViewModel2;
        private readonly TextFieldViewModel3 _textFieldViewModel3;
        private readonly DisabledTextFieldViewModel _disabledTextFieldViewModel;

        private string _windowTitle = "My Prism App - Regions";
        public string WindowTitle
        {
            get { return _windowTitle; }
            set { SetProperty(ref _windowTitle, value); }
        }

        private string _sharedInputText = string.Empty;
        public string SharedInputText
        {
            get { return _sharedInputText; }
            set
            {
                if (SetProperty(ref _sharedInputText, value))
                {
                    _eventAggregator.GetEvent<SharedInputTextChangedEvent>().Publish(_sharedInputText);
                }
            }
        }

        private BindableBase? _focusedViewModel;
        public BindableBase? FocusedViewModel
        {
            get { return _focusedViewModel; }
            private set
            {
                if (!object.Equals(_focusedViewModel, value))
                {
                    _focusedViewModel = value;
                    RaisePropertyChanged(nameof(FocusedViewModel));
                }
            }
        }

        public MainViewModel(IEventAggregator eventAggregator,
                               TextFieldViewModel1 textFieldViewModel1,
                               TextFieldViewModel2 textFieldViewModel2,
                               TextFieldViewModel3 textFieldViewModel3,
                               DisabledTextFieldViewModel disabledTextFieldViewModel)
        {
            _eventAggregator = eventAggregator;
            _textFieldViewModel1 = textFieldViewModel1;
            _textFieldViewModel2 = textFieldViewModel2;
            _textFieldViewModel3 = textFieldViewModel3;
            _disabledTextFieldViewModel = disabledTextFieldViewModel;
        }

        public void SetFocusedViewModel(BindableBase? viewModel)
        {
            FocusedViewModel = viewModel;

            var allTextViewModels = new BindableBase[]
            {
                _textFieldViewModel1, _textFieldViewModel2, _textFieldViewModel3, _disabledTextFieldViewModel
            };

            foreach (var vm in allTextViewModels.OfType<ITextFieldViewModel>())
            {
                if (vm != viewModel)
                {
                    vm.TextValue = string.Empty;
                }
            }
        }

        public void RelayInputToFocusedViewModel(string text)
        {
            if (FocusedViewModel is ITextFieldViewModel vm)
            {
                vm.TextValue = text;
            }
            else if (FocusedViewModel == null)
            {
                WindowTitle = text;
            }
        }
    }

    public interface ITextFieldViewModel
    {
        string TextValue { get; set; }
    }
}
