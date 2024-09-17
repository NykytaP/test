using Core.Loaders.MainUI;
using Infrastructure.AssetsManagement;
using Infrastructure.Factories;
using Infrastructure.Factories.StateFactory;
using Infrastructure.Helpers.CancellationTokenHelper;
using Infrastructure.Helpers.GameObjectHelper;
using Infrastructure.Installers.SignalsHandler;
using Infrastructure.SceneManagement;
using Infrastructure.SessionStorage;
using Zenject;
namespace Infrastructure.Installers
{
    public class InfrastructureInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(ISessionStorage<>)).To(typeof(SessionStorage<>)).AsCached();
            Container.Bind<ICancellationTokenHelper>().To<CancellationTokenHelper>().AsTransient();

            DeclareSignals();
            InstallSignalsHandlers();

            BindFactories();
            BindLoaders();
            BindServices();
        }
        
        private void InstallSignalsHandlers()
        {
            Container.Bind<ISignalsHandlerInstaller>().To<SignalsHandlerInstaller>().AsSingle().NonLazy();
        }
        
        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<UIFactory>().AsCached();
        }
        
        private void BindLoaders()
        {
            Container.Bind<IMainUILoader>().To<MainUILoader>().AsTransient();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
            Container.Bind<SceneLoader>().AsSingle();
            
            Container.Bind<IGameObjectHelper>().To<GameObjectHelper>().AsSingle();
        }

        private void DeclareSignals()
        {
            SignalBusInstaller.Install(Container);
            SignalBus signalBus = Container.Resolve<SignalBus>();
        }
    }
}