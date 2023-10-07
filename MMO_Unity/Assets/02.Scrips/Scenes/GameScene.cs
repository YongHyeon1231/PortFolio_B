using RPG.UI;
using RPG.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Scenes
{
    public class GameScene : BaseScene
    {
        protected override void Init()
        {
            base.Init();

            SceneType = Define.Scene.Game;
            Managers.Managers.UI.ShowSceneUI<UI_Inven>();

            for (int i = 0; i < 5; i++)
            {
                Managers.Managers.Resource.Instantiate("UnityChan");
            }
        }

        public override void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}