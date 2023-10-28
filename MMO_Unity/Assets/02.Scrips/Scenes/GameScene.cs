using RPG.Contents;
using RPG.Data;
using RPG.Managers;
using RPG.UI;
using RPG.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Scenes
{
    public class GameScene : BaseScene
    {
        protected override void Init()
        {
            base.Init();

            SceneType = Define.Scene.Game;
            //Managers.Managers.UI.ShowSceneUI<UI_Inven>();
            Dictionary<int, Data.Stat> dict = Managers.Managers.Data.StatDict;
            gameObject.GetOrAddComponent<CursorController>();

            GameObject player = Managers.Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
            Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

            //Managers.Managers.Game.Spawn(Define.WorldObject.Monster, "Enemy/SkeletonWarriorUnity");
            GameObject go = new GameObject { name = "SpawningPool" };
            SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
            pool.SetKeepMonsterCount(5);
        }


        public override void Clear()
        {

        }
    }
}