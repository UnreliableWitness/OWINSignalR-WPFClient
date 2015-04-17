using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.AspNet.SignalR.Client;

namespace Hoebeke.SignalRClient
{

    public delegate void ConnectionOpenedEventHandler(object sender, EventArgs e);
    public delegate void ConnectionClosedHandler(object sender, EventArgs e);
    public delegate void MessageReceivedEventHandler(object sender, MessageEventArgs e);

    public class ChatMessageManager
    {
        public event ConnectionOpenedEventHandler ConnectionOpened;
        public event ConnectionClosedHandler ConnectionClosed;
        public event MessageReceivedEventHandler MessageReceived;


        public IHubProxy HubProxy { get; set; }
        const string ServerUri = "http://localhost:8080/signalr";
        public HubConnection Connection { get; set; }

        public async Task ConnectAsync()
        {
            Connection = new HubConnection(ServerUri);
            Connection.Closed += Connection_Closed;
            HubProxy = Connection.CreateHubProxy("ChatHub");

            HubProxy.On<string, string>("BroadcastMessage", (name, message) =>
                {
                    if(MessageReceived != null)
                        MessageReceived(this, new MessageEventArgs
                        {
                            Message = message,
                            Name = name
                        });
                }
            );
            try
            {
                await Connection.Start();
                if(ConnectionOpened != null)
                    ConnectionOpened(this, new EventArgs());
            }
            catch (HttpRequestException)
            {
                return;
            }
        }

        private void Connection_Closed()
        {
            if(ConnectionClosed != null)
                ConnectionClosed(this, new EventArgs());
        }

        public void SendMessage(string name, string message)
        {
            HubProxy.Invoke("Send", name, message);
        }
    }

    public class MessageEventArgs : EventArgs
    {
        public string Name { get; set; }

        public string Message { get; set; }
    }
}
