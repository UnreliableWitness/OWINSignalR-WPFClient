using System.Dynamic;
using Hoebeke.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using NUnit.Framework;

namespace Hoebeke.Tests
{
    [TestFixture]
    public class Test
    {
        public interface IClientContract
        {
            void broadcastMessage(string name, string message);
        }

        [Test]
        public void BroadcastMessage_ShouldBeInvoked()
        {
            var hub = new ChatHub();

            //context that contains the client connections
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            //make the hub believe it has actual clients
            hub.Clients = mockClients.Object;

            //create a mock of the client contract, this is the interface that defines what methods are available for clients
            var client = new Mock<IClientContract>();
            //set the mock up so that the available method has restrictions on parameters
            client.Setup(m => m.broadcastMessage(It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            //make the all property of the clients return an instance that implements the iclientcontract
            mockClients.Setup(m => m.All).Returns(client.Object);

            //invoke the "Send" method on this hub, this will in turn invoke the broadCastMessage on the connected clients
            //debug into this hub method to more easily understand what previous fluf is all about
            hub.Send("user", "message");

            client.VerifyAll();
        }
    }
}
