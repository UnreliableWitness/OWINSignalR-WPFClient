using System.Windows;
using Caliburn.Micro;
using Hoebeke.SignalRClient.ViewModels;

namespace Hoebeke.SignalRClient
{
    public class MyBootstrapper : BootstrapperBase
    {
        public MyBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
