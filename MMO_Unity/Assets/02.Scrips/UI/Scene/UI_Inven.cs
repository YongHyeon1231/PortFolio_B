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

            // �ڽ��� ã�Ƽ� �����ϴ� ���
            GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
            foreach (Transform child in gridPanel.transform)
            {
                Managers.Managers.Resource.Destroy(child.gameObject);
            }

            // ���� �κ��丮 ������ �����ؼ�
            for (int i = 0; i < 10; i++)
            {
                GameObject item = Managers.Managers.Resource.Instantiate("UI/Scene/UI_Inven_Item");
                item.transform.SetParent(gridPanel.transform);

                UI_Inven_Item invenItem = Util.GetOrAddComponent<UI_Inven_Item>(item);
                invenItem.SetInfo($"��{i}��");
            }
        }
    }
}