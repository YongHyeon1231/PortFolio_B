using RPG.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.Managers
{
    public class InputManager
    {
        public Action KeyAction = null;
        public Action<Define.MouseEvent> MouseAction = null;

        private bool _pressed = false;
        private float _pressedTime = 0.0f;

        public void OnUpdate()
        {
            //UI가 클릭된 상황이면 바로 return;
            if (EventSystem.current == null || EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.anyKey && KeyAction != null)
            {
                KeyAction.Invoke();
            }

            if (MouseAction != null)
            {
                if (Input.GetMouseButton(0))
                {
                    if (!_pressed)
                    {
                        MouseAction.Invoke(Define.MouseEvent.PointerDown);
                        _pressedTime = Time.time;
                    }
                    MouseAction.Invoke(Define.MouseEvent.Press);
                    _pressed = true;
                }
                else
                {
                    if ( _pressed )
                    {
                        if (Time.time < _pressedTime + 0.2f)
                        {
                            MouseAction.Invoke(Define.MouseEvent.Click);
                        }
                        MouseAction.Invoke(Define.MouseEvent.PointerUp);
                    }
                    _pressed = false;
                    _pressedTime = 0.0f;
                }
            }
        }

        public void Clear()
        {
            KeyAction = null;
            MouseAction = null;
        }
    }
}