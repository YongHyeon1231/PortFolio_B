using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Managers
{
    public class Managers : MonoBehaviour
    {
        private static Managers s_instnace; // ���ϼ��� ����ȴ�.
        private static Managers Instance { get { Init(); return s_instnace; } } // ������ �Ŵ����� �����ִ�.

        private InputManager _input = new InputManager();
        private ResourceManager _resource = new ResourceManager();
        public static InputManager Input { get { return Instance._input; } }
        public static ResourceManager Resource { get {  return Instance._resource; } }


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
            }
        }
    }
}