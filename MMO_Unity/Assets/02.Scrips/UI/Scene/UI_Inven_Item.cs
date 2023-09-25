using RPG.UI;
using RPG.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.UI
{
    public class UI_Inven_Item : UI_Base
    {
        private enum GameObjects
        {
            ItemIcon,
            ItemNameText_TMP,
            ItemCountText_TMP,
        }

        string _name;

        void Start()
        {
            Init();
        }

        public override void Init()
        {
            Bind<GameObject>(typeof(GameObjects));
            Get<GameObject>((int)GameObjects.ItemNameText_TMP).GetComponent<TMP_Text>().text = _name;

            Get<GameObject>((int)GameObjects.ItemIcon).AddUIEvent((PointerEventData) => { Debug.Log($"아이템 클릭! {_name}"); });
        }

        public void SetInfo(string name)
        {
            _name = name;
        }
    }
}