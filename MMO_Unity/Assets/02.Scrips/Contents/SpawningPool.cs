using RPG.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Contents
{
    public class SpawningPool : MonoBehaviour
    {
        [SerializeField]
        int _monsterCount = 0;
        int _reserveCount = 0;

        [SerializeField]
        int _keepMonsterCount = 0;

        [SerializeField]
        Vector3 _spawnPos;
        [SerializeField]
        float _spwanRadius = 15.0f;
        [SerializeField]
        float _spawnTime = 5.0f;

        public void AddMonsterCount(int value) { _monsterCount += value; }
        public void SetKeepMonsterCount(int count) {  _keepMonsterCount = count; }

        void Start()
        {
            Managers.Managers.Game.OnSpawnEvent -= AddMonsterCount;
            Managers.Managers.Game.OnSpawnEvent += AddMonsterCount;
        }

        void Update()
        {
            while (_reserveCount + _monsterCount < _keepMonsterCount)
            {
                StartCoroutine("ReserveSpawn");
            }
        }

        private IEnumerator ReserveSpawn()
        {
            _reserveCount++;
            // Wait
            yield return new WaitForSeconds(Random.Range(1.0f, _spawnTime));
            // Spawn
            GameObject obj = Managers.Managers.Game.Spawn(Define.WorldObject.Monster, "Enemy/SkeletonWarriorUnity");

            // 갈 수 있는 영역인지 체크 하기
            NavMeshAgent nma = obj.GetOrAddComponent<NavMeshAgent>();

            // 길을 찾을 때까지 무한대로 실행
            // 랜덤 위치로 이동해주기
            Vector3 randPos;
            while (true)
            {
                // 랜덤으로 방향 벡터 뽑아옴
                Vector3 randDir = Random.insideUnitSphere * Random.Range(1.0f, _spwanRadius);
                // 땅 밑으로 내려가지 않게
                randDir.y = 0;
                // 랜덤 위치 = _spawnPosition + 원형으로 랜덤좌표 뽑아온것
                randPos = _spawnPos + randDir;

                // 갈 수 있나
                NavMeshPath path = new NavMeshPath();
                // return true가 나오면 그냥 갈 수 있는 지역이니까 바로 break
                if (nma.CalculatePath(randPos, path))
                    break;
            }

            obj.transform.position = randPos;
            _reserveCount--;
        }
    }
}