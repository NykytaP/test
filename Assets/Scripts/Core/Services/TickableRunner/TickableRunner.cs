using System.Collections.Generic;
using Zenject;

namespace Core.Services.TickableRunner
{
    public class TickableRunner : IFixedTickable, ITickable, ITickableRunner
    {
        private HashSet<IFixedTickable> _fixedTickables = new();
        private HashSet<ITickable> _tickables = new();

        public void FixedTick()
        {
            foreach (IFixedTickable fixedTickable in _fixedTickables)
            {
                fixedTickable.FixedTick();
            }
        }
        
        public void Tick()
        {
            foreach (ITickable tickable in _tickables)
            {
                tickable.Tick();
            }
        }

        public void RegisterTickable(IFixedTickable fixedTickable)
        {
            _fixedTickables.Add(fixedTickable);
        }

        public void UnRegisterTickable(IFixedTickable fixedTickable)
        {
            _fixedTickables.Remove(fixedTickable);
        }
        
        public void RegisterTickable(ITickable fixedTickable)
        {
            _tickables.Add(fixedTickable);
        }

        public void UnRegisterTickable(ITickable fixedTickable)
        {
            _tickables.Remove(fixedTickable);
        }
    }
}