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