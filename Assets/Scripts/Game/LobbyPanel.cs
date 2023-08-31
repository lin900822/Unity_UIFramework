using UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LobbyPanel : UIPanel
    {
        [SerializeField] private Button _shopBtn;
        [SerializeField] private Button _friendBtn;

        protected override void OnInit()
        {
            _shopBtn.onClick.AddListener(OnShopBtnClicked);
            _friendBtn.onClick.AddListener(OnFriendBtnClicked);
        }
        
        public override void OnFocus()
        {
            UISystem.Instance.ShowPanel(UIPanelDefine.Info.ToString());
        }

        private void OnShopBtnClicked()
        {
            var data = new ConfirmPanelData
            {
                Title           = "系統提示",
                Message         = "水晶不足，是否購買",
                ConfirmCallback = () => { UISystem.Instance.ShowPanel(UIPanelDefine.Shop.ToString()); }
            };

            UISystem.Instance.ShowPanel(UIPanelDefine.Confirm.ToString(), data);
        }
        
        private void OnFriendBtnClicked()
        {
            var data = new ConfirmPanelData
            {
                Title           = "系統提示",
                Message         = "你沒有朋友",
                ConfirmCallback = () => { Debug.Log("功能尚未實作"); }
            };

            UISystem.Instance.ShowPanel(UIPanelDefine.Confirm.ToString(), data);
        }
    }
}