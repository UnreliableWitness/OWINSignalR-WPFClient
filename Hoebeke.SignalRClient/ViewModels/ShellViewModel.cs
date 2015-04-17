using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using Hoebeke.SignalRClient.Models;

namespace Hoebeke.SignalRClient.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly string _name;
        private readonly ChatMessageManager _chatMessageManager;

        public BindableCollection<ChatMessage> MessageCollection { get; private set; }

        public ShellViewModel()
        {
            _name = string.Format("WPF Client {0}", DateTime.Now.Ticks);
            _chatMessageManager = new ChatMessageManager();
            MessageCollection = new BindableCollection<ChatMessage>();

            Task.Run(async () => await _chatMessageManager.ConnectAsync());

            _chatMessageManager.MessageReceived += ChatMessageManagerOnMessageReceived;
        }

        private void ChatMessageManagerOnMessageReceived(object sender, MessageEventArgs eventArgs)
        {
            MessageCollection.Add(new ChatMessage
            {
                Message = eventArgs.Message,
                Name = eventArgs.Name
            });
        }

        public void Send(string message)
        {
            _chatMessageManager.SendMessage(_name, message);
        }
    }
}
