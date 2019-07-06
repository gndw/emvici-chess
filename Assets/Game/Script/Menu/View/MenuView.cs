using System;
using Agate.Chess.Menu.Model;
using Agate.MVC.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Agate.Chess.Menu.View
{
    public class MenuView : BaseUIView<IMenuModel>
    {
        [SerializeField]
        private Text _txtPoint = null;
        [SerializeField]
        private Button _btnPlayerVsPlayer = null;

        public void Init(Action onButtonPlayerVsPlayerClicked)
        {
            _btnPlayerVsPlayer.onClick.RemoveAllListeners();
            _btnPlayerVsPlayer.onClick.AddListener(() => onButtonPlayerVsPlayerClicked());
        }

        protected override void UpdateView()
        {
            _txtPoint.text = _model.Point.ToString();
        }
    }
}