using UnityEngine;

namespace Core.Utils
{
    public static class LevelUtils
    {
        public static Vector3 GetRandomPointInBoxCollider(BoxCollider spawnArea, BoxCollider objectCollider)
        {
            int attempts = 100;
    
            Vector3 objectSize = objectCollider.size + Constants.GameConstants.SpawnOffset;
            
            int layerMask = ~LayerMask.GetMask(Constants.GameConstants.SpawnZoneLayerName, Constants.GameConstants.GroundLayerName);

            while (attempts > 0)
            {
                Vector3 spawnPoint = GetRandomPointInBoxCollider(spawnArea);

                if (!Physics.CheckBox(spawnPoint, objectSize / 2, Quaternion.identity, layerMask, QueryTriggerInteraction.Ignore))
                {
                    return spawnPoint;
                }

                attempts--;
            }

            return Vector3.zero;
        }

        private static Vector3 GetRandomPointInBoxCollider(BoxCollider collider)
        {
            Vector3 center = collider.center;
            Vector3 size = collider.size;
    
            Vector3 randomPoint = new Vector3(
                Random.Range(-size.x / 2, size.x / 2), 
                Random.Range(-size.y / 2, size.y / 2), 
                Random.Range(-size.z / 2, size.z / 2)
            );

            return collider.transform.TransformPoint(center + randomPoint);
        }
    }
}