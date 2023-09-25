using RPG.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// UI_Base에는 UI 게임오브젝트를 찾고 받아오고 AddUIEvent를 하는 기능을 가지고 있습니다.
namespace RPG.UI
{
    public abstract class UI_Base : MonoBehaviour
    {
        Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

        public abstract void Init();

        protected void Bind<T>(Type type) where T : UnityEngine.Object
        {
            //이름 변환
            string[] names = Enum.GetNames(type);

            //저장할 공간을 만들어 줌
            UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
            _objects.Add(typeof(T), objects);

            //하나하나 찾아가지고 매핑을 해줌
            for (int i = 0; i < names.Length; i++)
            {
                if (typeof(T) == typeof(GameObject))
                {
                    objects[i] = Util.FindChild(gameObject, names[i], true);
                }
                else
                {
                    objects[i] = Util.FindChild<T>(gameObject, names[i], true);
                }

                if (objects[i] == null)
                    Debug.Log($"Failed to bind!({names[i]})");
            }
        }

        protected T Get<T>(int idx) where T : UnityEngine.Object
        {
            //딕셔너리 안에 있는걸 꺼내기 위해 공간확보
            UnityEngine.Object[] objects = null;
            //딕셔너리 안에 있는것 꺼내오기
            if (_objects.TryGetValue(typeof(T), out objects) == false)
                return null;

            return objects[idx] as T;
        }

        protected TMP_Text GetText(int idx) { return Get<TMP_Text>(idx); }
        protected Button GetButton(int idx) { return Get<Button>(idx); }
        protected Image GetImage(int idx) { return Get<Image>(idx); }

        public static void AddUIEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
        {
            UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

            switch (type)
            {
                case Define.UIEvent.Click:
                    {
                        evt.OnClickHandler -= action;
                        evt.OnClickHandler += action;
                    }
                    break;
                case Define.UIEvent.Drag:
                    {
                        evt.OnDragHandler -= action;
                        evt.OnDragHandler += action;
                    }
                    break;
                case Define.UIEvent.DragBegin:
                    break;
                case Define.UIEvent.DragEnd:
                    break;
                default:
                    break;
            }
        }
    }
}