using UnityEngine;

namespace Core.Utils
{
    public static class Constants
    {
        public static class GameConstants
        {
            public static readonly string SpawnZoneTag = "SpawnZone";
            public static readonly string SpawnZoneLayerName = "SpawnZone";
            public static readonly string GroundLayerName = "Ground";
            
            public static readonly float TankTurnSpeed = 30;
            public static readonly float TankMoveSpeed = 4;
            
            public static readonly float ShootDelay = 1.5f;
            public static readonly float BulletSpeed = 65;
            public static readonly float ShootRecoilForce = 3;
            public static readonly float BulletLifetime = 5;
            
            public static readonly int DefaultBulletDamage = 1;
            public static readonly int MaxEntityHealth = 1;
            
            public static readonly float DeathAwait = 3;
            public static readonly Vector3 SpawnOffset = new(0.5f, 0, 0.5f);
        }

        public static class Infrastructure
        {
            public static string PoolRootName = "Pools";
        }
    }
}