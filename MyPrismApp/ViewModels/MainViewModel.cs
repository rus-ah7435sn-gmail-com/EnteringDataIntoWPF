using Prism.Mvvm;
using Prism.Events;
using MyPrismApp.ViewModels.Events;
using System;

namespace MyPrismApp.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

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
                    _eventAggregator.GetEvent<SharedInputTextChangedEvent>().Publish(string.Empty);
                }
            }
        }

        public MainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void SetFocusedViewModel(BindableBase? viewModel)
        {
            FocusedViewModel = viewModel;
        }

        public void RelayInputToFocusedViewModel(string text)
        {
            if (FocusedViewModel is TextFieldViewModel1 vm1)
            {
                vm1.TextValue = text;
            }
            else if (FocusedViewModel is TextFieldViewModel2 vm2)
            {
                vm2.TextValue = text;
            }
            else if (FocusedViewModel is TextFieldViewModel3 vm3)
            {
                vm3.TextValue = text;
            }
            else if (FocusedViewModel is DisabledTextFieldViewModel dvm)
            {
                dvm.TextValue = text;
            }
            else if (FocusedViewModel == null)
            {
                WindowTitle = text;
            }
        }
    }
}
