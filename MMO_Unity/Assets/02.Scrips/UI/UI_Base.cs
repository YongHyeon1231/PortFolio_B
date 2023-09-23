using RPG.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class UI_Base : MonoBehaviour
    {
        Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

        protected void Bind<T>(Type type) where T : UnityEngine.Object
        {
            //�̸� ��ȯ
            string[] names = Enum.GetNames(type);

            //������ ������ ����� ��
            UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
            _objects.Add(typeof(T), objects);

            //�ϳ��ϳ� ã�ư����� ������ ����
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
            //��ųʸ� �ȿ� �ִ°� ������ ���� ����Ȯ��
            UnityEngine.Object[] objects = null;
            //��ųʸ� �ȿ� �ִ°� ��������
            if (_objects.TryGetValue(typeof(T), out objects) == false)
                return null;

            return objects[idx] as T;
        }

        protected TMP_Text GetText(int idx) { return Get<TMP_Text>(idx); }

        protected Button GetButton(int idx) { return Get<Button>(idx); }

        protected Image GetImage(int idx) { return Get<Image>(idx); }
    }
}