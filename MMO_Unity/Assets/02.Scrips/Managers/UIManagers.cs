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
            //sorting�� ��û�ߴٸ�
            if (sort)
            {
                canvas.sortingOrder = _order;
                _order++;
            }
            else //sorting ��û�� ���ߴٸ� �ᱹ ���� �ִ� �˾��̶� ������ ���� �Ϲ� UI�̴�.
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

        // name�� �������� �̸�, T�� ��ũ��Ʈ�� ������ �ֽ��ϴ�.
        // ��κ��� ��쿡�� �츮�� name�̶� TŸ���� �̸��� ���� �� �ű� ������
        // �ɼ����� name�� �߰��� �־� �� �� �ְ� �ϰ� ���࿡ name�� �Է����� ������
        // T�� �̸��� �״�� ����ϰ� �� ���Դϴ�.
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
            popUp = null; //���� ����

            _order--;
        }

        public void CloseAllPopUpUI()
        {
            while (_popUpStack.Count > 0)
                ClosePopUpUI();
        }
    }
}