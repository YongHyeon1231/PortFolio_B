using RPG.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Scenes
{
    public class LoginScene : BaseScene
    {
        protected override void Init()
        {
            base.Init();

            SceneType = Define.Scene.Login;

            List<GameObject> list = new List<GameObject>();
            for (int i = 0; i < 2; i++)
            {
                list.Add(Managers.Managers.Resource.Instantiate("UnityChan"));
            }

            foreach (GameObject obj in list)
            {
                Managers.Managers.Resource.Destroy(obj);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Managers.Managers.Scene.LoadScene(Define.Scene.Game);
            }
        }

        public override void Clear()
        {
            Debug.Log("LoginScene Clear!");
        }
    }
}