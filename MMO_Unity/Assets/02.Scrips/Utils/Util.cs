using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace RPG.Utils
{
    public class Util
    {
        public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
        {
            T component = go.GetComponent<T>();
            if (component == null)
                component = go.AddComponent<T>(); 

            return component;
        }

        public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
        {
            Transform transform = FindChild<Transform>(go, name, recursive);
            if (transform == null)
                return null;

            return transform.gameObject;
        }

        // �ֻ��� �θ� �޾ƿ���,
        // �̸��� �޾��ٰǵ� �̸��� �Է����� ������ ������ �ʰ� Ÿ�Ը� ���ؼ� ������ �������ݴϴ�,
        // �Ҹ����� �޾Ƽ� ��������� �������� Ȯ���Ѵ�.(�ڽĸ� ã�������� �ڽ��� �ڽĵ� ã�� ������)
        // T���� ã����� Component�� �־��� �� �Դϴ�.
        public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
        {
            if (go == null)
                return null;

            if (recursive == false)
            {
                for (int i = 0; i < go.transform.childCount; i++)
                {
                    Transform transform = go.transform.GetChild(i);
                    if (string.IsNullOrEmpty(name) || transform.name == name)
                    {
                        //Component�� ã��
                        T component =  transform.GetComponent<T>();
                        //ã�� Component�� null���� �ƴ϶�� �� component�� �ִٰ� ������ �ݴϴ�.
                        if (component != null)
                            return component;
                    }
                }
            }
            else
            {
                // ��� �ڽ� ��ȸ
                foreach (T component in go.GetComponentsInChildren<T>())
                {
                    // �̸��� ����ְų� ��¥ ���� ���ϴ� �̸��̸� ����
                    if (string.IsNullOrEmpty(name) || component.name == name)
                        return component;
                }
            }

            return null;
        }
    }
}