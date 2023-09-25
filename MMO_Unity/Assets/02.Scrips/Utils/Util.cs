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

        // 최상위 부모를 받아오고,
        // 이름을 받아줄건데 이름을 입력하지 않으면 비교하지 않고 타입만 비교해서 맞으면 리턴해줍니다,
        // 불리언을 받아서 재귀적으로 받을건지 확인한다.(자식만 찾을것인지 자식의 자식도 찾을 것인지)
        // T에는 찾고싶은 Component를 넣어줄 것 입니다.
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
                        //Component를 찾고
                        T component =  transform.GetComponent<T>();
                        //찾은 Component가 null값이 아니라면 그 component가 있다고 리턴해 줍니다.
                        if (component != null)
                            return component;
                    }
                }
            }
            else
            {
                // 모든 자식 순회
                foreach (T component in go.GetComponentsInChildren<T>())
                {
                    // 이름이 비어있거나 진짜 내가 원하던 이름이면 리턴
                    if (string.IsNullOrEmpty(name) || component.name == name)
                        return component;
                }
            }

            return null;
        }
    }
}