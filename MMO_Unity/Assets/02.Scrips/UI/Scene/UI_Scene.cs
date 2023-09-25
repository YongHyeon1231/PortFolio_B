using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.Scene
{
    public class UI_Scene : UI_Base
    {
        public virtual void Init()
        {
            Managers.Managers.UI.SetCanvas(gameObject, false);
        }
    }
}