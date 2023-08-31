using System;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ConfirmPanelData : UIData
    {
        public string Title;
        public string Message;
        public Action ConfirmCallback;
    }

    public class ConfirmPanel : UIPanel
    {
        [SerializeField] private Button _cancelBtn;
        [SerializeField] private Button _okBtn;
        [SerializeField] private Text   _titleTxt;
        [SerializeField] private Text   _messageTxt;

        private Action _confirmCallback;
        
        protected override void OnInit()
        {
            _cancelBtn.onClick.AddListener(OnCancelBtnClicked);
            _okBtn.onClick.AddListener(OnOkBtnClicked);
        }

        protected override void OnUIDataSet(UIData data)
        {
            if (data is ConfirmPanelData uiData)
            {
                _titleTxt.text   = uiData.Title;
                _messageTxt.text = uiData.Message;
                _confirmCallback = uiData.ConfirmCallback;
            }
        }

        private void OnOkBtnClicked()
        {
            UISystem.Instance.HidePeekPanel();
            _confirmCallback?.Invoke();
        }

        private void OnCancelBtnClicked()
        {
            UISystem.Instance.HidePeekPanel();
        }
    }
}