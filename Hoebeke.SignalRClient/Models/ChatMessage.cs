using Caliburn.Micro;

namespace Hoebeke.SignalRClient.Models
{
    public class ChatMessage : PropertyChangedBase
    {
        private string _name;
        private string _message;

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                if (value == _message) return;
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }
    }
}
