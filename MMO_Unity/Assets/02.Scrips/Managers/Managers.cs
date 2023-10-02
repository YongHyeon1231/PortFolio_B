using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Managers
{
    public class Managers : MonoBehaviour
    {
        private static Managers s_instnace; // 유일성이 보장된다.
        private static Managers Instance { get { Init(); return s_instnace; } } // 유일한 매니저를 갖고있다.

        private InputManager _input = new InputManager();
        private ResourceManager _resource = new ResourceManager();
        private SceneManagerEx _scene = new SceneManagerEx();
        private SoundManager _sound = new SoundManager();
        private UIManagers _ui = new UIManagers();

        public static InputManager Input { get { return Instance._input; } }
        public static ResourceManager Resource { get {  return Instance._resource; } }
        public static SceneManagerEx Scene { get { return Instance._scene; } }
        public static SoundManager Sound { get { return Instance._sound; } }
        public static UIManagers UI { get { return Instance._ui; } }


        void Start()
        {
            Init();
        }

        void Update()
        {
            _input.OnUpdate();
        }

        static void Init()
        {
            if (s_instnace == null)
            {
                GameObject go = GameObject.Find("@Managers");
                if (go == null)
                {
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<Managers>();
                }

                DontDestroyOnLoad(go);
                s_instnace = go.GetComponent<Managers>();

                s_instnace._sound.Init();
            }
        }

        public static void Clear()
        {
            Input.Clear();
            Sound.Clear();
            Scene.Clear();
            UI.Clear();
        }
    }
}