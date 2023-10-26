using RPG.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Managers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Define.CameraMode _mode = Define.CameraMode.QuarterView;
        [SerializeField] private Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);
        [SerializeField] private GameObject _player = null;

        public void SetPlayer(GameObject player) { _player = player; }

        void Start()
        {

        }

        void LateUpdate()
        {
            if (_mode == Define.CameraMode.QuarterView)
            {
                if (_player.IsValid() == false)
                {
                    return;
                }

                RaycastHit hit;
                if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
                {
                    float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                    transform.position = _player.transform.position + _delta.normalized * dist + Vector3.up * 1.5f;
                }
                else
                {
                    transform.position = _player.transform.position + _delta;
                    transform.LookAt(_player.transform, Vector3.up * 1.5f);
                }
            }
        }

        public void SetQuaterView (Vector3 delta)
        {
            _mode = Define.CameraMode.QuarterView;
            _delta = delta;
        }
    }
}