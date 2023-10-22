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

    private int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    private PlayerStat _stat;
    private Vector3 _destPos;

    private GameObject _lockTarget;

    [SerializeField]
    private PlayerState _state = PlayerState.Idle;

    public PlayerState State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case PlayerState.Die:
                    {
                    }
                    break;
                case PlayerState.Moving:
                    {
                        anim.CrossFade("RUN", 0.1f);
                    }
                    break;
                case PlayerState.Idle:
                    {
                        anim.CrossFade("WAIT", 0.1f);
                    }
                    break;
                case PlayerState.Skill:
                    {
                        anim.CrossFade("ATTACK", 0.1f, -1, 0);
                    }
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
    }

    void Start()
    {
        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;

        //Managers.Resource.Instantiate("UI/UI_Button");


    }

    // GameObject (Player)
    // Transfrom
    // PlayerController (*)

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
            case PlayerState.Skill:
                UpdateSkill();
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
        // 몬스터가 내 사정거리보다 가까우면 공격
        if (_lockTarget != null)
        {
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= 1) // (distance <= 내 공격사거리)
            {
                State = PlayerState.Skill;
                return;
            }
        }    

        // 이동
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = PlayerState.Idle;
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
                if (Input.GetMouseButton(0) == false)
                    State = PlayerState.Idle;
                return;
            }

            //transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }
    }

    private void UpdateIdle()
    {
        
    }

    private void UpdateSkill()
    {
        if (_lockTarget != null)
        {
            //나에서 몬스터로 가는 방향벡터가 나옵니다.
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            //뚝뚝 끊기는 형상을 막기 위해 Lerp를 써줍니다.
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    private void OnHitEvent()
    {
        Debug.Log("OnHitEvent");

        if (_stopSkill)
        {
            State = PlayerState.Idle;
        }
        else
        {
            State = PlayerState.Skill;
        }
    }

    private bool _stopSkill = false;
    private void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case PlayerState.Die:
                break;
            case PlayerState.Moving:
                {
                    OnMouseEvent_IdleRun(evt);
                }
                break;
            case PlayerState.Idle:
                {
                    OnMouseEvent_IdleRun(evt);
                }
                break;
            case PlayerState.Skill:
                {
                    if (evt == Define.MouseEvent.PointerUp)
                    {
                        _stopSkill = true;
                    }
                }
                break;
            case PlayerState.Channelling:
                break;
            case PlayerState.Jumping:
                break;
            case PlayerState.Falling:
                break;
        }
    }
    private void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        switch (evt)
        {
            case Define.MouseEvent.Press:
                {
                    //Press 상태일때 목적지 위치가 바뀌지 않게
                    if (_lockTarget == null && raycastHit)
                    {
                        _destPos = hit.point;
                    }
                }
                break;
            case Define.MouseEvent.PointerDown:
                {
                    if (raycastHit)
                    {
                        _destPos = hit.point;
                        State = PlayerState.Moving;
                        _stopSkill = false;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                        {
                            _lockTarget = hit.collider.gameObject;
                            Debug.Log("Monster Click");
                        }
                        else if (hit.collider.gameObject.layer == (int)Define.Layer.Ground)
                        {
                            _lockTarget = null;
                            Debug.Log("Ground Click");
                        }
                        else
                        {
                            _lockTarget = null;
                        }
                    }
                }
                break;
            case Define.MouseEvent.PointerUp:
                {
                    _stopSkill = true;
                }
                break;
            case Define.MouseEvent.Click:
                break;
            default:
                break;
        }
    }
}