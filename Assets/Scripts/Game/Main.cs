using UIFramework;
using UnityEngine;

namespace Game
{
    public class Main : MonoBehaviour
    {
        private void Start()
        {
            UISystem.Instance.ShowPanel(UIPanelDefine.Lobby.ToString());
            UISystem.Instance.ShowPanel(UIPanelDefine.Info.ToString());
        }
    }
}