using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIFramework
{
    [Serializable]
    public class UIPanelSetting
    {
        public string  PanelId;
        public UIPanel Panel;
    }
    
    [CreateAssetMenu(fileName = "UISettings", menuName = "UIFramework/UISettings")]
    public class UISettings : ScriptableObject
    {
        public List<UIPanelSetting> PanelSettings;
    }
}