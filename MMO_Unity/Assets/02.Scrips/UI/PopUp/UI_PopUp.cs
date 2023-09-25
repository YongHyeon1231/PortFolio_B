using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.PopUp
{
    public class UI_PopUp : UI_Base
    {
        public override void Init()
        {
            Managers.Managers.UI.SetCanvas(gameObject, true);
        }

        public virtual void ClosePopUpUI()
        {
            Managers.Managers.UI.ClosePopUpUI();
        }
    }
}