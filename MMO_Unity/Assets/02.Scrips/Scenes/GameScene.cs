using RPG.UI;
using RPG.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Scenes
{
    public class GameScene : BaseScene
    {
        Coroutine co;

        protected override void Init()
        {
            base.Init();

            SceneType = Define.Scene.Game;
            Managers.Managers.UI.ShowSceneUI<UI_Inven>();

            co = StartCoroutine("ExplodeAfterSeconds", 4.0f);
            StartCoroutine("CoStopExplode", 2.0f);
        }

        private IEnumerator CoStopExplode(float seconds)
        {
            Debug.Log("Stop Enter");
            yield return new WaitForSeconds(seconds);
            Debug.Log("Stop Execute!!!");
            if ( co != null)
            {
                StopCoroutine(co);
                co = null;
            }
        }

        private IEnumerator ExplodeAfterSeconds(float seconds)
        {
            Debug.Log("Explode Enter");
            yield return new WaitForSeconds(seconds);
            Debug.Log("Explode Execute!!!");
            co = null;
        }

        public override void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}