using RPG.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Managers
{
    public class GameManagerEx
    {

        private GameObject _player;
        private HashSet<GameObject> _monster = new HashSet<GameObject>();
        // int <-> GameObject
        //private Dictionary<int, GameObject> _players = new Dictionary<int, GameObject>();
        //private Dictionary<int, GameObject> _monsters = new Dictionary<int, GameObject>();

        public GameObject GetPlayer() { return _player; }

        public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
        {
            GameObject go = Managers.Resource.Instantiate(path, parent);

            switch (type)
            {
                case Define.WorldObject.Player:
                    {
                        _player = go;
                    }
                    break;
                case Define.WorldObject.Monster:
                    {
                        _monster.Add(go);
                    }
                    break;
                default:
                    break;
            }

            return go;
        }

        public Define.WorldObject GetWorldObjectType(GameObject go)
        {
            BaseController bc = go.GetComponent<BaseController>();

            if (bc == null)
                return Define.WorldObject.Unknown;

            return bc.WorldObjectType;
        }

        public void Despawn(GameObject go)
        {
            Define.WorldObject type = GetWorldObjectType(go);

            switch (type)
            {
                case Define.WorldObject.Unknown:
                    break;
                case Define.WorldObject.Player:
                    {
                        if (_player == go)
                        {
                            _player = null;
                        }
                    }
                    break;
                case Define.WorldObject.Monster:
                    {
                        if (_monster.Contains(go))
                        {
                            _monster.Remove(go);
                        }
                    }
                    break;
                default:
                    break;
            }

            Managers.Resource.Destroy(go);
        }
    }
}