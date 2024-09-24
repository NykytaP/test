using Core.Views.Tank;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Factories
{
    public interface ITanksFactory
    {
        public UniTask<ITankViewContainer> SpawnPlayer(Transform root);
        public UniTask<ITankViewContainer> SpawnBot(Transform root);
        BoxCollider SpawnZone { get; }
    }
}