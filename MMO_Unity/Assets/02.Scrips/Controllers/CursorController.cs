using RPG.Managers;
using RPG.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private Texture2D _attackIcon;
    private Texture2D _handIcon;
    private Texture2D _lootIcon;

    private Vector2 _attackIconHotSpot;
    private Vector2 _handIconHotSpot;
    private Vector2 _lootIconHotSpot;

    private enum CursorType
    {
        None,
        Attack,
        Hand,
    }

    CursorType _cursorType = CursorType.None;

    private int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    void Start()
    {
        _attackIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        _handIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Hand");
        _lootIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Loot");

        _attackIconHotSpot = new Vector2(_attackIcon.width / 5, _attackIcon.height / 10);
        _handIconHotSpot = new Vector2(_attackIcon.width / 3, _attackIcon.height / 10);
        _lootIconHotSpot = new Vector2(_attackIcon.width / 3, _attackIcon.height / 10);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if (_cursorType != CursorType.Attack)
                {
                    Cursor.SetCursor(_attackIcon, _attackIconHotSpot, CursorMode.Auto);
                    _cursorType = CursorType.Attack;
                }
            }
            else if (hit.collider.gameObject.layer == (int)Define.Layer.Block)
            {
                if (_cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(_handIcon, _handIconHotSpot, CursorMode.Auto);
                    _cursorType = CursorType.Hand;
                }
            }
            else if (hit.collider.gameObject.layer == (int)Define.Layer.Ground)
            {
                if (_cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(_handIcon, _handIconHotSpot, CursorMode.Auto);
                    _cursorType = CursorType.Hand;
                }
            }
        }
    }
}
