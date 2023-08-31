using UIFramework;

namespace Game
{
    public class ActivityDetailPanel : UIPanel
    {
        public override void OnFocus()
        {
            UISystem.Instance.ShowPanel("InfoPanel");
        }
        
        public void OnBtnClick()
        {
            UISystem.Instance.HidePeekPanel();
        }
        
        public void OnActivityDetailBtnClick()
        {
            UISystem.Instance.ShowPanel("SettingPanel");
        }
    }
}