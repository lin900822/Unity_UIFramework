using UIFramework;

namespace Game
{
    public class SettingPanel : UIPanel
    {
        public override void OnFocus()
        {
            UISystem.Instance.HidePanel("InfoPanel");
        }
        
        public void OnBtnClick()
        {
            UISystem.Instance.HidePeekPanel();
        }
        
        public void OnFriendBtnClick()
        {
            UISystem.Instance.HidePeekPanel();
            UISystem.Instance.ShowPanel("FriendPanel");
        }
    }
}