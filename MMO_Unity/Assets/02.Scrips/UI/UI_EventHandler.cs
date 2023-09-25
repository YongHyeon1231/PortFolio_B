using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.UI
{
    public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler
    {
        public Action<PointerEventData> OnClickHandler = null;
        public Action<PointerEventData> OnBeginDragHandler = null;
        public Action<PointerEventData> OnDragHandler = null;

        //클릭을 하면 여기서 바로 Invoke 실행이 된다.
        public void OnPointerClick(PointerEventData eventData)
        {
            if (OnClickHandler != null)
                OnClickHandler.Invoke(eventData);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (OnBeginDragHandler != null)
                OnBeginDragHandler.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (OnDragHandler != null)
                OnDragHandler.Invoke(eventData);
        }

        
    }
}