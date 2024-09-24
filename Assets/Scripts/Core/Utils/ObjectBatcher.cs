using UnityEngine;

namespace Core.Utils
{
    [RequireComponent(typeof(MeshRenderer))]
    public class ObjectBatcher : MonoBehaviour
    {
        private void Awake()
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            
            meshRenderer.SetPropertyBlock(materialPropertyBlock);
        }
    }
}