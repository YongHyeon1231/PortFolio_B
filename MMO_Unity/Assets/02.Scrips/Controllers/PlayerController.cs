using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Managers;
using RPG.Utils;
using RPG.UI;
using RPG.UI.PopUp;
using UnityEngine.AI;
using RPG.Contents;

public class PlayerController : MonoBehaviour
{
    private PlayerStat _stat;

    private Vector3 _destPos;

    void Start()
    {
        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        //Managers.Resource.Instantiate("UI/UI_Button");

        
    }

    // GameObject (Player)
    // Transfrom
    // PlayerController (*)


    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Skill,
        Channelling,
        Jumping,
        Falling,
    }

    PlayerState _state = PlayerState.Idle;

    void Update()
    {
        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Channelling:
                break;
            case PlayerState.Jumping:
                break;
            case PlayerState.Falling:
                break;
            default:
                break;
        }
    }

    private void UpdateDie()
    {
        // 아무것도 못함
    }

    private void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            // TODO
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();

            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0.0f, dir.magnitude);
            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                _state = PlayerState.Idle;
                return;
            }

            //transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }

        // 애니메이션 처리
        Animator anim = GetComponent<Animator>();
        // 현재 게임 상태에 대한 정보를 넘겨준다. 
        // 움직이고 있으면 현재의 스피드를 넘겨준다.
        anim.SetFloat("speed", _stat.MoveSpeed);
    }

    private void UpdateIdle()
    {
        Animator anim = GetComponent<Animator>();

        anim.SetFloat("speed", 0.0f);
    }

    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    private void OnMouseClicked(Define.MouseEvent evt)
    {
        //if (evt != Define.MouseEvent.Click)
        //    return;
        if (_state == PlayerState.Die)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            _destPos = hit.point;
            _state = PlayerState.Moving;
            //Debug.Log($"Raycast Camera @ {hit.collider.gameObject.tag}");

            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                Debug.Log("Monster Click");
            }
            else if (hit.collider.gameObject.layer == (int)Define.Layer.Ground)
            {
                Debug.Log("Ground Click");
            }
        }
    }
}