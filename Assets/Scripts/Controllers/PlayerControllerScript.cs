using Assets.Scripts.Constants;
using Assets.Scripts.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _tail;

    private IList<Vector3> _deltaPosition;

    private IList<Rigidbody> _nodes;
    private Rigidbody _head => _nodes[0];

    private Rigidbody _body;

    private Transform _tr;

    private bool _createNodeAtTail;

    private float _counter;

    private bool _canMove;

    [HideInInspector]
    public EPlayerDirection PlayerDirection { get; set; }

    [HideInInspector]
    public float StepLength { get; set; } = 0.2f;

    [HideInInspector]
    public float MovementFrequency { get; set; } = 0.1f;


    void Awake()
    {
        _tr = transform;
        _body = GetComponent<Rigidbody>();
        InitSnakeNodes();
        InitPlayer();

        _deltaPosition = new List<Vector3>()
        {
            new Vector3(-StepLength, 0f), // -dx .. LEFT
            new Vector3(0f, StepLength), // dy .. UP
            new Vector3(StepLength, 0f), // dx .. RIGHT
            new Vector3(0f, -StepLength) // -dy .. DOWN
        };
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovementFrequency();
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            _canMove = false;
            Move();
        }
    }

    void InitSnakeNodes()
    {
        _nodes = new List<Rigidbody>();
        _nodes.Add(_tr.GetChild(0).GetComponent<Rigidbody>());
        _nodes.Add(_tr.GetChild(1).GetComponent<Rigidbody>());
        _nodes.Add(_tr.GetChild(2).GetComponent<Rigidbody>());
    }

    void SetDirectionRamdom()
    {
        PlayerDirection = (EPlayerDirection)Random.Range(0, Enum.GetNames(typeof(EPlayerDirection)).Length);
    }

    void InitPlayer()
    {
        SetDirectionRamdom();

        switch(PlayerDirection)
        {
            case EPlayerDirection.RIGHT:
                _nodes[1].position = _nodes[0].position - new Vector3(MetricsConstants.NODE, 0f, 0f);
                _nodes[2].position = _nodes[0].position - new Vector3(MetricsConstants.NODE * 2f, 0f, 0f);
                break;

            case EPlayerDirection.LEFT:
                _nodes[1].position = _nodes[0].position + new Vector3(MetricsConstants.NODE, 0f, 0f);
                _nodes[2].position = _nodes[0].position + new Vector3(MetricsConstants.NODE * 2f, 0f, 0f);
                break;

            case EPlayerDirection.UP:
                _nodes[1].position = _nodes[0].position - new Vector3(0f, MetricsConstants.NODE, 0f);
                _nodes[2].position = _nodes[0].position - new Vector3(0f, MetricsConstants.NODE * 2f, 0f);
                break;

            case EPlayerDirection.DOWN:
                _nodes[1].position = _nodes[0].position + new Vector3(0f, MetricsConstants.NODE, 0f);
                _nodes[2].position = _nodes[0].position + new Vector3(0f, MetricsConstants.NODE * 2f, 0f);
                break;
        }
    }

    void Move()
    {
        Vector3 deltaPosition = _deltaPosition[(int)PlayerDirection];
        Vector3 parentPosition = _head.position;
        Vector3 previousPosition;

        _body.position += deltaPosition;
        _head.position += deltaPosition;

        for (int i = 1; i < _nodes.Count; i++)
        {
            previousPosition = _nodes[i].position;
            _nodes[i].position = parentPosition;
            parentPosition = previousPosition;
        }

        if(_createNodeAtTail)
        {

        }
    }

    void CheckMovementFrequency()
    {
        _counter += Time.deltaTime;

        if(_counter >= MovementFrequency)
        {
            _counter = 0f;
            _canMove = true;
        }
    }

    public void SetInputDirection(EPlayerDirection playerDirection)
    {
        if (playerDirection.Equals(EPlayerDirection.UP) && PlayerDirection.Equals(EPlayerDirection.DOWN)
            || playerDirection.Equals(EPlayerDirection.DOWN) && PlayerDirection.Equals(EPlayerDirection.UP)
            || playerDirection.Equals(EPlayerDirection.RIGHT) && PlayerDirection.Equals(EPlayerDirection.LEFT)
            || playerDirection.Equals(EPlayerDirection.LEFT) && PlayerDirection.Equals(EPlayerDirection.RIGHT)
            ) return;

        PlayerDirection = playerDirection;
        ForceMove();
    }

    private void ForceMove()
    {
        _counter = 0;
        _canMove = false;
        Move();
    }
}
