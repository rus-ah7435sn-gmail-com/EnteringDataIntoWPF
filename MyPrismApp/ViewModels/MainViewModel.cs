using Prism.Mvvm;
using Prism.Events;
using MyPrismApp.ViewModels.Events;
using System;
using System.Linq;
using Prism.Ioc;

namespace MyPrismApp.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IContainerProvider _containerProvider;

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

        public MainViewModel(IEventAggregator eventAggregator, IContainerProvider containerProvider)
        {
            _eventAggregator = eventAggregator;
            _containerProvider = containerProvider;

            var textFieldViewModel1 = _containerProvider.Resolve<TextFieldViewModel1>();
            textFieldViewModel1.MainViewModel = this;

            var textFieldViewModel2 = _containerProvider.Resolve<TextFieldViewModel2>();
            textFieldViewModel2.MainViewModel = this;

            var textFieldViewModel3 = _containerProvider.Resolve<TextFieldViewModel3>();
            textFieldViewModel3.MainViewModel = this;

            var disabledTextFieldViewModel = _containerProvider.Resolve<DisabledTextFieldViewModel>();
            disabledTextFieldViewModel.MainViewModel = this;
        }

        public void SetFocusedViewModel(BindableBase? viewModel)
        {
            FocusedViewModel = viewModel;

            var allTextViewModels = new BindableBase[]
            {
                _containerProvider.Resolve<TextFieldViewModel1>(),
                _containerProvider.Resolve<TextFieldViewModel2>(),
                _containerProvider.Resolve<TextFieldViewModel3>(),
                _containerProvider.Resolve<DisabledTextFieldViewModel>()
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
