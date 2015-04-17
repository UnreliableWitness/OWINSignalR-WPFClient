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


            var all = new Mock<IClientContract>();

            hub.Clients = mockClients.Object;

            all.Setup(m => m.broadcastMessage(It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            mockClients.Setup(m => m.All).Returns(all.Object);

            hub.Send("test user", "test message");

            all.VerifyAll();
        }
    }
}
