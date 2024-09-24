namespace Core.Utils
{
    public static class Constants
    {
        public static class GameConstants
        {
            public static string SpawnZoneTag = "SpawnZone";
            public static string SpawnZoneLayerName = "SpawnZone";
            public static string GroundLayerName = "Ground";
            
            public static float TankTurnSpeed = 30;
            public static float TankMoveSpeed = 4;
            
            public static float ShootDelay = 1.5f;
            public static float BulletSpeed = 65;
            public static float ShootRecoilForce = 3;
            public static float BulletLifetime = 5;
        }

        public static class Infrastructure
        {
            public static string PoolRootName = "Pools";
        }
    }
}