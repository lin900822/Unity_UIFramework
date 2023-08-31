using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace UIFramework
{
    public class UISystem : MonoBehaviour
    {
        public static UISystem Instance { get; private set; }

        [SerializeField] private GameObject _root;
        [SerializeField] private Transform  _blockPanel;
        [SerializeField] private Transform  _maskPanel;

        [SerializeField] private UISettings _uiSettings;

        private string _focusPanelId;

        private Dictionary<string, UIPanel> _panelsLoaded = new Dictionary<string, UIPanel>();

        private Stack<string> _panelStack = new Stack<string>();

        #region - Init -

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            InitDOTween();
        }

        private void InitDOTween()
        {
            // 讓DOTween空轉一次，避免第一次使用DoTween時可能出現的閃爍
            int i = 0;
            DOTween.To(() => i, x => i = x, 1, 1);
        }

        #endregion

        #region - Show & Load -

        public void ShowPanel(string panelId, UIData data = null)
        {
            if (panelId == _focusPanelId) return;

            if (_panelsLoaded.TryGetValue(panelId, out var panelLoaded))
            {
                ShowPanel(panelId, panelLoaded, data);
                return;
            }

            var panelSpawned = LoadPanel(panelId);
            ShowPanel(panelId, panelSpawned, data);
        }

        private void ShowPanel(string panelId, UIPanel panel, UIData data = null)
        {
            if (panel.PanelType == UIPanelType.Stack)
            {
                PushToStack(panelId);
                
                _blockPanel.SetAsLastSibling();
                _maskPanel.SetAsLastSibling();
            }
            
            panel.transform.SetAsLastSibling();

            if (!panel.gameObject.activeSelf)
            {
                panel.StartShow();

                if (data != null)
                {
                    panel.SetData(data);
                }
            }

            if (panel.PanelType == UIPanelType.Stack)
            {
                SetPanelFocus(panelId);
            }
        }

        private UIPanel LoadPanel(string panelId)
        {
            UIPanel prefab = GetPanelPrefab(panelId);

            if (prefab == null)
            {
                Debug.Log("Con not Find Panel Prefab");
                return null;
            }

            var panelSpawned = Instantiate(prefab, _root.transform);
            _panelsLoaded[panelId] = panelSpawned;
            panelSpawned.gameObject.SetActive(false);
            panelSpawned.Init();
            return panelSpawned;
        }

        // 之後有資源管理模塊再串接
        private UIPanel GetPanelPrefab(string panelId)
        {
            UIPanel prefab = null;

            foreach (var panelSetting in _uiSettings.PanelSettings)
            {
                if (panelSetting.PanelId == panelId)
                {
                    prefab = panelSetting.Panel;
                    break;
                }
            }

            return prefab;
        }

        #endregion

        #region - Hide -

        public void HidePeekPanel()
        {
            if (!IsStackLeftOnePanel())
            {
                var panelId = _panelStack.Pop();
                HidePanel(panelId);
            }

            if (!IsStackEmpty())
            {
                var currentPeekPanelId = _panelStack.Peek();
                if (_panelsLoaded.TryGetValue(currentPeekPanelId, out var currentPeekPanel))
                {
                    if (currentPeekPanel.IsNeedHideWhenNotFocus)
                    {
                        currentPeekPanel.StartShow();
                    }

                    SetPanelFocus(currentPeekPanelId);
                }
            }
            else
            {
                SetPanelFocus(null);
            }
        }

        public async void HidePanel(string panelId)
        {
            if (_panelsLoaded.TryGetValue(panelId, out var panelLoaded))
            {
                if (!IsStackLeftOnePanel())
                {
                    var peekPanelId    = _panelStack.Peek();
                    var peekPanelIndex = _panelsLoaded[peekPanelId].transform.GetSiblingIndex();
                    _maskPanel.SetSiblingIndex(peekPanelIndex - 1);
                }
                else
                {
                    _maskPanel.SetAsFirstSibling();
                }
                
                await panelLoaded.StartHide();

                if (!IsStackLeftOnePanel())
                {
                    var peekPanelId    = _panelStack.Peek();
                    var peekPanelIndex = _panelsLoaded[peekPanelId].transform.GetSiblingIndex();
                    _blockPanel.SetSiblingIndex(peekPanelIndex - 1);
                }
                else
                {
                    _blockPanel.SetAsFirstSibling();
                }
            }
        }

        #endregion

        #region - Stack -

        private void PushToStack(string panelId)
        {
            if (!IsStackLeftOnePanel())
            {
                var currentPeekPanelId = _panelStack.Peek();
                if (_panelsLoaded.TryGetValue(currentPeekPanelId, out var currentPeekPanel))
                {
                    if (currentPeekPanel.IsNeedHideWhenNotFocus)
                    {
                        currentPeekPanel.StartHide();
                    }
                }
            }

            if (_panelStack.Contains(panelId))
            {
                PopPanelsAbove(panelId);
                return;
            }

            _panelStack.Push(panelId);
        }

        private void PopPanelsAbove(string panelId)
        {
            int index = 0;
            int i     = 0;
            foreach (var item in _panelStack)
            {
                if (item == panelId) index = i;
                i++;
            }

            int popCount = (_panelStack.Count - 1) - index;

            for (int j = 0; j < popCount; j++)
            {
                HidePeekPanel();
            }
        }

        private bool IsStackLeftOnePanel() => _panelStack.Count <= 1;

        private bool IsStackEmpty() => _panelStack.Count <= 0;

        #endregion

        private void SetPanelFocus(string panelId)
        {
            if (panelId == null)
            {
                _focusPanelId = null;
                return;
            }
            
            if (_panelsLoaded.TryGetValue(panelId, out var panel))
            {
                _focusPanelId = panelId;
                panel.OnFocus();
            }
        }
    }
}