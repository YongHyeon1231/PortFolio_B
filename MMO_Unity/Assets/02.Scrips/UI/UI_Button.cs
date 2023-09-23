using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using RPG.Utils;

namespace RPG.UI
{
    public class UI_Button :  UI_Base
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

        private void Start()
        {
            Bind<Button>(typeof(Buttons));
            Bind<TMP_Text>(typeof(Texts));
            Bind<GameObject>(typeof(GameObjects));

            GetText((int)Texts.ScoreText).text = "Bind Test";
            //Get<TMP_Text>((int)Texts.ScoreText).text = "Bind Test";
        }

        

        private int _score = 0;

        public void OnButtonClikced()
        {
            _score++;
        }
    }
}