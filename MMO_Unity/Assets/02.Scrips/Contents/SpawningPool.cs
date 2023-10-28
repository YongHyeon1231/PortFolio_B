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

            // �� �� �ִ� �������� üũ �ϱ�
            NavMeshAgent nma = obj.GetOrAddComponent<NavMeshAgent>();

            // ���� ã�� ������ ���Ѵ�� ����
            // ���� ��ġ�� �̵����ֱ�
            Vector3 randPos;
            while (true)
            {
                // �������� ���� ���� �̾ƿ�
                Vector3 randDir = Random.insideUnitSphere * Random.Range(1.0f, _spwanRadius);
                // �� ������ �������� �ʰ�
                randDir.y = 0;
                // ���� ��ġ = _spawnPosition + �������� ������ǥ �̾ƿ°�
                randPos = _spawnPos + randDir;

                // �� �� �ֳ�
                NavMeshPath path = new NavMeshPath();
                // return true�� ������ �׳� �� �� �ִ� �����̴ϱ� �ٷ� break
                if (nma.CalculatePath(randPos, path))
                    break;
            }

            obj.transform.position = randPos;
            _reserveCount--;
        }
    }
}