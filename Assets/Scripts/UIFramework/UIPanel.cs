using UnityEngine;

namespace UIFramework
{
    public enum UIPanelType
    {
        Normal,
        Stack
    }
    
    public class UIPanel : UIView
    {
        [Space(10)]
        [Header("基本配置")]
        public UIPanelType PanelType = UIPanelType.Stack;
        public bool IsNeedHideWhenNotFocus = false;
        
        public virtual void OnFocus()
        {
            
        }
    }
}