using UIFramework;

namespace Game
{
    public class AnnouncementPanel : UIPanel
    {
        public void OnBtnClick()
        {
            UISystem.Instance.HidePeekPanel();
        }
        
        public void OnActivityDetailBtnClick()
        {
            UISystem.Instance.HidePeekPanel();
            UISystem.Instance.ShowPanel("ActivityDetailPanel");
        }
    }
}