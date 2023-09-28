using RPG.UI.Scene;
using RPG.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class UI_Inven : UI_Scene
    {
        enum GameObjects
        {
            GridPanel,
        }

        private void Start()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();

            Bind<GameObject>(typeof(GameObjects));

            // 자식을 찾아서 삭제하는 방법
            GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
            foreach (Transform child in gridPanel.transform)
            {
                Managers.Managers.Resource.Destroy(child.gameObject);
            }

            // 실제 인벤토리 정보를 참고해서
            for (int i = 0; i < 10; i++)
            {
                GameObject item = Managers.Managers.UI.MakeSubItem<UI_Inven_Item>(parent: gridPanel.transform).gameObject;

                // item.GetOrAddComponent<>
                //UI_Inven_Item invenItem = Util.GetOrAddComponent<UI_Inven_Item>(item);
                UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
                invenItem.SetInfo($"검{i}번");
            }
        }
    }
}