using Infrastructure.Helpers.CancellationTokenHelper;
using Zenject;
namespace Infrastructure.Installers.SignalsHandler
{
    public class SignalsHandlerInstaller : ISignalsHandlerInstaller
    {
        private readonly SignalBus _signalBus;
        private readonly DiContainer _container;
        private readonly ICancellationTokenHelper _cancellationTokenHelper;

        public SignalsHandlerInstaller(SignalBus signalBus, DiContainer container, ICancellationTokenHelper cancellationTokenHelper)
        {
            _signalBus = signalBus;
            _container = container;
            _cancellationTokenHelper = cancellationTokenHelper;

            InstallHandlers();
        }
        
        private void InstallHandlers() {}
    }
}