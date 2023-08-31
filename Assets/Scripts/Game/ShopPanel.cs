using UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ShopPanel : UIPanel
    {
        [SerializeField] private Button _backBtn;

        protected override void OnInit()
        {
            _backBtn.onClick.AddListener(OnBackBtnClicked);
        }
        
        public override void OnFocus()
        {
            UISystem.Instance.ShowPanel(UIPanelDefine.Info.ToString());
        }
        
        private void OnBackBtnClicked()
        {
            UISystem.Instance.HidePeekPanel();
        }
    }
}