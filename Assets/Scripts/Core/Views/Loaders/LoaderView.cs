using UnityEngine;
namespace Core.Views.Loaders
{
    public class LoaderView : MonoBehaviour, ILoaderView
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