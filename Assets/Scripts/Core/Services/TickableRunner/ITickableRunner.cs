using Zenject;

namespace Core.Services.TickableRunner
{
    public interface ITickableRunner
    {
        public void RegisterTickable(IFixedTickable fixedTickable);
        public void UnRegisterTickable(IFixedTickable fixedTickable);
        public void RegisterTickable(ITickable fixedTickable);
        public void UnRegisterTickable(ITickable fixedTickable);
    }
}