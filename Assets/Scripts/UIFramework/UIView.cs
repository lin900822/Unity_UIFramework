using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace UIFramework
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIView : MonoBehaviour
    {
        [Space(5)]
        [Header("動畫檔")]
        [SerializeField] private ViewAnimationClip _showAnimation;
        [SerializeField] private ViewAnimationClip _hideAnimation;

        [Space(5)]
        [Header("需要連帶播放動畫的子UI")]
        [SerializeField] private List<UIView> _subViews;

        private ViewAnimationPlayer _viewAnimationPlayer;
        private UIData              _uiData;

        #region - Init -
        
        public void Init()
        {
            _viewAnimationPlayer = new ViewAnimationPlayer(gameObject);
            foreach (var subView in _subViews)
            {
                subView.Init();
            }
            OnInit();
        }

        protected virtual void OnInit()
        {
            
        }
        
        #endregion
        
        #region - Show & Hide -
        
        public async Task StartShow()
        {
            OnStartShow();
            gameObject.SetActive(true);
            foreach (var subView in _subViews)
            {
                subView.StartShow();
            }
            await _viewAnimationPlayer.PlayClip(_showAnimation);
            OnShowFinish();
        }

        public async Task StartHide()
        {
            OnStartHide();
            foreach (var subView in _subViews)
            {
                subView.StartHide();
            }
            await _viewAnimationPlayer.PlayClip(_hideAnimation);
            OnHideFinish();
            gameObject.SetActive(false);
        }

        protected virtual void OnStartShow()
        {
            
        }

        protected virtual void OnStartHide()
        {
            
        }
        
        protected virtual void OnShowFinish()
        {
            
        }

        protected virtual void OnHideFinish()
        {
            
        }
        
        #endregion
        
        #region - UIData -
        
        public void SetData(UIData uiData)
        {
            _uiData = uiData;
            OnUIDataSet(_uiData);
        }

        protected virtual void OnUIDataSet(UIData uiData)
        {
            
        }
        
        #endregion
    }
}