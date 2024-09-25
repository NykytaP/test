using Core.Factories;
using Core.Loaders.BotLoader;
using Core.Loaders.Bullets;
using Core.Loaders.MainUI;
using Core.Loaders.Player;
using Core.Loaders.VFX;
using Core.Pools.Bullets;
using Core.Pools.VFX;
using Core.Presenters.Bot;
using Core.Presenters.Player;
using Core.Services.BotSpawner;
using Core.Services.Health;
using Core.Services.Movement;
using Core.Services.Movement.Bot;
using Core.Services.Movement.Player;
using Core.Services.Shooting;
using Core.Services.Shooting.Player;
using Core.Services.TickableRunner;
using Infrastructure.AssetsManagement;
using Infrastructure.Factories;
using Infrastructure.Factories.StateFactory;
using Infrastructure.Helpers.CancellationTokenHelper;
using Infrastructure.Helpers.GameObjectHelper;
using Infrastructure.Installers.SignalsHandler;
using Infrastructure.ObjectPool;
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
            BindPresenters();
            BindManagers();
        }

        private void DeclareSignals()
        {
            SignalBusInstaller.Install(Container);
            SignalBus signalBus = Container.Resolve<SignalBus>();
        }

        private void InstallSignalsHandlers()
        {
            Container.Bind<ISignalsHandlerInstaller>().To<SignalsHandlerInstaller>().AsSingle().NonLazy();
        }
        
        private void BindFactories()
        {
            Container.Bind(typeof(IObjectPool<>)).To(typeof(ObjectPool<>)).AsCached();
            
            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<UIFactory>().AsCached();
            Container.BindInterfacesAndSelfTo<TanksFactory>().AsCached();
        }
        
        private void BindLoaders()
        {
            Container.Bind<IMainUILoader>().To<MainUILoader>().AsTransient();
            Container.Bind<IPlayerLoader>().To<PlayerLoader>().AsTransient();
            Container.Bind<IBotLoader>().To<BotLoader>().AsTransient();
            Container.Bind<IBulletsLoader>().To<BulletsLoader>().AsTransient();
            Container.Bind<IVFXLoader>().To<VFXLoader>().AsTransient();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<IGameObjectHelper>().To<GameObjectHelper>().AsSingle();
            Container.BindInterfacesAndSelfTo<TickableRunner>().AsSingle();

            Container.BindInterfacesAndSelfTo<TankMovement>().AsTransient();
            Container.Bind<IPlayerMovement>().To<WASDMovement>().AsTransient();
            Container.Bind<IShootingService>().To<ShootingService>().AsTransient();
            Container.Bind<IPlayerShootingService>().To<PlayerShootingService>().AsSingle();
            Container.Bind<IHealthService>().To<HealthService>().AsTransient();
            Container.Bind<IBotSpawnService>().To<BotSpawnService>().AsSingle();
            Container.Bind<IBotMovement>().To<BotMovement>().AsTransient();
        }
        
        private void BindPresenters()
        {
            Container.Bind<IPlayerPresenter>().To<PlayerPresenter>().AsSingle().Lazy();
            Container.Bind<IBotPresenter>().To<BotPresenter>().AsTransient();
        }
        
        private void BindManagers()
        {
            Container.Bind<IBulletsPoolManager>().To<BulletsPoolManager>().AsCached();
            Container.Bind<IVFXPoolManager>().To<VFXPoolManager>().AsCached();
        }
    }
}