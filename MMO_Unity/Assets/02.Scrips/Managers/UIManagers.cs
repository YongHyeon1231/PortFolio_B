using RPG.UI.PopUp;
using RPG.UI.Scene;
using RPG.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace RPG.Managers
{
    public class UIManagers
    {
        private int _order = 10;
        
        private Stack<UI_PopUp> _popUpStack = new Stack<UI_PopUp>();
        private UI_Scene _sceneUI = null;

        public GameObject Root
        {
            get
            {
                GameObject root = GameObject.Find("@UI_Root");
                if (root == null)
                    root = new GameObject { name = "@UI_Root" };

                return root;
            }
        }

        public void SetCanvas(GameObject go, bool sort = true)
        {
            Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;
            //sorting을 요청했다면
            if (sort)
            {
                canvas.sortingOrder = _order;
                _order++;
            }
            else //sorting 요청을 안했다면 결국 여기 있는 팝업이랑 연관이 없는 일반 UI이다.
            {
                canvas.sortingOrder = 0;
            }
        }

        public T ScenePopUpUI<T>(string name = null) where T : UI_Scene
        {
            if (string.IsNullOrEmpty(name))
                name = typeof(T).Name;

            GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
            T sceneUI = Util.GetOrAddComponent<T>(go);
            _sceneUI = sceneUI;

            go.transform.SetParent(Root.transform);

            return sceneUI;
        }

        // name은 프리팹의 이름, T는 스크립트랑 연관이 있습니다.
        // 대부분의 경우에는 우리가 name이랑 T타입의 이름을 맞춰 줄 거기 때문에
        // 옵션으로 name을 추가로 넣어 줄 수 있게 하고 만약에 name을 입력하지 않으면
        // T에 이름을 그대로 사용하게 될 것입니다.
        public T ShowPopUpUI<T>(string name = null) where T : UI_PopUp
        {
            if (string.IsNullOrEmpty(name))
                name = typeof(T).Name;

            GameObject go = Managers.Resource.Instantiate($"UI/PopUp/{name}");
            T popUp = Util.GetOrAddComponent<T>(go);
            _popUpStack.Push(popUp);

            go.transform.SetParent(Root.transform);

            return popUp;
        }

        public void ClosePopUpUI(UI_PopUp popUp)
        {
            if (_popUpStack.Count == 0)
                return;

            if (_popUpStack.Peek() != popUp)
            {
                Debug.Log("Close PopUp Failed!");
                return;
            }

            ClosePopUpUI();
        }

        public void ClosePopUpUI()
        {
            if (_popUpStack.Count == 0)
                return;

            UI_PopUp popUp = _popUpStack.Pop();
            Managers.Resource.Destroy(popUp.gameObject);
            popUp = null; //접근 금지

            _order--;
        }

        public void CloseAllPopUpUI()
        {
            while (_popUpStack.Count > 0)
                ClosePopUpUI();
        }
    }
}