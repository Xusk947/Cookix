using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ClientController : Controller
{
    public float WaitTime { get; private set; } = 60f;
    public float WaitTimer { get; private set; } = 0f;
    private float _thinkTime = 3f;
    private FoodReciever _targetFoodReciever;

    protected NavMeshAgent _agent;

    private MeshRenderer _bodyRenderer;
    private MeshRenderer _handLeft, _handRight, _head;

    private State _state = State.Idle;
    private Vector3 _target = Vector3.zero;
    public Vector3 Target
    {
        get { return _target; }
        set
        {
            _target = value;
            if (_agent != null)
            {
                _agent.SetDestination(value);
            }
        }
    }
    protected override void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = true;

        _bodyRenderer = transform.Find("body").GetComponent<MeshRenderer>();
        ChangeClothColor();

        _head = transform.Find("head").GetComponent<MeshRenderer>();
        _handLeft = transform.Find("hand-left").GetComponent<MeshRenderer>();
        _handRight = transform.Find("hand-right").GetComponent<MeshRenderer>();
        ChangeSkinColor();

        _thinkTime += Random.Range(-2.5f, 2.5f);
    }

    protected void Update()
    {
        _animator.SetBool("IsMoving", _agent.velocity.magnitude > 0.1f);
        switch (_state)
        {
            case State.Idle:
                FindFoodReciever();
                break;
            case State.GoToReciever:
                GoToReciever();
                break;
            case State.Thinking:
                Think();
                break;
            case State.TakeAnOrder:
                TakeAnOrder();
                break;
            case State.WaitAnOrder:
                WaitAnOrder();
                break;
            case State.Angry:
                Angry();
                break;
            case State.OutFromReciever:
                GoToExit();
                break;
        }
    }
    private void GoToReciever()
    {
        if (_agent.remainingDistance > 1) return;
        _state = State.Thinking;
    }

    private void Think()
    {
        _thinkTime -= Time.deltaTime;
        if (_thinkTime < 0)
        {
            _state = State.TakeAnOrder;
        }
    }

    private void TakeAnOrder()
    {
        FoodTaskManager.Instance.CreateTask();
        _targetFoodReciever.Task = FoodTaskManager.Instance.TakeTask();
        _targetFoodReciever.Client = this;

        WaitTime *= _targetFoodReciever.Task.difficult * GameManager.Instance.rules.ClientWaitTimeMultiplayer;
        WaitTimer = WaitTime;

        _state = State.WaitAnOrder;
    }

    private void WaitAnOrder()
    {
        WaitTimer -= Time.deltaTime;
        if (WaitTimer < 0) _state = State.Angry;
    }
    
    private void Angry()
    {
        _targetFoodReciever.isEmpty = true;
        _targetFoodReciever.Client = null;
        _targetFoodReciever = null;

        Target = GameManager.Instance.clienExit.transform.position;
        _agent.SetDestination(Target);

        _state = State.OutFromReciever;
    }
    private void GoToExit()
    {
        if (_agent.remainingDistance > 1) return;
        Destroy(gameObject);
    }

    protected void FindFoodReciever()
    {
        foreach(FoodReciever foodReciever in GameManager.Instance.Recievers)
        {
            if (!foodReciever.isEmpty) continue;
            _targetFoodReciever = foodReciever;
            _targetFoodReciever.isEmpty = false;

            Target = foodReciever.transform.position + foodReciever.transform.forward;
            _agent.SetDestination(Target);

            _state = State.GoToReciever;
            return;
            
        }
    }

    public void GetAnOrder()
    {
        Target = GameManager.Instance.clienExit.transform.position;
        _agent.SetDestination(Target);
        
        _targetFoodReciever.isEmpty = true;
        _targetFoodReciever.Client = null;
        _targetFoodReciever = null;

        _state = State.OutFromReciever;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (Target != null)
        {
            Gizmos.DrawSphere(Target, 0.2f);
        }
    }

    public enum State
    {
        Idle, GoToReciever, Thinking, TakeAnOrder, WaitAnOrder, OutFromReciever, Angry
    }

    private void ChangeClothColor()
    {
        _bodyRenderer.materials[0].color = Random.ColorHSV();
    }
    private void ChangeSkinColor()
    {
        float r = Random.Range(0.6f, 1.0f);
        float g = Random.Range(0.4f, 0.8f);
        float b = Random.Range(0.2f, 0.6f);

        Color skinColor = new Color(r, g, b);
        _handLeft.material.color = skinColor;
        _handRight.material.color = skinColor;
        _head.material.color = skinColor; 
    }
}
