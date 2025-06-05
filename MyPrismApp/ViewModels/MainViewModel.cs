using Prism.Mvvm;

namespace MyPrismApp.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private string _message = "Hello from MainViewModel!";
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public MainViewModel()
        {

        }
    }
}
