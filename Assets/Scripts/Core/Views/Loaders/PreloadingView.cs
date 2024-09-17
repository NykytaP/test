using UnityEngine;
namespace Core.Views.Loaders
{
    public class PreloadingView : MonoBehaviour, IPreloadingView
    {
        [SerializeField]
        private GameObject _loadingObject;

        private void Awake()
        {
            HideLoading();
        }

        public void ShowLoading()
        {
            SetLoadingState(true);
        }

        public void HideLoading()
        {
            SetLoadingState(false);
        }

        private void SetLoadingState(bool state)
        {
            _loadingObject.SetActive(state);
        }
    }
}