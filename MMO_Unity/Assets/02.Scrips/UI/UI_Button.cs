using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using RPG.Utils;
using UnityEngine.EventSystems;
using RPG.UI.PopUp;

namespace RPG.UI
{
    public class UI_Button :  UI_PopUp
    {
        enum Buttons
        {
            PointButton,
        }

        enum Texts
        {
            PointText,
            ScoreText,
        }

        enum GameObjects
        {
            TestObject,
        }

        enum Images
        {
            ItemIcon,
        }

        private void Start()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();

            Bind<Button>(typeof(Buttons));
            Bind<TMP_Text>(typeof(Texts));
            Bind<GameObject>(typeof(GameObjects));
            Bind<Image>(typeof(Images));

            //Get<TMP_Text>((int)Texts.ScoreText).text = "Bind Test";

            GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonClikced);

            GameObject go = GetImage((int)Images.ItemIcon).gameObject;
            AddUIEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);
        }

        private int _score = 0;

        public void OnButtonClikced(PointerEventData data)
        {
            _score++;

            GetText((int)Texts.ScoreText).text = $"Á¡¼ö : {_score}";
        }
    }
}